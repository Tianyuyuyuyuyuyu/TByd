using System;
using TBydFramework.Log.Runtime.Enum;
using TBydFramework.Log.Runtime.Interface;

namespace TBydFramework.Log.Runtime.Implementation
{
    public class Log4NetILogFactory : ILogFactory
    {
        /// <summary>
        /// 获取或设置日志级别。
        /// </summary>
        public Level Level { get; set; } = Level.ALL;

        /// <summary>
        /// 获取或设置是否在Unity环境中运行。
        /// </summary>
        public bool InUnity { get; set; } = true;

        public ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }

        public ILog GetLogger(Type type)
        {
            var log = log4net.LogManager.GetLogger(type);
            return new Log4NetLogImpl(log);
        }

        public ILog GetLogger(string name)
        {
            var log = log4net.LogManager.GetLogger(name);
            return new Log4NetLogImpl(log);
        }
    }
}
