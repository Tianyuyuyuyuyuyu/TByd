using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 对象池管理器,用于全局管理和复用对象池
    /// </summary>
    public sealed class PoolManager : SingletonBehaviour<PoolManager>
    {
        private static PoolManager _instance;
        
        /// <summary>
        /// 获取PoolManager单例实例
        /// </summary>
        public static PoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("[PoolManager]");
                    _instance = go.AddComponent<PoolManager>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        // 使用泛型缓存优化性能
        private readonly ConcurrentDictionary<Type, object> _poolMap = new();
        
        // 存储所有GameObject对象池
        private readonly ConcurrentDictionary<string, GameObjectPool> _gameObjectPools = new();
        
        // 存储所有共享GameObject对象池
        private readonly Dictionary<string, ISharedGameObjectPool> _sharedGameObjectPools = new();

        private readonly Dictionary<Type, GameObject> _prefabMap = new();

        private readonly Dictionary<Type, string> _resourcePathMap = new();

        // 添加池配置
        [Serializable]
        public class PoolConfig
        {
            public int DefaultCapacity = 32;
            public int MaxSize = 1000;
            public float CleanupInterval = 60f;
        }
        
        [SerializeField] private PoolConfig _config = new PoolConfig();

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
        /// 获取或创建指定类型的对象池
        /// </summary>
        public ObjectPool<T> GetPool<T>(Func<T> createFunc = null) where T : class
        {
            var type = typeof(T);
            if (!_poolMap.TryGetValue(type, out var pool))
            {
                pool = new ObjectPool<T>(createFunc ?? (() => Activator.CreateInstance<T>()));
                _poolMap[type] = pool;
            }
            return (ObjectPool<T>)pool;
        }

        /// <summary>
        /// 获取或创建GameObject对象池
        /// </summary>
        public GameObjectPool GetGameObjectPool(string key, GameObject prefab)
        {
            if (!_gameObjectPools.TryGetValue(key, out var pool))
            {
                pool = new GameObjectPool(prefab);
                _gameObjectPools[key] = pool;
            }
            return pool;
        }

        /// <summary>
        /// 获取或创建共享GameObject对象池
        /// </summary>
        public ISharedGameObjectPool GetSharedGameObjectPool(string key, GameObject prefab)
        {
            if (!_sharedGameObjectPools.TryGetValue(key, out var pool))
            {
                pool = new SharedGameObjectPool(prefab, transform);
                _sharedGameObjectPools[key] = pool;
            }
            return pool;
        }

        /// <summary>
        /// 预热指定类型的对象池
        /// </summary>
        public void Prewarm<T>(int count) where T : class
        {
            GetPool<T>().Prewarm(count);
        }

        /// <summary>
        /// 预热GameObject对象池
        /// </summary>
        public void PrewarmGameObjectPool(string key, int count)
        {
            if (_gameObjectPools.TryGetValue(key, out var pool))
            {
                pool.Prewarm(count);
            }
        }

        /// <summary>
        /// 清理指定类型的对象池
        /// </summary>
        public void ClearPool<T>() where T : class
        {
            if (_poolMap.TryGetValue(typeof(T), out var pool))
            {
                ((ObjectPool<T>)pool).Clear();
            }
        }

        /// <summary>
        /// 清理指定的GameObject对象池
        /// </summary>
        public void ClearGameObjectPool(string key)
        {
            if (_gameObjectPools.TryGetValue(key, out var pool))
            {
                pool.Clear();
            }
        }

        /// <summary>
        /// 清理所有对象池
        /// </summary>
        public void ClearAllPools()
        {
            foreach (var pool in _poolMap.Values)
            {
                if (pool is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _poolMap.Clear();

            foreach (var pool in _gameObjectPools.Values)
            {
                pool.Clear();
            }
            _gameObjectPools.Clear();

            foreach (var pool in _sharedGameObjectPools.Values)
            {
                pool.Clear();
            }
            _sharedGameObjectPools.Clear();
        }

        public void RegisterPrefab<T>(GameObject prefab) where T : Component
        {
            RegisterPrefab(typeof(T), prefab);
        }

        public GameObject GetPrefab(Type type)
        {
            return _prefabMap.TryGetValue(type, out var prefab) ? prefab : null;
        }

        public void RegisterPrefab(Type type, GameObject prefab)
        {
            if (!prefab.GetComponent(type))
                throw new ArgumentException($"预制体上没有找到组件 {type}");

            _prefabMap[type] = prefab;
        }

        public void RegisterResourcePath(Type type, string path)
        {
            _resourcePathMap[type] = path;
        }

        public void RegisterResourcePath<T>(string path)
        {
            RegisterResourcePath(typeof(T), path);
        }

        public string GetResourcePath(Type type)
        {
            return _resourcePathMap.TryGetValue(type, out var path) ? path : null;
        }

        public string GetResourcePath<T>()
        {
            return GetResourcePath(typeof(T));
        }

        private void OnDestroy()
        {
            ClearAllPools();
        }
    }
} 