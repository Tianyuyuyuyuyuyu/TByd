#define MyAppName "TByd NPM"
#define MyAppVersion "0.1.0-beta.2"
#define MyAppPublisher "TByd Team"
#define MyAppURL "https://github.com/Tianyuyuyuyuyuyu/TByd"
#define MyAppExeName "npm_registry_manage.exe"
#define MyAppSourceDir "D:\UnityProjects\TByd\TByd.VisualizationTools\NpmRegistryPackageManageTool"

[Setup]
; 注意: AppId的值为唯一标识此应用程序。
; 不要在其他安装程序中使用相同的AppId值。
AppId={{D3A3D9B1-7E37-44C6-A73E-B5F9D7FF8C3D}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
; 删除上一个版本时保留用户数据
UsePreviousAppDir=yes
; 推荐的设置
OutputDir={#MyAppSourceDir}\installer
OutputBaseFilename=tbyd_npm_setup
SetupIconFile={#MyAppSourceDir}\windows\runner\resources\app_icon.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
; 需要管理员权限
PrivilegesRequired=admin
; 使用公共区域而不是用户区域
UsedUserAreasWarning=no

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "chinesesimplified"; MessagesFile: "compiler:Languages\ChineseSimplified.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#MyAppSourceDir}\build\windows\x64\runner\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#MyAppSourceDir}\build\windows\x64\runner\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; 注意: 不要在任何共享系统文件上使用"Flags: ignoreversion"

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[UninstallDelete]
Type: filesandordirs; Name: "{app}"

[Code]
function InitializeSetup(): Boolean;
var
  ResultCode: Integer;
begin
  Result := True;
  // 检查是否已安装旧版本
  if RegKeyExists(HKEY_LOCAL_MACHINE, 'Software\Microsoft\Windows\CurrentVersion\Uninstall\{#SetupSetting("AppId")}_is1') then
  begin
    // 提示用户卸载旧版本
    if MsgBox('检测到已安装旧版本，是否卸载？' + #13#10 + '点击"是"卸载旧版本，点击"否"继续安装。', mbConfirmation, MB_YESNO) = IDYES then
    begin
      // 运行卸载程序
      if not Exec(ExpandConstant('{uninstallexe}'), '/SILENT', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
      begin
        MsgBox('卸载旧版本失败，请手动卸载后重试。', mbError, MB_OK);
        Result := False;
      end;
    end;
  end;
end; 