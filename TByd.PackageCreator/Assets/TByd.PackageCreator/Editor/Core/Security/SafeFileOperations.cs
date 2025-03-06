using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Utils;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core.Security
{
    /// <summary>
    /// 安全文件操作类，为文件操作提供安全检查和事务记录
    /// </summary>
    public class SafeFileOperations
    {
        private static SafeFileOperations _instance;

        /// <summary>
        /// 单例实例
        /// </summary>
        public static SafeFileOperations Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SafeFileOperations();
                }
                return _instance;
            }
        }

        private readonly ErrorHandler _errorHandler;
        private readonly TemplateSecurityChecker _securityChecker;
        private readonly SecurityTransactionLogger _transactionLogger;

        // 当前活动事务ID
        private string _currentTransactionId = null;

        /// <summary>
        /// 是否有活动的事务
        /// </summary>
        public bool HasActiveTransaction => !string.IsNullOrEmpty(_currentTransactionId);

        /// <summary>
        /// 构造函数
        /// </summary>
        private SafeFileOperations()
        {
            _errorHandler = ErrorHandler.Instance;
            _securityChecker = TemplateSecurityChecker.Instance;
            _transactionLogger = SecurityTransactionLogger.Instance;
        }

        /// <summary>
        /// 开始文件操作事务
        /// </summary>
        /// <param name="description">事务描述</param>
        /// <returns>是否成功开始事务</returns>
        public bool BeginTransaction(string description)
        {
            if (HasActiveTransaction)
            {
                _errorHandler.LogError(ErrorType.Security, "尝试开始新事务时存在活动事务");
                return false;
            }

            _currentTransactionId = _transactionLogger.BeginTransaction(description);
            return true;
        }

        /// <summary>
        /// 提交当前事务
        /// </summary>
        /// <returns>是否成功提交</returns>
        public bool CommitTransaction()
        {
            if (!HasActiveTransaction)
            {
                _errorHandler.LogError(ErrorType.Security, "尝试提交不存在的事务");
                return false;
            }

            bool result = _transactionLogger.CommitTransaction(_currentTransactionId);
            _currentTransactionId = null;
            return result;
        }

        /// <summary>
        /// 回滚当前事务
        /// </summary>
        /// <returns>是否成功回滚</returns>
        public bool RollbackTransaction()
        {
            if (!HasActiveTransaction)
            {
                _errorHandler.LogError(ErrorType.Security, "尝试回滚不存在的事务");
                return false;
            }

            bool result = _transactionLogger.RollbackTransaction(_currentTransactionId);
            _currentTransactionId = null;
            return result;
        }

        /// <summary>
        /// 安全验证模板
        /// </summary>
        /// <param name="template">要验证的模板</param>
        /// <returns>验证结果</returns>
        public ValidationResult ValidateTemplate(IPackageTemplate template)
        {
            if (template == null)
            {
                var result = new ValidationResult();
                result.AddError("模板不能为空");
                return result;
            }

            return _securityChecker.ValidateTemplate(template);
        }

        /// <summary>
        /// 安全创建目录
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <returns>验证结果</returns>
        public ValidationResult CreateDirectory(string directoryPath)
        {
            var result = _securityChecker.ValidateDirectoryPath(directoryPath);
            if (!result.IsValid)
            {
                return result;
            }

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    // 记录目录创建操作（如果在事务中）
                    if (HasActiveTransaction)
                    {
                        _transactionLogger.LogDirectoryCreation(_currentTransactionId, directoryPath);
                    }

                    Directory.CreateDirectory(directoryPath);
                    result.AddInfo($"成功创建目录: {directoryPath}");
                }
                else
                {
                    result.AddInfo($"目录已存在: {directoryPath}");
                }
            }
            catch (Exception ex)
            {
                result.AddError($"创建目录时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.FileOperation, ex, $"创建目录失败: {directoryPath}");
            }

            return result;
        }

        /// <summary>
        /// 安全写入文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <returns>验证结果</returns>
        public ValidationResult WriteFile(string filePath, string content)
        {
            // 验证文件路径
            var result = _securityChecker.ValidateFilePath(filePath);
            if (!result.IsValid)
            {
                return result;
            }

            // 验证文件内容
            var contentResult = _securityChecker.ValidateFileContent(content, filePath);
            result.Merge(contentResult);

            if (result.HasErrors)
            {
                return result;
            }

            try
            {
                // 确保目录存在
                string directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    var dirResult = CreateDirectory(directory);
                    result.Merge(dirResult);

                    if (result.HasErrors)
                    {
                        return result;
                    }
                }

                // 记录文件操作（如果在事务中）
                if (HasActiveTransaction)
                {
                    if (File.Exists(filePath))
                    {
                        _transactionLogger.LogFileModification(_currentTransactionId, filePath);
                    }
                    else
                    {
                        _transactionLogger.LogFileCreation(_currentTransactionId, filePath);
                    }
                }

                // 原子写入文件
                bool writeSuccess = true;

                try
                {
                    File.WriteAllText(filePath, content);
                }
                catch (Exception ex)
                {
                    writeSuccess = false;
                    throw ex; // 重新抛出异常以便被上层 catch 捕获
                }

                if (writeSuccess)
                {
                    result.AddInfo($"成功写入文件: {filePath}");
                }
                else
                {
                    result.AddError($"写入文件失败: {filePath}");
                }
            }
            catch (Exception ex)
            {
                result.AddError($"写入文件时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.FileWriteError, ex, $"写入文件失败: {filePath}");
            }

            return result;
        }

        /// <summary>
        /// 异步安全写入文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        /// <returns>验证结果</returns>
        public async Task<ValidationResult> WriteFileAsync(string filePath, string content)
        {
            // 验证文件路径
            var result = _securityChecker.ValidateFilePath(filePath);
            if (!result.IsValid)
            {
                return result;
            }

            // 验证文件内容
            var contentResult = _securityChecker.ValidateFileContent(content, filePath);
            result.Merge(contentResult);

            if (result.HasErrors)
            {
                return result;
            }

            try
            {
                // 确保目录存在
                string directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    var dirResult = CreateDirectory(directory);
                    result.Merge(dirResult);

                    if (result.HasErrors)
                    {
                        return result;
                    }
                }

                // 记录文件操作（如果在事务中）
                if (HasActiveTransaction)
                {
                    if (File.Exists(filePath))
                    {
                        _transactionLogger.LogFileModification(_currentTransactionId, filePath);
                    }
                    else
                    {
                        _transactionLogger.LogFileCreation(_currentTransactionId, filePath);
                    }
                }

                // 异步写入文件
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    await writer.WriteAsync(content);
                }

                result.AddInfo($"成功写入文件: {filePath}");
            }
            catch (Exception ex)
            {
                result.AddError($"写入文件时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.FileWriteError, ex, $"写入文件失败: {filePath}");
            }

            return result;
        }

        /// <summary>
        /// 安全删除文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>验证结果</returns>
        public ValidationResult DeleteFile(string filePath)
        {
            var result = _securityChecker.ValidateFilePath(filePath);
            if (!result.IsValid)
            {
                return result;
            }

            try
            {
                if (!File.Exists(filePath))
                {
                    result.AddWarning($"文件不存在，无需删除: {filePath}");
                    return result;
                }

                // 记录文件删除操作（如果在事务中）
                if (HasActiveTransaction)
                {
                    _transactionLogger.LogFileDeletion(_currentTransactionId, filePath);
                }

                File.Delete(filePath);
                result.AddInfo($"成功删除文件: {filePath}");
            }
            catch (Exception ex)
            {
                result.AddError($"删除文件时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.FileDeleteError, ex, $"删除文件失败: {filePath}");
            }

            return result;
        }

        /// <summary>
        /// 安全复制文件
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <param name="overwrite">是否覆盖已存在的文件</param>
        /// <returns>验证结果</returns>
        public ValidationResult CopyFile(string sourcePath, string targetPath, bool overwrite = true)
        {
            var result = new ValidationResult();

            // 验证源文件路径
            var sourceResult = _securityChecker.ValidateFilePath(sourcePath);
            // 验证目标文件路径
            var targetResult = _securityChecker.ValidateFilePath(targetPath);

            // 首先检查源文件是否存在
            if (!File.Exists(sourcePath))
            {
                result.AddError($"源文件不存在: {sourcePath}");
                _errorHandler.LogError(ErrorType.FileNotFound, $"源文件不存在: {sourcePath}");
            }

            // 合并其他验证结果
            result.Merge(sourceResult);
            result.Merge(targetResult);

            if (result.HasErrors)
            {
                return result;
            }

            try
            {
                // 确保目标目录存在
                string targetDirectory = Path.GetDirectoryName(targetPath);
                if (!string.IsNullOrEmpty(targetDirectory) && !Directory.Exists(targetDirectory))
                {
                    var dirResult = CreateDirectory(targetDirectory);
                    result.Merge(dirResult);

                    if (result.HasErrors)
                    {
                        return result;
                    }
                }

                // 记录文件操作（如果在事务中）
                if (HasActiveTransaction)
                {
                    if (File.Exists(targetPath))
                    {
                        _transactionLogger.LogFileModification(_currentTransactionId, targetPath);
                    }
                    else
                    {
                        _transactionLogger.LogFileCreation(_currentTransactionId, targetPath);
                    }
                }

                // 直接复制文件
                File.Copy(sourcePath, targetPath, overwrite);
                result.AddInfo($"成功复制文件从 {sourcePath} 到 {targetPath}");
            }
            catch (Exception ex)
            {
                result.AddError($"复制文件时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.FileCopyError, ex, $"复制文件失败: {sourcePath} -> {targetPath}");
            }

            return result;
        }

        /// <summary>
        /// 安全移动文件
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <param name="overwrite">是否覆盖已存在的文件</param>
        /// <returns>验证结果</returns>
        public ValidationResult MoveFile(string sourcePath, string targetPath, bool overwrite = true)
        {
            // 验证源文件路径
            var sourceResult = _securityChecker.ValidateFilePath(sourcePath);
            if (!sourceResult.IsValid)
            {
                return sourceResult;
            }

            // 验证目标文件路径
            var targetResult = _securityChecker.ValidateFilePath(targetPath);
            if (!targetResult.IsValid)
            {
                return targetResult;
            }

            var result = new ValidationResult();
            result.Merge(sourceResult);
            result.Merge(targetResult);

            if (result.HasErrors)
            {
                return result;
            }

            try
            {
                if (!File.Exists(sourcePath))
                {
                    result.AddError($"源文件不存在: {sourcePath}");
                    _errorHandler.LogError(ErrorType.FileNotFound, $"源文件不存在: {sourcePath}");
                    return result;
                }

                // 确保目标目录存在
                string targetDirectory = Path.GetDirectoryName(targetPath);
                if (!string.IsNullOrEmpty(targetDirectory) && !Directory.Exists(targetDirectory))
                {
                    var dirResult = CreateDirectory(targetDirectory);
                    result.Merge(dirResult);

                    if (result.HasErrors)
                    {
                        return result;
                    }
                }

                // 记录文件操作（如果在事务中）
                if (HasActiveTransaction)
                {
                    if (File.Exists(targetPath))
                    {
                        _transactionLogger.LogFileModification(_currentTransactionId, targetPath);
                    }
                    else
                    {
                        _transactionLogger.LogFileCreation(_currentTransactionId, targetPath);
                    }

                    _transactionLogger.LogFileDeletion(_currentTransactionId, sourcePath);
                }

                // 如果目标文件存在且不允许覆盖，则返回错误
                if (File.Exists(targetPath) && !overwrite)
                {
                    result.AddError($"目标文件已存在: {targetPath}");
                    return result;
                }

                // 直接移动文件
                if (File.Exists(targetPath))
                {
                    File.Delete(targetPath);
                }

                File.Move(sourcePath, targetPath);
                result.AddInfo($"成功移动文件: {sourcePath} -> {targetPath}");
            }
            catch (Exception ex)
            {
                result.AddError($"移动文件时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.FileOperation, ex, $"移动文件失败: {sourcePath} -> {targetPath}");
            }

            return result;
        }

        /// <summary>
        /// 安全读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">输出文件内容</param>
        /// <returns>验证结果</returns>
        public ValidationResult ReadFile(string filePath, out string content)
        {
            content = null;
            var result = _securityChecker.ValidateFilePath(filePath);

            if (!result.IsValid)
            {
                return result;
            }

            try
            {
                if (!File.Exists(filePath))
                {
                    result.AddError($"文件不存在: {filePath}");
                    _errorHandler.LogError(ErrorType.FileNotFound, $"尝试读取不存在的文件: {filePath}");
                    return result;
                }

                content = File.ReadAllText(filePath);
                result.AddInfo($"成功读取文件: {filePath}");
            }
            catch (Exception ex)
            {
                result.AddError($"读取文件时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.FileReadError, ex, $"读取文件失败: {filePath}");
            }

            return result;
        }

        /// <summary>
        /// 异步安全读取文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件内容和验证结果的元组</returns>
        public async Task<(string content, ValidationResult result)> ReadFileAsync(string filePath)
        {
            var result = _securityChecker.ValidateFilePath(filePath);
            string content = null;

            if (!result.IsValid)
            {
                return (null, result);
            }

            try
            {
                if (!File.Exists(filePath))
                {
                    result.AddError($"文件不存在: {filePath}");
                    _errorHandler.LogError(ErrorType.FileNotFound, $"尝试读取不存在的文件: {filePath}");
                    return (null, result);
                }

                // 异步读取文件内容
                using (StreamReader reader = new StreamReader(filePath))
                {
                    content = await reader.ReadToEndAsync();
                }

                result.AddInfo($"成功读取文件: {filePath}");
            }
            catch (Exception ex)
            {
                result.AddError($"读取文件时发生错误: {ex.Message}");
                _errorHandler.LogException(ErrorType.FileReadError, ex, $"读取文件失败: {filePath}");
            }

            return (content, result);
        }
    }
}
