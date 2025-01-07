using System;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 容器构建器接口
    /// </summary>
    public interface IContainerBuilder
    {
        /// <summary>
        /// 注册类型
        /// </summary>
        IRegistration Register(Type serviceType, Type implementationType, LifetimeType lifetime = LifetimeType.Transient, string id = null);

        /// <summary>
        /// 注册实例
        /// </summary>
        IRegistration RegisterInstance(Type serviceType, object instance, string id = null);

        /// <summary>
        /// 注册工厂方法
        /// </summary>
        IRegistration RegisterFactory(Type serviceType, Func<ILifetimeScope, object> factory, LifetimeType lifetime = LifetimeType.Transient, string id = null);

        /// <summary>
        /// 构建容器
        /// </summary>
        IContainer Build();
    }

    /// <summary>
    /// 容器构建器扩展方法
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// 注册类型（泛型版本）
        /// </summary>
        public static IRegistration Register<TService, TImplementation>(
            this IContainerBuilder builder,
            LifetimeType lifetime = LifetimeType.Transient,
            string id = null) where TImplementation : TService
        {
            return builder.Register(typeof(TService), typeof(TImplementation), lifetime, id);
        }

        /// <summary>
        /// 注册实例（泛型版本）
        /// </summary>
        public static IRegistration RegisterInstance<TService>(
            this IContainerBuilder builder,
            TService instance,
            string id = null)
        {
            return builder.RegisterInstance(typeof(TService), instance, id);
        }

        /// <summary>
        /// 注册工厂方法（泛型版本）
        /// </summary>
        public static IRegistration RegisterFactory<TService>(
            this IContainerBuilder builder,
            Func<ILifetimeScope, TService> factory,
            LifetimeType lifetime = LifetimeType.Transient,
            string id = null)
        {
            return builder.RegisterFactory(typeof(TService), scope => factory(scope), lifetime, id);
        }
    }
} 