using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.CodeCheck;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Editor.UI.Utils;
using TByd.CodeStyle.Runtime.CodeCheck;
using TByd.CodeStyle.Runtime.Config;

namespace TByd.CodeStyle.Editor.UI.Settings
{
    /// <summary>
    /// 代码检查设置提供者，用于在Project Settings窗口中显示代码检查设置
    /// </summary>
    public class CodeCheckSettingsProvider : SettingsProvider
    {
        // 设置路径
        private const string c_SettingsPath = "Project/TByd/代码检查";

        // 关键字
        private static readonly string[] s_Keywords = new string[]
        {
            "TByd", "Code", "Check", "Style", "代码", "检查", "风格"
        };

        // 配置
        private CodeStyleConfig m_Config;

        // 是否已初始化
        private bool m_Initialized;

        // 是否已修改
        private bool m_IsDirty;

        // 滚动位置
        private Vector2 m_ScrollPosition;

        // 规则分类折叠状态
        private Dictionary<CodeCheckRuleCategory, bool> m_CategoryFoldouts = new Dictionary<CodeCheckRuleCategory, bool>();

        // 新忽略路径
        private string m_NewIgnorePath = string.Empty;

        // 测试文件路径
        private string m_TestFilePath = string.Empty;

        // 测试结果
        private string m_TestResult = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_path">设置路径</param>
        /// <param name="_scopes">设置范围</param>
        /// <param name="_keywords">关键字</param>
        public CodeCheckSettingsProvider(string _path, SettingsScope _scopes, IEnumerable<string> _keywords = null)
            : base(_path, _scopes, _keywords)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            if (m_Initialized)
                return;

            m_Config = ConfigProvider.GetConfig();

            // 订阅配置变更事件
            ConfigProvider.ConfigChanged += OnConfigChanged;

            // 初始化分类折叠状态
            foreach (CodeCheckRuleCategory category in System.Enum.GetValues(typeof(CodeCheckRuleCategory)))
            {
                m_CategoryFoldouts[category] = true;
            }

            m_Initialized = true;
        }

        /// <summary>
        /// 配置变更处理
        /// </summary>
        private void OnConfigChanged()
        {
            m_Config = ConfigProvider.GetConfig();
            m_IsDirty = false;
        }

        /// <summary>
        /// 绘制设置UI
        /// </summary>
        /// <param name="_searchContext">搜索上下文</param>
        public override void OnGUI(string _searchContext)
        {
            Initialize();

            EditorGUILayout.Space();

            // 显示保存和重置按钮
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("保存设置", GUILayout.Width(100)))
            {
                SaveSettings();
            }

            if (GUILayout.Button("重置设置", GUILayout.Width(100)))
            {
                ResetSettings();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("代码检查设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            DrawGeneralSettings();
            DrawIgnoredPathsSettings();
            DrawRuleSettings();
            DrawTestSettings();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndScrollView();

            // 如果有修改，显示保存提示
            if (m_IsDirty)
            {
                EditorGUILayout.Space();
                EditorGUILayout.HelpBox("设置已修改，请点击 保存设置 按钮保存更改。", MessageType.Info);
            }
        }

        /// <summary>
        /// 绘制通用设置
        /// </summary>
        private void DrawGeneralSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("通用设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            bool enableCodeCheck = EditorGUILayout.Toggle("启用代码检查", m_Config.EnableCodeStyleCheck);

            if (EditorGUI.EndChangeCheck())
            {
                m_Config.EnableCodeStyleCheck = enableCodeCheck;
                m_IsDirty = true;
            }

            // 如果未启用代码检查，则显示提示并返回
            if (!m_Config.EnableCodeStyleCheck)
            {
                EditorGUILayout.HelpBox("代码检查已禁用，代码检查设置将不会生效。", MessageType.Info);
                EditorGUILayout.EndVertical();
                return;
            }

            EditorGUI.BeginChangeCheck();

            bool checkOnSave = EditorGUILayout.Toggle("保存时检查", m_Config.CodeCheckConfig.CheckOnSave);
            bool checkOnBuild = EditorGUILayout.Toggle("构建时检查", m_Config.CodeCheckConfig.CheckOnBuild);
            bool checkOnCommit = EditorGUILayout.Toggle("提交时检查", m_Config.CodeCheckConfig.CheckOnCommit);
            bool fixOnSave = EditorGUILayout.Toggle("保存时自动修复", m_Config.CodeCheckConfig.FixOnSave);

            if (EditorGUI.EndChangeCheck())
            {
                m_Config.CodeCheckConfig.CheckOnSave = checkOnSave;
                m_Config.CodeCheckConfig.CheckOnBuild = checkOnBuild;
                m_Config.CodeCheckConfig.CheckOnCommit = checkOnCommit;
                m_Config.CodeCheckConfig.FixOnSave = fixOnSave;

                m_IsDirty = true;
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制忽略路径设置
        /// </summary>
        private void DrawIgnoredPathsSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("忽略路径设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // 绘制忽略路径列表
            for (int i = 0; i < m_Config.CodeCheckConfig.IgnoredPaths.Count; i++)
            {
                string path = m_Config.CodeCheckConfig.IgnoredPaths[i];

                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();

                string newPath = EditorGUILayout.TextField(path);

                if (EditorGUI.EndChangeCheck())
                {
                    m_Config.CodeCheckConfig.IgnoredPaths[i] = newPath;
                    m_IsDirty = true;
                }

                if (GUILayout.Button("删除", GUILayout.Width(60)))
                {
                    m_Config.CodeCheckConfig.IgnoredPaths.RemoveAt(i);
                    m_IsDirty = true;
                    break;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            // 添加新忽略路径
            EditorGUILayout.BeginHorizontal();

            m_NewIgnorePath = EditorGUILayout.TextField("新忽略路径", m_NewIgnorePath);

            if (GUILayout.Button("添加", GUILayout.Width(60)))
            {
                if (!string.IsNullOrEmpty(m_NewIgnorePath))
                {
                    m_Config.CodeCheckConfig.IgnoredPaths.Add(m_NewIgnorePath);
                    m_NewIgnorePath = string.Empty;

                    m_IsDirty = true;
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("忽略路径支持通配符，例如：*/Plugins/*", MessageType.Info);

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制规则设置
        /// </summary>
        private void DrawRuleSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("规则设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // 获取所有规则
            List<ICodeCheckRule> rules = CodeCheckRunner.GetRules();

            // 按分类分组
            var rulesByCategory = rules
                .GroupBy(r => r.Category)
                .OrderBy(g => g.Key);

            foreach (var categoryGroup in rulesByCategory)
            {
                CodeCheckRuleCategory category = categoryGroup.Key;

                // 绘制分类折叠标题
                m_CategoryFoldouts[category] = EditorGUILayout.Foldout(m_CategoryFoldouts[category], GetCategoryName(category), true);

                if (m_CategoryFoldouts[category])
                {
                    EditorGUI.indentLevel++;

                    // 绘制规则列表
                    foreach (var rule in categoryGroup.OrderBy(r => r.Id))
                    {
                        EditorGUILayout.BeginHorizontal();

                        EditorGUI.BeginChangeCheck();

                        bool enabled = EditorGUILayout.Toggle(rule.Enabled, GUILayout.Width(20));
                        EditorGUILayout.LabelField(rule.Name, GUILayout.Width(200));

                        CodeCheckRuleSeverity severity = (CodeCheckRuleSeverity)EditorGUILayout.EnumPopup(rule.Severity, GUILayout.Width(100));

                        if (EditorGUI.EndChangeCheck())
                        {
                            rule.Enabled = enabled;
                            rule.Severity = severity;

                            m_IsDirty = true;
                        }

                        EditorGUILayout.EndHorizontal();

                        // 显示规则描述
                        EditorGUILayout.LabelField(rule.Description, EditorStyles.miniLabel);

                        EditorGUILayout.Space();
                    }

                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.Space();
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制测试设置
        /// </summary>
        private void DrawTestSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("测试设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            m_TestFilePath = EditorGUILayout.TextField("测试文件路径", m_TestFilePath);

            if (GUILayout.Button("浏览", GUILayout.Width(60)))
            {
                string path = EditorUtility.OpenFilePanel("选择C#文件", Application.dataPath, "cs");
                if (!string.IsNullOrEmpty(path))
                {
                    m_TestFilePath = path;
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("检查文件", GUILayout.Width(100)))
            {
                if (!string.IsNullOrEmpty(m_TestFilePath))
                {
                    CodeCheckResult result = CodeCheckRunner.CheckFile(m_TestFilePath);
                    m_TestResult = CodeCheckRunner.GenerateReport(result);
                }
                else
                {
                    m_TestResult = "请选择要检查的文件";
                }
            }

            if (GUILayout.Button("修复文件", GUILayout.Width(100)))
            {
                if (!string.IsNullOrEmpty(m_TestFilePath))
                {
                    bool isFixed = CodeCheckRunner.FixFile(m_TestFilePath);
                    m_TestResult = isFixed ? "文件已修复" : "文件无需修复";
                }
                else
                {
                    m_TestResult = "请选择要修复的文件";
                }
            }

            EditorGUILayout.EndHorizontal();

            if (!string.IsNullOrEmpty(m_TestResult))
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("测试结果：");
                EditorGUILayout.TextArea(m_TestResult, GUILayout.Height(200));
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="_category">分类</param>
        /// <returns>分类名称</returns>
        private string GetCategoryName(CodeCheckRuleCategory _category)
        {
            return _category switch
            {
                CodeCheckRuleCategory.Naming => "命名规则",
                CodeCheckRuleCategory.Formatting => "格式规则",
                CodeCheckRuleCategory.Style => "代码风格规则",
                CodeCheckRuleCategory.Performance => "性能规则",
                CodeCheckRuleCategory.Unity => "Unity特定规则",
                CodeCheckRuleCategory.Security => "安全规则",
                CodeCheckRuleCategory.Other => "其他规则",
                _ => _category.ToString()
            };
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        private void SaveSettings()
        {
            ConfigProvider.SaveConfig();
            m_IsDirty = false;

            NotificationSystem.ShowNotification("代码检查设置已保存", NotificationType.Success);
        }

        /// <summary>
        /// 重置设置
        /// </summary>
        private void ResetSettings()
        {
            if (EditorUtility.DisplayDialog("重置设置", "确定要重置所有代码检查设置吗？这将恢复默认设置。", "确定", "取消"))
            {
                // 创建新的代码检查配置
                m_Config.CodeCheckConfig = new CodeCheckConfig();

                // 保存配置
                ConfigProvider.SaveConfig();

                m_IsDirty = false;

                NotificationSystem.ShowNotification("代码检查设置已重置为默认值", NotificationType.Info);
            }
        }

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        /// <returns>设置提供者实例</returns>
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new CodeCheckSettingsProvider(c_SettingsPath, SettingsScope.Project, s_Keywords);
        }
    }
}
