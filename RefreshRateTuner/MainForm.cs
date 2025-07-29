using Microsoft.Win32;
using RefreshRateTuner.Config;
using RefreshRateTuner.Win32;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace RefreshRateTuner
{
    internal sealed partial class MainForm : Form
    {
        private readonly bool Startup;

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

        public MainForm(bool startup)
        {
            InitializeComponent();
            TrayIcon.Icon = Icon = Icon.ExtractAssociatedIcon(Assembly.GetEntryAssembly().Location);
            TrayIcon.Text = $"Refresh Rate Tuner v{Program.GetVerString()}";

            // check if program start on boot is enabled by checking
            // for existence of the required registry key
            RegistryKey key = Registry.CurrentUser.OpenSubKey(
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            try
            {
                key.GetValueKind("Refresh Rate Tuner");
                chkSysStart.Checked = true;
            }
            // IOException is thrown if registry key does not exist
            catch (IOException) { }
            key.Close();

            Startup = startup;
            Config = RefreshRateConfig.Load(ConfPath);

            // make sure display configuration hasn't changed since last run.
            // If it hasn't, and this program was launched with the --startup
            // command, apply the refresh rate settings
            bool confCheck = RefreshDispSettings();
            if (startup && confCheck)
            {
                ApplyRefreshRate();
            }
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(PowerModeChanged);
        }

        protected override void SetVisibleCore(bool value)
        {
            // hide form on first launch
            if (Startup && !IsHandleCreated)
            {
                value = false;
                CreateHandle();
            }
            base.SetVisibleCore(value);
        }

        protected override void WndProc(ref Message m)
        {
            // 0x112 = WM_SYSCOMMAND, 0xF020 == SC_MINIMIZE
            if (m.Msg == 0x112 && (m.WParam.ToInt32() & 0xfff0) == 0xF020)
            {
                Hide();
                return;
            }
            base.WndProc(ref m);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            SystemEvents.PowerModeChanged -= PowerModeChanged;
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

        private bool RefreshDispSettings()
        {
            cboDisplay.Items.Clear();
            for (int i = 0; User32.EnumDisplayDevicesW(null, i, out string devName); i++)
            {
                // attempt to detect if no monitors
                // connected to this display adapter
                if (User32.EnumDisplayDevicesW(devName, 0, out _))
                {
                    cboDisplay.Items.Add(devName);
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
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        warn = true;
                    }
                    Config.DispConfs[i].Name = (string)cboDisplay.Items[i];
                }
            }

            cboDisplay.SelectedIndex = 0;
            chkBatt.Checked = Config.DispConfs[0].ChangeOnBattery;
            return !warn;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDispSettings();
        }

        private void DisplayChanged(object sender, EventArgs e)
        {
            List<int> refreshRates = [];
            cboRateAC.Items.Clear();
            cboRateDC.Items.Clear();

            DispSettings current = User32.EnumDisplaySettingsW(cboDisplay.Text, User32.ENUM_CURRENT_SETTINGS);
            for (int i = 0; ; i++)
            {
                DispSettings temp = User32.EnumDisplaySettingsW(cboDisplay.Text, i);
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
                chkBatt.Enabled = true;
                cboRateAC.Enabled = true;
                cboRateDC.Enabled = true;
            }
            else
            {
                chkBatt.Enabled = false;
                cboRateAC.Enabled = false;
                cboRateDC.Enabled = false;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Config.Save(ConfPath);
            ApplyRefreshRate();
        }

        private void BattRateToggle(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            Config.DispConfs[cboDisplay.SelectedIndex].ChangeOnBattery =
                cboRateDC.Enabled = lblDC.Enabled = cb.Checked;
        }

        private void ApplyRefreshRate()
        {
            for (int i = 0; i < cboDisplay.Items.Count; i++)
            {
                DispConf display = Config.DispConfs[i];
                DispChange result;

                switch (SystemInformation.PowerStatus.PowerLineStatus)
                {
                    case PowerLineStatus.Online:
                        result = User32.ChangeRefreshRate(
                            display.Name, display.RefreshRateAC, CDSFlags.None);
                        break;
                    case PowerLineStatus.Offline:
                        result = User32.ChangeRefreshRate(
                            display.Name, display.RefreshRateDC, CDSFlags.None);
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

        private void ACRateChanged(object sender, EventArgs e)
        {
            Config.DispConfs[cboDisplay.SelectedIndex].RefreshRateAC = int.Parse(cboRateAC.Text, CultureInfo.InvariantCulture);
        }

        private void DCRateChanged(object sender, EventArgs e)
        {
            Config.DispConfs[cboDisplay.SelectedIndex].RefreshRateDC = int.Parse(cboRateDC.Text, CultureInfo.InvariantCulture);
        }

        private void ToggleSysStart(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(
            @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (cb.Checked)
            {
                key.SetValue("Refresh Rate Tuner", $"\"{Assembly.GetEntryAssembly().Location}\" --startup");
            }
            else
            {
                key.DeleteValue("Refresh Rate Tuner", false);
            }
        }

        private void tsiExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowConfig(object sender, EventArgs e)
        {
            Show();
            Activate();
        }
    }
}
