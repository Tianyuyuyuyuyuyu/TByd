using System;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.Config
{
    /// <summary>
    /// 代码风格配置数据
    /// </summary>
    [Serializable]
    public class CodeStyleConfig
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
    }
} 