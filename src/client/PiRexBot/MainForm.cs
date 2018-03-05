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
using AForge.Video;

namespace PiRexBot
{
    public partial class MainForm : Form
    {
        HttpCommandsThread commandsThread  = new HttpCommandsThread( null );
        bool               connected       = false;

        uint               notifyCommandId = 0;
        bool               commandSuccess  = false;
        string             commandMessage  = null;
        AutoResetEvent     commandProcessed;

        const int          commandTimeout  = 10000;

        public MainForm( )
        {
            InitializeComponent( );

            videoModeCombo.SelectedIndex = 0;

            ipAddressBox.Text = "192.168.0.12"; //"127.0.0.1";

            commandsThread.HttpCommandCompletion += commandsThread_HttpCommandCompletion;

            botControlGroup.Enabled = false;

            commandProcessed = new AutoResetEvent( false );
        }

        private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            Disconnect( );
        }

        private void ErrorBox( string message )
        {
            MessageBox.Show( this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand );
        }

        private void exitMenuItem_Click( object sender, EventArgs e )
        {
            this.Close( );
        }

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
        }

        private bool CheckPiRexConnectivity( )
        {
            bool ret = false;

            try
            {
                // check if version information can be retrieved
                WaitRequestCompletion( "/version" );

                if ( ( !commandMessage.Contains( "\"status\":\"OK\"" ) ) ||
                     ( !commandMessage.Contains( "\"product\":\"pirexbot\"" ) ) )
                {
                    throw new ApplicationException( "Remote side does not look like a PiRex bot" );
                }

                // check if robot's information can be retrieved, which requires viewer access rights
                WaitRequestCompletion( "/info" );

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
                }
            }

            return ret;
        }

        private void Disconnect( )
        {
            commandsThread.SignalToStop( );
            commandsThread.WaitForStop( );

            videoSourcePlayer.SignalToStop( );
            videoSourcePlayer.WaitForStop( );
            videoSourcePlayer.VideoSource = null;
        }

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

            /*
            string      baseAddress = string.Format( "http://{0}:{1}", ipAddressBox.Text.Trim( ), portUpDown.Value );
            MJPEGStream videoSource = new MJPEGStream( baseAddress + "/camera/mjpeg" );

            videoSourcePlayer.VideoSource = videoSource;
            videoSourcePlayer.Start( );

            commandsThread.BaseAddress = baseAddress;
            commandsThread.Start( );

            System.Diagnostics.Debug.WriteLine( "Version command, ID: {0}", commandsThread.EnqueueGetRequest( "/version" ) );

            System.Diagnostics.Debug.WriteLine( "Motors command, ID: {0}", commandsThread.EnqueuePostRequest( "/motors/config", "{\"leftPower\":0, \"rightPower\":0}" ) );
            */
        }

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
    }
}
