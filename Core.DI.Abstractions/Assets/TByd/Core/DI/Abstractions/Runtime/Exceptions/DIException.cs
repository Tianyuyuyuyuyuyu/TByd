using System;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// DI框架的基础异常类。
    /// </summary>
    public class DIException : Exception
    {
        public DIException(string message) : base(message) { }
        public DIException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// 当尝试解析的服务未注册时抛出的异常。
    /// </summary>
    public class ServiceNotRegisteredException : DIException
    {
        public ServiceNotRegisteredException(Type serviceType)
            : base($"服务类型 {serviceType.FullName} 未注册。") { }
    }

    /// <summary>
    /// 当发生循环依赖时抛出的异常。
    /// </summary>
    public class CircularDependencyException : DIException
    {
        public CircularDependencyException(string dependencyChain)
            : base($"检测到循环依赖：{dependencyChain}") { }
    }

    /// <summary>
    /// 当容器已被释放时抛出的异常。
    /// </summary>
    public class ContainerDisposedException : DIException
    {
        public ContainerDisposedException()
            : base("容器已被释放，无法执行操作。") { }
    }

    /// <summary>
    /// 当注册配置无效时抛出的异常。
    /// </summary>
    public class InvalidRegistrationException : DIException
    {
        public InvalidRegistrationException(string message)
            : base(message) { }
    }

    /// <summary>
    /// 当依赖注入过程中发生错误时抛出的异常。
    /// </summary>
    public class DependencyResolutionException : DIException
    {
        public DependencyResolutionException(string message)
            : base(message) { }

        public DependencyResolutionException(string message, Exception innerException)
            : base(message, innerException) { }
    }
} 