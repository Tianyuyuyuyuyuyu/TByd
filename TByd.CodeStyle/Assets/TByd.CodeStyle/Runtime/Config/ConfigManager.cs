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
        /// 配置文件路径
        /// </summary>
        public static string ConfigPath
        {
            get => s_ConfigFilePath;
            set => SetConfigPath(value);
        }

        /// <summary>
        /// 设置配置文件路径
        /// </summary>
        /// <param name="_path">配置文件路径</param>
        public static void SetConfigPath(string _path)
        {
            if (string.IsNullOrEmpty(_path))
            {
                Debug.LogError("[TByd.CodeStyle] 配置文件路径不能为空");
                return;
            }

            s_ConfigFilePath = _path;
            s_IsConfigLoaded = false; // 重置配置加载状态，以便下次获取配置时重新加载
        }

        /// <summary>
        /// 初始化配置管理器
        /// </summary>
        public static void Initialize()
        {
            // 只有在未设置路径时才设置默认路径
            if (string.IsNullOrEmpty(s_ConfigFilePath))
            {
                s_ConfigFilePath = Path.Combine(Application.dataPath, "..", "ProjectSettings", c_ConfigFileName);
                Debug.Log($"[TByd.CodeStyle] 使用默认配置路径: {s_ConfigFilePath}");
            }
            else
            {
                Debug.Log($"[TByd.CodeStyle] 使用已设置的配置路径: {s_ConfigFilePath}");
            }

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
                Debug.LogError("[TByd.CodeStyle] 保存配置失败: 当前配置为空");
                return;
            }

            try
            {
                string configJson = JsonUtility.ToJson(s_CurrentConfig, true);
                string directoryPath = Path.GetDirectoryName(s_ConfigFilePath);

                Debug.Log($"[TByd.CodeStyle] 准备保存配置到: {s_ConfigFilePath}");
                Debug.Log($"[TByd.CodeStyle] 配置目录: {directoryPath}");

                if (!Directory.Exists(directoryPath))
                {
                    Debug.Log($"[TByd.CodeStyle] 创建配置目录: {directoryPath}");
                    Directory.CreateDirectory(directoryPath);
                }
                else
                {
                    Debug.Log($"[TByd.CodeStyle] 配置目录已存在: {directoryPath}");
                }

                // 确保有写入权限
                try
                {
                    // 创建一个临时文件来测试写入权限
                    string testFile = Path.Combine(directoryPath, "test_write.tmp");
                    File.WriteAllText(testFile, "test");
                    Debug.Log($"[TByd.CodeStyle] 成功写入测试文件: {testFile}");
                    File.Delete(testFile);
                    Debug.Log($"[TByd.CodeStyle] 成功删除测试文件: {testFile}");
                }
                catch (Exception e)
                {
                    Debug.LogError($"[TByd.CodeStyle] 无法写入配置目录: {e.Message}");
                    throw; // 重新抛出异常，让上层处理
                }

                File.WriteAllText(s_ConfigFilePath, configJson);

                // 验证文件是否成功写入
                if (File.Exists(s_ConfigFilePath))
                {
                    Debug.Log($"[TByd.CodeStyle] 配置已成功保存到: {s_ConfigFilePath}");
                }
                else
                {
                    Debug.LogError($"[TByd.CodeStyle] 配置文件保存后不存在: {s_ConfigFilePath}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 保存配置失败: {e.Message}\n{e.StackTrace}");
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
