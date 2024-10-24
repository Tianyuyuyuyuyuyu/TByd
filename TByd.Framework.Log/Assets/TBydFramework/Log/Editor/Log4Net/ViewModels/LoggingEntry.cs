using System;
using System.Collections.Generic;
using TBydFramework.Log.Runtime.Enum;
using TBydFramework.Log.Runtime.Serialization;

namespace TBydFramework.Log.Editor.Log4Net.ViewModels
{
    [Serializable]
    public class LoggingEntry
    {
        private List<LoggingData> _loggingDataList = new List<LoggingData>();

        public LoggingEntry(LoggingData loggingData)
        {
            _loggingDataList.Add(loggingData);
        }

        public List<LoggingData> LoggingDataList => _loggingDataList;

        public LoggingData LoggingData => _loggingDataList[0];

        public Level Level => _loggingDataList[0].Level;

        public string Message => _loggingDataList[0].Message;

        public DateTime TimeStamp => _loggingDataList[0].TimeStamp;

        public string ThreadName => _loggingDataList[0].ThreadName;

        public string LoggerName => _loggingDataList[0].LoggerName;

        public string UserName => _loggingDataList[0].UserName;

        public LocationInfo LocationInfo => _loggingDataList[0].LocationInfo;

        public int Count => _loggingDataList.Count;

        public bool IsMatch(LoggingData loggindData)
        {
            return loggindData.Level == LoggingData.Level && string.Equals(LoggingData.Message, loggindData.Message);
        }

        public void Add(LoggingData loggingData)
        {
            _loggingDataList.Add(loggingData);
        }

        public void RemoveAt(int index)
        {
            _loggingDataList.RemoveAt(index);
        }
    }
}