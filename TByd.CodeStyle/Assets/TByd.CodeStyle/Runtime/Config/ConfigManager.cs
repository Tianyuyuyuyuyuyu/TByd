using System;
using System.IO;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.Config
{
    /// <summary>
    /// 配置管理器，用于管理配置的加载、保存和迁移
    /// </summary>
    public static class ConfigManager
    {
        // 配置文件名
        private const string c_ConfigFileName = "TBydCodeStyleConfig.json";
        
        // 配置文件路径
        private static string s_ConfigFilePath;
        
        // 当前配置
        private static CodeStyleConfig s_CurrentConfig;
        
        // 配置是否已加载
        private static bool s_IsConfigLoaded;
        
        // 配置变更事件
        public static event Action ConfigChanged;
        
        /// <summary>
        /// 初始化配置管理器
        /// </summary>
        public static void Initialize()
        {
            s_ConfigFilePath = Path.Combine(Application.dataPath, "..", "ProjectSettings", c_ConfigFileName);
            LoadConfig();
        }
        
        /// <summary>
        /// 获取当前配置
        /// </summary>
        /// <returns>当前配置</returns>
        public static CodeStyleConfig GetConfig()
        {
            if (!s_IsConfigLoaded)
            {
                LoadConfig();
            }
            
            return s_CurrentConfig;
        }
        
        /// <summary>
        /// 保存配置
        /// </summary>
        public static void SaveConfig()
        {
            if (s_CurrentConfig == null)
            {
                return;
            }
            
            try
            {
                string configJson = JsonUtility.ToJson(s_CurrentConfig, true);
                string directoryPath = Path.GetDirectoryName(s_ConfigFilePath);
                
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                
                File.WriteAllText(s_ConfigFilePath, configJson);
                Debug.Log($"[TByd.CodeStyle] 配置已保存到: {s_ConfigFilePath}");
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 保存配置失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 加载配置
        /// </summary>
        public static void LoadConfig()
        {
            try
            {
                if (File.Exists(s_ConfigFilePath))
                {
                    string configJson = File.ReadAllText(s_ConfigFilePath);
                    s_CurrentConfig = JsonUtility.FromJson<CodeStyleConfig>(configJson);
                    
                    // 检查配置版本并进行迁移
                    MigrateConfigIfNeeded();
                }
                else
                {
                    // 创建默认配置
                    s_CurrentConfig = new CodeStyleConfig();
                    SaveConfig();
                }
                
                s_IsConfigLoaded = true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 加载配置失败: {e.Message}");
                s_CurrentConfig = new CodeStyleConfig();
                s_IsConfigLoaded = true;
            }
        }
        
        /// <summary>
        /// 重置配置
        /// </summary>
        public static void ResetConfig()
        {
            s_CurrentConfig = new CodeStyleConfig();
            SaveConfig();
            
            // 触发配置变更事件
            ConfigChanged?.Invoke();
        }
        
        /// <summary>
        /// 检查配置版本并进行迁移
        /// </summary>
        private static void MigrateConfigIfNeeded()
        {
            // 当前配置版本
            int currentVersion = s_CurrentConfig.ConfigVersion;
            
            // 最新配置版本
            int latestVersion = 1;
            
            if (currentVersion < latestVersion)
            {
                // 执行迁移
                for (int version = currentVersion + 1; version <= latestVersion; version++)
                {
                    MigrateConfig(version);
                }
                
                // 更新配置版本
                s_CurrentConfig.ConfigVersion = latestVersion;
                
                // 保存迁移后的配置
                SaveConfig();
                
                Debug.Log($"[TByd.CodeStyle] 配置已从版本 {currentVersion} 迁移到版本 {latestVersion}");
            }
        }
        
        /// <summary>
        /// 迁移配置
        /// </summary>
        /// <param name="_targetVersion">目标版本</param>
        private static void MigrateConfig(int _targetVersion)
        {
            switch (_targetVersion)
            {
                case 1:
                    // 版本1的迁移逻辑
                    break;
                    
                // 添加更多版本的迁移逻辑
                
                default:
                    Debug.LogWarning($"[TByd.CodeStyle] 未知的配置版本: {_targetVersion}");
                    break;
            }
        }
        
        /// <summary>
        /// 通知配置变更
        /// </summary>
        public static void NotifyConfigChanged()
        {
            ConfigChanged?.Invoke();
        }
    }
} 