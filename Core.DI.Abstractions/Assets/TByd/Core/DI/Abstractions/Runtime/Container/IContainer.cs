using System;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 依赖注入容器的核心接口
    /// </summary>
    public interface IContainer : IDisposable
    {
        /// <summary>
        /// 注册类型
        /// </summary>
        void Register<TService, TImplementation>() where TImplementation : TService;

        /// <summary>
        /// 注册实例
        /// </summary>
        void RegisterInstance<TService>(TService instance);

        /// <summary>
        /// 注册单例
        /// </summary>
        void RegisterSingleton<TService, TImplementation>() where TImplementation : TService;

        /// <summary>
        /// 解析类型
        /// </summary>
        TService Resolve<TService>();

        /// <summary>
        /// 尝试解析类型
        /// </summary>
        bool TryResolve<TService>(out TService service);

        /// <summary>
        /// 判断是否已注册类型
        /// </summary>
        bool IsRegistered<TService>();

        /// <summary>
        /// 创建子容器
        /// </summary>
        IContainer CreateChildContainer();
    }
} 