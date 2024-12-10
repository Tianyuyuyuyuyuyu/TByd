using System;

namespace TBydFramework.Pool.Runtime.Exceptions
{
    /// <summary>
    /// 当对象池操作无效时抛出的异常基类
    /// </summary>
    public class PoolException : Exception
    {
        public PoolException(string message) : base(message) { }
        public PoolException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// 当尝试返回一个不属于池的对象时抛出的异常
    /// </summary>
    public class InvalidPoolObjectException : PoolException
    {
        public InvalidPoolObjectException(string message) : base(message) { }
    }

    /// <summary>
    /// 当池容量已满且不允许扩展时抛出的异常
    /// </summary>
    public class PoolCapacityExceededException : PoolException
    {
        public PoolCapacityExceededException(string message) : base(message) { }
    }

    /// <summary>
    /// 当池已被释放但仍尝试使用时抛出的异常
    /// </summary>
    public class PoolDisposedException : PoolException
    {
        public PoolDisposedException(string message) : base(message) { }
    }

    /// <summary>
    /// 当池配置无效时抛出的异常
    /// </summary>
    public class InvalidPoolConfigurationException : PoolException
    {
        public InvalidPoolConfigurationException(string message) : base(message) { }
    }
} 