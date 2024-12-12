using UnityEngine;
using System;

namespace TBydFramework.Pool.Runtime.Config
{
    [CreateAssetMenu(fileName = "PoolConfiguration", menuName = "TByd/Pool/Configuration")]
    public class PoolConfigurationAsset : ScriptableObject
    {
        [Serializable]
        public class PoolPreset
        {
            public string Name;
            public GameObject Prefab;
            public int InitialCapacity = 10;
            public int MaxSize = 100;
            public bool AutoPrewarm = true;
            public bool EnableDiagnostics = true;
        }

        [Header("Global Settings")]
        [SerializeField] private bool _enableGlobalDiagnostics = true;
        [SerializeField] private bool _enableAutoPrewarm = true;
        [SerializeField] private float _maintenanceInterval = 60f;

        [Header("Default Values")]
        [SerializeField] private int _defaultInitialCapacity = 10;
        [SerializeField] private int _defaultMaxSize = 100;
        [SerializeField] private float _defaultGrowthFactor = 2f;

        [Header("Pool Presets")]
        [SerializeField] private PoolPreset[] _poolPresets;

        // Properties
        public bool EnableGlobalDiagnostics => _enableGlobalDiagnostics;
        public bool EnableAutoPrewarm => _enableAutoPrewarm;
        public float MaintenanceInterval => _maintenanceInterval;
        public int DefaultInitialCapacity => _defaultInitialCapacity;
        public int DefaultMaxSize => _defaultMaxSize;
        public float DefaultGrowthFactor => _defaultGrowthFactor;
        public PoolPreset[] PoolPresets => _poolPresets;
    }
} 