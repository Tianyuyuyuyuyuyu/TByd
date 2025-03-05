using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using UnityEditor;
using UnityEngine;

namespace TByd.CodeStyle.Editor.CodeCheck.IDE
{
    /// <summary>
    /// VS Code IDE集成实现
    /// </summary>
    public class VSCodeIntegration : IdeIntegrationBase
    {
        // VS Code配置文件名
        private const string k_CvsCodeConfigFileName = ".editorconfig";

        // VS Code设置目录
        private const string k_CvsCodeSettingsDirectory = ".vscode";

        // VS Code设置文件
        private const string k_CvsCodeSettingsFileName = "settings.json";

        // VS Code OmniSharp配置文件
        private const string k_CvsCodeOmniSharpFileName = "omnisharp.json";

        // VS Code插件类名

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
        /// <param name="rules">EditorConfig规则</param>
        /// <returns>是否成功</returns>
        public override bool ExportConfig(List<EditorConfigRule> rules)
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
                var editorConfigPath = Path.Combine(configPath, k_CvsCodeConfigFileName);
                EditorConfigManager.SaveRulesToFile(rules, editorConfigPath);

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
        /// 更新VS Code设置
        /// </summary>
        /// <param name="vscodeDir">VS Code设置目录</param>
        private void UpdateVSCodeSettings(string vscodeDir)
        {
            try
            {
                // 设置文件路径
                var settingsPath = Path.Combine(vscodeDir, k_CvsCodeSettingsFileName);

                // 默认设置
                var defaultSettings = @"{
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
                var existingSettings = File.ReadAllText(settingsPath);

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
                    var updatedSettings = existingSettings + @",
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
        /// <param name="vscodeDir">VS Code设置目录</param>
        private void CreateOmniSharpConfig(string vscodeDir)
        {
            try
            {
                var configContent = @"{
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

                var configPath = Path.Combine(vscodeDir, k_CvsCodeOmniSharpFileName);
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
            var vscodePathFromUnity = GetVSCodePathFromUnityPrefs();
            if (!string.IsNullOrEmpty(vscodePathFromUnity) && File.Exists(vscodePathFromUnity))
            {
                return vscodePathFromUnity;
            }

            // 在Windows上查找VS Code
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                // 尝试从注册表获取
                var pathFromRegistry = GetVSCodePathFromRegistry();
                if (!string.IsNullOrEmpty(pathFromRegistry) && File.Exists(pathFromRegistry))
                {
                    return pathFromRegistry;
                }

                var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                var programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                // 标准安装路径
                var windowsPaths = new[]
                {
                    Path.Combine(localAppData, "Programs", "Microsoft VS Code", "Code.exe"),
                    Path.Combine(programFiles, "Microsoft VS Code", "Code.exe"),
                    Path.Combine(programFilesX86, "Microsoft VS Code", "Code.exe")
                };

                foreach (var path in windowsPaths)
                {
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }

                // 检查常见的自定义安装位置
                var commonCustomPaths = new[]
                {
                    @"C:\VSCode\Code.exe", @"D:\VSCode\Code.exe", @"E:\VSCode\Code.exe", @"F:\VSCode\Code.exe",
                    @"G:\VSCode\Code.exe", Path.Combine(userProfile, "VSCode", "Code.exe"),
                    @"C:\Program Files\VSCode\Code.exe", @"D:\Program Files\VSCode\Code.exe",
                    @"E:\Program Files\VSCode\Code.exe", @"F:\Program Files\VSCode\Code.exe",
                    @"G:\Program Files\VSCode\Code.exe"
                };

                foreach (var path in commonCustomPaths)
                {
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }

                // 检查所有逻辑驱动器的根目录
                foreach (var drive in Directory.GetLogicalDrives())
                {
                    // 检查驱动器根目录下的VSCode目录
                    var vscodePath = Path.Combine(drive, "VSCode");
                    if (Directory.Exists(vscodePath))
                    {
                        var exePath = Path.Combine(vscodePath, "Code.exe");
                        if (File.Exists(exePath))
                        {
                            return exePath;
                        }
                    }

                    // 检查驱动器根目录下的Microsoft VS Code目录
                    var msvscodePath = Path.Combine(drive, "Microsoft VS Code");
                    if (Directory.Exists(msvscodePath))
                    {
                        var exePath = Path.Combine(msvscodePath, "Code.exe");
                        if (File.Exists(exePath))
                        {
                            return exePath;
                        }
                    }

                    // 递归搜索驱动器根目录下的所有目录（限制深度为2）
                    try
                    {
                        var rootDirs = Directory.GetDirectories(drive);
                        foreach (var rootDir in rootDirs)
                        {
                            // 检查是否包含Code.exe
                            var exePath = Path.Combine(rootDir, "Code.exe");
                            if (File.Exists(exePath))
                            {
                                return exePath;
                            }

                            // 检查子目录
                            try
                            {
                                var subDirs = Directory.GetDirectories(rootDir);
                                foreach (var subDir in subDirs)
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
                var macPaths = new[]
                {
                    "/Applications/Visual Studio Code.app/Contents/MacOS/Electron",
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/Applications/Visual Studio Code.app/Contents/MacOS/Electron",
                    "/usr/local/bin/code"
                };

                foreach (var path in macPaths)
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
                var linuxPaths = new[]
                {
                    "/usr/bin/code", "/usr/local/bin/code", "/snap/bin/code",
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/bin/code"
                };

                foreach (var path in linuxPaths)
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
                var editorPath = EditorPrefs.GetString("kScriptsDefaultApp");
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
                using (var key = Registry.CurrentUser.OpenSubKey(
                           @"Software\Microsoft\Windows\CurrentVersion\Uninstall"))
                {
                    if (key != null)
                    {
                        foreach (var subKeyName in key.GetSubKeyNames())
                        {
                            using (var subKey = key.OpenSubKey(subKeyName))
                            {
                                if (subKey != null)
                                {
                                    var displayName = subKey.GetValue("DisplayName") as string;
                                    if (!string.IsNullOrEmpty(displayName) && displayName.Contains("Visual Studio Code",
                                            StringComparison.OrdinalIgnoreCase))
                                    {
                                        var installLocation = subKey.GetValue("InstallLocation") as string;
                                        if (!string.IsNullOrEmpty(installLocation))
                                        {
                                            var exePath = Path.Combine(installLocation, "Code.exe");
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
                using (var key = Registry.LocalMachine.OpenSubKey(
                           @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Code.exe"))
                {
                    if (key != null)
                    {
                        var path = key.GetValue(null) as string;
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
            var projectRoot = Path.GetDirectoryName(Application.dataPath);

            // 返回.vscode目录路径
            if (projectRoot != null)
            {
                return Path.Combine(projectRoot, k_CvsCodeSettingsDirectory);
            }

            return string.Empty;
        }
    }
}
