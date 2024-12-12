using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Base;
using TBydFramework.Pool.Runtime.Internal;
using TBydFramework.Pool.Runtime.Exceptions;
using UnityEngine;
using TBydFramework.Pool.Runtime.Config;
using TBydFramework.Pool.Runtime.Interfaces;
using TBydFramework.Pool.Runtime.Diagnostics;
using TBydFramework.Pool.Runtime.Enums;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// GameObject对象池，用于管理和复用GameObject实例。
    /// </summary>
    public class GameObjectPool : IPoolInfo, IPool<GameObject>
    {
        private readonly GameObject _prefab;
        private readonly Stack<GameObject> _inactiveObjects = new();
        private readonly HashSet<GameObject> _activeObjects = new();
        private int _maxSize = 100;
        private int _initialCapacity = 32;
        private readonly PoolSettings _settings;
        private readonly PoolStatistics _statistics = new();

        public GameObjectPool(GameObject prefab, PoolSettings settings)
        {
            _prefab = prefab;
            _settings = settings;
            
            if (settings != null)
            {
                _maxSize = settings.DefaultPoolSize;
                _initialCapacity = settings.PrewarmSize;
            }
            
            if (_initialCapacity > 0)
            {
                Prewarm(_initialCapacity);
            }
        }

        public string Name => _prefab.name;
        public PoolType Type => PoolType.GameObject;
        public int Count => _inactiveObjects.Count;
        public int ActiveCount => _activeObjects.Count;
        public int MaxSize 
        { 
            get => _maxSize;
            set => _maxSize = Mathf.Max(1, value);
        }
        public int Capacity 
        { 
            get => _initialCapacity;
            set => _initialCapacity = Mathf.Max(0, value);
        }

        public PoolStatistics GetStatistics()
        {
            _statistics.UpdateUptime();
            _statistics.ActiveCount = ActiveCount;
            _statistics.InactiveCount = Count;
            _statistics.TotalSize = ActiveCount + Count;
            return _statistics;
        }

        public GameObject Get()
        {
            if (_inactiveObjects.Count > 0)
            {
                var obj = _inactiveObjects.Pop();
                _activeObjects.Add(obj);
                obj.SetActive(true);
                _statistics.IncrementGet();
                return obj;
            }

            if (_activeObjects.Count >= MaxSize)
            {
                return null;
            }

            var newObj = GameObject.Instantiate(_prefab);
            _activeObjects.Add(newObj);
            _statistics.IncrementCreated();
            _statistics.IncrementGet();
            return newObj;
        }

        public void Return(GameObject obj)
        {
            if (obj == null || !_activeObjects.Contains(obj)) return;

            obj.SetActive(false);
            _activeObjects.Remove(obj);
            _inactiveObjects.Push(obj);
            _statistics.IncrementReturn();
        }

        public void Clear()
        {
            foreach (var obj in _inactiveObjects)
            {
                if (obj != null)
                {
                    GameObject.Destroy(obj);
                }
            }
            _inactiveObjects.Clear();

            foreach (var obj in _activeObjects)
            {
                if (obj != null)
                {
                    GameObject.Destroy(obj);
                }
            }
            _activeObjects.Clear();
        }

        public void Prewarm(int count)
        {
            count = Mathf.Min(count, MaxSize);
            for (int i = 0; i < count; i++)
            {
                var obj = GameObject.Instantiate(_prefab);
                obj.SetActive(false);
                _inactiveObjects.Push(obj);
            }
        }

        public void Cleanup()
        {
            var objectsToRemove = new List<GameObject>();
            foreach (var obj in _inactiveObjects)
            {
                if (obj == null)
                {
                    objectsToRemove.Add(obj);
                }
            }

            foreach (var obj in objectsToRemove)
            {
                _inactiveObjects.Pop();
            }
        }
    }
}
