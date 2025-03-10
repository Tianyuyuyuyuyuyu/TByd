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

        private readonly string _mTitle;
        private readonly string _mDescription;
        private readonly string _mDetails;
        private readonly Texture2D _mIcon;
        private readonly bool _mIsSelected;

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
            _mTitle = title;
            _mDescription = description;
            _mDetails = details;
            _mIcon = icon;
            _mIsSelected = isSelected;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 绘制模板卡片
        /// </summary>
        /// <returns>如果卡片被点击则返回true</returns>
        public bool Draw()
        {
            var result = false;

            // 选择适当的样式
            var cardStyle = _mIsSelected ? PackageCreatorStyles.SelectedCard : PackageCreatorStyles.Card;

            // 允许事件处理
            var cardRect = GUILayoutUtility.GetRect(GUIContent.none, cardStyle, GUILayout.ExpandWidth(true), GUILayout.Height(100));

            // 处理鼠标事件
            var evt = Event.current;
            if (evt.type == EventType.MouseDown && cardRect.Contains(evt.mousePosition))
            {
                result = true;
                evt.Use(); // 使用该事件避免其他控件处理
            }

            // 背景
            GUI.Box(cardRect, GUIContent.none, cardStyle);

            // 内容区域
            var contentRect = new Rect(cardRect.x + 10, cardRect.y + 10, cardRect.width - 20, cardRect.height - 20);

            // 图标和标题
            var iconRect = new Rect(contentRect.x, contentRect.y, 32, 32);
            var titleRect = new Rect(iconRect.xMax + 10, contentRect.y, contentRect.width - iconRect.width - 10, 20);

            // 描述区域
            var descriptionRect = new Rect(contentRect.x, titleRect.yMax + 5, contentRect.width, 40);

            // 绘制内容
            if (_mIcon)
            {
                GUI.DrawTexture(iconRect, _mIcon, ScaleMode.ScaleToFit);
            }

            // 调整选中状态下的文本颜色
            var originalColor = GUI.color;
            if (_mIsSelected)
            {
                GUI.color = Color.white;
            }

            // 绘制标题
            GUI.Label(titleRect, _mTitle, EditorStyles.boldLabel);

            // 描述
            GUI.Label(descriptionRect, _mDescription, EditorStyles.wordWrappedLabel);

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
            // 开始卡片区域
            PackageCreatorStyles.BeginGroup();

            // 绘制基本卡片
            var cardClicked = Draw();

            // 如果选中，则显示详细信息
            if (_mIsSelected && !string.IsNullOrEmpty(_mDetails))
            {
                PackageCreatorStyles.DrawSeparator();

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("详细信息", EditorStyles.boldLabel);
                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField(_mDetails, EditorStyles.wordWrappedLabel);
                EditorGUILayout.EndVertical();
            }

            // 结束卡片区域
            PackageCreatorStyles.EndGroup();

            return cardClicked;
        }

        /// <summary>
        /// 绘制紧凑版模板卡片，适合左右分栏布局
        /// </summary>
        /// <returns>如果卡片被点击则返回true</returns>
        public bool DrawCompact()
        {
            var result = false;

            // 选择适当的样式
            var cardStyle = _mIsSelected ? PackageCreatorStyles.SelectedCard : PackageCreatorStyles.Card;

            // 创建更大字体的标题样式
            var titleStyle = new GUIStyle(EditorStyles.boldLabel);
            titleStyle.fontSize = 13; // 增大字体大小

            // 创建更大字体的描述样式，确保自动换行
            var descStyle = new GUIStyle(EditorStyles.wordWrappedMiniLabel);
            descStyle.fontSize = 12; // 增大字体大小
            descStyle.wordWrap = true; // 确保文本自动换行

            // 计算描述文本的高度
            float descHeight = descStyle.CalcHeight(new GUIContent(_mDescription), EditorGUIUtility.currentViewWidth * 0.45f - 60);
            // 确保最小高度为25
            descHeight = Mathf.Max(25, descHeight);

            // 根据描述文本高度动态计算卡片高度
            float cardHeight = 40 + descHeight; // 基础高度 + 描述文本高度

            // 预先分配一个矩形区域（不是开始一个组）
            var cardRect = GUILayoutUtility.GetRect(GUIContent.none, cardStyle, GUILayout.ExpandWidth(true), GUILayout.Height(cardHeight));

            // 处理点击事件
            var evt = Event.current;
            if (evt.type == EventType.MouseDown && cardRect.Contains(evt.mousePosition))
            {
                result = true;
                evt.Use();
            }

            // 绘制卡片背景
            GUI.Box(cardRect, GUIContent.none, cardStyle);

            // 计算内容区域
            var contentRect = new Rect(cardRect.x + 10, cardRect.y + 5, cardRect.width - 20, cardRect.height - 10);

            // 图标区域
            if (_mIcon)
            {
                var iconRect = new Rect(contentRect.x, contentRect.y + 3, 30, 30);
                GUI.DrawTexture(iconRect, _mIcon, ScaleMode.ScaleToFit);
                contentRect.x += 38; // 移动到图标右侧
                contentRect.width -= 38;
            }

            // 如果选中，显示标记
            if (_mIsSelected)
            {
                var checkRect = new Rect(contentRect.x + contentRect.width - 18, contentRect.y + 5, 20, 20);
                GUI.Label(checkRect, "✓", EditorStyles.boldLabel);
                contentRect.width -= 22; // 为选中标记留出空间
            }

            // 标题
            var titleRect = new Rect(contentRect.x, contentRect.y, contentRect.width, 20);
            GUI.Label(titleRect, _mTitle, titleStyle);

            // 描述 - 使用计算出的高度
            var descRect = new Rect(contentRect.x, titleRect.y + titleRect.height + 2, contentRect.width, descHeight);
            GUI.Label(descRect, _mDescription, descStyle);

            // 额外的空间用于卡片间隔
            GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(2));

            return result;
        }

        #endregion
    }
}
