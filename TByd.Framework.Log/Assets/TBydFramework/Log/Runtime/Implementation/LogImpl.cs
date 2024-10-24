using System;
using TBydFramework.Log.Runtime.Enum;
using TBydFramework.Log.Runtime.Interface;

namespace TBydFramework.Log.Runtime.Implementation
{
    /// <summary>
    /// 日志实现类,提供具体的日志记录功能。
    /// </summary>
    internal class LogImpl : ILog
    {
        private readonly string _name;
        private readonly DefaultILogFactory _factory;

        /// <summary>
        /// 初始化日志实现类的新实例。
        /// </summary>
        /// <param name="name">日志记录器的名称。</param>
        /// <param name="factory">日志工厂实例。</param>
        public LogImpl(string name, DefaultILogFactory factory)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// 获取日志记录器的名称。
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// 格式化日志消息。
        /// </summary>
        /// <param name="message">日志消息。</param>
        /// <param name="level">日志级别。</param>
        /// <returns>格式化后的日志消息。</returns>
        protected virtual string Format(object message, string level)
        {
            return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {_name} - {message}";
        }

        public virtual void Debug(object message) => Log(message, "DEBUG");
        public virtual void Debug(object message, Exception exception) => Log($"{message} Exception:{exception}", "DEBUG");
        public virtual void DebugFormat(string format, params object[] args) => Log(string.Format(format, args), "DEBUG");

        public virtual void Info(object message) => Log(message, "INFO");
        public virtual void Info(object message, Exception exception) => Log($"{message} Exception:{exception}", "INFO");
        public virtual void InfoFormat(string format, params object[] args) => Log(string.Format(format, args), "INFO");

        public virtual void Warn(object message) => Log(message, "WARN");
        public virtual void Warn(object message, Exception exception) => Log($"{message} Exception:{exception}", "WARN");
        public virtual void WarnFormat(string format, params object[] args) => Log(string.Format(format, args), "WARN");

        public virtual void Error(object message) => Log(message, "ERROR");
        public virtual void Error(object message, Exception exception) => Log($"{message} Exception:{exception}", "ERROR");
        public virtual void ErrorFormat(string format, params object[] args) => Log(string.Format(format, args), "ERROR");

        public virtual void Fatal(object message) => Log(message, "FATAL");
        public virtual void Fatal(object message, Exception exception) => Log($"{message} Exception:{exception}", "FATAL");
        public virtual void FatalFormat(string format, params object[] args) => Log(string.Format(format, args), "FATAL");

        private void Log(object message, string level)
        {
            if (_factory.InUnity)
                UnityEngine.Debug.Log(Format(message, level));
#if !NETFX_CORE
            else
                Console.WriteLine(Format(message, level));
#endif
        }

        /// <summary>
        /// 检查指定的日志级别是否启用。
        /// </summary>
        /// <param name="level">要检查的日志级别。</param>
        /// <returns>如果指定的日志级别已启用,则为true;否则为false。</returns>
        protected bool IsEnabled(Level level)
        {
            return level >= _factory.Level;
        }

        public virtual bool IsDebugEnabled => IsEnabled(Level.DEBUG);
        public virtual bool IsInfoEnabled => IsEnabled(Level.INFO);
        public virtual bool IsWarnEnabled => IsEnabled(Level.WARN);
        public virtual bool IsErrorEnabled => IsEnabled(Level.ERROR);
        public virtual bool IsFatalEnabled => IsEnabled(Level.FATAL);
    }
}
