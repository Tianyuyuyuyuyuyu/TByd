using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TByd.CodeStyle.Runtime.Config;
using UnityEngine;

namespace TByd.CodeStyle.Runtime.CodeCheck
{
    /// <summary>
    /// 代码修复器，用于修复代码中的问题
    /// </summary>
    public class CodeFixer
    {
        // 代码检查配置
        private readonly CodeCheckConfig m_Config;

        // 代码检查器
        private readonly CodeChecker m_Checker;

        // 修复处理器字典
        private readonly Dictionary<string, Func<string, CodeCheckIssue, string>> m_FixHandlers =
            new Dictionary<string, Func<string, CodeCheckIssue, string>>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_config">代码检查配置</param>
        /// <param name="_checker">代码检查器</param>
        public CodeFixer(CodeCheckConfig _config, CodeChecker _checker)
        {
            m_Config = _config;
            m_Checker = _checker;

            // 注册默认修复处理器
            RegisterDefaultFixHandlers();
        }

        /// <summary>
        /// 注册默认修复处理器
        /// </summary>
        private void RegisterDefaultFixHandlers()
        {
            // 命名规则修复处理器
            RegisterFixHandler("naming-private-field", FixPrivateFieldNaming);
            RegisterFixHandler("naming-static-field", FixStaticFieldNaming);
            RegisterFixHandler("naming-constant", FixConstantNaming);
            RegisterFixHandler("naming-property", FixPropertyNaming);
            RegisterFixHandler("naming-method", FixMethodNaming);
            RegisterFixHandler("naming-parameter", FixParameterNaming);

            // Unity规则修复处理器
            RegisterFixHandler("unity-avoid-empty-monobehaviour-methods", FixEmptyMonoBehaviourMethods);
        }

        /// <summary>
        /// 注册修复处理器
        /// </summary>
        /// <param name="_ruleId">规则ID</param>
        /// <param name="_handler">处理器</param>
        public void RegisterFixHandler(string _ruleId, Func<string, CodeCheckIssue, string> _handler)
        {
            m_FixHandlers[_ruleId] = _handler;
        }

        /// <summary>
        /// 修复代码
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_filePath">文件路径</param>
        /// <returns>修复后的代码</returns>
        public string FixCode(string _code, string _filePath)
        {
            if (string.IsNullOrEmpty(_code))
            {
                return _code;
            }

            // 检查代码
            CodeCheckResult result = m_Checker.CheckCode(_code, _filePath);

            if (result.IsValid)
            {
                return _code;
            }

            string fixedCode = _code;

            // 按规则ID分组
            var issuesByRule = result.Issues
                .GroupBy(i => i.RuleId)
                .OrderBy(g => g.Key);

            foreach (var ruleGroup in issuesByRule)
            {
                string ruleId = ruleGroup.Key;

                // 检查是否有修复处理器
                if (m_FixHandlers.TryGetValue(ruleId, out var handler))
                {
                    // 按行号倒序排序，避免修复时影响后续行号
                    var sortedIssues = ruleGroup.OrderByDescending(i => i.LineNumber).ThenByDescending(i => i.ColumnNumber);

                    foreach (var issue in sortedIssues)
                    {
                        try
                        {
                            fixedCode = handler(fixedCode, issue);
                        }
                        catch (Exception e)
                        {
                            Debug.LogError($"[TByd.CodeStyle] 修复规则 '{ruleId}' 异常: {e.Message}");
                        }
                    }
                }
            }

            return fixedCode;
        }

        /// <summary>
        /// 修复文件
        /// </summary>
        /// <param name="_filePath">文件路径</param>
        /// <returns>是否修复成功</returns>
        public bool FixFile(string _filePath)
        {
            if (!File.Exists(_filePath))
            {
                Debug.LogError($"[TByd.CodeStyle] 文件不存在: {_filePath}");
                return false;
            }

            try
            {
                string code = File.ReadAllText(_filePath);
                string fixedCode = FixCode(code, _filePath);

                // 检查是否有修改
                if (code != fixedCode)
                {
                    File.WriteAllText(_filePath, fixedCode);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 修复文件失败: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// 修复目录
        /// </summary>
        /// <param name="_directoryPath">目录路径</param>
        /// <param name="_recursive">是否递归修复子目录</param>
        /// <returns>修复的文件数量</returns>
        public int FixDirectory(string _directoryPath, bool _recursive = true)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Debug.LogError($"[TByd.CodeStyle] 目录不存在: {_directoryPath}");
                return 0;
            }

            int fixedCount = 0;

            // 获取所有C#文件
            string[] files = Directory.GetFiles(
                _directoryPath,
                "*.cs",
                _recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            foreach (string filePath in files)
            {
                if (FixFile(filePath))
                {
                    fixedCount++;
                }
            }

            return fixedCount;
        }

        #region 命名规则修复处理器

        /// <summary>
        /// 修复私有字段命名
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixPrivateFieldNaming(string _code, CodeCheckIssue _issue)
        {
            // 提取代码片段
            string codeSnippet = _issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return _code;
            }

            // 提取字段名
            Match match = Regex.Match(codeSnippet, @"private\s+(?!static\s+)(?<type>[\w<>[\],\s]+)\s+(?<name>\w+)\s*[;=]");
            if (!match.Success)
            {
                return _code;
            }

            string fieldName = match.Groups["name"].Value;
            string pascalName = ToPascalCase(fieldName);
            string newFieldName = $"m_{pascalName}";

            // 替换字段名
            return ReplaceAtPosition(_code, _issue.LineNumber, _issue.ColumnNumber, fieldName, newFieldName);
        }

        /// <summary>
        /// 修复静态字段命名
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixStaticFieldNaming(string _code, CodeCheckIssue _issue)
        {
            // 提取代码片段
            string codeSnippet = _issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return _code;
            }

            // 提取字段名
            Match match = Regex.Match(codeSnippet, @"private\s+static\s+(?<type>[\w<>[\],\s]+)\s+(?<name>\w+)\s*[;=]");
            if (!match.Success)
            {
                return _code;
            }

            string fieldName = match.Groups["name"].Value;
            string pascalName = ToPascalCase(fieldName);
            string newFieldName = $"s_{pascalName}";

            // 替换字段名
            return ReplaceAtPosition(_code, _issue.LineNumber, _issue.ColumnNumber, fieldName, newFieldName);
        }

        /// <summary>
        /// 修复常量命名
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixConstantNaming(string _code, CodeCheckIssue _issue)
        {
            // 提取代码片段
            string codeSnippet = _issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return _code;
            }

            // 提取常量名
            Match match = Regex.Match(codeSnippet, @"(?:private|public|protected|internal)\s+const\s+(?<type>[\w<>[\],\s]+)\s+(?<name>\w+)\s*[;=]");
            if (!match.Success)
            {
                return _code;
            }

            string constantName = match.Groups["name"].Value;
            string pascalName = ToPascalCase(constantName);
            string newConstantName = $"c_{pascalName}";

            // 替换常量名
            return ReplaceAtPosition(_code, _issue.LineNumber, _issue.ColumnNumber, constantName, newConstantName);
        }

        /// <summary>
        /// 修复属性命名
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixPropertyNaming(string _code, CodeCheckIssue _issue)
        {
            // 提取代码片段
            string codeSnippet = _issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return _code;
            }

            // 提取属性名
            Match match = Regex.Match(codeSnippet, @"(?:public|protected|internal)\s+(?<type>[\w<>[\],\s]+)\s+(?<name>[a-z]\w*)\s*\{");
            if (!match.Success)
            {
                return _code;
            }

            string propertyName = match.Groups["name"].Value;
            string pascalName = ToPascalCase(propertyName);

            // 替换属性名
            return ReplaceAtPosition(_code, _issue.LineNumber, _issue.ColumnNumber, propertyName, pascalName);
        }

        /// <summary>
        /// 修复方法命名
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixMethodNaming(string _code, CodeCheckIssue _issue)
        {
            // 提取代码片段
            string codeSnippet = _issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return _code;
            }

            // 提取方法名
            Match match = Regex.Match(codeSnippet, @"(?:public|protected|internal|private)\s+(?<returnType>[\w<>[\],\s]+)\s+(?<name>[a-z]\w*)\s*\(");
            if (!match.Success)
            {
                return _code;
            }

            string methodName = match.Groups["name"].Value;
            string pascalName = ToPascalCase(methodName);

            // 替换方法名
            return ReplaceAtPosition(_code, _issue.LineNumber, _issue.ColumnNumber, methodName, pascalName);
        }

        /// <summary>
        /// 修复参数命名
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixParameterNaming(string _code, CodeCheckIssue _issue)
        {
            // 提取代码片段
            string codeSnippet = _issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return _code;
            }

            // 提取参数名
            Match match = Regex.Match(codeSnippet, @"\(\s*(?<type>[\w<>\[\],\s]+)\s+(?<n>\w+)(?:,|\))");
            if (!match.Success)
            {
                return _code;
            }

            string paramName = match.Groups["n"].Value;
            string camelName = ToCamelCase(paramName);
            string newParamName = $"_{camelName}";

            // 替换参数名
            return ReplaceAtPosition(_code, _issue.LineNumber, _issue.ColumnNumber, paramName, newParamName);
        }

        #endregion

        #region Unity规则修复处理器

        /// <summary>
        /// 修复空MonoBehaviour方法
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixEmptyMonoBehaviourMethods(string _code, CodeCheckIssue _issue)
        {
            // 提取代码片段
            string codeSnippet = _issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return _code;
            }

            // 删除整个方法
            return _code.Replace(codeSnippet, string.Empty);
        }

        #endregion

        #region 辅助方法

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

        /// <summary>
        /// 在指定位置替换文本
        /// </summary>
        /// <param name="_code">代码内容</param>
        /// <param name="_lineNumber">行号</param>
        /// <param name="_columnNumber">列号</param>
        /// <param name="_oldText">旧文本</param>
        /// <param name="_newText">新文本</param>
        /// <returns>替换后的代码</returns>
        private string ReplaceAtPosition(string _code, int _lineNumber, int _columnNumber, string _oldText, string _newText)
        {
            // 分割为行
            string[] lines = _code.Split(new[] { '\r', '\n' }, StringSplitOptions.None);

            // 检查行号是否有效
            if (_lineNumber <= 0 || _lineNumber > lines.Length)
            {
                return _code;
            }

            // 获取行内容
            string line = lines[_lineNumber - 1];

            // 检查列号是否有效
            if (_columnNumber <= 0 || _columnNumber > line.Length)
            {
                return _code;
            }

            // 查找旧文本在行中的位置
            int startIndex = line.IndexOf(_oldText, _columnNumber - 1);
            if (startIndex < 0)
            {
                // 如果在指定列号后找不到，尝试在整行中查找
                startIndex = line.IndexOf(_oldText);
                if (startIndex < 0)
                {
                    return _code;
                }
            }

            // 替换文本
            string newLine = line.Substring(0, startIndex) + _newText + line.Substring(startIndex + _oldText.Length);
            lines[_lineNumber - 1] = newLine;

            // 重新组合代码
            return string.Join(Environment.NewLine, lines);
        }

        #endregion
    }
}
