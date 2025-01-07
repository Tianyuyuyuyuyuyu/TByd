using System;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 生命周期接口
    /// </summary>
    public interface ILifetime
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        object GetInstance(ILifetimeScope scope, Type type, Func<object> createInstance);

        /// <summary>
        /// 释放实例
        /// </summary>
        void Dispose(ILifetimeScope scope);
    }

    /// <summary>
    /// 生命周期类型
    /// </summary>
    public enum LifetimeType
    {
        /// <summary>
        /// 瞬态（每次解析创建新实例）
        /// </summary>
        Transient,

        /// <summary>
        /// 作用域单例（在同一作用域内共享实例）
        /// </summary>
        Scoped,

        /// <summary>
        /// 全局单例（在容器内共享实例）
        /// </summary>
        Singleton
    }
} 