#if TBYD_UNITASK_SUPPORT
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace TBydFramework.Pool.Runtime.External
{
    /// <summary>
    /// 异步对象池的具体实现类,用于管理可异步创建的对象。
    /// </summary>
    /// <typeparam name="T">池中管理的对象类型</typeparam>
    public sealed class AsyncObjectPool<T> : AsyncObjectPoolBase<T>
        where T : class
    {
        private readonly Func<CancellationToken, UniTask<T>> _createFunc;
        private readonly Action<T> _onRent;
        private readonly Action<T> _onReturn;
        private readonly Action<T> _onDestroy;

        /// <summary>
        /// 构造函数,使用不需要取消令牌的创建函数初始化池。
        /// </summary>
        /// <param name="createFunc">用于创建新实例的异步函数</param>
        /// <param name="onRent">当对象被租用时调用的操作</param>
        /// <param name="onReturn">当对象被归还时调用的操作</param>
        /// <param name="onDestroy">当对象被销毁时调用的操作</param>
        public AsyncObjectPool(Func<UniTask<T>> createFunc, Action<T> onRent = null, Action<T> onReturn = null, Action<T> onDestroy = null)
        {
            if (createFunc == null) throw new ArgumentNullException(nameof(createFunc));

            _createFunc = _ => createFunc();
            _onRent = onRent;
            _onReturn = onReturn;
            _onDestroy = onDestroy;
        }
        
        /// <summary>
        /// 构造函数,使用需要取消令牌的创建函数初始化池。
        /// </summary>
        /// <param name="createFunc">用于创建新实例的异步函数,接受取消令牌</param>
        /// <param name="onRent">当对象被租用时调用的操作</param>
        /// <param name="onReturn">当对象被归还时调用的操作</param>
        /// <param name="onDestroy">当对象被销毁时调用的操作</param>
        public AsyncObjectPool(Func<CancellationToken, UniTask<T>> createFunc, Action<T> onRent = null, Action<T> onReturn = null, Action<T> onDestroy = null)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _onRent = onRent;
            _onReturn = onReturn;
            _onDestroy = onDestroy;
        }

        /// <summary>
        /// 异步创建新的实例。
        /// </summary>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>创建的新实例</returns>
        protected override UniTask<T> CreateInstanceAsync(CancellationToken cancellationToken)
        {
            return _createFunc(cancellationToken);
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
#endif
