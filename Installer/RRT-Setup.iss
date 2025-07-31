; Defined when compiled via GitHub Actions. Uncomment one of the below when compiling locally.
;#define BuildConfig "Debug"
;#define BuildConfig "Release"

; Define constants used in other sections of the installer.
#define AppName "Refresh Rate Tuner"
#define AppVer "1.0.0"
#define AppVerFriendly "1.0"
#define AppPublisher "Sparronator9999"
#define AppURL "https://github.com/Sparronator9999/RefreshRateTuner"
#define AppExe "RRT.exe"

; Used to determine which Win32 function to use (ANSI or Unicode version).
; Should resolve to "W" since Inno Setup 6 and later since the Unicode version is always used in that case.
#ifdef UNICODE
  #define AW "W"
#else
  #define AW "A"
#endif

[Setup]
AllowNetworkDrive=no
AllowUNCPath=no
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{51F1C00A-8E28-4510-AC19-956B6D9049F6}
AppName={#AppName}
AppVersion={#AppVerFriendly} ({#BuildConfig})
AppPublisher={#AppPublisher}
AppPublisherURL={#AppURL}
AppSupportURL={#AppURL}/issues
AppUpdatesURL={#AppURL}/releases
ArchitecturesAllowed=x86compatible or x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
Compression=lzma/ultra64
DefaultDirName={autopf}\{#AppPublisher}\{#AppName}
DisableProgramGroupPage=yes
DisableWelcomePage=no
LicenseFile=Installer\LICENSE.rtf
LZMANumFastBytes=273
OutputBaseFilename=RRT-v{#AppVer}-{#BuildConfig}-setup
SetupIconFile=Installer\installer.ico
SetupMutex=RRT-Setup-{{51F1C00A-8E28-4510-AC19-956B6D9049F6}
SolidCompression=yes
SourceDir=..
Uninstallable=not IsPortableMode
UninstallDisplayIcon={app}\{#AppExe}
WizardStyle=modern
WizardImageFile=Installer\setup.bmp
WizardSmallImageFile=Installer\setup-small.bmp

[CustomMessages]
english.StartMenu=Start menu:
english.StartIcons=Create Start menu shortcut
english.DeskIcons=Create desktop icon
english.DeskIconsCommon=For all users
english.DeskIconsUser=For the current user only
english.LaunchCE=Launch Refresh Rate Tuner

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "starticons"; Description: "{cm:StartIcons}"; GroupDescription: "{cm:StartMenu}"
Name: "deskicons"; Description: "{cm:DeskIcons}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "deskicons\common"; Description: "{cm:DeskIconsCommon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: exclusive unchecked
Name: "deskicons\user"; Description: "{cm:DeskIconsUser}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: exclusive unchecked

[Files]
Source: "RefreshRateTuner\bin\{#BuildConfig}\net48\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\{#AppName}\{#AppName}"; Filename: "{app}\{#AppExe}"; Tasks: starticons; Check: not IsPortableMode
Name: "{commondesktop}\{#AppName} {#AppName}"; Filename: "{app}\{#AppExe}"; Tasks: deskicons\common
Name: "{userdesktop}\{#AppName} {#AppName}"; Filename: "{app}\{#AppExe}"; Tasks: deskicons\user

[Code]
var
  SetupTypePage: TInputOptionWizardPage;

function IsPortableMode: Boolean;
begin
  Result := SetupTypePage.SelectedValueIndex = 1;
end;

function InitializeSetup: Boolean;
begin
  // make sure the correct .NET Framework is installed
  Result := IsDotNetInstalled(net48, 0)
  if not Result then
    SuppressibleMsgBox(FmtMessage(SetupMessage(msgWinVersionTooLowError), ['.NET Framework', '4.8']), mbCriticalError, MB_OK, IDOK);
end;

procedure InitializeWizard;
begin
  // https://stackoverflow.com/a/25811746
  SetupTypePage := CreateInputOptionPage(wpLicense, 'Setup type', 'How to install Refresh Rate Tuner', 'Should Refresh Rate Tuner be installed like any other app, or just extracted to a selected directory?', True, False);
  SetupTypePage.Add('Standard install (select this option if unsure)');
  SetupTypePage.Add('Portable mode (extract files only)');
  SetupTypePage.Values[0] := True;
  SetupTypePage.Values[1] := False;
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  if IsPortableMode then
  begin
    case PageID of
      wpSelectComponents, wpSelectProgramGroup, wpSelectTasks, wpReady:
        Result := True;
    else
      Result := False;
    end;
  end
  else
    Result := False;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  AppDataDir: String;
begin
  // Ask to delete the Refresh Rate Tuner data directory on successful uninstall.
  if CurUninstallStep = usPostUninstall then
  begin
    AppDataDir := ExpandConstant('{userappdata}\Sparronator9999\RefreshRateTuner')
    if DirExists(AppDataDir) and (SuppressibleMsgBox('Keep Refresh Rate Tuner''s config (located at `' + AppDataDir + '`)?' + #10#10
      'Click "No" if you don''t intend to re-install Refresh Rate Tuner.', mbConfirmation, MB_YESNO, IDYES) = IDNO) then
    begin
      Log('Deleting Refresh Rate Tuner''s config...');
      DelTree(AppDataDir, True, True, True);
    end
    else
      Log('Leaving Refresh Rate Tuner''s config untouched.');
  end;
end;
