using System.Collections.Generic;
using System.Linq;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using TByd.CodeStyle.Runtime.Config;
using UnityEditor;
using UnityEngine;

namespace TByd.CodeStyle.Editor.CodeCheck.IDE
{
    /// <summary>
    /// IDE集成管理器，用于管理和执行IDE集成
    /// </summary>
    public static class IdeIntegrationManager
    {
        // IDE集成列表
        private static readonly List<IDeIntegration> s_Integrations = new();

        // 是否已初始化
        private static bool s_Initialized;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static IdeIntegrationManager()
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

            // 注册IDE集成
            RegisterIntegrations();

            s_Initialized = true;

            Debug.Log("[TByd.CodeStyle] IDE集成管理器初始化成功");
        }

        /// <summary>
        /// 注册IDE集成
        /// </summary>
        private static void RegisterIntegrations()
        {
            // 注册Rider集成
            s_Integrations.Add(new RiderIntegration());

            // 注册Visual Studio集成
            s_Integrations.Add(new VisualStudioIntegration());

            // 注册VS Code集成
            s_Integrations.Add(new VSCodeIntegration());
        }

        /// <summary>
        /// 注册IDE集成 (用于测试和扩展)
        /// </summary>
        /// <param name="integration">要注册的IDE集成</param>
        public static void RegisterIntegration(IDeIntegration integration)
        {
            // 确保集成不为空
            if (integration == null)
            {
                return;
            }

            // 确保已初始化
            if (!s_Initialized)
            {
                Initialize();
            }

            // 确保不重复添加
            if (!s_Integrations.Any(i => i.Name == integration.Name))
            {
                s_Integrations.Add(integration);
            }
        }

        /// <summary>
        /// 获取当前使用的IDE集成
        /// </summary>
        /// <returns>当前IDE集成</returns>
        public static IDeIntegration GetCurrentIntegration()
        {
            // 确保已初始化
            if (!s_Initialized)
            {
                Initialize();
            }

            // 获取已安装的IDE
            var installedIDEs = GetInstalledIntegrations();

            // 如果没有安装任何IDE，返回null
            if (installedIDEs.Count == 0)
            {
                return null;
            }

            // 获取Unity的外部脚本编辑器设置
            var externalEditorPath = EditorPrefs.GetString("kScriptsDefaultApp");

            // 根据外部编辑器路径匹配IDE
            return installedIDEs.FirstOrDefault(ide =>
                externalEditorPath.Contains(ide.Name, System.StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 获取已安装的IDE集成列表
        /// </summary>
        /// <returns>已安装的IDE集成列表</returns>
        public static List<IDeIntegration> GetInstalledIntegrations()
        {
            // 确保已初始化
            if (!s_Initialized)
            {
                Initialize();
            }

            return s_Integrations.FindAll(i => i.IsInstalled);
        }

        /// <summary>
        /// 获取特定类型的IDE集成
        /// </summary>
        /// <param name="ideType">IDE类型</param>
        /// <returns>IDE集成</returns>
        public static IDeIntegration GetIntegration(IdeType ideType)
        {
            // 确保已初始化
            if (!s_Initialized)
            {
                Initialize();
            }

            // 基于IDE类型查找对应的集成
            return s_Integrations.Find(i => i.GetType().Name.Contains(ideType.ToString()));
        }

        /// <summary>
        /// 检查特定IDE是否已安装
        /// </summary>
        /// <param name="ideType">IDE类型</param>
        /// <returns>是否已安装</returns>
        public static bool IsIdeInstalled(IdeType ideType)
        {
            var integration = GetIntegration(ideType);
            return integration != null && integration.IsInstalled;
        }

        /// <summary>
        /// 导出配置到当前IDE
        /// </summary>
        /// <param name="rules">EditorConfig规则列表</param>
        /// <returns>是否成功</returns>
        public static bool ExportConfigToCurrentIde(List<EditorConfigRule> rules)
        {
            var currentIde = GetCurrentIntegration();
            if (currentIde != null)
            {
                return currentIde.ExportConfig(rules);
            }
            return false;
        }

        /// <summary>
        /// 导出配置到特定IDE
        /// </summary>
        /// <param name="ideType">IDE类型</param>
        /// <param name="config">代码风格配置</param>
        /// <returns>是否成功</returns>
        public static bool ExportConfigToIde(IdeType ideType, CodeStyleConfig config)
        {
            // 获取EditorConfig规则列表
            var rules = EditorConfigManager.GetRules();

            var integration = GetIntegration(ideType);
            if (integration != null && integration.IsInstalled)
            {
                return integration.ExportConfig(rules);
            }
            return false;
        }

        /// <summary>
        /// 导出配置到所有已安装的IDE
        /// </summary>
        /// <param name="config">代码风格配置</param>
        /// <returns>是否成功</returns>
        public static bool ExportConfigToAllIDEs(CodeStyleConfig config)
        {
            // 获取EditorConfig规则列表
            var rules = EditorConfigManager.GetRules();
            return ExportConfigToAllIDEs(rules);
        }

        /// <summary>
        /// 导出配置到所有已安装的IDE
        /// </summary>
        /// <param name="rules">EditorConfig规则列表</param>
        /// <returns>是否成功</returns>
        public static bool ExportConfigToAllIDEs(List<EditorConfigRule> rules)
        {
            var allSuccess = true;
            var integrations = GetInstalledIntegrations();

            foreach (var integration in integrations)
            {
                if (!integration.ExportConfig(rules))
                {
                    allSuccess = false;
                }
            }

            return allSuccess;
        }
    }
}
