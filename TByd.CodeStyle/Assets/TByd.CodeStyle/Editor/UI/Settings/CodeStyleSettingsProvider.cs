using System.Collections.Generic;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Editor.UI.Utils;
using TByd.CodeStyle.Runtime.Config;
using UnityEditor;
using UnityEngine;

namespace TByd.CodeStyle.Editor.UI.Settings
{
    /// <summary>
    /// 代码风格设置提供者，用于在Project Settings窗口中显示设置
    /// </summary>
    public class CodeStyleSettingsProvider : SettingsProvider
    {
        // 设置路径
        private const string k_CSettingsPath = "Project/TByd/代码风格";

        // 关键字
        private static readonly string[] s_Keywords =
        {
            "TByd", "代码风格", "Code", "Style", "Git", "Commit", "EditorConfig"
        };

        // 配置
        private CodeStyleConfig m_Config;

        // 是否已初始化
        private bool m_Initialized;

        // 是否已修改
        private bool m_IsDirty;

        // 滚动位置
        private Vector2 m_ScrollPosition;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">设置路径</param>
        /// <param name="scopes">设置范围</param>
        /// <param name="keywords">关键字</param>
        public CodeStyleSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
            : base(path, scopes, keywords)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            if (m_Initialized)
            {
                return;
            }

            m_Config = ConfigProvider.GetConfig();

            // 订阅配置变更事件
            ConfigProvider.OnConfigChanged += OnConfigChanged;

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
        /// <param name="searchContext">搜索上下文</param>
        public override void OnGUI(string searchContext)
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

            if (GUILayout.Button("导出设置", GUILayout.Width(100)))
            {
                ExportSettings();
            }

            if (GUILayout.Button("导入设置", GUILayout.Width(100)))
            {
                ImportSettings();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("TByd 代码风格设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            DrawGeneralSettings();
            DrawGitCommitSettings();
            DrawCodeCheckSettings();

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

            var enableGitCommitCheck = EditorGUILayout.Toggle("启用Git提交规范检查", m_Config.EnableGitCommitCheck);
            var enableCodeStyleCheck = EditorGUILayout.Toggle("启用代码风格检查", m_Config.EnableCodeStyleCheck);
            var checkOnCompile = EditorGUILayout.Toggle("编译时检查代码风格", m_Config.CheckOnCompile);
            var checkBeforeCommit = EditorGUILayout.Toggle("提交前检查代码风格", m_Config.CheckBeforeCommit);

            if (EditorGUI.EndChangeCheck())
            {
                m_Config.EnableGitCommitCheck = enableGitCommitCheck;
                m_Config.EnableCodeStyleCheck = enableCodeStyleCheck;
                m_Config.CheckOnCompile = checkOnCompile;
                m_Config.CheckBeforeCommit = checkBeforeCommit;

                m_IsDirty = true;
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制Git提交设置
        /// </summary>
        private void DrawGitCommitSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Git提交设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (!m_Config.EnableGitCommitCheck)
            {
                EditorGUILayout.HelpBox("Git提交规范检查已禁用。", MessageType.Info);
                EditorGUILayout.EndVertical();
                return;
            }

            EditorGUI.BeginChangeCheck();

            var gitConfig = m_Config.GitCommitConfig;

            var forceUseTemplate = EditorGUILayout.Toggle("强制使用提交模板", gitConfig.ForceUseTemplate);
            var requireType = EditorGUILayout.Toggle("要求提交类型", gitConfig.RequireType);
            var requireScope = EditorGUILayout.Toggle("要求作用域", gitConfig.RequireScope);
            var requireSubject = EditorGUILayout.Toggle("要求简短描述", gitConfig.RequireSubject);
            var requireBody = EditorGUILayout.Toggle("要求详细描述", gitConfig.RequireBody);
            var requireFooter = EditorGUILayout.Toggle("要求关闭的问题", gitConfig.RequireFooter);

            var subjectMaxLength = EditorGUILayout.IntSlider("简短描述最大长度", gitConfig.SubjectMaxLength, 10, 200);

            if (EditorGUI.EndChangeCheck())
            {
                gitConfig.ForceUseTemplate = forceUseTemplate;
                gitConfig.RequireType = requireType;
                gitConfig.RequireScope = requireScope;
                gitConfig.RequireSubject = requireSubject;
                gitConfig.RequireBody = requireBody;
                gitConfig.RequireFooter = requireFooter;
                gitConfig.SubjectMaxLength = subjectMaxLength;

                m_IsDirty = true;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("提交类型", EditorStyles.boldLabel);

            // 绘制提交类型列表
            for (var i = 0; i < gitConfig.CommitTypes.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();

                var commitType = gitConfig.CommitTypes[i];

                var enabled = EditorGUILayout.Toggle(commitType.Enabled, GUILayout.Width(20));
                var type = EditorGUILayout.TextField(commitType.Type, GUILayout.Width(100));
                var description = EditorGUILayout.TextField(commitType.Description);

                if (EditorGUI.EndChangeCheck())
                {
                    commitType.Enabled = enabled;
                    commitType.Type = type;
                    commitType.Description = description;

                    m_IsDirty = true;
                }

                if (GUILayout.Button("删除", GUILayout.Width(60)))
                {
                    gitConfig.CommitTypes.RemoveAt(i);
                    m_IsDirty = true;
                    i--;
                }

                EditorGUILayout.EndHorizontal();
            }

            // 添加新的提交类型
            if (GUILayout.Button("添加提交类型", GUILayout.Width(120)))
            {
                gitConfig.CommitTypes.Add(new GitCommitConfig.CommitType("new", "新类型"));
                m_IsDirty = true;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("作用域", EditorStyles.boldLabel);

            // 绘制作用域列表
            for (var i = 0; i < gitConfig.Scopes.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();

                var scopeTmp = EditorGUILayout.TextField(gitConfig.Scopes[i]);

                if (EditorGUI.EndChangeCheck())
                {
                    gitConfig.Scopes[i] = scopeTmp;
                    m_IsDirty = true;
                }

                if (GUILayout.Button("删除", GUILayout.Width(60)))
                {
                    gitConfig.Scopes.RemoveAt(i);
                    m_IsDirty = true;
                    i--;
                }

                EditorGUILayout.EndHorizontal();
            }

            // 添加新的作用域
            if (GUILayout.Button("添加作用域", GUILayout.Width(120)))
            {
                gitConfig.Scopes.Add("new-scope");
                m_IsDirty = true;
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制代码检查设置
        /// </summary>
        private void DrawCodeCheckSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("代码检查设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (!m_Config.EnableCodeStyleCheck)
            {
                EditorGUILayout.HelpBox("代码风格检查已禁用。", MessageType.Info);
                EditorGUILayout.EndVertical();
                return;
            }

            EditorGUI.BeginChangeCheck();

            var codeConfig = m_Config.CodeCheckConfig;

            var ignoreGeneratedCode = EditorGUILayout.Toggle("忽略生成的代码", codeConfig.IgnoreGeneratedCode);
            var ignoreThirdPartyCode = EditorGUILayout.Toggle("忽略第三方代码", codeConfig.IgnoreThirdPartyCode);
            var ignoreTestCode = EditorGUILayout.Toggle("忽略测试代码", codeConfig.IgnoreTestCode);

            if (EditorGUI.EndChangeCheck())
            {
                codeConfig.IgnoreGeneratedCode = ignoreGeneratedCode;
                codeConfig.IgnoreThirdPartyCode = ignoreThirdPartyCode;
                codeConfig.IgnoreTestCode = ignoreTestCode;

                m_IsDirty = true;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("忽略的路径", EditorStyles.boldLabel);

            // 绘制忽略的路径列表
            for (var i = 0; i < codeConfig.IgnoredPaths.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();

                var path = EditorGUILayout.TextField(codeConfig.IgnoredPaths[i]);

                if (EditorGUI.EndChangeCheck())
                {
                    codeConfig.IgnoredPaths[i] = path;
                    m_IsDirty = true;
                }

                if (GUILayout.Button("删除", GUILayout.Width(60)))
                {
                    codeConfig.IgnoredPaths.RemoveAt(i);
                    m_IsDirty = true;
                    i--;
                }

                EditorGUILayout.EndHorizontal();
            }

            // 添加新的忽略路径
            if (GUILayout.Button("添加忽略路径", GUILayout.Width(120)))
            {
                codeConfig.IgnoredPaths.Add("Assets/NewPath/");
                m_IsDirty = true;
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("代码规则", EditorStyles.boldLabel);

            // 绘制代码规则列表
            for (var i = 0; i < codeConfig.Rules.Count; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                EditorGUI.BeginChangeCheck();

                var rule = codeConfig.Rules[i];

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(rule.Id, GUILayout.Width(80));
                var name = EditorGUILayout.TextField(rule.Name);
                EditorGUILayout.EndHorizontal();

                var description = EditorGUILayout.TextField("描述", rule.Description);

                var severity = (CodeCheckConfig.RuleSeverity)EditorGUILayout.EnumPopup("严重程度", rule.Severity);

                if (EditorGUI.EndChangeCheck())
                {
                    rule.Name = name;
                    rule.Description = description;
                    rule.Severity = severity;

                    m_IsDirty = true;
                }

                if (GUILayout.Button("删除规则", GUILayout.Width(120)))
                {
                    codeConfig.Rules.RemoveAt(i);
                    m_IsDirty = true;
                    i--;
                }

                EditorGUILayout.EndVertical();
            }

            // 添加新的代码规则
            if (GUILayout.Button("添加代码规则", GUILayout.Width(120)))
            {
                var newId = "CS" + (1000 + codeConfig.Rules.Count);
                codeConfig.Rules.Add(new CodeCheckConfig.CodeRule(newId, "新规则", "新规则描述"));
                m_IsDirty = true;
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        private void SaveSettings()
        {
            ConfigProvider.SaveConfig();
            m_IsDirty = false;

            NotificationSystem.ShowNotification("设置已保存", NotificationType.k_Success);
        }

        /// <summary>
        /// 重置设置
        /// </summary>
        private void ResetSettings()
        {
            if (EditorUtility.DisplayDialog("重置设置", "确定要重置所有设置吗？这将恢复默认设置。", "确定", "取消"))
            {
                ConfigProvider.ResetConfig();
                m_IsDirty = false;

                NotificationSystem.ShowNotification("设置已重置为默认值");
            }
        }

        /// <summary>
        /// 导出设置
        /// </summary>
        private void ExportSettings()
        {
            var path = EditorUtility.SaveFilePanel("导出设置", "", "TBydCodeStyleConfig.json", "json");
            if (!string.IsNullOrEmpty(path))
            {
                ConfigProvider.ExportConfig(path);
                NotificationSystem.ShowNotification("设置已导出", NotificationType.k_Success);
            }
        }

        /// <summary>
        /// 导入设置
        /// </summary>
        private void ImportSettings()
        {
            var path = EditorUtility.OpenFilePanel("导入设置", "", "json");
            if (!string.IsNullOrEmpty(path))
            {
                ConfigProvider.ImportConfig(path);
                m_IsDirty = false;

                NotificationSystem.ShowNotification("设置已导入", NotificationType.k_Success);
            }
        }

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        /// <returns>设置提供者实例</returns>
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new CodeStyleSettingsProvider(k_CSettingsPath, SettingsScope.Project, s_Keywords);
        }
    }
}
