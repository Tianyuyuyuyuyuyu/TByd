using System;

namespace TByd.PackageCreator.Editor.Core.ErrorHandling
{
    /// <summary>
    /// ErrorHandler扩展方法，提供更便捷的错误日志记录方式
    /// </summary>
    public static class ErrorHandlerExtensions
    {
        /// <summary>
        /// 记录信息级别日志
        /// </summary>
        /// <param name="handler">错误处理器</param>
        /// <param name="message">消息内容</param>
        /// <returns>错误信息</returns>
        public static ErrorInfo LogInfo(this ErrorHandler handler, string message)
        {
            return handler.LogError(ErrorType.None, message, ErrorLevel.Info);
        }

        /// <summary>
        /// 记录警告级别错误
        /// </summary>
        /// <param name="handler">错误处理器</param>
        /// <param name="errorType">错误类型</param>
        /// <param name="message">错误消息</param>
        /// <returns>错误信息</returns>
        public static ErrorInfo LogWarning(this ErrorHandler handler, ErrorType errorType, string message)
        {
            return handler.LogError(errorType, message, ErrorLevel.Warning);
        }

        /// <summary>
        /// 记录常规错误
        /// </summary>
        /// <param name="handler">错误处理器</param>
        /// <param name="errorType">错误类型</param>
        /// <param name="message">错误消息</param>
        /// <returns>错误信息</returns>
        public static ErrorInfo LogError(this ErrorHandler handler, ErrorType errorType, string message)
        {
            return handler.LogError(errorType, message, ErrorLevel.Error);
        }

        /// <summary>
        /// 记录异常相关错误
        /// </summary>
        /// <param name="handler">错误处理器</param>
        /// <param name="errorType">错误类型</param>
        /// <param name="exception">异常</param>
        /// <param name="message">错误消息</param>
        /// <returns>错误信息</returns>
        public static ErrorInfo LogException(this ErrorHandler handler, ErrorType errorType, Exception exception, string message)
        {
            string fullMessage = $"{message} 异常: {exception.Message}";
            return handler.LogError(errorType, fullMessage, ErrorLevel.Error, exception);
        }

        /// <summary>
        /// 记录严重错误
        /// </summary>
        /// <param name="handler">错误处理器</param>
        /// <param name="errorType">错误类型</param>
        /// <param name="message">错误消息</param>
        /// <param name="exception">异常（可选）</param>
        /// <returns>错误信息</returns>
        public static ErrorInfo LogCritical(this ErrorHandler handler, ErrorType errorType, string message, Exception exception = null)
        {
            return handler.LogError(errorType, message, ErrorLevel.Critical, exception);
        }
    }
}
