using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Utils.FileSystem;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core.Services
{
    /// <summary>
    /// C#文件生成策略，专门用于生成C#代码文件
    /// </summary>
    public class CSharpFileGenerationStrategy : IFileGenerationStrategy
    {
        /// <summary>
        /// 策略名称
        /// </summary>
        public string StrategyName => "C#文件生成策略";

        /// <summary>
        /// 支持的文件扩展名，C#相关文件
        /// </summary>
        public string[] SupportedFileExtensions => new[] { ".cs" };

        // 变量替换处理器（委托给FileGenerator处理）
        private readonly FileGenerator _mVariableProcessor;

        // 命名空间占位符正则表达式
        private static readonly Regex SNamespacePattern = new Regex(@"namespace\s+\$\{namespace\}", RegexOptions.Compiled);

        /// <summary>
        /// 创建C#文件生成策略
        /// </summary>
        public CSharpFileGenerationStrategy()
        {
            _mVariableProcessor = new FileGenerator();
        }

        /// <summary>
        /// 检查此策略是否支持指定的文件类型
        /// </summary>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns>是否支持</returns>
        public bool SupportsFileType(string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                return false;

            // 转为小写进行比较
            var lowerExtension = fileExtension.ToLowerInvariant();
            return SupportedFileExtensions.Contains(lowerExtension);
        }

        /// <summary>
        /// 生成文件内容
        /// </summary>
        /// <param name="templateFile">模板文件定义</param>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns>生成结果</returns>
        public async Task<ValidationResult> GenerateFileAsync(TemplateFile templateFile, PackageConfig config, string targetPath)
        {
            var result = new ValidationResult();

            try
            {
                // 获取文件内容
                var fileContent = templateFile.ContentTemplate;

                // 如果文件支持变量替换，则进行替换
                if (templateFile.SupportsVariableReplacement)
                {
                    // 特殊处理命名空间
                    var namespaceName = GetNamespaceFromConfig(config, targetPath);
                    fileContent = SNamespacePattern.Replace(fileContent, $"namespace {namespaceName}");

                    // 处理其他变量
                    fileContent = _mVariableProcessor.ReplaceVariables(fileContent, config);
                }

                // 确保目标目录存在
                var directoryPath = Path.GetDirectoryName(targetPath);
                if (!string.IsNullOrEmpty(directoryPath))
                {
                    FileUtils.EnsureDirectoryExists(directoryPath);
                }

                // 安全写入文件
                var writeSuccess = await WriteFileAsync(targetPath, fileContent);

                if (writeSuccess)
                {
                    result.AddInfo($"成功创建C#文件: {Path.GetFileName(targetPath)}");
                }
                else
                {
                    result.AddError($"创建C#文件失败: {Path.GetFileName(targetPath)}");
                }
            }
            catch (Exception ex)
            {
                result.AddError($"生成C#文件时发生异常: {ex.Message}");
                Debug.LogException(ex);
            }

            return result;
        }

        /// <summary>
        /// 从配置和文件路径推断命名空间
        /// </summary>
        /// <param name="config">包配置</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>命名空间</returns>
        private string GetNamespaceFromConfig(PackageConfig config, string filePath)
        {
            // 优先使用配置的根命名空间
            var baseNamespace = !string.IsNullOrEmpty(config.RootNamespace)
                ? config.RootNamespace
                : GenerateNamespaceFromPackageName(config.Name);

            // 根据文件位置添加子命名空间
            var subNamespace = GetSubNamespaceFromPath(filePath);

            // 组合命名空间
            if (!string.IsNullOrEmpty(subNamespace))
            {
                return $"{baseNamespace}.{subNamespace}";
            }

            return baseNamespace;
        }

        /// <summary>
        /// 从文件路径生成子命名空间
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>子命名空间</returns>
        private string GetSubNamespaceFromPath(string filePath)
        {
            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (string.IsNullOrEmpty(directory))
                    return string.Empty;

                // 获取相对于包根目录的路径
                var parts = directory.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

                // 查找关键目录标识
                var runtimeIndex = Array.IndexOf(parts, "Runtime");
                var editorIndex = Array.IndexOf(parts, "Editor");
                var scriptsIndex = Array.IndexOf(parts, "Scripts");

                // 选择最靠前的索引
                var startIndex = -1;
                if (runtimeIndex >= 0) startIndex = runtimeIndex;
                if (editorIndex >= 0 && (startIndex < 0 || editorIndex < startIndex)) startIndex = editorIndex;
                if (scriptsIndex >= 0 && (startIndex < 0 || scriptsIndex < startIndex)) startIndex = scriptsIndex;

                // 如果找到了关键目录
                if (startIndex >= 0 && startIndex + 1 < parts.Length)
                {
                    // 连接子目录作为子命名空间
                    var subNamespace = string.Join(".", parts.Skip(startIndex + 1)
                        .Select(p => MakeSafeIdentifier(p)));

                    return subNamespace;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"无法从路径推导子命名空间: {ex.Message}");
            }

            return string.Empty;
        }

        /// <summary>
        /// 从包名生成命名空间
        /// </summary>
        /// <param name="packageName">包名</param>
        /// <returns>生成的命名空间</returns>
        private string GenerateNamespaceFromPackageName(string packageName)
        {
            if (string.IsNullOrEmpty(packageName))
                return string.Empty;

            // 按点分隔
            var parts = packageName.Split('.');

            // 将每个部分转换为有效的标识符
            for (var i = 0; i < parts.Length; i++)
            {
                parts[i] = MakeSafeIdentifier(parts[i]);
            }

            // 重新组合
            return string.Join(".", parts);
        }

        /// <summary>
        /// 将字符串转换为安全的C#标识符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>安全的标识符</returns>
        private string MakeSafeIdentifier(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "_";

            // 替换无效字符为下划线
            var invalidChars = new Regex(@"[^\p{L}\p{Nl}\p{Mn}\p{Mc}\p{Nd}\p{Pc}\p{Cf}]");
            var safeName = invalidChars.Replace(input, "_");

            // 如果第一个字符不是字母或下划线，则添加前缀
            if (!char.IsLetter(safeName[0]) && safeName[0] != '_')
            {
                safeName = "_" + safeName;
            }

            // 验证是否为C#关键字，如果是则添加@前缀
            if (IsCSharpKeyword(safeName))
            {
                safeName = "@" + safeName;
            }

            return safeName;
        }

        /// <summary>
        /// 检查是否为C#关键字
        /// </summary>
        /// <param name="identifier">标识符</param>
        /// <returns>是否为关键字</returns>
        private bool IsCSharpKeyword(string identifier)
        {
            // C#关键字列表
            var keywords = new[]
            {
                "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
                "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else",
                "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for",
                "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock",
                "long", "namespace", "new", "null", "object", "operator", "out", "override", "params",
                "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed",
                "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw",
                "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using",
                "virtual", "void", "volatile", "while"
            };

            return Array.IndexOf(keywords, identifier) >= 0;
        }

        /// <summary>
        /// 异步写入文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <returns>是否成功</returns>
        private async Task<bool> WriteFileAsync(string filePath, string content)
        {
            try
            {
                // 首先创建备份（如果文件已存在）
                if (File.Exists(filePath))
                {
                    SecureFileOperations.CreateBackup(filePath);
                }

                // 使用异步文件操作写入文件
                await FileUtils.WriteTextFileAsync(filePath, content);

                // 检查文件是否创建成功
                if (File.Exists(filePath))
                {
                    return true;
                }

                Debug.LogError($"C#文件写入失败（文件不存在）: {filePath}");
                return false;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return false;
            }
        }
    }
}
