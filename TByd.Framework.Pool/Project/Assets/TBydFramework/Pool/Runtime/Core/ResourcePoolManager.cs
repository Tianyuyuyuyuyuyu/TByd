using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 资源回收池管理器，用于全局管理不同类型的资源回收池
    /// </summary>
    public class ResourcePoolManager : MonoBehaviour
    {
        private static ResourcePoolManager _instance;
        
        private readonly Dictionary<Type, object> _pools = new();
        
        public static ResourcePoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("[ResourcePoolManager]");
                    _instance = go.AddComponent<ResourcePoolManager>();
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
        }

        /// <summary>
        /// 获取指定类型的资源池
        /// </summary>
        public ResourcePool<T> GetPool<T>(int maxSize = 8) where T : Object
        {
            var type = typeof(T);
            if (!_pools.TryGetValue(type, out var pool))
            {
                pool = new ResourcePool<T>(maxSize);
                _pools[type] = pool;
            }
            return (ResourcePool<T>)pool;
        }

        /// <summary>
        /// 清理指定类型的资源池
        /// </summary>
        public void ClearPool<T>() where T : Object
        {
            if (_pools.TryGetValue(typeof(T), out var pool))
            {
                ((ResourcePool<T>)pool).ClearAll();
            }
        }

        /// <summary>
        /// 清理所有资源池
        /// </summary>
        public void ClearAllPools()
        {
            foreach (var pool in _pools.Values)
            {
                if (pool is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _pools.Clear();
        }

        private void OnDestroy()
        {
            ClearAllPools();
        }
    }
} 