using System;
using System.Collections.Generic;
using UnityEngine;

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
            Disabled,

            /// <summary>
            /// 信息
            /// </summary>
            Info,

            /// <summary>
            /// 警告
            /// </summary>
            Warning,

            /// <summary>
            /// 错误
            /// </summary>
            Error
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
            [SerializeField]
            private string m_Id;

            /// <summary>
            /// 规则名称
            /// </summary>
            [SerializeField]
            private string m_Name;

            /// <summary>
            /// 规则描述
            /// </summary>
            [SerializeField]
            private string m_Description;

            /// <summary>
            /// 规则严重程度
            /// </summary>
            [SerializeField]
            private RuleSeverity m_Severity = RuleSeverity.Warning;

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
            /// <param name="_id">规则ID</param>
            /// <param name="_name">规则名称</param>
            /// <param name="_description">规则描述</param>
            /// <param name="_severity">规则严重程度</param>
            public CodeRule(string _id, string _name, string _description, RuleSeverity _severity = RuleSeverity.Warning)
            {
                m_Id = _id;
                m_Name = _name;
                m_Description = _description;
                m_Severity = _severity;
            }

            /// <summary>
            /// 规则ID
            /// </summary>
            public string Id
            {
                get => m_Id;
                set => m_Id = value;
            }

            /// <summary>
            /// 规则名称
            /// </summary>
            public string Name
            {
                get => m_Name;
                set => m_Name = value;
            }

            /// <summary>
            /// 规则描述
            /// </summary>
            public string Description
            {
                get => m_Description;
                set => m_Description = value;
            }

            /// <summary>
            /// 规则严重程度
            /// </summary>
            public RuleSeverity Severity
            {
                get => m_Severity;
                set => m_Severity = value;
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
            /// <param name="_key">参数名</param>
            /// <param name="_value">参数值</param>
            public void SetParameter(string _key, string _value)
            {
                if (m_Parameters.ContainsKey(_key))
                {
                    m_Parameters[_key] = _value;
                }
                else
                {
                    m_Parameters.Add(_key, _value);
                }
            }

            /// <summary>
            /// 获取参数
            /// </summary>
            /// <param name="_key">参数名</param>
            /// <param name="_defaultValue">默认值</param>
            /// <returns>参数值</returns>
            public string GetParameter(string _key, string _defaultValue = "")
            {
                return m_Parameters.TryGetValue(_key, out var value) ? value : _defaultValue;
            }
        }

        /// <summary>
        /// 是否忽略生成的代码
        /// </summary>
        [SerializeField]
        private bool m_IgnoreGeneratedCode = true;

        /// <summary>
        /// 是否忽略第三方代码
        /// </summary>
        [SerializeField]
        private bool m_IgnoreThirdPartyCode = true;

        /// <summary>
        /// 是否忽略测试代码
        /// </summary>
        [SerializeField]
        private bool m_IgnoreTestCode = false;

        /// <summary>
        /// 是否在保存时检查代码
        /// </summary>
        [SerializeField]
        private bool m_CheckOnSave = true;

        /// <summary>
        /// 是否在构建时检查代码
        /// </summary>
        [SerializeField]
        private bool m_CheckOnBuild = true;

        /// <summary>
        /// 是否在提交时检查代码
        /// </summary>
        [SerializeField]
        private bool m_CheckOnCommit = true;

        /// <summary>
        /// 是否在保存时自动修复代码
        /// </summary>
        [SerializeField]
        private bool m_FixOnSave = false;

        /// <summary>
        /// 忽略的文件或目录
        /// </summary>
        [SerializeField]
        private List<string> m_IgnoredPaths = new List<string>();

        /// <summary>
        /// 代码规则列表
        /// </summary>
        [SerializeField]
        private List<CodeRule> m_Rules = new List<CodeRule>();

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
            m_Rules.Add(new CodeRule("CS0001", "类名使用PascalCase", "类名应该使用PascalCase命名法", RuleSeverity.Error));
            m_Rules.Add(new CodeRule("CS0002", "方法名使用PascalCase", "方法名应该使用PascalCase命名法", RuleSeverity.Error));
            m_Rules.Add(new CodeRule("CS0003", "私有成员变量使用m_前缀", "私有成员变量应该使用m_前缀", RuleSeverity.Warning));
            m_Rules.Add(new CodeRule("CS0004", "常量使用c_前缀", "常量应该使用c_前缀", RuleSeverity.Warning));
            m_Rules.Add(new CodeRule("CS0005", "静态变量使用s_前缀", "静态变量应该使用s_前缀", RuleSeverity.Warning));
            m_Rules.Add(new CodeRule("CS0006", "参数使用_前缀", "参数应该使用_前缀", RuleSeverity.Warning));

            // 格式规则
            m_Rules.Add(new CodeRule("CS0101", "使用空格而不是制表符", "应该使用空格而不是制表符进行缩进", RuleSeverity.Warning));
            m_Rules.Add(new CodeRule("CS0102", "缩进使用4个空格", "缩进应该使用4个空格", RuleSeverity.Warning));
            m_Rules.Add(new CodeRule("CS0103", "大括号应该独占一行", "大括号应该独占一行", RuleSeverity.Info));

            // Unity特定规则
            m_Rules.Add(new CodeRule("UN0001", "避免使用GameObject.Find", "应该避免使用GameObject.Find，因为它性能较低", RuleSeverity.Warning));
            m_Rules.Add(new CodeRule("UN0002", "避免在Update中使用GetComponent", "应该避免在Update中使用GetComponent，因为它性能较低", RuleSeverity.Warning));
            m_Rules.Add(new CodeRule("UN0003", "避免使用SendMessage", "应该避免使用SendMessage，因为它性能较低", RuleSeverity.Warning));
        }

        /// <summary>
        /// 初始化默认忽略路径
        /// </summary>
        private void InitDefaultIgnoredPaths()
        {
            m_IgnoredPaths.Add("Assets/Plugins/");
            m_IgnoredPaths.Add("Assets/ThirdParty/");
            m_IgnoredPaths.Add("Assets/Generated/");
        }

        /// <summary>
        /// 是否忽略生成的代码
        /// </summary>
        public bool IgnoreGeneratedCode
        {
            get => m_IgnoreGeneratedCode;
            set => m_IgnoreGeneratedCode = value;
        }

        /// <summary>
        /// 是否忽略第三方代码
        /// </summary>
        public bool IgnoreThirdPartyCode
        {
            get => m_IgnoreThirdPartyCode;
            set => m_IgnoreThirdPartyCode = value;
        }

        /// <summary>
        /// 是否忽略测试代码
        /// </summary>
        public bool IgnoreTestCode
        {
            get => m_IgnoreTestCode;
            set => m_IgnoreTestCode = value;
        }

        /// <summary>
        /// 是否在保存时检查代码
        /// </summary>
        public bool CheckOnSave
        {
            get => m_CheckOnSave;
            set => m_CheckOnSave = value;
        }

        /// <summary>
        /// 是否在构建时检查代码
        /// </summary>
        public bool CheckOnBuild
        {
            get => m_CheckOnBuild;
            set => m_CheckOnBuild = value;
        }

        /// <summary>
        /// 是否在提交时检查代码
        /// </summary>
        public bool CheckOnCommit
        {
            get => m_CheckOnCommit;
            set => m_CheckOnCommit = value;
        }

        /// <summary>
        /// 是否在保存时自动修复代码
        /// </summary>
        public bool FixOnSave
        {
            get => m_FixOnSave;
            set => m_FixOnSave = value;
        }

        /// <summary>
        /// 忽略的文件或目录
        /// </summary>
        public List<string> IgnoredPaths
        {
            get => m_IgnoredPaths;
            set => m_IgnoredPaths = value;
        }

        /// <summary>
        /// 代码规则列表
        /// </summary>
        public List<CodeRule> Rules
        {
            get => m_Rules;
            set => m_Rules = value;
        }

        /// <summary>
        /// 获取规则
        /// </summary>
        /// <param name="_id">规则ID</param>
        /// <returns>规则</returns>
        public CodeRule GetRule(string _id)
        {
            return m_Rules.Find(rule => rule.Id == _id);
        }

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="_rule">规则</param>
        public void AddRule(CodeRule _rule)
        {
            var existingRule = GetRule(_rule.Id);
            if (existingRule != null)
            {
                m_Rules.Remove(existingRule);
            }

            m_Rules.Add(_rule);
        }

        /// <summary>
        /// 移除规则
        /// </summary>
        /// <param name="_id">规则ID</param>
        public void RemoveRule(string _id)
        {
            var rule = GetRule(_id);
            if (rule != null)
            {
                m_Rules.Remove(rule);
            }
        }
    }
}
