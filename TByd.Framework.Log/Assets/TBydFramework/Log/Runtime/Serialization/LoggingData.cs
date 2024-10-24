using System;
using TBydFramework.Log.Runtime.Enum;
using UnityEngine;

namespace TBydFramework.Log.Runtime.Serialization
{
    /// <summary>
    /// 表示日志事件的可序列化数据结构。
    /// 此类用于封装和传输日志相关的信息，便于日志的存储、传输和分析。
    /// </summary>
    [Serializable]
    public class LoggingData : ISerializationCallbackReceiver
    {
        /// <summary>日志事件的唯一标识符</summary>
        private string _identity;

        /// <summary>记录日志的logger名称</summary>
        private string _loggerName;

        /// <summary>日志级别</summary>
        private Level _level;

        /// <summary>日志消息内容</summary>
        private string _message;

        /// <summary>记录日志的线程名称</summary>
        private string _threadName;

        /// <summary>日志记录的位置信息</summary>
        private LocationInfo _locationInfo;

        /// <summary>用户名（在此实现中为设备名称）</summary>
        private string _userName;

        /// <summary>异常信息的字符串表示</summary>
        private string _exceptionString;

        /// <summary>应用程序域名称</summary>
        private string _domain;

        /// <summary>时间戳的Ticks值，用于序列化</summary>
        private long _timeStampTicks;

        /// <summary>日志记录的时间戳</summary>
        private DateTime _timeStamp;
        
        /// <summary>
        /// 从 log4net 的 LoggingEventData 创建 LoggingData 实例。
        /// 此构造函数将 log4net 的日志事件数据转换为我们自定义的 LoggingData 格式。
        /// </summary>
        /// <param name="data">log4net 的日志事件数据</param>
        public LoggingData(log4net.Core.LoggingEventData data)
        {
            _identity = data.Identity;
            _loggerName = data.LoggerName;
            _level = ConvertLevel(data.Level.Value);
            _message = data.Message;
            _threadName = data.ThreadName;
            _timeStampTicks = data.TimeStamp.Ticks;
            _locationInfo = new LocationInfo(data.LocationInfo);
            _exceptionString = data.ExceptionString;
            _domain = data.Domain;
            _timeStamp = new DateTime(_timeStampTicks);
            _userName = GetDeviceName();
        }

        /// <summary>
        /// 将 log4net 的日志级别转换为自定义的 Level 枚举。
        /// 此方法用于统一日志级别的表示方式。
        /// </summary>
        /// <param name="value">log4net 的日志级别值</param>
        /// <returns>对应的 Level 枚举值</returns>
        private Level ConvertLevel(int value)
        {
            if (value <= 30000) return Level.DEBUG;
            if (value <= 40000) return Level.INFO;
            if (value <= 60000) return Level.WARN;
            if (value <= 70000) return Level.ERROR;
            return Level.FATAL;
        }

        /// <summary>
        /// 获取设备名称。
        /// 此方法尝试获取多种设备信息，以确保能够获得有意义的设备标识。
        /// </summary>
        /// <returns>设备名称或 "&lt;unknown&gt;" 如果无法获取</returns>
        private string GetDeviceName()
        {
            try
            {
                string[] deviceInfos = {
                    SystemInfo.deviceName,
                    SystemInfo.deviceModel,
                    SystemInfo.operatingSystem,
                    SystemInfo.graphicsDeviceName,
                    SystemInfo.processorType
                };

                foreach (var info in deviceInfos)
                {
                    if (!string.IsNullOrEmpty(info) && !info.Contains("unknown"))
                    {
                        return info;
                    }
                }

                return "<unknown>";
            }
            catch (Exception)
            {
                return "<unknown>";
            }
        }

        /// <summary>
        /// 序列化前的回调方法。
        /// 确保时间戳的Ticks值被正确序列化。
        /// </summary>
        public void OnBeforeSerialize()
        {
            _timeStampTicks = _timeStamp.Ticks;
        }

        /// <summary>
        /// 反序列化后的回调方法。
        /// 从序列化的Ticks值重建DateTime对象。
        /// </summary>
        public void OnAfterDeserialize()
        {
            _timeStamp = new DateTime(_timeStampTicks);
        }

        // 以下是属性的 getter 方法，提供对私有字段的只读访问

        /// <summary>获取日志事件的唯一标识符</summary>
        public string Identity => _identity;

        /// <summary>获取记录日志的logger名称</summary>
        public string LoggerName => _loggerName;

        /// <summary>获取日志级别</summary>
        public Level Level => _level;

        /// <summary>获取日志消息内容</summary>
        public string Message => _message;

        /// <summary>获取记录日志的线程名称</summary>
        public string ThreadName => _threadName;

        /// <summary>获取日志记录的时间戳</summary>
        public DateTime TimeStamp => _timeStamp;

        /// <summary>获取日志记录的位置信息</summary>
        public LocationInfo LocationInfo => _locationInfo;

        /// <summary>获取用户名（在此实现中为设备名称）</summary>
        public string UserName => _userName;

        /// <summary>获取异常信息的字符串表示</summary>
        public string ExceptionString => _exceptionString;

        /// <summary>获取应用程序域名称</summary>
        public string Domain => _domain;
    }
}
