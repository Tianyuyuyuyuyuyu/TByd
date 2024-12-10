using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Base;
using TBydFramework.Pool.Runtime.Internal;
using TBydFramework.Pool.Runtime.Exceptions;
using UnityEngine;
using TBydFramework.Pool.Runtime.Config;
using TBydFramework.Pool.Runtime.Interfaces;
using TBydFramework.Pool.Runtime.Diagnostics;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// GameObject对象池，用于管理和复用GameObject实例。
    /// </summary>
    public sealed class GameObjectPool : IPool<GameObject>, IDisposable
    {
        private readonly GameObject _original;
        private readonly Stack<GameObject> _stack = new(32);
        private readonly PoolSettings _settings;
        private bool _isDisposed;
        private readonly PoolStatistics _statistics = new();

        /// <summary>
        /// 初始化GameObject对象池。
        /// </summary>
        /// <param name="original">用于创建新实例的原始GameObject</param>
        /// <param name="settings">池配置</param>
        public GameObjectPool(GameObject original, PoolSettings settings = null)
        {
            _original = original ? original : throw new ArgumentNullException(nameof(original));
            _settings = settings;
        }

        /// <summary>
        /// 获取池中当前的对象数量。
        /// </summary>
        public int Count => _stack.Count;

        /// <summary>
        /// 获取池的容量。
        /// </summary>
        public int Capacity => _settings?.DefaultPoolSize ?? 32;

        /// <summary>
        /// 从池中获取一个GameObject实例。
        /// </summary>
        public GameObject Get()
        {
            ThrowIfDisposed();

            var startTime = Time.realtimeSinceStartup;
            GameObject obj;
            
            if (_stack.Count > 0)
            {
                obj = _stack.Pop();
            }
            else
            {
                obj = UnityEngine.Object.Instantiate(_original);
                _statistics.TotalCreated++;
                _statistics.MissCount++;
                PoolCallbackHelper.InvokeOnCreate(obj);
            }

            _statistics.CurrentSize = Count;
            _statistics.PeakSize = Math.Max(_statistics.PeakSize, Count);
            _statistics.UpdateLatency(Time.realtimeSinceStartup - startTime);
            
            obj.SetActive(true);
            PoolCallbackHelper.InvokeOnGet(obj);
            return obj;
        }

        /// <summary>
        /// 将GameObject实例返回到池中。
        /// </summary>
        /// <param name="obj">要返回的GameObject实例</param>
        public void Return(GameObject obj)
        {
            if (obj == null) return;
            ThrowIfDisposed();

            if (_settings != null && Count >= _settings.DefaultPoolSize)
            {
                UnityEngine.Object.Destroy(obj);
                PoolCallbackHelper.InvokeOnDestroy(obj);
                return;
            }

            _stack.Push(obj);
            obj.SetActive(false);
            PoolCallbackHelper.InvokeOnReturn(obj);
            
            _statistics.TotalReturned++;
            _statistics.CurrentSize = Count;
        }

        /// <summary>
        /// 清空池中的所有对象。
        /// </summary>
        public void Clear()
        {
            ThrowIfDisposed();
            
            while (_stack.Count > 0)
            {
                var obj = _stack.Pop();
                if (obj != null)
                {
                    PoolCallbackHelper.InvokeOnDestroy(obj);
                    UnityEngine.Object.Destroy(obj);
                }
            }
        }

        /// <summary>
        /// 预热池，创建指定数量的对象并添加到池中。
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        public void Prewarm(int count)
        {
            ThrowIfDisposed();

            // 检查预热数量是否超出最大容量
            if (_settings != null)
            {
                count = Mathf.Min(count, _settings.DefaultPoolSize - Count);
            }

            for (int i = 0; i < count; i++)
            {
                var obj = UnityEngine.Object.Instantiate(_original);
                _stack.Push(obj);
                obj.SetActive(false);
                PoolCallbackHelper.InvokeOnReturn(obj);
            }
        }

        /// <summary>
        /// 释放池中的��有资源。
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed) return;
            
            Clear();
            _isDisposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(GameObjectPool));
            }
        }

        public PoolStatistics GetStatistics()
        {
            _statistics.UpdateUptime();
            return _statistics;
        }
    }
}
