using System;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 生命周期作用域接口
    /// </summary>
    public interface ILifetimeScope : IDisposable
    {
        /// <summary>
        /// 当前容器
        /// </summary>
        IContainer Container { get; }

        /// <summary>
        /// 父作用域
        /// </summary>
        ILifetimeScope Parent { get; }

        /// <summary>
        /// 创建子作用域
        /// </summary>
        ILifetimeScope CreateChildScope();

        /// <summary>
        /// 解析服务
        /// </summary>
        TService Resolve<TService>();
    }
} 