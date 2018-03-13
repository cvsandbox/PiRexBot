using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiRexBot
{
    public partial class AboutBox : Form
    {
        public AboutBox( )
        {
            InitializeComponent( );

            // set links
            webLinkLabel.Links.Add( 0, webLinkLabel.Text.Length, "http://github.com/cvsandbox/PiRexBot" );
            emailLinkLabel.Links.Add( 0, emailLinkLabel.Text.Length, "mailto:cvsandbox@gmail.com" );
        }

        // Follow the clicked link label
        private void label_LinkClicked( object sender, LinkLabelLinkClickedEventArgs e )
        {
            System.Diagnostics.Process.Start( e.Link.LinkData.ToString( ) );
        }
    }
}
