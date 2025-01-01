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
            this.cboDisplay = new System.Windows.Forms.ComboBox();
            this.cboRateAC = new System.Windows.Forms.ComboBox();
            this.cboRateDC = new System.Windows.Forms.ComboBox();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.lblAC = new System.Windows.Forms.Label();
            this.lblDC = new System.Windows.Forms.Label();
            this.chkBattery = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboDisplay
            // 
            this.cboDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDisplay.FormattingEnabled = true;
            this.cboDisplay.Location = new System.Drawing.Point(69, 12);
            this.cboDisplay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboDisplay.Name = "cboDisplay";
            this.cboDisplay.Size = new System.Drawing.Size(150, 23);
            this.cboDisplay.TabIndex = 0;
            this.cboDisplay.SelectedIndexChanged += new System.EventHandler(this.cboDisplay_SelectedIndexChanged);
            // 
            // cboRateAC
            // 
            this.cboRateAC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRateAC.FormattingEnabled = true;
            this.cboRateAC.Location = new System.Drawing.Point(115, 66);
            this.cboRateAC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboRateAC.Name = "cboRateAC";
            this.cboRateAC.Size = new System.Drawing.Size(185, 23);
            this.cboRateAC.TabIndex = 1;
            // 
            // cboRateDC
            // 
            this.cboRateDC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRateDC.Enabled = false;
            this.cboRateDC.FormattingEnabled = true;
            this.cboRateDC.Location = new System.Drawing.Point(115, 95);
            this.cboRateDC.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboRateDC.Name = "cboRateDC";
            this.cboRateDC.Size = new System.Drawing.Size(185, 23);
            this.cboRateDC.TabIndex = 2;
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Location = new System.Drawing.Point(13, 15);
            this.lblDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(48, 15);
            this.lblDisplay.TabIndex = 3;
            this.lblDisplay.Text = "Display:";
            // 
            // lblAC
            // 
            this.lblAC.AutoSize = true;
            this.lblAC.Location = new System.Drawing.Point(13, 69);
            this.lblAC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAC.Name = "lblAC";
            this.lblAC.Size = new System.Drawing.Size(94, 15);
            this.lblAC.TabIndex = 4;
            this.lblAC.Text = "Plugged in (AC):";
            // 
            // lblDC
            // 
            this.lblDC.AutoSize = true;
            this.lblDC.Enabled = false;
            this.lblDC.Location = new System.Drawing.Point(14, 98);
            this.lblDC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDC.Name = "lblDC";
            this.lblDC.Size = new System.Drawing.Size(93, 15);
            this.lblDC.TabIndex = 5;
            this.lblDC.Text = "On battery (DC):";
            // 
            // chkBattery
            // 
            this.chkBattery.AutoSize = true;
            this.chkBattery.Location = new System.Drawing.Point(13, 41);
            this.chkBattery.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkBattery.Name = "chkBattery";
            this.chkBattery.Size = new System.Drawing.Size(186, 19);
            this.chkBattery.TabIndex = 6;
            this.chkBattery.Text = "Change refresh rate on battery";
            this.chkBattery.UseVisualStyleBackColor = true;
            this.chkBattery.CheckedChanged += new System.EventHandler(this.chkBattery_CheckedChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(226, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(226, 132);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(313, 167);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.chkBattery);
            this.Controls.Add(this.lblDC);
            this.Controls.Add(this.lblAC);
            this.Controls.Add(this.lblDisplay);
            this.Controls.Add(this.cboRateDC);
            this.Controls.Add(this.cboRateAC);
            this.Controls.Add(this.cboDisplay);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Refresh Rate Tuner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboDisplay;
        private System.Windows.Forms.ComboBox cboRateAC;
        private System.Windows.Forms.ComboBox cboRateDC;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.Label lblAC;
        private System.Windows.Forms.Label lblDC;
        private System.Windows.Forms.CheckBox chkBattery;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnApply;
    }
}

