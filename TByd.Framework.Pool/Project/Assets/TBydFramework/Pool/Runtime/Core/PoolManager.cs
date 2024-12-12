using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;
using TBydFramework.Pool.Runtime.Interfaces;
using TBydFramework.Pool.Runtime.Config;
using System.Threading.Tasks;
using TBydFramework.Pool.Runtime.Diagnostics;
using TBydFramework.Pool.Runtime.External;
using Cysharp.Threading.Tasks;
using TBydFramework.Pool.Runtime.Enums;
using TBydFramework.Pool.Runtime.Internal;

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

        private readonly Dictionary<string, AddressableGameObjectPool> _addressablePools = new();

        // 添加配置
        [Serializable]
        public class PoolConfig
        {
            public int DefaultCapacity = 32;
            public int MaxSize = 1000;
            public float CleanupInterval = 60f;
        }
        
        [SerializeField] private PoolConfig _config = new PoolConfig();

        [SerializeField] private PoolSettings _settings = ScriptableObject.CreateInstance<PoolSettings>();

        private PoolDiagnosticsManager _diagnosticsManager;

        private bool _autoSaveEnabled;
        private float _autoSaveInterval;
        private float _lastSaveTime;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            
            // 将 PoolConfig 的值同步到 PoolSettings
            _settings.DefaultPoolSize = _config.DefaultCapacity;
            _settings.MaxPoolSize = _config.MaxSize;
            _settings.MaintenanceInterval = _config.CleanupInterval;
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
                pool = new GameObjectPool(prefab, _settings);
                _gameObjectPools[key] = pool;
                
                // 应用配置文件
                var profile = _settings.GetProfile(key);
                if (profile != null)
                {
                    ApplyPoolProfile(pool, profile);
                }
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

        private async UniTask PrewarmPoolsAsync()
        {
            var settings = PoolConfigManager.GetSettings();
            if (settings == null) return;

            foreach (var profile in settings.GetAllProfiles())
            {
                if (!profile.EnablePrewarm) continue;

                try
                {
                    var pool = GetGameObjectPool(profile.Key, profile.Prefab);
                    if (pool != null)
                    {
                        await PoolPrewarmSystem.PrewarmPoolAsync(pool, profile.InitialCapacity);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to prewarm pool {profile.Key}: {e.Message}");
                }
            }
        }

        private async UniTask PrewarmAddressablePoolAsync(string key, int count)
        {
            if (_addressablePools.TryGetValue(key, out var pool))
            {
                try
                {
                    await PoolPrewarmSystem.PrewarmPoolAsync(pool, count);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to prewarm addressable pool {key}: {e.Message}");
                }
            }
        }

        private void InitializeDiagnostics()
        {
            if (_diagnosticsManager == null)
            {
                var go = new GameObject("[PoolDiagnostics]");
                go.transform.SetParent(transform);
                _diagnosticsManager = go.AddComponent<PoolDiagnosticsManager>();
            }
        }

        public void EnableDiagnostics(bool enable)
        {
            if (enable)
            {
                InitializeDiagnostics();
            }
            else if (_diagnosticsManager != null)
            {
                Destroy(_diagnosticsManager.gameObject);
                _diagnosticsManager = null;
            }
        }

        public PoolDiagnosticInfo GetPoolDiagnostics(string poolKey)
        {
            return PoolDiagnosticsManager.GetPoolDiagnostics(poolKey);
        }

        public IReadOnlyList<PoolDiagnosticsManager.DiagnosticEvent> GetDiagnosticEvents()
        {
            return PoolDiagnosticsManager.GetEventLog();
        }

        public async UniTask<AddressableGameObjectPool> GetAddressablePoolAsync(string poolName, string key)
        {
            if (!_addressablePools.TryGetValue(poolName, out var pool))
            {
                var settings = PoolConfigManager.GetSettings();
                var profile = settings?.GetProfile(poolName);
                int maxSize = profile?.MaxSize ?? _config.MaxSize;
                pool = new AddressableGameObjectPool(key, maxSize);
                _addressablePools[poolName] = pool;
                await pool.InitializeAsync();
            }
            return pool;
        }

        public async UniTask<GameObject> GetAddressableAsync(string poolName, string key)
        {
            var pool = await GetAddressablePoolAsync(poolName, key);
            return await pool.GetAsync();
        }

        public void ReturnAddressable(string key, GameObject obj)
        {
            if (_addressablePools.TryGetValue(key, out var pool))
            {
                pool.Return(obj);
            }
        }

        public void EnableAutoSave(bool enable, float interval = 300f)
        {
            _autoSaveEnabled = enable;
            _autoSaveInterval = interval;
        }

        private void ApplyPoolProfile(GameObjectPool pool, PoolSettings.PoolProfile profile)
        {
            if (profile == null || pool == null) return;
            
            pool.MaxSize = profile.MaxSize;
            if (profile.EnablePrewarm)
            {
                pool.Prewarm(profile.PrewarmSize);
            }
        }

        public async UniTask<bool> SavePoolStatesAsync()
        {
            var states = new Dictionary<string, PoolState>();
            foreach (var pair in _gameObjectPools)
            {
                var profile = _settings.GetProfile(pair.Key);
                states[pair.Key] = new PoolState
                {
                    Count = pair.Value.Count,
                    MaxSize = pair.Value.MaxSize,
                    Type = pair.Value.Type,
                    EnablePrewarm = profile?.EnablePrewarm ?? false,
                    PrewarmSize = profile?.PrewarmSize ?? 0
                };
            }
            return await SaveStates(states);
        }

        private async UniTask<bool> SaveStates(Dictionary<string, PoolState> states)
        {
            try
            {
                // 实现保存逻辑
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save pool states: {e.Message}");
                return false;
            }
        }

        private async UniTask<Dictionary<string, PoolState>> LoadStates()
        {
            try
            {
                // 实现加载逻辑
                return new Dictionary<string, PoolState>();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load pool states: {e.Message}");
                return null;
            }
        }

        public async UniTask<bool> LoadPoolStatesAsync()
        {
            var states = await LoadStates();
            if (states == null) return false;

            foreach (var pair in states)
            {
                if (_gameObjectPools.TryGetValue(pair.Key, out var pool))
                {
                    pool.MaxSize = pair.Value.MaxSize;
                    if (pair.Value.EnablePrewarm)
                    {
                        pool.Prewarm(pair.Value.PrewarmSize);
                    }
                }
            }
            return true;
        }

        private void Update()
        {
            if (_autoSaveEnabled && Time.time - _lastSaveTime >= _autoSaveInterval)
            {
                _ = SavePoolStatesAsync();
                _lastSaveTime = Time.time;
            }
        }
    }
} 