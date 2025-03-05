using System;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using TByd.CodeStyle.Runtime.Config;

namespace TByd.CodeStyle.Editor.CodeCheck.IDE
{
    /// <summary>
    /// IDE检测器，用于检测当前使用的IDE并提供自动配置功能
    /// </summary>
    public static class IdeDetector
    {
        // 上次检测时间
        private static DateTime s_LastDetectionTime = DateTime.MinValue;

        // 检测间隔（秒）
        private const float k_CDetectionInterval = 300f; // 5分钟

        // 是否已初始化
        private static bool s_Initialized;

        // 当前IDE类型
        private static IdeType s_CurrentIdeType = IdeType.k_Unknown;

        // IDE配置状态
        private static bool s_IsIdeConfigured;

        /// <summary>
        /// 静态构造函数，在编辑器加载时初始化
        /// </summary>
        static IdeDetector()
        {
            // 延迟初始化，确保Unity完全加载
            EditorApplication.delayCall += Initialize;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private static void Initialize()
        {
            if (s_Initialized)
            {
                return;
            }

            // 订阅编辑器更新事件
            EditorApplication.update += OnEditorUpdate;

            s_Initialized = true;

            Debug.Log("[TByd.CodeStyle] IDE检测器初始化成功");
        }

        /// <summary>
        /// 编辑器更新事件处理
        /// </summary>
        private static void OnEditorUpdate()
        {
            // 检查是否需要进行检测
            if ((DateTime.Now - s_LastDetectionTime).TotalSeconds >= k_CDetectionInterval)
            {
                // 更新检测时间
                s_LastDetectionTime = DateTime.Now;

                // 检测IDE
                DetectIde();
            }
        }

        /// <summary>
        /// 检测当前IDE
        /// </summary>
        /// <returns>当前IDE类型</returns>
        public static IdeType DetectCurrentIde()
        {
            // 获取当前使用的IDE
            var currentIde = IdeIntegrationManager.GetCurrentIntegration();

            if (currentIde != null)
            {
                if (currentIde is RiderIntegration)
                {
                    s_CurrentIdeType = IdeType.k_Rider;
                }
                else if (currentIde is VisualStudioIntegration)
                {
                    s_CurrentIdeType = IdeType.k_VisualStudio;
                }
                else if (currentIde is VSCodeIntegration)
                {
                    s_CurrentIdeType = IdeType.k_VSCode;
                }
                else
                {
                    s_CurrentIdeType = IdeType.k_Unknown;
                }
            }
            else
            {
                s_CurrentIdeType = IdeType.k_Unknown;
            }

            return s_CurrentIdeType;
        }

        /// <summary>
        /// 检查IDE是否已配置
        /// </summary>
        /// <param name="ideType">IDE类型</param>
        /// <returns>是否已配置</returns>
        public static bool IsIdeConfigured(IdeType ideType)
        {
            // 获取配置
            var config = ConfigManager.GetConfig();

            // 检查IDE是否已配置
            switch (ideType)
            {
                case IdeType.k_Rider:
                    s_IsIdeConfigured = config.RiderConfig.EnableCodeAnalysis ||
                                      config.RiderConfig.EnableStyleCop ||
                                      config.RiderConfig.EnableReSharper;
                    break;
                case IdeType.k_VisualStudio:
                    s_IsIdeConfigured = config.VisualStudioConfig.EnableRoslynAnalyzers ||
                                      config.VisualStudioConfig.EnableStyleCop ||
                                      config.VisualStudioConfig.EnableCodeAnalysis;
                    break;
                case IdeType.k_VSCode:
                    s_IsIdeConfigured = config.VSCodeConfig.EnableOmniSharp ||
                                      config.VSCodeConfig.EnableRoslynAnalyzers ||
                                      config.VSCodeConfig.EnableEditorConfig;
                    break;
                default:
                    s_IsIdeConfigured = false;
                    break;
            }

            return s_IsIdeConfigured;
        }

        /// <summary>
        /// 提示IDE配置
        /// </summary>
        /// <param name="ideType">IDE类型</param>
        public static void PromptIdeConfiguration(IdeType ideType)
        {
            // 获取配置
            var config = ConfigManager.GetConfig();

            // 如果未启用IDE集成或不自动配置，则不提示
            if (!config.EnableIdeIntegration || !config.AutoConfigureIde)
            {
                return;
            }

            // 如果用户选择了不再询问，则不提示
            if (EditorPrefs.GetBool("TByd.CodeStyle.IDE.DontAskAgain", false))
            {
                return;
            }

            // 如果已经配置过，则不提示
            if (IsIdeConfigured(ideType))
            {
                return;
            }

            // 提示用户是否要配置IDE
            if (EditorUtility.DisplayDialog(
                "TByd.CodeStyle - IDE检测",
                $"检测到您正在使用 {ideType} 作为脚本编辑器。\n\n是否要配置 {ideType} 以支持代码风格检查？",
                "是，现在配置",
                "否，稍后再说"))
            {
                ConfigureIde(ideType);
            }
            else
            {
                // 用户选择稍后再说，记录选择
                EditorPrefs.SetBool("TByd.CodeStyle.IDE.DontAskAgain", true);
            }
        }

        /// <summary>
        /// 配置IDE
        /// </summary>
        /// <param name="ideType">IDE类型</param>
        public static void ConfigureIde(IdeType ideType)
        {
            // 获取配置
            var config = ConfigManager.GetConfig();

            // 根据IDE类型配置
            switch (ideType)
            {
                case IdeType.k_Rider:
                    config.RiderConfig.EnableCodeAnalysis = true;
                    config.RiderConfig.EnableStyleCop = true;
                    config.RiderConfig.EnableReSharper = true;
                    break;
                case IdeType.k_VisualStudio:
                    config.VisualStudioConfig.EnableRoslynAnalyzers = true;
                    config.VisualStudioConfig.EnableStyleCop = true;
                    config.VisualStudioConfig.EnableCodeAnalysis = true;
                    break;
                case IdeType.k_VSCode:
                    config.VSCodeConfig.EnableOmniSharp = true;
                    config.VSCodeConfig.EnableRoslynAnalyzers = true;
                    config.VSCodeConfig.EnableEditorConfig = true;
                    break;
            }

            // 保存配置
            ConfigManager.SaveConfig();

            // 导出EditorConfig
            if (config.SyncEditorConfigWithIde)
            {
                IdeIntegrationManager.ExportConfigToCurrentIde(EditorConfigManager.GetRules());
            }

            // 更新状态
            s_IsIdeConfigured = true;

            // 提示用户配置成功
            EditorUtility.DisplayDialog(
                "TByd.CodeStyle - IDE配置",
                $"{ideType} 已成功配置。\n\n您现在可以使用代码风格检查功能了。",
                "确定");
        }

        /// <summary>
        /// 重置IDE配置
        /// </summary>
        /// <param name="ideType">IDE类型</param>
        public static void ResetIdeConfiguration(IdeType ideType)
        {
            // 获取配置
            var config = ConfigManager.GetConfig();

            // 根据IDE类型重置配置
            switch (ideType)
            {
                case IdeType.k_Rider:
                    config.RiderConfig = new RiderConfig();
                    break;
                case IdeType.k_VisualStudio:
                    config.VisualStudioConfig = new VisualStudioConfig();
                    break;
                case IdeType.k_VSCode:
                    config.VSCodeConfig = new VSCodeConfig();
                    break;
            }

            // 保存配置
            ConfigManager.SaveConfig();

            // 更新状态
            s_IsIdeConfigured = false;

            // 重置自动配置状态
            ResetAutoConfigState();
        }

        /// <summary>
        /// 重置自动配置状态
        /// </summary>
        public static void ResetAutoConfigState()
        {
            EditorPrefs.DeleteKey("TByd.CodeStyle.IDE.AutoConfigured");
            EditorPrefs.DeleteKey("TByd.CodeStyle.IDE.DontAskAgain");

            Debug.Log("[TByd.CodeStyle] IDE自动配置状态已重置");
        }

        /// <summary>
        /// 立即检测IDE
        /// </summary>
        public static void DetectIdeNow()
        {
            s_LastDetectionTime = DateTime.MinValue;
            DetectIde();
        }

        /// <summary>
        /// 检测IDE
        /// </summary>
        private static void DetectIde()
        {
            // 获取当前IDE类型
            var currentType = DetectCurrentIde();

            if (currentType != IdeType.k_Unknown)
            {
                Debug.Log($"[TByd.CodeStyle] 检测到当前使用的IDE: {currentType}");

                // 检查是否需要配置
                if (!IsIdeConfigured(currentType))
                {
                    PromptIdeConfiguration(currentType);
                }
            }
        }
    }
}
