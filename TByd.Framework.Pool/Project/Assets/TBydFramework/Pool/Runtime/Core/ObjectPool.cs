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
        private readonly Func<T, bool> _validateFunc;

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
        /// <param name="validateFunc">用于验证对象有效性的函数，默认为null</param>
        public ObjectPool(
            Func<T> createFunc, 
            Action<T> onRent = null, 
            Action<T> onReturn = null, 
            Action<T> onDestroy = null, 
            int maxSize = int.MaxValue,
            Func<T, bool> validateFunc = null)
            : base()
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _onRent = onRent;
            _onReturn = onReturn;
            _onDestroy = onDestroy;
            _maxSize = maxSize;
            _validateFunc = validateFunc;
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
        /// 验证对象是否有效。
        /// </summary>
        /// <param name="obj">要验证的对象</param>
        /// <returns>对象是否有效</returns>
        protected override bool ValidateObject(T obj)
        {
            if (obj == null) return false;
            
            // 如果有自定义验证函数，则使用它
            if (_validateFunc != null)
            {
                return _validateFunc(obj);
            }

            // 对于Unity对象，检查是否已被销毁
            if (obj is UnityEngine.Object unityObj)
            {
                return unityObj != null;
            }

            return true;
        }

        /// <summary>
        /// 从池中租用一个对象。
        /// </summary>
        /// <returns>租用的对象</returns>
        public override T Rent()
        {
            T instance;
            // 尝试获取有效的对象
            while (Stack.Count > 0)
            {
                instance = base.Rent();
                if (ValidateObject(instance))
                {
                    OnRent(instance);
                    return instance;
                }
                // 如果对象无效，则销毁它并继续尝试
                OnDestroy(instance);
                _totalCreated--; // 减少计数，因为这个对象已经无效
            }

            // 如果没有有效对象，创建新的
            instance = CreateInstance();
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

            // 验证对象是否有效
            if (!ValidateObject(obj))
            {
                OnDestroy(obj);
                _totalCreated--; // 减少计数，因为这个对象已经无效
                return;
            }

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
        public PoolStatistics GetStatistics() => new()
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
