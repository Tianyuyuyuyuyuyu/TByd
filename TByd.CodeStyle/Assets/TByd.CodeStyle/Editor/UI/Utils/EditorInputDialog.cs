using UnityEngine;
using UnityEditor;

namespace TByd.CodeStyle.Editor.UI.Utils
{
    /// <summary>
    /// 编辑器输入对话框
    /// </summary>
    public class EditorInputDialog : EditorWindow
    {
        private string m_Title;
        private string m_Message;
        private string m_Input;
        private string m_DefaultValue;
        private bool m_IsCanceled;
        private bool m_IsInitialized;

        /// <summary>
        /// 显示输入对话框
        /// </summary>
        /// <param name="_title">标题</param>
        /// <param name="_message">提示信息</param>
        /// <param name="_defaultValue">默认值</param>
        /// <returns>用户输入的文本</returns>
        public static string Show(string _title, string _message, string _defaultValue = "")
        {
            var window = CreateInstance<EditorInputDialog>();
            window.titleContent = new GUIContent(_title);
            window.m_Title = _title;
            window.m_Message = _message;
            window.m_DefaultValue = _defaultValue;
            window.minSize = new Vector2(300, 100);
            window.maxSize = new Vector2(300, 100);
            window.ShowModal();
            return window.m_IsCanceled ? null : window.m_Input;
        }

        private void OnEnable()
        {
            m_Input = m_DefaultValue;
            m_IsCanceled = false;
            m_IsInitialized = false;
        }

        private void OnGUI()
        {
            // 确保窗口获得焦点
            if (!m_IsInitialized)
            {
                EditorGUI.FocusTextInControl("InputField");
                m_IsInitialized = true;
            }

            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField(m_Message);
            EditorGUILayout.Space(5);

            // 处理回车和ESC键
            var e = Event.current;
            if (e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.Return)
                {
                    Close();
                    e.Use();
                }
                else if (e.keyCode == KeyCode.Escape)
                {
                    m_IsCanceled = true;
                    Close();
                    e.Use();
                }
            }

            // 输入框
            GUI.SetNextControlName("InputField");
            m_Input = EditorGUILayout.TextField(m_Input);

            EditorGUILayout.Space(10);

            // 按钮
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("确定"))
                {
                    Close();
                }

                if (GUILayout.Button("取消"))
                {
                    m_IsCanceled = true;
                    Close();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
} 