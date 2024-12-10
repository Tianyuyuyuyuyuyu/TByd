using UnityEngine;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Diagnostics;

namespace TBydFramework.Pool.Runtime.Config
{
    public class PoolConfigManager : MonoBehaviour
    {
        private static PoolConfigManager _instance;
        
        [SerializeField] 
        private PoolConfig _defaultConfig;
        
        [SerializeField] 
        private List<PoolConfig> _environmentConfigs;
        
        private Dictionary<string, PoolConfig> _activeConfigs = new();

        public static PoolConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("[PoolConfigManager]");
                    _instance = go.AddComponent<PoolConfigManager>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            
            InitializeConfigs();
        }

        private void InitializeConfigs()
        {
            if (_defaultConfig == null)
            {
                _defaultConfig = CreateDefaultConfig();
            }
            
            foreach (var config in _environmentConfigs)
            {
                if (config != null)
                {
                    _activeConfigs[config.name] = config;
                }
            }
        }

        private PoolConfig CreateDefaultConfig()
        {
            var config = ScriptableObject.CreateInstance<PoolConfig>();
            return config;
        }

        public PoolConfig GetConfig(string configName = null)
        {
            if (string.IsNullOrEmpty(configName) || !_activeConfigs.TryGetValue(configName, out var config))
            {
                return _defaultConfig;
            }
            return config;
        }

        public void ApplyConfig(string configName)
        {
            var config = GetConfig(configName);
            // 通知所有池更新配置
            PoolDiagnosticEvents.RaisePoolOperation("ConfigChange", PoolOperationType.Maintenance);
        }
    }
} 