using System.Collections.Generic;
using System.Linq;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using UnityEditor;
using UnityEngine;

namespace TByd.CodeStyle.Editor.CodeCheck.IDE
{
    /// <summary>
    /// IDE集成管理器，用于管理和执行IDE集成
    /// </summary>
    public static class IDEIntegrationManager
    {
        // IDE集成列表
        private static readonly List<IDEIntegration> s_Integrations = new List<IDEIntegration>();

        // 是否已初始化
        private static bool s_Initialized;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static IDEIntegrationManager()
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
        /// 获取当前使用的IDE集成
        /// </summary>
        /// <returns>当前IDE集成</returns>
        public static IDEIntegration GetCurrentIntegration()
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
        public static List<IDEIntegration> GetInstalledIntegrations()
        {
            return s_Integrations.FindAll(i => i.IsInstalled);
        }

        /// <summary>
        /// 导出配置到当前IDE
        /// </summary>
        /// <param name="_rules">EditorConfig规则列表</param>
        /// <returns>是否成功</returns>
        public static bool ExportConfigToCurrentIDE(List<EditorConfigRule> _rules)
        {
            var currentIDE = GetCurrentIntegration();
            if (currentIDE != null)
            {
                return currentIDE.ExportConfig(_rules);
            }

            Debug.LogWarning("[TByd.CodeStyle] 未检测到当前使用的IDE，无法导出配置");
            return false;
        }

        /// <summary>
        /// 导出配置到所有已安装的IDE
        /// </summary>
        /// <param name="_rules">EditorConfig规则列表</param>
        /// <returns>是否全部成功</returns>
        public static bool ExportConfigToAllIDEs(List<EditorConfigRule> _rules)
        {
            bool allSuccess = true;
            foreach (var ide in GetInstalledIntegrations())
            {
                if (!ide.ExportConfig(_rules))
                {
                    allSuccess = false;
                }
            }
            return allSuccess;
        }
    }
} 