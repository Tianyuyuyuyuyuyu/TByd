using System;
using UnityEngine;
using System.Collections.Generic;

namespace TBydFramework.Pool.Runtime.Config
{
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "TByd/Pool/PoolConfig")]
    public class PoolConfig : ScriptableObject
    {
        [Header("基础配置")]
        [SerializeField, Range(1, 1000)] 
        private int _defaultMaxSize = 100;
        
        [SerializeField, Range(1, 300)] 
        private float _maintenanceInterval = 60f;
        
        [SerializeField, Range(1, 100)] 
        private int _prewarmSize = 5;

        [Header("性能优化")]
        [SerializeField, Range(8, 1024)] 
        private int _initialCapacity = 32;
        
        [SerializeField, Range(0.1f, 10f)] 
        private float _growthFactor = 2f;
        
        [SerializeField, Range(0f, 1f)] 
        private float _shrinkThreshold = 0.4f;

        [Header("监控设置")]
        [SerializeField] 
        private bool _enableDiagnostics = true;
        
        [SerializeField] 
        private bool _enableAutoPrewarm = true;
        
        [SerializeField] 
        private bool _enablePerformanceMonitoring = true;
        
        [SerializeField, Range(10, 1000)] 
        private int _metricsHistorySize = 100;

        [Header("资源管理")]
        [SerializeField] 
        private bool _enableAutoRelease = true;
        
        [SerializeField, Range(1, 3600)] 
        private float _autoReleaseInterval = 300f;
        
        [SerializeField, Range(0f, 1f)] 
        private float _keepAliveRatio = 0.25f;

        [Header("类型特定配置")]
        [SerializeField]
        private List<TypeSpecificConfig> _typeConfigs = new();

        // 公共属性
        public int DefaultMaxSize => _defaultMaxSize;
        public float MaintenanceInterval => _maintenanceInterval;
        public int PrewarmSize => _prewarmSize;
        public int InitialCapacity => _initialCapacity;
        public float GrowthFactor => _growthFactor;
        public float ShrinkThreshold => _shrinkThreshold;
        public bool EnableDiagnostics => _enableDiagnostics;
        public bool EnableAutoPrewarm => _enableAutoPrewarm;
        public bool EnablePerformanceMonitoring => _enablePerformanceMonitoring;
        public int MetricsHistorySize => _metricsHistorySize;
        public bool EnableAutoRelease => _enableAutoRelease;
        public float AutoReleaseInterval => _autoReleaseInterval;
        public float KeepAliveRatio => _keepAliveRatio;

        public void Validate()
        {
            _defaultMaxSize = Mathf.Max(1, _defaultMaxSize);
            _maintenanceInterval = Mathf.Max(1f, _maintenanceInterval);
            _prewarmSize = Mathf.Min(_prewarmSize, _defaultMaxSize);
            _initialCapacity = Mathf.Max(8, _initialCapacity);
            _metricsHistorySize = Mathf.Max(10, _metricsHistorySize);
            _autoReleaseInterval = Mathf.Max(1f, _autoReleaseInterval);
            _keepAliveRatio = Mathf.Clamp01(_keepAliveRatio);
        }

        private void OnValidate()
        {
            Validate();
        }

        public TypeSpecificConfig GetTypeConfig(Type type)
        {
            return _typeConfigs.Find(c => c.TypeName == type.FullName);
        }
    }

    [Serializable]
    public class TypeSpecificConfig
    {
        public string TypeName;
        public int MaxSize;
        public int PrewarmSize;
        public float MaintenanceInterval;
        public bool EnableAutoRelease;
    }
} 