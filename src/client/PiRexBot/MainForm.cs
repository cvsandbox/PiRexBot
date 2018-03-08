/*
    PiRexBot .NET Client - remote controlling of a RaspberryPi based PiRex robot.

    Copyright (C) 2018, cvsandbox, cvsandbox@gmail.com

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License along
    with this program; if not, write to the Free Software Foundation, Inc.,
    51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using AForge.Video;

namespace PiRexBot
{
    public partial class MainForm : Form
    {
        HttpCommandsThread commandsThread  = new HttpCommandsThread( null );
        bool               connected       = false;

        Stopwatch          fpsStopWatch    = null;

        bool               distanceMeasurementAvailable = false;
        Thread             distanceMeasurementThread    = null;
        ManualResetEvent   stopDistanceMeasurementEvent = null;

        int                leftMotorPower  = 0;
        int                rightMotorPower = 0;
        bool               mouseDownOnControlButton = false;

        uint               notifyCommandId = 0;
        bool               commandSuccess  = false;
        string             commandMessage  = null;
        AutoResetEvent     commandProcessed;

        const int          commandTimeout  = 10000;

        public MainForm( )
        {
            InitializeComponent( );

            videoModeCombo.SelectedIndex = 0;
            EnableConnectionControls( false );

            ipAddressBox.Text = "192.168.0.12"; //"127.0.0.1";

            // register for HTTP request completion events
            commandsThread.HttpCommandCompletion += commandsThread_HttpCommandCompletion;

            // event used for signalling about complete requested
            commandProcessed = new AutoResetEvent( false );

            //  event used for signalling distance measurement thread to stop
            stopDistanceMeasurementEvent = new ManualResetEvent( false );
        }

        // Main form closing - close any connections
        private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            Disconnect( );
        }

        // Show an error message box
        private void ErrorBox( string message )
        {
            MessageBox.Show( this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand );
        }

        // Delegates to enable async calls for setting controls properties
        private delegate void SetTextCallback( System.Windows.Forms.Control control, string text );

        // Thread safe updating of control's text property
        private void SetText( System.Windows.Forms.Control control, string text )
        {
            if ( control.InvokeRequired )
            {
                SetTextCallback d = new SetTextCallback( SetText );
                Invoke( d, new object[] { control, text } );
            }
            else
            {
                control.Text = text;
            }
        }

        // Enable/disable controls available on successful connection.
        // Do the opposite for controls controls required to make connection.
        private void EnableConnectionControls( bool enable )
        {
            // controls available after successful connection
            botControlGroup.Enabled = enable;

            // controls required to configure/make connection
            ipAddressBox.Enabled   = !enable;
            portUpDown.Enabled     = !enable;
            loginBox.Enabled       = !enable;
            passwordBox.Enabled    = !enable;
            videoModeCombo.Enabled = !enable;
        }

        // File->Exit - exit the application
        private void exitMenuItem_Click( object sender, EventArgs e )
        {
            this.Close( );
        }

        // Enqueue GET request and wait for its completion/response - check it is successful and looks valid
        private void WaitRequestCompletion( string url )
        {
            notifyCommandId = commandsThread.EnqueueGetRequest( url );
            if ( !commandProcessed.WaitOne( commandTimeout ) )
            {
                throw new ApplicationException( "No command response" );
            }

            if ( !commandSuccess )
            {
                throw new ApplicationException( commandMessage );
            }

            if ( !commandMessage.Contains( "{\"status\":" ) )
            {
                throw new ApplicationException( "Response status is missing - not a PiRex bot" );
            }
        }

        // Check connectivity to PiRex bot - check common URLs are accessible
        private bool CheckPiRexConnectivity( )
        {
            bool ret = false;

            try
            {
                // check if version information can be retrieved
                WaitRequestCompletion( "/version" );

                if ( !commandMessage.Contains( "\"product\":\"pirexbot\"" ) )
                {
                    throw new ApplicationException( "Remote side does not look like a PiRex bot" );
                }

                // check if robot's information can be retrieved, which requires viewer access rights
                WaitRequestCompletion( "/info" );

                // check if the bot is equipped with distance sensor
                if ( commandMessage.Contains( "\"providesDistance\":\"true\"" ) )
                {
                    distanceMeasurementAvailable = true;
                }

                // check if motors' configuration can be retrieved, which requires configuration access rights
                WaitRequestCompletion( "/motors/config" );

                ret = true;
            }
            catch ( ApplicationException ex )
            {
                ErrorBox( "Failed connecting to PiRex bot:\n\n" + ex.Message );
            }

            return ret;
        }

        // Connect to a PiRex bot
        private bool Connect( )
        {
            string strIpAddress = ipAddressBox.Text.Trim( );
            string strLogin     = loginBox.Text.Trim( );
            string strPassword  = passwordBox.Text.Trim( );
            bool   ret          = false;

            if ( ( string.IsNullOrEmpty( strIpAddress ) ) ||
                 ( !Regex.IsMatch( strIpAddress, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$" ) ) )
            {
                ErrorBox( "The specified IP address does not look valid." );
            }
            else
            {
                string  baseAddress = string.Format( "http://{0}:{1}", strIpAddress, portUpDown.Value );

                commandsThread.BaseAddress = baseAddress;
                commandsThread.Login       = strLogin;
                commandsThread.Password    = strPassword;

                commandsThread.Start( );

                ret = CheckPiRexConnectivity( );

                if ( !ret )
                {
                    commandsThread.SignalToStop( );
                    commandsThread.WaitForStop( );
                }
                else
                {
                    if ( videoModeCombo.SelectedIndex == 0 )
                    {
                        MJPEGStream videoSource = new MJPEGStream( baseAddress + "/camera/mjpeg" );

                        videoSource.Login    = strLogin;
                        videoSource.Password = strPassword;

                        videoSourcePlayer.VideoSource = videoSource;
                    }
                    else
                    {
                        JPEGStream videoSource = new JPEGStream( baseAddress + "/camera/jpeg" );

                        videoSource.FrameInterval = 33;
                        videoSource.Login         = strLogin;
                        videoSource.Password      = strPassword;

                        videoSourcePlayer.VideoSource = videoSource;
                    }

                    videoSourcePlayer.Start( );

                    // start distance measurement thread
                    distanceLabel.Text = ( distanceMeasurementAvailable ) ? string.Empty : "Unavailable";
                    if ( distanceMeasurementAvailable )
                    {
                        stopDistanceMeasurementEvent.Reset( );
                        distanceMeasurementThread = new Thread( new ThreadStart( DistanceMeasurementThreadHandler ) );
                        distanceMeasurementThread.Start( );
                    }

                    // start timer for reporting video FPS
                    fpsTimer.Start( );

                    connectionStatusLabel.Text = "Connected to PiRex bot";
                }
            }

            EnableConnectionControls( ret );

            return ret;
        }

        // Disconnect from PiRex bot
        private void Disconnect( )
        {
            fpsTimer.Stop( );

            stopDistanceMeasurementEvent.Set( );
            distanceMeasurementThread = null;

            commandsThread.SignalToStop( );
            commandsThread.WaitForStop( );

            videoSourcePlayer.SignalToStop( );
            videoSourcePlayer.WaitForStop( );
            videoSourcePlayer.VideoSource = null;

            EnableConnectionControls( false );
            distanceLabel.Text         = string.Empty;
            fpsStatusLabel.Text        = string.Empty;
            connectionStatusLabel.Text = string.Empty;
        }

        // Connect/Disconnect button clicked
        private void connectButton_Click( object sender, EventArgs e )
        {
            connectButton.Enabled = false;

            if ( !connected )
            {
                if ( Connect( ) )
                {
                    connected = true;
                    connectButton.Text = "&Disconnect";
                }
            }
            else
            {
                Disconnect( );

                connected = false;
                connectButton.Text = "&Connect";
            }

            connectButton.Enabled = true;
        }

        // Event handler for complete HTTP request
        void commandsThread_HttpCommandCompletion( object sender, HttpCommandEventArgs eventArgs )
        {
            System.Diagnostics.Debug.WriteLine( "Command completed: {0}, {1}", eventArgs.Id, eventArgs.Success );
            System.Diagnostics.Debug.WriteLine( eventArgs.Message );

            if ( notifyCommandId == eventArgs.Id )
            {
                notifyCommandId = 0;
                commandSuccess  = eventArgs.Success;
                commandMessage  = eventArgs.Message;
                commandProcessed.Set( );
            }
        }

        // Move robot forward
        private void moveForwardButton_MouseDown( object sender, MouseEventArgs e )
        {
            ControlButtonDown( 100, 100 );
        }

        // Move robot backward
        private void moveBackwardButton_MouseDown( object sender, MouseEventArgs e )
        {
            ControlButtonDown( -100, -100 );
        }

        // Move robot left
        private void moveLeftButton_MouseDown( object sender, MouseEventArgs e )
        {
            ControlButtonDown( 25, 100 );
        }

        // Move robot right
        private void moveRightButton_MouseDown( object sender, MouseEventArgs e )
        {
            ControlButtonDown( 100, 25 );
        }

        // Rotate robot left
        private void rotateLeftButton_MouseDown( object sender, MouseEventArgs e )
        {
            ControlButtonDown( -100, 100 );
        }

        // Rotate robot right
        private void rotateRightButton_MouseDown( object sender, MouseEventArgs e )
        {
            ControlButtonDown( 100, -100 );
        }

        // Mouse up for one of the motors' control buttons
        private void controlButton_MouseUp( object sender, MouseEventArgs e )
        {
            ControlButtonUp( );
        }

        // Mouse left from one of the motors' control button
        private void controlButton_MouseLeave( object sender, EventArgs e )
        {
            if ( mouseDownOnControlButton )
            {
                ControlButtonUp( );
            }
        }

        // Handle mouse down for control buttons
        private void ControlButtonDown( int leftPower, int rightPower )
        {
            mouseDownOnControlButton = true;
            leftMotorPower  = leftPower;
            rightMotorPower = rightPower;
            UpdateMotorsState( );
            updateMotorsStateTimer.Enabled = true;
        }

        // Handle mouse up for control buttons
        private void ControlButtonUp( )
        {
            updateMotorsStateTimer.Enabled = false;

            mouseDownOnControlButton = false;
            leftMotorPower  = 0;
            rightMotorPower = 0;
            UpdateMotorsState( );
        }

        // Timer for updating motors' state
        private void updateMotorsStateTimer_Tick( object sender, EventArgs e )
        {
            // keep sending motors' state updates - PiRex will stop for safety, if no updates come in half a second
            UpdateMotorsState( );
        }

        // Send motors' state update to PiRex bot
        private void UpdateMotorsState( )
        {
            string postData = string.Format( "{{\"leftPower\":{0}, \"rightPower\":{1}}}", leftMotorPower, rightMotorPower );

            commandsThread.EnqueuePostRequest( "/motors/config", postData, true );
        }

        // Handler of the thread to query distance measurements
        void DistanceMeasurementThreadHandler( )
        {
            while ( !stopDistanceMeasurementEvent.WaitOne( 50, false ) )
            {
                string strDistance = "?";

                try
                {
                    WaitRequestCompletion( "/distance" );

                    int valueIndex = commandMessage.IndexOf( "\"medianDistance\":\"" );

                    if ( valueIndex != -1 )
                    {
                        valueIndex += 18;

                        int delimiterIndex = commandMessage.IndexOf( '"', valueIndex );

                        if ( delimiterIndex != -1 )
                        {
                            float distance = float.Parse( commandMessage.Substring( valueIndex, delimiterIndex - valueIndex ) );

                            strDistance = distance.ToString( "F2" ) + " cm";
                        }
                    }
                }
                catch ( Exception )
                {

                }

                SetText( distanceLabel, strDistance );
            }
        }

        // Update video FPS info
        private void fpsTimer_Tick( object sender, EventArgs e )
        {
            IVideoSource videoSource = videoSourcePlayer.VideoSource;

            if ( videoSource != null )
            {
                // get number of frames since the last timer tick
                int framesReceived = videoSource.FramesReceived;

                if ( fpsStopWatch == null )
                {
                    fpsStopWatch = new Stopwatch( );
                    fpsStopWatch.Start( );
                }
                else
                {
                    fpsStopWatch.Stop( );

                    float fps = 1000.0f * framesReceived / fpsStopWatch.ElapsedMilliseconds;
                    fpsStatusLabel.Text = fps.ToString( "F2" ) + " fps";

                    fpsStopWatch.Reset( );
                    fpsStopWatch.Start( );
                }
            }
        }
    }
}
