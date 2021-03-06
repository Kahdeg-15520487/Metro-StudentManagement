; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "StudentManagement"
#define MyAppVersion "1.0"
#define MyAppPublisher "Nh�m 4"
#define MyAppURL "https://github.com/hoideptrai123/Metro-StudentManagement"
#define MyAppExeName "StudentManagement.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{6919EE35-DC9A-43F9-AE14-25876317E68A}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableProgramGroupPage=yes
OutputDir=E:\Workspace\c#\Metro-StudentManagement\Installer
OutputBaseFilename=FileCaiDat
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\StudentManagement.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\EntityFramework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\EntityFramework.SqlServer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\EntityFramework.SqlServer.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\EntityFramework.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\MahApps.Metro.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\MahApps.Metro.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\Microsoft.Practices.ServiceLocation.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\Microsoft.Practices.ServiceLocation.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\Microsoft.Practices.ServiceLocation.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\StudentDB.mdf"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\StudentDB_log.ldf"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\StudentManagement.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\StudentManagement.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\System.Windows.Interactivity.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\Workspace\c#\Metro-StudentManagement\StudentManagement\StudentManagement\bin\Release\Resources\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

