using System;
using TBydFramework.Pool.Runtime.Base;

namespace TBydFramework.Pool.Runtime.Core
{
    /// <summary>
    /// 通用对象池类，用于管理和复用对象实例。
    /// </summary>
    /// <typeparam name="T">池中管理的对象类型</typeparam>
    public sealed class ObjectPool<T> : ObjectPoolBase<T>
        where T : class
    {
        private readonly Func<T> _createFunc;
        private readonly Action<T> _onRent;
        private readonly Action<T> _onReturn;
        private readonly Action<T> _onDestroy;
        private readonly int _maxSize;

        private int _totalCreated;
        private int _maxInUse;
        private int _currentInUse;

        /// <summary>
        /// 初始化对象池。
        /// </summary>
        /// <param name="createFunc">用于创建新实例的函数</param>
        /// <param name="onRent">当对象被租用时调用的操作</param>
        /// <param name="onReturn">当对象被归还时调用的操作</param>
        /// <param name="onDestroy">当对象被销毁时调用的操作</param>
        /// <param name="maxSize">池的最大大小，默认为int.MaxValue</param>
        public ObjectPool(Func<T> createFunc, Action<T> onRent = null, Action<T> onReturn = null, Action<T> onDestroy = null, int maxSize = int.MaxValue)
            : base()
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _onRent = onRent;
            _onReturn = onReturn;
            _onDestroy = onDestroy;
            _maxSize = maxSize;
        }

        /// <summary>
        /// 创建新的实例。
        /// </summary>
        /// <returns>创建的新实例</returns>
        protected override T CreateInstance()
        {
            _totalCreated++;
            return _createFunc();
        }

        /// <summary>
        /// 当实例被销毁时调用。
        /// </summary>
        /// <param name="instance">被销毁的实例</param>
        protected override void OnDestroy(T instance)
        {
            _onDestroy?.Invoke(instance);
        }

        /// <summary>
        /// 当实例被租用时调用。
        /// </summary>
        /// <param name="instance">被租用的实例</param>
        protected override void OnRent(T instance)
        {
            _currentInUse++;
            _maxInUse = Math.Max(_maxInUse, _currentInUse);
            _onRent?.Invoke(instance);
        }

        /// <summary>
        /// 当实例被归还时调用。
        /// </summary>
        /// <param name="instance">被归还的实例</param>
        protected override void OnReturn(T instance)
        {
            _currentInUse--;
            _onReturn?.Invoke(instance);
        }

        /// <summary>
        /// 从池中租用一个对象。
        /// </summary>
        /// <returns>租用的对象</returns>
        public override T Rent()
        {
            var instance = base.Rent();
            OnRent(instance);
            return instance;
        }

        /// <summary>
        /// 将对象归还到池中。
        /// </summary>
        /// <param name="obj">要归还的对象</param>
        public override void Return(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            OnReturn(obj);
            if (Stack.Count < _maxSize)
            {
                base.Return(obj);
            }
            else
            {
                OnDestroy(obj);
            }
        }

        /// <summary>
        /// 获取池的统计信息。
        /// </summary>
        /// <returns>池的统计信息</returns>
        public PoolStatistics GetStatistics() => new PoolStatistics
        {
            TotalCreated = _totalCreated,
            MaxInUse = _maxInUse,
            CurrentInUse = _currentInUse,
            AvailableInPool = Count
        };
    }

    /// <summary>
    /// 池的统计信息结构。
    /// </summary>
    public struct PoolStatistics
    {
        public int TotalCreated { get; set; }
        public int MaxInUse { get; set; }
        public int CurrentInUse { get; set; }
        public int AvailableInPool { get; set; }
    }
}
