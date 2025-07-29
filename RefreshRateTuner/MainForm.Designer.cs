namespace RefreshRateTuner
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
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator sep1;
            System.Windows.Forms.Label lblDisplay;
            System.Windows.Forms.Label lblAC;
            System.Windows.Forms.Button btnRefresh;
            System.Windows.Forms.Button btnApply;
            System.Windows.Forms.ToolStripMenuItem tsiShow;
            System.Windows.Forms.ToolStripMenuItem tsiExit;
            System.Windows.Forms.ContextMenuStrip TrayMenu;
            this.cboDisplay = new System.Windows.Forms.ComboBox();
            this.cboRateAC = new System.Windows.Forms.ComboBox();
            this.cboRateDC = new System.Windows.Forms.ComboBox();
            this.lblDC = new System.Windows.Forms.Label();
            this.chkBatt = new System.Windows.Forms.CheckBox();
            this.chkSysStart = new System.Windows.Forms.CheckBox();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            sep1 = new System.Windows.Forms.ToolStripSeparator();
            lblDisplay = new System.Windows.Forms.Label();
            lblAC = new System.Windows.Forms.Label();
            btnRefresh = new System.Windows.Forms.Button();
            btnApply = new System.Windows.Forms.Button();
            tsiShow = new System.Windows.Forms.ToolStripMenuItem();
            tsiExit = new System.Windows.Forms.ToolStripMenuItem();
            TrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            TrayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // sep1
            // 
            sep1.Name = "sep1";
            sep1.Size = new System.Drawing.Size(196, 6);
            // 
            // lblDisplay
            // 
            lblDisplay.AutoSize = true;
            lblDisplay.Location = new System.Drawing.Point(13, 15);
            lblDisplay.Name = "lblDisplay";
            lblDisplay.Size = new System.Drawing.Size(48, 15);
            lblDisplay.TabIndex = 0;
            lblDisplay.Text = "Display:";
            // 
            // lblAC
            // 
            lblAC.AutoSize = true;
            lblAC.Location = new System.Drawing.Point(13, 69);
            lblAC.Name = "lblAC";
            lblAC.Size = new System.Drawing.Size(94, 15);
            lblAC.TabIndex = 4;
            lblAC.Text = "Plugged in (AC):";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new System.Drawing.Point(226, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new System.Drawing.Size(75, 23);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "&Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnApply
            // 
            btnApply.Location = new System.Drawing.Point(226, 132);
            btnApply.Name = "btnApply";
            btnApply.Size = new System.Drawing.Size(75, 23);
            btnApply.TabIndex = 8;
            btnApply.Text = "&Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // tsiShow
            // 
            tsiShow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tsiShow.Name = "tsiShow";
            tsiShow.Size = new System.Drawing.Size(199, 22);
            tsiShow.Text = "Configure refresh rate";
            tsiShow.Click += new System.EventHandler(this.ShowConfig);
            // 
            // tsiExit
            // 
            tsiExit.Name = "tsiExit";
            tsiExit.Size = new System.Drawing.Size(199, 22);
            tsiExit.Text = "Exit";
            tsiExit.Click += new System.EventHandler(this.tsiExit_Click);
            // 
            // cboDisplay
            // 
            this.cboDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDisplay.Location = new System.Drawing.Point(69, 12);
            this.cboDisplay.Name = "cboDisplay";
            this.cboDisplay.Size = new System.Drawing.Size(150, 23);
            this.cboDisplay.TabIndex = 1;
            this.cboDisplay.SelectedIndexChanged += new System.EventHandler(this.DisplayChanged);
            // 
            // cboRateAC
            // 
            this.cboRateAC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRateAC.Location = new System.Drawing.Point(115, 66);
            this.cboRateAC.Name = "cboRateAC";
            this.cboRateAC.Size = new System.Drawing.Size(185, 23);
            this.cboRateAC.TabIndex = 5;
            this.cboRateAC.SelectedIndexChanged += new System.EventHandler(this.ACRateChanged);
            // 
            // cboRateDC
            // 
            this.cboRateDC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRateDC.Enabled = false;
            this.cboRateDC.Location = new System.Drawing.Point(115, 95);
            this.cboRateDC.Name = "cboRateDC";
            this.cboRateDC.Size = new System.Drawing.Size(185, 23);
            this.cboRateDC.TabIndex = 7;
            this.cboRateDC.SelectedIndexChanged += new System.EventHandler(this.DCRateChanged);
            // 
            // lblDC
            // 
            this.lblDC.AutoSize = true;
            this.lblDC.Enabled = false;
            this.lblDC.Location = new System.Drawing.Point(14, 98);
            this.lblDC.Name = "lblDC";
            this.lblDC.Size = new System.Drawing.Size(93, 15);
            this.lblDC.TabIndex = 6;
            this.lblDC.Text = "On battery (DC):";
            // 
            // chkBatt
            // 
            this.chkBatt.AutoSize = true;
            this.chkBatt.Location = new System.Drawing.Point(13, 41);
            this.chkBatt.Name = "chkBatt";
            this.chkBatt.Size = new System.Drawing.Size(186, 19);
            this.chkBatt.TabIndex = 3;
            this.chkBatt.Text = "Change refresh rate on &battery";
            this.chkBatt.UseVisualStyleBackColor = true;
            this.chkBatt.CheckedChanged += new System.EventHandler(this.BattRateToggle);
            // 
            // chkSysStart
            // 
            this.chkSysStart.AutoSize = true;
            this.chkSysStart.Location = new System.Drawing.Point(12, 135);
            this.chkSysStart.Name = "chkSysStart";
            this.chkSysStart.Size = new System.Drawing.Size(95, 19);
            this.chkSysStart.TabIndex = 9;
            this.chkSysStart.Text = "&Start on boot";
            this.chkSysStart.UseVisualStyleBackColor = true;
            this.chkSysStart.CheckedChanged += new System.EventHandler(this.ToggleSysStart);
            // 
            // TrayIcon
            // 
            this.TrayIcon.ContextMenuStrip = TrayMenu;
            this.TrayIcon.Visible = true;
            this.TrayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ShowConfig);
            // 
            // TrayMenu
            // 
            TrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            tsiShow,
            sep1,
            tsiExit});
            TrayMenu.Name = "TrayMenu";
            TrayMenu.Size = new System.Drawing.Size(200, 54);
            // 
            // MainForm
            // 
            this.AcceptButton = btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(313, 167);
            this.Controls.Add(this.chkSysStart);
            this.Controls.Add(btnApply);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(this.chkBatt);
            this.Controls.Add(this.lblDC);
            this.Controls.Add(lblAC);
            this.Controls.Add(lblDisplay);
            this.Controls.Add(this.cboRateDC);
            this.Controls.Add(this.cboRateAC);
            this.Controls.Add(this.cboDisplay);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Refresh Rate Tuner";
            TrayMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cboRateAC;
        private System.Windows.Forms.ComboBox cboRateDC;
        private System.Windows.Forms.CheckBox chkBatt;
        private System.Windows.Forms.CheckBox chkSysStart;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.ComboBox cboDisplay;
        private System.Windows.Forms.Label lblDC;
    }
}

