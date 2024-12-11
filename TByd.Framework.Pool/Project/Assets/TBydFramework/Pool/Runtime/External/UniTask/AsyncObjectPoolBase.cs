#if TBYDPOOL_UNITASK_SUPPORT
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TBydFramework.Pool.Runtime.Base;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Runtime.External.UniTask
{
    /// <summary>
    /// 异步对象池的基类,提供了异步对象池的基本实现。
    /// </summary>
    /// <typeparam name="T">池中管理的对象类型</typeparam>
    public abstract class AsyncObjectPoolBase<T> : IAsyncObjectPool<T>
        where T : class
    {
        /// <summary>
        /// 用于存储池中对象的栈。
        /// </summary>
        protected readonly Stack<T> stack = new(32);

        /// <summary>
        /// 标记池是否已被释放。
        /// </summary>
        private bool isDisposed;

        /// <summary>
        /// 异步创建新实例的抽象方法。
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>创建的新实例</returns>
        protected abstract UniTask<T> CreateInstanceAsync(CancellationToken cancellationToken);

        /// <summary>
        /// 当实例被销毁时调用的虚方法。
        /// </summary>
        /// <param name="instance">被销毁的实例</param>
        protected virtual void OnDestroy(T instance) { }

        /// <summary>
        /// 当实例被租用时调用的虚方法。
        /// </summary>
        /// <param name="instance">被租用的实例</param>
        protected virtual void OnRent(T instance) { }

        /// <summary>
        /// 当实例被归还时调用的虚方法。
        /// </summary>
        /// <param name="instance">被归还的实例</param>
        protected virtual void OnReturn(T instance) { }

        /// <summary>
        /// 异步租用一个对象。
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>租用的对象</returns>
        public UniTask<T> RentAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            if (stack.TryPop(out var obj))
            {
                OnRent(obj);
                if (obj is IPoolCallbackReceiver receiver) receiver.OnRent();
                return new UniTask<T>(obj);
            }

            return CreateInstanceAsync(cancellationToken);
        }

        /// <summary>
        /// 归还一个对象到池中。
        /// </summary>
        /// <param name="obj">要归还的对象</param>
        public void Return(T obj)
        {
            ThrowIfDisposed();
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            OnReturn(obj);
            if (obj is IPoolCallbackReceiver receiver) receiver.OnReturn();
            stack.Push(obj);
        }

        /// <summary>
        /// 清空池中的所有对象。
        /// </summary>
        public void Clear()
        {
            ThrowIfDisposed();
            while (stack.TryPop(out var obj))
            {
                OnDestroy(obj);
            }
        }

        /// <summary>
        /// 异步预热池,创建指定数量的对象并添加到池中。
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        /// <param name="cancellationToken">取消令牌</param>
        public async Cysharp.Threading.Tasks.UniTask PrewarmAsync(int count, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            for (int i = 0; i < count; i++)
            {
                var instance = await CreateInstanceAsync(cancellationToken);
                Return(instance);
            }
        }

        /// <summary>
        /// 获取池中当前的对象数量。
        /// </summary>
        public int Count => stack.Count;

        /// <summary>
        /// 获取池是否已被释放。
        /// </summary>
        public bool IsDisposed => isDisposed;

        /// <summary>
        /// 释放池中的所有资源。
        /// </summary>
        public virtual void Dispose()
        {
            ThrowIfDisposed();
            Clear();
            isDisposed = true;
        }

        /// <summary>
        /// 检查池是否已被释放,如果已释放则抛出异常。
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (isDisposed) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
#endif
