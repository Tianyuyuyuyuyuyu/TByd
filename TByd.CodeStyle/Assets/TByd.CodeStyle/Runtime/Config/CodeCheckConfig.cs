using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace TByd.CodeStyle.Runtime.Config
{
    /// <summary>
    /// 代码检查配置
    /// </summary>
    [Serializable]
    public class CodeCheckConfig
    {
        /// <summary>
        /// 规则严重程度
        /// </summary>
        public enum RuleSeverity
        {
            /// <summary>
            /// 禁用
            /// </summary>
            k_Disabled,

            /// <summary>
            /// 信息
            /// </summary>
            k_Info,

            /// <summary>
            /// 警告
            /// </summary>
            k_Warning,

            /// <summary>
            /// 错误
            /// </summary>
            k_Error
        }

        /// <summary>
        /// 代码检查规则
        /// </summary>
        [Serializable]
        public class CodeRule
        {
            /// <summary>
            /// 规则ID
            /// </summary>
            [FormerlySerializedAs("m_Id")] [SerializeField]
            private string mId;

            /// <summary>
            /// 规则名称
            /// </summary>
            [FormerlySerializedAs("m_Name")] [SerializeField]
            private string mName;

            /// <summary>
            /// 规则描述
            /// </summary>
            [FormerlySerializedAs("m_Description")] [SerializeField]
            private string mDescription;

            /// <summary>
            /// 规则严重程度
            /// </summary>
            [FormerlySerializedAs("m_Severity")] [SerializeField]
            private RuleSeverity mSeverity = RuleSeverity.k_Warning;

            /// <summary>
            /// 规则参数
            /// </summary>
            [SerializeField]
            private Dictionary<string, string> m_Parameters = new Dictionary<string, string>();

            /// <summary>
            /// 构造函数
            /// </summary>
            public CodeRule() { }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="id">规则ID</param>
            /// <param name="name">规则名称</param>
            /// <param name="description">规则描述</param>
            /// <param name="severity">规则严重程度</param>
            public CodeRule(string id, string name, string description, RuleSeverity severity = RuleSeverity.k_Warning)
            {
                mId = id;
                mName = name;
                mDescription = description;
                mSeverity = severity;
            }

            /// <summary>
            /// 规则ID
            /// </summary>
            public string Id
            {
                get => mId;
                set => mId = value;
            }

            /// <summary>
            /// 规则名称
            /// </summary>
            public string Name
            {
                get => mName;
                set => mName = value;
            }

            /// <summary>
            /// 规则描述
            /// </summary>
            public string Description
            {
                get => mDescription;
                set => mDescription = value;
            }

            /// <summary>
            /// 规则严重程度
            /// </summary>
            public RuleSeverity Severity
            {
                get => mSeverity;
                set => mSeverity = value;
            }

            /// <summary>
            /// 规则参数
            /// </summary>
            public Dictionary<string, string> Parameters
            {
                get => m_Parameters;
                set => m_Parameters = value;
            }

            /// <summary>
            /// 设置参数
            /// </summary>
            /// <param name="key">参数名</param>
            /// <param name="value">参数值</param>
            public void SetParameter(string key, string value)
            {
                if (m_Parameters.ContainsKey(key))
                {
                    m_Parameters[key] = value;
                }
                else
                {
                    m_Parameters.Add(key, value);
                }
            }

            /// <summary>
            /// 获取参数
            /// </summary>
            /// <param name="key">参数名</param>
            /// <param name="defaultValue">默认值</param>
            /// <returns>参数值</returns>
            public string GetParameter(string key, string defaultValue = "")
            {
                return m_Parameters.TryGetValue(key, out var value) ? value : defaultValue;
            }
        }

        /// <summary>
        /// 是否忽略生成的代码
        /// </summary>
        [FormerlySerializedAs("m_IgnoreGeneratedCode")] [SerializeField]
        private bool mIgnoreGeneratedCode = true;

        /// <summary>
        /// 是否忽略第三方代码
        /// </summary>
        [FormerlySerializedAs("m_IgnoreThirdPartyCode")] [SerializeField]
        private bool mIgnoreThirdPartyCode = true;

        /// <summary>
        /// 是否忽略测试代码
        /// </summary>
        [FormerlySerializedAs("m_IgnoreTestCode")] [SerializeField]
        private bool mIgnoreTestCode = false;

        /// <summary>
        /// 是否在保存时检查代码
        /// </summary>
        [FormerlySerializedAs("m_CheckOnSave")] [SerializeField]
        private bool mCheckOnSave = true;

        /// <summary>
        /// 是否在构建时检查代码
        /// </summary>
        [FormerlySerializedAs("m_CheckOnBuild")] [SerializeField]
        private bool mCheckOnBuild = true;

        /// <summary>
        /// 是否在提交时检查代码
        /// </summary>
        [FormerlySerializedAs("m_CheckOnCommit")] [SerializeField]
        private bool mCheckOnCommit = true;

        /// <summary>
        /// 是否在保存时自动修复代码
        /// </summary>
        [FormerlySerializedAs("m_FixOnSave")] [SerializeField]
        private bool mFixOnSave = false;

        /// <summary>
        /// 忽略的文件或目录
        /// </summary>
        [FormerlySerializedAs("m_IgnoredPaths")] [SerializeField]
        private List<string> mIgnoredPaths = new List<string>();

        /// <summary>
        /// 代码规则列表
        /// </summary>
        [FormerlySerializedAs("m_Rules")] [SerializeField]
        private List<CodeRule> mRules = new List<CodeRule>();

        /// <summary>
        /// 构造函数，初始化默认规则
        /// </summary>
        public CodeCheckConfig()
        {
            InitDefaultRules();
            InitDefaultIgnoredPaths();
        }

        /// <summary>
        /// 初始化默认规则
        /// </summary>
        private void InitDefaultRules()
        {
            // 命名规则
            mRules.Add(new CodeRule("CS0001", "类名使用PascalCase", "类名应该使用PascalCase命名法", RuleSeverity.k_Error));
            mRules.Add(new CodeRule("CS0002", "方法名使用PascalCase", "方法名应该使用PascalCase命名法", RuleSeverity.k_Error));
            mRules.Add(new CodeRule("CS0003", "私有成员变量使用m_前缀", "私有成员变量应该使用m_前缀", RuleSeverity.k_Warning));
            mRules.Add(new CodeRule("CS0004", "常量使用c_前缀", "常量应该使用c_前缀", RuleSeverity.k_Warning));
            mRules.Add(new CodeRule("CS0005", "静态变量使用s_前缀", "静态变量应该使用s_前缀", RuleSeverity.k_Warning));
            mRules.Add(new CodeRule("CS0006", "参数使用_前缀", "参数应该使用_前缀", RuleSeverity.k_Warning));

            // 格式规则
            mRules.Add(new CodeRule("CS0101", "使用空格而不是制表符", "应该使用空格而不是制表符进行缩进", RuleSeverity.k_Warning));
            mRules.Add(new CodeRule("CS0102", "缩进使用4个空格", "缩进应该使用4个空格", RuleSeverity.k_Warning));
            mRules.Add(new CodeRule("CS0103", "大括号应该独占一行", "大括号应该独占一行", RuleSeverity.k_Info));

            // Unity特定规则
            mRules.Add(new CodeRule("UN0001", "避免使用GameObject.Find", "应该避免使用GameObject.Find，因为它性能较低", RuleSeverity.k_Warning));
            mRules.Add(new CodeRule("UN0002", "避免在Update中使用GetComponent", "应该避免在Update中使用GetComponent，因为它性能较低", RuleSeverity.k_Warning));
            mRules.Add(new CodeRule("UN0003", "避免使用SendMessage", "应该避免使用SendMessage，因为它性能较低", RuleSeverity.k_Warning));
        }

        /// <summary>
        /// 初始化默认忽略路径
        /// </summary>
        private void InitDefaultIgnoredPaths()
        {
            mIgnoredPaths.Add("Assets/Plugins/");
            mIgnoredPaths.Add("Assets/ThirdParty/");
            mIgnoredPaths.Add("Assets/Generated/");
        }

        /// <summary>
        /// 是否忽略生成的代码
        /// </summary>
        public bool IgnoreGeneratedCode
        {
            get => mIgnoreGeneratedCode;
            set => mIgnoreGeneratedCode = value;
        }

        /// <summary>
        /// 是否忽略第三方代码
        /// </summary>
        public bool IgnoreThirdPartyCode
        {
            get => mIgnoreThirdPartyCode;
            set => mIgnoreThirdPartyCode = value;
        }

        /// <summary>
        /// 是否忽略测试代码
        /// </summary>
        public bool IgnoreTestCode
        {
            get => mIgnoreTestCode;
            set => mIgnoreTestCode = value;
        }

        /// <summary>
        /// 是否在保存时检查代码
        /// </summary>
        public bool CheckOnSave
        {
            get => mCheckOnSave;
            set => mCheckOnSave = value;
        }

        /// <summary>
        /// 是否在构建时检查代码
        /// </summary>
        public bool CheckOnBuild
        {
            get => mCheckOnBuild;
            set => mCheckOnBuild = value;
        }

        /// <summary>
        /// 是否在提交时检查代码
        /// </summary>
        public bool CheckOnCommit
        {
            get => mCheckOnCommit;
            set => mCheckOnCommit = value;
        }

        /// <summary>
        /// 是否在保存时自动修复代码
        /// </summary>
        public bool FixOnSave
        {
            get => mFixOnSave;
            set => mFixOnSave = value;
        }

        /// <summary>
        /// 忽略的文件或目录
        /// </summary>
        public List<string> IgnoredPaths
        {
            get => mIgnoredPaths;
            set => mIgnoredPaths = value;
        }

        /// <summary>
        /// 代码规则列表
        /// </summary>
        public List<CodeRule> Rules
        {
            get => mRules;
            set => mRules = value;
        }

        /// <summary>
        /// 获取规则
        /// </summary>
        /// <param name="id">规则ID</param>
        /// <returns>规则</returns>
        public CodeRule GetRule(string id)
        {
            return mRules.Find(rule => rule.Id == id);
        }

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="rule">规则</param>
        public void AddRule(CodeRule rule)
        {
            var existingRule = GetRule(rule.Id);
            if (existingRule != null)
            {
                mRules.Remove(existingRule);
            }

            mRules.Add(rule);
        }

        /// <summary>
        /// 移除规则
        /// </summary>
        /// <param name="id">规则ID</param>
        public void RemoveRule(string id)
        {
            var rule = GetRule(id);
            if (rule != null)
            {
                mRules.Remove(rule);
            }
        }
    }
}
