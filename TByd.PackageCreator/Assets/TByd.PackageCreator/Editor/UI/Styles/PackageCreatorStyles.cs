using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Styles
{
    /// <summary>
    /// 提供包创建器UI的共享样式定义
    /// </summary>
    public static class PackageCreatorStyles
    {
        #region 常量

        private const int HEADER_FONT_SIZE = 18;
        private const int TITLE_FONT_SIZE = 14;
        private const int STANDARD_FONT_SIZE = 12;

        #endregion

        #region 字段

        // 标题样式
        private static GUIStyle s_headerLabelStyle;
        private static GUIStyle s_titleLabelStyle;

        // 描述样式
        private static GUIStyle s_descriptionStyle;

        // 按钮样式
        private static GUIStyle s_primaryButtonStyle;
        private static GUIStyle s_secondaryButtonStyle;

        // 卡片样式
        private static GUIStyle s_cardStyle;
        private static GUIStyle s_selectedCardStyle;

        // 分割线样式
        private static GUIStyle s_separatorStyle;

        // 图标尺寸
        private static readonly Vector2 s_largeIconSize = new Vector2(32, 32);
        private static readonly Vector2 s_mediumIconSize = new Vector2(24, 24);
        private static readonly Vector2 s_smallIconSize = new Vector2(16, 16);

        #endregion

        #region 属性

        /// <summary>
        /// 标题样式 - 用于主标题
        /// </summary>
        public static GUIStyle HeaderLabel
        {
            get
            {
                if (s_headerLabelStyle == null)
                {
                    s_headerLabelStyle = new GUIStyle(EditorStyles.largeLabel)
                    {
                        fontSize = HEADER_FONT_SIZE,
                        fontStyle = FontStyle.Bold,
                        alignment = TextAnchor.MiddleLeft,
                        margin = new RectOffset(0, 0, 10, 10)
                    };
                }
                return s_headerLabelStyle;
            }
        }

        /// <summary>
        /// 标题样式 - 用于小标题
        /// </summary>
        public static GUIStyle TitleLabel
        {
            get
            {
                if (s_titleLabelStyle == null)
                {
                    s_titleLabelStyle = new GUIStyle(EditorStyles.boldLabel)
                    {
                        fontSize = TITLE_FONT_SIZE,
                        alignment = TextAnchor.MiddleLeft,
                        margin = new RectOffset(0, 0, 5, 5)
                    };
                }
                return s_titleLabelStyle;
            }
        }

        /// <summary>
        /// 描述文本样式
        /// </summary>
        public static GUIStyle Description
        {
            get
            {
                if (s_descriptionStyle == null)
                {
                    s_descriptionStyle = new GUIStyle(EditorStyles.label)
                    {
                        wordWrap = true,
                        fontSize = STANDARD_FONT_SIZE,
                        margin = new RectOffset(5, 5, 5, 10)
                    };
                }
                return s_descriptionStyle;
            }
        }

        /// <summary>
        /// 主按钮样式
        /// </summary>
        public static GUIStyle PrimaryButton
        {
            get
            {
                if (s_primaryButtonStyle == null)
                {
                    s_primaryButtonStyle = new GUIStyle(GUI.skin.button)
                    {
                        fontSize = STANDARD_FONT_SIZE,
                        fontStyle = FontStyle.Bold,
                        fixedHeight = 30,
                        margin = new RectOffset(10, 10, 5, 5),
                        padding = new RectOffset(15, 15, 5, 5)
                    };
                }
                return s_primaryButtonStyle;
            }
        }

        /// <summary>
        /// 次要按钮样式
        /// </summary>
        public static GUIStyle SecondaryButton
        {
            get
            {
                if (s_secondaryButtonStyle == null)
                {
                    s_secondaryButtonStyle = new GUIStyle(GUI.skin.button)
                    {
                        fontSize = STANDARD_FONT_SIZE,
                        fixedHeight = 25,
                        margin = new RectOffset(5, 5, 5, 5),
                        padding = new RectOffset(10, 10, 3, 3)
                    };
                }
                return s_secondaryButtonStyle;
            }
        }

        /// <summary>
        /// 卡片样式 - 用于选项卡或列表项
        /// </summary>
        public static GUIStyle Card
        {
            get
            {
                if (s_cardStyle == null)
                {
                    s_cardStyle = new GUIStyle(EditorStyles.helpBox)
                    {
                        margin = new RectOffset(5, 5, 5, 5),
                        padding = new RectOffset(10, 10, 10, 10)
                    };
                }
                return s_cardStyle;
            }
        }

        /// <summary>
        /// 选中状态的卡片样式
        /// </summary>
        public static GUIStyle SelectedCard
        {
            get
            {
                if (s_selectedCardStyle == null)
                {
                    s_selectedCardStyle = new GUIStyle(Card)
                    {
                        border = new RectOffset(3, 3, 3, 3)
                    };

                    // 获取选中状态下的颜色
                    Color selectionColor = EditorGUIUtility.isProSkin
                        ? new Color(0.243f, 0.373f, 0.588f, 1.0f) // 深色主题
                        : new Color(0.239f, 0.501f, 0.874f, 1.0f); // 浅色主题

                    // 创建1x1像素的纹理作为背景
                    Texture2D backgroundTexture = new Texture2D(1, 1);
                    backgroundTexture.SetPixel(0, 0, selectionColor);
                    backgroundTexture.Apply();

                    s_selectedCardStyle.normal.background = backgroundTexture;

                    // 调整选中状态的文本颜色
                    s_selectedCardStyle.normal.textColor = Color.white;
                }
                return s_selectedCardStyle;
            }
        }

        /// <summary>
        /// 分割线样式
        /// </summary>
        public static GUIStyle Separator
        {
            get
            {
                if (s_separatorStyle == null)
                {
                    s_separatorStyle = new GUIStyle()
                    {
                        margin = new RectOffset(0, 0, 10, 10),
                        fixedHeight = 1,
                        stretchWidth = true
                    };

                    // 创建1x1像素的纹理作为分割线
                    Color separatorColor = EditorGUIUtility.isProSkin
                        ? new Color(0.1f, 0.1f, 0.1f, 1.0f)
                        : new Color(0.6f, 0.6f, 0.6f, 1.0f);

                    Texture2D lineTexture = new Texture2D(1, 1);
                    lineTexture.SetPixel(0, 0, separatorColor);
                    lineTexture.Apply();

                    s_separatorStyle.normal.background = lineTexture;
                }
                return s_separatorStyle;
            }
        }

        #endregion

        #region 图标尺寸属性

        /// <summary>
        /// 大图标尺寸 (32x32)
        /// </summary>
        public static Vector2 LargeIconSize => s_largeIconSize;

        /// <summary>
        /// 中等图标尺寸 (24x24)
        /// </summary>
        public static Vector2 MediumIconSize => s_mediumIconSize;

        /// <summary>
        /// 小图标尺寸 (16x16)
        /// </summary>
        public static Vector2 SmallIconSize => s_smallIconSize;

        #endregion

        #region 辅助方法

        /// <summary>
        /// 绘制水平分割线
        /// </summary>
        public static void DrawSeparator()
        {
            GUILayout.Box(GUIContent.none, Separator);
        }

        /// <summary>
        /// 绘制带有可选标题的分组开始
        /// </summary>
        /// <param name="title">分组标题，可为null</param>
        public static void BeginGroup(string title = null)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            if (!string.IsNullOrEmpty(title))
            {
                EditorGUILayout.LabelField(title, TitleLabel);
                DrawSeparator();
            }
        }

        /// <summary>
        /// 结束分组
        /// </summary>
        public static void EndGroup()
        {
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }

        #endregion
    }
}
