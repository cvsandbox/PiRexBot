namespace PiRexBot
{
    partial class CameraOptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.label1 = new System.Windows.Forms.Label();
            this.brightnessTrackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.contrastTrackBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.saturationTrackBar = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.sharpnessTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.verticalFlipCheck = new System.Windows.Forms.CheckBox();
            this.horizontalFlipCheck = new System.Windows.Forms.CheckBox();
            this.imageEffectCombo = new System.Windows.Forms.ComboBox();
            this.meteringModeCombo = new System.Windows.Forms.ComboBox();
            this.exposureModeCombo = new System.Windows.Forms.ComboBox();
            this.whiteBalanceCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sharpnessBox = new System.Windows.Forms.TextBox();
            this.saturationBox = new System.Windows.Forms.TextBox();
            this.contrastBox = new System.Windows.Forms.TextBox();
            this.brightnessBox = new System.Windows.Forms.TextBox();
            this.resetButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharpnessTrackBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Brightness:";
            // 
            // brightnessTrackBar
            // 
            this.brightnessTrackBar.LargeChange = 10;
            this.brightnessTrackBar.Location = new System.Drawing.Point(100, 15);
            this.brightnessTrackBar.Maximum = 100;
            this.brightnessTrackBar.Name = "brightnessTrackBar";
            this.brightnessTrackBar.Size = new System.Drawing.Size(170, 56);
            this.brightnessTrackBar.TabIndex = 1;
            this.brightnessTrackBar.Tag = "brightness";
            this.brightnessTrackBar.TickFrequency = 5;
            this.brightnessTrackBar.Value = 50;
            this.brightnessTrackBar.ValueChanged += new System.EventHandler(this.settingsTrackBar_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Contrast:";
            // 
            // contrastTrackBar
            // 
            this.contrastTrackBar.LargeChange = 10;
            this.contrastTrackBar.Location = new System.Drawing.Point(100, 60);
            this.contrastTrackBar.Maximum = 100;
            this.contrastTrackBar.Minimum = -100;
            this.contrastTrackBar.Name = "contrastTrackBar";
            this.contrastTrackBar.Size = new System.Drawing.Size(170, 56);
            this.contrastTrackBar.TabIndex = 4;
            this.contrastTrackBar.Tag = "contrast";
            this.contrastTrackBar.TickFrequency = 10;
            this.contrastTrackBar.ValueChanged += new System.EventHandler(this.settingsTrackBar_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Saturation:";
            // 
            // saturationTrackBar
            // 
            this.saturationTrackBar.LargeChange = 10;
            this.saturationTrackBar.Location = new System.Drawing.Point(100, 105);
            this.saturationTrackBar.Maximum = 100;
            this.saturationTrackBar.Minimum = -100;
            this.saturationTrackBar.Name = "saturationTrackBar";
            this.saturationTrackBar.Size = new System.Drawing.Size(170, 56);
            this.saturationTrackBar.TabIndex = 7;
            this.saturationTrackBar.Tag = "saturation";
            this.saturationTrackBar.TickFrequency = 10;
            this.saturationTrackBar.ValueChanged += new System.EventHandler(this.settingsTrackBar_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Sharpness:";
            // 
            // sharpnessTrackBar
            // 
            this.sharpnessTrackBar.LargeChange = 10;
            this.sharpnessTrackBar.Location = new System.Drawing.Point(100, 150);
            this.sharpnessTrackBar.Maximum = 100;
            this.sharpnessTrackBar.Minimum = -100;
            this.sharpnessTrackBar.Name = "sharpnessTrackBar";
            this.sharpnessTrackBar.Size = new System.Drawing.Size(170, 56);
            this.sharpnessTrackBar.TabIndex = 10;
            this.sharpnessTrackBar.Tag = "sharpness";
            this.sharpnessTrackBar.TickFrequency = 10;
            this.sharpnessTrackBar.ValueChanged += new System.EventHandler(this.settingsTrackBar_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.verticalFlipCheck);
            this.groupBox1.Controls.Add(this.horizontalFlipCheck);
            this.groupBox1.Controls.Add(this.imageEffectCombo);
            this.groupBox1.Controls.Add(this.meteringModeCombo);
            this.groupBox1.Controls.Add(this.exposureModeCombo);
            this.groupBox1.Controls.Add(this.whiteBalanceCombo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.sharpnessBox);
            this.groupBox1.Controls.Add(this.saturationBox);
            this.groupBox1.Controls.Add(this.contrastBox);
            this.groupBox1.Controls.Add(this.brightnessBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.sharpnessTrackBar);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.saturationTrackBar);
            this.groupBox1.Controls.Add(this.contrastTrackBar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.brightnessTrackBar);
            this.groupBox1.Location = new System.Drawing.Point(10, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(330, 365);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // verticalFlipCheck
            // 
            this.verticalFlipCheck.AutoSize = true;
            this.verticalFlipCheck.Location = new System.Drawing.Point(145, 330);
            this.verticalFlipCheck.Name = "verticalFlipCheck";
            this.verticalFlipCheck.Size = new System.Drawing.Size(103, 21);
            this.verticalFlipCheck.TabIndex = 21;
            this.verticalFlipCheck.Tag = "vflip";
            this.verticalFlipCheck.Text = "Vertical Flip";
            this.verticalFlipCheck.UseVisualStyleBackColor = true;
            this.verticalFlipCheck.CheckedChanged += new System.EventHandler(this.settingsCheckBox_CheckedChanged);
            // 
            // horizontalFlipCheck
            // 
            this.horizontalFlipCheck.AutoSize = true;
            this.horizontalFlipCheck.Location = new System.Drawing.Point(10, 330);
            this.horizontalFlipCheck.Name = "horizontalFlipCheck";
            this.horizontalFlipCheck.Size = new System.Drawing.Size(120, 21);
            this.horizontalFlipCheck.TabIndex = 20;
            this.horizontalFlipCheck.Tag = "hflip";
            this.horizontalFlipCheck.Text = "Horizontal Flip";
            this.horizontalFlipCheck.UseVisualStyleBackColor = true;
            this.horizontalFlipCheck.CheckedChanged += new System.EventHandler(this.settingsCheckBox_CheckedChanged);
            // 
            // imageEffectCombo
            // 
            this.imageEffectCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageEffectCombo.FormattingEnabled = true;
            this.imageEffectCombo.Items.AddRange(new object[] {
            "None",
            "Negative",
            "Solarize",
            "Sketch",
            "Denoise",
            "Emboss",
            "Oil Paint",
            "Hatch",
            "G-Pen",
            "Pastel",
            "Water Color",
            "Film",
            "Blur",
            "Saturation",
            "Color Swap",
            "Washed Out",
            "Posterise",
            "Color Point",
            "Color Balance",
            "Cartoon"});
            this.imageEffectCombo.Location = new System.Drawing.Point(130, 290);
            this.imageEffectCombo.Name = "imageEffectCombo";
            this.imageEffectCombo.Size = new System.Drawing.Size(190, 24);
            this.imageEffectCombo.TabIndex = 19;
            this.imageEffectCombo.Tag = "effect";
            this.imageEffectCombo.SelectedIndexChanged += new System.EventHandler(this.settingsComboBox_SelectedIndexChanged);
            // 
            // meteringModeCombo
            // 
            this.meteringModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.meteringModeCombo.FormattingEnabled = true;
            this.meteringModeCombo.Items.AddRange(new object[] {
            "Average",
            "Spot",
            "Backlit",
            "Matrix"});
            this.meteringModeCombo.Location = new System.Drawing.Point(130, 260);
            this.meteringModeCombo.Name = "meteringModeCombo";
            this.meteringModeCombo.Size = new System.Drawing.Size(190, 24);
            this.meteringModeCombo.TabIndex = 17;
            this.meteringModeCombo.Tag = "expmeteringmode";
            this.meteringModeCombo.SelectedIndexChanged += new System.EventHandler(this.settingsComboBox_SelectedIndexChanged);
            // 
            // exposureModeCombo
            // 
            this.exposureModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exposureModeCombo.FormattingEnabled = true;
            this.exposureModeCombo.Items.AddRange(new object[] {
            "Off",
            "Auto",
            "Night",
            "Sports",
            "Snow",
            "Beach",
            "Very Long",
            "Fixed Fps",
            "Anti Shake",
            "Fireworks"});
            this.exposureModeCombo.Location = new System.Drawing.Point(130, 230);
            this.exposureModeCombo.Name = "exposureModeCombo";
            this.exposureModeCombo.Size = new System.Drawing.Size(190, 24);
            this.exposureModeCombo.TabIndex = 15;
            this.exposureModeCombo.Tag = "expmode";
            this.exposureModeCombo.SelectedIndexChanged += new System.EventHandler(this.settingsComboBox_SelectedIndexChanged);
            // 
            // whiteBalanceCombo
            // 
            this.whiteBalanceCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.whiteBalanceCombo.FormattingEnabled = true;
            this.whiteBalanceCombo.Items.AddRange(new object[] {
            "Off",
            "Auto",
            "Sunlight",
            "Cloudy",
            "Shade",
            "Tungsten",
            "Fluorescent",
            "Incandescent",
            "Flash",
            "Horizon"});
            this.whiteBalanceCombo.Location = new System.Drawing.Point(130, 200);
            this.whiteBalanceCombo.Name = "whiteBalanceCombo";
            this.whiteBalanceCombo.Size = new System.Drawing.Size(190, 24);
            this.whiteBalanceCombo.TabIndex = 13;
            this.whiteBalanceCombo.Tag = "awb";
            this.whiteBalanceCombo.SelectedIndexChanged += new System.EventHandler(this.settingsComboBox_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 293);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "Image Effect:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Metering Mode:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Exposure Mode:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "White Balance:";
            // 
            // sharpnessBox
            // 
            this.sharpnessBox.Location = new System.Drawing.Point(270, 153);
            this.sharpnessBox.Name = "sharpnessBox";
            this.sharpnessBox.ReadOnly = true;
            this.sharpnessBox.Size = new System.Drawing.Size(50, 22);
            this.sharpnessBox.TabIndex = 11;
            this.sharpnessBox.TabStop = false;
            this.sharpnessBox.Tag = "";
            // 
            // saturationBox
            // 
            this.saturationBox.Location = new System.Drawing.Point(270, 108);
            this.saturationBox.Name = "saturationBox";
            this.saturationBox.ReadOnly = true;
            this.saturationBox.Size = new System.Drawing.Size(50, 22);
            this.saturationBox.TabIndex = 8;
            this.saturationBox.TabStop = false;
            this.saturationBox.Tag = "";
            // 
            // contrastBox
            // 
            this.contrastBox.Location = new System.Drawing.Point(270, 63);
            this.contrastBox.Name = "contrastBox";
            this.contrastBox.ReadOnly = true;
            this.contrastBox.Size = new System.Drawing.Size(50, 22);
            this.contrastBox.TabIndex = 5;
            this.contrastBox.TabStop = false;
            this.contrastBox.Tag = "";
            // 
            // brightnessBox
            // 
            this.brightnessBox.Location = new System.Drawing.Point(270, 18);
            this.brightnessBox.Name = "brightnessBox";
            this.brightnessBox.ReadOnly = true;
            this.brightnessBox.Size = new System.Drawing.Size(50, 22);
            this.brightnessBox.TabIndex = 2;
            this.brightnessBox.TabStop = false;
            this.brightnessBox.Tag = "";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(195, 375);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(145, 25);
            this.resetButton.TabIndex = 1;
            this.resetButton.Text = "Reset to default";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // CameraOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 408);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CameraOptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Camera options";
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharpnessTrackBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar brightnessTrackBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar contrastTrackBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar saturationTrackBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar sharpnessTrackBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox sharpnessBox;
        private System.Windows.Forms.TextBox saturationBox;
        private System.Windows.Forms.TextBox contrastBox;
        private System.Windows.Forms.TextBox brightnessBox;
        private System.Windows.Forms.CheckBox verticalFlipCheck;
        private System.Windows.Forms.CheckBox horizontalFlipCheck;
        private System.Windows.Forms.ComboBox imageEffectCombo;
        private System.Windows.Forms.ComboBox meteringModeCombo;
        private System.Windows.Forms.ComboBox exposureModeCombo;
        private System.Windows.Forms.ComboBox whiteBalanceCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button resetButton;
    }
}