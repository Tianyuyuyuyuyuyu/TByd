using System;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 提供注册配置的链式API。
    /// </summary>
    /// <typeparam name="TLimit">注册类型的限制</typeparam>
    /// <typeparam name="TActivatorData">激活器数据类型</typeparam>
    /// <typeparam name="TRegistrationStyle">注册风格类型</typeparam>
    public interface IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
    {
        /// <summary>
        /// 获取当前注册的限制类型。
        /// </summary>
        Type LimitType { get; }

        /// <summary>
        /// 获取激活器数据。
        /// </summary>
        TActivatorData ActivatorData { get; }

        /// <summary>
        /// 获取注册风格。
        /// </summary>
        TRegistrationStyle RegistrationStyle { get; }

        /// <summary>
        /// 设置组件的生命周期。
        /// </summary>
        /// <param name="lifetime">生命周期实例</param>
        /// <returns>注册构建器</returns>
        IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> SetLifetime(ILifetime lifetime);

        /// <summary>
        /// 为组件添加别名。
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>注册构建器</returns>
        IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> As<TService>() where TService : class;

        /// <summary>
        /// 标记组件为单例。
        /// </summary>
        /// <returns>注册构建器</returns>
        IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> SingleInstance();

        /// <summary>
        /// 标记组件为瞬态。
        /// </summary>
        /// <returns>注册构建器</returns>
        IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> InstancePerDependency();
    }
} 