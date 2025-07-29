using RefreshRateTuner.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace RefreshRateTuner
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string [] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // multi-instance detection
            // NOTE: GUID is used to prevent conflicts with potential
            // identically named but different program
            // based on: https://stackoverflow.com/a/184143
            using (Mutex mutex = new(true, "{c71c80c6-2ce4-4798-915f-bdac1b1afb76}", out bool createdNew))
            {
                // this instance is the first to open; proceed as normal:
                if (createdNew)
                {
                    foreach (string arg in args)
                    {
                        if (arg == "--startup")
                        {
                            Application.Run(new MainForm(true));
                            return;
                        }
                    }
                    Application.Run(new MainForm(false));
                }
                else
                {
                    // Refresh Rate Tuner is already running, focus
                    // (and restore, if minimised) its window:
                    Process current = Process.GetCurrentProcess();
                    foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                    {
                        // TODO:
                        // fix already existing Refresh Rate Tuner
                        // instance not being restored
                        if (process.Id != current.Id)
                        {
                            if (process.MainWindowHandle != IntPtr.Zero)
                            {
                                User32.ShowWindow(process.MainWindowHandle, 9);    // SW_RESTORE
                                User32.SetForegroundWindow(process.MainWindowHandle);
                            }
                            break;
                        }
                    }
                }
            }
        }

        private static string GetVerSuffix()
        {
            // format: X.Y.Z-SUFFIX[.W]+REVISION,
            // where W is a beta/release candidate version if applicable
            string verSuffix = Application.ProductVersion;
            int suffixNum = 0;

            if (verSuffix.Contains("-"))
            {
                suffixNum++;
                int index;
                do
                {
                    // SUFFIX[.W][-SUFFIX2[.V]...]+REVISION
                    index = verSuffix.IndexOf('-');
                    verSuffix = verSuffix.Remove(0, index + 1);
                    suffixNum--;
                }
                while (index != -1 && suffixNum > 0);

                if (verSuffix.Contains("."))
                {
                    // remove suffix version number
                    verSuffix = verSuffix.Remove(verSuffix.IndexOf('.'));
                }
                else if (verSuffix.Contains("+"))
                {
                    // remove Git hash, if it exists (for "dev" detection)
                    verSuffix = verSuffix.Remove(verSuffix.IndexOf('+'));
                }
            }
            else
            {
                // suffix probably doesn't exist...
                verSuffix = string.Empty;
            }

            return verSuffix.ToLowerInvariant();
        }

        internal static string GetVerString()
        {
            // format: X.Y.Z-SUFFIX[.W]+REVISION,
            // where W is a beta/release candidate version if applicable
            string prodVer = Application.ProductVersion;

            return GetVerSuffix() switch
            {
                // only show the version number (e.g. X.Y.Z):
                "release" => prodVer.Remove(prodVer.IndexOf('-')),
                "dev" => prodVer.Contains("+")
                    // probably a development release (e.g. X.Y.Z-dev+REVISION);
                    // show shortened Git commit hash if it exists:
                    ? prodVer.Remove(prodVer.IndexOf('+') + 8)
                    // Return the product version if not in expected format
                    : prodVer,
                // everything else (i.e. beta, RC, etc.)
                _ => prodVer.Contains(".") && prodVer.Contains("+")
                    // Beta releases should be in format X.Y.Z-beta.W+REVISION.
                    // Remove the revision (i.e. only show X.Y.Z-beta.W):
                    ? prodVer.Remove(prodVer.IndexOf('+'))
                    // Just return the product version if not in expected format
                    : prodVer,
            };
        }
    }
}
