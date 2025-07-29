# Refresh Rate Tuner

A lightweight screen refresh rate changer for Windows.

## Disclaimer

- This program is not yet complete; expect things to be missing.

## Features

- **Automatic refresh rate switching:** Change your laptop refresh rate
  when running on battery to save power.
- **Multi-display support:** Refresh Rate Tuner can automatically apply refresh
  rates for multiple connected displays.
- **Lightweight:** The Refresh Rate Tuner executable is only 30 KB in size, and
  is designed to be light on your laptop's CPU.
- **System startup support:** Refresh Rate Tuner starts up with your computer
  and automatically applies your previous refresh rate settings.

## Supported systems

Any computer or laptop with a display that has more than one refresh rate.

If your display only supports one refresh rate, you can try using
[Custom Resolution Utility](https://customresolutionutility.net/) to add more. **(Use at your own risk!)**

## Roadmap

No new features are currently planned.

## FAQ

### What versions of Windows will this run on?

In theory, any version of Windows that can run any .NET version supported by
this project (either .NET Framework 3.5 or 4.8, or .NET 8), i.e. Windows XP SP2
or later (since it is .NET Framework 3.5's minimum supported Windows version).
x86, x64, and ARM architectures should all work (where supported by the OS/.NET version).

### What versions of Windows do you support?

Windows 10 and 11, both 64-bit. The .NET Framework 4.8 or .NET 8 variants are recommended on these OSes.

Please don't daily drive Windows 7, XP, etc. in 2025.

## Compiling

### Using Visual Studio

1.  Install Visual Studio 2022 with the `.NET Desktop Development` workload checked.
2.  Download the code repository, or clone it with `git`.
3.  Extract the downloaded code, if needed.
4.  Open `RefreshRateTuner.sln` in Visual Studio.
5.  Click `Build` > `Build Solution` to build everything.
6.  Your output, assuming default build settings, is located in `RefreshRateTuner\bin\Debug\`.
    The subfolders each contain a variant of Refresh Rate Tuner compiled against a different version of .NET.
7.  ???
8.  Profit!

### From the command line

1.  Follow steps 1-3 above to install Visual Studio and download the code.
2.  Open `Developer Command Prompt for VS 2022` and `cd` to your project directory.
3.  Run `msbuild /t:restore` to restore the solution, including NuGet packages.
4.  Run `msbuild RefreshRateTuner.sln /p:platform="Any CPU" /p:configuration="Debug"` to build
    the project, substituting `Debug` with `Release` (or `Any CPU` with `x86` or `x64`)
    if you want a release build instead.
5.  Your output should be located in `RefreshRateTuner\bin\Debug\`, assuming you built
    with the above unmodified command. The subfolders each contain a variant of Refresh Rate Tuner compiled against a different version of .NET.
6.  ???
7.  Profit!

## License and Copyright

Copyright © 2025 Sparronator9999.

This program is free software: you can redistribute it and/or modify it under
the terms of the GNU General Public License as published by the Free Software
Foundation, either version 3 of the License, or (at your option) any later
version.

This program is distributed in the hope that it will be useful, but WITHOUT ANY
WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A
PARTICULAR PURPOSE. See the [GNU General Public License](LICENSE.md) for more
details.
