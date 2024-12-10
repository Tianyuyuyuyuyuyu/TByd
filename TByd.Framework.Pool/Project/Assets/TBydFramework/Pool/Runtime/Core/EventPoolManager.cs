using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Base;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 事件池管理器，用于全局管理不同类型的事件池
    /// </summary>
    public class EventPoolManager : MonoBehaviour
    {
        private static EventPoolManager _instance;
        private readonly Dictionary<Type, object> _eventPools = new();
        
        public static EventPoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("[EventPoolManager]");
                    _instance = go.AddComponent<EventPoolManager>();
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
        /// 获取指定类型的事件池
        /// </summary>
        public EventPool<T> GetPool<T>(int maxSize = 32, Action<T> resetAction = null) where T : EventArgs, new()
        {
            var type = typeof(T);
            if (!_eventPools.TryGetValue(type, out var pool))
            {
                pool = new EventPool<T>(maxSize, resetAction);
                _eventPools[type] = pool;
            }
            return (EventPool<T>)pool;
        }

        /// <summary>
        /// 清理指定类型的事件池
        /// </summary>
        public void ClearPool<T>() where T : EventArgs
        {
            if (_eventPools.TryGetValue(typeof(T), out var pool))
            {
                ((IEventPool<T>)pool).Clear();
            }
        }

        /// <summary>
        /// 清理所有事件池
        /// </summary>
        public void ClearAllPools()
        {
            foreach (var pool in _eventPools.Values)
            {
                if (pool is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _eventPools.Clear();
        }

        private void OnDestroy()
        {
            ClearAllPools();
        }
    }
} 