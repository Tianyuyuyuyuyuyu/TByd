using System;
using System.IO;
using TByd.CodeStyle.Runtime.Config;
using UnityEditor;
using UnityEngine;

namespace TByd.CodeStyle.Editor.Config
{
    /// <summary>
    /// 编辑器配置提供者，用于在编辑器中管理配置
    /// </summary>
    [InitializeOnLoad]
    public static class ConfigProvider
    {
        /// <summary>
        /// 静态构造函数，在编辑器加载时初始化
        /// </summary>
        static ConfigProvider()
        {
            // 初始化配置管理器
            ConfigManager.Initialize();

            // 订阅配置变更事件
            ConfigManager.OnConfigChanged += OnConfigChangedMethod;

            // 订阅编辑器更新事件，用于检测配置文件变更
            EditorApplication.update += CheckConfigFileChange;
        }

        // 配置变更事件
        public static event Action OnConfigChanged;

        /// <summary>
        /// 获取当前配置
        /// </summary>
        /// <returns>当前配置</returns>
        public static CodeStyleConfig GetConfig()
        {
            return ConfigManager.GetConfig();
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public static void SaveConfig()
        {
            ConfigManager.SaveConfig();

            // 触发配置变更事件
            OnConfigChangedMethod();
        }

        /// <summary>
        /// 重置配置
        /// </summary>
        public static void ResetConfig()
        {
            ConfigManager.ResetConfig();
        }

        /// <summary>
        /// 配置变更处理
        /// </summary>
        private static void OnConfigChangedMethod()
        {
            // 触发编辑器配置变更事件
            OnConfigChanged?.Invoke();

            // 刷新编辑器窗口
            // 注意：不要使用EditorUtility.SetDirty(null)，这会导致ArgumentNullException
            // 使用EditorWindow.RepaintAll()来刷新所有编辑器窗口
            if (EditorWindow.focusedWindow != null)
            {
                EditorWindow.focusedWindow.Repaint();
            }
        }

        /// <summary>
        /// 检查配置文件变更
        /// </summary>
        private static void CheckConfigFileChange()
        {
            // 这里可以添加检测配置文件变更的逻辑
            // 例如，检查文件修改时间，如果变更则重新加载配置
        }

        /// <summary>
        /// 导出配置
        /// </summary>
        /// <param name="path">导出路径</param>
        public static void ExportConfig(string path)
        {
            try
            {
                var configJson = JsonUtility.ToJson(GetConfig(), true);
                File.WriteAllText(path, configJson);
                Debug.Log($"[TByd.CodeStyle] 配置已导出到: {path}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 导出配置失败: {e.Message}");
            }
        }

        /// <summary>
        /// 导入配置
        /// </summary>
        /// <param name="path">导入路径</param>
        public static void ImportConfig(string path)
        {
            try
            {
                var configJson = File.ReadAllText(path);
                var config = JsonUtility.FromJson<CodeStyleConfig>(configJson);

                // 更新当前配置
                ConfigManager.GetConfig().ConfigVersion = config.ConfigVersion;
                ConfigManager.GetConfig().EnableGitCommitCheck = config.EnableGitCommitCheck;
                ConfigManager.GetConfig().EnableCodeStyleCheck = config.EnableCodeStyleCheck;
                ConfigManager.GetConfig().CheckOnCompile = config.CheckOnCompile;
                ConfigManager.GetConfig().CheckBeforeCommit = config.CheckBeforeCommit;
                ConfigManager.GetConfig().GitCommitConfig = config.GitCommitConfig;
                ConfigManager.GetConfig().CodeCheckConfig = config.CodeCheckConfig;

                // 保存配置
                SaveConfig();

                Debug.Log($"[TByd.CodeStyle] 配置已从 {path} 导入");
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 导入配置失败: {e.Message}");
            }
        }
    }
}
