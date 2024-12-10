using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using TBydFramework.Pool.Runtime.Config;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 资源回收池管理器，用于全局管理不同类型的资源回收池
    /// </summary>
    public class ResourcePoolManager : MonoBehaviour
    {
        private static ResourcePoolManager _instance;
        
        private readonly Dictionary<Type, object> _pools = new();
        private readonly Dictionary<Type, string> _resourcePaths = new();
        
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
        /// 注册资源路径
        /// </summary>
        public void RegisterResourcePath<T>(string resourcePath) where T : Object
        {
            _resourcePaths[typeof(T)] = resourcePath;
        }

        /// <summary>
        /// 获取指定类型的资源池
        /// </summary>
        public ResourcePool<T> GetPool<T>() where T : Object
        {
            var type = typeof(T);
            if (!_pools.TryGetValue(type, out var pool))
            {
                if (!_resourcePaths.TryGetValue(type, out var path))
                {
                    throw new ArgumentException($"未找到类型 {type} 的资源路径，请先调用 RegisterResourcePath");
                }
                
                pool = new ResourcePool<T>(path);
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
                ((ResourcePool<T>)pool).Clear();
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