using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TBydFramework.Pool.Runtime.Interfaces;
using TBydFramework.Pool.Runtime.Config;

namespace TBydFramework.Pool.Runtime.Utilities
{
    public static class PoolHelper
    {
        private static readonly Dictionary<int, string> _poolNames = new();

        public static string GetPoolName(object obj)
        {
            if (obj == null) return "Unknown";
            
            var instanceId = obj.GetHashCode();
            if (!_poolNames.TryGetValue(instanceId, out var name))
            {
                name = GeneratePoolName(obj);
                _poolNames[instanceId] = name;
            }
            return name;
        }

        private static string GeneratePoolName(object obj)
        {
            if (obj is GameObject go)
                return $"Pool_{go.name}_{go.GetInstanceID()}";
            
            return $"Pool_{obj.GetType().Name}_{obj.GetHashCode()}";
        }

        public static void WarmupAll()
        {
            var pools = Object.FindObjectsOfType<MonoBehaviour>()
                .OfType<IPool>()
                .Where(p => p is IPoolConfiguration config && 
                           config.GetCurrentConfig()?.EnableAutoPrewarm == true);

            foreach (var pool in pools)
            {
                var config = ((IPoolConfiguration)pool).GetCurrentConfig();
                if (config != null)
                {
                    pool.Prewarm(config.PrewarmSize);
                }
            }
        }

        public static void WarmupPool<T>(IPool<T> pool, int count) where T : class
        {
            if (pool == null) return;
            pool.Prewarm(count);
        }
    }
} 