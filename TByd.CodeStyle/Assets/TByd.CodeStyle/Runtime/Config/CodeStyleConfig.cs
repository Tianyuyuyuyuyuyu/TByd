using System;
using TByd.CodeStyle.Runtime.Git;
using UnityEngine;
using UnityEngine.Serialization;

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
        [FormerlySerializedAs("m_ConfigVersion")] [SerializeField]
        private int mConfigVersion = 1;

        /// <summary>
        /// 是否启用Git提交规范检查
        /// </summary>
        [FormerlySerializedAs("m_EnableGitCommitCheck")] [SerializeField]
        private bool mEnableGitCommitCheck = true;

        /// <summary>
        /// 是否启用代码风格检查
        /// </summary>
        [FormerlySerializedAs("m_EnableCodeStyleCheck")] [SerializeField]
        private bool mEnableCodeStyleCheck = true;

        /// <summary>
        /// 是否启用EditorConfig支持
        /// </summary>
        [FormerlySerializedAs("m_EnableEditorConfig")] [SerializeField]
        private bool mEnableEditorConfig = true;

        /// <summary>
        /// 是否在编译时自动检查代码风格
        /// </summary>
        [FormerlySerializedAs("m_CheckOnCompile")] [SerializeField]
        private bool mCheckOnCompile = false;

        /// <summary>
        /// 是否在Git提交前检查代码风格
        /// </summary>
        [FormerlySerializedAs("m_CheckBeforeCommit")] [SerializeField]
        private bool mCheckBeforeCommit = true;

        /// <summary>
        /// 自定义Git仓库路径，为空则使用Unity项目根目录
        /// </summary>
        [FormerlySerializedAs("m_CustomGitRepositoryPath")] [SerializeField]
        private string mCustomGitRepositoryPath = string.Empty;

        /// <summary>
        /// Git提交规范配置
        /// </summary>
        [FormerlySerializedAs("m_GitCommitConfig")] [SerializeField]
        private GitCommitConfig mGitCommitConfig = new GitCommitConfig();

        /// <summary>
        /// 代码风格检查配置
        /// </summary>
        [FormerlySerializedAs("m_CodeCheckConfig")] [SerializeField]
        private CodeCheckConfig mCodeCheckConfig = new CodeCheckConfig();

        /// <summary>
        /// Git钩子配置
        /// </summary>
        [FormerlySerializedAs("m_GitHookConfig")] [SerializeField]
        private GitHookConfig mGitHookConfig = new GitHookConfig();

        /// <summary>
        /// 是否启用IDE集成
        /// </summary>
        [FormerlySerializedAs("m_EnableIDEIntegration")] [SerializeField]
        private bool mEnableIdeIntegration = true;

        /// <summary>
        /// 是否自动配置IDE
        /// </summary>
        [FormerlySerializedAs("m_AutoConfigureIDE")] [SerializeField]
        private bool mAutoConfigureIde = true;

        /// <summary>
        /// 是否同步EditorConfig到IDE
        /// </summary>
        [FormerlySerializedAs("m_SyncEditorConfigWithIDE")] [SerializeField]
        private bool mSyncEditorConfigWithIde = true;

        /// <summary>
        /// Rider配置
        /// </summary>
        [FormerlySerializedAs("m_RiderConfig")] [SerializeField]
        private RiderConfig mRiderConfig = new RiderConfig();

        /// <summary>
        /// Visual Studio配置
        /// </summary>
        [FormerlySerializedAs("m_VisualStudioConfig")] [SerializeField]
        private VisualStudioConfig mVisualStudioConfig = new VisualStudioConfig();

        /// <summary>
        /// VS Code配置
        /// </summary>
        [FormerlySerializedAs("m_VSCodeConfig")] [SerializeField]
        private VSCodeConfig mVSCodeConfig = new VSCodeConfig();

        /// <summary>
        /// 配置版本号
        /// </summary>
        public int ConfigVersion
        {
            get => mConfigVersion;
            set => mConfigVersion = value;
        }

        /// <summary>
        /// 是否启用Git提交规范检查
        /// </summary>
        public bool EnableGitCommitCheck
        {
            get => mEnableGitCommitCheck;
            set => mEnableGitCommitCheck = value;
        }

        /// <summary>
        /// 是否启用代码风格检查
        /// </summary>
        public bool EnableCodeStyleCheck
        {
            get => mEnableCodeStyleCheck;
            set => mEnableCodeStyleCheck = value;
        }

        /// <summary>
        /// 是否启用EditorConfig支持
        /// </summary>
        public bool EnableEditorConfig
        {
            get => mEnableEditorConfig;
            set => mEnableEditorConfig = value;
        }

        /// <summary>
        /// 是否在编译时自动检查代码风格
        /// </summary>
        public bool CheckOnCompile
        {
            get => mCheckOnCompile;
            set => mCheckOnCompile = value;
        }

        /// <summary>
        /// 是否在Git提交前检查代码风格
        /// </summary>
        public bool CheckBeforeCommit
        {
            get => mCheckBeforeCommit;
            set => mCheckBeforeCommit = value;
        }

        /// <summary>
        /// 自定义Git仓库路径，为空则使用Unity项目根目录
        /// </summary>
        public string CustomGitRepositoryPath
        {
            get => mCustomGitRepositoryPath;
            set => mCustomGitRepositoryPath = value;
        }

        /// <summary>
        /// Git提交规范配置
        /// </summary>
        public GitCommitConfig GitCommitConfig
        {
            get => mGitCommitConfig;
            set => mGitCommitConfig = value;
        }

        /// <summary>
        /// 代码风格检查配置
        /// </summary>
        public CodeCheckConfig CodeCheckConfig
        {
            get => mCodeCheckConfig;
            set => mCodeCheckConfig = value;
        }

        /// <summary>
        /// Git钩子配置
        /// </summary>
        public GitHookConfig GitHookConfig
        {
            get => mGitHookConfig;
            set => mGitHookConfig = value;
        }

        /// <summary>
        /// Git设置，用于测试兼容性
        /// </summary>
        public GitHookConfig GitSettings
        {
            get => mGitHookConfig;
        }

        /// <summary>
        /// 提交消息设置，用于测试兼容性
        /// </summary>
        public GitCommitConfig CommitMessageSettings
        {
            get => mGitCommitConfig;
        }

        /// <summary>
        /// 是否启用IDE集成
        /// </summary>
        public bool EnableIdeIntegration
        {
            get => mEnableIdeIntegration;
            set => mEnableIdeIntegration = value;
        }

        /// <summary>
        /// 是否自动配置IDE
        /// </summary>
        public bool AutoConfigureIde
        {
            get => mAutoConfigureIde;
            set => mAutoConfigureIde = value;
        }

        /// <summary>
        /// 是否同步EditorConfig到IDE
        /// </summary>
        public bool SyncEditorConfigWithIde
        {
            get => mSyncEditorConfigWithIde;
            set => mSyncEditorConfigWithIde = value;
        }

        /// <summary>
        /// Rider配置
        /// </summary>
        public RiderConfig RiderConfig
        {
            get => mRiderConfig;
            set => mRiderConfig = value;
        }

        /// <summary>
        /// Visual Studio配置
        /// </summary>
        public VisualStudioConfig VisualStudioConfig
        {
            get => mVisualStudioConfig;
            set => mVisualStudioConfig = value;
        }

        /// <summary>
        /// VS Code配置
        /// </summary>
        public VSCodeConfig VSCodeConfig
        {
            get => mVSCodeConfig;
            set => mVSCodeConfig = value;
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
        [FormerlySerializedAs("m_EnableCodeAnalysis")] [SerializeField]
        private bool mEnableCodeAnalysis = true;

        /// <summary>
        /// 是否启用StyleCop
        /// </summary>
        [FormerlySerializedAs("m_EnableStyleCop")] [SerializeField]
        private bool mEnableStyleCop = true;

        /// <summary>
        /// 是否启用ReSharper
        /// </summary>
        [FormerlySerializedAs("m_EnableReSharper")] [SerializeField]
        private bool mEnableReSharper = true;

        /// <summary>
        /// 是否启用代码分析
        /// </summary>
        public bool EnableCodeAnalysis
        {
            get => mEnableCodeAnalysis;
            set => mEnableCodeAnalysis = value;
        }

        /// <summary>
        /// 是否启用StyleCop
        /// </summary>
        public bool EnableStyleCop
        {
            get => mEnableStyleCop;
            set => mEnableStyleCop = value;
        }

        /// <summary>
        /// 是否启用ReSharper
        /// </summary>
        public bool EnableReSharper
        {
            get => mEnableReSharper;
            set => mEnableReSharper = value;
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
        [FormerlySerializedAs("m_EnableRoslynAnalyzers")] [SerializeField]
        private bool mEnableRoslynAnalyzers = true;

        /// <summary>
        /// 是否启用StyleCop
        /// </summary>
        [FormerlySerializedAs("m_EnableStyleCop")] [SerializeField]
        private bool mEnableStyleCop = true;

        /// <summary>
        /// 是否启用代码分析
        /// </summary>
        [FormerlySerializedAs("m_EnableCodeAnalysis")] [SerializeField]
        private bool mEnableCodeAnalysis = true;

        /// <summary>
        /// 是否启用Roslyn分析器
        /// </summary>
        public bool EnableRoslynAnalyzers
        {
            get => mEnableRoslynAnalyzers;
            set => mEnableRoslynAnalyzers = value;
        }

        /// <summary>
        /// 是否启用StyleCop
        /// </summary>
        public bool EnableStyleCop
        {
            get => mEnableStyleCop;
            set => mEnableStyleCop = value;
        }

        /// <summary>
        /// 是否启用代码分析
        /// </summary>
        public bool EnableCodeAnalysis
        {
            get => mEnableCodeAnalysis;
            set => mEnableCodeAnalysis = value;
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
        [FormerlySerializedAs("m_EnableOmniSharp")] [SerializeField]
        private bool mEnableOmniSharp = true;

        /// <summary>
        /// 是否启用Roslyn分析器
        /// </summary>
        [FormerlySerializedAs("m_EnableRoslynAnalyzers")] [SerializeField]
        private bool mEnableRoslynAnalyzers = true;

        /// <summary>
        /// 是否启用EditorConfig
        /// </summary>
        [FormerlySerializedAs("m_EnableEditorConfig")] [SerializeField]
        private bool mEnableEditorConfig = true;

        /// <summary>
        /// 是否启用OmniSharp
        /// </summary>
        public bool EnableOmniSharp
        {
            get => mEnableOmniSharp;
            set => mEnableOmniSharp = value;
        }

        /// <summary>
        /// 是否启用Roslyn分析器
        /// </summary>
        public bool EnableRoslynAnalyzers
        {
            get => mEnableRoslynAnalyzers;
            set => mEnableRoslynAnalyzers = value;
        }

        /// <summary>
        /// 是否启用EditorConfig
        /// </summary>
        public bool EnableEditorConfig
        {
            get => mEnableEditorConfig;
            set => mEnableEditorConfig = value;
        }
    }
}
