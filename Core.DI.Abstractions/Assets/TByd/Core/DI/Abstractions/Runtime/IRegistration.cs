using System;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 注册信息接口
    /// </summary>
    public interface IRegistration
    {
        /// <summary>
        /// 服务类型
        /// </summary>
        Type ServiceType { get; }

        /// <summary>
        /// 实现类型
        /// </summary>
        Type ImplementationType { get; }

        /// <summary>
        /// 生命周期类型
        /// </summary>
        LifetimeType Lifetime { get; }

        /// <summary>
        /// 实例（如果是单例注册）
        /// </summary>
        object Instance { get; }

        /// <summary>
        /// 注册ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 创建实例
        /// </summary>
        object CreateInstance(ILifetimeScope scope);
    }
} 