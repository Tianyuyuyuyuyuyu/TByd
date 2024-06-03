using TBydFramework.Runtime.Enums;

namespace TBydFramework.Runtime.Interface
{
    /// <summary>
    /// 游戏框架日志辅助器接口。
    /// </summary>
    public interface ILogHelper
    {
        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">游戏框架日志等级。</param>
        /// <param name="message">日志内容。</param>
        void Log(EnumLogLevel level, object message);
    }
}