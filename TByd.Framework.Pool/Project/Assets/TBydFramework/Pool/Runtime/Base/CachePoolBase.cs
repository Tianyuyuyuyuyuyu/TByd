using System;
using System.Collections.Generic;
using System.Linq;

namespace TBydFramework.Pool.Runtime.Base
{
    /// <summary>
    /// 缓存池的抽象基类，提供缓存池的基本实现
    /// </summary>
    public abstract class CachePoolBase<TKey, TValue> : ICachePool<TKey, TValue>
    {
        protected readonly Dictionary<TKey, CacheItem> Cache;
        protected readonly object SyncRoot = new object();
        
        private readonly int _capacity;
        private readonly TimeSpan _defaultExpiration;
        private bool _isDisposed;

        protected class CacheItem
        {
            public TValue Value { get; set; }
            public DateTime LastAccessed { get; set; }
            public DateTime ExpirationTime { get; set; }
        }

        protected CachePoolBase(int capacity = 1000, TimeSpan? defaultExpiration = null)
        {
            _capacity = capacity;
            _defaultExpiration = defaultExpiration ?? TimeSpan.FromMinutes(30);
            Cache = new Dictionary<TKey, CacheItem>(capacity);
            
            StartCleanupTimer();
        }

        public virtual TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            ThrowIfDisposed();
            
            lock (SyncRoot)
            {
                if (TryGet(key, out var value))
                {
                    return value;
                }

                value = valueFactory(key);
                Set(key, value);
                return value;
            }
        }

        public virtual bool TryGet(TKey key, out TValue value)
        {
            ThrowIfDisposed();
            
            lock (SyncRoot)
            {
                if (Cache.TryGetValue(key, out var item) && !IsExpired(item))
                {
                    item.LastAccessed = DateTime.UtcNow;
                    value = item.Value;
                    return true;
                }

                value = default;
                return false;
            }
        }

        public virtual void Set(TKey key, TValue value)
        {
            ThrowIfDisposed();
            
            lock (SyncRoot)
            {
                EnsureCapacity();
                
                var item = new CacheItem
                {
                    Value = value,
                    LastAccessed = DateTime.UtcNow,
                    ExpirationTime = DateTime.UtcNow.Add(_defaultExpiration)
                };

                Cache[key] = item;
            }
        }

        public virtual bool Remove(TKey key)
        {
            ThrowIfDisposed();
            
            lock (SyncRoot)
            {
                if (Cache.TryGetValue(key, out var item))
                {
                    OnRemove(key, item.Value);
                    return Cache.Remove(key);
                }
                return false;
            }
        }

        public int Count => Cache.Count;
        
        public int Capacity => _capacity;

        public virtual void Clear()
        {
            ThrowIfDisposed();
            
            lock (SyncRoot)
            {
                foreach (var kvp in Cache)
                {
                    OnRemove(kvp.Key, kvp.Value.Value);
                }
                Cache.Clear();
            }
        }

        public bool Contains(TKey key)
        {
            ThrowIfDisposed();
            
            lock (SyncRoot)
            {
                return Cache.ContainsKey(key) && !IsExpired(Cache[key]);
            }
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            
            Clear();
            _cleanupTimer?.Dispose();
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

        protected virtual void OnRemove(TKey key, TValue value)
        {
            // 子类可以重写此方法以在移除缓存项时执行清理
        }

        protected void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }

        private bool IsExpired(CacheItem item)
        {
            return DateTime.UtcNow > item.ExpirationTime;
        }

        private void EnsureCapacity()
        {
            if (Cache.Count >= _capacity)
            {
                // 移除最久未访问的项
                var oldest = Cache.OrderBy(x => x.Value.LastAccessed).First();
                OnRemove(oldest.Key, oldest.Value.Value);
                Cache.Remove(oldest.Key);
            }
        }

        private System.Threading.Timer _cleanupTimer;
        
        private void StartCleanupTimer()
        {
            _cleanupTimer = new System.Threading.Timer(CleanupCallback, null, 
                TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        }

        private void CleanupCallback(object state)
        {
            if (_isDisposed) return;

            try
            {
                RemoveExpiredItems();
            }
            catch (Exception)
            {
                // 记录日志但不抛出异常
            }
        }

        private void RemoveExpiredItems()
        {
            lock (SyncRoot)
            {
                var expiredKeys = Cache.Where(x => IsExpired(x.Value))
                    .Select(x => x.Key)
                    .ToList();

                foreach (var key in expiredKeys)
                {
                    Remove(key);
                }
            }
        }
    }
} 