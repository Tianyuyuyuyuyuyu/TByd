using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TBydFramework.Log.Editor.Log4Net.ViewModels;
using TBydFramework.Log.Runtime.Serialization;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Level = TBydFramework.Log.Runtime.Enum.Level;
using LocationInfo = TBydFramework.Log.Runtime.Serialization.LocationInfo;

namespace TBydFramework.Log.Editor.Log4Net.Views
{
    public class ConsoleWindow : EditorWindow
    {
        [MenuItem("Tools/TBydFramework/Log4Net Console")]
        static void ShowWindow()
        {
            var window = GetWindow<ConsoleWindow>(false, "Remoting");
            window.Show();
        }

        private ConsoleVM _consoleVm;

        private Vector2 _loggingPanelScrollPosition;
        private Vector2 _detailPanelScrollPosition;
        private float _loggingVerticalScrollBarPercent;

        private Texture[] _levelIconTextures;
        private GUISwitchContentData _playToggle;
        private GUISwitchContentData[] _levelButtonDatas;

        private GUIContentData _clearButton;
        private GUIContentData _saveButton;

        private GUISwitchContentData _collapseToggle;

        private GUISwitchContentData[] _columnButtonDatas;

        private GUIStyle _entryStyleBackEven;
        private GUIStyle _entryStyleBackOdd;
        private GUIStyle _detailStyle;
        private GUIStyle _countBadgeStyle;

        private GUIStyle _toolbarSeachTextFieldStyle;
        private GUIStyle _toolbarSeachCancelButtonStyle;

        private float _toolbarHeight = 20f;
        private float _splitterRectHeight = 20f;
        private Rect _verticalSplitterRect;
        private Rect _verticalSplitterLineRect;
        private float _verticalSplitterPercent;
        private bool _resizingVerticalSplitter = false;

        [NonSerialized] private Texture2D _splitterLineTexture;

        private float _lineHeight;

        private int _selectedIndex = -1;
        private double _lastClickTime = 0f;
        private double _doubleClickInterval = 0.2f;

        private List<LoggingData> _renderedList;
        private List<int> _renderedLineCountList;

        private void OnEnable()
        {
            _loggingVerticalScrollBarPercent = 1f;
            _lineHeight = 30;
            _toolbarHeight = 20f;
            _splitterRectHeight = 10f;
            _verticalSplitterPercent = 0.7f;
            _verticalSplitterLineRect = new Rect(0f, (position.height * _verticalSplitterPercent), position.width, 1);
            _verticalSplitterRect = new Rect(_verticalSplitterLineRect.x,
                _verticalSplitterLineRect.y - _splitterRectHeight / 2f, _verticalSplitterLineRect.width,
                _splitterRectHeight);
            _renderedList = new List<LoggingData>();
            _renderedLineCountList = new List<int>();

            if (_consoleVm == null)
                _consoleVm = new ConsoleVM();
            _consoleVm.OnEnable();
        }

        private void Disable()
        {
            if (_consoleVm != null)
                _consoleVm.OnDisable();
        }

        void OnDestroy()
        {
            if (_consoleVm != null)
            {
                _consoleVm.Stop();
                _consoleVm = null;
            }
        }

        private void Init()
        {
            if (_splitterLineTexture != null)
                return;

            _entryStyleBackEven = new GUIStyle("CN EntryBackEven");
            _entryStyleBackEven.margin = new RectOffset(0, 0, 0, 0);
            _entryStyleBackEven.border = new RectOffset(0, 0, 0, 0);
            _entryStyleBackEven.fixedHeight = 0;
            _entryStyleBackEven.fixedWidth = 0;
            _entryStyleBackEven.richText = true;
            _entryStyleBackEven.imagePosition = ImagePosition.ImageLeft;
            _entryStyleBackEven.contentOffset = new Vector2(0f, 0f);
            _entryStyleBackEven.padding = new RectOffset(10, 0, 0, 0);
            _entryStyleBackEven.alignment = TextAnchor.MiddleLeft;

            _entryStyleBackOdd = new GUIStyle("CN EntryBackOdd");
            _entryStyleBackOdd.margin = new RectOffset(0, 0, 0, 0);
            _entryStyleBackOdd.border = new RectOffset(0, 0, 0, 0);
            _entryStyleBackOdd.fixedHeight = 0;
            _entryStyleBackOdd.fixedWidth = 0;
            _entryStyleBackOdd.richText = true;
            _entryStyleBackOdd.imagePosition = ImagePosition.ImageLeft;
            _entryStyleBackOdd.contentOffset = new Vector2(0f, 0f);
            _entryStyleBackOdd.padding = new RectOffset(10, 0, 0, 0);
            _entryStyleBackOdd.alignment = TextAnchor.MiddleLeft;

            _splitterLineTexture = new Texture2D(1, 1);
            _splitterLineTexture.SetPixel(0, 0, Color.black);
            _splitterLineTexture.Apply();

            _detailStyle = new GUIStyle("CN EntryBackOdd")
            {
                margin = new RectOffset(0, 0, 0, 0),
                border = new RectOffset(5, 5, 5, 5),
                richText = true,
                contentOffset = new Vector2(0f, 0f),
                padding = new RectOffset(5, 5, 5, 5),
                alignment = TextAnchor.UpperLeft,
                fixedWidth = 0,
                wordWrap = true
            };

            _countBadgeStyle = new GUIStyle("CN CountBadge")
            {
                margin = new RectOffset(0, 0, 0, 0),
                border = new RectOffset(0, 0, 0, 0),
                contentOffset = new Vector2(0f, 0f),
                padding = new RectOffset(3, 3, 3, 3),
                fixedHeight = 0f,
                fixedWidth = 0f,
                alignment = TextAnchor.MiddleCenter
            };

            if (_toolbarSeachTextFieldStyle == null)
                _toolbarSeachTextFieldStyle = new GUIStyle("ToolbarSeachTextField");

            if (_toolbarSeachCancelButtonStyle == null)
                _toolbarSeachCancelButtonStyle = new GUIStyle("ToolbarSeachCancelButton");

            _playToggle = new GUISwitchContentData(false,
                new GUIContent(Resources.Load<Texture2D>("Icons/play-on"), "Start"),
                new GUIContent(Resources.Load<Texture2D>("Icons/play-off"), "Stop"), EditorStyles.toolbarButton);

            _clearButton = new GUIContentData(new GUIContent(Resources.Load<Texture2D>("Icons/delete"), "Clear"),
                EditorStyles.toolbarButton);
            _saveButton = new GUIContentData(new GUIContent(Resources.Load<Texture2D>("Icons/save"), "Save"),
                EditorStyles.toolbarButton);

            _collapseToggle = new GUISwitchContentData(_consoleVm.Collapse, new GUIContent("Collapse", "Collapse"),
                EditorStyles.toolbarButton);

            _levelIconTextures = new Texture[]
            {
                Resources.Load<Texture2D>("Icons/debug"),
                Resources.Load<Texture2D>("Icons/info"),
                Resources.Load<Texture2D>("Icons/warn"),
                Resources.Load<Texture2D>("Icons/error"),
                Resources.Load<Texture2D>("Icons/fatal")
            };

            _levelButtonDatas = new GUISwitchContentData[5];
            _levelButtonDatas[0] = new GUISwitchContentData(_consoleVm.IsLevelShow(Level.DEBUG),
                new GUIContent(_levelIconTextures[0], "Debug"), EditorStyles.toolbarButton);
            _levelButtonDatas[1] = new GUISwitchContentData(_consoleVm.IsLevelShow(Level.INFO),
                new GUIContent(_levelIconTextures[1], "Info"), EditorStyles.toolbarButton);
            _levelButtonDatas[2] = new GUISwitchContentData(_consoleVm.IsLevelShow(Level.WARN),
                new GUIContent(_levelIconTextures[2], "Warn"), EditorStyles.toolbarButton);
            _levelButtonDatas[3] = new GUISwitchContentData(_consoleVm.IsLevelShow(Level.ERROR),
                new GUIContent(_levelIconTextures[3], "Error"), EditorStyles.toolbarButton);
            _levelButtonDatas[4] = new GUISwitchContentData(_consoleVm.IsLevelShow(Level.FATAL),
                new GUIContent(_levelIconTextures[4], "Fatal"), EditorStyles.toolbarButton);


            _columnButtonDatas = new GUISwitchContentData[3];
            _columnButtonDatas[0] = new GUISwitchContentData(_consoleVm.IsColumnShow(EnumColumns.TimeStamp),
                new GUIContent("Time", "Time"), EditorStyles.toolbarButton);
            _columnButtonDatas[1] = new GUISwitchContentData(_consoleVm.IsColumnShow(EnumColumns.Thread),
                new GUIContent("Thread", "Thread"), EditorStyles.toolbarButton);
            _columnButtonDatas[2] = new GUISwitchContentData(_consoleVm.IsColumnShow(EnumColumns.Logger),
                new GUIContent("Logger", "Logger"), EditorStyles.toolbarButton);
        }

        private void OnGUI()
        {
            Init();

            var container = _consoleVm.GetCurrentContainer();

            var oldColor = GUI.backgroundColor;
            DrawToolbar(container);
            GUI.backgroundColor = oldColor;
            DrawVerticalSplitter();
            GUI.backgroundColor = oldColor;
            DrawLoggingGrid(container);
            GUI.backgroundColor = oldColor;
            DrawLoggingDetail();
            GUI.backgroundColor = oldColor;

            Repaint();
        }

        private Texture GetTextureIcon(Level level)
        {
            switch (level)
            {
                case Level.DEBUG:
                    return _levelIconTextures[0];
                case Level.INFO:
                    return _levelIconTextures[1];
                case Level.WARN:
                    return _levelIconTextures[2];
                case Level.ERROR:
                    return _levelIconTextures[3];
                case Level.FATAL:
                    return _levelIconTextures[4];
                default:
                    return _levelIconTextures[0];
            }
        }

        private static string GetColorString(Level level)
        {
            switch (level)
            {
                case Level.DEBUG:
                    return "#b4b4b4";
                case Level.INFO:
                    return "#0097e5";
                case Level.WARN:
                    return "#c1c04c";
                case Level.ERROR:
                    return "#e58600";
                case Level.FATAL:
                    return "#c04e43";
                default:
                    return "#b4b4b4";
            }
        }

        private GUIContent GetLogLineGUIContent(LoggingData data)
        {
            var buf = new StringBuilder();
            buf.AppendFormat("<color={0}>", GetColorString(data.Level));

            if (_consoleVm.IsColumnShow(EnumColumns.TimeStamp))
                buf.AppendFormat("{0:yyyy-MM-dd HH:mm:ss.fff}", data.TimeStamp);

            if (_consoleVm.IsColumnShow(EnumColumns.Thread))
                buf.AppendFormat(" Thread[{0}]", data.ThreadName);

            buf.AppendFormat(" {0}", data.Level.ToString());

            if (_consoleVm.IsColumnShow(EnumColumns.Logger))
                buf.AppendFormat(" {0}", data.LoggerName);

            buf.AppendFormat(" - {0}", data.Message);
            buf.Append("</color>");

            return new GUIContent(buf.ToString(), GetTextureIcon(data.Level));
        }

        private string GetLogDetailContent(LoggingData data)
        {
            var buf = new StringBuilder();
            buf.AppendFormat("<color={0}>", GetColorString(data.Level));

            if (_consoleVm.IsColumnShow(EnumColumns.TimeStamp))
                buf.AppendFormat("{0:yyyy-MM-dd HH:mm:ss.fff}", data.TimeStamp);
            if (_consoleVm.IsColumnShow(EnumColumns.Thread))
                buf.AppendFormat(" Thread[{0}]", data.ThreadName);

            buf.AppendFormat(" {0}", data.Level.ToString());

            if (_consoleVm.IsColumnShow(EnumColumns.Logger))
                buf.AppendFormat(" {0}", data.LoggerName);

            buf.AppendFormat(" - {0}", data.Message);
            buf.Append("</color>");
            buf.Append("\r\n\r\n");

            if (data.LocationInfo != null && data.LocationInfo.StackFrames != null)
            {
                foreach (var frame in data.LocationInfo.StackFrames)
                {
                    buf.Append(frame.FullInfo.Replace(Directory.GetCurrentDirectory().ToString() + @"\", ""))
                        .Append("\r\n");
                }
            }

            return buf.ToString();
        }

        private string[] GetTerminalInfoOptions()
        {
            return _consoleVm.TerminalInfos.Select(info => info.ToString()).ToArray();
        }

        private void DrawToolbar(LoggingContainer container)
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            if (_consoleVm.CurrentIndex >= 0)
                _consoleVm.CurrentIndex = EditorGUILayout.Popup(_consoleVm.CurrentIndex, GetTerminalInfoOptions(),
                    EditorStyles.toolbarDropDown, GUILayout.MinWidth(100), GUILayout.MaxWidth(200));

            GUILayout.Space(5f);

            _collapseToggle.Value = _consoleVm.Collapse;
            Toggle(_collapseToggle, () => { _consoleVm.Collapse = _collapseToggle.Value; });

            GUILayout.Space(5f);

            DrawToolbarColumnButtons(container);

            GUILayout.Space(5f);

            GUILayout.FlexibleSpace();

            GUI.SetNextControlName("searchTextField");
            _consoleVm.FilterText = EditorGUILayout.TextField(_consoleVm.FilterText, _toolbarSeachTextFieldStyle,
                GUILayout.MinWidth(100), GUILayout.MaxWidth(500));
            if (GUILayout.Button("", _toolbarSeachCancelButtonStyle))
            {
                _consoleVm.FilterText = "";
                GUI.FocusControl("");
            }

            GUILayout.FlexibleSpace();

            GUILayout.Space(5f);

            _playToggle.Value = _consoleVm.PlayState;
            Toggle(_playToggle, () =>
            {
                _consoleVm.PlayState = _playToggle.Value;
                if (_playToggle.Value)
                    _consoleVm.Start();
                else
                    _consoleVm.Stop();
            });

            if (_consoleVm.PlayState)
            {
                if (GUILayout.Button("Running", EditorStyles.toolbarDropDown, GUILayout.Width(60)))
                {
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(
                        new GUIContent(string.Format("{0}:{1}", _consoleVm.GetLocalIPAddress().ToString(),
                            _consoleVm.Port.ToString())), false, delegate() { });
                    menu.ShowAsContext();
                }
            }
            else
            {
                _consoleVm.Port =
                    EditorGUILayout.IntField(_consoleVm.Port, EditorStyles.toolbarTextField, GUILayout.Width(52));
            }

            GUILayout.Space(5f);

            DrawToolbarLevelButtons(container);


            if (Button(_clearButton))
            {
                EditorApplication.delayCall += () => _consoleVm.ClearLoggingData();
            }

            if (Button(_saveButton))
            {
                EditorApplication.delayCall += () => _consoleVm.SaveLoggingData();
            }

            GUILayout.Space(5f);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawToolbarLevelButtons(LoggingContainer container)
        {
            for (int i = 0; i < _levelButtonDatas.Length; i++)
            {
                var data = _levelButtonDatas[i];
                var level = (Level)(i + 1);
                data.Value = _consoleVm.IsLevelShow(level);
                var count = container.GetCount(level);
                data.Text = count < 1000 ? count.ToString() : "999+";
                Toggle(data, () => { _consoleVm.SetLevelShow(level, data.Value); });
            }
        }

        private void DrawToolbarColumnButtons(LoggingContainer container)
        {
            for (var i = 0; i < _columnButtonDatas.Length; i++)
            {
                var data = _columnButtonDatas[i];
                var enumColumns = (EnumColumns)i;
                data.Value = _consoleVm.IsColumnShow(enumColumns);
                Toggle(data, () => { _consoleVm.SetColumnShow(enumColumns, data.Value); });
            }
        }

        private bool ShouldShow(LoggingEntry logging)
        {
            if (!_consoleVm.IsLevelShow(logging.Level))
                return false;

            if (string.IsNullOrEmpty(_consoleVm.FilterText))
                return true;

            if (Regex.IsMatch(logging.Message, _consoleVm.FilterText) || (_consoleVm.IsColumnShow(EnumColumns.Logger) &&
                                                                         Regex.IsMatch(logging.LoggerName,
                                                                             _consoleVm.FilterText)))
                return true;

            return false;
        }

        private void DrawLoggingGrid(LoggingContainer container)
        {
            var areaRect = new Rect(0f, _toolbarHeight, position.width,
                position.height * _verticalSplitterPercent - _toolbarHeight - _splitterRectHeight / 2f);
            List<LoggingEntry> list = container.GetLoggingList();

            _renderedList.Clear();
            _renderedLineCountList.Clear();

            foreach (LoggingEntry logging in list)
            {
                if (!ShouldShow(logging))
                    continue;

                if (_consoleVm.Collapse)
                {
                    _renderedList.Add(logging.LoggingData);
                    _renderedLineCountList.Add(logging.Count);
                }
                else
                    _renderedList.AddRange(logging.LoggingDataList);
            }

            var count = _renderedList.Count;
            var viewRect = new Rect(0, 0, areaRect.width - 20, count * _lineHeight);

            if (viewRect.height >= areaRect.height && _loggingVerticalScrollBarPercent > 0.95f)
                _loggingPanelScrollPosition.y = viewRect.height - areaRect.height;

            _loggingPanelScrollPosition = GUI.BeginScrollView(areaRect, _loggingPanelScrollPosition, viewRect);
            if (viewRect.height >= areaRect.height)
                _loggingVerticalScrollBarPercent =
                    _loggingPanelScrollPosition.y / (viewRect.height - areaRect.height);

            var firstIndex = (int)(_loggingPanelScrollPosition.y / _lineHeight);
            var lastIndex = firstIndex + (int)(areaRect.height / _lineHeight);

            firstIndex = Mathf.Clamp(firstIndex - 5, 0, count);
            lastIndex = Mathf.Clamp(lastIndex + 5, 0, count);

            if (_selectedIndex >= count)
                _selectedIndex = -1;

            for (var i = firstIndex; i < lastIndex; i++)
            {
                var data = _renderedList[i];
                var content = GetLogLineGUIContent(data);
                var lineStyle = (i % 2 == 0) ? _entryStyleBackEven : _entryStyleBackOdd;

                var selected = i == _selectedIndex;
                lineStyle.normal = selected
                    ? GUI.skin.GetStyle(lineStyle.name).onNormal
                    : GUI.skin.GetStyle(lineStyle.name).normal;

                if (GUI.Button(new Rect(0f, i * _lineHeight, viewRect.width, _lineHeight), content, lineStyle))
                {
                    if (selected)
                    {
                        if (EditorApplication.timeSinceStartup - _lastClickTime < _doubleClickInterval)
                        {
                            _lastClickTime = 0;
                            OpenSourceFile(data.LocationInfo);
                        }
                        else
                        {
                            _lastClickTime = EditorApplication.timeSinceStartup;
                        }
                    }
                    else
                    {
                        _selectedIndex = i;
                        _lastClickTime = EditorApplication.timeSinceStartup;
                    }
                }

                if (_consoleVm.Collapse)
                {
                    var logCount = _renderedLineCountList[i];
                    var countContent = new GUIContent(logCount < 100 ? logCount.ToString() : "99+");
                    var size = _countBadgeStyle.CalcSize(countContent);
                    size.x = Mathf.Clamp(size.x + 5, 20, 30);
                    size.y = Mathf.Clamp(size.y, 20, _lineHeight);
                    GUI.Label(
                        new Rect(viewRect.width - 15f - size.x / 2f, i * _lineHeight + (_lineHeight - size.y) / 2f,
                            size.x, size.y), countContent, _countBadgeStyle);
                }
            }

            GUI.EndScrollView();
        }

        private void DrawLoggingDetail()
        {
            var areaRect = new Rect(0f, _verticalSplitterLineRect.y + _splitterRectHeight / 2f, position.width,
                position.height * (1f - _verticalSplitterPercent) - _splitterRectHeight / 2f);
            GUILayout.BeginArea(areaRect);
            _detailPanelScrollPosition = EditorGUILayout.BeginScrollView(_detailPanelScrollPosition);

            if (_selectedIndex >= 0 && _selectedIndex < _renderedList.Count)
            {
                var data = _renderedList[_selectedIndex];
                EditorGUILayout.SelectableLabel(GetLogDetailContent(data), _detailStyle,
                    GUILayout.Width(areaRect.width - 10), GUILayout.Height(areaRect.height - 10));
            }

            EditorGUILayout.EndScrollView();
            GUILayout.EndArea();
        }

        private void DrawVerticalSplitter()
        {
            if (_splitterLineTexture == null)
                return;

            _verticalSplitterLineRect.Set(_verticalSplitterLineRect.x, position.height * _verticalSplitterPercent,
                position.width, 1);
            _verticalSplitterRect.Set(_verticalSplitterLineRect.x, _verticalSplitterLineRect.y - _splitterRectHeight / 2f,
                _verticalSplitterLineRect.width, _splitterRectHeight);

            GUI.DrawTexture(_verticalSplitterLineRect, _splitterLineTexture);
            EditorGUIUtility.AddCursorRect(_verticalSplitterRect, MouseCursor.ResizeVertical);

            if (Event.current.type == EventType.MouseDown && _verticalSplitterRect.Contains(Event.current.mousePosition))
                _resizingVerticalSplitter = true;

            if (_resizingVerticalSplitter)
            {
                _verticalSplitterPercent =
                    Mathf.Clamp(Event.current.mousePosition.y / position.height, 0.15f, 0.9f);
                _verticalSplitterLineRect.y = position.height * _verticalSplitterPercent;
            }

            if (Event.current.type == EventType.MouseUp)
                _resizingVerticalSplitter = false;
        }

        private static bool Button(GUIContentData model)
        {
            if (model == null)
                return false;

            return GUILayout.Button(model.Content, model.Style, model.LayoutOptions);
        }

        private static bool Toggle(GUISwitchContentData model, Action onValueChanged = null)
        {
            if (model == null)
                return false;

            var result = GUILayout.Toggle(model.Value, model.Content, model.Style, model.LayoutOptions);
            if (result != model.Value)
            {
                model.Value = result;
                if (onValueChanged != null)
                    onValueChanged();
            }

            return result;
        }

        private static bool OpenSourceFile(LocationInfo location)
        {
            if (location?.StackFrames == null)
                return false;

            foreach (var frame in location.StackFrames)
            {
                try
                {
                    if (frame == null)
                        continue;

                    string fileName = frame.FileName;
                    if (string.IsNullOrEmpty(fileName))
                        continue;

                    var dir = Directory.GetCurrentDirectory().ToString();
                    if (!fileName.StartsWith(dir))
                        continue;

                    if (!File.Exists(fileName))
                        continue;

                    if (Regex.IsMatch(fileName, @"Log4NetLogImpl.cs"))
                        continue;

                    if (InternalEditorUtility.OpenFileAtLineExternal(fileName, frame.LineNumber))
                        return true;
                }
                catch (Exception)
                {
                }
            }

            return false;
        }

        [Serializable]
        class GUIContentData
        {
            protected bool _dirty;
            protected GUIContent _content;
            protected GUIStyle _style;
            protected GUILayoutOption[] _layoutOptions;

            public GUIContentData(GUIContent content, GUIStyle style)
            {
                _dirty = true;
                _content = content;
                _style = style;
                Vector2 size = style.CalcSize(content);
                _layoutOptions = new GUILayoutOption[] { GUILayout.Width(size.x) };
            }

            public string Text
            {
                get => _content.text;
                set
                {
                    _content.text = value;
                    _dirty = true;
                }
            }

            public Texture Image
            {
                get => _content.image;
                set
                {
                    _content.image = value;
                    _dirty = true;
                }
            }

            public GUIContent Content => _content;

            public GUIStyle Style => _style;


            public GUILayoutOption[] LayoutOptions
            {
                get
                {
                    if (_dirty)
                    {
                        Vector2 size = _style.CalcSize(Content);
                        _layoutOptions = new GUILayoutOption[] { GUILayout.Width(size.x) };
                        _dirty = false;
                    }

                    return _layoutOptions;
                }
            }
        }


        [Serializable]
        class GUISwitchContentData
        {
            protected bool _dirty;
            protected bool _value;
            protected GUIContent _contentOn;
            protected GUIContent _contentOff;
            protected GUIStyle _style;
            protected GUILayoutOption[] _layoutOptions;

            public GUISwitchContentData(bool value, GUIContent contentOn, GUIStyle style) : this(value, contentOn, null,
                style)
            {
            }

            public GUISwitchContentData(bool value, GUIContent contentOn, GUIContent contentOff, GUIStyle style)
            {
                _dirty = true;
                _value = value;
                _contentOn = contentOn;
                _contentOff = contentOff != null ? contentOff : contentOn;
                _style = style;
                _layoutOptions = null;
            }

            public bool Value
            {
                get => _value;
                set
                {
                    if (_value == value)
                        return;

                    _value = value;
                    _dirty = true;
                }
            }

            public string Text
            {
                get => Content.text;
                set
                {
                    if (string.Equals(Content.text, value))
                        return;

                    Content.text = value;
                    _dirty = true;
                }
            }

            public Texture Image
            {
                get => Content.image;
                set
                {
                    Content.image = value;
                    _dirty = true;
                }
            }

            public GUIContent Content => _value ? _contentOn : _contentOff;

            public GUIStyle Style => _style;

            public GUILayoutOption[] LayoutOptions
            {
                get
                {
                    if (_dirty)
                    {
                        Vector2 size = _style.CalcSize(Content);
                        _layoutOptions = new GUILayoutOption[] { GUILayout.Width(size.x) };
                        _dirty = false;
                    }

                    return _layoutOptions;
                }
            }
        }
    }
}