using System.Collections.Generic;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Config
{
    /// <summary>
    /// 对象池配置管理器
    /// </summary>
    public static class PoolConfigManager
    {
        private static PoolSettings _settings;
        private static readonly Dictionary<string, object> _runtimeOverrides = new();

        /// <summary>
        /// 初始化配置管理器
        /// </summary>
        /// <param name="settings">对象池配置</param>
        public static void Initialize(PoolSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// 获取当前配置
        /// </summary>
        public static PoolSettings GetSettings() => _settings;

        /// <summary>
        /// 获取指定池的配置
        /// </summary>
        public static PoolSettings.PoolProfile GetPoolProfile(string key)
        {
            return _settings?.GetProfile(key);
        }

        public static void SetRuntimeOverride<T>(string key, T value)
        {
            _runtimeOverrides[key] = value;
        }

        public static T GetSetting<T>(string key)
        {
            if (_runtimeOverrides.TryGetValue(key, out var value) && value is T typedValue)
            {
                return typedValue;
            }
            return default;
        }

        public static void ClearRuntimeOverrides()
        {
            _runtimeOverrides.Clear();
        }
    }
} 