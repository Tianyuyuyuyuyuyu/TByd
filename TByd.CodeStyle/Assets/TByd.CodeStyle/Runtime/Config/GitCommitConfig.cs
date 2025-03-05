using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
            [FormerlySerializedAs("m_Type")] [SerializeField]
            private string mType;

            /// <summary>
            /// 类型描述
            /// </summary>
            [FormerlySerializedAs("m_Description")] [SerializeField]
            private string mDescription;

            /// <summary>
            /// 是否启用
            /// </summary>
            [FormerlySerializedAs("m_Enabled")] [SerializeField]
            private bool mEnabled = true;

            /// <summary>
            /// 构造函数
            /// </summary>
            public CommitType() { }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="type">类型标识</param>
            /// <param name="description">类型描述</param>
            /// <param name="enabled">是否启用</param>
            public CommitType(string type, string description, bool enabled = true)
            {
                mType = type;
                mDescription = description;
                mEnabled = enabled;
            }

            /// <summary>
            /// 类型标识
            /// </summary>
            public string Type
            {
                get => mType;
                set => mType = value;
            }

            /// <summary>
            /// 类型描述
            /// </summary>
            public string Description
            {
                get => mDescription;
                set => mDescription = value;
            }

            /// <summary>
            /// 是否启用
            /// </summary>
            public bool Enabled
            {
                get => mEnabled;
                set => mEnabled = value;
            }
        }

        /// <summary>
        /// 是否强制使用提交模板
        /// </summary>
        [FormerlySerializedAs("m_ForceUseTemplate")] [SerializeField]
        private bool mForceUseTemplate = true;

        /// <summary>
        /// 是否要求提交类型
        /// </summary>
        [FormerlySerializedAs("m_RequireType")] [SerializeField]
        private bool mRequireType = true;

        /// <summary>
        /// 是否要求作用域
        /// </summary>
        [FormerlySerializedAs("m_RequireScope")] [SerializeField]
        private bool mRequireScope = false;

        /// <summary>
        /// 是否要求简短描述
        /// </summary>
        [FormerlySerializedAs("m_RequireSubject")] [SerializeField]
        private bool mRequireSubject = true;

        /// <summary>
        /// 是否要求详细描述
        /// </summary>
        [FormerlySerializedAs("m_RequireBody")] [SerializeField]
        private bool mRequireBody = false;

        /// <summary>
        /// 是否要求关闭的问题
        /// </summary>
        [FormerlySerializedAs("m_RequireFooter")] [SerializeField]
        private bool mRequireFooter = false;

        /// <summary>
        /// 简短描述的最大长度
        /// </summary>
        [FormerlySerializedAs("m_SubjectMaxLength")] [SerializeField]
        private int mSubjectMaxLength = 100;

        /// <summary>
        /// 提交类型列表
        /// </summary>
        [FormerlySerializedAs("m_CommitTypes")] [SerializeField]
        private List<CommitType> mCommitTypes = new List<CommitType>();

        /// <summary>
        /// 作用域列表
        /// </summary>
        [FormerlySerializedAs("m_Scopes")] [SerializeField]
        private List<string> mScopes = new List<string>();

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
            mCommitTypes.Add(new CommitType("feat", "新功能"));
            mCommitTypes.Add(new CommitType("fix", "修复Bug"));
            mCommitTypes.Add(new CommitType("docs", "文档更新"));
            mCommitTypes.Add(new CommitType("style", "代码风格调整（不影响功能）"));
            mCommitTypes.Add(new CommitType("refactor", "代码重构"));
            mCommitTypes.Add(new CommitType("perf", "性能优化"));
            mCommitTypes.Add(new CommitType("test", "测试相关"));
            mCommitTypes.Add(new CommitType("build", "构建系统或外部依赖项更改"));
            mCommitTypes.Add(new CommitType("ci", "CI配置更改"));
            mCommitTypes.Add(new CommitType("chore", "其他修改"));
            mCommitTypes.Add(new CommitType("revert", "回退提交"));
        }

        /// <summary>
        /// 初始化默认作用域
        /// </summary>
        private void InitDefaultScopes()
        {
            mScopes.Add("core");
            mScopes.Add("ui");
            mScopes.Add("config");
            mScopes.Add("git");
            mScopes.Add("editor");
            mScopes.Add("docs");
        }

        /// <summary>
        /// 是否强制使用提交模板
        /// </summary>
        public bool ForceUseTemplate
        {
            get => mForceUseTemplate;
            set => mForceUseTemplate = value;
        }

        /// <summary>
        /// 是否要求提交类型
        /// </summary>
        public bool RequireType
        {
            get => mRequireType;
            set => mRequireType = value;
        }

        /// <summary>
        /// 是否要求作用域
        /// </summary>
        public bool RequireScope
        {
            get => mRequireScope;
            set => mRequireScope = value;
        }

        /// <summary>
        /// 是否要求简短描述
        /// </summary>
        public bool RequireSubject
        {
            get => mRequireSubject;
            set => mRequireSubject = value;
        }

        /// <summary>
        /// 是否要求详细描述
        /// </summary>
        public bool RequireBody
        {
            get => mRequireBody;
            set => mRequireBody = value;
        }

        /// <summary>
        /// 是否要求关闭的问题
        /// </summary>
        public bool RequireFooter
        {
            get => mRequireFooter;
            set => mRequireFooter = value;
        }

        /// <summary>
        /// 简短描述的最大长度
        /// </summary>
        public int SubjectMaxLength
        {
            get => mSubjectMaxLength;
            set => mSubjectMaxLength = value;
        }

        /// <summary>
        /// 提交类型列表
        /// </summary>
        public List<CommitType> CommitTypes
        {
            get => mCommitTypes;
            set => mCommitTypes = value;
        }

        /// <summary>
        /// 作用域列表
        /// </summary>
        public List<string> Scopes
        {
            get => mScopes;
            set => mScopes = value;
        }
    }
}
