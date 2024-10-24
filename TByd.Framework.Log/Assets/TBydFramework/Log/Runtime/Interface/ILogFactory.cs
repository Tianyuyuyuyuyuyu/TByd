using System;
using TBydFramework.Log.Runtime.Enum;

namespace TBydFramework.Log.Runtime.Interface
{
    /// <summary>
    /// 定义日志工厂的接口。
    /// </summary>
    public interface ILogFactory
    {
        /// <summary>
        /// 获取或设置日志级别。
        /// </summary>
        Level Level { get; set; }

        /// <summary>
        /// 获取或设置是否在Unity环境中运行。
        /// </summary>
        bool InUnity { get; set; }

        /// <summary>
        /// 获取指定类型的日志记录器。
        /// </summary>
        /// <typeparam name="T">要获取日志记录器的类型。</typeparam>
        /// <returns>对应类型的日志记录器实例。</returns>
        ILog GetLogger<T>();

        /// <summary>
        /// 获取指定类型的日志记录器。
        /// </summary>
        /// <param name="type">要获取日志记录器的类型。</param>
        /// <returns>对应类型的日志记录器实例。</returns>
        ILog GetLogger(Type type);

        /// <summary>
        /// 获取指定名称的日志记录器。
        /// </summary>
        /// <param name="name">日志记录器的名称。</param>
        /// <returns>对应名称的日志记录器实例。</returns>
        ILog GetLogger(string name);
    }
}
