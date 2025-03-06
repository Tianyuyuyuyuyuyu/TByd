using TByd.PackageCreator.Editor.UI.Styles;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Controls
{
    /// <summary>
    /// 模板卡片控件，用于显示和选择包模板
    /// </summary>
    public class TemplateCardControl
    {
        #region 字段

        private readonly string _title;
        private readonly string _description;
        private readonly string _details;
        private readonly Texture2D _icon;
        private readonly bool _isSelected;

        #endregion

        #region 构造函数

        /// <summary>
        /// 创建模板卡片控件
        /// </summary>
        /// <param name="title">模板标题</param>
        /// <param name="description">模板简要描述</param>
        /// <param name="details">模板详细信息</param>
        /// <param name="icon">模板图标</param>
        /// <param name="isSelected">是否选中</param>
        public TemplateCardControl(string title, string description, string details, Texture2D icon, bool isSelected)
        {
            _title = title;
            _description = description;
            _details = details;
            _icon = icon;
            _isSelected = isSelected;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 绘制模板卡片
        /// </summary>
        /// <returns>如果卡片被点击则返回true</returns>
        public bool Draw()
        {
            bool result = false;

            // 选择适当的样式
            GUIStyle cardStyle = _isSelected ? PackageCreatorStyles.SelectedCard : PackageCreatorStyles.Card;

            // 允许事件处理
            Rect cardRect = GUILayoutUtility.GetRect(GUIContent.none, cardStyle, GUILayout.ExpandWidth(true), GUILayout.Height(100));

            // 处理鼠标事件
            Event evt = Event.current;
            if (evt.type == EventType.MouseDown && cardRect.Contains(evt.mousePosition))
            {
                result = true;
                evt.Use(); // 使用该事件避免其他控件处理
            }

            // 背景
            GUI.Box(cardRect, GUIContent.none, cardStyle);

            // 内容区域
            Rect contentRect = new Rect(cardRect.x + 10, cardRect.y + 10, cardRect.width - 20, cardRect.height - 20);

            // 图标和标题
            Rect iconRect = new Rect(contentRect.x, contentRect.y, 32, 32);
            Rect titleRect = new Rect(iconRect.xMax + 10, contentRect.y, contentRect.width - iconRect.width - 10, 20);

            // 描述区域
            Rect descriptionRect = new Rect(contentRect.x, titleRect.yMax + 5, contentRect.width, 40);

            // 绘制内容
            if (_icon != null)
            {
                GUI.DrawTexture(iconRect, _icon, ScaleMode.ScaleToFit);
            }

            // 调整选中状态下的文本颜色
            Color originalColor = GUI.color;
            if (_isSelected)
            {
                GUI.color = Color.white;
            }

            // 绘制标题
            GUI.Label(titleRect, _title, EditorStyles.boldLabel);

            // 描述
            GUI.Label(descriptionRect, _description, EditorStyles.wordWrappedLabel);

            // 恢复颜色
            GUI.color = originalColor;

            return result;
        }

        /// <summary>
        /// 绘制带有详细信息的完整模板卡片
        /// </summary>
        /// <returns>如果卡片被点击则返回true</returns>
        public bool DrawDetailed()
        {
            bool cardClicked = false;

            // 开始卡片区域
            PackageCreatorStyles.BeginGroup();

            // 绘制基本卡片
            cardClicked = Draw();

            // 如果选中，则显示详细信息
            if (_isSelected && !string.IsNullOrEmpty(_details))
            {
                PackageCreatorStyles.DrawSeparator();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("详细信息", EditorStyles.boldLabel);
                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField(_details, EditorStyles.wordWrappedLabel);
                EditorGUILayout.EndVertical();
            }

            // 结束卡片区域
            PackageCreatorStyles.EndGroup();

            return cardClicked;
        }

        #endregion
    }
}
