using System;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TByd.CodeStyle.Runtime.Config
{
    /// <summary>
    /// 配置管理器，用于管理配置的加载、保存和迁移
    /// </summary>
    public static class ConfigManager
    {
        // 配置文件名
        private const string c_ConfigFileName = "TBydCodeStyleConfig.json";
        private const string c_AssetPath = "Assets/TByd.CodeStyle/Resources/CodeStyleConfig.asset";

        // 配置文件路径
        private static string s_ConfigFilePath;

        // 当前配置
        private static CodeStyleConfig s_CurrentConfig;
        private static CodeStyleConfig s_RuntimeConfig;

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

#if UNITY_EDITOR
            // 在编辑器中，如果需要序列化，返回ScriptableObject实例
            if (s_RuntimeConfig == null)
            {
                s_RuntimeConfig = AssetDatabase.LoadAssetAtPath<CodeStyleConfig>(c_AssetPath);
                if (s_RuntimeConfig == null)
                {
                    s_RuntimeConfig = ScriptableObject.CreateInstance<CodeStyleConfig>();
                    
                    // 确保目录存在
                    var directory = Path.GetDirectoryName(c_AssetPath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    AssetDatabase.CreateAsset(s_RuntimeConfig, c_AssetPath);
                    AssetDatabase.SaveAssets();
                }
            }

            // 同步配置数据
            CopyConfigData(s_CurrentConfig, s_RuntimeConfig);
            return s_RuntimeConfig;
#else
            return s_CurrentConfig;
#endif
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public static void SaveConfig()
        {
#if UNITY_EDITOR
            if (s_RuntimeConfig != null)
            {
                // 同步配置数据
                CopyConfigData(s_RuntimeConfig, s_CurrentConfig);
                EditorUtility.SetDirty(s_RuntimeConfig);
                AssetDatabase.SaveAssets();
            }
#endif

            if (s_CurrentConfig == null)
            {
                Debug.LogError("[TByd.CodeStyle] 保存配置失败: 当前配置为空");
                return;
            }

            try
            {
                var configJson = JsonUtility.ToJson(s_CurrentConfig, true);
                var directoryPath = Path.GetDirectoryName(s_ConfigFilePath);

                Debug.Log($"[TByd.CodeStyle] 准备保存配置到: {s_ConfigFilePath}");
                Debug.Log($"[TByd.CodeStyle] 配置目录: {directoryPath}");

                if (!Directory.Exists(directoryPath))
                {
                    Debug.Log($"[TByd.CodeStyle] 创建配置目录: {directoryPath}");
                    Directory.CreateDirectory(directoryPath);
                }

                File.WriteAllText(s_ConfigFilePath, configJson);
                Debug.Log($"[TByd.CodeStyle] 配置已成功保存到: {s_ConfigFilePath}");

                // 触发配置变更事件
                ConfigChanged?.Invoke();
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
                    var configJson = File.ReadAllText(s_ConfigFilePath);
                    s_CurrentConfig = ScriptableObject.CreateInstance<CodeStyleConfig>();
                    JsonUtility.FromJsonOverwrite(configJson, s_CurrentConfig);

                    // 检查配置版本并进行迁移
                    MigrateConfigIfNeeded();
                }
                else
                {
                    // 创建默认配置
                    s_CurrentConfig = ScriptableObject.CreateInstance<CodeStyleConfig>();
                    SaveConfig();
                }

                s_IsConfigLoaded = true;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 加载配置失败: {e.Message}");
                s_CurrentConfig = ScriptableObject.CreateInstance<CodeStyleConfig>();
                s_IsConfigLoaded = true;
            }
        }

        /// <summary>
        /// 重置配置
        /// </summary>
        public static void ResetConfig()
        {
#if UNITY_EDITOR
            if (s_RuntimeConfig != null)
            {
                UnityEngine.Object.DestroyImmediate(s_RuntimeConfig, true);
                AssetDatabase.DeleteAsset(c_AssetPath);
                s_RuntimeConfig = null;
            }
#endif

            s_CurrentConfig = ScriptableObject.CreateInstance<CodeStyleConfig>();
            SaveConfig();
        }

        /// <summary>
        /// 检查配置版本并进行迁移
        /// </summary>
        private static void MigrateConfigIfNeeded()
        {
            // 当前配置版本
            var currentVersion = s_CurrentConfig.ConfigVersion;

            // 最新配置版本
            var latestVersion = 1;

            if (currentVersion < latestVersion)
            {
                // 执行迁移
                for (var version = currentVersion + 1; version <= latestVersion; version++)
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

#if UNITY_EDITOR
        /// <summary>
        /// 复制配置数据
        /// </summary>
        private static void CopyConfigData(CodeStyleConfig _source, CodeStyleConfig _target)
        {
            if (_source == null || _target == null)
                return;

            JsonUtility.FromJsonOverwrite(JsonUtility.ToJson(_source), _target);
        }
#endif
    }
}
