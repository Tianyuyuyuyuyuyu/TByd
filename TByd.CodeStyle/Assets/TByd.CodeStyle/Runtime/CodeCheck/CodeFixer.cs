using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        /// <param name="config">代码检查配置</param>
        /// <param name="checker">代码检查器</param>
        public CodeFixer(CodeCheckConfig config, CodeChecker checker)
        {
            m_Config = config;
            m_Checker = checker;

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
        /// <param name="ruleId">规则ID</param>
        /// <param name="handler">处理器</param>
        public void RegisterFixHandler(string ruleId, Func<string, CodeCheckIssue, string> handler)
        {
            m_FixHandlers[ruleId] = handler;
        }

        /// <summary>
        /// 修复代码
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>修复后的代码</returns>
        public string FixCode(string code, string filePath)
        {
            if (string.IsNullOrEmpty(code))
            {
                return code;
            }

            // 检查代码
            var result = m_Checker.CheckCode(code, filePath);

            if (result.IsValid)
            {
                return code;
            }

            var fixedCode = code;

            // 按规则ID分组
            var issuesByRule = result.Issues
                .GroupBy(i => i.RuleId)
                .OrderBy(g => g.Key);

            foreach (var ruleGroup in issuesByRule)
            {
                var ruleId = ruleGroup.Key;

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
        /// <param name="filePath">文件路径</param>
        /// <returns>是否修复成功</returns>
        public bool FixFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError($"[TByd.CodeStyle] 文件不存在: {filePath}");
                return false;
            }

            try
            {
                var code = File.ReadAllText(filePath);
                var fixedCode = FixCode(code, filePath);

                // 检查是否有修改
                if (code != fixedCode)
                {
                    File.WriteAllText(filePath, fixedCode);
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
        /// <param name="directoryPath">目录路径</param>
        /// <param name="recursive">是否递归修复子目录</param>
        /// <returns>修复的文件数量</returns>
        public int FixDirectory(string directoryPath, bool recursive = true)
        {
            if (!Directory.Exists(directoryPath))
            {
                Debug.LogError($"[TByd.CodeStyle] 目录不存在: {directoryPath}");
                return 0;
            }

            // 获取所有C#文件
            var files = Directory.GetFiles(
                directoryPath,
                "*.cs",
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            return files.Count(FixFile);
        }

        #region 命名规则修复处理器

        /// <summary>
        /// 修复私有字段命名
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixPrivateFieldNaming(string code, CodeCheckIssue issue)
        {
            // 提取代码片段
            var codeSnippet = issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return code;
            }

            // 提取字段名
            var match = Regex.Match(codeSnippet, @"private\s+(?!static\s+)(?<type>[\w<>[\],\s]+)\s+(?<name>\w+)\s*[;=]");
            if (!match.Success)
            {
                return code;
            }

            var fieldName = match.Groups["name"].Value;
            var pascalName = ToPascalCase(fieldName);
            var newFieldName = $"m_{pascalName}";

            // 替换字段名
            return ReplaceAtPosition(code, issue.LineNumber, issue.ColumnNumber, fieldName, newFieldName);
        }

        /// <summary>
        /// 修复静态字段命名
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixStaticFieldNaming(string code, CodeCheckIssue issue)
        {
            // 提取代码片段
            var codeSnippet = issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return code;
            }

            // 提取字段名
            var match = Regex.Match(codeSnippet, @"private\s+static\s+(?<type>[\w<>[\],\s]+)\s+(?<name>\w+)\s*[;=]");
            if (!match.Success)
            {
                return code;
            }

            var fieldName = match.Groups["name"].Value;
            var pascalName = ToPascalCase(fieldName);
            var newFieldName = $"s_{pascalName}";

            // 替换字段名
            return ReplaceAtPosition(code, issue.LineNumber, issue.ColumnNumber, fieldName, newFieldName);
        }

        /// <summary>
        /// 修复常量命名
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixConstantNaming(string code, CodeCheckIssue issue)
        {
            // 提取代码片段
            var codeSnippet = issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return code;
            }

            // 提取常量名
            var match = Regex.Match(codeSnippet, @"(?:private|public|protected|internal)\s+const\s+(?<type>[\w<>[\],\s]+)\s+(?<name>\w+)\s*[;=]");
            if (!match.Success)
            {
                return code;
            }

            var constantName = match.Groups["name"].Value;
            var pascalName = ToPascalCase(constantName);
            var newConstantName = $"c_{pascalName}";

            // 替换常量名
            return ReplaceAtPosition(code, issue.LineNumber, issue.ColumnNumber, constantName, newConstantName);
        }

        /// <summary>
        /// 修复属性命名
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixPropertyNaming(string code, CodeCheckIssue issue)
        {
            // 提取代码片段
            var codeSnippet = issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return code;
            }

            // 提取属性名
            var match = Regex.Match(codeSnippet, @"(?:public|protected|internal)\s+(?<type>[\w<>[\],\s]+)\s+(?<name>[a-z]\w*)\s*\{");
            if (!match.Success)
            {
                return code;
            }

            var propertyName = match.Groups["name"].Value;
            var pascalName = ToPascalCase(propertyName);

            // 替换属性名
            return ReplaceAtPosition(code, issue.LineNumber, issue.ColumnNumber, propertyName, pascalName);
        }

        /// <summary>
        /// 修复方法命名
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixMethodNaming(string code, CodeCheckIssue issue)
        {
            // 提取代码片段
            var codeSnippet = issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return code;
            }

            // 提取方法名
            var match = Regex.Match(codeSnippet, @"(?:public|protected|internal|private)\s+(?<returnType>[\w<>[\],\s]+)\s+(?<name>[a-z]\w*)\s*\(");
            if (!match.Success)
            {
                return code;
            }

            var methodName = match.Groups["name"].Value;
            var pascalName = ToPascalCase(methodName);

            // 替换方法名
            return ReplaceAtPosition(code, issue.LineNumber, issue.ColumnNumber, methodName, pascalName);
        }

        /// <summary>
        /// 修复参数命名
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixParameterNaming(string code, CodeCheckIssue issue)
        {
            // 提取代码片段
            var codeSnippet = issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return code;
            }

            // 提取参数名
            var match = Regex.Match(codeSnippet, @"\(\s*(?<type>[\w<>\[\],\s]+)\s+(?<n>\w+)(?:,|\))");
            if (!match.Success)
            {
                return code;
            }

            var paramName = match.Groups["n"].Value;
            var camelName = ToCamelCase(paramName);
            var newParamName = $"_{camelName}";

            // 替换参数名
            return ReplaceAtPosition(code, issue.LineNumber, issue.ColumnNumber, paramName, newParamName);
        }

        #endregion

        #region Unity规则修复处理器

        /// <summary>
        /// 修复空MonoBehaviour方法
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="issue">问题</param>
        /// <returns>修复后的代码</returns>
        private string FixEmptyMonoBehaviourMethods(string code, CodeCheckIssue issue)
        {
            // 提取代码片段
            var codeSnippet = issue.CodeSnippet;
            if (string.IsNullOrEmpty(codeSnippet))
            {
                return code;
            }

            // 删除整个方法
            return code.Replace(codeSnippet, string.Empty);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 转换为PascalCase
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>PascalCase名称</returns>
        private string ToPascalCase(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            // 如果已经是PascalCase，直接返回
            if (char.IsUpper(name[0]))
            {
                return name;
            }

            // 如果是camelCase，转换为PascalCase
            return char.ToUpper(name[0]) + name.Substring(1);
        }

        /// <summary>
        /// 转换为camelCase
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>camelCase名称</returns>
        private string ToCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            // 如果已经是camelCase，直接返回
            if (char.IsLower(name[0]))
            {
                return name;
            }

            // 如果是PascalCase，转换为camelCase
            return char.ToLower(name[0]) + name.Substring(1);
        }

        /// <summary>
        /// 在指定位置替换文本
        /// </summary>
        /// <param name="code">代码内容</param>
        /// <param name="lineNumber">行号</param>
        /// <param name="columnNumber">列号</param>
        /// <param name="oldText">旧文本</param>
        /// <param name="newText">新文本</param>
        /// <returns>替换后的代码</returns>
        private string ReplaceAtPosition(string code, int lineNumber, int columnNumber, string oldText, string newText)
        {
            // 分割为行
            var lines = code.Split(new[] { '\r', '\n' }, StringSplitOptions.None);

            // 检查行号是否有效
            if (lineNumber <= 0 || lineNumber > lines.Length)
            {
                return code;
            }

            // 获取行内容
            var line = lines[lineNumber - 1];

            // 检查列号是否有效
            if (columnNumber <= 0 || columnNumber > line.Length)
            {
                return code;
            }

            // 查找旧文本在行中的位置
            var startIndex = line.IndexOf(oldText, columnNumber - 1);
            if (startIndex < 0)
            {
                // 如果在指定列号后找不到，尝试在整行中查找
                startIndex = line.IndexOf(oldText);
                if (startIndex < 0)
                {
                    return code;
                }
            }

            // 替换文本
            var newLine = line.Substring(0, startIndex) + newText + line.Substring(startIndex + oldText.Length);
            lines[lineNumber - 1] = newLine;

            // 重新组合代码
            return string.Join(Environment.NewLine, lines);
        }

        #endregion
    }
}
