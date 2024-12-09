using System;
using System.Collections.Generic;

namespace TBydFramework.Pool.Runtime.Base
{
    /// <summary>
    /// 对象池的抽象基类，提供了对象池的基本实现。
    /// </summary>
    /// <typeparam name="T">池中管理的对象类型</typeparam>
    public abstract class ObjectPoolBase<T> : IObjectPool<T>
        where T : class
    {
        /// <summary>
        /// 用于存储池中对象的栈。
        /// </summary>
        protected readonly Stack<T> Stack = new(32);

        /// <summary>
        /// 标记池是否已被释放。
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// 创建新实例的抽象方法。
        /// </summary>
        /// <returns>创建的新实例</returns>
        protected abstract T CreateInstance();

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
        /// 从池中租用一个对象。
        /// </summary>
        /// <returns>租用的对象</returns>
        public virtual T Rent()
        {
            ThrowIfDisposed();
            if (Stack.TryPop(out var obj))
            {
                OnRent(obj);
                if (obj is IPoolCallbackReceiver receiver) receiver.OnRent();
                return obj;
            }

            return CreateInstance();
        }

        /// <summary>
        /// 将对象归还到池中。
        /// </summary>
        /// <param name="obj">要归还的对象</param>
        public virtual void Return(T obj)
        {
            ThrowIfDisposed();
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            OnReturn(obj);
            if (obj is IPoolCallbackReceiver receiver) receiver.OnReturn();
            Stack.Push(obj);
        }

        /// <summary>
        /// 清空池中的所有对象。
        /// </summary>
        public void Clear()
        {
            ThrowIfDisposed();
            while (Stack.TryPop(out var obj))
            {
                OnDestroy(obj);
            }
        }

        /// <summary>
        /// 预热池，创建指定数量的对象并添加到池中。
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        public void Prewarm(int count)
        {
            ThrowIfDisposed();
            for (int i = 0; i < count; i++)
            {
                var instance = CreateInstance();
                Return(instance);
            }
        }

        /// <summary>
        /// 获取池中当前的对象数量。
        /// </summary>
        public int Count => Stack.Count;

        /// <summary>
        /// 获取池是否已被释放。
        /// </summary>
        public bool IsDisposed => _isDisposed;

        /// <summary>
        /// 释放池中的所有资源。
        /// </summary>
        public void Dispose()
        {
            ThrowIfDisposed();
            Clear();
            _isDisposed = true;
        }

        /// <summary>
        /// 检查池是否已被释放，如果已释放则抛出异常。
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(GetType().Name);
        }
    }
}
