using System.Text.RegularExpressions;
using TByd.CodeStyle.Runtime.Config;

namespace TByd.CodeStyle.Runtime.CodeCheck.Rules
{
    /// <summary>
    /// 私有字段命名规则
    /// </summary>
    public class PrivateFieldNamingRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "CS0003";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "私有字段命名规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "私有字段应使用m_前缀加PascalCase命名";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Naming;

        /// <summary>
        /// 规则严重程度
        /// </summary>
        public new CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.Warning;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"private\s+(?!static|const)(?<type>[\w<>[\],\s]+)\s+(?!m_)(?<name>\w+)\s*([;=]|{)";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "私有成员变量 '{name}' 应使用m_前缀加PascalCase命名";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "将 '{name}' 重命名为 'm_{pascalName}'";

        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="_template">模板</param>
        /// <param name="_match">匹配结果</param>
        /// <returns>格式化后的消息</returns>
        protected override string FormatMessage(string _template, Match _match)
        {
            string name = _match.Groups["name"].Value;
            string pascalName = ToPascalCase(name);

            return _template.Replace("{name}", name)
                           .Replace("{pascalName}", pascalName);
        }

        /// <summary>
        /// 转换为PascalCase
        /// </summary>
        /// <param name="_name">名称</param>
        /// <returns>PascalCase名称</returns>
        private string ToPascalCase(string _name)
        {
            if (string.IsNullOrEmpty(_name))
            {
                return string.Empty;
            }

            // 如果已经是PascalCase，直接返回
            if (char.IsUpper(_name[0]))
            {
                return _name;
            }

            // 如果是camelCase，转换为PascalCase
            return char.ToUpper(_name[0]) + _name.Substring(1);
        }
    }

    /// <summary>
    /// 静态字段命名规则
    /// </summary>
    public class StaticFieldNamingRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "CS0005";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "静态字段命名规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "静态字段应使用s_前缀加PascalCase命名";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Naming;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"private\s+static\s+(?<type>[\w<>[\],\s]+)\s+(?!s_[A-Z])(?<name>\w+)\s*[;=]";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "静态字段 '{name}' 应使用s_前缀加PascalCase命名";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "将 '{name}' 重命名为 's_{PascalName}'";

        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="_template">模板</param>
        /// <param name="_match">匹配结果</param>
        /// <returns>格式化后的消息</returns>
        protected override string FormatMessage(string _template, Match _match)
        {
            string result = base.FormatMessage(_template, _match);

            if (_match != null && _match.Groups["name"].Success)
            {
                string name = _match.Groups["name"].Value;
                string pascalName = ToPascalCase(name);

                result = result.Replace("{PascalName}", pascalName);
            }

            return result;
        }

        /// <summary>
        /// 转换为PascalCase
        /// </summary>
        /// <param name="_name">名称</param>
        /// <returns>PascalCase名称</returns>
        private string ToPascalCase(string _name)
        {
            if (string.IsNullOrEmpty(_name))
            {
                return string.Empty;
            }

            // 如果已经是PascalCase，直接返回
            if (char.IsUpper(_name[0]))
            {
                return _name;
            }

            // 如果是camelCase，转换为PascalCase
            return char.ToUpper(_name[0]) + _name.Substring(1);
        }
    }

    /// <summary>
    /// 常量命名规则
    /// </summary>
    public class ConstantNamingRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "CS0004";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "常量命名规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "常量应使用c_前缀加PascalCase命名";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Naming;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"(?:private|public|protected|internal)\s+const\s+(?<type>[\w<>[\],\s]+)\s+(?!c_[A-Z])(?<name>\w+)\s*[;=]";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "常量 '{name}' 应使用c_前缀加PascalCase命名";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "将 '{name}' 重命名为 'c_{PascalName}'";

        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="_template">模板</param>
        /// <param name="_match">匹配结果</param>
        /// <returns>格式化后的消息</returns>
        protected override string FormatMessage(string _template, Match _match)
        {
            string result = base.FormatMessage(_template, _match);

            if (_match != null && _match.Groups["name"].Success)
            {
                string name = _match.Groups["name"].Value;
                string pascalName = ToPascalCase(name);

                result = result.Replace("{PascalName}", pascalName);
            }

            return result;
        }

        /// <summary>
        /// 转换为PascalCase
        /// </summary>
        /// <param name="_name">名称</param>
        /// <returns>PascalCase名称</returns>
        private string ToPascalCase(string _name)
        {
            if (string.IsNullOrEmpty(_name))
            {
                return string.Empty;
            }

            // 如果已经是PascalCase，直接返回
            if (char.IsUpper(_name[0]))
            {
                return _name;
            }

            // 如果是camelCase，转换为PascalCase
            return char.ToUpper(_name[0]) + _name.Substring(1);
        }
    }

    /// <summary>
    /// 属性命名规则
    /// </summary>
    public class PropertyNamingRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "CS0007";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "属性命名规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "属性应使用PascalCase命名";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Naming;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"(?:public|protected|internal)\s+(?<type>[\w<>[\],\s]+)\s+(?<name>[a-z]\w*)\s*\{";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "属性 '{name}' 应使用PascalCase命名";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "将 '{name}' 重命名为 '{PascalName}'";

        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="_template">模板</param>
        /// <param name="_match">匹配结果</param>
        /// <returns>格式化后的消息</returns>
        protected override string FormatMessage(string _template, Match _match)
        {
            string result = base.FormatMessage(_template, _match);

            if (_match != null && _match.Groups["name"].Success)
            {
                string name = _match.Groups["name"].Value;
                string pascalName = ToPascalCase(name);

                result = result.Replace("{PascalName}", pascalName);
            }

            return result;
        }

        /// <summary>
        /// 转换为PascalCase
        /// </summary>
        /// <param name="_name">名称</param>
        /// <returns>PascalCase名称</returns>
        private string ToPascalCase(string _name)
        {
            if (string.IsNullOrEmpty(_name))
            {
                return string.Empty;
            }

            // 如果已经是PascalCase，直接返回
            if (char.IsUpper(_name[0]))
            {
                return _name;
            }

            // 如果是camelCase，转换为PascalCase
            return char.ToUpper(_name[0]) + _name.Substring(1);
        }
    }

    /// <summary>
    /// 方法命名规则
    /// </summary>
    public class MethodNamingRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "CS0002";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "方法命名规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "方法应使用PascalCase命名";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Naming;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"(?:public|protected|internal|private)\s+(?<returnType>[\w<>[\],\s]+)\s+(?<name>[a-z]\w*)\s*\(";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "方法 '{name}' 应使用PascalCase命名";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "将 '{name}' 重命名为 '{PascalName}'";

        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="_template">模板</param>
        /// <param name="_match">匹配结果</param>
        /// <returns>格式化后的消息</returns>
        protected override string FormatMessage(string _template, Match _match)
        {
            string result = base.FormatMessage(_template, _match);

            if (_match != null && _match.Groups["name"].Success)
            {
                string name = _match.Groups["name"].Value;
                string pascalName = ToPascalCase(name);

                result = result.Replace("{PascalName}", pascalName);
            }

            return result;
        }

        /// <summary>
        /// 转换为PascalCase
        /// </summary>
        /// <param name="_name">名称</param>
        /// <returns>PascalCase名称</returns>
        private string ToPascalCase(string _name)
        {
            if (string.IsNullOrEmpty(_name))
            {
                return string.Empty;
            }

            // 如果已经是PascalCase，直接返回
            if (char.IsUpper(_name[0]))
            {
                return _name;
            }

            // 如果是camelCase，转换为PascalCase
            return char.ToUpper(_name[0]) + _name.Substring(1);
        }
    }

    /// <summary>
    /// 参数命名规则
    /// </summary>
    public class ParameterNamingRule : RegexCodeCheckRule
    {
        /// <summary>
        /// 规则ID
        /// </summary>
        public override string Id => "CS0006";

        /// <summary>
        /// 规则名称
        /// </summary>
        public override string Name => "参数命名规则";

        /// <summary>
        /// 规则描述
        /// </summary>
        public override string Description => "参数应使用_前缀加camelCase命名";

        /// <summary>
        /// 规则类别
        /// </summary>
        public override CodeCheckRuleCategory Category => CodeCheckRuleCategory.Naming;

        /// <summary>
        /// 规则严重程度
        /// </summary>
        public new CodeCheckRuleSeverity Severity { get; set; } = CodeCheckRuleSeverity.Warning;

        /// <summary>
        /// 正则表达式模式
        /// </summary>
        protected override string Pattern => @"\(\s*(?:(?<type>[\w<>[\],\s]+)\s+(?!_)(?<name>\w+)(?:,|\)))";

        /// <summary>
        /// 问题消息模板
        /// </summary>
        protected override string IssueMessageTemplate => "参数 '{name}' 应使用_前缀加camelCase命名";

        /// <summary>
        /// 修复建议模板
        /// </summary>
        protected override string FixSuggestionTemplate => "将 '{name}' 重命名为 '_{camelName}'";

        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="_template">模板</param>
        /// <param name="_match">匹配结果</param>
        /// <returns>格式化后的消息</returns>
        protected override string FormatMessage(string _template, Match _match)
        {
            string result = base.FormatMessage(_template, _match);

            if (_match != null && _match.Groups["name"].Success)
            {
                string name = _match.Groups["name"].Value;
                string camelName = ToCamelCase(name);

                result = result.Replace("{camelName}", camelName);
            }

            return result;
        }

        /// <summary>
        /// 转换为camelCase
        /// </summary>
        /// <param name="_name">名称</param>
        /// <returns>camelCase名称</returns>
        private string ToCamelCase(string _name)
        {
            if (string.IsNullOrEmpty(_name))
            {
                return string.Empty;
            }

            // 如果已经是camelCase，直接返回
            if (char.IsLower(_name[0]))
            {
                return _name;
            }

            // 如果是PascalCase，转换为camelCase
            return char.ToLower(_name[0]) + _name.Substring(1);
        }
    }
}
