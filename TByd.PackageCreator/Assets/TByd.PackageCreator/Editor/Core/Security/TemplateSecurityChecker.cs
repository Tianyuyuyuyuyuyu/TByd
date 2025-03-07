using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core.Security
{
    /// <summary>
    /// 模板安全检查器，负责验证模板操作的安全性
    /// </summary>
    public class TemplateSecurityChecker
    {
        private static TemplateSecurityChecker _sInstance;

        /// <summary>
        /// 单例实例
        /// </summary>
        public static TemplateSecurityChecker Instance
        {
            get
            {
                if (_sInstance == null)
                {
                    _sInstance = new TemplateSecurityChecker();
                }
                return _sInstance;
            }
        }

        private readonly ErrorHandler _mErrorHandler;

        // 受保护的关键目录列表（相对于Unity项目根目录）
        private readonly List<string> _mProtectedDirectories = new List<string>
        {
            "Assets/Editor",
            "Assets/Plugins",
            "Assets/Resources",
            "Library",
            "Logs",
            "obj",
            "Packages",
            "ProjectSettings",
            "Temp",
            "UserSettings",
            ".git"
        };

        // 允许的文件类型列表（扩展名）
        private readonly HashSet<string> _mAllowedFileExtensions = new HashSet<string>
        {
            ".cs", ".js", ".txt", ".md", ".json", ".asmdef", ".asmref", ".xml", ".yaml",
            ".yml", ".html", ".css", ".hlsl", ".shader", ".compute", ".cginc", ".meta",
            ".png", ".jpg", ".jpeg", ".gif", ".svg", ".asset", ".prefab", ".unity",
            ".mat", ".anim", ".controller", ".preset", ".spriteatlas", ".ttf", ".otf",
            ".wav", ".mp3", ".ogg"
        };

        // 潜在危险的文件扩展名列表
        private readonly HashSet<string> _mDangerousFileExtensions = new HashSet<string>
        {
            ".exe", ".dll", ".bat", ".cmd", ".sh", ".app", ".jar", ".msi", ".vbs",
            ".ps1", ".psd1", ".psm1", ".py", ".rb", ".php"
        };

        // 危险的模板内容正则表达式模式
        private readonly List<Regex> _mDangerousContentPatterns = new List<Regex>();

        /// <summary>
        /// 构造函数
        /// </summary>
        private TemplateSecurityChecker()
        {
            _mErrorHandler = ErrorHandler.Instance;
            InitializeDangerousPatterns();
        }

        /// <summary>
        /// 初始化危险内容检测模式
        /// </summary>
        private void InitializeDangerousPatterns()
        {
            // 这些模式检测潜在的危险代码模式
            _mDangerousContentPatterns.Add(new Regex(@"System\.Diagnostics\.Process\.Start", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"System\.IO\.File\.Delete", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"Directory\.Delete", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"DeleteAsset", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"AssetDatabase\.DeleteAsset", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"EditorApplication\.ExecuteMenuItem", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"Application\.Quit", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"Environment\.(Exit|FailFast)", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"System\.Net\.WebClient", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"System\.Net\.(Http|Web)", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"eval\s*\(", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"new\s+DynamicMethod", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"Assembly\.(Load|LoadFrom|LoadFile)", RegexOptions.Compiled));
            _mDangerousContentPatterns.Add(new Regex(@"System\.Reflection\.(Emit|Invoke)", RegexOptions.Compiled));
        }

        /// <summary>
        /// 验证目录路径的安全性
        /// </summary>
        /// <param name="directoryPath">要验证的目录路径</param>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateDirectoryPath(string directoryPath)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(directoryPath))
            {
                result.AddError("目录路径不能为空");
                return result;
            }

            // 归一化路径（统一斜杠方向，移除多余斜杠）
            var normalizedPath = NormalizePath(directoryPath);

            // 检查路径是否包含非法字符
            if (ContainsInvalidPathChars(normalizedPath))
            {
                result.AddError($"目录路径包含非法字符: {directoryPath}");
            }

            // 检查是否尝试访问受保护的目录
            if (IsProtectedDirectory(normalizedPath))
            {
                result.AddError($"安全限制：不允许在受保护目录中操作: {directoryPath}");
            }

            // 检查是否尝试访问项目根目录之外的位置
            if (IsOutsideProjectDirectory(normalizedPath))
            {
                result.AddError($"安全限制：不允许在项目目录外部操作: {directoryPath}");
            }

            // 检查路径深度是否过深（防止过深嵌套）
            if (IsPathTooDeep(normalizedPath, 10))
            {
                result.AddWarning($"目录路径嵌套过深，可能导致部分平台问题: {directoryPath}");
            }

            return result;
        }

        /// <summary>
        /// 验证文件路径的安全性
        /// </summary>
        /// <param name="filePath">要验证的文件路径</param>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateFilePath(string filePath)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(filePath))
            {
                result.AddError("文件路径不能为空");
                return result;
            }

            // 归一化路径
            var normalizedPath = NormalizePath(filePath);

            // 检查路径是否包含非法字符
            if (ContainsInvalidPathChars(normalizedPath))
            {
                result.AddError($"文件路径包含非法字符: {filePath}");
            }

            // 检查是否尝试访问受保护的目录
            if (IsProtectedDirectory(Path.GetDirectoryName(normalizedPath)))
            {
                result.AddError($"安全限制：不允许在受保护目录中操作: {filePath}");
            }

            // 检查是否尝试访问项目根目录之外的位置
            if (IsOutsideProjectDirectory(normalizedPath))
            {
                result.AddError($"安全限制：不允许在项目目录外部操作: {filePath}");
            }

            // 检查文件扩展名是否被允许
            var extension = Path.GetExtension(normalizedPath).ToLowerInvariant();

            if (_mDangerousFileExtensions.Contains(extension))
            {
                result.AddError($"安全限制：不允许创建潜在危险的文件类型: {extension}");
            }
            else if (!_mAllowedFileExtensions.Contains(extension))
            {
                result.AddWarning($"未在已知安全文件类型列表中的扩展名: {extension}");
            }

            return result;
        }

        /// <summary>
        /// 验证模板文件内容的安全性
        /// </summary>
        /// <param name="content">文件内容</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateFileContent(string content, string filePath)
        {
            var result = new ValidationResult();

            if (string.IsNullOrEmpty(content))
            {
                return result; // 空内容是安全的
            }

            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            // 只检查代码和脚本文件
            if (extension == ".cs" || extension == ".js" || extension == ".shader"
                || extension == ".compute" || extension == ".cginc" || extension == ".hlsl"
                || extension == ".bat" || extension == ".sh" || extension == ".cmd")
            {
                // 检查危险内容模式
                foreach (var pattern in _mDangerousContentPatterns)
                {
                    var matches = pattern.Matches(content);

                    if (matches.Count > 0)
                    {
                        // 获取匹配到的行号
                        var lineNumbers = new List<int>();
                        var lineNumber = 1;
                        var position = 0;

                        foreach (Match match in matches)
                        {
                            // 计算匹配内容所在的行号
                            while (position < match.Index)
                            {
                                if (content[position] == '\n')
                                {
                                    lineNumber++;
                                }
                                position++;
                            }

                            lineNumbers.Add(lineNumber);
                        }

                        var lineInfo = string.Join(", ", lineNumbers.Select(l => $"行{l}"));
                        result.AddWarning($"文件 {Path.GetFileName(filePath)} 包含潜在危险代码模式 ({pattern})，位于{lineInfo}");
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 验证模板目录结构的安全性
        /// </summary>
        /// <param name="template">要验证的模板</param>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateTemplate(IPackageTemplate template)
        {
            var result = new ValidationResult();

            // 验证模板的所有目录
            foreach (var directory in template.Directories)
            {
                var dirResult = ValidateTemplateDirectory(directory);
                result.Merge(dirResult);
            }

            // 验证模板的所有文件
            foreach (var file in template.Files)
            {
                var filePathResult = ValidateFilePath(file.RelativePath);
                result.Merge(filePathResult);

                var fileContentResult = ValidateFileContent(file.ContentTemplate, file.RelativePath);
                result.Merge(fileContentResult);
            }

            return result;
        }

        /// <summary>
        /// 验证模板目录的安全性
        /// </summary>
        /// <param name="directory">要验证的目录</param>
        /// <returns>验证结果</returns>
        private ValidationResult ValidateTemplateDirectory(TemplateDirectory directory)
        {
            var result = ValidateDirectoryPath(directory.RelativePath);

            // 递归验证子目录
            foreach (var subDirectory in directory.Subdirectories)
            {
                var subResult = ValidateTemplateDirectory(subDirectory);
                result.Merge(subResult);
            }

            return result;
        }

        /// <summary>
        /// 创建文件的临时备份
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>备份文件路径，失败则返回null</returns>
        public string CreateFileBackup(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return null;

                var backupFileName = $"{Path.GetFileName(filePath)}.{DateTime.Now:yyyyMMddHHmmss}.bak";
                var backupDir = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Temp", "PackageCreator", "Backups");

                if (!Directory.Exists(backupDir))
                    Directory.CreateDirectory(backupDir);

                var backupPath = Path.Combine(backupDir, backupFileName);
                File.Copy(filePath, backupPath, true);

                return backupPath;
            }
            catch (Exception ex)
            {
                _mErrorHandler.LogException(ErrorType.FileCopyError, ex, $"创建文件备份失败: {filePath}");
                return null;
            }
        }

        /// <summary>
        /// 从备份恢复文件
        /// </summary>
        /// <param name="backupPath">备份文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns>是否恢复成功</returns>
        public bool RestoreFileFromBackup(string backupPath, string targetPath)
        {
            try
            {
                if (!File.Exists(backupPath))
                    return false;

                File.Copy(backupPath, targetPath, true);
                return true;
            }
            catch (Exception ex)
            {
                _mErrorHandler.LogException(ErrorType.FileCopyError, ex, $"从备份恢复文件失败: {targetPath}");
                return false;
            }
        }

        /// <summary>
        /// 创建原子文件写入操作
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <returns>是否成功写入</returns>
        public bool AtomicWriteFile(string filePath, string content)
        {
            var tempFilePath = Path.Combine(
                Path.GetDirectoryName(filePath),
                $"{Path.GetFileNameWithoutExtension(filePath)}.temp{Path.GetExtension(filePath)}");

            string backupPath = null;

            try
            {
                // 如果文件已存在，创建备份
                if (File.Exists(filePath))
                {
                    backupPath = CreateFileBackup(filePath);
                }

                // 确保目录存在
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    try
                    {
                        Directory.CreateDirectory(directory);
                    }
                    catch (Exception ex)
                    {
                        _mErrorHandler.LogException(ErrorType.FileOperation, ex, $"创建目录失败: {directory}");
                        return false;
                    }
                }

                // 先写入临时文件
                File.WriteAllText(tempFilePath, content);

                // 检查临时文件是否成功创建
                if (!File.Exists(tempFilePath))
                {
                    throw new IOException($"创建临时文件失败: {tempFilePath}");
                }

                // 替换目标文件
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        // 清理临时文件
                        try { File.Delete(tempFilePath); } catch { }

                        _mErrorHandler.LogException(ErrorType.FileDeleteError, ex, $"删除目标文件失败: {filePath}");
                        return false;
                    }
                }

                try
                {
                    File.Move(tempFilePath, filePath);
                    return true;
                }
                catch (Exception ex)
                {
                    _mErrorHandler.LogException(ErrorType.FileWriteError, ex, $"移动临时文件到目标位置失败: {filePath}");

                    // 尝试恢复备份
                    if (backupPath != null && File.Exists(backupPath))
                    {
                        RestoreFileFromBackup(backupPath, filePath);
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                // 清理临时文件
                try { if (File.Exists(tempFilePath)) File.Delete(tempFilePath); } catch { }

                _mErrorHandler.LogException(ErrorType.FileWriteError, ex, $"写入文件失败: {filePath}");

                // 尝试恢复备份
                if (backupPath != null && File.Exists(backupPath))
                {
                    RestoreFileFromBackup(backupPath, filePath);
                }

                return false;
            }
        }

        #region 辅助方法

        /// <summary>
        /// 归一化路径
        /// </summary>
        private string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            // 替换反斜杠为正斜杠
            var normalized = path.Replace('\\', '/');

            // 移除多余的斜杠
            while (normalized.Contains("//"))
            {
                normalized = normalized.Replace("//", "/");
            }

            return normalized;
        }

        /// <summary>
        /// 检查路径是否包含非法字符
        /// </summary>
        private bool ContainsInvalidPathChars(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            var invalidFileNameChars = Path.GetInvalidFileNameChars();
            var invalidPathChars = Path.GetInvalidPathChars();

            // 额外检查一些特殊字符
            var additionalInvalidChars = new char[] { '<', '>', '|', '*', '?', '\"' };

            // 检查每个路径段
            var segments = path.Split('/');
            foreach (var segment in segments)
            {
                if (string.IsNullOrEmpty(segment))
                    continue;

                if (segment.Any(c => invalidFileNameChars.Contains(c) ||
                                   invalidPathChars.Contains(c) ||
                                   additionalInvalidChars.Contains(c)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 检查是否为受保护的目录
        /// </summary>
        private bool IsProtectedDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            var normalizedPath = NormalizePath(path);

            return _mProtectedDirectories.Any(protectedDir =>
            {
                var normalizedProtectedDir = NormalizePath(protectedDir);

                // 判断是否为保护目录自身或其子目录
                return normalizedPath.Equals(normalizedProtectedDir, StringComparison.OrdinalIgnoreCase) ||
                       normalizedPath.StartsWith(normalizedProtectedDir + "/", StringComparison.OrdinalIgnoreCase);
            });
        }

        /// <summary>
        /// 检查路径是否在项目目录外部
        /// </summary>
        private bool IsOutsideProjectDirectory(string path)
        {
            // 检查是否使用了 ../ 尝试访问上级目录
            if (path.Contains("../") || path.Contains("..\\"))
                return true;

            // 使用绝对路径检查
            try
            {
                var projectRoot = Path.GetDirectoryName(Application.dataPath);
                var absolutePath = Path.GetFullPath(path);
                var normalizedProjectRoot = NormalizePath(projectRoot);
                var normalizedAbsolutePath = NormalizePath(absolutePath);

                return !normalizedAbsolutePath.StartsWith(normalizedProjectRoot, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                // 如果路径无法被解析为有效的绝对路径，视为不安全
                return true;
            }
        }

        /// <summary>
        /// 检查路径深度是否过深
        /// </summary>
        private bool IsPathTooDeep(string path, int maxDepth)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            var segments = path.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            return segments.Length > maxDepth;
        }

        /// <summary>
        /// 公共方法，用于测试代码检查路径是否受保护
        /// </summary>
        /// <param name="path">要检查的路径</param>
        /// <returns>如果路径是受保护的则返回true</returns>
        public bool IsPathProtected(string path)
        {
            return IsProtectedDirectory(path);
        }

        #endregion
    }
}
