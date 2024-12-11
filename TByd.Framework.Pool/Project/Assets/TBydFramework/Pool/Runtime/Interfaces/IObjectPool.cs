using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace TBydFramework.Pool.Runtime.Interfaces
{
    /// <summary>
    /// 定义对象池的通用接口。
    /// </summary>
    /// <typeparam name="T">池中管理的对象类型</typeparam>
    public interface IObjectPool<T> : IDisposable where T : class
    {
        /// <summary>
        /// 从池中租用一个对象。
        /// </summary>
        /// <returns>租用的对象</returns>
        T Rent();

        /// <summary>
        /// 将对象归还到池中。
        /// </summary>
        /// <param name="obj">要归还的对象</param>
        void Return(T obj);

        /// <summary>
        /// 获取池中当前的对象数量。
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取池是否已被释放。
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// 清空池中的所有对象。
        /// </summary>
        void Clear();

        /// <summary>
        /// 预热池，创建指定数量的对象并添加到池中。
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        void Prewarm(int count);
    }

    // 建议添加异步接口
    public interface IAsyncObjectPool<T> : IObjectPool<T> where T : class
    {
        UniTask<T> RentAsync(CancellationToken cancellationToken = default);
        UniTask PrewarmAsync(int count, CancellationToken cancellationToken = default); 
    }
}
