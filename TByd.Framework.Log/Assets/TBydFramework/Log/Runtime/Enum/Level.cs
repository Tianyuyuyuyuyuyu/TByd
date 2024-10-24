namespace TBydFramework.Log.Runtime.Enum
{
    /// <summary>
    /// 定义日志级别的枚举。
    /// </summary>
    public enum Level
    {
        /// <summary>
        /// 所有级别。
        /// </summary>
        ALL = 0,

        /// <summary>
        /// 调试级别。
        /// </summary>
        DEBUG = 1,

        /// <summary>
        /// 信息级别。
        /// </summary>
        INFO = 2,

        /// <summary>
        /// 警告级别。
        /// </summary>
        WARN = 3,

        /// <summary>
        /// 错误级别。
        /// </summary>
        ERROR = 4,

        /// <summary>
        /// 致命错误级别。
        /// </summary>
        FATAL = 5,

        /// <summary>
        /// 关闭所有日志。
        /// </summary>
        OFF = 6
    }
}
