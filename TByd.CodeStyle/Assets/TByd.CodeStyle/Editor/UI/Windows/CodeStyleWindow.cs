using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
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
        [MenuItem("Tools/TByd/代码风格工具", false, 100)]
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
            CodeStyleConfig config = ConfigProvider.GetConfig();
            
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
            
            // 功能状态
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("功能状态", EditorStyles.boldLabel);
            
            CodeStyleConfig config = ConfigProvider.GetConfig();
            
            bool enableGitCommit = config.EnableGitCommitCheck;
            bool newEnableGitCommit = EditorGUILayout.ToggleLeft("启用Git提交规范检查", enableGitCommit);
            
            if (newEnableGitCommit != enableGitCommit)
            {
                config.EnableGitCommitCheck = newEnableGitCommit;
                ConfigProvider.SaveConfig();
            }
            
            bool enableCodeCheck = config.EnableCodeStyleCheck;
            bool newEnableCodeCheck = EditorGUILayout.ToggleLeft("启用代码风格检查", enableCodeCheck);
            
            if (newEnableCodeCheck != enableCodeCheck)
            {
                config.EnableCodeStyleCheck = newEnableCodeCheck;
                ConfigProvider.SaveConfig();
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
            
            if (!ConfigProvider.GetConfig().EnableGitCommitCheck)
            {
                EditorGUILayout.HelpBox("Git提交规范检查已禁用，请在设置中启用。", MessageType.Warning);
                
                if (GUILayout.Button("启用Git提交规范检查"))
                {
                    CodeStyleConfig config = ConfigProvider.GetConfig();
                    config.EnableGitCommitCheck = true;
                    ConfigProvider.SaveConfig();
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
                    m_CommitType = ConfigProvider.GetConfig().GitCommitConfig.CommitTypes[m_SelectedCommitTypeIndex - 1].Type;
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
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            GUILayout.Label("代码检查", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.HelpBox("代码检查功能将在第三阶段实现，敬请期待！", MessageType.Info);
            
            // 显示开发路线图
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("开发路线图:");
            EditorGUILayout.LabelField("第一阶段: Git提交规范（已完成）");
            EditorGUILayout.LabelField("第二阶段: EditorConfig支持与代码风格规范");
            EditorGUILayout.LabelField("第三阶段: 代码检查功能");
            EditorGUILayout.LabelField("第四阶段: 高级功能与优化");
            
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// 绘制设置标签页
        /// </summary>
        private void DrawSettingsTab()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            GUILayout.Label("设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            if (GUILayout.Button("打开项目设置"))
            {
                SettingsService.OpenProjectSettings("Project/TByd/代码风格");
            }
            
            EditorGUILayout.Space();
            
            // Git仓库设置
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("Git仓库设置", EditorStyles.boldLabel);
            
            // 获取当前配置
            CodeStyleConfig config = ConfigProvider.GetConfig();
            
            // 显示当前Git仓库路径
            string currentRepoPath = string.IsNullOrEmpty(config.CustomGitRepositoryPath) ? 
                "使用Unity项目根目录" : config.CustomGitRepositoryPath;
            
            EditorGUILayout.LabelField("当前Git仓库路径:", currentRepoPath);
            
            // 检测当前路径是否有效
            bool isValidRepo = GitRepository.IsProjectGitRepository();
            if (isValidRepo)
            {
                EditorGUILayout.HelpBox("已检测到有效的Git仓库", MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox("未检测到有效的Git仓库，请检查路径是否正确", MessageType.Warning);
            }
            
            EditorGUILayout.Space();
            
            // 自定义Git仓库路径
            EditorGUILayout.BeginHorizontal();
            
            string newPath = EditorGUILayout.TextField("自定义Git仓库路径:", config.CustomGitRepositoryPath);
            if (newPath != config.CustomGitRepositoryPath)
            {
                config.CustomGitRepositoryPath = newPath;
                ConfigProvider.SaveConfig();
                
                // 重新检查Git仓库状态
                CheckGitRepositoryStatus();
            }
            
            if (GUILayout.Button("浏览...", GUILayout.Width(80)))
            {
                string path = EditorUtility.OpenFolderPanel("选择Git仓库根目录", "", "");
                if (!string.IsNullOrEmpty(path))
                {
                    // 检查选择的目录是否是有效的Git仓库
                    if (GitRepository.IsGitRepository(path))
                    {
                        config.CustomGitRepositoryPath = path;
                        ConfigProvider.SaveConfig();
                        
                        // 重新检查Git仓库状态
                        CheckGitRepositoryStatus();
                        
                        NotificationSystem.ShowNotification("Git仓库路径已更新", NotificationType.Success);
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("无效的Git仓库", 
                            "所选目录不是有效的Git仓库，请确保目录中包含.git文件夹。", "确定");
                    }
                }
            }
            
            EditorGUILayout.EndHorizontal();
            
            if (GUILayout.Button("重置为默认路径"))
            {
                config.CustomGitRepositoryPath = string.Empty;
                ConfigProvider.SaveConfig();
                
                // 重新检查Git仓库状态
                CheckGitRepositoryStatus();
                
                NotificationSystem.ShowNotification("已重置为默认路径", NotificationType.Info);
            }
            
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space();
            
            // 配置导入导出
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("配置管理", EditorStyles.boldLabel);
            
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("导出配置"))
            {
                string path = EditorUtility.SaveFilePanel("导出配置", "", "TBydCodeStyleConfig.json", "json");
                if (!string.IsNullOrEmpty(path))
                {
                    ConfigProvider.ExportConfig(path);
                    NotificationSystem.ShowNotification($"配置已导出到: {path}", NotificationType.Success);
                }
            }
            
            if (GUILayout.Button("导入配置"))
            {
                string path = EditorUtility.OpenFilePanel("导入配置", "", "json");
                if (!string.IsNullOrEmpty(path))
                {
                    ConfigProvider.ImportConfig(path);
                    NotificationSystem.ShowNotification("配置导入成功", NotificationType.Success);
                    
                    // 刷新Git提交相关数据
                    InitializeGitCommitData();
                }
            }
            
            EditorGUILayout.EndHorizontal();
            
            if (GUILayout.Button("重置配置"))
            {
                if (EditorUtility.DisplayDialog("重置配置", "确定要重置所有配置吗？此操作不可撤销。", "重置", "取消"))
                {
                    ConfigProvider.ResetConfig();
                    NotificationSystem.ShowNotification("配置已重置为默认值", NotificationType.Info);
                    
                    // 刷新Git提交相关数据
                    InitializeGitCommitData();
                }
            }
            
            EditorGUILayout.EndVertical();
            
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