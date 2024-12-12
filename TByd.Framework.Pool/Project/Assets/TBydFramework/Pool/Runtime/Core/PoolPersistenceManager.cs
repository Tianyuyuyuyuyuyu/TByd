using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TBydFramework.Pool.Runtime.External;
using TBydFramework.Pool.Runtime.Config;
using TBydFramework.Pool.Runtime.Enums;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 对象池持久化管理器
    /// </summary>
    public static class PoolPersistenceManager
    {
        private const string StateFileName = "pool_state.json";
        private static string SavePath => Path.Combine(Application.persistentDataPath, "PoolSystem");

        [Serializable]
        private class PoolStateData
        {
            public string Name;
            public int Count;
            public int MaxSize;
            public bool UseAddressables;
            public string AddressableKey;
            public Dictionary<string, object> CustomData = new();
            public Dictionary<string, object> GlobalSettings = new();
        }

        [Serializable]
        private class PoolSystemState
        {
            public List<PoolStateData> Pools = new();
            public Dictionary<string, object> GlobalSettings = new();
        }

        /// <summary>
        /// 保存对象池系统的状态
        /// </summary>
        public static void SaveState(IEnumerable<IPoolInfo> pools)
        {
            var data = new PoolSystemState();

            foreach (var pool in pools)
            {
                var stateData = new PoolStateData
                {
                    Name = pool.Name,
                    Count = pool.Count,
                    MaxSize = pool.MaxSize
                };

                if (pool.Type == PoolType.Addressable)
                {
                    stateData.UseAddressables = true;
                    stateData.AddressableKey = pool.Name;
                }

                data.Pools.Add(stateData);
            }

            // 保存全局设置
            var settings = PoolConfigManager.GetSettings();
            if (settings != null)
            {
                data.GlobalSettings["EnableGlobalDiagnostics"] = settings.EnableGlobalDiagnostics;
                data.GlobalSettings["EnableAutoCleanup"] = settings.EnableAutoMaintenance;
                data.GlobalSettings["MaintenanceInterval"] = settings.MaintenanceInterval;
            }

            try
            {
                Directory.CreateDirectory(SavePath);
                var json = JsonUtility.ToJson(data, true);
                File.WriteAllText(Path.Combine(SavePath, StateFileName), json);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save pool state: {e.Message}");
            }
        }

        /// <summary>
        /// 加载对象池系统的状态
        /// </summary>
        public static bool LoadState()
        {
            var statePath = Path.Combine(SavePath, StateFileName);
            if (!File.Exists(statePath)) return false;

            try
            {
                var json = File.ReadAllText(statePath);
                var data = JsonUtility.FromJson<PoolSystemState>(json);

                // 应用全局设置
                if (data.GlobalSettings.Count > 0)
                {
                    var settings = ScriptableObject.CreateInstance<PoolSettings>();
                    settings.Initialize(
                        enableDiagnostics: data.GlobalSettings.TryGetValue("EnableGlobalDiagnostics", out var diagnostics) && (bool)diagnostics,
                        enableAutoMaintenance: data.GlobalSettings.TryGetValue("EnableAutoCleanup", out var cleanup) && (bool)cleanup,
                        maintenanceInterval: data.GlobalSettings.TryGetValue("MaintenanceInterval", out var interval) ? (float)interval : 60f
                    );

                    PoolConfigManager.Initialize(settings);
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load pool state: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// 清除保存的状态
        /// </summary>
        public static void ClearSavedStates()
        {
            var statePath = Path.Combine(SavePath, StateFileName);
            if (File.Exists(statePath))
            {
                try
                {
                    File.Delete(statePath);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to clear saved states: {e.Message}");
                }
            }
        }

        /// <summary>
        /// 检查是否存在保存的状态
        /// </summary>
        public static bool HasSavedStates()
        {
            var statePath = Path.Combine(SavePath, StateFileName);
            return File.Exists(statePath);
        }

        private static bool LoadPoolState()
        {
            try
            {
                var statePath = Path.Combine(SavePath, StateFileName);
                if (!File.Exists(statePath)) return false;

                var json = File.ReadAllText(statePath);
                var data = JsonUtility.FromJson<PoolSystemState>(json);

                if (data != null)
                {
                    var settings = ScriptableObject.CreateInstance<PoolSettings>();
                    
                    // 直接设置属性值
                    if (data.GlobalSettings != null)
                    {
                        if (data.GlobalSettings.TryGetValue("EnableGlobalDiagnostics", out var diagnostics))
                            settings.EnableGlobalDiagnostics = (bool)diagnostics;
                        
                        if (data.GlobalSettings.TryGetValue("EnableAutoMaintenance", out var maintenance))
                            settings.EnableAutoMaintenance = (bool)maintenance;
                        
                        if (data.GlobalSettings.TryGetValue("MaintenanceInterval", out var interval))
                            settings.MaintenanceInterval = (float)interval;
                    }

                    PoolConfigManager.Initialize(settings);
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load pool state: {e.Message}");
                return false;
            }
        }
    }
} 