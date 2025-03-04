using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;

namespace TByd.CodeStyle.Editor.CodeCheck.IDE
{
    /// <summary>
    /// VS Code IDE集成实现
    /// </summary>
    public class VSCodeIntegration : IDEIntegrationBase
    {
        // VS Code配置文件名
        private const string c_VSCodeConfigFileName = ".editorconfig";

        // VS Code设置目录
        private const string c_VSCodeSettingsDirectory = ".vscode";

        // VS Code设置文件
        private const string c_VSCodeSettingsFileName = "settings.json";

        // VS Code OmniSharp配置文件
        private const string c_VSCodeOmniSharpFileName = "omnisharp.json";

        // VS Code插件类名
        private const string c_VSCodeUnityIntegrationClassName = "VSCodeEditor.VSCodeScriptEditor";

        /// <summary>
        /// IDE名称
        /// </summary>
        public override string Name => "VS Code";

        /// <summary>
        /// 是否已安装
        /// </summary>
        public override bool IsInstalled
        {
            get
            {
                // 检查VS Code是否已安装
                var vscodePath = GetVSCodeExecutablePath();
                return !string.IsNullOrEmpty(vscodePath) && File.Exists(vscodePath);
            }
        }

        /// <summary>
        /// 导出配置到VS Code
        /// </summary>
        /// <param name="_rules">EditorConfig规则</param>
        /// <returns>是否成功</returns>
        public override bool ExportConfig(List<EditorConfigRule> _rules)
        {
            try
            {
                // 获取VS Code配置目录
                var configPath = GetVSCodeConfigPath();
                if (string.IsNullOrEmpty(configPath))
                {
                    Debug.LogError("[TByd.CodeStyle] 未找到VS Code配置目录");
                    return false;
                }

                // 确保配置目录存在
                Directory.CreateDirectory(configPath);

                // 导出EditorConfig规则
                var editorConfigPath = Path.Combine(configPath, c_VSCodeConfigFileName);
                EditorConfigManager.SaveRulesToFile(_rules, editorConfigPath);

                // 更新VS Code设置
                UpdateVSCodeSettings(configPath);

                // 创建OmniSharp配置
                CreateOmniSharpConfig(configPath);

                Debug.Log($"[TByd.CodeStyle] 成功导出配置到VS Code: {editorConfigPath}");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 导出配置到VS Code失败: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// 检查是否使用VS Code作为脚本编辑器
        /// </summary>
        /// <returns>是否使用VS Code作为脚本编辑器</returns>
        private bool IsVSCodeScriptEditor()
        {
            // 方法1：检查EditorPrefs
            string scriptEditorPref = EditorPrefs.GetString("kScriptsDefaultApp", "");
            if (!string.IsNullOrEmpty(scriptEditorPref) && 
                (scriptEditorPref.IndexOf("Code", StringComparison.OrdinalIgnoreCase) >= 0 ||
                 scriptEditorPref.IndexOf("VSCode", StringComparison.OrdinalIgnoreCase) >= 0))
            {
                return true;
            }

            // 方法2：检查当前脚本编辑器类型
            try
            {
                var editorType = Type.GetType(c_VSCodeUnityIntegrationClassName, false);
                if (editorType != null)
                {
                    var currentEditorProperty = editorType.GetProperty("CurrentEditor", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                    if (currentEditorProperty != null)
                    {
                        var currentEditor = currentEditorProperty.GetValue(null);
                        return currentEditor != null && currentEditor.GetType().Name.Contains("VSCode");
                    }
                }
            }
            catch (Exception)
            {
                // 忽略异常
            }

            return false;
        }

        /// <summary>
        /// 更新VS Code设置
        /// </summary>
        /// <param name="_vscodeDir">VS Code设置目录</param>
        private void UpdateVSCodeSettings(string _vscodeDir)
        {
            try
            {
                // 设置文件路径
                string settingsPath = Path.Combine(_vscodeDir, c_VSCodeSettingsFileName);

                // 默认设置
                string defaultSettings = @"{
    ""editor.formatOnSave"": true,
    ""editor.formatOnType"": true,
    ""editor.formatOnPaste"": true,
    ""editor.codeActionsOnSave"": {
        ""source.fixAll"": true,
        ""source.organizeImports"": true
    },
    ""editor.rulers"": [120],
    ""files.trimTrailingWhitespace"": true,
    ""files.insertFinalNewline"": true,
    ""files.trimFinalNewlines"": true,
    ""csharp.format.enable"": true,
    ""csharp.semanticHighlighting.enabled"": true,
    ""omnisharp.enableEditorConfigSupport"": true,
    ""omnisharp.enableRoslynAnalyzers"": true,
    ""omnisharp.useModernNet"": true,
    ""omnisharp.enableImportCompletion"": true,
    ""omnisharp.organizeImportsOnFormat"": true,
    ""[csharp]"": {
        ""editor.defaultFormatter"": ""ms-dotnettools.csharp"",
        ""editor.formatOnSave"": true,
        ""editor.formatOnType"": true
    }
}";

                // 如果设置文件不存在，创建默认设置
                if (!File.Exists(settingsPath))
                {
                    File.WriteAllText(settingsPath, defaultSettings);
                    return;
                }

                // 读取现有设置
                string existingSettings = File.ReadAllText(settingsPath);

                // 如果已经包含EditorConfig支持，不需要更新
                if (existingSettings.Contains("omnisharp.enableEditorConfigSupport"))
                {
                    return;
                }

                // 解析JSON
                // 注意：在实际项目中，应该使用JSON库来处理，这里简化处理
                if (existingSettings.Trim().EndsWith("}"))
                {
                    // 移除最后的大括号
                    existingSettings = existingSettings.Trim().TrimEnd('}');

                    // 添加EditorConfig支持
                    string updatedSettings = existingSettings + @",
    ""omnisharp.enableEditorConfigSupport"": true,
    ""omnisharp.enableRoslynAnalyzers"": true,
    ""omnisharp.useModernNet"": true,
    ""omnisharp.enableImportCompletion"": true,
    ""omnisharp.organizeImportsOnFormat"": true,
    ""[csharp]"": {
        ""editor.defaultFormatter"": ""ms-dotnettools.csharp"",
        ""editor.formatOnSave"": true,
        ""editor.formatOnType"": true
    }
}";

                    // 保存更新后的设置
                    File.WriteAllText(settingsPath, updatedSettings);
                }
                else
                {
                    // 如果JSON格式不正确，直接使用默认设置
                    File.WriteAllText(settingsPath, defaultSettings);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 更新VS Code设置失败: {e.Message}");
            }
        }

        /// <summary>
        /// 创建OmniSharp配置
        /// </summary>
        /// <param name="_vscodeDir">VS Code设置目录</param>
        private void CreateOmniSharpConfig(string _vscodeDir)
        {
            try
            {
                string configContent = @"{
    ""FormattingOptions"": {
        ""EnableEditorConfigSupport"": true,
        ""OrganizeImports"": true,
        ""NewLine"": ""\n"",
        ""UseTabs"": false,
        ""TabSize"": 4,
        ""IndentationSize"": 4,
        ""SpacingAfterMethodDeclarationName"": false,
        ""SpaceWithinMethodDeclarationParenthesis"": false,
        ""SpaceBetweenEmptyMethodDeclarationParentheses"": false,
        ""SpaceAfterMethodCallName"": false,
        ""SpaceWithinMethodCallParentheses"": false,
        ""SpaceBetweenEmptyMethodCallParentheses"": false,
        ""SpaceAfterControlFlowStatementKeyword"": true,
        ""SpaceWithinExpressionParentheses"": false,
        ""SpaceWithinCastParentheses"": false,
        ""SpaceWithinOtherParentheses"": false,
        ""SpaceAfterCast"": false,
        ""SpacesIgnoreAroundVariableDeclaration"": false,
        ""SpaceBeforeOpenSquareBracket"": false,
        ""SpaceBetweenEmptySquareBrackets"": false,
        ""SpaceWithinSquareBrackets"": false,
        ""SpaceAfterColonInBaseTypeDeclaration"": true,
        ""SpaceAfterComma"": true,
        ""SpaceAfterDot"": false,
        ""SpaceAfterSemicolonsInForStatement"": true,
        ""SpaceBeforeColonInBaseTypeDeclaration"": true,
        ""SpaceBeforeComma"": false,
        ""SpaceBeforeDot"": false,
        ""SpaceBeforeSemicolonsInForStatement"": false,
        ""SpacingAroundBinaryOperator"": ""single"",
        ""IndentBraces"": false,
        ""IndentBlock"": true,
        ""IndentSwitchSection"": true,
        ""IndentSwitchCaseSection"": true,
        ""IndentSwitchCaseSectionWhenBlock"": true,
        ""LabelPositioning"": ""oneLess"",
        ""WrappingPreserveSingleLine"": true,
        ""WrappingKeepStatementsOnSingleLine"": true,
        ""NewLinesForBracesInTypes"": true,
        ""NewLinesForBracesInMethods"": true,
        ""NewLinesForBracesInProperties"": true,
        ""NewLinesForBracesInAccessors"": true,
        ""NewLinesForBracesInAnonymousMethods"": true,
        ""NewLinesForBracesInControlBlocks"": true,
        ""NewLinesForBracesInAnonymousTypes"": true,
        ""NewLinesForBracesInObjectCollectionArrayInitializers"": true,
        ""NewLinesForBracesInLambdaExpressionBody"": true,
        ""NewLineForElse"": true,
        ""NewLineForCatch"": true,
        ""NewLineForFinally"": true,
        ""NewLineForMembersInObjectInit"": true,
        ""NewLineForMembersInAnonymousTypes"": true,
        ""NewLineForClausesInQuery"": true
    },
    ""RoslynExtensionsOptions"": {
        ""EnableAnalyzersSupport"": true,
        ""EnableDecompilationSupport"": true,
        ""EnableImportCompletion"": true
    },
    ""FileOptions"": {
        ""SystemExcludeSearchPatterns"": [""**/node_modules/**/*"", ""**/bin/**/*"", ""**/obj/**/*""],
        ""ExcludeSearchPatterns"": []
    }
}";

                string configPath = Path.Combine(_vscodeDir, c_VSCodeOmniSharpFileName);
                File.WriteAllText(configPath, configContent);
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 创建OmniSharp配置失败: {e.Message}");
            }
        }

        /// <summary>
        /// 获取VS Code可执行文件路径
        /// </summary>
        /// <returns>VS Code可执行文件路径</returns>
        private string GetVSCodeExecutablePath()
        {
            // 首先尝试从Unity编辑器设置中获取
            string vscodePathFromUnity = GetVSCodePathFromUnityPrefs();
            if (!string.IsNullOrEmpty(vscodePathFromUnity) && File.Exists(vscodePathFromUnity))
            {
                return vscodePathFromUnity;
            }

            // 在Windows上查找VS Code
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                // 尝试从注册表获取
                string pathFromRegistry = GetVSCodePathFromRegistry();
                if (!string.IsNullOrEmpty(pathFromRegistry) && File.Exists(pathFromRegistry))
                {
                    return pathFromRegistry;
                }

                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                string programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                // 标准安装路径
                string[] windowsPaths = new string[]
                {
                    Path.Combine(localAppData, "Programs", "Microsoft VS Code", "Code.exe"),
                    Path.Combine(programFiles, "Microsoft VS Code", "Code.exe"),
                    Path.Combine(programFilesX86, "Microsoft VS Code", "Code.exe")
                };

                foreach (string path in windowsPaths)
                {
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }

                // 检查常见的自定义安装位置
                string[] commonCustomPaths = new string[]
                {
                    @"C:\VSCode\Code.exe",
                    @"D:\VSCode\Code.exe",
                    @"E:\VSCode\Code.exe",
                    @"F:\VSCode\Code.exe",
                    @"G:\VSCode\Code.exe",
                    Path.Combine(userProfile, "VSCode", "Code.exe"),
                    @"C:\Program Files\VSCode\Code.exe",
                    @"D:\Program Files\VSCode\Code.exe",
                    @"E:\Program Files\VSCode\Code.exe",
                    @"F:\Program Files\VSCode\Code.exe",
                    @"G:\Program Files\VSCode\Code.exe"
                };

                foreach (string path in commonCustomPaths)
                {
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }

                // 检查所有逻辑驱动器的根目录
                foreach (string drive in Directory.GetLogicalDrives())
                {
                    // 检查驱动器根目录下的VSCode目录
                    string vscodePath = Path.Combine(drive, "VSCode");
                    if (Directory.Exists(vscodePath))
                    {
                        string exePath = Path.Combine(vscodePath, "Code.exe");
                        if (File.Exists(exePath))
                        {
                            return exePath;
                        }
                    }

                    // 检查驱动器根目录下的Microsoft VS Code目录
                    string msvscodePath = Path.Combine(drive, "Microsoft VS Code");
                    if (Directory.Exists(msvscodePath))
                    {
                        string exePath = Path.Combine(msvscodePath, "Code.exe");
                        if (File.Exists(exePath))
                        {
                            return exePath;
                        }
                    }

                    // 递归搜索驱动器根目录下的所有目录（限制深度为2）
                    try
                    {
                        string[] rootDirs = Directory.GetDirectories(drive);
                        foreach (string rootDir in rootDirs)
                        {
                            // 检查是否包含Code.exe
                            string exePath = Path.Combine(rootDir, "Code.exe");
                            if (File.Exists(exePath))
                            {
                                return exePath;
                            }

                            // 检查子目录
                            try
                            {
                                string[] subDirs = Directory.GetDirectories(rootDir);
                                foreach (string subDir in subDirs)
                                {
                                    if (subDir.Contains("VS Code", StringComparison.OrdinalIgnoreCase) || 
                                        subDir.Contains("VSCode", StringComparison.OrdinalIgnoreCase))
                                    {
                                        exePath = Path.Combine(subDir, "Code.exe");
                                        if (File.Exists(exePath))
                                        {
                                            return exePath;
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                // 忽略访问受限的目录
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // 忽略访问受限的驱动器
                    }
                }
            }
            // 在macOS上查找VS Code
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
                string[] macPaths = new string[]
                {
                    "/Applications/Visual Studio Code.app/Contents/MacOS/Electron",
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/Applications/Visual Studio Code.app/Contents/MacOS/Electron",
                    "/usr/local/bin/code"
                };

                foreach (string path in macPaths)
                {
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }
            }
            // 在Linux上查找VS Code
            else if (Application.platform == RuntimePlatform.LinuxEditor)
            {
                string[] linuxPaths = new string[]
                {
                    "/usr/bin/code",
                    "/usr/local/bin/code",
                    "/snap/bin/code",
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/bin/code"
                };

                foreach (string path in linuxPaths)
                {
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 从Unity编辑器首选项中获取VS Code路径
        /// </summary>
        /// <returns>VS Code可执行文件路径</returns>
        private string GetVSCodePathFromUnityPrefs()
        {
            try
            {
                // 尝试从Unity编辑器设置中获取VS Code路径
                string editorPath = EditorPrefs.GetString("kScriptsDefaultApp");
                if (!string.IsNullOrEmpty(editorPath) && 
                    (editorPath.Contains("Code.exe", StringComparison.OrdinalIgnoreCase) || 
                     editorPath.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase)))
                {
                    return editorPath;
                }
            }
            catch (Exception)
            {
                // 忽略异常
            }
            return string.Empty;
        }

        /// <summary>
        /// 从Windows注册表获取VS Code路径
        /// </summary>
        /// <returns>VS Code可执行文件路径</returns>
        private string GetVSCodePathFromRegistry()
        {
#if UNITY_EDITOR_WIN
            try
            {
                // 尝试从注册表获取VS Code路径
                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall"))
                {
                    if (key != null)
                    {
                        foreach (string subKeyName in key.GetSubKeyNames())
                        {
                            using (Microsoft.Win32.RegistryKey subKey = key.OpenSubKey(subKeyName))
                            {
                                if (subKey != null)
                                {
                                    string displayName = subKey.GetValue("DisplayName") as string;
                                    if (!string.IsNullOrEmpty(displayName) && displayName.Contains("Visual Studio Code", StringComparison.OrdinalIgnoreCase))
                                    {
                                        string installLocation = subKey.GetValue("InstallLocation") as string;
                                        if (!string.IsNullOrEmpty(installLocation))
                                        {
                                            string exePath = Path.Combine(installLocation, "Code.exe");
                                            if (File.Exists(exePath))
                                            {
                                                return exePath;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // 尝试从另一个注册表位置获取
                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Code.exe"))
                {
                    if (key != null)
                    {
                        string path = key.GetValue(null) as string;
                        if (!string.IsNullOrEmpty(path) && File.Exists(path))
                        {
                            return path;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // 忽略注册表访问异常
            }
#endif
            return string.Empty;
        }

        /// <summary>
        /// 获取VS Code配置目录
        /// </summary>
        private string GetVSCodeConfigPath()
        {
            // 获取项目根目录
            string projectRoot = Path.GetDirectoryName(Application.dataPath);

            // 返回.vscode目录路径
            return Path.Combine(projectRoot, c_VSCodeSettingsDirectory);
        }
    }
} 