using System;

namespace TBydFramework.Pool.Runtime.Interfaces
{
    /// <summary>
    /// 事件池接口，定义事件池的基本操作
    /// </summary>
    /// <typeparam name="TEventArgs">事件参数类型</typeparam>
    public interface IEventPool<TEventArgs> : IDisposable where TEventArgs : EventArgs
    {
        /// <summary>
        /// 从池中获取一个事件参数对象
        /// </summary>
        TEventArgs Get();

        /// <summary>
        /// 将事件参数对象归还到池中
        /// </summary>
        void Return(TEventArgs args);

        /// <summary>
        /// 获取池中当前的对象数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 清空池中的所有对象
        /// </summary>
        void Clear();

        /// <summary>
        /// 预热池，创建指定数量的对象
        /// </summary>
        void Prewarm(int count);
    }
} 