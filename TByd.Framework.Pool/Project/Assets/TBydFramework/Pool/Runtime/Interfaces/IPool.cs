using TBydFramework.Pool.Runtime.Config;
using TBydFramework.Pool.Runtime.Core;
using TBydFramework.Pool.Runtime.Diagnostics;

namespace TBydFramework.Pool.Runtime.Interfaces
{
    /// <summary>
    /// 对象池基础接口
    /// </summary>
    public interface IPool
    {
        /// <summary>
        /// 清理池中的所有对象
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取当前池中的对象数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取池的容量
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// 预热池，创建指定数量的对象
        /// </summary>
        void Prewarm(int count);

        /// <summary>
        /// 获取池的统计信息
        /// </summary>
        PoolStatistics GetStatistics();
    }

    /// <summary>
    /// 泛型对象池接口
    /// </summary>
    public interface IPool<T> : IPool where T : class
    {
        /// <summary>
        /// 从池中获取一个对象
        /// </summary>
        T Get();

        /// <summary>
        /// 将对象返回到池中
        /// </summary>
        void Return(T item);
    }
} 