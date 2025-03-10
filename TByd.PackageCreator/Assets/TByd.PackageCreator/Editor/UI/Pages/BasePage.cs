using TByd.PackageCreator.Editor.UI.Styles;
using TByd.PackageCreator.Editor.UI.Utils;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Pages
{
    /// <summary>
    /// 基础页面类，提供页面通用功能
    /// </summary>
    public abstract class BasePage : IPage
    {
        /// <summary>
        /// 页面标题
        /// </summary>
        public abstract string Title { get; }

        /// <summary>
        /// 页面是否有效
        /// </summary>
        public abstract bool IsValid { get; }

        /// <summary>
        /// 进入页面时调用
        /// </summary>
        public virtual void OnEnter() { }

        /// <summary>
        /// 离开页面时调用
        /// </summary>
        public virtual void OnExit() { }

        /// <summary>
        /// 绘制页面内容
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// 绘制页面标题
        /// </summary>
        protected void DrawTitle()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(Title, PackageCreatorStyles.HeaderLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);
        }

        /// <summary>
        /// 绘制分隔线
        /// </summary>
        protected void DrawSeparator()
        {
            EditorGUILayout.Space(5);
            PackageCreatorStyles.DrawSeparator();
            EditorGUILayout.Space(5);
        }

        /// <summary>
        /// 绘制分组标题
        /// </summary>
        /// <param name="title">标题文本</param>
        protected void DrawGroupTitle(string title)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(title, PackageCreatorStyles.TitleLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(5);
        }

        /// <summary>
        /// 绘制帮助框
        /// </summary>
        /// <param name="message">帮助信息</param>
        /// <param name="type">消息类型</param>
        protected void DrawHelpBox(string message, MessageType type = MessageType.Info)
        {
            EditorGUILayout.HelpBox(message, type);
            EditorGUILayout.Space(5);
        }

        /// <summary>
        /// 开始垂直分组
        /// </summary>
        /// <param name="title">分组标题，为null则不显示标题</param>
        protected void BeginVerticalGroup(string title = null)
        {
            PackageCreatorStyles.BeginGroup(title);
        }

        /// <summary>
        /// 结束垂直分组
        /// </summary>
        protected void EndVerticalGroup()
        {
            PackageCreatorStyles.EndGroup();
        }
    }
}
