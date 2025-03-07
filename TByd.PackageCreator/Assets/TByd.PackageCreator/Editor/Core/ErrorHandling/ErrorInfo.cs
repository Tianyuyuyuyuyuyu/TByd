using System;

namespace TByd.PackageCreator.Editor.Core.ErrorHandling
{
    /// <summary>
    /// 错误信息类，存储错误的详细信息
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// 错误类型
        /// </summary>
        public ErrorType ErrorType { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 错误级别
        /// </summary>
        public ErrorLevel Level { get; set; }

        /// <summary>
        /// 错误发生的时间
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 相关异常（如果有）
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 错误相关的上下文数据（可选）
        /// </summary>
        public object Context { get; set; }

        /// <summary>
        /// 错误ID（用于查找和引用）
        /// </summary>
        public string ErrorId { get; set; }

        /// <summary>
        /// 错误信息构造函数
        /// </summary>
        public ErrorInfo()
        {
            Timestamp = DateTime.Now;
            ErrorId = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 创建包含基本信息的错误信息实例
        /// </summary>
        /// <param name="errorType">错误类型</param>
        /// <param name="message">错误消息</param>
        /// <param name="level">错误级别</param>
        /// <returns>错误信息实例</returns>
        public static ErrorInfo Create(ErrorType errorType, string message, ErrorLevel level)
        {
            return new ErrorInfo
            {
                ErrorType = errorType,
                Message = message,
                Level = level
            };
        }

        /// <summary>
        /// 从异常创建错误信息实例
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <param name="errorType">错误类型（默认为Unknown）</param>
        /// <param name="level">错误级别（默认为Error）</param>
        /// <returns>错误信息实例</returns>
        public static ErrorInfo FromException(Exception exception, ErrorType errorType = ErrorType.Unknown, ErrorLevel level = ErrorLevel.Error)
        {
            return new ErrorInfo
            {
                ErrorType = errorType,
                Message = exception.Message,
                Level = level,
                Exception = exception
            };
        }

        /// <summary>
        /// 将错误信息转换为易读的字符串
        /// </summary>
        /// <returns>包含错误详情的字符串</returns>
        public override string ToString()
        {
            return $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level}] [{ErrorType}] {Message}";
        }

        /// <summary>
        /// 获取包含堆栈跟踪的完整错误信息
        /// </summary>
        /// <returns>包含完整错误详情的字符串</returns>
        public string ToDetailedString()
        {
            var result = ToString();

            if (Exception != null)
            {
                result += $"\n异常类型: {Exception.GetType().Name}";
                result += $"\n异常消息: {Exception.Message}";
                result += $"\n堆栈跟踪: {Exception.StackTrace}";
            }

            return result;
        }
    }
}
