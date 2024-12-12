using UnityEngine;
using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Enums;

namespace TBydFramework.Pool.Runtime.Config
{
    /// <summary>
    /// 对象池配置设置
    /// </summary>
    [CreateAssetMenu(fileName = "PoolSettings", menuName = "TByd Framework/Pool/Pool Settings")]
    public class PoolSettings : ScriptableObject
    {
        /// <summary>
        /// 默认对象池大小
        /// </summary>
        [SerializeField] private int _defaultPoolSize = 32;
        [SerializeField] private int _maxPoolSize = 100;
        [SerializeField] private int _prewarmSize = 0;

        [Header("全局设置")]
        [SerializeField] private bool _enableGlobalDiagnostics = true;
        [SerializeField] private bool _enableAutoMaintenance = true;
        [SerializeField] private bool _enablePooling = true;
        [SerializeField] private bool _enableAutoPrewarm = true;
        [SerializeField] private float _maintenanceInterval = 60f;

        /// <summary>
        /// 获取默认对象池大小
        /// </summary>
        public int DefaultPoolSize
        {
            get => _defaultPoolSize;
            set => _defaultPoolSize = Mathf.Max(0, value);
        }

        public int MaxPoolSize
        {
            get => _maxPoolSize;
            set => _maxPoolSize = Mathf.Max(1, value);
        }

        public int PrewarmSize
        {
            get => _prewarmSize;
            set => _prewarmSize = Mathf.Max(0, value);
        }

        /// <summary>
        /// 是否启用全局诊断
        /// </summary>
        public bool EnableGlobalDiagnostics { get => _enableGlobalDiagnostics; set => _enableGlobalDiagnostics = value; }
        public bool EnableAutoMaintenance { get => _enableAutoMaintenance; set => _enableAutoMaintenance = value; }
        public bool EnablePooling { get => _enablePooling; set => _enablePooling = value; }
        public bool EnableAutoPrewarm { get => _enableAutoPrewarm; set => _enableAutoPrewarm = value; }

        /// <summary>
        /// 全局维护间隔（秒）
        /// </summary>
        public float MaintenanceInterval
        {
            get => _maintenanceInterval;
            set => _maintenanceInterval = Mathf.Max(0f, value);
        }
        public bool EnableDiagnostics { get => _enableGlobalDiagnostics; set => _enableGlobalDiagnostics = value; }

        /// <summary>
        /// 对象池配置文件
        /// </summary>
        [SerializeField] private List<PoolProfile> _profiles = new();

        /// <summary>
        /// 使用指定的设置初始化
        /// </summary>
        public void Initialize(bool enableDiagnostics, bool enableAutoMaintenance, float maintenanceInterval)
        {
            _enableGlobalDiagnostics = enableDiagnostics;
            _enableAutoMaintenance = enableAutoMaintenance;
            _maintenanceInterval = maintenanceInterval;
        }

        /// <summary>
        /// 获取所有对象池配置文件
        /// </summary>
        public IReadOnlyList<PoolProfile> PoolProfiles => _profiles;

        /// <summary>
        /// 清除所有对象池配置文件
        /// </summary>
        public void ClearProfiles()
        {
            _profiles.Clear();
        }

        /// <summary>
        /// 获取指定键的对象池配置
        /// </summary>
        public PoolProfile GetProfile(string key)
        {
            return _profiles.Find(p => p.Key == key);
        }

        /// <summary>
        /// 获取所有对象池配置
        /// </summary>
        public IReadOnlyList<PoolProfile> GetAllProfiles()
        {
            return _profiles.AsReadOnly();
        }

        /// <summary>
        /// 添加对象池配置
        /// </summary>
        public void AddProfile(PoolProfile profile)
        {
            if (profile == null) return;
            
            var existingIndex = _profiles.FindIndex(p => p.Key == profile.Key);
            if (existingIndex >= 0)
                _profiles[existingIndex] = profile;
            else
                _profiles.Add(profile);
        }

        /// <summary>
        /// 对象池配置文件
        /// </summary>
        [Serializable]
        public class PoolProfile
        {
            public string Key;
            public GameObject Prefab;
            public int InitialCapacity;
            public int MaxSize;
            public PoolType Type;
            public float MaintenanceInterval;
            public bool EnablePrewarm;
            public int PrewarmSize;
            public bool UseAddressables;
            public string AddressableKey;
        }

        public void ValidateSettings()
        {
            foreach (var profile in _profiles)
            {
                profile.InitialCapacity = Mathf.Max(0, profile.InitialCapacity);
                profile.MaxSize = Mathf.Max(1, profile.MaxSize);
                profile.MaintenanceInterval = Mathf.Max(0f, profile.MaintenanceInterval);
                profile.PrewarmSize = Mathf.Max(0, profile.PrewarmSize);
            }
        }
    }
} 