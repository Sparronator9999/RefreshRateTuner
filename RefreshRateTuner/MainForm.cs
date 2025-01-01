using Microsoft.Win32;
using RefreshRateTuner.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RefreshRateTuner
{
    internal partial class MainForm : Form, IDisposable
    {
        private readonly List<int> RefreshRates = new List<int>();

        public MainForm()
        {
            InitializeComponent();
            RefreshDisplaySettings();
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.StatusChange)
            {
                ApplyRefreshRate();
            }
        }

        private void RefreshDisplaySettings()
        {
            cboDisplay.Items.Clear();
            while (true)
            {
                if (User32.EnumDisplayDevices(null, cboDisplay.Items.Count, out string devName))
                {
                    cboDisplay.Items.Add(devName);
                }
                else break;
            }
            cboDisplay.SelectedIndex = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDisplaySettings();
        }

        private void cboDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshRates.Clear();
            cboRateAC.Items.Clear();

            DisplaySettings current = User32.EnumDisplaySettings(cboDisplay.Text, User32.ENUM_CURRENT_SETTINGS);
            for (int i = 0; ; i++)
            {
                DisplaySettings temp = User32.EnumDisplaySettings(cboDisplay.Text, i);
                if (temp is null)
                {
                    break;
                }
                if (!RefreshRates.Contains(temp.RefreshRate))
                {
                    RefreshRates.Add(temp.RefreshRate);
                }
            }

            // make sure refresh rates are sorted lowest to highest
            RefreshRates.Sort();
            foreach (int rate in RefreshRates)
            {
                cboRateAC.Items.Add(rate);
                cboRateDC.Items.Add(rate);
            }

            // set current refresh rate for AC and lowest refresh rate for DC
            cboRateAC.SelectedIndex = RefreshRates.IndexOf(current.RefreshRate);
            cboRateDC.SelectedIndex = 0;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ApplyRefreshRate();
        }

        private void chkBattery_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox c)
            {
                cboRateDC.Enabled = lblDC.Enabled = c.Checked;
            }
        }

        private void ApplyRefreshRate()
        {
            DispChange result;
            switch (SystemInformation.PowerStatus.PowerLineStatus)
            {
                case PowerLineStatus.Online:
                    result = User32.ChangeRefreshRate(cboDisplay.Text,
                        RefreshRates[cboRateAC.SelectedIndex],
                        User32.ChangeDisplaySettingsFlags.None);
                    break;
                case PowerLineStatus.Offline:
                    result = User32.ChangeRefreshRate(cboDisplay.Text,
                        RefreshRates[cboRateDC.SelectedIndex],
                        User32.ChangeDisplaySettingsFlags.None);
                    break;
                default:
                    MessageBox.Show("Error: Unknown power line status");
                    return;
            }

            if (result != DispChange.Successful)
            {
                MessageBox.Show($"Error: ChangeRefreshRate returned {result}");
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;
        }
    }
}
