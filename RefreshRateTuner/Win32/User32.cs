using System;
using System.Drawing;
using System.Runtime.InteropServices;

// DefaultDllImportSearchPaths not supported by old .NET framework...
#if NET45_OR_GREATER || NETCOREAPP
[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
#endif

namespace RefreshRateTuner.Win32
{
    internal sealed class User32
    {
        public const int ENUM_CURRENT_SETTINGS = -1;
        public const int ENUM_REGISTRY_SETTINGS = -2;

        [DllImport("user32.dll", SetLastError = true, ExactSpelling = true,
            CharSet = CharSet.Unicode, EntryPoint = "EnumDisplayDevicesW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumDisplayDevices(
            [MarshalAs(UnmanagedType.LPWStr)] string lpDevice,
            int iDevNum,
            [MarshalAs(UnmanagedType.Struct)] ref DisplayDevice lpDisplayDevice,
            int dwFlags);

        public static bool EnumDisplayDevices(
            string device,
            int devNum,
            out string devName)
        {
            DisplayDevice dispDev = new()
            {
#if NET451_OR_GREATER || NETCOREAPP
                cb = Marshal.SizeOf<DisplayDevice>(),
#else
                cb = Marshal.SizeOf(typeof(DisplayDevice)),
#endif
            };

                bool success = EnumDisplayDevices(device, devNum, ref dispDev, 0);
            devName = dispDev.DeviceName;
            return success;
        }

        [DllImport("user32.dll", SetLastError = true, ExactSpelling = true,
            CharSet = CharSet.Unicode, EntryPoint = "EnumDisplaySettingsW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumDisplaySettings(
            [MarshalAs(UnmanagedType.LPWStr)] string devName,
            int mode,
            [MarshalAs(UnmanagedType.Struct)] ref DeviceMode devMode);

        public static DisplaySettings EnumDisplaySettings(
            string devName,
            int mode)
        {
            DeviceMode devMode = new()
            {
#if NET451_OR_GREATER || NETCOREAPP
                Size = (ushort)Marshal.SizeOf<DeviceMode>(),
#else
                Size = (ushort)Marshal.SizeOf(typeof(DeviceMode)),
#endif
                DriverExtra = 0,
            };

            return EnumDisplaySettings(devName, mode, ref devMode)
                ? new DisplaySettings
                {
                    Name = devName,
                    BitsPerPixel = devMode.BitsPerPel,
                    Width = devMode.PelsWidth,
                    Height = devMode.PelsHeight,
                    RefreshRate = devMode.DisplayFrequency,
                }
                : null;
        }

        [DllImport("user32.dll", SetLastError = true, ExactSpelling = true,
            CharSet = CharSet.Unicode, EntryPoint = "ChangeDisplaySettingsExW")]
        private static extern DispChange ChangeDisplaySettingsEx(
        [MarshalAs(UnmanagedType.LPWStr)] string deviceName,
            [MarshalAs(UnmanagedType.Struct)] ref DeviceMode devMode,
            IntPtr hwnd,
            CDSFlags flags,
            IntPtr lParam);

        public static DispChange ChangeRefreshRate(
            [MarshalAs(UnmanagedType.LPWStr)] string deviceName,
            int refreshRate,
            CDSFlags flags)
        {
            DeviceMode devMode = new()
            {
#if NET451_OR_GREATER || NETCOREAPP
                Size = (ushort)Marshal.SizeOf<DeviceMode>(),
#else
                Size = (ushort)Marshal.SizeOf(typeof(DeviceMode)),
#endif
                DriverExtra = 0,
                DisplayFrequency = refreshRate,
                Fields = DM.DisplayFrequency,
            };

            return ChangeDisplaySettingsEx(
                deviceName, ref devMode, IntPtr.Zero, flags, IntPtr.Zero);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        private struct DisplayDevice
        {
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;

            public int StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
        private struct DeviceMode
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            public ushort SpecVersion;
            public ushort DriverVersion;
            public ushort Size;
            public ushort DriverExtra;
            public DM Fields;

            [MarshalAs(UnmanagedType.Struct)]
            public Point Position;
            public int DisplayOrientation;
            public int DisplayFixedOutput;

            public short Color;
            public short Duplex;
            public short YResolution;
            public short TTOption;
            public short Collate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FormName;
            public ushort LogPixels;

            public int BitsPerPel;
            public int PelsWidth;
            public int PelsHeight;
            public int DisplayFlags;
            public int DisplayFrequency;

            public int ICMMethod;
            public int ICMIntent;
            public int MediaType;
            public int DitherType;
            public int Reserved1;
            public int Reserved2;
            public int PanningWidth;
            public int PanningHeight;
        }

        [Flags]
        private enum DM
        {
            Orientation = 0x1,
            PaperSize = 0x2,
            PaperLength = 0x4,
            PaperWidth = 0x8,
            Scale = 0x10,
            Position = 0x20,
            NUP = 0x40,
            DisplayOrientation = 0x80,
            Copies = 0x100,
            DefaultSource = 0x200,
            PrintQuality = 0x400,
            Color = 0x800,
            Duplex = 0x1000,
            YResolution = 0x2000,
            TTOption = 0x4000,
            Collate = 0x8000,
            FormName = 0x10000,
            LogPixels = 0x20000,
            BitsPerPixel = 0x40000,
            PelsWidth = 0x80000,
            PelsHeight = 0x100000,
            DisplayFlags = 0x200000,
            DisplayFrequency = 0x400000,
            ICMMethod = 0x800000,
            ICMIntent = 0x1000000,
            MeduaType = 0x2000000,
            DitherType = 0x4000000,
            PanningWidth = 0x8000000,
            PanningHeight = 0x10000000,
            DisplayFixedOutput = 0x20000000
        }
    }

    internal enum CDSFlags : uint
    {
        None = 0,
        UpdateRegistry = 0x00000001,
        Test = 0x00000002,
        FullScreen = 0x00000004,
        Global = 0x00000008,
        SetPrimary = 0x00000010,
        VideoParameters = 0x00000020,
        EnableUnsafeModes = 0x00000100,
        DisableUnsafeModes = 0x00000200,
        NoReset = 0x10000000,
        ResetEx = 0x20000000,
        Reset = 0x40000000,
    }

    internal enum DispChange
    {
        Successful = 0,
        Restart = 1,
        Failed = -1,
        BadMode = -2,
        NotUpdated = -3,
        BadFlags = -4,
        BadParam = -5,
        BadDualView = -6
    }
}
