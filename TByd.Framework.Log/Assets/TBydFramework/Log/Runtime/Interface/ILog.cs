using System;

namespace TBydFramework.Log.Runtime.Interface
{
    /// <summary>
    /// 定义日志记录器的接口。
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 获取日志记录器的名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 记录调试级别的日志。
        /// </summary>
        /// <param name="message">日志消息。</param>
        void Debug(object message);

        /// <summary>
        /// 记录调试级别的日志，包含异常信息。
        /// </summary>
        /// <param name="message">日志消息。</param>
        /// <param name="exception">异常对象。</param>
        void Debug(object message, Exception exception);

        /// <summary>
        /// 使用格式化字符串记录调试级别的日志。
        /// </summary>
        /// <param name="format">格式化字符串。</param>
        /// <param name="args">格式化参数。</param>
        void DebugFormat(string format, params object[] args);

        /// <summary>
        /// 记录信息级别的日志。
        /// </summary>
        /// <param name="message">日志消息。</param>
        void Info(object message);

        /// <summary>
        /// 记录信息级别的日志，包含异常信息。
        /// </summary>
        /// <param name="message">日志消息。</param>
        /// <param name="exception">异常对象。</param>
        void Info(object message, Exception exception);

        /// <summary>
        /// 使用格式化字符串记录信息级别的日志。
        /// </summary>
        /// <param name="format">格式化字符串。</param>
        /// <param name="args">格式化参数。</param>
        void InfoFormat(string format, params object[] args);

        /// <summary>
        /// 记录警告级别的日志。
        /// </summary>
        /// <param name="message">日志消息。</param>
        void Warn(object message);

        /// <summary>
        /// 记录警告级别的日志，包含异常信息。
        /// </summary>
        /// <param name="message">日志消息。</param>
        /// <param name="exception">异常对象。</param>
        void Warn(object message, Exception exception);

        /// <summary>
        /// 使用格式化字符串记录警告级别的日志。
        /// </summary>
        /// <param name="format">格式化字符串。</param>
        /// <param name="args">格式化参数。</param>
        void WarnFormat(string format, params object[] args);

        /// <summary>
        /// 记录错误级别的日志。
        /// </summary>
        /// <param name="message">日志消息。</param>
        void Error(object message);

        /// <summary>
        /// 记录错误级别的日志，包含异常信息。
        /// </summary>
        /// <param name="message">日志消息。</param>
        /// <param name="exception">异常对象。</param>
        void Error(object message, Exception exception);

        /// <summary>
        /// 使用格式化字符串记录错误级别的日志。
        /// </summary>
        /// <param name="format">格式化字符串。</param>
        /// <param name="args">格式化参数。</param>
        void ErrorFormat(string format, params object[] args);

        /// <summary>
        /// 记录致命错误级别的日志。
        /// </summary>
        /// <param name="message">日志消息。</param>
        void Fatal(object message);

        /// <summary>
        /// 记录致命错误级别的日志，包含异常信息。
        /// </summary>
        /// <param name="message">日志消息。</param>
        /// <param name="exception">异常对象。</param>
        void Fatal(object message, Exception exception);

        /// <summary>
        /// 使用格式化字符串记录致命错误级别的日志。
        /// </summary>
        /// <param name="format">格式化字符串。</param>
        /// <param name="args">格式化参数。</param>
        void FatalFormat(string format, params object[] args);

        /// <summary>
        /// 获取调试级别日志是否启用。
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// 获取信息级别日志是否启用。
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// 获取警告级别日志是否启用。
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// 获取错误级别日志是否启用。
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// 获取致命错误级别日志是否启用。
        /// </summary>
        bool IsFatalEnabled { get; }
    }
}
