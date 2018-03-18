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
using System.Windows.Forms;

namespace PiRexBot
{
    public delegate void PropertyValueChangeHandler( string name, string value );

    public partial class CameraOptionsForm : Form
    {
        bool notifyPropertyChanged = true;

        // Mapping from configuration track bar to corresponding edit box showing the value
        Dictionary<TrackBar, TextBox> trackBar2EditMapping = new Dictionary<TrackBar, TextBox>( );

        // Mapping from configuration combo box to list of configuration values
        Dictionary<ComboBox, List<string>> combo2ValuesMapping = new Dictionary<ComboBox, List<string>>( );

        // List of possible values for Automatic Wite Balance, Exposure Mode,
        // Exposure Metering Mod and Image Effect properties
        List<string> awbModes      = new List<string>( );
        List<string> expModes      = new List<string>( );
        List<string> meteringModes = new List<string>( );
        List<string> imageEffects  = new List<string>( );

        public event PropertyValueChangeHandler PropertyValueChanged;

        public CameraOptionsForm( )
        {
            InitializeComponent( );

            trackBar2EditMapping[brightnessTrackBar] = brightnessBox;
            trackBar2EditMapping[contrastTrackBar]   = contrastBox;
            trackBar2EditMapping[saturationTrackBar] = saturationBox;
            trackBar2EditMapping[sharpnessTrackBar]  = sharpnessBox;

            combo2ValuesMapping[whiteBalanceCombo] = awbModes;
            combo2ValuesMapping[exposureModeCombo] = expModes;
            combo2ValuesMapping[meteringModeCombo] = meteringModes;
            combo2ValuesMapping[imageEffectCombo]  = imageEffects;

            // add values for Automatic White Balance, Exposure Mode,
            // Exposure Metering Mod and Image Effect properties
            awbModes.AddRange( "Off,Auto,Sunlight,Cloudy,Shade,Tungsten,Fluorescent,Incandescent,Flash,Horizon".Split( ',' ) );
            expModes.AddRange( "Off,Auto,Night,NightPreview,Backlight,Spotlight,Sports,Snow,Beach,VeryLong,FixedFps,AntiShake,Fireworks".Split( ',' ) );
            meteringModes.AddRange( "Average,Spot,Backlit,Matrix".Split( ',' ) );
            imageEffects.AddRange( "None,Negative,Solarize,Sketch,Denoise,Emboss,OilPaint,Hatch,Gpen,Pastel,WaterColor,Film,Blur,Saturation,ColorSwap,WashedOut,Posterise,ColorPoint,ColorBalance,Cartoon".Split( ',' ) );
        }

        // Helper function to extract value of the given setting out of JSON response coming from PiRex bot
        private string GetConfigValue( string jsonSettings, string name )
        {
            string value  = null;
            string toFind = "\"" + name + "\":\"";

            int startIndex = jsonSettings.IndexOf( toFind );
            if ( startIndex != -1 )
            {
                startIndex += toFind.Length;

                int endIndex = jsonSettings.IndexOf( '"', startIndex );

                if ( endIndex != -1 )
                {
                    value = jsonSettings.Substring( startIndex, endIndex - startIndex );
                }
            }

            System.Diagnostics.Debug.WriteLine( "got value, {0} : {1}", name, value );

            return value;
        }

        // Display current setting of the camera
        public void SetCurrentSettings( string jsonSettings )
        {
            int  intValue  = 0;

            notifyPropertyChanged = false;

            if ( int.TryParse( GetConfigValue( jsonSettings, "brightness" ), out intValue ) )
            {
                brightnessTrackBar.Value = intValue;
                brightnessBox.Text = intValue.ToString( );
            }
            if ( int.TryParse( GetConfigValue( jsonSettings, "contrast" ), out intValue ) )
            {
                contrastTrackBar.Value = intValue;
                contrastBox.Text = intValue.ToString( );
            }
            if ( int.TryParse( GetConfigValue( jsonSettings, "saturation" ), out intValue ) )
            {
                saturationTrackBar.Value = intValue;
                saturationBox.Text = intValue.ToString( );
            }
            if ( int.TryParse( GetConfigValue( jsonSettings, "sharpness" ), out intValue ) )
            {
                sharpnessTrackBar.Value = intValue;
                sharpnessBox.Text = intValue.ToString( );
            }
            if ( int.TryParse( GetConfigValue( jsonSettings, "hflip" ), out intValue ) )
            {
                horizontalFlipCheck.Checked = ( intValue != 0 );
            }
            if ( int.TryParse( GetConfigValue( jsonSettings, "vflip" ), out intValue ) )
            {
                verticalFlipCheck.Checked = ( intValue != 0 );
            }

            whiteBalanceCombo.SelectedIndex = awbModes.IndexOf( GetConfigValue( jsonSettings, "awb" ) );
            exposureModeCombo.SelectedIndex = expModes.IndexOf( GetConfigValue( jsonSettings, "expmode" ) );
            meteringModeCombo.SelectedIndex = meteringModes.IndexOf( GetConfigValue( jsonSettings, "expmeteringmode" ) );
            imageEffectCombo.SelectedIndex  = imageEffects.IndexOf( GetConfigValue( jsonSettings, "effect" ) );

            notifyPropertyChanged = true;
        }

        // Value of some setting's track bar has changed
        private void settingsTrackBar_ValueChanged( object sender, EventArgs e )
        {
            TrackBar track    = sender as TrackBar;
            string   strValue = track.Value.ToString( );

            trackBar2EditMapping[track].Text = strValue;

            if ( ( notifyPropertyChanged ) && ( PropertyValueChanged != null ) )
            {
                PropertyValueChanged( track.Tag as string, strValue );
            }
        }

        // Checked value of some setting's check box has changed
        private void settingsCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            CheckBox check    = sender as CheckBox;
            int      intValue = ( check.Checked ) ? 1 : 0;

            if ( ( notifyPropertyChanged ) && ( PropertyValueChanged != null ) )
            {
                PropertyValueChanged( check.Tag as string, intValue.ToString( ) );
            }
        }

        // Selection index has changed in one of the settings combo boxes
        private void settingsComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            ComboBox combo = sender as ComboBox;

            if ( ( notifyPropertyChanged ) && ( PropertyValueChanged != null ) )
            {
                PropertyValueChanged( combo.Tag as string, combo2ValuesMapping[combo][combo.SelectedIndex] );
            }
        }

        // Reset settings to default values
        private void resetButton_Click( object sender, EventArgs e )
        {
            if ( PropertyValueChanged != null )
            {
                brightnessTrackBar.Value        = 50;
                contrastTrackBar.Value          = 0;
                saturationTrackBar.Value        = 0;
                sharpnessTrackBar.Value         = 0;
                horizontalFlipCheck.Checked     = false;
                verticalFlipCheck.Checked       = false;
                whiteBalanceCombo.SelectedIndex = 1;
                exposureModeCombo.SelectedIndex = 1;
                meteringModeCombo.SelectedIndex = 0;
                imageEffectCombo.SelectedIndex  = 0;
            }
        }
    }
}
