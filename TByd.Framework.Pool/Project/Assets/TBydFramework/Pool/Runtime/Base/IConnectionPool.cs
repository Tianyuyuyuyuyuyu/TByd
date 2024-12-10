using System;

namespace TBydFramework.Pool.Runtime.Base
{
    /// <summary>
    /// 定义连接池的通用接口
    /// </summary>
    /// <typeparam name="TConnection">连接对象类型</typeparam>
    public interface IConnectionPool<TConnection> : IDisposable
    {
        /// <summary>
        /// 从池中获取一个可用的连接
        /// </summary>
        /// <returns>可用的连接对象</returns>
        TConnection Acquire();

        /// <summary>
        /// 将连接归还到池中
        /// </summary>
        /// <param name="connection">要归还的连接</param>
        void Release(TConnection connection);

        /// <summary>
        /// 获取池中当前的连接数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取活跃的连接数量
        /// </summary>
        int ActiveCount { get; }

        /// <summary>
        /// 获取池是否已被释放
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// 清空池中的所有连接
        /// </summary>
        void Clear();

        /// <summary>
        /// 预热池，创建指定数量的连接
        /// </summary>
        /// <param name="count">要预热的连接数量</param>
        void Prewarm(int count);
    }
} 