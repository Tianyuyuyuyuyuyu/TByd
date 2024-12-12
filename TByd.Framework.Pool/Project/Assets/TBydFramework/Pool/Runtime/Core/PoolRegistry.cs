using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 对象池注册表，用于管理和追踪所有活动的对象池。
    /// </summary>
    public static class PoolRegistry
    {
        private static readonly Dictionary<string, IPoolInfo> _pools = new();
        
        /// <summary>
        /// 注册一个对象池
        /// </summary>
        /// <param name="pool">要注册的对象池</param>
        public static void RegisterPool(IPoolInfo pool)
        {
            if (pool == null) throw new ArgumentNullException(nameof(pool));
            _pools[pool.Name] = pool;
        }

        /// <summary>
        /// 获取指定名称的对象池信息
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <returns>对象池信息，如果不存在则返回null</returns>
        public static IPoolInfo GetPoolInfo(string poolName)
        {
            return _pools.TryGetValue(poolName, out var pool) ? pool : null;
        }

        /// <summary>
        /// 获取所有活动的对象池名称
        /// </summary>
        /// <returns>对象池名称列表</returns>
        public static IReadOnlyCollection<string> GetActivePoolNames()
        {
            return _pools.Keys;
        }

        /// <summary>
        /// 注销指定名称的对象池
        /// </summary>
        /// <param name="poolName">要注销的对象池名称</param>
        public static void UnregisterPool(string poolName)
        {
            _pools.Remove(poolName);
        }

        /// <summary>
        /// 清空所有注册的对象池
        /// </summary>
        public static void Clear()
        {
            _pools.Clear();
        }
    }
} 