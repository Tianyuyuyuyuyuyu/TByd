using System;
using TByd.CodeStyle.Runtime.Git;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.Config
{
    /// <summary>
    /// 代码风格配置
    /// </summary>
    public class CodeStyleConfig : ScriptableObject
    {
        /// <summary>
        /// 配置版本号，用于配置迁移
        /// </summary>
        [SerializeField]
        private int m_ConfigVersion = 1;

        /// <summary>
        /// 是否启用Git提交规范检查
        /// </summary>
        [SerializeField]
        private bool m_EnableGitCommitCheck = true;

        /// <summary>
        /// 是否启用代码风格检查
        /// </summary>
        [SerializeField]
        private bool m_EnableCodeStyleCheck = true;

        /// <summary>
        /// 是否启用EditorConfig支持
        /// </summary>
        [SerializeField]
        private bool m_EnableEditorConfig = true;

        /// <summary>
        /// 是否在编译时自动检查代码风格
        /// </summary>
        [SerializeField]
        private bool m_CheckOnCompile = false;

        /// <summary>
        /// 是否在Git提交前检查代码风格
        /// </summary>
        [SerializeField]
        private bool m_CheckBeforeCommit = true;

        /// <summary>
        /// 自定义Git仓库路径，为空则使用Unity项目根目录
        /// </summary>
        [SerializeField]
        private string m_CustomGitRepositoryPath = string.Empty;

        /// <summary>
        /// Git提交规范配置
        /// </summary>
        [SerializeField]
        private GitCommitConfig m_GitCommitConfig = new GitCommitConfig();

        /// <summary>
        /// 代码风格检查配置
        /// </summary>
        [SerializeField]
        private CodeCheckConfig m_CodeCheckConfig = new CodeCheckConfig();

        /// <summary>
        /// Git钩子配置
        /// </summary>
        [SerializeField]
        private GitHookConfig m_GitHookConfig = new GitHookConfig();

        /// <summary>
        /// 是否启用IDE集成
        /// </summary>
        [SerializeField]
        private bool m_EnableIDEIntegration = true;

        /// <summary>
        /// 是否自动配置IDE
        /// </summary>
        [SerializeField]
        private bool m_AutoConfigureIDE = true;

        /// <summary>
        /// 是否同步EditorConfig到IDE
        /// </summary>
        [SerializeField]
        private bool m_SyncEditorConfigWithIDE = true;

        /// <summary>
        /// Rider配置
        /// </summary>
        [SerializeField]
        private RiderConfig m_RiderConfig = new RiderConfig();

        /// <summary>
        /// Visual Studio配置
        /// </summary>
        [SerializeField]
        private VisualStudioConfig m_VisualStudioConfig = new VisualStudioConfig();

        /// <summary>
        /// VS Code配置
        /// </summary>
        [SerializeField]
        private VSCodeConfig m_VSCodeConfig = new VSCodeConfig();

        /// <summary>
        /// 配置版本号
        /// </summary>
        public int ConfigVersion
        {
            get => m_ConfigVersion;
            set => m_ConfigVersion = value;
        }

        /// <summary>
        /// 是否启用Git提交规范检查
        /// </summary>
        public bool EnableGitCommitCheck
        {
            get => m_EnableGitCommitCheck;
            set => m_EnableGitCommitCheck = value;
        }

        /// <summary>
        /// 是否启用代码风格检查
        /// </summary>
        public bool EnableCodeStyleCheck
        {
            get => m_EnableCodeStyleCheck;
            set => m_EnableCodeStyleCheck = value;
        }

        /// <summary>
        /// 是否启用EditorConfig支持
        /// </summary>
        public bool EnableEditorConfig
        {
            get => m_EnableEditorConfig;
            set => m_EnableEditorConfig = value;
        }

        /// <summary>
        /// 是否在编译时自动检查代码风格
        /// </summary>
        public bool CheckOnCompile
        {
            get => m_CheckOnCompile;
            set => m_CheckOnCompile = value;
        }

        /// <summary>
        /// 是否在Git提交前检查代码风格
        /// </summary>
        public bool CheckBeforeCommit
        {
            get => m_CheckBeforeCommit;
            set => m_CheckBeforeCommit = value;
        }

        /// <summary>
        /// 自定义Git仓库路径，为空则使用Unity项目根目录
        /// </summary>
        public string CustomGitRepositoryPath
        {
            get => m_CustomGitRepositoryPath;
            set => m_CustomGitRepositoryPath = value;
        }

        /// <summary>
        /// Git提交规范配置
        /// </summary>
        public GitCommitConfig GitCommitConfig
        {
            get => m_GitCommitConfig;
            set => m_GitCommitConfig = value;
        }

        /// <summary>
        /// 代码风格检查配置
        /// </summary>
        public CodeCheckConfig CodeCheckConfig
        {
            get => m_CodeCheckConfig;
            set => m_CodeCheckConfig = value;
        }

        /// <summary>
        /// Git钩子配置
        /// </summary>
        public GitHookConfig GitHookConfig
        {
            get => m_GitHookConfig;
            set => m_GitHookConfig = value;
        }

        /// <summary>
        /// Git设置，用于测试兼容性
        /// </summary>
        public GitHookConfig GitSettings
        {
            get => m_GitHookConfig;
        }

        /// <summary>
        /// 提交消息设置，用于测试兼容性
        /// </summary>
        public GitCommitConfig CommitMessageSettings
        {
            get => m_GitCommitConfig;
        }

        /// <summary>
        /// 是否启用IDE集成
        /// </summary>
        public bool EnableIDEIntegration
        {
            get => m_EnableIDEIntegration;
            set => m_EnableIDEIntegration = value;
        }

        /// <summary>
        /// 是否自动配置IDE
        /// </summary>
        public bool AutoConfigureIDE
        {
            get => m_AutoConfigureIDE;
            set => m_AutoConfigureIDE = value;
        }

        /// <summary>
        /// 是否同步EditorConfig到IDE
        /// </summary>
        public bool SyncEditorConfigWithIDE
        {
            get => m_SyncEditorConfigWithIDE;
            set => m_SyncEditorConfigWithIDE = value;
        }

        /// <summary>
        /// Rider配置
        /// </summary>
        public RiderConfig RiderConfig
        {
            get => m_RiderConfig;
            set => m_RiderConfig = value;
        }

        /// <summary>
        /// Visual Studio配置
        /// </summary>
        public VisualStudioConfig VisualStudioConfig
        {
            get => m_VisualStudioConfig;
            set => m_VisualStudioConfig = value;
        }

        /// <summary>
        /// VS Code配置
        /// </summary>
        public VSCodeConfig VSCodeConfig
        {
            get => m_VSCodeConfig;
            set => m_VSCodeConfig = value;
        }
    }

    /// <summary>
    /// Rider配置
    /// </summary>
    [Serializable]
    public class RiderConfig
    {
        /// <summary>
        /// 是否启用代码分析
        /// </summary>
        [SerializeField]
        private bool m_EnableCodeAnalysis = true;

        /// <summary>
        /// 是否启用StyleCop
        /// </summary>
        [SerializeField]
        private bool m_EnableStyleCop = true;

        /// <summary>
        /// 是否启用ReSharper
        /// </summary>
        [SerializeField]
        private bool m_EnableReSharper = true;

        /// <summary>
        /// 是否启用代码分析
        /// </summary>
        public bool EnableCodeAnalysis
        {
            get => m_EnableCodeAnalysis;
            set => m_EnableCodeAnalysis = value;
        }

        /// <summary>
        /// 是否启用StyleCop
        /// </summary>
        public bool EnableStyleCop
        {
            get => m_EnableStyleCop;
            set => m_EnableStyleCop = value;
        }

        /// <summary>
        /// 是否启用ReSharper
        /// </summary>
        public bool EnableReSharper
        {
            get => m_EnableReSharper;
            set => m_EnableReSharper = value;
        }
    }

    /// <summary>
    /// Visual Studio配置
    /// </summary>
    [Serializable]
    public class VisualStudioConfig
    {
        /// <summary>
        /// 是否启用Roslyn分析器
        /// </summary>
        [SerializeField]
        private bool m_EnableRoslynAnalyzers = true;

        /// <summary>
        /// 是否启用StyleCop
        /// </summary>
        [SerializeField]
        private bool m_EnableStyleCop = true;

        /// <summary>
        /// 是否启用代码分析
        /// </summary>
        [SerializeField]
        private bool m_EnableCodeAnalysis = true;

        /// <summary>
        /// 是否启用Roslyn分析器
        /// </summary>
        public bool EnableRoslynAnalyzers
        {
            get => m_EnableRoslynAnalyzers;
            set => m_EnableRoslynAnalyzers = value;
        }

        /// <summary>
        /// 是否启用StyleCop
        /// </summary>
        public bool EnableStyleCop
        {
            get => m_EnableStyleCop;
            set => m_EnableStyleCop = value;
        }

        /// <summary>
        /// 是否启用代码分析
        /// </summary>
        public bool EnableCodeAnalysis
        {
            get => m_EnableCodeAnalysis;
            set => m_EnableCodeAnalysis = value;
        }
    }

    /// <summary>
    /// VS Code配置
    /// </summary>
    [Serializable]
    public class VSCodeConfig
    {
        /// <summary>
        /// 是否启用OmniSharp
        /// </summary>
        [SerializeField]
        private bool m_EnableOmniSharp = true;

        /// <summary>
        /// 是否启用Roslyn分析器
        /// </summary>
        [SerializeField]
        private bool m_EnableRoslynAnalyzers = true;

        /// <summary>
        /// 是否启用EditorConfig
        /// </summary>
        [SerializeField]
        private bool m_EnableEditorConfig = true;

        /// <summary>
        /// 是否启用OmniSharp
        /// </summary>
        public bool EnableOmniSharp
        {
            get => m_EnableOmniSharp;
            set => m_EnableOmniSharp = value;
        }

        /// <summary>
        /// 是否启用Roslyn分析器
        /// </summary>
        public bool EnableRoslynAnalyzers
        {
            get => m_EnableRoslynAnalyzers;
            set => m_EnableRoslynAnalyzers = value;
        }

        /// <summary>
        /// 是否启用EditorConfig
        /// </summary>
        public bool EnableEditorConfig
        {
            get => m_EnableEditorConfig;
            set => m_EnableEditorConfig = value;
        }
    }
}
