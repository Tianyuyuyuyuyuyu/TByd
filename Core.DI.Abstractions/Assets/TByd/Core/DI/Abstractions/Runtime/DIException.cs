using System;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 依赖注入异常
    /// </summary>
    public class DIException : Exception
    {
        public DIException() { }

        public DIException(string message) : base(message) { }

        public DIException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// 依赖解析异常
    /// </summary>
    public class ResolutionException : DIException
    {
        public ResolutionException() { }

        public ResolutionException(string message) : base(message) { }

        public ResolutionException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// 依赖注册异常
    /// </summary>
    public class RegistrationException : DIException
    {
        public RegistrationException() { }

        public RegistrationException(string message) : base(message) { }

        public RegistrationException(string message, Exception innerException) : base(message, innerException) { }
    }
} 