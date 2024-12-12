#if TBYD_ADDRESSABLES_SUPPORT
using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Interfaces;
using TBydFramework.Pool.Runtime.Internal;
using UnityEngine;
using UnityEngine.AddressableAssets;
using TBydFramework.Pool.Runtime.Diagnostics;
using Cysharp.Threading.Tasks;
using TBydFramework.Pool.Runtime.Enums;

namespace TBydFramework.Pool.Runtime.External
{
    /// <summary>
    /// Addressable资源对象池,用于管理通过Addressables加载的GameObject实例。
    /// </summary>
    public sealed class AddressableGameObjectPool : IObjectPool<GameObject>, IPoolDiagnostics, IPoolInfo
    {
        private readonly object _key;
        private readonly Stack<GameObject> _stack = new(32);
        private readonly HashSet<GameObject> _activeObjects = new();
        private bool _isDisposed;
        private readonly PoolStatistics _statistics = new();
        private PoolDiagnosticInfo _diagnosticInfo = new();
        private readonly System.Diagnostics.Stopwatch _stopwatch = new();
        private GameObject _prefab;
        private bool _isInitialized;

        public string Name => _key.ToString();
        public int ActiveCount => _activeObjects.Count;
        public int Capacity { get; private set; }
        public int MaxSize { get; }
        public PoolType Type => PoolType.Addressable;

        /// <summary>
        /// 使用资源key初始化Addressable对象池。
        /// </summary>
        /// <param name="key">Addressable资源的key</param>
        /// <param name="maxSize">对象池的最大容量</param>
        public AddressableGameObjectPool(object key, int maxSize = 100)
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));
            MaxSize = maxSize;
            Capacity = maxSize;
        }

        /// <summary>
        /// 使用AssetReference初始化Addressable对象池。
        /// </summary>
        /// <param name="reference">GameObject的AssetReference</param>
        public AddressableGameObjectPool(AssetReferenceGameObject reference)
        {
            if (reference == null) throw new ArgumentNullException(nameof(reference));
            _key = reference.RuntimeKey;
        }

        /// <summary>
        /// 获取池中当前的对象数量。
        /// </summary>
        public int Count => _stack.Count;

        /// <summary>
        /// 获取池是否已被释放。
        /// </summary>
        public bool IsDisposed => _isDisposed;

        /// <summary>
        /// 异步初始化对象池
        /// </summary>
        public async UniTask InitializeAsync()
        {
            if (_isInitialized) return;
            _prefab = await Addressables.LoadAssetAsync<GameObject>(_key).Task;
            _isInitialized = true;
        }

        /// <summary>
        /// 异步获取对象
        /// </summary>
        public async UniTask<GameObject> GetAsync()
        {
            if (!_isInitialized)
                await InitializeAsync();
            return Rent();
        }

        /// <summary>
        /// 从池中租用一个GameObject。
        /// </summary>
        /// <returns>租用的GameObject</returns>
        public GameObject Rent()
        {
            ThrowIfDisposed();
            _stopwatch.Restart();

            GameObject obj;
            if (!_stack.TryPop(out obj))
            {
                obj = Addressables.InstantiateAsync(_key).WaitForCompletion();
                _statistics.TotalCreated++;
                _diagnosticInfo.TotalCreated++;
            }
            else
            {
                obj.SetActive(true);
            }

            OnRent(obj);
            return obj;
        }

        /// <summary>
        /// 从池中租用一个GameObject并设置其父级。
        /// </summary>
        /// <param name="parent">父级Transform</param>
        /// <returns>租用的GameObject</returns>
        public GameObject Rent(Transform parent)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = Addressables.InstantiateAsync(_key, parent).WaitForCompletion();
            }
            else
            {
                obj.transform.SetParent(parent);
                obj.SetActive(true);
            }

            OnRent(obj);
            return obj;
        }

        /// <summary>
        /// 从池中租用一个GameObject并设置其位置和旋转。
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <returns>租用的GameObject</returns>
        public GameObject Rent(Vector3 position, Quaternion rotation)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = Addressables.InstantiateAsync(_key, position, rotation).WaitForCompletion();
            }
            else
            {
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);
            }

            OnRent(obj);
            return obj;
        }

        /// <summary>
        /// 从池中租用一个GameObject并设置其位置、旋转和父级。
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <param name="parent">父级Transform</param>
        /// <returns>租用的GameObject</returns>
        public GameObject Rent(Vector3 position, Quaternion rotation, Transform parent)
        {
            ThrowIfDisposed();

            if (!_stack.TryPop(out var obj))
            {
                obj = Addressables.InstantiateAsync(_key, position, rotation, parent).WaitForCompletion();
            }
            else
            {
                obj.transform.SetParent(parent);
                obj.transform.SetPositionAndRotation(position, rotation);
                obj.SetActive(true);
            }

            OnRent(obj);
            return obj;
        }

        /// <summary>
        /// 将GameObject归还到池中。
        /// </summary>
        /// <param name="obj">要归还的GameObject</param>
        public void Return(GameObject obj)
        {
            ThrowIfDisposed();
            if (obj == null || !_activeObjects.Contains(obj)) return;

            _stopwatch.Restart();

            _stack.Push(obj);
            obj.SetActive(false);
            _activeObjects.Remove(obj);
            OnReturn(obj);

            _statistics.TotalReturned++;
            _stopwatch.Stop();
            UpdateReturnDiagnostics();
        }

        /// <summary>
        /// 清空池中的所有对象。
        /// </summary>
        public void Clear()
        {
            ThrowIfDisposed();
            
            foreach (var obj in _activeObjects)
            {
                if (obj != null)
                {
                    Addressables.ReleaseInstance(obj);
                }
            }
            _activeObjects.Clear();

            while (_stack.TryPop(out var obj))
            {
                Addressables.ReleaseInstance(obj);
            }

            _statistics.Reset();
        }

        /// <summary>
        /// 预热池,创建指定数量的对象并添加到池中。
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        public void Prewarm(int count)
        {
            ThrowIfDisposed();

            count = Math.Min(count, MaxSize - Count);
            for (int i = 0; i < count; i++)
            {
                var obj = Addressables.InstantiateAsync(_key).WaitForCompletion();
                _stack.Push(obj);
                obj.SetActive(false);
                OnReturn(obj);
                _statistics.TotalCreated++;
            }
        }

        /// <summary>
        /// 释放池中的所有资源。
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed) return;
            Clear();
            _isDisposed = true;
        }

        public PoolDiagnosticInfo GetDiagnosticInfo() => _diagnosticInfo;

        public void ResetStatistics()
        {
            _diagnosticInfo = new PoolDiagnosticInfo();
            _statistics.Reset();
        }

        public PoolStatistics GetStatistics()
        {
            _statistics.UpdateUptime();
            return _statistics;
        }

        private void UpdateGetDiagnostics()
        {
            var time = _stopwatch.ElapsedMilliseconds;
            var newDiagnostics = new PoolDiagnosticInfo
            {
                AverageGetTime = (_diagnosticInfo.AverageGetTime + time) * 0.5f,
                CurrentActive = _activeObjects.Count,
                CurrentInactive = Count,
                PeakActive = Math.Max(_diagnosticInfo.PeakActive, _activeObjects.Count),
                LastAccessTime = DateTime.Now
            };
            _diagnosticInfo = newDiagnostics;
            PoolDiagnosticsManager.RecordDiagnostics(Name, _diagnosticInfo);
        }

        private void UpdateReturnDiagnostics()
        {
            var time = _stopwatch.ElapsedMilliseconds;
            var newDiagnostics = new PoolDiagnosticInfo
            {
                AverageReturnTime = (_diagnosticInfo.AverageReturnTime + time) * 0.5f,
                CurrentActive = _activeObjects.Count,
                CurrentInactive = Count,
                LastAccessTime = DateTime.Now,
                PeakActive = _diagnosticInfo.PeakActive
            };
            _diagnosticInfo = newDiagnostics;
            PoolDiagnosticsManager.RecordDiagnostics(Name, _diagnosticInfo);
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }

        private void OnRent(GameObject obj)
        {
            // 实现对象租用时的回调逻辑
        }

        private void OnReturn(GameObject obj)
        {
            // 实现对象返回时的回调逻辑
        }
    }
}
#endif 