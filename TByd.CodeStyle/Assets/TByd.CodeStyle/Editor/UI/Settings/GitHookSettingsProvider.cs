using System.Collections.Generic;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Editor.Git;
using TByd.CodeStyle.Runtime.Config;
using TByd.CodeStyle.Runtime.Git;
using UnityEditor;
using UnityEngine;

namespace TByd.CodeStyle.Editor.UI.Settings
{
    /// <summary>
    /// Git钩子设置提供者，用于在Project Settings窗口中显示Git钩子设置
    /// </summary>
    public class GitHookSettingsProvider : SettingsProvider
    {
        // 设置路径
        private const string k_CSettingsPath = "Project/TByd/Git钩子";

        // 关键字
        private static readonly string[] s_Keywords = { "TByd", "Git", "Hook", "钩子", "提交", "Commit" };

        // 配置
        private CodeStyleConfig m_Config;

        // 钩子状态
        private Dictionary<GitHookType, bool> m_HookStatus = new();

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
        public GitHookSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
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

            // 获取钩子状态
            m_HookStatus = GitHookMonitor.GetHookStatus();

            m_Initialized = true;
        }

        /// <summary>
        /// 配置变更处理
        /// </summary>
        private void OnConfigChanged()
        {
            m_Config = ConfigProvider.GetConfig();
            m_IsDirty = false;

            // 更新钩子状态
            m_HookStatus = GitHookMonitor.GetHookStatus();
        }

        /// <summary>
        /// 绘制设置UI
        /// </summary>
        /// <param name="searchContext">搜索上下文</param>
        public override void OnGUI(string searchContext)
        {
            if (!m_Initialized)
            {
                Initialize();
            }

            // 绘制标题
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Git钩子设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // 检查是否是Git仓库
            if (!GitRepository.IsProjectGitRepository())
            {
                EditorGUILayout.HelpBox("当前项目不是Git仓库，无法使用Git钩子功能。", MessageType.Warning);

                // 添加Git仓库路径配置
                DrawGitRepositoryPathSettings();

                return;
            }

            // 滚动视图
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);

            // 添加Git仓库路径配置
            DrawGitRepositoryPathSettings();

            EditorGUILayout.Space();

            // 绘制自动安装钩子设置
            var autoInstall = m_Config.GitHookConfig.AutoInstallHooks;
            var newAutoInstall = EditorGUILayout.ToggleLeft("自动安装钩子", autoInstall);
            if (newAutoInstall != autoInstall)
            {
                m_Config.GitHookConfig.AutoInstallHooks = newAutoInstall;
                m_IsDirty = true;
            }

            // 绘制启动时检查钩子状态设置
            var checkOnStartup = m_Config.GitHookConfig.CheckHooksOnStartup;
            var newCheckOnStartup = EditorGUILayout.ToggleLeft("启动时检查钩子状态", checkOnStartup);
            if (newCheckOnStartup != checkOnStartup)
            {
                m_Config.GitHookConfig.CheckHooksOnStartup = newCheckOnStartup;
                m_IsDirty = true;
            }

            EditorGUILayout.Space();

            // 绘制钩子列表
            EditorGUILayout.LabelField("钩子列表", EditorStyles.boldLabel);

            // 刷新钩子状态
            RefreshHookStatus();

            // 绘制钩子配置
            foreach (var hookConfig in m_Config.GitHookConfig.HookConfigs)
            {
                DrawHookConfig(hookConfig);
            }

            EditorGUILayout.Space();

            // 绘制操作按钮
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("安装所有钩子"))
            {
                GitHookMonitor.InstallEnabledHooks();
                RefreshHookStatus();
            }

            if (GUILayout.Button("卸载所有钩子"))
            {
                GitHookMonitor.UninstallAllHooks();
                RefreshHookStatus();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();

            // 保存修改
            if (m_IsDirty)
            {
                ConfigProvider.SaveConfig();
                m_IsDirty = false;
            }
        }

        /// <summary>
        /// 绘制Git仓库路径设置
        /// </summary>
        private void DrawGitRepositoryPathSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Git仓库路径设置", EditorStyles.boldLabel);

            // 显示当前Git仓库路径
            var currentRepoPath = string.IsNullOrEmpty(m_Config.CustomGitRepositoryPath)
                ? "使用Unity项目根目录"
                : m_Config.CustomGitRepositoryPath;

            EditorGUILayout.LabelField("当前Git仓库路径:", currentRepoPath);

            // 检测当前路径是否有效
            var isValidRepo = GitRepository.IsProjectGitRepository();
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

            var newPath = EditorGUILayout.TextField("自定义Git仓库路径:", m_Config.CustomGitRepositoryPath);
            if (newPath != m_Config.CustomGitRepositoryPath)
            {
                m_Config.CustomGitRepositoryPath = newPath;
                m_IsDirty = true;
            }

            if (GUILayout.Button("浏览...", GUILayout.Width(80)))
            {
                var path = EditorUtility.OpenFolderPanel("选择Git仓库根目录", "", "");
                if (!string.IsNullOrEmpty(path))
                {
                    // 检查选择的目录是否是有效的Git仓库
                    if (GitRepository.IsGitRepository(path))
                    {
                        m_Config.CustomGitRepositoryPath = path;
                        m_IsDirty = true;
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
                m_Config.CustomGitRepositoryPath = string.Empty;
                m_IsDirty = true;
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制钩子配置
        /// </summary>
        private void DrawHookConfig(GitHookConfig.HookConfig hookConfig)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUI.BeginChangeCheck();

            // 获取钩子状态
            m_HookStatus.TryGetValue(hookConfig.HookType, out var isInstalled);

            // 显示钩子状态
            var statusStyle = new GUIStyle(EditorStyles.label) { normal = { textColor = isInstalled ? Color.green : Color.red } };

            EditorGUILayout.LabelField(isInstalled ? "已安装" : "未安装", statusStyle, GUILayout.Width(60));

            // 显示钩子类型
            EditorGUILayout.LabelField(hookConfig.HookType.ToString(), GUILayout.Width(120));

            // 显示钩子文件名
            EditorGUILayout.LabelField(hookConfig.HookType.GetFileName(), GUILayout.Width(120));

            // 显示钩子启用状态
            var enabled = EditorGUILayout.Toggle(hookConfig.Enabled, GUILayout.Width(20));

            if (EditorGUI.EndChangeCheck())
            {
                hookConfig.Enabled = enabled;
                m_IsDirty = true;
            }

            // 显示安装/卸载按钮
            if (isInstalled)
            {
                if (GUILayout.Button("卸载", GUILayout.Width(60)))
                {
                    GitHookManager.UninstallHook(hookConfig.HookType);
                    m_HookStatus = GitHookMonitor.GetHookStatus();
                }
            }
            else
            {
                if (GUILayout.Button("安装", GUILayout.Width(60)))
                {
                    GitHookManager.InstallHook(hookConfig.HookType);
                    m_HookStatus = GitHookMonitor.GetHookStatus();
                }
            }

            EditorGUILayout.EndHorizontal();

            // 显示钩子描述
            EditorGUILayout.LabelField(hookConfig.HookType.GetDescription(), EditorStyles.miniLabel);

            EditorGUILayout.Space();
        }

        /// <summary>
        /// 刷新钩子状态
        /// </summary>
        private void RefreshHookStatus()
        {
            GitHookManager.RefreshHookStatusCache();
            m_HookStatus = GitHookMonitor.GetHookStatus();
        }

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        /// <returns>设置提供者实例</returns>
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new GitHookSettingsProvider(k_CSettingsPath, SettingsScope.Project, s_Keywords);
        }
    }
}
