using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using TByd.CodeStyle.Runtime.Config;

namespace TByd.CodeStyle.Editor.CodeCheck.IDE
{
    /// <summary>
    /// IDE检测器，用于检测当前使用的IDE并提供自动配置功能
    /// </summary>
    public static class IDEDetector
    {
        // 上次检测时间
        private static DateTime s_LastDetectionTime = DateTime.MinValue;

        // 检测间隔（秒）
        private const float c_DetectionInterval = 300f; // 5分钟

        // 是否已初始化
        private static bool s_Initialized;

        // 当前IDE类型
        private static IDEType s_CurrentIDEType = IDEType.Unknown;

        // IDE配置状态
        private static bool s_IsIDEConfigured;

        /// <summary>
        /// 静态构造函数，在编辑器加载时初始化
        /// </summary>
        static IDEDetector()
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
                return;

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
            if ((DateTime.Now - s_LastDetectionTime).TotalSeconds >= c_DetectionInterval)
            {
                // 更新检测时间
                s_LastDetectionTime = DateTime.Now;

                // 检测IDE
                DetectIDE();
            }
        }

        /// <summary>
        /// 检测当前IDE
        /// </summary>
        /// <returns>当前IDE类型</returns>
        public static IDEType DetectCurrentIDE()
        {
            // 获取当前使用的IDE
            IDEIntegration currentIDE = IDEIntegrationManager.GetCurrentIntegration();

            if (currentIDE != null)
            {
                if (currentIDE is RiderIntegration)
                {
                    s_CurrentIDEType = IDEType.Rider;
                }
                else if (currentIDE is VisualStudioIntegration)
                {
                    s_CurrentIDEType = IDEType.VisualStudio;
                }
                else if (currentIDE is VSCodeIntegration)
                {
                    s_CurrentIDEType = IDEType.VSCode;
                }
                else
                {
                    s_CurrentIDEType = IDEType.Unknown;
                }
            }
            else
            {
                s_CurrentIDEType = IDEType.Unknown;
            }

            return s_CurrentIDEType;
        }

        /// <summary>
        /// 检查IDE是否已配置
        /// </summary>
        /// <param name="_ideType">IDE类型</param>
        /// <returns>是否已配置</returns>
        public static bool IsIDEConfigured(IDEType _ideType)
        {
            // 获取配置
            var config = ConfigManager.GetConfig();

            // 检查IDE是否已配置
            switch (_ideType)
            {
                case IDEType.Rider:
                    s_IsIDEConfigured = config.RiderConfig.EnableCodeAnalysis ||
                                      config.RiderConfig.EnableStyleCop ||
                                      config.RiderConfig.EnableReSharper;
                    break;
                case IDEType.VisualStudio:
                    s_IsIDEConfigured = config.VisualStudioConfig.EnableRoslynAnalyzers ||
                                      config.VisualStudioConfig.EnableStyleCop ||
                                      config.VisualStudioConfig.EnableCodeAnalysis;
                    break;
                case IDEType.VSCode:
                    s_IsIDEConfigured = config.VSCodeConfig.EnableOmniSharp ||
                                      config.VSCodeConfig.EnableRoslynAnalyzers ||
                                      config.VSCodeConfig.EnableEditorConfig;
                    break;
                default:
                    s_IsIDEConfigured = false;
                    break;
            }

            return s_IsIDEConfigured;
        }

        /// <summary>
        /// 提示IDE配置
        /// </summary>
        /// <param name="_ideType">IDE类型</param>
        public static void PromptIDEConfiguration(IDEType _ideType)
        {
            // 获取配置
            var config = ConfigManager.GetConfig();

            // 如果未启用IDE集成或不自动配置，则不提示
            if (!config.EnableIDEIntegration || !config.AutoConfigureIDE)
            {
                return;
            }

            // 如果用户选择了不再询问，则不提示
            if (EditorPrefs.GetBool("TByd.CodeStyle.IDE.DontAskAgain", false))
            {
                return;
            }

            // 如果已经配置过，则不提示
            if (IsIDEConfigured(_ideType))
            {
                return;
            }

            // 提示用户是否要配置IDE
            if (EditorUtility.DisplayDialog(
                "TByd.CodeStyle - IDE检测",
                $"检测到您正在使用 {_ideType} 作为脚本编辑器。\n\n是否要配置 {_ideType} 以支持代码风格检查？",
                "是，现在配置",
                "否，稍后再说"))
            {
                ConfigureIDE(_ideType);
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
        /// <param name="_ideType">IDE类型</param>
        public static void ConfigureIDE(IDEType _ideType)
        {
            // 获取配置
            var config = ConfigManager.GetConfig();

            // 根据IDE类型配置
            switch (_ideType)
            {
                case IDEType.Rider:
                    config.RiderConfig.EnableCodeAnalysis = true;
                    config.RiderConfig.EnableStyleCop = true;
                    config.RiderConfig.EnableReSharper = true;
                    break;
                case IDEType.VisualStudio:
                    config.VisualStudioConfig.EnableRoslynAnalyzers = true;
                    config.VisualStudioConfig.EnableStyleCop = true;
                    config.VisualStudioConfig.EnableCodeAnalysis = true;
                    break;
                case IDEType.VSCode:
                    config.VSCodeConfig.EnableOmniSharp = true;
                    config.VSCodeConfig.EnableRoslynAnalyzers = true;
                    config.VSCodeConfig.EnableEditorConfig = true;
                    break;
            }

            // 保存配置
            ConfigManager.SaveConfig();

            // 导出EditorConfig
            if (config.SyncEditorConfigWithIDE)
            {
                IDEIntegrationManager.ExportConfigToCurrentIDE(EditorConfigManager.GetRules());
            }

            // 更新状态
            s_IsIDEConfigured = true;

            // 提示用户配置成功
            EditorUtility.DisplayDialog(
                "TByd.CodeStyle - IDE配置",
                $"{_ideType} 已成功配置。\n\n您现在可以使用代码风格检查功能了。",
                "确定");
        }

        /// <summary>
        /// 重置IDE配置
        /// </summary>
        /// <param name="_ideType">IDE类型</param>
        public static void ResetIDEConfiguration(IDEType _ideType)
        {
            // 获取配置
            var config = ConfigManager.GetConfig();

            // 根据IDE类型重置配置
            switch (_ideType)
            {
                case IDEType.Rider:
                    config.RiderConfig = new RiderConfig();
                    break;
                case IDEType.VisualStudio:
                    config.VisualStudioConfig = new VisualStudioConfig();
                    break;
                case IDEType.VSCode:
                    config.VSCodeConfig = new VSCodeConfig();
                    break;
            }

            // 保存配置
            ConfigManager.SaveConfig();

            // 更新状态
            s_IsIDEConfigured = false;

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
        public static void DetectIDENow()
        {
            s_LastDetectionTime = DateTime.MinValue;
            DetectIDE();
        }

        /// <summary>
        /// 检测IDE
        /// </summary>
        private static void DetectIDE()
        {
            // 获取当前IDE类型
            IDEType currentType = DetectCurrentIDE();

            if (currentType != IDEType.Unknown)
            {
                Debug.Log($"[TByd.CodeStyle] 检测到当前使用的IDE: {currentType}");

                // 检查是否需要配置
                if (!IsIDEConfigured(currentType))
                {
                    PromptIDEConfiguration(currentType);
                }
            }
        }
    }
}
