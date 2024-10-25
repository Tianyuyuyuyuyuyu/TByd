using System;

namespace TBydFramework.Pool.Runtime
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

        /// <summary>
        /// 初始化对象池。
        /// </summary>
        /// <param name="createFunc">用于创建新实例的函数</param>
        /// <param name="onRent">当对象被租用时调用的操作</param>
        /// <param name="onReturn">当对象被归还时调用的操作</param>
        /// <param name="onDestroy">当对象被销毁时调用的操作</param>
        public ObjectPool(Func<T> createFunc, Action<T> onRent = null, Action<T> onReturn = null, Action<T> onDestroy = null)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _onRent = onRent;
            _onReturn = onReturn;
            _onDestroy = onDestroy;
        }

        /// <summary>
        /// 创建新的实例。
        /// </summary>
        /// <returns>创建的新实例</returns>
        protected override T CreateInstance()
        {
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
            _onRent?.Invoke(instance);
        }

        /// <summary>
        /// 当实例被归还时调用。
        /// </summary>
        /// <param name="instance">被归还的实例</param>
        protected override void OnReturn(T instance)
        {
            _onReturn?.Invoke(instance);
        }
    }
}
