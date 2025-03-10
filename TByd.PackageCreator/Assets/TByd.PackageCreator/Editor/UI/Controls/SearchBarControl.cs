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

        // 清除图标
        private static Texture2D _clearIcon;

        // 搜索框样式
        private static GUIStyle _searchFieldStyle;

        // 搜索按钮样式
        private static GUIStyle _searchButtonStyle;

        // 清除按钮样式
        private static GUIStyle _clearButtonStyle;

        /// <summary>
        /// 初始化资源
        /// </summary>
        private static void InitializeResources()
        {
            if (_searchIcon == null)
            {
                _searchIcon = EditorGUIUtility.FindTexture("Search Icon");
            }

            if (_clearIcon == null)
            {
                _clearIcon = EditorGUIUtility.FindTexture("d_winbtn_win_close");
            }

            if (_searchFieldStyle == null)
            {
                _searchFieldStyle = new GUIStyle(EditorStyles.toolbarSearchField)
                {
                    margin = new RectOffset(0, 0, 2, 2),
                    padding = new RectOffset(20, 20, 0, 0),
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

            if (_clearButtonStyle == null)
            {
                _clearButtonStyle = new GUIStyle(EditorStyles.label)
                {
                    margin = new RectOffset(0, 4, 4, 0),
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

            // 绘制搜索图标
            Rect searchIconRect = new Rect(controlRect.x + 4, controlRect.y + 3, 16, 16);
            GUI.Label(searchIconRect, _searchIcon, _searchButtonStyle);

            // 绘制搜索框
            string newSearchText = searchText;

            // 如果搜索文本为空，显示占位符
            if (string.IsNullOrEmpty(searchText))
            {
                // 绘制占位符
                GUI.enabled = false;
                EditorGUI.TextField(controlRect, placeholder, _searchFieldStyle);
                GUI.enabled = true;

                // 处理用户输入
                newSearchText = EditorGUI.TextField(controlRect, "", _searchFieldStyle);
            }
            else
            {
                // 绘制搜索文本
                newSearchText = EditorGUI.TextField(controlRect, searchText, _searchFieldStyle);

                // 绘制清除按钮
                Rect clearIconRect = new Rect(controlRect.xMax - 20, controlRect.y + 3, 16, 16);
                if (GUI.Button(clearIconRect, _clearIcon, _clearButtonStyle))
                {
                    newSearchText = "";
                    GUI.FocusControl(null);
                    // 立即重绘UI，确保变更立即生效
                    EditorGUIUtility.ExitGUI();
                }
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
