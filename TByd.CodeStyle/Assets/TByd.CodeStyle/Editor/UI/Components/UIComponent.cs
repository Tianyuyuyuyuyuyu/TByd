using UnityEditor;

namespace TByd.CodeStyle.Editor.UI.Components
{
    /// <summary>
    /// UI组件的基类，提供基础功能和接口
    /// </summary>
    public abstract class UIComponent
    {
        /// <summary>
        /// 组件描述
        /// </summary>
        protected string m_Description;

        /// <summary>
        /// 是否已折叠
        /// </summary>
        protected bool m_IsCollapsed;

        /// <summary>
        /// 是否可折叠
        /// </summary>
        protected bool m_IsCollapsible;

        /// <summary>
        /// 组件是否可见
        /// </summary>
        protected bool m_IsVisible = true;

        /// <summary>
        /// 组件标题
        /// </summary>
        protected string m_Title;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="title">组件标题</param>
        /// <param name="description">组件描述</param>
        /// <param name="isCollapsible">是否可折叠</param>
        protected UIComponent(string title, string description = "", bool isCollapsible = false)
        {
            m_Title = title;
            m_Description = description;
            m_IsCollapsible = isCollapsible;
            m_IsCollapsed = false;
        }

        /// <summary>
        /// 组件标题
        /// </summary>
        public string Title
        {
            get => m_Title;
            set => m_Title = value;
        }

        /// <summary>
        /// 组件描述
        /// </summary>
        public string Description
        {
            get => m_Description;
            set => m_Description = value;
        }

        /// <summary>
        /// 绘制组件
        /// </summary>
        public virtual void Draw()
        {
            if (!m_IsVisible)
            {
                return;
            }

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            DrawHeader();

            if (!m_IsCollapsible || !m_IsCollapsed)
            {
                DrawContent();
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制组件头部
        /// </summary>
        protected virtual void DrawHeader()
        {
            EditorGUILayout.BeginHorizontal();

            if (m_IsCollapsible)
            {
                m_IsCollapsed = EditorGUILayout.Foldout(m_IsCollapsed, m_Title, true);
            }
            else
            {
                EditorGUILayout.LabelField(m_Title, EditorStyles.boldLabel);
            }

            EditorGUILayout.EndHorizontal();

            if (!string.IsNullOrEmpty(m_Description))
            {
                EditorGUILayout.HelpBox(m_Description, MessageType.None);
            }
        }

        /// <summary>
        /// 绘制组件内容
        /// </summary>
        protected abstract void DrawContent();

        /// <summary>
        /// 设置组件可见性
        /// </summary>
        /// <param name="isVisible">是否可见</param>
        public void SetVisible(bool isVisible)
        {
            m_IsVisible = isVisible;
        }

        /// <summary>
        /// 设置组件标题
        /// </summary>
        /// <param name="title">标题</param>
        public void SetTitle(string title)
        {
            m_Title = title;
        }

        /// <summary>
        /// 设置组件描述
        /// </summary>
        /// <param name="description">描述</param>
        public void SetDescription(string description)
        {
            m_Description = description;
        }

        /// <summary>
        /// 切换折叠状态
        /// </summary>
        public void ToggleCollapsed()
        {
            if (m_IsCollapsible)
            {
                m_IsCollapsed = !m_IsCollapsed;
            }
        }
    }
}
