using UnityEngine;

namespace TBydFramework.Pool.Runtime.Config
{
    [CreateAssetMenu(fileName = "PoolSettings", menuName = "TByd/Pool/Settings")]
    public class PoolSettings : ScriptableObject
    {
        [Header("全局设置")]
        [SerializeField] private bool _enablePooling = true;
        [SerializeField] private bool _enableDiagnostics = true;
        [SerializeField] private bool _enableAutoPrewarm = true;

        [Header("性能设置")]
        [SerializeField, Range(1, 1000)] private int _defaultPoolSize = 32;
        [SerializeField, Range(0, 100)] private int _prewarmSize = 5;
        [SerializeField, Range(1, 300)] private float _maintenanceInterval = 60f;

        // 添加属性访问器
        public bool EnablePooling => _enablePooling;
        public bool EnableDiagnostics => _enableDiagnostics;
        public bool EnableAutoPrewarm => _enableAutoPrewarm;
        public int DefaultPoolSize => _defaultPoolSize;
        public int PrewarmSize => _prewarmSize;
        public float MaintenanceInterval => _maintenanceInterval;

        private void OnValidate()
        {
            _prewarmSize = Mathf.Min(_prewarmSize, _defaultPoolSize);
        }
    }
} 