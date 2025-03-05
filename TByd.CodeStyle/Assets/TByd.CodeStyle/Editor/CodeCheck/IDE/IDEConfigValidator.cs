using System;
using System.Collections.Generic;
using System.IO;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;

namespace TByd.CodeStyle.Editor.CodeCheck.IDE
{
    /// <summary>
    /// IDE配置验证器
    /// </summary>
    public static class IDEConfigValidator
    {
        /// <summary>
        /// 验证结果
        /// </summary>
        public class ValidationResult
        {
            /// <summary>
            /// 是否有效
            /// </summary>
            public bool IsValid { get; set; }

            /// <summary>
            /// 错误信息列表
            /// </summary>
            public List<string> Errors { get; } = new List<string>();

            /// <summary>
            /// 警告信息列表
            /// </summary>
            public List<string> Warnings { get; } = new List<string>();

            /// <summary>
            /// 建议信息列表
            /// </summary>
            public List<string> Suggestions { get; } = new List<string>();
        }

        /// <summary>
        /// 验证IDE配置
        /// </summary>
        /// <param name="_ideType">IDE类型</param>
        /// <param name="_configPath">配置路径</param>
        /// <returns>验证结果</returns>
        public static ValidationResult ValidateConfig(IDEType _ideType, string _configPath)
        {
            var result = new ValidationResult { IsValid = true };

            try
            {
                // 验证配置目录
                if (!Directory.Exists(_configPath))
                {
                    result.Errors.Add($"配置目录不存在: {_configPath}");
                    result.IsValid = false;
                    return result;
                }

                // 根据IDE类型验证特定配置
                switch (_ideType)
                {
                    case IDEType.Rider:
                        ValidateRiderConfig(_configPath, result);
                        break;
                    case IDEType.VisualStudio:
                        ValidateVisualStudioConfig(_configPath, result);
                        break;
                    case IDEType.VSCode:
                        ValidateVSCodeConfig(_configPath, result);
                        break;
                    default:
                        result.Errors.Add($"不支持的IDE类型: {_ideType}");
                        result.IsValid = false;
                        break;
                }

                // 验证EditorConfig
                ValidateEditorConfig(_configPath, result);

                return result;
            }
            catch (Exception e)
            {
                result.Errors.Add($"验证配置时发生错误: {e.Message}");
                result.IsValid = false;
                return result;
            }
        }

        /// <summary>
        /// 验证Rider配置
        /// </summary>
        private static void ValidateRiderConfig(string _configPath, ValidationResult _result)
        {
            // 验证.idea目录
            var ideaPath = Path.Combine(_configPath, ".idea");
            if (!Directory.Exists(ideaPath))
            {
                _result.Warnings.Add("未找到.idea目录，可能影响Rider配置");
            }

            // 验证codeStyleConfig.xml
            var codeStylePath = Path.Combine(ideaPath, "codeStyleConfig.xml");
            if (!File.Exists(codeStylePath))
            {
                _result.Errors.Add("未找到codeStyleConfig.xml文件");
                _result.IsValid = false;
            }
            else
            {
                // 验证文件内容
                try
                {
                    var content = File.ReadAllText(codeStylePath);
                    if (!content.Contains("TByd.CodeStyle"))
                    {
                        _result.Warnings.Add("codeStyleConfig.xml可能不是TByd.CodeStyle的配置文件");
                    }
                }
                catch (Exception e)
                {
                    _result.Errors.Add($"读取codeStyleConfig.xml失败: {e.Message}");
                    _result.IsValid = false;
                }
            }

            // 验证csharpier.json
            var csharpierPath = Path.Combine(ideaPath, "csharpier.json");
            if (!File.Exists(csharpierPath))
            {
                _result.Warnings.Add("未找到csharpier.json文件，代码格式化可能不完整");
            }
            else
            {
                // 验证文件内容
                try
                {
                    var content = File.ReadAllText(csharpierPath);
                    if (!content.Contains("fileHeader") || !content.Contains("formatting"))
                    {
                        _result.Warnings.Add("csharpier.json可能缺少必要的配置项");
                    }
                }
                catch (Exception e)
                {
                    _result.Errors.Add($"读取csharpier.json失败: {e.Message}");
                    _result.IsValid = false;
                }
            }

            // 添加建议
            _result.Suggestions.Add("建议在Rider中启用EditorConfig支持");
            _result.Suggestions.Add("建议在Rider中启用代码分析功能");
        }

        /// <summary>
        /// 验证Visual Studio配置
        /// </summary>
        private static void ValidateVisualStudioConfig(string _configPath, ValidationResult _result)
        {
            // 验证.vssettings
            var vsSettingsPath = Path.Combine(_configPath, ".vssettings");
            if (!File.Exists(vsSettingsPath))
            {
                _result.Errors.Add("未找到.vssettings文件");
                _result.IsValid = false;
            }
            else
            {
                // 验证文件内容
                try
                {
                    var content = File.ReadAllText(vsSettingsPath);
                    if (!content.Contains("TextEditor") || !content.Contains("CSharp"))
                    {
                        _result.Warnings.Add(".vssettings可能缺少必要的配置项");
                    }
                }
                catch (Exception e)
                {
                    _result.Errors.Add($"读取.vssettings失败: {e.Message}");
                    _result.IsValid = false;
                }
            }

            // 添加建议
            _result.Suggestions.Add("建议在Visual Studio中启用Roslyn分析器");
            _result.Suggestions.Add("建议在Visual Studio中启用StyleCop支持");
        }

        /// <summary>
        /// 验证VS Code配置
        /// </summary>
        private static void ValidateVSCodeConfig(string _configPath, ValidationResult _result)
        {
            // 验证.vscode目录
            var vscodePath = Path.Combine(_configPath, ".vscode");
            if (!Directory.Exists(vscodePath))
            {
                _result.Warnings.Add("未找到.vscode目录，可能影响VS Code配置");
            }

            // 验证settings.json
            var settingsPath = Path.Combine(vscodePath, "settings.json");
            if (!File.Exists(settingsPath))
            {
                _result.Errors.Add("未找到settings.json文件");
                _result.IsValid = false;
            }
            else
            {
                // 验证文件内容
                try
                {
                    var content = File.ReadAllText(settingsPath);
                    if (!content.Contains("omnisharp.enableEditorConfigSupport"))
                    {
                        _result.Warnings.Add("settings.json可能缺少OmniSharp配置");
                    }
                }
                catch (Exception e)
                {
                    _result.Errors.Add($"读取settings.json失败: {e.Message}");
                    _result.IsValid = false;
                }
            }

            // 验证omnisharp.json
            var omnisharpPath = Path.Combine(vscodePath, "omnisharp.json");
            if (!File.Exists(omnisharpPath))
            {
                _result.Warnings.Add("未找到omnisharp.json文件，C#语言服务可能不完整");
            }
            else
            {
                // 验证文件内容
                try
                {
                    var content = File.ReadAllText(omnisharpPath);
                    if (!content.Contains("FormattingOptions") || !content.Contains("RoslynExtensionsOptions"))
                    {
                        _result.Warnings.Add("omnisharp.json可能缺少必要的配置项");
                    }
                }
                catch (Exception e)
                {
                    _result.Errors.Add($"读取omnisharp.json失败: {e.Message}");
                    _result.IsValid = false;
                }
            }

            // 添加建议
            _result.Suggestions.Add("建议安装C#扩展以获得更好的开发体验");
            _result.Suggestions.Add("建议在VS Code中启用OmniSharp支持");
        }

        /// <summary>
        /// 验证EditorConfig配置
        /// </summary>
        private static void ValidateEditorConfig(string _configPath, ValidationResult _result)
        {
            // 验证.editorconfig
            var editorConfigPath = Path.Combine(_configPath, ".editorconfig");
            if (!File.Exists(editorConfigPath))
            {
                _result.Errors.Add("未找到.editorconfig文件");
                _result.IsValid = false;
            }
            else
            {
                // 验证文件内容
                try
                {
                    var rules = EditorConfigManager.GetRules();
                    if (rules == null || rules.Count == 0)
                    {
                        _result.Warnings.Add(".editorconfig文件可能没有有效的规则");
                    }
                    else
                    {
                        // 验证是否包含基本规则
                        var hasBasicRules = false;
                        foreach (var rule in rules)
                        {
                            if (rule.Pattern == "*")
                            {
                                hasBasicRules = true;
                                break;
                            }
                        }

                        if (!hasBasicRules)
                        {
                            _result.Warnings.Add(".editorconfig文件缺少基本规则配置");
                        }

                        // 验证是否包含C#规则
                        var hasCSharpRules = false;
                        foreach (var rule in rules)
                        {
                            if (rule.Pattern == "*.cs")
                            {
                                hasCSharpRules = true;
                                break;
                            }
                        }

                        if (!hasCSharpRules)
                        {
                            _result.Warnings.Add(".editorconfig文件缺少C#特定规则配置");
                        }
                    }
                }
                catch (Exception e)
                {
                    _result.Errors.Add($"验证.editorconfig失败: {e.Message}");
                    _result.IsValid = false;
                }
            }
        }

        /// <summary>
        /// 检查配置冲突
        /// </summary>
        /// <param name="_ideType">IDE类型</param>
        /// <param name="_configPath">配置路径</param>
        /// <returns>冲突列表</returns>
        public static List<string> CheckConfigConflicts(IDEType _ideType, string _configPath)
        {
            var conflicts = new List<string>();

            try
            {
                // 检查EditorConfig冲突
                var editorConfigPath = Path.Combine(_configPath, ".editorconfig");
                if (File.Exists(editorConfigPath))
                {
                    var rules = EditorConfigManager.GetRules();
                    foreach (var rule in rules)
                    {
                        // 检查缩进设置冲突
                        if (rule.Properties.ContainsKey("indent_style") && rule.Properties.ContainsKey("indent_size"))
                        {
                            var indentStyle = rule.Properties["indent_style"];

                            switch (_ideType)
                            {
                                case IDEType.Rider:
                                    // 检查Rider设置
                                    var riderConfigPath = Path.Combine(_configPath, ".idea", "codeStyleConfig.xml");
                                    if (File.Exists(riderConfigPath))
                                    {
                                        var content = File.ReadAllText(riderConfigPath);
                                        if ((indentStyle == "space" && content.Contains("USE_TABS")) ||
                                            (indentStyle == "tab" && !content.Contains("USE_TABS")))
                                        {
                                            conflicts.Add($"Rider的缩进设置与EditorConfig冲突: {rule.Pattern}");
                                        }
                                    }
                                    break;

                                case IDEType.VisualStudio:
                                    // 检查VS设置
                                    var vsSettingsPath = Path.Combine(_configPath, ".vssettings");
                                    if (File.Exists(vsSettingsPath))
                                    {
                                        var content = File.ReadAllText(vsSettingsPath);
                                        if ((indentStyle == "space" && content.Contains("InsertTabs>true")) ||
                                            (indentStyle == "tab" && content.Contains("InsertTabs>false")))
                                        {
                                            conflicts.Add($"Visual Studio的缩进设置与EditorConfig冲突: {rule.Pattern}");
                                        }
                                    }
                                    break;

                                case IDEType.VSCode:
                                    // 检查VS Code设置
                                    var vscodePath = Path.Combine(_configPath, ".vscode", "settings.json");
                                    if (File.Exists(vscodePath))
                                    {
                                        var content = File.ReadAllText(vscodePath);
                                        if ((indentStyle == "space" && content.Contains("\"useTabs\": true")) ||
                                            (indentStyle == "tab" && content.Contains("\"useTabs\": false")))
                                        {
                                            conflicts.Add($"VS Code的缩进设置与EditorConfig冲突: {rule.Pattern}");
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }

                return conflicts;
            }
            catch (Exception e)
            {
                conflicts.Add($"检查配置冲突时发生错误: {e.Message}");
                return conflicts;
            }
        }
    }
} 