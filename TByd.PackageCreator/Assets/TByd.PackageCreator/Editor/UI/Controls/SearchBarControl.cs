using System;
using TByd.PackageCreator.Editor.UI.Styles;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Controls
{
    /// <summary>
    /// 搜索栏控件
    /// </summary>
    public static class SearchBarControl
    {
        // 搜索图标
        private static Texture2D _searchIcon;

        // 搜索框样式
        private static GUIStyle _searchFieldStyle;

        // 搜索按钮样式
        private static GUIStyle _searchButtonStyle;

        /// <summary>
        /// 初始化资源
        /// </summary>
        private static void InitializeResources()
        {
            if (_searchIcon == null)
            {
                _searchIcon = EditorGUIUtility.FindTexture("Search Icon");
            }

            if (_searchFieldStyle == null)
            {
                _searchFieldStyle = new GUIStyle(EditorStyles.toolbarSearchField)
                {
                    margin = new RectOffset(0, 0, 2, 2),
                    padding = new RectOffset(20, 5, 0, 0),
                    fixedHeight = 22
                };
            }

            if (_searchButtonStyle == null)
            {
                _searchButtonStyle = new GUIStyle(EditorStyles.label)
                {
                    margin = new RectOffset(4, 0, 4, 0),
                    padding = new RectOffset(0, 0, 0, 0)
                };
            }
        }

        /// <summary>
        /// 绘制搜索栏
        /// </summary>
        /// <param name="searchText">当前搜索文本</param>
        /// <param name="placeholder">占位符文本</param>
        /// <param name="width">宽度，默认为0（自动宽度）</param>
        /// <returns>用户输入的新搜索文本</returns>
        public static string Draw(string searchText, string placeholder = "搜索...", float width = 0)
        {
            InitializeResources();

            // 计算控件区域
            Rect controlRect;
            if (width > 0)
            {
                controlRect = EditorGUILayout.GetControlRect(false, _searchFieldStyle.fixedHeight, _searchFieldStyle, GUILayout.Width(width));
            }
            else
            {
                controlRect = EditorGUILayout.GetControlRect(false, _searchFieldStyle.fixedHeight, _searchFieldStyle);
            }

            // 为搜索框创建唯一的控件ID
            string controlName = "SearchBarControl" + controlRect.GetHashCode();
            GUI.SetNextControlName(controlName);

            // 绘制搜索图标
            Rect searchIconRect = new Rect(controlRect.x + 4, controlRect.y + 3, 16, 16);
            GUI.Label(searchIconRect, _searchIcon, _searchButtonStyle);

            // 绘制搜索框
            string newSearchText;

            // 如果搜索文本为空，显示占位符
            if (string.IsNullOrEmpty(searchText))
            {
                // 创建一个临时的样式来显示占位符
                GUIStyle placeholderStyle = new GUIStyle(_searchFieldStyle);
                placeholderStyle.normal.textColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);

                // 绘制实际的输入框
                newSearchText = EditorGUI.TextField(controlRect, "", _searchFieldStyle);

                // 如果输入框为空，绘制占位符文本
                if (string.IsNullOrEmpty(newSearchText) && GUI.GetNameOfFocusedControl() != controlName)
                {
                    // 使用 GUI.Label 绘制占位符，避免与输入冲突
                    EditorGUI.LabelField(controlRect, placeholder, placeholderStyle);
                }
            }
            else
            {
                // 绘制搜索文本
                newSearchText = EditorGUI.TextField(controlRect, searchText, _searchFieldStyle);
            }

            return newSearchText;
        }

        /// <summary>
        /// 绘制带标签的搜索栏
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="searchText">当前搜索文本</param>
        /// <param name="placeholder">占位符文本</param>
        /// <param name="width">宽度，默认为0（自动宽度）</param>
        /// <returns>用户输入的新搜索文本</returns>
        public static string DrawWithLabel(string label, string searchText, string placeholder = "搜索...", float width = 0)
        {
            EditorGUILayout.BeginHorizontal();

            // 绘制标签
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));

            // 绘制搜索栏
            string result = Draw(searchText, placeholder, width);

            EditorGUILayout.EndHorizontal();

            return result;
        }
    }
}
