using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TBydFramework.Log.Editor.Log4Net.LogReceiver;
using TBydFramework.Log.Editor.Log4Net.Views;
using TBydFramework.Log.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using Level = TBydFramework.Log.Runtime.Enum.Level;

namespace TBydFramework.Log.Editor.Log4Net.ViewModels
{
    [Serializable]
    public class ConsoleVM
    {
        private static readonly LoggingContainer EMPTY_PAGE = new LoggingContainer(null, 0);

        private const string LEVEL_MASK_KEY = "TBydFramework::LOG::LOG4NET::LEVEL";
        private const string COLUMN_MASK_KEY = "TBydFramework::LOG::LOG4NET::COLUMN";
        private const string COLLAPSE_SHOW_KEY = "TBydFramework::LOG::LOG4NET::COLLAPSE";
        private const string PORT_KEY = "TBydFramework::LOG::LOG4NET::PORT";
        private const string PLAY_STATE_KEY = "TBydFramework::LOG::LOG4NET::PLAY_STATE";
        private const string LAST_SAVE_DIR_KEY = "TBydFramework::LOG::LOG4NET::LAST_DIR";

        private int _levelMask = 31;
        private int _columnMask = 7;
        private string _filterText;
        private bool _collapse;
        private int _maxCapacity = 10000;
        private int _port = 8085;
        private bool _playState = false;
        private int _currentIndex = -1;
        private List<TerminalInfo> _terminalInfos = new List<TerminalInfo>();
        private List<LoggingContainer> _containers = new List<LoggingContainer>();
        private string _lastSaveDir = "";

        [NonSerialized] private object _lock = new object();

        [NonSerialized] private ILogReceiver _receiver;

        public ConsoleVM()
        {
        }

        public void OnEnable()
        {
            try
            {
                _levelMask = EditorPrefs.GetInt(LEVEL_MASK_KEY, 31);
                _columnMask = EditorPrefs.GetInt(COLUMN_MASK_KEY, 7);
                _collapse = EditorPrefs.GetBool(COLLAPSE_SHOW_KEY, false);
                _port = EditorPrefs.GetInt(PORT_KEY, 8085);
                _playState = EditorPrefs.GetBool(PLAY_STATE_KEY, false);
                _lastSaveDir = EditorPrefs.GetString(LAST_SAVE_DIR_KEY, "");

                if (PlayState)
                    Start();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void OnDisable()
        {
        }

        public int MaxCapacity
        {
            get => _maxCapacity;
            set => _maxCapacity = value;
        }

        public bool IsLevelShow(Level level)
        {
            switch (level)
            {
                case Level.DEBUG:
                    return (_levelMask & 1) > 0;
                case Level.INFO:
                    return (_levelMask & 2) > 0;
                case Level.WARN:
                    return (_levelMask & 4) > 0;
                case Level.ERROR:
                    return (_levelMask & 8) > 0;
                case Level.FATAL:
                    return (_levelMask & 16) > 0;
                default:
                    return true;
            }
        }

        public void SetLevelShow(Level level, bool show)
        {
            switch (level)
            {
                case Level.DEBUG:
                    if (show)
                        _levelMask |= 1;
                    else
                        _levelMask &= ~1;
                    break;
                case Level.INFO:
                    if (show)
                        _levelMask |= 2;
                    else
                        _levelMask &= ~2;
                    break;
                case Level.WARN:
                    if (show)
                        _levelMask |= 4;
                    else
                        _levelMask &= ~4;
                    break;
                case Level.ERROR:
                    if (show)
                        _levelMask |= 8;
                    else
                        _levelMask &= ~8;
                    break;
                case Level.FATAL:
                    if (show)
                        _levelMask |= 16;
                    else
                        _levelMask &= ~16;
                    break;
                default:
                    break;
            }

            EditorPrefs.SetInt(LEVEL_MASK_KEY, _levelMask);
        }

        public bool IsColumnShow(EnumColumns enumColumns)
        {
            switch (enumColumns)
            {
                case EnumColumns.TimeStamp:
                    return (_columnMask & 1) > 0;
                case EnumColumns.Thread:
                    return (_columnMask & 2) > 0;
                case EnumColumns.Logger:
                    return (_columnMask & 4) > 0;
                default:
                    return true;
            }
        }

        public void SetColumnShow(EnumColumns enumColumns, bool show)
        {
            switch (enumColumns)
            {
                case EnumColumns.TimeStamp:
                    if (show)
                        _columnMask |= 1;
                    else
                        _columnMask &= ~1;
                    break;
                case EnumColumns.Thread:
                    if (show)
                        _columnMask |= 2;
                    else
                        _columnMask &= ~2;
                    break;
                case EnumColumns.Logger:
                    if (show)
                        _columnMask |= 4;
                    else
                        _columnMask &= ~4;
                    break;
                default:
                    break;
            }

            EditorPrefs.SetInt(COLUMN_MASK_KEY, _columnMask);
        }

        public bool Collapse
        {
            get => _collapse;
            set
            {
                if (_collapse == value)
                    return;

                _collapse = value;
                EditorPrefs.SetBool(COLLAPSE_SHOW_KEY, _collapse);
            }
        }

        public string FilterText
        {
            get => _filterText;
            set => _filterText = value;
        }

        public TerminalInfo CurrentTerminalInfo =>
            _currentIndex >= 0 && _currentIndex < _terminalInfos.Count
            ? _terminalInfos[_currentIndex]
            : null;

        public bool PlayState
        {
            get => _playState;
            set
            {
                if (_playState == value)
                    return;
                _playState = value;
                EditorPrefs.SetBool(PLAY_STATE_KEY, value);
            }
        }

        public bool Started => _receiver != null && _receiver.Started;

        public int Port
        {
            get => _port;
            set
            {
                if (_port == value)
                    return;

                _port = value;
                EditorPrefs.SetInt(PORT_KEY, value);
            }
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set => _currentIndex = value;
        }

        public List<TerminalInfo> TerminalInfos => _terminalInfos;

        public IPAddress GetLocalIPAddress()
        {
            var name = Dns.GetHostName();
            foreach (var ipa in Dns.GetHostAddresses(name))
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    return ipa;
            }

            return IPAddress.Loopback;
        }

        public virtual void Start()
        {
            try
            {
                lock (_lock)
                {
                    if (_receiver != null)
                    {
                        _receiver.MessageReceived -= OnMessageReceived;
                        _receiver.Stop();
                        _receiver = null;
                    }

                    _receiver = new UdpLogReceiver(Port);
                    _receiver.MessageReceived += OnMessageReceived;
                    _receiver.Start();
                }
            }
            catch (Exception e)
            {
                _playState = false;
                if (_receiver != null)
                    _receiver.MessageReceived -= OnMessageReceived;

                Debug.LogError(e);
            }
        }

        public virtual void Stop()
        {
            try
            {
                lock (_lock)
                {
                    if (_receiver != null)
                    {
                        _receiver.MessageReceived -= OnMessageReceived;
                        _receiver.Stop();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void OnMessageReceived(TerminalInfo terminalInfo, LoggingData loggingData)
        {
            AddLoggingData(terminalInfo, loggingData);
        }

        void AddLoggingData(TerminalInfo terminalInfo, LoggingData loggingData)
        {
            lock (_lock)
            {
                LoggingContainer container;
                int index = _terminalInfos.IndexOf(terminalInfo);
                if (index < 0)
                {
                    _terminalInfos.Add(terminalInfo);
                    container = new LoggingContainer(terminalInfo, _maxCapacity);
                    _containers.Add(container);
                    if (_currentIndex < 0 || _currentIndex >= _terminalInfos.Count)
                        _currentIndex = 0;
                }
                else
                {
                    container = _containers[index];
                }

                container.Add(loggingData);
            }
        }

        public void ClearLoggingData()
        {
            lock (_lock)
            {
                if (_currentIndex < 0 || _currentIndex >= _terminalInfos.Count)
                    return;

                _terminalInfos.RemoveAt(_currentIndex);
                _containers.RemoveAt(_currentIndex);

                if (_currentIndex >= _terminalInfos.Count)
                    _currentIndex -= 1;
            }
        }

        public void SaveLoggingData()
        {
            var container = GetCurrentContainer();
            var list = container.GetLoggingList();

            var filename = $"log-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";
            var location = EditorUtility.SaveFilePanel("Save", _lastSaveDir, filename, "txt");
            if (string.IsNullOrEmpty(location))
                return;

            var file = new FileInfo(location);
            var dir = file.Directory;
            if (dir != null)
            {
                if (!dir.Exists)
                    dir.Create();

                _lastSaveDir = dir.FullName;

                var buf = new StringBuilder();
                foreach (var data in list.SelectMany(entry => entry.LoggingDataList))
                {
                    buf.AppendFormat("{0:yyyy-MM-dd HH:mm:ss.fff}", data.TimeStamp);
                    buf.AppendFormat(" Thread[{0}]", data.ThreadName);
                    buf.AppendFormat(" {0}", data.Level.ToString());
                    buf.AppendFormat(" {0}", data.LoggerName);
                    buf.AppendFormat(" - {0}", data.Message);
                    buf.Append("\r\n");

                    if (data.LocationInfo != null && data.LocationInfo.StackFrames != null)
                    {
                        foreach (var frame in data.LocationInfo.StackFrames)
                        {
                            buf.Append(
                                    frame.FullInfo.Replace(Directory.GetCurrentDirectory().ToString() + @"\", ""))
                                .Append("\r\n");
                        }
                    }

                    buf.Append("\r\n");
                }

                File.WriteAllText(location, buf.ToString());
            }
        }

        public LoggingContainer GetCurrentContainer()
        {
            lock (_lock)
            {
                if (_currentIndex < 0 || _currentIndex >= _terminalInfos.Count)
                    return EMPTY_PAGE;

                return _containers[_currentIndex];
            }
        }
    }
}