using System;
using System.Collections.Generic;
using TBydFramework.Log.Editor.Log4Net.Views;
using TBydFramework.Log.Runtime.Enum;
using TBydFramework.Log.Runtime.Serialization;

namespace TBydFramework.Log.Editor.Log4Net.ViewModels
{
    [Serializable]
    public class LoggingContainer
    {
        private TerminalInfo _terminalInfo;
        private int _capacity = 10000;
        private List<LoggingEntry> _loggings = new List<LoggingEntry>();
        private int[] _counters = new int[5];

        public LoggingContainer(TerminalInfo terminalInfo, int capacity)
        {
            _terminalInfo = terminalInfo;
            _capacity = capacity;
        }

        public TerminalInfo TerminalInfo => _terminalInfo;

        public int Capacity
        {
            get => _capacity;
            set => _capacity = value;
        }

        public int GetCount(Level level)
        {
            switch (level)
            {
                case Level.DEBUG:
                    return _counters[0];
                case Level.INFO:
                    return _counters[1];
                case Level.WARN:
                    return _counters[2];
                case Level.ERROR:
                    return _counters[3];
                case Level.FATAL:
                    return _counters[4];
                default:
                    return _counters[0];
            }
        }

        private void UpdateCount(Level level, int value)
        {
            switch (level)
            {
                case Level.DEBUG:
                    _counters[0] += value;
                    break;
                case Level.INFO:
                    _counters[1] += value;
                    break;
                case Level.WARN:
                    _counters[2] += value;
                    break;
                case Level.ERROR:
                    _counters[3] += value;
                    break;
                case Level.FATAL:
                    _counters[4] += value;
                    break;
                default:
                    break;
            }
        }

        public void Add(LoggingData loggingData)
        {
            lock (_loggings)
            {
                var last = _loggings.Count > 0 ? _loggings[_loggings.Count - 1] : null;
                if (last != null && last.IsMatch(loggingData))
                {
                    last.Add(loggingData);
                    UpdateCount(last.Level, 1);
                }
                else
                {
                    var logging = new LoggingEntry(loggingData);
                    _loggings.Add(logging);
                    UpdateCount(logging.Level, 1);
                }

                if (_loggings.Count > _capacity)
                {
                    var oldest = _loggings[0];
                    UpdateCount(oldest.Level, -1);
                    if (oldest.Count <= 1)
                        _loggings.RemoveAt(0);
                    else
                        oldest.RemoveAt(0);
                }
            }
        }

        public void Clear()
        {
            lock (_loggings)
            {
                _counters = new int[5];
                _loggings.Clear();
            }
        }

        public List<LoggingEntry> GetLoggingList()
        {
            lock (_loggings)
            {
                return new List<LoggingEntry>(_loggings);
            }
        }
    }
}