using System;
using System.Collections.Generic;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.Config
{
    /// <summary>
    /// Git提交规范配置
    /// </summary>
    [Serializable]
    public class GitCommitConfig
    {
        /// <summary>
        /// 提交类型
        /// </summary>
        [Serializable]
        public class CommitType
        {
            /// <summary>
            /// 类型标识
            /// </summary>
            [SerializeField] 
            private string m_Type;
            
            /// <summary>
            /// 类型描述
            /// </summary>
            [SerializeField] 
            private string m_Description;
            
            /// <summary>
            /// 是否启用
            /// </summary>
            [SerializeField] 
            private bool m_Enabled = true;
            
            /// <summary>
            /// 构造函数
            /// </summary>
            public CommitType() { }
            
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="_type">类型标识</param>
            /// <param name="_description">类型描述</param>
            /// <param name="_enabled">是否启用</param>
            public CommitType(string _type, string _description, bool _enabled = true)
            {
                m_Type = _type;
                m_Description = _description;
                m_Enabled = _enabled;
            }
            
            /// <summary>
            /// 类型标识
            /// </summary>
            public string Type
            {
                get => m_Type;
                set => m_Type = value;
            }
            
            /// <summary>
            /// 类型描述
            /// </summary>
            public string Description
            {
                get => m_Description;
                set => m_Description = value;
            }
            
            /// <summary>
            /// 是否启用
            /// </summary>
            public bool Enabled
            {
                get => m_Enabled;
                set => m_Enabled = value;
            }
        }
        
        /// <summary>
        /// 是否强制使用提交模板
        /// </summary>
        [SerializeField] 
        private bool m_ForceUseTemplate = true;
        
        /// <summary>
        /// 是否要求提交类型
        /// </summary>
        [SerializeField] 
        private bool m_RequireType = true;
        
        /// <summary>
        /// 是否要求作用域
        /// </summary>
        [SerializeField] 
        private bool m_RequireScope = false;
        
        /// <summary>
        /// 是否要求简短描述
        /// </summary>
        [SerializeField] 
        private bool m_RequireSubject = true;
        
        /// <summary>
        /// 是否要求详细描述
        /// </summary>
        [SerializeField] 
        private bool m_RequireBody = false;
        
        /// <summary>
        /// 是否要求关闭的问题
        /// </summary>
        [SerializeField] 
        private bool m_RequireFooter = false;
        
        /// <summary>
        /// 简短描述的最大长度
        /// </summary>
        [SerializeField] 
        private int m_SubjectMaxLength = 100;
        
        /// <summary>
        /// 提交类型列表
        /// </summary>
        [SerializeField] 
        private List<CommitType> m_CommitTypes = new List<CommitType>();
        
        /// <summary>
        /// 作用域列表
        /// </summary>
        [SerializeField] 
        private List<string> m_Scopes = new List<string>();
        
        /// <summary>
        /// 构造函数，初始化默认提交类型
        /// </summary>
        public GitCommitConfig()
        {
            InitDefaultCommitTypes();
            InitDefaultScopes();
        }
        
        /// <summary>
        /// 初始化默认提交类型
        /// </summary>
        private void InitDefaultCommitTypes()
        {
            m_CommitTypes.Add(new CommitType("feat", "新功能"));
            m_CommitTypes.Add(new CommitType("fix", "修复Bug"));
            m_CommitTypes.Add(new CommitType("docs", "文档更新"));
            m_CommitTypes.Add(new CommitType("style", "代码风格调整（不影响功能）"));
            m_CommitTypes.Add(new CommitType("refactor", "代码重构"));
            m_CommitTypes.Add(new CommitType("perf", "性能优化"));
            m_CommitTypes.Add(new CommitType("test", "测试相关"));
            m_CommitTypes.Add(new CommitType("build", "构建系统或外部依赖项更改"));
            m_CommitTypes.Add(new CommitType("ci", "CI配置更改"));
            m_CommitTypes.Add(new CommitType("chore", "其他修改"));
            m_CommitTypes.Add(new CommitType("revert", "回退提交"));
        }
        
        /// <summary>
        /// 初始化默认作用域
        /// </summary>
        private void InitDefaultScopes()
        {
            m_Scopes.Add("core");
            m_Scopes.Add("ui");
            m_Scopes.Add("config");
            m_Scopes.Add("git");
            m_Scopes.Add("editor");
            m_Scopes.Add("docs");
        }
        
        /// <summary>
        /// 是否强制使用提交模板
        /// </summary>
        public bool ForceUseTemplate
        {
            get => m_ForceUseTemplate;
            set => m_ForceUseTemplate = value;
        }
        
        /// <summary>
        /// 是否要求提交类型
        /// </summary>
        public bool RequireType
        {
            get => m_RequireType;
            set => m_RequireType = value;
        }
        
        /// <summary>
        /// 是否要求作用域
        /// </summary>
        public bool RequireScope
        {
            get => m_RequireScope;
            set => m_RequireScope = value;
        }
        
        /// <summary>
        /// 是否要求简短描述
        /// </summary>
        public bool RequireSubject
        {
            get => m_RequireSubject;
            set => m_RequireSubject = value;
        }
        
        /// <summary>
        /// 是否要求详细描述
        /// </summary>
        public bool RequireBody
        {
            get => m_RequireBody;
            set => m_RequireBody = value;
        }
        
        /// <summary>
        /// 是否要求关闭的问题
        /// </summary>
        public bool RequireFooter
        {
            get => m_RequireFooter;
            set => m_RequireFooter = value;
        }
        
        /// <summary>
        /// 简短描述的最大长度
        /// </summary>
        public int SubjectMaxLength
        {
            get => m_SubjectMaxLength;
            set => m_SubjectMaxLength = value;
        }
        
        /// <summary>
        /// 提交类型列表
        /// </summary>
        public List<CommitType> CommitTypes
        {
            get => m_CommitTypes;
            set => m_CommitTypes = value;
        }
        
        /// <summary>
        /// 作用域列表
        /// </summary>
        public List<string> Scopes
        {
            get => m_Scopes;
            set => m_Scopes = value;
        }
    }
} 