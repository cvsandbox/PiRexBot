using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video;

namespace PiRexBot
{
    public partial class MainForm : Form
    {
        public MainForm( )
        {
            InitializeComponent( );
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
            videoSourcePlayer.Stop( );
            videoSourcePlayer.VideoSource = null;
        }

        private void connectButton_Click( object sender, EventArgs e )
        {
            MJPEGStream videoSource = new MJPEGStream( string.Format( "http://{0}:{1}/camera/mjpeg",
                                                       ipAddressBox.Text.Trim( ), portUpDown.Value ) );

            videoSourcePlayer.VideoSource = videoSource;
            videoSourcePlayer.Start( );
        }

        private void disconnectButton_Click( object sender, EventArgs e )
        {
            Disconnect( );
        }


    }
}
