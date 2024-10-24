using System;
using TBydFramework.Log.Runtime.Enum;
using UnityEngine;

namespace TBydFramework.Log.Runtime.Serialization
{
    [Serializable]
    public class LoggingData : ISerializationCallbackReceiver
    {
        private string identity;
        private string loggerName;
        private Level level;
        private string message;
        private string threadName;
        private LocationInfo locationInfo;
        private string userName;
        private string exceptionString;
        private string domain;
        private long timeStampTicks;
        private DateTime timeStamp;
        
        public LoggingData(log4net.Core.LoggingEventData data)
        {
            this.identity = data.Identity;
            this.loggerName = data.LoggerName;
            this.level = Level.DEBUG;
            this.message = data.Message;
            this.threadName = data.ThreadName;
            this.timeStampTicks = data.TimeStamp.Ticks;
            this.locationInfo = new LocationInfo(data.LocationInfo);
            this.exceptionString = data.ExceptionString;
            this.domain = data.Domain;
            this.level = ConvertLevel(data.Level.Value);
            this.timeStamp = new DateTime(this.timeStampTicks);
            this.userName = GetDeviceName();
        }

        private Level ConvertLevel(int value)
        {
            if (value <= 30000)
                return Level.DEBUG;
            if (value <= 40000)
                return Level.INFO;
            if (value <= 60000)
                return Level.WARN;
            if (value <= 70000)
                return Level.ERROR;
            return Level.FATAL;
        }

        private string GetDeviceName()
        {
            try
            {
                var deviceName = SystemInfo.deviceName;
                if (!string.IsNullOrEmpty(deviceName) && !deviceName.Contains("unknown"))
                    return deviceName;

                deviceName = SystemInfo.deviceModel;
                if (!string.IsNullOrEmpty(deviceName) && !deviceName.Contains("unknown"))
                    return deviceName;

                deviceName = SystemInfo.operatingSystem;
                if (!string.IsNullOrEmpty(deviceName) && !deviceName.Contains("unknown"))
                    return deviceName;

                deviceName = SystemInfo.graphicsDeviceName;
                if (!string.IsNullOrEmpty(deviceName) && !deviceName.Contains("unknown"))
                    return deviceName;

                deviceName = SystemInfo.processorType;
                if (!string.IsNullOrEmpty(deviceName) && !deviceName.Contains("unknown"))
                    return deviceName;
                return "<unknown>";
            }
            catch (Exception)
            {
                return "<unknown>";
            }
        }

        public void OnBeforeSerialize()
        {
            this.timeStampTicks = this.timeStamp.Ticks;
        }

        public void OnAfterDeserialize()
        {
            this.timeStamp = new DateTime(this.timeStampTicks);
        }

        public string Identity
        {
            get { return this.identity; }
        }

        public string LoggerName
        {
            get { return this.loggerName; }
        }

        public Level Level
        {
            get { return this.level; }
        }

        public string Message
        {
            get { return this.message; }
        }

        public string ThreadName
        {
            get { return this.threadName; }
        }

        public DateTime TimeStamp
        {
            get { return this.timeStamp; }
        }

        public LocationInfo LocationInfo
        {
            get { return this.locationInfo; }
        }

        public string UserName
        {
            get { return this.userName; }
        }

        public string ExceptionString
        {
            get { return this.exceptionString; }
        }

        public string Domain
        {
            get { return this.domain; }
        }
    }
}