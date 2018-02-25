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
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video;

namespace PiRexBot
{
    public partial class MainForm : Form
    {
        HttpCommandsThread commandsThread = new HttpCommandsThread( null );

        public MainForm( )
        {
            InitializeComponent( );

            ipAddressBox.Text = "192.168.0.12"; //"127.0.0.1";

            commandsThread.HttpCommandCompletion += commandsThread_HttpCommandCompletion;
        }

        private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            Disconnect( );
        }

        private void exitMenuItem_Click( object sender, EventArgs e )
        {
            this.Close( );
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
            string      baseAddress = string.Format( "http://{0}:{1}", ipAddressBox.Text.Trim( ), portUpDown.Value );
            MJPEGStream videoSource = new MJPEGStream( baseAddress + "/camera/mjpeg" );

            videoSourcePlayer.VideoSource = videoSource;
            videoSourcePlayer.Start( );

            commandsThread.BaseAddress = baseAddress;
            commandsThread.Start( );

            System.Diagnostics.Debug.WriteLine( "Version command, ID: {0}", commandsThread.EnqueueGetRequest( "/version" ) );

            System.Diagnostics.Debug.WriteLine( "Motors command, ID: {0}", commandsThread.EnqueuePostRequest( "/motors/config", "{\"leftPower\":0, \"rightPower\":0}" ) );
             
        }

        private void disconnectButton_Click( object sender, EventArgs e )
        {
            Disconnect( );
        }

        void commandsThread_HttpCommandCompletion( object sender, HttpCommandEventArgs eventArgs )
        {
            System.Diagnostics.Debug.WriteLine( "Coomand completed: {0}, {1}", eventArgs.Id, eventArgs.Success );
            System.Diagnostics.Debug.WriteLine( eventArgs.Message );
        }
    }
}
