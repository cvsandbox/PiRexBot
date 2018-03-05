namespace PiRexBot
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.portUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ipAddressBox = new System.Windows.Forms.TextBox();
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.botControlGroup = new System.Windows.Forms.GroupBox();
            this.rotateRightButton = new System.Windows.Forms.Button();
            this.moveBackwardButton = new System.Windows.Forms.Button();
            this.rotateLeftButton = new System.Windows.Forms.Button();
            this.moveRightButton = new System.Windows.Forms.Button();
            this.moveForwardButton = new System.Windows.Forms.Button();
            this.moveLeftButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.videoModeCombo = new System.Windows.Forms.ComboBox();
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.menuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.botControlGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1054, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileMenuItem.Text = "&File";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(102, 24);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Location = new System.Drawing.Point(0, 661);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1054, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.videoModeCombo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.passwordBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.loginBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.connectButton);
            this.groupBox1.Controls.Add(this.portUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ipAddressBox);
            this.groupBox1.Controls.Add(this.ipAddressLabel);
            this.groupBox1.Location = new System.Drawing.Point(10, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 90);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(557, 53);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(101, 24);
            this.connectButton.TabIndex = 10;
            this.connectButton.Text = "&Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // portUpDown
            // 
            this.portUpDown.Location = new System.Drawing.Point(330, 25);
            this.portUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.portUpDown.Name = "portUpDown";
            this.portUpDown.Size = new System.Drawing.Size(150, 22);
            this.portUpDown.TabIndex = 3;
            this.portUpDown.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Port:";
            // 
            // ipAddressBox
            // 
            this.ipAddressBox.Location = new System.Drawing.Point(95, 25);
            this.ipAddressBox.Name = "ipAddressBox";
            this.ipAddressBox.Size = new System.Drawing.Size(150, 22);
            this.ipAddressBox.TabIndex = 1;
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Location = new System.Drawing.Point(10, 28);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(80, 17);
            this.ipAddressLabel.TabIndex = 0;
            this.ipAddressLabel.Text = "IP Address:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.videoSourcePlayer);
            this.groupBox2.Location = new System.Drawing.Point(10, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(672, 517);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Camera View";
            // 
            // botControlGroup
            // 
            this.botControlGroup.Controls.Add(this.rotateRightButton);
            this.botControlGroup.Controls.Add(this.moveBackwardButton);
            this.botControlGroup.Controls.Add(this.rotateLeftButton);
            this.botControlGroup.Controls.Add(this.moveRightButton);
            this.botControlGroup.Controls.Add(this.moveForwardButton);
            this.botControlGroup.Controls.Add(this.moveLeftButton);
            this.botControlGroup.Location = new System.Drawing.Point(692, 135);
            this.botControlGroup.Name = "botControlGroup";
            this.botControlGroup.Size = new System.Drawing.Size(350, 250);
            this.botControlGroup.TabIndex = 3;
            this.botControlGroup.TabStop = false;
            this.botControlGroup.Text = "Bot control";
            // 
            // rotateRightButton
            // 
            this.rotateRightButton.Image = global::PiRexBot.Properties.Resources.rotate_right;
            this.rotateRightButton.Location = new System.Drawing.Point(235, 135);
            this.rotateRightButton.Name = "rotateRightButton";
            this.rotateRightButton.Size = new System.Drawing.Size(100, 100);
            this.rotateRightButton.TabIndex = 5;
            this.rotateRightButton.TabStop = false;
            this.toolTip.SetToolTip(this.rotateRightButton, "Rotate right");
            this.rotateRightButton.UseVisualStyleBackColor = true;
            // 
            // moveBackwardButton
            // 
            this.moveBackwardButton.Image = global::PiRexBot.Properties.Resources.backward;
            this.moveBackwardButton.Location = new System.Drawing.Point(125, 135);
            this.moveBackwardButton.Name = "moveBackwardButton";
            this.moveBackwardButton.Size = new System.Drawing.Size(100, 100);
            this.moveBackwardButton.TabIndex = 4;
            this.moveBackwardButton.TabStop = false;
            this.toolTip.SetToolTip(this.moveBackwardButton, "Move backward");
            this.moveBackwardButton.UseVisualStyleBackColor = true;
            // 
            // rotateLeftButton
            // 
            this.rotateLeftButton.Image = global::PiRexBot.Properties.Resources.rotate_left;
            this.rotateLeftButton.Location = new System.Drawing.Point(15, 135);
            this.rotateLeftButton.Name = "rotateLeftButton";
            this.rotateLeftButton.Size = new System.Drawing.Size(100, 100);
            this.rotateLeftButton.TabIndex = 3;
            this.rotateLeftButton.TabStop = false;
            this.toolTip.SetToolTip(this.rotateLeftButton, "Rotate left");
            this.rotateLeftButton.UseVisualStyleBackColor = true;
            // 
            // moveRightButton
            // 
            this.moveRightButton.Image = global::PiRexBot.Properties.Resources.slight_right;
            this.moveRightButton.Location = new System.Drawing.Point(235, 25);
            this.moveRightButton.Name = "moveRightButton";
            this.moveRightButton.Size = new System.Drawing.Size(100, 100);
            this.moveRightButton.TabIndex = 2;
            this.moveRightButton.TabStop = false;
            this.toolTip.SetToolTip(this.moveRightButton, "Move right");
            this.moveRightButton.UseVisualStyleBackColor = true;
            // 
            // moveForwardButton
            // 
            this.moveForwardButton.Image = global::PiRexBot.Properties.Resources.forward;
            this.moveForwardButton.Location = new System.Drawing.Point(125, 25);
            this.moveForwardButton.Name = "moveForwardButton";
            this.moveForwardButton.Size = new System.Drawing.Size(100, 100);
            this.moveForwardButton.TabIndex = 1;
            this.moveForwardButton.TabStop = false;
            this.toolTip.SetToolTip(this.moveForwardButton, "Move forward");
            this.moveForwardButton.UseVisualStyleBackColor = true;
            // 
            // moveLeftButton
            // 
            this.moveLeftButton.Image = global::PiRexBot.Properties.Resources.slight_left;
            this.moveLeftButton.Location = new System.Drawing.Point(15, 25);
            this.moveLeftButton.Name = "moveLeftButton";
            this.moveLeftButton.Size = new System.Drawing.Size(100, 100);
            this.moveLeftButton.TabIndex = 0;
            this.moveLeftButton.TabStop = false;
            this.toolTip.SetToolTip(this.moveLeftButton, "Move left");
            this.moveLeftButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Login:";
            // 
            // loginBox
            // 
            this.loginBox.Location = new System.Drawing.Point(95, 55);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(150, 22);
            this.loginBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password:";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(330, 55);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(150, 22);
            this.passwordBox.TabIndex = 9;
            this.passwordBox.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(505, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Video:";
            // 
            // videoModeCombo
            // 
            this.videoModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.videoModeCombo.FormattingEnabled = true;
            this.videoModeCombo.Items.AddRange(new object[] {
            "MJPEG",
            "JPEG"});
            this.videoModeCombo.Location = new System.Drawing.Point(557, 25);
            this.videoModeCombo.Name = "videoModeCombo";
            this.videoModeCombo.Size = new System.Drawing.Size(100, 24);
            this.videoModeCombo.TabIndex = 5;
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.videoSourcePlayer.Location = new System.Drawing.Point(15, 25);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(642, 482);
            this.videoSourcePlayer.TabIndex = 0;
            this.videoSourcePlayer.VideoSource = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 683);
            this.Controls.Add(this.botControlGroup);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PiRex Bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.botControlGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.NumericUpDown portUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ipAddressBox;
        private System.Windows.Forms.Label ipAddressLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.GroupBox botControlGroup;
        private System.Windows.Forms.Button moveLeftButton;
        private System.Windows.Forms.Button rotateRightButton;
        private System.Windows.Forms.Button moveBackwardButton;
        private System.Windows.Forms.Button rotateLeftButton;
        private System.Windows.Forms.Button moveRightButton;
        private System.Windows.Forms.Button moveForwardButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox loginBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox videoModeCombo;
        private System.Windows.Forms.Label label4;
    }
}

