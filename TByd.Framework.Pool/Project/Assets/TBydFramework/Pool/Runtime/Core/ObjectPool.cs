using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Config;
using TBydFramework.Pool.Runtime.Interfaces;
using TBydFramework.Pool.Runtime.Diagnostics;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Core
{
    public class ObjectPool<T> : IPool<T> where T : class
    {
        private readonly Stack<T> _stack;
        private readonly Func<T> _createFunc;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onReturn;
        private readonly PoolSettings _settings;
        private readonly PoolStatistics _statistics = new();

        public ObjectPool(Func<T> createFunc = null, PoolSettings settings = null, Action<T> onGet = null, Action<T> onReturn = null)
        {
            _createFunc = createFunc ?? (() => Activator.CreateInstance<T>());
            _settings = settings;
            _onGet = onGet;
            _onReturn = onReturn;
            _stack = new Stack<T>(_settings?.DefaultPoolSize ?? 32);
        }

        public T Get()
        {
            var startTime = Time.realtimeSinceStartup;
            T item;

            if (_stack.Count > 0)
            {
                item = _stack.Pop();
            }
            else
            {
                item = _createFunc();
                _statistics.TotalCreated++;
                _statistics.MissCount++;
            }

            _statistics.CurrentSize = Count;
            _statistics.PeakSize = Math.Max(_statistics.PeakSize, Count);
            _statistics.UpdateLatency(Time.realtimeSinceStartup - startTime);

            _onGet?.Invoke(item);
            return item;
        }

        public void Return(T item)
        {
            if (item == null) return;

            if (_settings != null && Count >= _settings.DefaultPoolSize)
                return;

            _onReturn?.Invoke(item);
            _stack.Push(item);
            
            _statistics.TotalReturned++;
            _statistics.CurrentSize = Count;
        }

        public void Clear()
        {
            _stack.Clear();
            _statistics.CurrentSize = 0;
        }

        public int Count => _stack.Count;

        public int Capacity => _settings?.DefaultPoolSize ?? 32;

        public void Prewarm(int count)
        {
            var targetCount = _settings != null ? Math.Min(count, _settings.DefaultPoolSize - Count) : count;
            for (int i = 0; i < targetCount; i++)
            {
                var item = _createFunc();
                _statistics.TotalCreated++;
                Return(item);
            }
        }

        public PoolStatistics GetStatistics()
        {
            _statistics.UpdateUptime();
            return _statistics;
        }
    }
}
