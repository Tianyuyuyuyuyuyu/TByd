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

        private const int k_HeaderFontSize = 18;
        private const int k_TitleFontSize = 14;
        private const int k_StandardFontSize = 12;

        #endregion

        #region 字段

        // 标题样式
        private static GUIStyle s_HeaderLabelStyle;
        private static GUIStyle s_TitleLabelStyle;

        // 描述样式
        private static GUIStyle s_DescriptionStyle;

        // 按钮样式
        private static GUIStyle s_PrimaryButtonStyle;
        private static GUIStyle s_SecondaryButtonStyle;

        // 卡片样式
        private static GUIStyle s_CardStyle;
        private static GUIStyle s_SelectedCardStyle;

        // 分割线样式
        private static GUIStyle s_SeparatorStyle;

        // 图标尺寸
        private static readonly Vector2 s_LargeIconSize = new Vector2(32, 32);
        private static readonly Vector2 s_MediumIconSize = new Vector2(24, 24);
        private static readonly Vector2 s_SmallIconSize = new Vector2(16, 16);

        #endregion

        #region 属性

        /// <summary>
        /// 标题样式 - 用于主标题
        /// </summary>
        public static GUIStyle HeaderLabel
        {
            get
            {
                if (s_HeaderLabelStyle == null)
                {
                    s_HeaderLabelStyle = new GUIStyle(EditorStyles.largeLabel)
                    {
                        fontSize = k_HeaderFontSize,
                        fontStyle = FontStyle.Bold,
                        alignment = TextAnchor.MiddleLeft,
                        margin = new RectOffset(0, 0, 10, 10)
                    };
                }
                return s_HeaderLabelStyle;
            }
        }

        /// <summary>
        /// 标题样式 - 用于小标题
        /// </summary>
        public static GUIStyle TitleLabel
        {
            get
            {
                if (s_TitleLabelStyle == null)
                {
                    s_TitleLabelStyle = new GUIStyle(EditorStyles.boldLabel)
                    {
                        fontSize = k_TitleFontSize,
                        alignment = TextAnchor.MiddleLeft,
                        margin = new RectOffset(0, 0, 5, 5)
                    };
                }
                return s_TitleLabelStyle;
            }
        }

        /// <summary>
        /// 描述文本样式
        /// </summary>
        public static GUIStyle Description
        {
            get
            {
                if (s_DescriptionStyle == null)
                {
                    s_DescriptionStyle = new GUIStyle(EditorStyles.label)
                    {
                        wordWrap = true,
                        fontSize = k_StandardFontSize,
                        margin = new RectOffset(5, 5, 5, 10)
                    };
                }
                return s_DescriptionStyle;
            }
        }

        /// <summary>
        /// 主按钮样式
        /// </summary>
        public static GUIStyle PrimaryButton
        {
            get
            {
                if (s_PrimaryButtonStyle == null)
                {
                    s_PrimaryButtonStyle = new GUIStyle(GUI.skin.button)
                    {
                        fontSize = k_StandardFontSize,
                        fontStyle = FontStyle.Bold,
                        fixedHeight = 30,
                        margin = new RectOffset(10, 10, 5, 5),
                        padding = new RectOffset(15, 15, 5, 5)
                    };
                }
                return s_PrimaryButtonStyle;
            }
        }

        /// <summary>
        /// 次要按钮样式
        /// </summary>
        public static GUIStyle SecondaryButton
        {
            get
            {
                if (s_SecondaryButtonStyle == null)
                {
                    s_SecondaryButtonStyle = new GUIStyle(GUI.skin.button)
                    {
                        fontSize = k_StandardFontSize,
                        fixedHeight = 25,
                        margin = new RectOffset(5, 5, 5, 5),
                        padding = new RectOffset(10, 10, 3, 3)
                    };
                }
                return s_SecondaryButtonStyle;
            }
        }

        /// <summary>
        /// 卡片样式 - 用于选项卡或列表项
        /// </summary>
        public static GUIStyle Card
        {
            get
            {
                if (s_CardStyle == null)
                {
                    s_CardStyle = new GUIStyle(EditorStyles.helpBox)
                    {
                        margin = new RectOffset(5, 5, 5, 5),
                        padding = new RectOffset(10, 10, 10, 10)
                    };
                }
                return s_CardStyle;
            }
        }

        /// <summary>
        /// 选中状态的卡片样式
        /// </summary>
        public static GUIStyle SelectedCard
        {
            get
            {
                if (s_SelectedCardStyle == null)
                {
                    s_SelectedCardStyle = new GUIStyle(Card)
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

                    s_SelectedCardStyle.normal.background = backgroundTexture;

                    // 调整选中状态的文本颜色
                    s_SelectedCardStyle.normal.textColor = Color.white;
                }
                return s_SelectedCardStyle;
            }
        }

        /// <summary>
        /// 分割线样式
        /// </summary>
        public static GUIStyle Separator
        {
            get
            {
                if (s_SeparatorStyle == null)
                {
                    s_SeparatorStyle = new GUIStyle()
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

                    s_SeparatorStyle.normal.background = lineTexture;
                }
                return s_SeparatorStyle;
            }
        }

        #endregion

        #region 图标尺寸属性

        /// <summary>
        /// 大图标尺寸 (32x32)
        /// </summary>
        public static Vector2 LargeIconSize => s_LargeIconSize;

        /// <summary>
        /// 中等图标尺寸 (24x24)
        /// </summary>
        public static Vector2 MediumIconSize => s_MediumIconSize;

        /// <summary>
        /// 小图标尺寸 (16x16)
        /// </summary>
        public static Vector2 SmallIconSize => s_SmallIconSize;

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
