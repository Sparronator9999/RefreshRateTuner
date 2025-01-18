# Refresh Rate Tuner

A lightweight screen refresh rate changer for Windows.

## Disclaimer

- This program is not yet complete; expect things to be missing.

## Features

- **Automatic refresh rate switching:** Change your laptop refresh rate
  when running on battery to save power.
- **Lightweight:** Refresh Rate Tuner is only a few KB in size, and is
  designed to be light on your laptop's CPU.

This feature list will grow as more features are added (see the [disclaimer](#disclaimer)).

## Supported systems

Any computer or laptop with a display that has more than one refresh rate.

## Roadmap

- [x] Save selected refresh rates between sessions
- [ ] System startup support
- [ ] Minimise to system tray

## FAQ

### What versions of Windows will this run on?

Any version of Windows that can run .NET Framework 4.8 (Windows 7 SP1 and later), both 32-bit and 64-bit. ARM should work as well, but I haven't tested it (due to lack of ARM-powered laptop).

### What versions of Windows do you support?

Windows 10 and 11, both 64-bit.

Please don't daily drive Windows 7, XP, etc. in 2025.

### .NET (Core) 5/6/8/<insert latest .NET version>!

Soon™.

## Compiling

### Using Visual Studio

1.  Install Visual Studio 2022 with the `.NET Desktop Development` workload checked.
2.  Download the code repository, or clone it with `git`.
3.  Extract the downloaded code, if needed.
4.  Open `RefreshRateTuner.sln` in Visual Studio.
5.  Click `Build` > `Build Solution` to build everything.
6.  Your output, assuming default build settings, is located in `RefreshRateTuner\bin\Debug\net48\`.
7.  ???
8.  Profit!

### From the command line

1.  Follow steps 1-3 above to install Visual Studio and download the code.
2.  Open `Developer Command Prompt for VS 2022` and `cd` to your project directory.
3.  Run `msbuild /t:restore` to restore the solution, including NuGet packages.
4.  Run `msbuild RefreshRateTuner.sln /p:platform="Any CPU" /p:configuration="Debug"` to build
    the project, substituting `Debug` with `Release` (or `Any CPU` with `x86` or `x64`)
    if you want a release build instead.
5.  Your output should be located in `RefreshRateTuner\bin\Debug\net48\`, assuming you built
    with the above unmodified command.
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
