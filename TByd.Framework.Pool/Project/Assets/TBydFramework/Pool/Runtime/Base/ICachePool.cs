using System;

namespace TBydFramework.Pool.Runtime.Base
{
    /// <summary>
    /// 定义缓存池的通用接口
    /// </summary>
    /// <typeparam name="TKey">缓存键类型</typeparam>
    /// <typeparam name="TValue">缓存值类型</typeparam>
    public interface ICachePool<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// 获取或添加缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="valueFactory">当缓存未命中时用于创建值的工厂方法</param>
        /// <returns>缓存的值</returns>
        TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory);

        /// <summary>
        /// 尝试从缓存中获取值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">获取到的值</param>
        /// <returns>是否成功获取值</returns>
        bool TryGet(TKey key, out TValue value);

        /// <summary>
        /// 添加或更新缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">要缓存的值</param>
        void Set(TKey key, TValue value);

        /// <summary>
        /// 从缓存中移除指定项
        /// </summary>
        /// <param name="key">要移除的缓存键</param>
        /// <returns>是否成功移除</returns>
        bool Remove(TKey key);

        /// <summary>
        /// 获取缓存中的项数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取缓存池的最大容量
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// 清空缓存池
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取缓存项是否存在
        /// </summary>
        bool Contains(TKey key);
    }
} 