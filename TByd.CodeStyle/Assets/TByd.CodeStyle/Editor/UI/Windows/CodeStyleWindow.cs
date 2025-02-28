using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Editor.Git;
using TByd.CodeStyle.Editor.Git.Commit;
using TByd.CodeStyle.Editor.UI.Utils;
using TByd.CodeStyle.Runtime.Config;
using TByd.CodeStyle.Runtime.Git;
using TByd.CodeStyle.Runtime.Git.Commit;

namespace TByd.CodeStyle.Editor.UI.Windows
{
    /// <summary>
    /// 代码风格工具的主窗口
    /// </summary>
    public class CodeStyleWindow : EditorWindow
    {
        // 窗口标题
        private const string c_WindowTitle = "TByd 代码风格";

        // 窗口实例
        private static CodeStyleWindow s_Instance;

        // 滚动位置
        private Vector2 m_ScrollPosition;
        private Vector2 m_CommitScrollPosition;

        // 当前选中的标签页索引
        private int m_SelectedTabIndex;

        // 标签页名称
        private readonly string[] m_TabNames = { "概览", "Git提交规范", "代码检查", "设置" };

        // Git提交相关
        private string m_CommitMessage = string.Empty;
        private string m_CommitType = string.Empty;
        private string m_CommitScope = string.Empty;
        private string m_CommitSubject = string.Empty;
        private string m_CommitBody = string.Empty;
        private string m_CommitFooter = string.Empty;
        private bool m_CommitIsBreakingChange = false;
        private bool m_ShowCommitPreview = false;
        private CommitMessageValidationResult m_ValidationResult;

        // 提交类型选项
        private string[] m_CommitTypeOptions;
        private int m_SelectedCommitTypeIndex = 0;

        // 作用域选项
        private string[] m_ScopeOptions;
        private int m_SelectedScopeIndex = 0;

        // Git仓库状态
        private bool m_IsGitRepository = false;
        private bool m_AreHooksInstalled = false;

        /// <summary>
        /// 打开窗口的菜单项
        /// </summary>
        [MenuItem("TByd/CodeStyle/代码风格工具", false, 100)]
        public static void ShowWindow()
        {
            s_Instance = GetWindow<CodeStyleWindow>(false, c_WindowTitle, true);
            s_Instance.minSize = new Vector2(600, 400);
            s_Instance.Show();

            // 显示欢迎通知
            NotificationSystem.ShowNotification("欢迎使用TByd代码风格工具！", NotificationType.Info);
        }

        /// <summary>
        /// 窗口初始化
        /// </summary>
        private void OnEnable()
        {
            // 加载窗口图标
            titleContent.image = EditorGUIUtility.FindTexture("d_UnityEditor.ConsoleWindow");

            // 初始化Git提交相关数据
            InitializeGitCommitData();

            // 检查Git仓库状态
            CheckGitRepositoryStatus();
        }

        /// <summary>
        /// 初始化Git提交相关数据
        /// </summary>
        private void InitializeGitCommitData()
        {
            // 获取配置
            CodeStyleConfig config = ConfigManager.GetConfig();

            // 初始化提交类型选项
            List<string> typeOptions = new List<string>();
            typeOptions.Add("-- 选择提交类型 --");

            foreach (var type in config.GitCommitConfig.CommitTypes)
            {
                typeOptions.Add($"{type.Type} - {type.Description}");
            }

            m_CommitTypeOptions = typeOptions.ToArray();

            // 初始化作用域选项
            List<string> scopeOptions = new List<string>();
            scopeOptions.Add("-- 选择作用域 --");
            scopeOptions.Add("(无作用域)");

            foreach (var scope in config.GitCommitConfig.Scopes)
            {
                scopeOptions.Add(scope);
            }

            m_ScopeOptions = scopeOptions.ToArray();
        }

        /// <summary>
        /// 检查Git仓库状态
        /// </summary>
        private void CheckGitRepositoryStatus()
        {
            // 检查是否是Git仓库
            m_IsGitRepository = GitRepository.IsProjectGitRepository();

            // 如果是Git仓库，检查钩子是否已安装
            if (m_IsGitRepository)
            {
                // 获取所有钩子状态
                Dictionary<GitHookType, bool> hookStatus = GitHookMonitor.GetHookStatus();

                // 检查是否所有必要的钩子都已安装
                m_AreHooksInstalled = hookStatus.Count > 0 &&
                                    hookStatus.ContainsKey(GitHookType.PreCommit) &&
                                    hookStatus[GitHookType.PreCommit] &&
                                    hookStatus.ContainsKey(GitHookType.CommitMsg) &&
                                    hookStatus[GitHookType.CommitMsg];
            }
            else
            {
                m_AreHooksInstalled = false;
            }
        }

        /// <summary>
        /// 绘制窗口内容
        /// </summary>
        private void OnGUI()
        {
            // 检查Git仓库状态（每次绘制时检查，以便及时响应路径变更）
            CheckGitRepositoryStatus();

            // 绘制标题
            EditorGUILayout.Space();
            GUILayout.Label(c_WindowTitle, EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // 绘制通知
            NotificationSystem.DrawNotification();

            DrawToolbar();

            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);

            switch (m_SelectedTabIndex)
            {
                case 0:
                    DrawOverviewTab();
                    break;
                case 1:
                    DrawGitCommitTab();
                    break;
                case 2:
                    DrawCodeCheckTab();
                    break;
                case 3:
                    DrawSettingsTab();
                    break;
            }

            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// 绘制工具栏
        /// </summary>
        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

            m_SelectedTabIndex = GUILayout.Toolbar(m_SelectedTabIndex, m_TabNames, EditorStyles.toolbarButton);

            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// 绘制概览标签页
        /// </summary>
        private void DrawOverviewTab()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.Label("TByd 代码风格工具", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("版本:", "0.1.0");
            EditorGUILayout.LabelField("状态:", "技术预览版");

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("这个工具帮助您维护代码质量和提交规范。");

            EditorGUILayout.Space();

            // 显示Git仓库状态
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("Git仓库状态", EditorStyles.boldLabel);

            if (m_IsGitRepository)
            {
                EditorGUILayout.LabelField("Git仓库:", "已检测到");
                EditorGUILayout.LabelField("提交钩子:", m_AreHooksInstalled ? "已安装" : "未安装");

                if (!m_AreHooksInstalled)
                {
                    EditorGUILayout.Space();
                    if (GUILayout.Button("安装Git钩子"))
                    {
                        GitHookMonitor.InstallEnabledHooks();
                        CheckGitRepositoryStatus();
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("Git仓库:", "未检测到");
                EditorGUILayout.HelpBox("当前项目不是Git仓库，无法使用Git提交规范功能。", MessageType.Warning);
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            // 显示EditorConfig状态
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("EditorConfig状态", EditorStyles.boldLabel);

            bool hasEditorConfig = EditorConfigManager.HasProjectEditorConfig();

            if (hasEditorConfig)
            {
                EditorGUILayout.LabelField("EditorConfig:", "已检测到");
                EditorGUILayout.LabelField("规则数量:", EditorConfigManager.GetRules().Count.ToString());

                EditorGUILayout.Space();
                if (GUILayout.Button("查看EditorConfig设置"))
                {
                    SettingsService.OpenProjectSettings("Project/TByd/EditorConfig");
                }
            }
            else
            {
                EditorGUILayout.LabelField("EditorConfig:", "未检测到");
                EditorGUILayout.HelpBox("当前项目未配置EditorConfig，建议添加以保持代码风格一致性。", MessageType.Info);

                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("创建默认配置"))
                {
                    EditorConfigManager.CreateDefaultEditorConfig();
                }

                if (GUILayout.Button("创建Unity项目配置"))
                {
                    EditorConfigManager.CreateUnityProjectEditorConfig();
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            // 功能状态
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("功能状态", EditorStyles.boldLabel);

            CodeStyleConfig config = ConfigManager.GetConfig();

            bool enableGitCommit = config.EnableGitCommitCheck;
            bool enableCodeCheck = config.EnableCodeStyleCheck;
            bool enableEditorConfig = config.EnableEditorConfig;

            EditorGUI.BeginChangeCheck();

            enableGitCommit = EditorGUILayout.ToggleLeft("启用Git提交规范检查", enableGitCommit);
            enableCodeCheck = EditorGUILayout.ToggleLeft("启用代码风格检查", enableCodeCheck);
            enableEditorConfig = EditorGUILayout.ToggleLeft("启用EditorConfig支持", enableEditorConfig);

            if (EditorGUI.EndChangeCheck())
            {
                config.EnableGitCommitCheck = enableGitCommit;
                config.EnableCodeStyleCheck = enableCodeCheck;
                config.EnableEditorConfig = enableEditorConfig;
                ConfigManager.SaveConfig();
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制Git提交规范标签页
        /// </summary>
        private void DrawGitCommitTab()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.Label("Git提交规范", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            if (!m_IsGitRepository)
            {
                EditorGUILayout.HelpBox("当前项目不是Git仓库，无法使用Git提交规范功能。", MessageType.Warning);
                EditorGUILayout.EndVertical();
                return;
            }

            if (!ConfigManager.GetConfig().EnableGitCommitCheck)
            {
                EditorGUILayout.HelpBox("Git提交规范检查已禁用，请在设置中启用。", MessageType.Warning);

                if (GUILayout.Button("启用Git提交规范检查"))
                {
                    CodeStyleConfig config = ConfigManager.GetConfig();
                    config.EnableGitCommitCheck = true;
                    ConfigManager.SaveConfig();
                }

                EditorGUILayout.EndVertical();
                return;
            }

            // 提交消息编辑器
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("提交消息编辑器", EditorStyles.boldLabel);

            // 提交类型选择
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("提交类型:", GUILayout.Width(80));
            int newTypeIndex = EditorGUILayout.Popup(m_SelectedCommitTypeIndex, m_CommitTypeOptions);
            if (newTypeIndex != m_SelectedCommitTypeIndex)
            {
                m_SelectedCommitTypeIndex = newTypeIndex;
                if (m_SelectedCommitTypeIndex > 0)
                {
                    m_CommitType = ConfigManager.GetConfig().GitCommitConfig.CommitTypes[m_SelectedCommitTypeIndex - 1].Type;
                }
                else
                {
                    m_CommitType = string.Empty;
                }
                UpdateCommitMessage();
            }
            EditorGUILayout.EndHorizontal();

            // 作用域选择
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("作用域:", GUILayout.Width(80));
            int newScopeIndex = EditorGUILayout.Popup(m_SelectedScopeIndex, m_ScopeOptions);
            if (newScopeIndex != m_SelectedScopeIndex)
            {
                m_SelectedScopeIndex = newScopeIndex;
                if (m_SelectedScopeIndex == 1) // 无作用域
                {
                    m_CommitScope = string.Empty;
                }
                else if (m_SelectedScopeIndex > 1)
                {
                    m_CommitScope = m_ScopeOptions[m_SelectedScopeIndex];
                }
                else
                {
                    m_CommitScope = string.Empty;
                }
                UpdateCommitMessage();
            }
            EditorGUILayout.EndHorizontal();

            // 是否是破坏性变更
            bool newIsBreakingChange = EditorGUILayout.ToggleLeft("破坏性变更", m_CommitIsBreakingChange);
            if (newIsBreakingChange != m_CommitIsBreakingChange)
            {
                m_CommitIsBreakingChange = newIsBreakingChange;
                UpdateCommitMessage();
            }

            // 主题
            EditorGUILayout.LabelField("主题:");
            string newSubject = EditorGUILayout.TextField(m_CommitSubject);
            if (newSubject != m_CommitSubject)
            {
                m_CommitSubject = newSubject;
                UpdateCommitMessage();
            }

            // 正文
            EditorGUILayout.LabelField("正文:");
            string newBody = EditorGUILayout.TextArea(m_CommitBody, GUILayout.Height(60));
            if (newBody != m_CommitBody)
            {
                m_CommitBody = newBody;
                UpdateCommitMessage();
            }

            // 页脚
            EditorGUILayout.LabelField("页脚:");
            string newFooter = EditorGUILayout.TextArea(m_CommitFooter, GUILayout.Height(40));
            if (newFooter != m_CommitFooter)
            {
                m_CommitFooter = newFooter;
                UpdateCommitMessage();
            }

            EditorGUILayout.EndVertical();

            // 提交消息预览
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("提交消息预览", EditorStyles.boldLabel);

            m_ShowCommitPreview = EditorGUILayout.Foldout(m_ShowCommitPreview, GUIContent.none);

            EditorGUILayout.EndHorizontal();

            if (m_ShowCommitPreview)
            {
                EditorGUILayout.Space();

                GUI.enabled = false;
                m_CommitScrollPosition = EditorGUILayout.BeginScrollView(m_CommitScrollPosition, GUILayout.Height(100));
                EditorGUILayout.TextArea(m_CommitMessage);
                EditorGUILayout.EndScrollView();
                GUI.enabled = true;

                EditorGUILayout.Space();

                // 验证提交消息
                if (!string.IsNullOrEmpty(m_CommitMessage))
                {
                    m_ValidationResult = CommitMessageChecker.ValidateMessage(m_CommitMessage);

                    if (m_ValidationResult.IsValid)
                    {
                        EditorGUILayout.HelpBox("提交消息格式有效", MessageType.Info);
                    }
                    else
                    {
                        EditorGUILayout.HelpBox(m_ValidationResult.GetFormattedErrorMessage(), MessageType.Error);
                    }
                }
            }

            EditorGUILayout.EndVertical();

            // 操作按钮
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("复制到剪贴板"))
            {
                EditorGUIUtility.systemCopyBuffer = m_CommitMessage;
                NotificationSystem.ShowNotification("提交消息已复制到剪贴板", NotificationType.Success);
            }

            if (GUILayout.Button("重置"))
            {
                ResetCommitMessage();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // Git钩子状态
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("Git钩子状态", EditorStyles.boldLabel);

            EditorGUILayout.LabelField("提交钩子:", m_AreHooksInstalled ? "已安装" : "未安装");

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("安装钩子"))
            {
                GitHookMonitor.InstallEnabledHooks();
                CheckGitRepositoryStatus();
                NotificationSystem.ShowNotification("Git钩子安装成功", NotificationType.Success);
            }

            if (GUILayout.Button("卸载钩子"))
            {
                GitHookMonitor.UninstallAllHooks();
                CheckGitRepositoryStatus();
                NotificationSystem.ShowNotification("Git钩子卸载成功", NotificationType.Info);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 更新提交消息
        /// </summary>
        private void UpdateCommitMessage()
        {
            // 构建提交消息
            string message = string.Empty;

            // 类型
            if (!string.IsNullOrEmpty(m_CommitType))
            {
                message += m_CommitType;

                // 作用域
                if (!string.IsNullOrEmpty(m_CommitScope))
                {
                    message += $"({m_CommitScope})";
                }

                // 破坏性变更
                if (m_CommitIsBreakingChange)
                {
                    message += "!";
                }

                // 主题
                if (!string.IsNullOrEmpty(m_CommitSubject))
                {
                    message += $": {m_CommitSubject}";
                }
            }

            // 正文
            if (!string.IsNullOrEmpty(m_CommitBody))
            {
                message += $"\n\n{m_CommitBody}";
            }

            // 页脚
            if (!string.IsNullOrEmpty(m_CommitFooter))
            {
                message += $"\n\n{m_CommitFooter}";

                // 如果是破坏性变更但页脚中没有BREAKING CHANGE，则添加
                if (m_CommitIsBreakingChange && !m_CommitFooter.Contains("BREAKING CHANGE:"))
                {
                    message += "\n\nBREAKING CHANGE: 此提交包含破坏性变更";
                }
            }
            else if (m_CommitIsBreakingChange)
            {
                // 如果是破坏性变更但没有页脚，则添加BREAKING CHANGE
                message += "\n\nBREAKING CHANGE: 此提交包含破坏性变更";
            }

            m_CommitMessage = message;
        }

        /// <summary>
        /// 重置提交消息
        /// </summary>
        private void ResetCommitMessage()
        {
            m_CommitType = string.Empty;
            m_CommitScope = string.Empty;
            m_CommitSubject = string.Empty;
            m_CommitBody = string.Empty;
            m_CommitFooter = string.Empty;
            m_CommitIsBreakingChange = false;
            m_CommitMessage = string.Empty;
            m_SelectedCommitTypeIndex = 0;
            m_SelectedScopeIndex = 0;

            NotificationSystem.ShowNotification("提交消息已重置", NotificationType.Info);
        }

        /// <summary>
        /// 绘制代码检查标签页
        /// </summary>
        private void DrawCodeCheckTab()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.LabelField("代码检查设置", EditorStyles.boldLabel);
                EditorGUILayout.Space();

                // 获取配置
                CodeStyleConfig config = ConfigManager.GetConfig();

                // 启用代码风格检查
                bool enableCodeStyleCheck = EditorGUILayout.Toggle("启用代码风格检查", config.EnableCodeStyleCheck);
                if (enableCodeStyleCheck != config.EnableCodeStyleCheck)
                {
                    config.EnableCodeStyleCheck = enableCodeStyleCheck;
                    ConfigManager.SaveConfig();
                }

                // 启用EditorConfig支持
                bool enableEditorConfig = EditorGUILayout.Toggle("启用EditorConfig支持", config.EnableEditorConfig);
                if (enableEditorConfig != config.EnableEditorConfig)
                {
                    config.EnableEditorConfig = enableEditorConfig;
                    ConfigManager.SaveConfig();

                    // 如果启用了EditorConfig，确保加载配置
                    if (enableEditorConfig)
                    {
                        EditorConfigManager.Initialize();
                    }
                }

                // 如果启用了EditorConfig，显示EditorConfig相关选项
                if (enableEditorConfig)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("EditorConfig设置", EditorStyles.boldLabel);

                    // 检查项目是否存在EditorConfig文件
                    bool hasEditorConfig = EditorConfigManager.HasProjectEditorConfig();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("EditorConfig状态:", GUILayout.Width(150));

                        if (hasEditorConfig)
                        {
                            EditorGUILayout.LabelField("已启用", EditorStyles.boldLabel);
                        }
                        else
                        {
                            EditorGUILayout.LabelField("未启用", EditorStyles.boldLabel);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField("配置文件路径:", GUILayout.Width(150));
                        EditorGUILayout.TextField(EditorConfigManager.GetProjectEditorConfigPath());
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    // 绘制操作按钮
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (hasEditorConfig)
                        {
                            // 重新加载按钮
                            if (GUILayout.Button("重新加载", GUILayout.Width(100)))
                            {
                                EditorConfigManager.LoadProjectEditorConfig();
                            }

                            // 编辑按钮
                            if (GUILayout.Button("编辑文件", GUILayout.Width(100)))
                            {
                                string editorConfigPath = EditorConfigManager.GetProjectEditorConfigPath();
                                if (File.Exists(editorConfigPath))
                                {
                                    // 使用系统默认编辑器打开文件
                                    EditorUtility.OpenWithDefaultApp(editorConfigPath);
                                }
                            }

                            // 验证按钮
                            if (GUILayout.Button("验证项目文件", GUILayout.Width(120)))
                            {
                                ValidateProjectFiles();
                            }

                            // 格式化按钮
                            if (GUILayout.Button("格式化项目文件", GUILayout.Width(120)))
                            {
                                FormatProjectFiles();
                            }
                        }
                        else
                        {
                            // 创建默认配置按钮
                            if (GUILayout.Button("创建默认配置", GUILayout.Width(120)))
                            {
                                EditorConfigManager.CreateDefaultEditorConfig();
                            }

                            // 创建Unity项目配置按钮
                            if (GUILayout.Button("创建Unity项目配置", GUILayout.Width(150)))
                            {
                                EditorConfigManager.CreateUnityProjectEditorConfig();
                            }
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    // 显示更多设置的链接
                    EditorGUILayout.Space();
                    if (GUILayout.Button("更多EditorConfig设置...", GUILayout.Width(200)))
                    {
                        SettingsService.OpenProjectSettings("Project/TByd/EditorConfig");
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 验证项目文件是否符合EditorConfig规则
        /// </summary>
        private void ValidateProjectFiles()
        {
            // 显示进度条
            EditorUtility.DisplayProgressBar("验证项目文件", "正在准备验证...", 0f);

            try
            {
                // 获取项目中的所有C#文件
                string projectPath = Application.dataPath;
                string[] csharpFiles = Directory.GetFiles(projectPath, "*.cs", SearchOption.AllDirectories);

                int totalFiles = csharpFiles.Length;
                int validFiles = 0;
                int invalidFiles = 0;
                List<string> invalidFilesList = new List<string>();

                // 验证每个文件
                for (int i = 0; i < totalFiles; i++)
                {
                    string file = csharpFiles[i];

                    // 更新进度条
                    EditorUtility.DisplayProgressBar(
                        "验证项目文件",
                        $"正在验证 {Path.GetFileName(file)}...",
                        (float)i / totalFiles);

                    // 验证文件
                    bool isValid = EditorConfigManager.ValidateFile(file);

                    if (isValid)
                    {
                        validFiles++;
                    }
                    else
                    {
                        invalidFiles++;
                        invalidFilesList.Add(file);
                    }
                }

                // 显示结果
                EditorUtility.ClearProgressBar();

                string message = $"验证完成！\n\n" +
                                $"总文件数: {totalFiles}\n" +
                                $"符合规则的文件: {validFiles}\n" +
                                $"不符合规则的文件: {invalidFiles}";

                if (invalidFiles > 0)
                {
                    message += "\n\n不符合规则的文件:";

                    // 最多显示10个文件
                    int displayCount = Mathf.Min(invalidFilesList.Count, 10);
                    for (int i = 0; i < displayCount; i++)
                    {
                        string relativePath = invalidFilesList[i].Replace(Application.dataPath, "Assets");
                        message += $"\n- {relativePath}";
                    }

                    if (invalidFilesList.Count > 10)
                    {
                        message += $"\n... 以及其他 {invalidFilesList.Count - 10} 个文件";
                    }

                    EditorUtility.DisplayDialog("验证结果", message, "确定");
                }
                else
                {
                    EditorUtility.DisplayDialog("验证结果", message, "确定");
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        /// <summary>
        /// 格式化项目文件使其符合EditorConfig规则
        /// </summary>
        private void FormatProjectFiles()
        {
            // 显示确认对话框
            bool confirm = EditorUtility.DisplayDialog(
                "格式化项目文件",
                "此操作将根据EditorConfig规则格式化项目中的所有C#文件。\n\n" +
                "格式化可能会修改文件的缩进、行尾、空白字符等。\n\n" +
                "建议在执行此操作前备份或提交您的更改。\n\n" +
                "是否继续？",
                "继续",
                "取消");

            if (!confirm)
            {
                return;
            }

            // 显示进度条
            EditorUtility.DisplayProgressBar("格式化项目文件", "正在准备格式化...", 0f);

            try
            {
                // 获取项目中的所有C#文件
                string projectPath = Application.dataPath;
                string[] csharpFiles = Directory.GetFiles(projectPath, "*.cs", SearchOption.AllDirectories);

                int totalFiles = csharpFiles.Length;
                int formattedFiles = 0;
                int failedFiles = 0;
                List<string> failedFilesList = new List<string>();

                // 格式化每个文件
                for (int i = 0; i < totalFiles; i++)
                {
                    string file = csharpFiles[i];

                    // 更新进度条
                    EditorUtility.DisplayProgressBar(
                        "格式化项目文件",
                        $"正在格式化 {Path.GetFileName(file)}...",
                        (float)i / totalFiles);

                    // 格式化文件
                    bool success = EditorConfigManager.FormatFile(file);

                    if (success)
                    {
                        formattedFiles++;
                    }
                    else
                    {
                        failedFiles++;
                        failedFilesList.Add(file);
                    }
                }

                // 刷新资源数据库
                AssetDatabase.Refresh();

                // 显示结果
                EditorUtility.ClearProgressBar();

                string message = $"格式化完成！\n\n" +
                                $"总文件数: {totalFiles}\n" +
                                $"成功格式化的文件: {formattedFiles}\n" +
                                $"格式化失败的文件: {failedFiles}";

                if (failedFiles > 0)
                {
                    message += "\n\n格式化失败的文件:";

                    // 最多显示10个文件
                    int displayCount = Mathf.Min(failedFilesList.Count, 10);
                    for (int i = 0; i < displayCount; i++)
                    {
                        string relativePath = failedFilesList[i].Replace(Application.dataPath, "Assets");
                        message += $"\n- {relativePath}";
                    }

                    if (failedFilesList.Count > 10)
                    {
                        message += $"\n... 以及其他 {failedFilesList.Count - 10} 个文件";
                    }

                    EditorUtility.DisplayDialog("格式化结果", message, "确定");
                }
                else
                {
                    EditorUtility.DisplayDialog("格式化结果", message, "确定");
                }
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        /// <summary>
        /// 绘制设置标签页
        /// </summary>
        private void DrawSettingsTab()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.Label("设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            CodeStyleConfig config = ConfigManager.GetConfig();

            EditorGUI.BeginChangeCheck();

            // 基本设置
            EditorGUILayout.LabelField("基本设置", EditorStyles.boldLabel);

            config.EnableGitCommitCheck = EditorGUILayout.ToggleLeft("启用Git提交规范检查", config.EnableGitCommitCheck);
            config.EnableCodeStyleCheck = EditorGUILayout.ToggleLeft("启用代码风格检查", config.EnableCodeStyleCheck);
            config.EnableEditorConfig = EditorGUILayout.ToggleLeft("启用EditorConfig支持", config.EnableEditorConfig);

            EditorGUILayout.Space();

            // 检查设置
            EditorGUILayout.LabelField("检查设置", EditorStyles.boldLabel);

            config.CheckOnCompile = EditorGUILayout.ToggleLeft("在编译时检查代码风格", config.CheckOnCompile);
            config.CheckBeforeCommit = EditorGUILayout.ToggleLeft("在提交前检查代码风格", config.CheckBeforeCommit);

            EditorGUILayout.Space();

            // Git仓库设置
            EditorGUILayout.LabelField("Git仓库设置", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("自定义Git仓库路径:", GUILayout.Width(150));

            string repositoryPath = config.CustomGitRepositoryPath;
            repositoryPath = EditorGUILayout.TextField(repositoryPath);

            if (GUILayout.Button("浏览...", GUILayout.Width(80)))
            {
                string path = EditorUtility.OpenFolderPanel("选择Git仓库路径", repositoryPath, "");
                if (!string.IsNullOrEmpty(path))
                {
                    repositoryPath = path;
                }
            }

            config.CustomGitRepositoryPath = repositoryPath;

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // EditorConfig设置
            EditorGUILayout.LabelField("EditorConfig设置", EditorStyles.boldLabel);

            bool hasEditorConfig = EditorConfigManager.HasProjectEditorConfig();

            if (hasEditorConfig)
            {
                EditorGUILayout.LabelField("EditorConfig状态:", "已启用");
                EditorGUILayout.LabelField("规则数量:", EditorConfigManager.GetRules().Count.ToString());

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("查看EditorConfig设置"))
                {
                    SettingsService.OpenProjectSettings("Project/TByd/EditorConfig");
                }

                if (GUILayout.Button("编辑EditorConfig文件"))
                {
                    string editorConfigPath = EditorConfigManager.GetProjectEditorConfigPath();
                    if (System.IO.File.Exists(editorConfigPath))
                    {
                        EditorUtility.OpenWithDefaultApp(editorConfigPath);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.LabelField("EditorConfig状态:", "未启用");
                EditorGUILayout.HelpBox("当前项目未配置EditorConfig，建议添加以保持代码风格一致性。", MessageType.Info);

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("创建默认配置"))
                {
                    EditorConfigManager.CreateDefaultEditorConfig();
                }

                if (GUILayout.Button("创建Unity项目配置"))
                {
                    EditorConfigManager.CreateUnityProjectEditorConfig();
                }

                if (GUILayout.Button("导入配置"))
                {
                    string path = EditorUtility.OpenFilePanel("导入EditorConfig", "", "");
                    if (!string.IsNullOrEmpty(path))
                    {
                        EditorConfigManager.ImportEditorConfig(path);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            // 保存按钮
            if (EditorGUI.EndChangeCheck())
            {
                ConfigManager.SaveConfig();
                NotificationSystem.ShowNotification("设置已保存", NotificationType.Success);
            }

            EditorGUILayout.Space();

            // 重置按钮
            if (GUILayout.Button("重置所有设置"))
            {
                if (EditorUtility.DisplayDialog("重置设置", "确定要重置所有设置吗？这将恢复默认配置。", "确定", "取消"))
                {
                    ConfigManager.ResetConfig();
                    NotificationSystem.ShowNotification("设置已重置为默认值", NotificationType.Info);
                }
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 显示测试进度
        /// </summary>
        private void ShowTestProgress()
        {
            EditorApplication.delayCall += () =>
            {
                for (int i = 0; i <= 10; i++)
                {
                    float progress = i / 10f;
                    NotificationSystem.ShowProgress("测试进度", $"正在处理... {i * 10}%", progress);

                    // 模拟处理时间
                    System.Threading.Thread.Sleep(200);
                }

                NotificationSystem.HideProgress();
                NotificationSystem.ShowNotification("进度测试完成", NotificationType.Success);
            };
        }
    }
}
