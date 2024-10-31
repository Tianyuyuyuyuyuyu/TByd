#if TBYDPOOL_UNITASK_SUPPORT
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace TBydFramework.Pool.Runtime.External.UniTask
{
    /// <summary>
    /// 定义异步对象池的接口。
    /// </summary>
    /// <typeparam name="T">池中管理的对象类型</typeparam>
    public interface IAsyncObjectPool<T> : IDisposable
    {
        /// <summary>
        /// 异步租用一个对象。
        /// </summary>
        /// <param name="cancellationToken">用于取消操作的令牌</param>
        /// <returns>表示异步操作的UniTask,其结果为租用的对象</returns>
        UniTask<T> RentAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 将对象归还到池中。
        /// </summary>
        /// <param name="instance">要归还的对象实例</param>
        void Return(T instance);

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
        /// 异步预热池,创建指定数量的对象并添加到池中。
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        /// <param name="cancellationToken">用于取消操作的令牌</param>
        /// <returns>表示异步操作的UniTask</returns>
        Cysharp.Threading.Tasks.UniTask PrewarmAsync(int count, CancellationToken cancellationToken = default);
    }
}
#endif
