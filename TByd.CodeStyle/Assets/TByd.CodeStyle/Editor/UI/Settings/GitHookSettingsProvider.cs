using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Editor.Git;
using TByd.CodeStyle.Editor.UI.Utils;
using TByd.CodeStyle.Runtime.Config;
using TByd.CodeStyle.Runtime.Git;

namespace TByd.CodeStyle.Editor.UI.Settings
{
    /// <summary>
    /// Git钩子设置提供者，用于在Project Settings窗口中显示Git钩子设置
    /// </summary>
    public class GitHookSettingsProvider : SettingsProvider
    {
        // 设置路径
        private const string c_SettingsPath = "Project/TByd/Git钩子";
        
        // 关键字
        private static readonly string[] s_Keywords = new string[] 
        { 
            "TByd", "Git", "Hook", "钩子", "提交", "Commit" 
        };
        
        // 配置
        private CodeStyleConfig m_Config;
        
        // 是否已初始化
        private bool m_Initialized;
        
        // 是否已修改
        private bool m_IsDirty;
        
        // 滚动位置
        private Vector2 m_ScrollPosition;
        
        // 钩子状态
        private Dictionary<GitHookType, bool> m_HookStatus = new Dictionary<GitHookType, bool>();
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_path">设置路径</param>
        /// <param name="_scopes">设置范围</param>
        /// <param name="_keywords">关键字</param>
        public GitHookSettingsProvider(string _path, SettingsScope _scopes, IEnumerable<string> _keywords = null) 
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
            
            // 检查是否是Git仓库
            if (!GitRepository.IsProjectGitRepository())
            {
                EditorGUILayout.HelpBox("当前项目不是Git仓库，无法管理Git钩子。", MessageType.Warning);
                return;
            }
            
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Git钩子设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            DrawGeneralSettings();
            DrawHookSettings();
            DrawHookActions();
            
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
            
            bool autoInstallHooks = EditorGUILayout.Toggle("自动安装钩子", m_Config.GitHookConfig.AutoInstallHooks);
            bool checkHooksOnStartup = EditorGUILayout.Toggle("启动时检查钩子状态", m_Config.GitHookConfig.CheckHooksOnStartup);
            
            if (EditorGUI.EndChangeCheck())
            {
                m_Config.GitHookConfig.AutoInstallHooks = autoInstallHooks;
                m_Config.GitHookConfig.CheckHooksOnStartup = checkHooksOnStartup;
                
                m_IsDirty = true;
            }
            
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// 绘制钩子设置
        /// </summary>
        private void DrawHookSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("钩子设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            // 如果未启用Git提交规范检查，则显示提示
            if (!m_Config.EnableGitCommitCheck)
            {
                EditorGUILayout.HelpBox("Git提交规范检查已禁用，钩子将不会生效。", MessageType.Info);
                EditorGUILayout.EndVertical();
                return;
            }
            
            // 绘制钩子列表
            foreach (var hookConfig in m_Config.GitHookConfig.HookConfigs)
            {
                EditorGUILayout.BeginHorizontal();
                
                EditorGUI.BeginChangeCheck();
                
                // 获取钩子状态
                bool isInstalled = false;
                m_HookStatus.TryGetValue(hookConfig.HookType, out isInstalled);
                
                // 显示钩子状态
                GUIStyle statusStyle = new GUIStyle(EditorStyles.label);
                statusStyle.normal.textColor = isInstalled ? Color.green : Color.red;
                
                EditorGUILayout.LabelField(isInstalled ? "已安装" : "未安装", statusStyle, GUILayout.Width(60));
                
                // 显示钩子类型
                EditorGUILayout.LabelField(hookConfig.HookType.ToString(), GUILayout.Width(120));
                
                // 显示钩子文件名
                EditorGUILayout.LabelField(hookConfig.HookType.GetFileName(), GUILayout.Width(120));
                
                // 显示钩子启用状态
                bool enabled = EditorGUILayout.Toggle(hookConfig.Enabled, GUILayout.Width(20));
                
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
            
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// 绘制钩子操作
        /// </summary>
        private void DrawHookActions()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("钩子操作", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("安装所有已启用的钩子", GUILayout.Height(30)))
            {
                GitHookMonitor.InstallEnabledHooks();
                m_HookStatus = GitHookMonitor.GetHookStatus();
            }
            
            if (GUILayout.Button("卸载所有钩子", GUILayout.Height(30)))
            {
                if (EditorUtility.DisplayDialog("卸载所有钩子", "确定要卸载所有Git钩子吗？", "确定", "取消"))
                {
                    GitHookMonitor.UninstallAllHooks();
                    m_HookStatus = GitHookMonitor.GetHookStatus();
                }
            }
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space();
            
            if (GUILayout.Button("刷新钩子状态", GUILayout.Height(30)))
            {
                GitHookManager.RefreshHookStatusCache();
                m_HookStatus = GitHookMonitor.GetHookStatus();
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
            
            NotificationSystem.ShowNotification("Git钩子设置已保存", NotificationType.Success);
        }
        
        /// <summary>
        /// 重置设置
        /// </summary>
        private void ResetSettings()
        {
            if (EditorUtility.DisplayDialog("重置设置", "确定要重置所有Git钩子设置吗？这将恢复默认设置。", "确定", "取消"))
            {
                // 创建新的Git钩子配置
                m_Config.GitHookConfig = new GitHookConfig();
                
                // 保存配置
                ConfigProvider.SaveConfig();
                
                m_IsDirty = false;
                
                NotificationSystem.ShowNotification("Git钩子设置已重置为默认值", NotificationType.Info);
            }
        }
        
        /// <summary>
        /// 创建设置提供者
        /// </summary>
        /// <returns>设置提供者实例</returns>
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new GitHookSettingsProvider(c_SettingsPath, SettingsScope.Project, s_Keywords);
        }
    }
} 