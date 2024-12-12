using System;
using TBydFramework.Pool.Runtime.Enums;

namespace TBydFramework.Pool.Runtime.Interfaces
{
    /// <summary>
    /// 定义池信息的接口
    /// </summary>
    public interface IPoolInfo
    {
        /// <summary>
        /// 获取池的名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 获取池的类型
        /// </summary>
        PoolType Type { get; }

        /// <summary>
        /// 获取池中当前的对象数量
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取池中活动对象的数量
        /// </summary>
        int ActiveCount { get; }

        /// <summary>
        /// 获取池的最大容量
        /// </summary>
        int MaxSize { get; }

        /// <summary>
        /// 获取池的当前容量
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// 清空池中的所有对象
        /// </summary>
        void Clear();

        /// <summary>
        /// 预热池，创建指定数量的对象
        /// </summary>
        /// <param name="count">要预热的对象数量</param>
        void Prewarm(int count);
    }
} 