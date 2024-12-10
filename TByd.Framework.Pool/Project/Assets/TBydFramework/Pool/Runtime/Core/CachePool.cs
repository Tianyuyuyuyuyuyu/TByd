using System;
using TBydFramework.Pool.Runtime.Base;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 通用缓存池实现，用于管理和缓存各种类型的数据
    /// </summary>
    public sealed class CachePool<TKey, TValue> : CachePoolBase<TKey, TValue>
    {
        private readonly Action<TKey, TValue> _onRemove;

        /// <summary>
        /// 初始化缓存池
        /// </summary>
        /// <param name="capacity">缓存池最大容量</param>
        /// <param name="defaultExpiration">默认过期时间</param>
        /// <param name="onRemove">当缓存项被移除时的回调</param>
        public CachePool(int capacity = 1000, TimeSpan? defaultExpiration = null, Action<TKey, TValue> onRemove = null) 
            : base(capacity, defaultExpiration)
        {
            _onRemove = onRemove;
        }

        protected override void OnRemove(TKey key, TValue value)
        {
            _onRemove?.Invoke(key, value);
        }
    }
} 