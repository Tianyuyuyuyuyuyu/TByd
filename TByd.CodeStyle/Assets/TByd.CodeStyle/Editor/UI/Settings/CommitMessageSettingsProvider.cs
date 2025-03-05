using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Editor.Git.Commit;
using TByd.CodeStyle.Editor.UI.Utils;
using TByd.CodeStyle.Runtime.Config;
using TByd.CodeStyle.Runtime.Git;

namespace TByd.CodeStyle.Editor.UI.Settings
{
    /// <summary>
    /// 提交消息设置提供者，用于在Project Settings窗口中显示提交消息设置
    /// </summary>
    public class CommitMessageSettingsProvider : SettingsProvider
    {
        // 设置路径
        private const string k_CSettingsPath = "Project/TByd/提交消息";

        // 关键字
        private static readonly string[] s_Keywords = new string[]
        {
            "TByd", "Git", "Commit", "提交", "消息", "规范"
        };

        // 配置
        private CodeStyleConfig m_Config;

        // 是否已初始化
        private bool m_Initialized;

        // 是否已修改
        private bool m_IsDirty;

        // 滚动位置
        private Vector2 m_ScrollPosition;

        // 新提交类型
        private string m_NewCommitType = string.Empty;

        // 新提交类型描述
        private string m_NewCommitTypeDescription = string.Empty;

        // 新作用域
        private string m_NewScope = string.Empty;

        // 测试提交消息
        private string m_TestCommitMessage = string.Empty;

        // 测试提交消息验证结果
        private string m_TestValidationResult = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">设置路径</param>
        /// <param name="scopes">设置范围</param>
        /// <param name="keywords">关键字</param>
        public CommitMessageSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
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

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // 检查是否是Git仓库
            if (!GitRepository.IsProjectGitRepository())
            {
                EditorGUILayout.HelpBox("当前项目不是Git仓库，无法使用提交消息检查功能。", MessageType.Warning);
                return;
            }

            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("提交消息设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            DrawGeneralSettings();
            DrawCommitTypeSettings();
            DrawScopeSettings();
            DrawTestCommitMessage();

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

            if (EditorGUI.EndChangeCheck())
            {
                m_Config.EnableGitCommitCheck = enableGitCommitCheck;
                m_IsDirty = true;
            }

            // 如果未启用Git提交规范检查，则显示提示并返回
            if (!m_Config.EnableGitCommitCheck)
            {
                EditorGUILayout.HelpBox("Git提交规范检查已禁用，提交消息设置将不会生效。", MessageType.Info);
                EditorGUILayout.EndVertical();
                return;
            }

            EditorGUI.BeginChangeCheck();

            var forceUseTemplate = EditorGUILayout.Toggle("强制使用提交模板", m_Config.GitCommitConfig.ForceUseTemplate);
            var requireType = EditorGUILayout.Toggle("要求提交类型", m_Config.GitCommitConfig.RequireType);
            var requireScope = EditorGUILayout.Toggle("要求作用域", m_Config.GitCommitConfig.RequireScope);
            var requireSubject = EditorGUILayout.Toggle("要求简短描述", m_Config.GitCommitConfig.RequireSubject);
            var requireBody = EditorGUILayout.Toggle("要求详细描述", m_Config.GitCommitConfig.RequireBody);
            var requireFooter = EditorGUILayout.Toggle("要求页脚", m_Config.GitCommitConfig.RequireFooter);

            var subjectMaxLength = EditorGUILayout.IntSlider("简短描述最大长度", m_Config.GitCommitConfig.SubjectMaxLength, 50, 200);

            if (EditorGUI.EndChangeCheck())
            {
                m_Config.GitCommitConfig.ForceUseTemplate = forceUseTemplate;
                m_Config.GitCommitConfig.RequireType = requireType;
                m_Config.GitCommitConfig.RequireScope = requireScope;
                m_Config.GitCommitConfig.RequireSubject = requireSubject;
                m_Config.GitCommitConfig.RequireBody = requireBody;
                m_Config.GitCommitConfig.RequireFooter = requireFooter;
                m_Config.GitCommitConfig.SubjectMaxLength = subjectMaxLength;

                m_IsDirty = true;
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制提交类型设置
        /// </summary>
        private void DrawCommitTypeSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("提交类型设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // 绘制提交类型列表
            for (var i = 0; i < m_Config.GitCommitConfig.CommitTypes.Count; i++)
            {
                var commitType = m_Config.GitCommitConfig.CommitTypes[i];

                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();

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
                    m_Config.GitCommitConfig.CommitTypes.RemoveAt(i);
                    m_IsDirty = true;
                    break;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            // 添加新提交类型
            EditorGUILayout.BeginHorizontal();

            m_NewCommitType = EditorGUILayout.TextField("新提交类型", m_NewCommitType, GUILayout.Width(200));
            m_NewCommitTypeDescription = EditorGUILayout.TextField(m_NewCommitTypeDescription);

            if (GUILayout.Button("添加", GUILayout.Width(60)))
            {
                if (!string.IsNullOrEmpty(m_NewCommitType))
                {
                    m_Config.GitCommitConfig.CommitTypes.Add(new GitCommitConfig.CommitType(
                        m_NewCommitType,
                        m_NewCommitTypeDescription));

                    m_NewCommitType = string.Empty;
                    m_NewCommitTypeDescription = string.Empty;

                    m_IsDirty = true;
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制作用域设置
        /// </summary>
        private void DrawScopeSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("作用域设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // 绘制作用域列表
            for (var i = 0; i < m_Config.GitCommitConfig.Scopes.Count; i++)
            {
                var scope = m_Config.GitCommitConfig.Scopes[i];

                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();

                var newScope = EditorGUILayout.TextField(scope);

                if (EditorGUI.EndChangeCheck())
                {
                    m_Config.GitCommitConfig.Scopes[i] = newScope;
                    m_IsDirty = true;
                }

                if (GUILayout.Button("删除", GUILayout.Width(60)))
                {
                    m_Config.GitCommitConfig.Scopes.RemoveAt(i);
                    m_IsDirty = true;
                    break;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            // 添加新作用域
            EditorGUILayout.BeginHorizontal();

            m_NewScope = EditorGUILayout.TextField("新作用域", m_NewScope);

            if (GUILayout.Button("添加", GUILayout.Width(60)))
            {
                if (!string.IsNullOrEmpty(m_NewScope))
                {
                    m_Config.GitCommitConfig.Scopes.Add(m_NewScope);
                    m_NewScope = string.Empty;

                    m_IsDirty = true;
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制测试提交消息
        /// </summary>
        private void DrawTestCommitMessage()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("测试提交消息", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("输入提交消息进行测试：");

            m_TestCommitMessage = EditorGUILayout.TextArea(m_TestCommitMessage, GUILayout.Height(100));

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("验证", GUILayout.Width(100)))
            {
                var result = CommitMessageChecker.ValidateMessage(m_TestCommitMessage);
                m_TestValidationResult = result.GetFormattedErrorMessage();
            }

            if (GUILayout.Button("修复", GUILayout.Width(100)))
            {
                m_TestCommitMessage = CommitMessageChecker.FixMessage(m_TestCommitMessage);
            }

            if (GUILayout.Button("生成模板", GUILayout.Width(100)))
            {
                m_TestCommitMessage = CommitMessageChecker.GenerateTemplate();
            }

            EditorGUILayout.EndHorizontal();

            if (!string.IsNullOrEmpty(m_TestValidationResult))
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("验证结果：");
                EditorGUILayout.HelpBox(m_TestValidationResult, m_TestValidationResult.Contains("验证通过") ? MessageType.Info : MessageType.Error);
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

            NotificationSystem.ShowNotification("提交消息设置已保存", NotificationType.k_Success);
        }

        /// <summary>
        /// 重置设置
        /// </summary>
        private void ResetSettings()
        {
            if (EditorUtility.DisplayDialog("重置设置", "确定要重置所有提交消息设置吗？这将恢复默认设置。", "确定", "取消"))
            {
                // 创建新的Git提交配置
                m_Config.GitCommitConfig = new GitCommitConfig();

                // 保存配置
                ConfigProvider.SaveConfig();

                m_IsDirty = false;

                NotificationSystem.ShowNotification("提交消息设置已重置为默认值", NotificationType.k_Info);
            }
        }

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        /// <returns>设置提供者实例</returns>
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new CommitMessageSettingsProvider(k_CSettingsPath, SettingsScope.Project, s_Keywords);
        }
    }
}
