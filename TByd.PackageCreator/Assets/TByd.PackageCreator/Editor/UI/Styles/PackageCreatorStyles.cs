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

        private const int HeaderFontSize = 18;
        private const int TitleFontSize = 14;
        private const int StandardFontSize = 12;

        #endregion

        #region 字段

        // 标题样式
        private static GUIStyle _sHeaderLabelStyle;
        private static GUIStyle _sTitleLabelStyle;

        // 描述样式
        private static GUIStyle _sDescriptionStyle;

        // 按钮样式
        private static GUIStyle _sPrimaryButtonStyle;
        private static GUIStyle _sSecondaryButtonStyle;

        // 卡片样式
        private static GUIStyle _sCardStyle;
        private static GUIStyle _sSelectedCardStyle;

        // 分割线样式
        private static GUIStyle _sSeparatorStyle;

        // 图标尺寸
        private static readonly Vector2 SLargeIconSize = new Vector2(32, 32);
        private static readonly Vector2 SMediumIconSize = new Vector2(24, 24);
        private static readonly Vector2 SSmallIconSize = new Vector2(16, 16);

        #endregion

        #region 属性

        /// <summary>
        /// 标题样式 - 用于主标题
        /// </summary>
        public static GUIStyle HeaderLabel
        {
            get
            {
                if (_sHeaderLabelStyle == null)
                {
                    _sHeaderLabelStyle = new GUIStyle(EditorStyles.largeLabel)
                    {
                        fontSize = HeaderFontSize,
                        fontStyle = FontStyle.Bold,
                        alignment = TextAnchor.MiddleLeft,
                        margin = new RectOffset(0, 0, 10, 10)
                    };
                }
                return _sHeaderLabelStyle;
            }
        }

        /// <summary>
        /// 标题样式 - 用于小标题
        /// </summary>
        public static GUIStyle TitleLabel
        {
            get
            {
                if (_sTitleLabelStyle == null)
                {
                    _sTitleLabelStyle = new GUIStyle(EditorStyles.boldLabel)
                    {
                        fontSize = TitleFontSize,
                        alignment = TextAnchor.MiddleLeft,
                        margin = new RectOffset(0, 0, 5, 5)
                    };
                }
                return _sTitleLabelStyle;
            }
        }

        /// <summary>
        /// 描述文本样式
        /// </summary>
        public static GUIStyle Description
        {
            get
            {
                if (_sDescriptionStyle == null)
                {
                    _sDescriptionStyle = new GUIStyle(EditorStyles.label)
                    {
                        wordWrap = true,
                        fontSize = StandardFontSize,
                        margin = new RectOffset(5, 5, 5, 10)
                    };
                }
                return _sDescriptionStyle;
            }
        }

        /// <summary>
        /// 主按钮样式
        /// </summary>
        public static GUIStyle PrimaryButton
        {
            get
            {
                if (_sPrimaryButtonStyle == null)
                {
                    _sPrimaryButtonStyle = new GUIStyle(GUI.skin.button)
                    {
                        fontSize = StandardFontSize,
                        fontStyle = FontStyle.Bold,
                        fixedHeight = 30,
                        margin = new RectOffset(10, 10, 5, 5),
                        padding = new RectOffset(15, 15, 5, 5)
                    };
                }
                return _sPrimaryButtonStyle;
            }
        }

        /// <summary>
        /// 次要按钮样式
        /// </summary>
        public static GUIStyle SecondaryButton
        {
            get
            {
                if (_sSecondaryButtonStyle == null)
                {
                    _sSecondaryButtonStyle = new GUIStyle(GUI.skin.button)
                    {
                        fontSize = StandardFontSize,
                        fixedHeight = 25,
                        margin = new RectOffset(5, 5, 5, 5),
                        padding = new RectOffset(10, 10, 3, 3)
                    };
                }
                return _sSecondaryButtonStyle;
            }
        }

        /// <summary>
        /// 卡片样式 - 用于选项卡或列表项
        /// </summary>
        public static GUIStyle Card
        {
            get
            {
                if (_sCardStyle == null)
                {
                    _sCardStyle = new GUIStyle(EditorStyles.helpBox)
                    {
                        margin = new RectOffset(5, 5, 5, 5),
                        padding = new RectOffset(10, 10, 10, 10)
                    };
                }
                return _sCardStyle;
            }
        }

        /// <summary>
        /// 选中状态的卡片样式
        /// </summary>
        public static GUIStyle SelectedCard
        {
            get
            {
                if (_sSelectedCardStyle == null)
                {
                    _sSelectedCardStyle = new GUIStyle(Card)
                    {
                        border = new RectOffset(3, 3, 3, 3)
                    };

                    // 获取选中状态下的颜色
                    var selectionColor = EditorGUIUtility.isProSkin
                        ? new Color(0.243f, 0.373f, 0.588f, 1.0f) // 深色主题
                        : new Color(0.239f, 0.501f, 0.874f, 1.0f); // 浅色主题

                    // 创建1x1像素的纹理作为背景
                    var backgroundTexture = new Texture2D(1, 1);
                    backgroundTexture.SetPixel(0, 0, selectionColor);
                    backgroundTexture.Apply();

                    _sSelectedCardStyle.normal.background = backgroundTexture;

                    // 调整选中状态的文本颜色
                    _sSelectedCardStyle.normal.textColor = Color.white;
                }
                return _sSelectedCardStyle;
            }
        }

        /// <summary>
        /// 分割线样式
        /// </summary>
        public static GUIStyle Separator
        {
            get
            {
                if (_sSeparatorStyle == null)
                {
                    _sSeparatorStyle = new GUIStyle()
                    {
                        margin = new RectOffset(0, 0, 10, 10),
                        fixedHeight = 1,
                        stretchWidth = true
                    };

                    // 创建1x1像素的纹理作为分割线
                    var separatorColor = EditorGUIUtility.isProSkin
                        ? new Color(0.1f, 0.1f, 0.1f, 1.0f)
                        : new Color(0.6f, 0.6f, 0.6f, 1.0f);

                    var lineTexture = new Texture2D(1, 1);
                    lineTexture.SetPixel(0, 0, separatorColor);
                    lineTexture.Apply();

                    _sSeparatorStyle.normal.background = lineTexture;
                }
                return _sSeparatorStyle;
            }
        }

        #endregion

        #region 图标尺寸属性

        /// <summary>
        /// 大图标尺寸 (32x32)
        /// </summary>
        public static Vector2 LargeIconSize => SLargeIconSize;

        /// <summary>
        /// 中等图标尺寸 (24x24)
        /// </summary>
        public static Vector2 MediumIconSize => SMediumIconSize;

        /// <summary>
        /// 小图标尺寸 (16x16)
        /// </summary>
        public static Vector2 SmallIconSize => SSmallIconSize;

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
