using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Cysharp.Threading.Tasks;
using TBydFramework.Pool.Runtime.Extensions;
using TBydFramework.Pool.Runtime.Config;
using TBydFramework.Pool.Runtime.Diagnostics;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 资源回收池，用于管理和复用Unity资源(如纹理、音频等)。
    /// </summary>
    /// <typeparam name="T">资源类型，必须继承自UnityEngine.Object</typeparam>
    public class ResourcePool<T> : IPool<T> where T : UnityEngine.Object
    {
        private readonly ObjectPool<T> _internalPool;
        private readonly string _resourcePath;
        private readonly PoolSettings _settings;
        private readonly PoolStatistics _statistics = new();

        public ResourcePool(string resourcePath, PoolSettings settings = null)
        {
            _resourcePath = resourcePath;
            _settings = settings;
            _internalPool = new ObjectPool<T>(CreateInstance, settings);
        }

        private T CreateInstance()
        {
            var startTime = Time.realtimeSinceStartup;
            var instance = Resources.Load<T>(_resourcePath);
            
            if (instance == null)
                throw new ArgumentException($"无法加载资源: {_resourcePath}");
            
            _statistics.TotalCreated++;
            _statistics.UpdateLatency(Time.realtimeSinceStartup - startTime);
            
            return instance;
        }

        public T Get()
        {
            var item = _internalPool.Get();
            _statistics.CurrentSize = Count;
            _statistics.PeakSize = Math.Max(_statistics.PeakSize, Count);
            return item;
        }

        public void Return(T item)
        {
            _internalPool.Return(item);
            _statistics.TotalReturned++;
            _statistics.CurrentSize = Count;
        }

        public void Clear()
        {
            _internalPool.Clear();
            _statistics.CurrentSize = 0;
        }

        public int Count => _internalPool.Count;
        public int Capacity => _settings?.DefaultPoolSize ?? 32;

        public void Prewarm(int count)
        {
            _internalPool.Prewarm(count);
            _statistics.CurrentSize = Count;
        }

        public PoolStatistics GetStatistics()
        {
            _statistics.UpdateUptime();
            return _statistics;
        }
    }
} 