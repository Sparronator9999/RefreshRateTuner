using Microsoft.Win32;
using RefreshRateTuner.Config;
using RefreshRateTuner.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace RefreshRateTuner
{
    internal sealed partial class MainForm : Form
    {
        private static readonly string ConfPath =
#if NET40_OR_GREATER || NETCOREAPP
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Sparronator9999", "RefreshRateTuner", "CurrentConfig.xml");
#else
            Path.Combine(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Sparronator9999"), Path.Combine("RefreshRateTuner", "CurrentConfig.xml"));
#endif

        private readonly RefreshRateConfig Config;

        public MainForm()
        {
            InitializeComponent();

            Config = RefreshRateConfig.Load(ConfPath);

            RefreshDisplaySettings();
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(PowerModeChanged);
        }

        private void PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.StatusChange)
            {
                // we shouldn't run time-consuming operations on this thread,
                // so change refresh rate on the UI thread instead
                // (same as when applying from "Apply" button)
                Invoke(ApplyRefreshRate);
            }
        }

        private void RefreshDisplaySettings()
        {
            cboDisplay.Items.Clear();
            for (int i = 0; ; i++)
            {
                if (User32.EnumDisplayDevices(null, i, out string devName))
                {
                    // attempt to detect if no monitors
                    // connected to this display adapter
                    if (User32.EnumDisplayDevices(devName, 0, out _))
                    {
                        cboDisplay.Items.Add(devName);
                    }
                }
                else
                {
                    break;
                }
            }

            while (Config.DispConfs.Count < cboDisplay.Items.Count)
            {
                Config.DispConfs.Add(new DispConf());
            }

            bool warn = false;
            for (int i = 0; i < cboDisplay.Items.Count; i++)
            {
                if ((string)cboDisplay.Items[i] != Config.DispConfs[i].Name)
                {
                    if (!warn)
                    {
                        MessageBox.Show(
                            "Your display configuration appears to have been changed since you last ran Refresh Rate Tuner.\n" +
                            "Some of your display settings may be reset or incorrect.\n\n" +
                            "If you are running Refresh Rate Tuner for the first time, you may safely ignore this message.",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); warn = true;
                    }
                    Config.DispConfs[i].Name = (string)cboDisplay.Items[i];
                }
            }

            cboDisplay.SelectedIndex = 0;
            chkBattery.Checked = Config.DispConfs[0].ChangeOnBattery;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDisplaySettings();
        }

        private void DisplayChanged(object sender, EventArgs e)
        {
            List<int> refreshRates = [];
            cboRateAC.Items.Clear();
            cboRateDC.Items.Clear();

            DisplaySettings current = User32.EnumDisplaySettings(cboDisplay.Text, User32.ENUM_CURRENT_SETTINGS);
            for (int i = 0; ; i++)
            {
                DisplaySettings temp = User32.EnumDisplaySettings(cboDisplay.Text, i);
                if (temp is null)
                {
                    break;
                }
                if (!refreshRates.Contains(temp.RefreshRate))
                {
                    refreshRates.Add(temp.RefreshRate);
                }
            }

            if (refreshRates.Count > 0)
            {
                // make sure refresh rates are sorted lowest to highest
                refreshRates.Sort();
                foreach (int rate in refreshRates)
                {
                    cboRateAC.Items.Add(rate);
                    cboRateDC.Items.Add(rate);
                }

                if (refreshRates.Contains(Config.DispConfs[cboDisplay.SelectedIndex].RefreshRateAC) &&
                    refreshRates.Contains(Config.DispConfs[cboDisplay.SelectedIndex].RefreshRateDC))
                {
                    cboRateAC.SelectedIndex = refreshRates.IndexOf(Config.DispConfs[cboDisplay.SelectedIndex].RefreshRateAC);
                    cboRateDC.SelectedIndex = refreshRates.IndexOf(Config.DispConfs[cboDisplay.SelectedIndex].RefreshRateDC);
                }
                else
                {
                    // set current refresh rate for AC and lowest refresh rate for DC
                    cboRateAC.SelectedIndex = current is null
                        ? 0
                        : refreshRates.IndexOf(current.RefreshRate);
                    cboRateDC.SelectedIndex = 0;
                }
                chkBattery.Enabled = true;
                cboRateAC.Enabled = true;
                cboRateDC.Enabled = true;
            }
            else
            {
                chkBattery.Enabled = false;
                cboRateAC.Enabled = false;
                cboRateDC.Enabled = false;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Config.Save(ConfPath);
            ApplyRefreshRate();
        }

        private void BatteryRateToggle(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            cboRateDC.Enabled = lblDC.Enabled = c.Checked;
            Config.DispConfs[cboDisplay.SelectedIndex].ChangeOnBattery = c.Checked;
        }

        private void ApplyRefreshRate()
        {
            CDSFlags flags = CDSFlags.None;
            DispChange result;

            for (int i = 0; i < cboDisplay.Items.Count; i++)
            {
                DispConf display = Config.DispConfs[i];

                switch (SystemInformation.PowerStatus.PowerLineStatus)
                {
                    case PowerLineStatus.Online:
                        result = User32.ChangeRefreshRate(
                            display.Name, display.RefreshRateAC, flags);
                        break;
                    case PowerLineStatus.Offline:
                        result = User32.ChangeRefreshRate(
                            display.Name, display.RefreshRateDC, flags);
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
        }

        private void MainFormClosed(object sender, FormClosedEventArgs e)
        {
            SystemEvents.PowerModeChanged -= PowerModeChanged;
        }

        private void ACRateChanged(object sender, EventArgs e)
        {
            Config.DispConfs[cboDisplay.SelectedIndex].RefreshRateAC = int.Parse(cboRateAC.Text, CultureInfo.InvariantCulture);
        }

        private void DCRateChanged(object sender, EventArgs e)
        {
            Config.DispConfs[cboDisplay.SelectedIndex].RefreshRateDC = int.Parse(cboRateDC.Text, CultureInfo.InvariantCulture);
        }
    }
}
