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
    /// Visual Studio IDE集成实现
    /// </summary>
    public class VisualStudioIntegration : IdeIntegrationBase
    {
        // Visual Studio配置文件名
        private const string k_CvsConfigFileName = ".editorconfig";

        // Visual Studio设置文件名
        private const string k_CvsSettingsFileName = ".vssettings";

        // Visual Studio插件类名

        /// <summary>
        /// IDE名称
        /// </summary>
        public override string Name => "Visual Studio";

        /// <summary>
        /// 是否已安装
        /// </summary>
        public override bool IsInstalled
        {
            get
            {
                // 检查Visual Studio是否已安装
                var vsPath = GetVSExecutablePath();
                return !string.IsNullOrEmpty(vsPath) && File.Exists(vsPath);
            }
        }

        /// <summary>
        /// 导出配置到Visual Studio
        /// </summary>
        /// <param name="rules">EditorConfig规则</param>
        /// <returns>是否成功</returns>
        public override bool ExportConfig(List<EditorConfigRule> rules)
        {
            try
            {
                // 获取Visual Studio配置目录
                var configPath = GetVisualStudioConfigPath();
                if (string.IsNullOrEmpty(configPath))
                {
                    Debug.LogError("[TByd.CodeStyle] 未找到Visual Studio配置目录");
                    return false;
                }

                // 确保配置目录存在
                Directory.CreateDirectory(configPath);

                // 导出EditorConfig规则
                var editorConfigPath = Path.Combine(configPath, k_CvsConfigFileName);
                EditorConfigManager.SaveRulesToFile(rules, editorConfigPath);

                // 创建Visual Studio设置
                CreateVisualStudioSettings(configPath);

                Debug.Log($"[TByd.CodeStyle] 成功导出配置到Visual Studio: {editorConfigPath}");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 导出配置到Visual Studio失败: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// 创建Visual Studio设置
        /// </summary>
        /// <param name="configPathStr">配置目录路径</param>
        private void CreateVisualStudioSettings(string configPathStr)
        {
            var configContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<UserSettings>
  <ApplicationIdentity version=""16.0""/>
  <ToolsOptions>
    <ToolsOptionsCategory name=""TextEditor"" RegisteredName=""TextEditor"">
      <ToolsOptionsSubCategory name=""CSharp"" RegisteredName=""CSharp"" PackageName=""Text Management Package"">
        <PropertyValue name=""TabSize"">4</PropertyValue>
        <PropertyValue name=""IndentSize"">4</PropertyValue>
        <PropertyValue name=""InsertTabs"">false</PropertyValue>
        <PropertyValue name=""IndentStyle"">2</PropertyValue>
        <PropertyValue name=""ShowChanges"">true</PropertyValue>
        <PropertyValue name=""ShowMarks"">true</PropertyValue>
        <PropertyValue name=""ShowLineNumbers"">true</PropertyValue>
        <PropertyValue name=""ShowVisualBasic"">true</PropertyValue>
        <PropertyValue name=""BraceCompletion"">true</PropertyValue>
        <PropertyValue name=""ShowNavigationBar"">true</PropertyValue>
        <PropertyValue name=""AutoListMembers"">true</PropertyValue>
        <PropertyValue name=""AutoListParams"">true</PropertyValue>
        <PropertyValue name=""CodeDefinitionWindow_DocumentationComment_IndentBase"">1</PropertyValue>
        <PropertyValue name=""CodeDefinitionWindow_DocumentationComment_IndentOffset"">2</PropertyValue>
        <PropertyValue name=""EditAndContinueEnabled"">true</PropertyValue>
        <PropertyValue name=""EditAndContinueReportEnterBreakStateOnFailure"">1</PropertyValue>
        <PropertyValue name=""EnableHighlightReferences"">true</PropertyValue>
        <PropertyValue name=""EnableHighlightCurrentLine"">true</PropertyValue>
        <PropertyValue name=""EnableQuickInfo"">true</PropertyValue>
        <PropertyValue name=""EnableQuickInfoToolTip"">true</PropertyValue>
        <PropertyValue name=""EnableParameterInformation"">true</PropertyValue>
        <PropertyValue name=""EnableOutlining"">true</PropertyValue>
        <PropertyValue name=""EnableAutoOutlining"">true</PropertyValue>
        <PropertyValue name=""EnableSelectionMargin"">true</PropertyValue>
        <PropertyValue name=""EnableLineNumbers"">true</PropertyValue>
        <PropertyValue name=""EnableMouseClickGotoDefinition"">true</PropertyValue>
        <PropertyValue name=""EnableCodeLens"">true</PropertyValue>
        <PropertyValue name=""EnableCodeDefinitionWindow"">true</PropertyValue>
        <PropertyValue name=""EnableSquiggles"">true</PropertyValue>
        <PropertyValue name=""EnableVsStructureGuideLines"">true</PropertyValue>
        <PropertyValue name=""EnableVsStructureGuideLines2"">true</PropertyValue>
        <PropertyValue name=""EnableVsStructureGuideLines3"">true</PropertyValue>
        <PropertyValue name=""EnableVsStructureGuideLines4"">true</PropertyValue>
        <PropertyValue name=""WordWrap"">false</PropertyValue>
        <PropertyValue name=""WordWrapGlyphs"">true</PropertyValue>
        <PropertyValue name=""CutCopyBlankLines"">true</PropertyValue>
        <PropertyValue name=""AutoIndentOnTab"">true</PropertyValue>
        <PropertyValue name=""AutoIndentOnReturn"">true</PropertyValue>
        <PropertyValue name=""IndentOnEnter"">true</PropertyValue>
        <PropertyValue name=""IndentOnPaste"">true</PropertyValue>
        <PropertyValue name=""FormatOnPaste"">true</PropertyValue>
        <PropertyValue name=""FormatOnSave"">true</PropertyValue>
        <PropertyValue name=""RenameTrackingPreview"">true</PropertyValue>
        <PropertyValue name=""ExtractMethodPreview"">true</PropertyValue>
        <PropertyValue name=""ExtractInterfacePreview"">true</PropertyValue>
        <PropertyValue name=""ExtractClassPreview"">true</PropertyValue>
        <PropertyValue name=""EncapsulateFieldPreview"">true</PropertyValue>
        <PropertyValue name=""RemoveParametersPreview"">true</PropertyValue>
        <PropertyValue name=""ReorderParametersPreview"">true</PropertyValue>
        <PropertyValue name=""AddParametersPreview"">true</PropertyValue>
        <PropertyValue name=""RemoveUnusedUsingsPreview"">true</PropertyValue>
        <PropertyValue name=""SortUsingsPreview"">true</PropertyValue>
        <PropertyValue name=""ConvertToAutoPropertyPreview"">true</PropertyValue>
        <PropertyValue name=""GenerateConstructorPreview"">true</PropertyValue>
        <PropertyValue name=""GenerateOverridesPreview"">true</PropertyValue>
        <PropertyValue name=""GenerateDelegatePreview"">true</PropertyValue>
        <PropertyValue name=""GenerateEqualsPreview"">true</PropertyValue>
        <PropertyValue name=""ImplementInterfacePreview"">true</PropertyValue>
        <PropertyValue name=""ImplementAbstractClassPreview"">true</PropertyValue>
        <PropertyValue name=""AddMissingConstructorsPreview"">true</PropertyValue>
        <PropertyValue name=""AddMissingOverridesPreview"">true</PropertyValue>
      </ToolsOptionsSubCategory>
    </ToolsOptionsCategory>
  </ToolsOptions>
</UserSettings>";

            var configPath = Path.Combine(configPathStr, k_CvsSettingsFileName);
            File.WriteAllText(configPath, configContent);
        }

        /// <summary>
        /// 获取Visual Studio可执行文件路径
        /// </summary>
        /// <returns>Visual Studio可执行文件路径</returns>
        private string GetVSExecutablePath()
        {
            // 首先尝试从Unity编辑器设置中获取
            var vsPathFromUnity = GetVSPathFromUnityPrefs();
            if (!string.IsNullOrEmpty(vsPathFromUnity) && File.Exists(vsPathFromUnity))
            {
                return vsPathFromUnity;
            }

            // 在Windows上查找Visual Studio
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                // 尝试从注册表获取
                var pathFromRegistry = GetVSPathFromRegistry();
                if (!string.IsNullOrEmpty(pathFromRegistry) && File.Exists(pathFromRegistry))
                {
                    return pathFromRegistry;
                }

                var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                var programFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                // 查找Visual Studio 2022
                var vs2022Paths = new[]
                {
                    Path.Combine(programFiles, "Microsoft Visual Studio", "2022"),
                    Path.Combine(programFilesX86, "Microsoft Visual Studio", "2022")
                };

                foreach (var basePath in vs2022Paths)
                {
                    if (Directory.Exists(basePath))
                    {
                        var editions = new[] { "Enterprise", "Professional", "Community" };
                        foreach (var edition in editions)
                        {
                            var exePath = Path.Combine(basePath, edition, "Common7", "IDE", "devenv.exe");
                            if (File.Exists(exePath))
                            {
                                return exePath;
                            }
                        }
                    }
                }

                // 查找Visual Studio 2019
                var vs2019Paths = new[]
                {
                    Path.Combine(programFiles, "Microsoft Visual Studio", "2019"),
                    Path.Combine(programFilesX86, "Microsoft Visual Studio", "2019")
                };

                foreach (var basePath in vs2019Paths)
                {
                    if (Directory.Exists(basePath))
                    {
                        var editions = new[] { "Enterprise", "Professional", "Community" };
                        foreach (var edition in editions)
                        {
                            var exePath = Path.Combine(basePath, edition, "Common7", "IDE", "devenv.exe");
                            if (File.Exists(exePath))
                            {
                                return exePath;
                            }
                        }
                    }
                }

                // 检查常见的自定义安装位置
                var commonCustomPaths = new[]
                {
                    @"C:\VisualStudio", @"D:\VisualStudio", @"E:\VisualStudio", @"F:\VisualStudio",
                    @"G:\VisualStudio", Path.Combine(userProfile, "VisualStudio"), @"C:\Program Files\VisualStudio",
                    @"D:\Program Files\VisualStudio", @"E:\Program Files\VisualStudio",
                    @"F:\Program Files\VisualStudio", @"G:\Program Files\VisualStudio"
                };

                foreach (var basePath in commonCustomPaths)
                {
                    if (Directory.Exists(basePath))
                    {
                        // 查找所有可能的Visual Studio目录
                        var vsDirs = Directory.GetDirectories(basePath, "*Visual Studio*");
                        foreach (var vsDir in vsDirs)
                        {
                            // 检查常见的路径模式
                            var possiblePaths = new[]
                            {
                                Path.Combine(vsDir, "Common7", "IDE", "devenv.exe"),
                                Path.Combine(vsDir, "IDE", "devenv.exe"), Path.Combine(vsDir, "devenv.exe")
                            };

                            foreach (var path in possiblePaths)
                            {
                                if (File.Exists(path))
                                {
                                    return path;
                                }
                            }

                            // 检查子目录
                            try
                            {
                                var subDirs = Directory.GetDirectories(vsDir);
                                foreach (var subDir in subDirs)
                                {
                                    var editions = new[] { "Enterprise", "Professional", "Community" };
                                    foreach (var edition in editions)
                                    {
                                        if (subDir.EndsWith(edition, StringComparison.OrdinalIgnoreCase))
                                        {
                                            var exePath = Path.Combine(subDir, "Common7", "IDE", "devenv.exe");
                                            if (File.Exists(exePath))
                                            {
                                                return exePath;
                                            }
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
                }

                // 检查所有逻辑驱动器的根目录
                foreach (var drive in Directory.GetLogicalDrives())
                {
                    try
                    {
                        // 检查驱动器根目录下的Microsoft Visual Studio目录
                        var vsPath = Path.Combine(drive, "Microsoft Visual Studio");
                        if (Directory.Exists(vsPath))
                        {
                            var versionDirs = Directory.GetDirectories(vsPath);
                            foreach (var versionDir in versionDirs)
                            {
                                var editions = new[] { "Enterprise", "Professional", "Community" };
                                foreach (var edition in editions)
                                {
                                    var editionPath = Path.Combine(versionDir, edition);
                                    if (Directory.Exists(editionPath))
                                    {
                                        var exePath = Path.Combine(editionPath, "Common7", "IDE", "devenv.exe");
                                        if (File.Exists(exePath))
                                        {
                                            return exePath;
                                        }
                                    }
                                }
                            }
                        }

                        // 递归搜索驱动器根目录下的所有目录（限制深度为2）
                        var rootDirs = Directory.GetDirectories(drive);
                        foreach (var rootDir in rootDirs)
                        {
                            if (rootDir.Contains("Visual Studio", StringComparison.OrdinalIgnoreCase))
                            {
                                var exePath = Path.Combine(rootDir, "Common7", "IDE", "devenv.exe");
                                if (File.Exists(exePath))
                                {
                                    return exePath;
                                }
                            }

                            // 检查子目录
                            try
                            {
                                var subDirs = Directory.GetDirectories(rootDir);
                                foreach (var subDir in subDirs)
                                {
                                    if (subDir.Contains("Visual Studio", StringComparison.OrdinalIgnoreCase))
                                    {
                                        var exePath = Path.Combine(subDir, "Common7", "IDE", "devenv.exe");
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
            // 在macOS上查找Visual Studio for Mac
            else if (Application.platform == RuntimePlatform.OSXEditor)
            {
                var macPaths = new[]
                {
                    "/Applications/Visual Studio.app/Contents/MacOS/VisualStudio",
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/Applications/Visual Studio.app/Contents/MacOS/VisualStudio"
                };

                foreach (var path in macPaths)
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
        /// 从Unity编辑器首选项中获取Visual Studio路径
        /// </summary>
        /// <returns>Visual Studio可执行文件路径</returns>
        private string GetVSPathFromUnityPrefs()
        {
            try
            {
                // 尝试从Unity编辑器设置中获取Visual Studio路径
                var editorPath = EditorPrefs.GetString("kScriptsDefaultApp");
                if (!string.IsNullOrEmpty(editorPath) &&
                    (editorPath.Contains("devenv.exe", StringComparison.OrdinalIgnoreCase) ||
                     editorPath.Contains("Visual Studio", StringComparison.OrdinalIgnoreCase)))
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
        /// 从Windows注册表获取Visual Studio路径
        /// </summary>
        /// <returns>Visual Studio可执行文件路径</returns>
        private string GetVSPathFromRegistry()
        {
#if UNITY_EDITOR_WIN
            try
            {
                // 尝试从注册表获取Visual Studio 2022路径
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio\SxS\VS7"))
                {
                    if (key != null)
                    {
                        // 尝试获取最新版本的Visual Studio
                        string[] versionKeys = { "17.0", "16.0", "15.0" }; // VS2022, VS2019, VS2017
                        foreach (var version in versionKeys)
                        {
                            var installDir = key.GetValue(version) as string;
                            if (!string.IsNullOrEmpty(installDir))
                            {
                                var exePath = Path.Combine(installDir, "Common7", "IDE", "devenv.exe");
                                if (File.Exists(exePath))
                                {
                                    return exePath;
                                }
                            }
                        }
                    }
                }

                // 尝试从另一个注册表位置获取
                using (var key = Registry.LocalMachine.OpenSubKey(
                           @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\devenv.exe"))
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
        /// 获取Visual Studio配置目录
        /// </summary>
        private string GetVisualStudioConfigPath()
        {
            // 获取项目根目录
            return Path.GetDirectoryName(Application.dataPath);
        }
    }
}
