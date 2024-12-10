using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Base;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 事件池，用于管理和复用事件参数对象
    /// </summary>
    /// <typeparam name="TEventArgs">事件参数类型</typeparam>
    public sealed class EventPool<TEventArgs> : IEventPool<TEventArgs> where TEventArgs : EventArgs, new()
    {
        private readonly Stack<TEventArgs> _pool;
        private readonly Action<TEventArgs> _resetAction;
        private readonly int _maxSize;
        private bool _isDisposed;

        /// <summary>
        /// 初始化事件池
        /// </summary>
        /// <param name="maxSize">池的最大容量</param>
        /// <param name="resetAction">重置事件参数的操作</param>
        public EventPool(int maxSize = 32, Action<TEventArgs> resetAction = null)
        {
            _pool = new Stack<TEventArgs>(maxSize);
            _maxSize = maxSize;
            _resetAction = resetAction;
        }

        /// <summary>
        /// 获取池中当前的对象数量
        /// </summary>
        public int Count => _pool.Count;

        /// <summary>
        /// 从池中获取一个事件参数对象
        /// </summary>
        public TEventArgs Get()
        {
            ThrowIfDisposed();

            if (_pool.Count > 0)
            {
                return _pool.Pop();
            }

            return new TEventArgs();
        }

        /// <summary>
        /// 将事件参数对象归还到池中
        /// </summary>
        public void Return(TEventArgs args)
        {
            ThrowIfDisposed();
            
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            if (_pool.Count >= _maxSize)
                return;

            _resetAction?.Invoke(args);
            _pool.Push(args);
        }

        /// <summary>
        /// 清空池中的所有对象
        /// </summary>
        public void Clear()
        {
            ThrowIfDisposed();
            _pool.Clear();
        }

        /// <summary>
        /// 预热池，创建指定数量的对象
        /// </summary>
        public void Prewarm(int count)
        {
            ThrowIfDisposed();
            
            for (int i = 0; i < count && _pool.Count < _maxSize; i++)
            {
                _pool.Push(new TEventArgs());
            }
        }

        /// <summary>
        /// 释放事件池资源
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed) return;
            
            Clear();
            _isDisposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(GetType().Name);
        }
    }
} 