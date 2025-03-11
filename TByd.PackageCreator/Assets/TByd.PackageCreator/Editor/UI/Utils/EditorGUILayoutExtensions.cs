using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Utils
{
    /// <summary>
    /// EditorGUILayout扩展方法类
    /// </summary>
    public static class EditorGUILayoutExtensions
    {
        /// <summary>
        /// 绘制带标签的文本字段
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="value">当前值</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户输入的新值</returns>
        public static string TextField(string label, string value, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            string result = EditorGUILayout.TextField(value, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的文本区域
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="value">当前值</param>
        /// <param name="height">文本区域高度</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户输入的新值</returns>
        public static string TextArea(string label, string value, float height = 60f, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth), GUILayout.Height(height));

            // 创建一个包含高度选项的新数组
            var allOptions = new List<GUILayoutOption> { GUILayout.Height(height) };
            if (options != null && options.Length > 0)
            {
                allOptions.AddRange(options);
            }

            string result = EditorGUILayout.TextArea(value, allOptions.ToArray());
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的整数字段
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="value">当前值</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户输入的新值</returns>
        public static int IntField(string label, int value, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            int result = EditorGUILayout.IntField(value, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的浮点数字段
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="value">当前值</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户输入的新值</returns>
        public static float FloatField(string label, float value, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            float result = EditorGUILayout.FloatField(value, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的切换按钮
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="value">当前值</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户选择的新值</returns>
        public static bool ToggleField(string label, bool value, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            bool result = EditorGUILayout.Toggle(value, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的弹出菜单
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="selectedIndex">当前选中索引</param>
        /// <param name="displayedOptions">显示选项</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户选择的新索引</returns>
        public static int PopupField(string label, int selectedIndex, string[] displayedOptions, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            int result = EditorGUILayout.Popup(selectedIndex, displayedOptions, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的枚举弹出菜单
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="label">标签文本</param>
        /// <param name="enumValue">当前枚举值</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户选择的新枚举值</returns>
        public static T EnumPopupField<T>(string label, T enumValue, params GUILayoutOption[] options) where T : Enum
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            T result = (T)EditorGUILayout.EnumPopup(enumValue, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的颜色字段
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="value">当前颜色值</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户选择的新颜色值</returns>
        public static Color ColorField(string label, Color value, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            Color result = EditorGUILayout.ColorField(value, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的对象字段
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="label">标签文本</param>
        /// <param name="obj">当前对象</param>
        /// <param name="allowSceneObjects">是否允许场景对象</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户选择的新对象</returns>
        public static T ObjectField<T>(string label, T obj, bool allowSceneObjects = false, params GUILayoutOption[] options) where T : UnityEngine.Object
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            T result = (T)EditorGUILayout.ObjectField(obj, typeof(T), allowSceneObjects, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的滑动条
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="value">当前值</param>
        /// <param name="leftValue">最小值</param>
        /// <param name="rightValue">最大值</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户调整的新值</returns>
        public static float SliderField(string label, float value, float leftValue, float rightValue, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            float result = EditorGUILayout.Slider(value, leftValue, rightValue, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的整数滑动条
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="value">当前值</param>
        /// <param name="leftValue">最小值</param>
        /// <param name="rightValue">最大值</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户调整的新值</returns>
        public static int IntSliderField(string label, int value, int leftValue, int rightValue, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            int result = EditorGUILayout.IntSlider(value, leftValue, rightValue, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的延迟文本字段（失去焦点时才更新值）
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="value">当前值</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户输入的新值</returns>
        public static string DelayedTextField(string label, string value, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            string result = EditorGUILayout.DelayedTextField(value, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的延迟整数字段（失去焦点时才更新值）
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="value">当前值</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户输入的新值</returns>
        public static int DelayedIntField(string label, int value, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            int result = EditorGUILayout.DelayedIntField(value, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }

        /// <summary>
        /// 绘制带标签的延迟浮点数字段（失去焦点时才更新值）
        /// </summary>
        /// <param name="label">标签文本</param>
        /// <param name="value">当前值</param>
        /// <param name="options">布局选项</param>
        /// <returns>用户输入的新值</returns>
        public static float DelayedFloatField(string label, float value, params GUILayoutOption[] options)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth));
            float result = EditorGUILayout.DelayedFloatField(value, options);
            EditorGUILayout.EndHorizontal();
            return result;
        }
    }
}
