using System.Collections.Generic;
using TByd.PackageCreator.Editor.Core.Models;

namespace TByd.PackageCreator.Editor.Templates.Implementations
{
    /// <summary>
    /// 编辑器工具模板，专为Unity编辑器扩展工具设计
    /// </summary>
    public class EditorToolTemplate : BasicPackageTemplate
    {
        /// <summary>
        /// 模板唯一标识符
        /// </summary>
        public override string Id => "com.tbyd.packagecreator.template.editortool";

        /// <summary>
        /// 模板名称
        /// </summary>
        public override string Name => "编辑器工具包";

        /// <summary>
        /// 模板描述
        /// </summary>
        public override string Description => "专为Unity编辑器扩展工具设计的包模板，提供编辑器UI和工具开发的基础结构";

        /// <summary>
        /// 初始化模板
        /// </summary>
        protected override void InitializeTemplate()
        {
            base.InitializeTemplate();

            // 添加编辑器工具特有的目录
            AddDirectory("Editor/UI", "编辑器UI代码目录", false);
            AddDirectory("Editor/Utils", "编辑器工具类目录", false);
            AddDirectory("Editor/Resources", "编辑器资源目录", false);
            AddDirectory("Editor/Styles", "编辑器样式目录", false);

            // 添加编辑器工具特有的文件
            AddFile("Editor/UI/MainEditorWindow.cs", GetMainEditorWindowTemplate(), "主编辑器窗口", false);
            AddFile("Editor/Utils/EditorUtils.cs", GetEditorUtilsTemplate(), "编辑器工具类", false);
            AddFile("Editor/Styles/EditorStyles.cs", GetEditorStylesTemplate(), "编辑器样式类", false);

            // 添加编辑器工具特有的选项
            AddOption("includeCustomEditor", "包含自定义Inspector", "是否包含自定义Inspector编辑器", TemplateOptionType.Boolean, "false");
            AddOption("includeEditorWindow", "包含编辑器窗口", "是否包含自定义编辑器窗口", TemplateOptionType.Boolean, "true");
            AddOption("includePropertyDrawer", "包含属性绘制器", "是否包含自定义属性绘制器", TemplateOptionType.Boolean, "false");
            AddOption("uiFramework", "UI框架", "使用的UI框架", TemplateOptionType.Enum, "IMGUI").PossibleValues =
                new List<string> { "IMGUI", "UIElements", "Hybrid" };
        }

        /// <summary>
        /// 获取主编辑器窗口模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetMainEditorWindowTemplate()
        {
            return @"using UnityEditor;
using UnityEngine;

namespace #ROOT_NAMESPACE#.Editor.UI
{
    /// <summary>
    /// 主编辑器窗口
    /// </summary>
    public class MainEditorWindow : EditorWindow
    {
        [MenuItem(""Window/#DISPLAY_NAME#"")]
        public static void ShowWindow()
        {
            var window = GetWindow<MainEditorWindow>();
            window.titleContent = new GUIContent(""#DISPLAY_NAME#"");
            window.Show();
        }

        private void OnEnable()
        {
            // 初始化窗口
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField(""#DISPLAY_NAME#"", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // 在这里添加您的UI代码
        }
    }
}";
        }

        /// <summary>
        /// 获取编辑器工具类模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetEditorUtilsTemplate()
        {
            return @"using UnityEditor;
using UnityEngine;

namespace #ROOT_NAMESPACE#.Editor.Utils
{
    /// <summary>
    /// 编辑器工具类，提供常用的编辑器辅助方法
    /// </summary>
    public static class EditorUtils
    {
        /// <summary>
        /// 绘制分隔线
        /// </summary>
        /// <param name=""thickness"">线条粗细</param>
        /// <param name=""padding"">上下间距</param>
        public static void DrawUILine(float thickness = 1, float padding = 10)
        {
            Rect rect = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            rect.height = thickness;
            rect.y += padding / 2;
            rect.x -= 2;
            rect.width += 4;

            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        }

        /// <summary>
        /// 绘制标题
        /// </summary>
        /// <param name=""title"">标题文本</param>
        public static void DrawTitle(string title)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
            DrawUILine();
        }

        /// <summary>
        /// 绘制只读文本字段
        /// </summary>
        /// <param name=""label"">标签</param>
        /// <param name=""text"">文本内容</param>
        public static void DrawReadOnlyTextField(string label, string text)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField(label, text);
            EditorGUI.EndDisabledGroup();
        }
    }
}";
        }

        /// <summary>
        /// 获取编辑器样式类模板
        /// </summary>
        /// <returns>模板内容</returns>
        protected virtual string GetEditorStylesTemplate()
        {
            return @"using UnityEditor;
using UnityEngine;

namespace #ROOT_NAMESPACE#.Editor.Styles
{
    /// <summary>
    /// 编辑器样式类，提供自定义的编辑器样式
    /// </summary>
    public static class EditorStyles
    {
        private static GUIStyle _headerStyle;
        private static GUIStyle _subHeaderStyle;
        private static GUIStyle _buttonStyle;

        /// <summary>
        /// 标题样式
        /// </summary>
        public static GUIStyle HeaderStyle
        {
            get
            {
                if (_headerStyle == null)
                {
                    _headerStyle = new GUIStyle(UnityEditor.EditorStyles.boldLabel);
                    _headerStyle.fontSize = 16;
                    _headerStyle.alignment = TextAnchor.MiddleLeft;
                    _headerStyle.margin = new RectOffset(4, 4, 4, 4);
                }
                return _headerStyle;
            }
        }

        /// <summary>
        /// 副标题样式
        /// </summary>
        public static GUIStyle SubHeaderStyle
        {
            get
            {
                if (_subHeaderStyle == null)
                {
                    _subHeaderStyle = new GUIStyle(UnityEditor.EditorStyles.boldLabel);
                    _subHeaderStyle.fontSize = 14;
                    _subHeaderStyle.alignment = TextAnchor.MiddleLeft;
                    _subHeaderStyle.margin = new RectOffset(4, 4, 4, 4);
                }
                return _subHeaderStyle;
            }
        }

        /// <summary>
        /// 按钮样式
        /// </summary>
        public static GUIStyle ButtonStyle
        {
            get
            {
                if (_buttonStyle == null)
                {
                    _buttonStyle = new GUIStyle(GUI.skin.button);
                    _buttonStyle.fontSize = 12;
                    _buttonStyle.alignment = TextAnchor.MiddleCenter;
                    _buttonStyle.margin = new RectOffset(4, 4, 4, 4);
                    _buttonStyle.padding = new RectOffset(8, 8, 4, 4);
                }
                return _buttonStyle;
            }
        }
    }
}";
        }

        /// <summary>
        /// 获取模板预览信息
        /// </summary>
        /// <returns>预览信息</returns>
        public override TemplatePreviewInfo GetPreviewInfo()
        {
            var previewInfo = base.GetPreviewInfo();

            // 添加编辑器工具特有的特点
            previewInfo.AddFeature("专为编辑器扩展设计的目录结构");
            previewInfo.AddFeature("包含编辑器窗口和工具类模板");
            previewInfo.AddFeature("支持IMGUI和UIElements UI框架");

            return previewInfo;
        }

        /// <summary>
        /// 验证包配置
        /// </summary>
        /// <param name="config">包配置</param>
        /// <returns>验证结果</returns>
        public override ValidationResult ValidateConfig(PackageConfig config)
        {
            var result = base.ValidateConfig(config);

            // 编辑器工具特有的验证逻辑
            if (!config.Name.Contains("editor") && !config.Name.Contains("tool"))
            {
                result.AddWarning("编辑器工具包名称建议包含'editor'或'tool'关键字");
            }

            return result;
        }
    }
}
