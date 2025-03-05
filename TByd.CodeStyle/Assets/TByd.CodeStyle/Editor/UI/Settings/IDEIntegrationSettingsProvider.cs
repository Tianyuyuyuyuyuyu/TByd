using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using TByd.CodeStyle.Editor.CodeCheck.IDE;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Runtime.Config;

namespace TByd.CodeStyle.Editor.UI.Settings
{
    /// <summary>
    /// IDE集成设置提供者，用于在Project Settings窗口中显示IDE集成设置
    /// </summary>
    public class IDEIntegrationSettingsProvider : SettingsProvider
    {
        // 设置路径
        private const string c_SettingsPath = "Project/TByd/IDE集成";

        // 关键字
        private static readonly string[] s_Keywords = new string[]
        {
            "TByd", "IDE", "集成", "Integration", "Rider", "Visual Studio", "VS Code"
        };

        // 配置
        private CodeStyleConfig m_Config;

        // 是否已初始化
        private bool m_Initialized;

        // 是否已修改
        private bool m_IsDirty;

        // 滚动位置
        private Vector2 m_ScrollPosition;

        // 当前IDE类型
        private IDEType m_CurrentIDEType;

        // IDE配置是否已初始化
        private bool m_IsIDEConfigured;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_path">设置路径</param>
        /// <param name="_scopes">设置范围</param>
        /// <param name="_keywords">关键字</param>
        public IDEIntegrationSettingsProvider(string _path, SettingsScope _scopes, IEnumerable<string> _keywords = null)
            : base(_path, _scopes, _keywords)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            if (m_Initialized)
                return;

            m_Config = ConfigProvider.GetConfig();

            // 订阅配置变更事件
            ConfigProvider.ConfigChanged += OnConfigChanged;

            // 检测当前IDE
            CheckIDEStatus();

            m_Initialized = true;
        }

        /// <summary>
        /// 检查IDE状态
        /// </summary>
        private void CheckIDEStatus()
        {
            m_CurrentIDEType = IDEDetector.DetectCurrentIDE();
            m_IsIDEConfigured = IDEDetector.IsIDEConfigured(m_CurrentIDEType);
        }

        /// <summary>
        /// 配置变更处理
        /// </summary>
        private void OnConfigChanged()
        {
            m_Config = ConfigProvider.GetConfig();
            m_IsDirty = false;
            CheckIDEStatus();
        }

        /// <summary>
        /// 绘制设置UI
        /// </summary>
        /// <param name="_searchContext">搜索上下文</param>
        public override void OnGUI(string _searchContext)
        {
            Initialize();

            EditorGUILayout.Space();

            // 显示保存和重置按钮
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("保存设置", GUILayout.Width(100)))
            {
                SaveSettings();
            }

            if (GUILayout.Button("重置设置", GUILayout.Width(100)))
            {
                ResetSettings();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("IDE集成设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            DrawGeneralSettings();
            DrawCurrentIDESettings();
            DrawIDESpecificSettings();
            DrawConfigSyncSettings();
            DrawConfigValidationSettings();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndScrollView();

            // 如果有修改，显示保存提示
            if (m_IsDirty)
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("设置已修改，请点击 保存设置 按钮保存更改。", MessageType.Info);
            }
        }

        /// <summary>
        /// 绘制通用设置
        /// </summary>
        private void DrawGeneralSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("通用设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            var enableIDEIntegration = EditorGUILayout.Toggle("启用IDE集成", m_Config.EnableIDEIntegration);
            var autoConfigureIDE = EditorGUILayout.Toggle("自动配置IDE", m_Config.AutoConfigureIDE);
            var syncEditorConfigWithIDE = EditorGUILayout.Toggle("同步EditorConfig到IDE", m_Config.SyncEditorConfigWithIDE);

            if (EditorGUI.EndChangeCheck())
            {
                m_Config.EnableIDEIntegration = enableIDEIntegration;
                m_Config.AutoConfigureIDE = autoConfigureIDE;
                m_Config.SyncEditorConfigWithIDE = syncEditorConfigWithIDE;

                m_IsDirty = true;
            }

            // 如果未启用IDE集成，则显示提示并返回
            if (!m_Config.EnableIDEIntegration)
            {
                EditorGUILayout.HelpBox("IDE集成已禁用，IDE集成设置将不会生效。", MessageType.Info);
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制当前IDE设置
        /// </summary>
        private void DrawCurrentIDESettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("当前IDE", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("检测到的IDE:", m_CurrentIDEType.ToString());
            EditorGUILayout.LabelField("配置状态:", m_IsIDEConfigured ? "已配置" : "未配置");

            if (!m_IsIDEConfigured)
            {
                EditorGUILayout.HelpBox("当前IDE未配置，建议配置以获得更好的开发体验。", MessageType.Info);
                
                if (GUILayout.Button("配置IDE"))
                {
                    IDEDetector.ConfigureIDE(m_CurrentIDEType);
                    CheckIDEStatus();
                }
            }
            else
            {
                if (GUILayout.Button("重新配置IDE"))
                {
                    IDEDetector.ConfigureIDE(m_CurrentIDEType);
                    CheckIDEStatus();
                }

                if (GUILayout.Button("重置IDE配置"))
                {
                    IDEDetector.ResetIDEConfiguration(m_CurrentIDEType);
                    CheckIDEStatus();
                }
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制IDE特定设置
        /// </summary>
        private void DrawIDESpecificSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("IDE特定设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            switch (m_CurrentIDEType)
            {
                case IDEType.Rider:
                    DrawRiderSettings();
                    break;
                case IDEType.VisualStudio:
                    DrawVisualStudioSettings();
                    break;
                case IDEType.VSCode:
                    DrawVSCodeSettings();
                    break;
                default:
                    EditorGUILayout.HelpBox("未检测到支持的IDE。", MessageType.Warning);
                    break;
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制Rider特定设置
        /// </summary>
        private void DrawRiderSettings()
        {
            var riderConfig = m_Config.RiderConfig;

            EditorGUI.BeginChangeCheck();

            riderConfig.EnableCodeAnalysis = EditorGUILayout.Toggle("启用代码分析", riderConfig.EnableCodeAnalysis);
            riderConfig.EnableStyleCop = EditorGUILayout.Toggle("启用StyleCop", riderConfig.EnableStyleCop);
            riderConfig.EnableReSharper = EditorGUILayout.Toggle("启用ReSharper", riderConfig.EnableReSharper);

            if (EditorGUI.EndChangeCheck())
            {
                m_IsDirty = true;
            }
        }

        /// <summary>
        /// 绘制Visual Studio特定设置
        /// </summary>
        private void DrawVisualStudioSettings()
        {
            var vsConfig = m_Config.VisualStudioConfig;

            EditorGUI.BeginChangeCheck();

            vsConfig.EnableRoslynAnalyzers = EditorGUILayout.Toggle("启用Roslyn分析器", vsConfig.EnableRoslynAnalyzers);
            vsConfig.EnableStyleCop = EditorGUILayout.Toggle("启用StyleCop", vsConfig.EnableStyleCop);
            vsConfig.EnableCodeAnalysis = EditorGUILayout.Toggle("启用代码分析", vsConfig.EnableCodeAnalysis);

            if (EditorGUI.EndChangeCheck())
            {
                m_IsDirty = true;
            }
        }

        /// <summary>
        /// 绘制VS Code特定设置
        /// </summary>
        private void DrawVSCodeSettings()
        {
            var vscodeConfig = m_Config.VSCodeConfig;

            EditorGUI.BeginChangeCheck();

            vscodeConfig.EnableOmniSharp = EditorGUILayout.Toggle("启用OmniSharp", vscodeConfig.EnableOmniSharp);
            vscodeConfig.EnableRoslynAnalyzers = EditorGUILayout.Toggle("启用Roslyn分析器", vscodeConfig.EnableRoslynAnalyzers);
            vscodeConfig.EnableEditorConfig = EditorGUILayout.Toggle("启用EditorConfig", vscodeConfig.EnableEditorConfig);

            if (EditorGUI.EndChangeCheck())
            {
                m_IsDirty = true;
            }
        }

        /// <summary>
        /// 绘制配置同步设置
        /// </summary>
        private void DrawConfigSyncSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("配置同步", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (GUILayout.Button("导出配置到当前IDE"))
            {
                var success = IDEIntegrationManager.ExportConfigToCurrentIDE(EditorConfigManager.GetRules());
                if (success)
                {
                    EditorUtility.DisplayDialog("导出成功", "配置已成功导出到当前IDE。", "确定");
                }
                else
                {
                    EditorUtility.DisplayDialog("导出失败", "配置导出失败，请检查IDE是否正确安装。", "确定");
                }
            }

            if (GUILayout.Button("导出配置到所有IDE"))
            {
                var success = IDEIntegrationManager.ExportConfigToAllIDEs(EditorConfigManager.GetRules());
                if (success)
                {
                    EditorUtility.DisplayDialog("导出成功", "配置已成功导出到所有IDE。", "确定");
                }
                else
                {
                    EditorUtility.DisplayDialog("导出失败", "配置导出失败，请检查IDE是否正确安装。", "确定");
                }
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制配置验证设置
        /// </summary>
        private void DrawConfigValidationSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("配置验证", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (GUILayout.Button("验证当前IDE配置"))
            {
                // 获取项目根目录
                var projectPath = Path.GetDirectoryName(Application.dataPath);
                
                // 验证配置
                var result = IDEConfigValidator.ValidateConfig(m_CurrentIDEType, projectPath);
                
                // 显示验证结果
                var message = "验证结果:\n\n";
                
                if (result.IsValid)
                {
                    message += "配置有效，未发现问题。\n";
                }
                else
                {
                    message += "配置存在问题:\n\n";
                }
                
                if (result.Errors.Count > 0)
                {
                    message += "错误:\n";
                    foreach (var error in result.Errors)
                    {
                        message += $"- {error}\n";
                    }
                    message += "\n";
                }
                
                if (result.Warnings.Count > 0)
                {
                    message += "警告:\n";
                    foreach (var warning in result.Warnings)
                    {
                        message += $"- {warning}\n";
                    }
                    message += "\n";
                }
                
                if (result.Suggestions.Count > 0)
                {
                    message += "建议:\n";
                    foreach (var suggestion in result.Suggestions)
                    {
                        message += $"- {suggestion}\n";
                    }
                }
                
                EditorUtility.DisplayDialog("IDE配置验证", message, "确定");
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        private void SaveSettings()
        {
            ConfigProvider.SaveConfig();
            m_IsDirty = false;
            
            // 如果启用了同步EditorConfig到IDE，则导出配置
            if (m_Config.EnableIDEIntegration && m_Config.SyncEditorConfigWithIDE)
            {
                IDEIntegrationManager.ExportConfigToCurrentIDE(EditorConfigManager.GetRules());
            }
        }

        /// <summary>
        /// 重置设置
        /// </summary>
        private void ResetSettings()
        {
            if (EditorUtility.DisplayDialog("重置设置", "确定要重置IDE集成设置吗？这将恢复所有设置为默认值。", "确定", "取消"))
            {
                // 重置IDE集成设置
                m_Config.EnableIDEIntegration = true;
                m_Config.AutoConfigureIDE = true;
                m_Config.SyncEditorConfigWithIDE = true;
                
                // 重置IDE特定设置
                m_Config.RiderConfig = new RiderConfig();
                m_Config.VisualStudioConfig = new VisualStudioConfig();
                m_Config.VSCodeConfig = new VSCodeConfig();
                
                // 保存配置
                ConfigProvider.SaveConfig();
                
                // 更新状态
                m_IsDirty = false;
                CheckIDEStatus();
            }
        }

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var provider = new IDEIntegrationSettingsProvider(c_SettingsPath, SettingsScope.Project, s_Keywords);
            return provider;
        }
    }
} 