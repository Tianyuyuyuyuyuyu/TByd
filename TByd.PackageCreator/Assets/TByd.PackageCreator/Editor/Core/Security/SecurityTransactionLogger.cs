using System;
using System.Collections.Generic;
using System.IO;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core.Security
{
    /// <summary>
    /// 安全事务记录器，用于记录文件操作事务并支持回滚操作
    /// </summary>
    public class SecurityTransactionLogger
    {
        private static SecurityTransactionLogger _sInstance;

        /// <summary>
        /// 单例实例
        /// </summary>
        public static SecurityTransactionLogger Instance
        {
            get
            {
                if (_sInstance == null)
                {
                    _sInstance = new SecurityTransactionLogger();
                }
                return _sInstance;
            }
        }

        private readonly ErrorHandler _mErrorHandler;
        private readonly TemplateSecurityChecker _mSecurityChecker;

        // 事务ID与操作记录的映射
        private readonly Dictionary<string, TransactionRecord> _mTransactions = new Dictionary<string, TransactionRecord>();

        /// <summary>
        /// 构造函数
        /// </summary>
        private SecurityTransactionLogger()
        {
            _mErrorHandler = ErrorHandler.Instance;
            _mSecurityChecker = TemplateSecurityChecker.Instance;
        }

        /// <summary>
        /// 开始新事务
        /// </summary>
        /// <param name="description">事务描述</param>
        /// <returns>事务ID</returns>
        public string BeginTransaction(string description)
        {
            var transactionId = Guid.NewGuid().ToString();
            var transaction = new TransactionRecord
            {
                TransactionId = transactionId,
                Description = description,
                StartTime = DateTime.Now,
                Operations = new List<FileOperation>()
            };

            _mTransactions[transactionId] = transaction;

            Debug.Log($"[SecurityTransactionLogger] 开始事务: {description} (ID: {transactionId})");

            return transactionId;
        }

        /// <summary>
        /// 记录文件创建操作
        /// </summary>
        /// <param name="transactionId">事务ID</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否记录成功</returns>
        public bool LogFileCreation(string transactionId, string filePath)
        {
            if (!_mTransactions.TryGetValue(transactionId, out var transaction))
            {
                _mErrorHandler.LogError(ErrorType.Security, $"尝试记录未知事务操作: {transactionId}");
                return false;
            }

            var operation = new FileOperation
            {
                OperationType = FileOperationType.Create,
                FilePath = filePath,
                Timestamp = DateTime.Now,
                BackupPath = null // 新建文件不需要备份
            };

            transaction.Operations.Add(operation);
            return true;
        }

        /// <summary>
        /// 记录文件修改操作
        /// </summary>
        /// <param name="transactionId">事务ID</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否记录成功</returns>
        public bool LogFileModification(string transactionId, string filePath)
        {
            if (!_mTransactions.TryGetValue(transactionId, out var transaction))
            {
                _mErrorHandler.LogError(ErrorType.Security, $"尝试记录未知事务操作: {transactionId}");
                return false;
            }

            // 创建文件备份
            string backupPath = null;
            if (File.Exists(filePath))
            {
                backupPath = _mSecurityChecker.CreateFileBackup(filePath);
            }

            var operation = new FileOperation
            {
                OperationType = FileOperationType.Modify,
                FilePath = filePath,
                Timestamp = DateTime.Now,
                BackupPath = backupPath
            };

            transaction.Operations.Add(operation);
            return true;
        }

        /// <summary>
        /// 记录文件删除操作
        /// </summary>
        /// <param name="transactionId">事务ID</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否记录成功</returns>
        public bool LogFileDeletion(string transactionId, string filePath)
        {
            if (!_mTransactions.TryGetValue(transactionId, out var transaction))
            {
                _mErrorHandler.LogError(ErrorType.Security, $"尝试记录未知事务操作: {transactionId}");
                return false;
            }

            // 创建文件备份
            string backupPath = null;
            if (File.Exists(filePath))
            {
                backupPath = _mSecurityChecker.CreateFileBackup(filePath);
            }

            var operation = new FileOperation
            {
                OperationType = FileOperationType.Delete,
                FilePath = filePath,
                Timestamp = DateTime.Now,
                BackupPath = backupPath
            };

            transaction.Operations.Add(operation);
            return true;
        }

        /// <summary>
        /// 记录目录创建操作
        /// </summary>
        /// <param name="transactionId">事务ID</param>
        /// <param name="directoryPath">目录路径</param>
        /// <returns>是否记录成功</returns>
        public bool LogDirectoryCreation(string transactionId, string directoryPath)
        {
            if (!_mTransactions.TryGetValue(transactionId, out var transaction))
            {
                _mErrorHandler.LogError(ErrorType.Security, $"尝试记录未知事务操作: {transactionId}");
                return false;
            }

            var operation = new FileOperation
            {
                OperationType = FileOperationType.CreateDirectory,
                FilePath = directoryPath,
                Timestamp = DateTime.Now,
                BackupPath = null
            };

            transaction.Operations.Add(operation);
            return true;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transactionId">事务ID</param>
        /// <returns>是否提交成功</returns>
        public bool CommitTransaction(string transactionId)
        {
            if (!_mTransactions.TryGetValue(transactionId, out var transaction))
            {
                _mErrorHandler.LogError(ErrorType.Security, $"尝试提交未知事务: {transactionId}");
                return false;
            }

            transaction.EndTime = DateTime.Now;
            transaction.IsCompleted = true;

            Debug.Log($"[SecurityTransactionLogger] 提交事务: {transaction.Description} (ID: {transactionId})，共 {transaction.Operations.Count} 个操作，耗时 {(transaction.EndTime - transaction.StartTime).TotalSeconds:F2} 秒");

            // 保留事务记录一段时间，以备回顾
            // 可以实现一个定期清理机制，清理旧事务记录

            return true;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <param name="transactionId">事务ID</param>
        /// <returns>是否回滚成功</returns>
        public bool RollbackTransaction(string transactionId)
        {
            if (!_mTransactions.TryGetValue(transactionId, out var transaction))
            {
                _mErrorHandler.LogError(ErrorType.Security, $"尝试回滚未知事务: {transactionId}");
                return false;
            }

            var success = true;

            // 逆序遍历操作记录，进行回滚
            for (var i = transaction.Operations.Count - 1; i >= 0; i--)
            {
                var operation = transaction.Operations[i];

                try
                {
                    switch (operation.OperationType)
                    {
                        case FileOperationType.Create:
                            // 回滚创建操作：删除文件
                            if (File.Exists(operation.FilePath))
                            {
                                try
                                {
                                    File.Delete(operation.FilePath);
                                    Debug.Log($"[SecurityTransactionLogger] 回滚：删除已创建的文件 {operation.FilePath}");
                                }
                                catch (Exception ex)
                                {
                                    _mErrorHandler.LogException(ErrorType.FileDeleteError, ex, $"回滚时删除文件失败: {operation.FilePath}");
                                    success = false;
                                }
                            }
                            break;

                        case FileOperationType.Modify:
                            // 回滚修改操作：从备份恢复
                            if (!string.IsNullOrEmpty(operation.BackupPath) && File.Exists(operation.BackupPath))
                            {
                                var restored = _mSecurityChecker.RestoreFileFromBackup(operation.BackupPath, operation.FilePath);
                                Debug.Log(restored
                                    ? $"[SecurityTransactionLogger] 回滚：从备份恢复文件 {operation.FilePath}"
                                    : $"[SecurityTransactionLogger] 回滚失败：无法从备份恢复文件 {operation.FilePath}");

                                success &= restored;
                            }
                            break;

                        case FileOperationType.Delete:
                            // 回滚删除操作：从备份恢复
                            if (!string.IsNullOrEmpty(operation.BackupPath) && File.Exists(operation.BackupPath))
                            {
                                var restored = _mSecurityChecker.RestoreFileFromBackup(operation.BackupPath, operation.FilePath);
                                Debug.Log(restored
                                    ? $"[SecurityTransactionLogger] 回滚：恢复已删除的文件 {operation.FilePath}"
                                    : $"[SecurityTransactionLogger] 回滚失败：无法恢复已删除的文件 {operation.FilePath}");

                                success &= restored;
                            }
                            break;

                        case FileOperationType.CreateDirectory:
                            // 回滚目录创建操作：删除目录
                            if (Directory.Exists(operation.FilePath))
                            {
                                try
                                {
                                    Directory.Delete(operation.FilePath, false); // 只删除空目录
                                    Debug.Log($"[SecurityTransactionLogger] 回滚：删除已创建的目录 {operation.FilePath}");
                                }
                                catch (IOException) // 目录不为空
                                {
                                    Debug.LogWarning($"[SecurityTransactionLogger] 回滚警告：目录不为空，无法删除 {operation.FilePath}");
                                }
                                catch (Exception ex)
                                {
                                    _mErrorHandler.LogException(ErrorType.FileDeleteError, ex, $"回滚时删除目录失败: {operation.FilePath}");
                                    success = false;
                                }
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    success = false;
                    _mErrorHandler.LogException(ErrorType.Security, ex, $"回滚操作失败: {operation.OperationType} {operation.FilePath}");
                }
            }

            transaction.IsRolledBack = true;
            transaction.EndTime = DateTime.Now;

            Debug.Log(success
                ? $"[SecurityTransactionLogger] 成功回滚事务: {transaction.Description} (ID: {transactionId})"
                : $"[SecurityTransactionLogger] 部分回滚事务: {transaction.Description} (ID: {transactionId})，部分操作无法回滚");

            return success;
        }

        /// <summary>
        /// 导出事务日志（用于调试和审计）
        /// </summary>
        /// <param name="filePath">导出文件路径</param>
        /// <returns>是否导出成功</returns>
        public bool ExportTransactionLogs(string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("# 安全事务日志");
                    writer.WriteLine($"导出时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    writer.WriteLine($"事务总数: {_mTransactions.Count}");
                    writer.WriteLine();

                    foreach (var transaction in _mTransactions.Values)
                    {
                        writer.WriteLine($"## 事务: {transaction.Description}");
                        writer.WriteLine($"ID: {transaction.TransactionId}");
                        writer.WriteLine($"开始时间: {transaction.StartTime:yyyy-MM-dd HH:mm:ss}");
                        writer.WriteLine($"结束时间: {transaction.EndTime:yyyy-MM-dd HH:mm:ss}");
                        writer.WriteLine($"状态: {(transaction.IsCompleted ? "已完成" : "未完成")}{(transaction.IsRolledBack ? "，已回滚" : "")}");
                        writer.WriteLine($"操作数: {transaction.Operations.Count}");
                        writer.WriteLine();

                        for (var i = 0; i < transaction.Operations.Count; i++)
                        {
                            var operation = transaction.Operations[i];
                            writer.WriteLine($"### 操作 {i + 1}: {operation.OperationType}");
                            writer.WriteLine($"时间: {operation.Timestamp:yyyy-MM-dd HH:mm:ss}");
                            writer.WriteLine($"路径: {operation.FilePath}");
                            if (!string.IsNullOrEmpty(operation.BackupPath))
                            {
                                writer.WriteLine($"备份: {operation.BackupPath}");
                            }
                            writer.WriteLine();
                        }

                        writer.WriteLine("---");
                        writer.WriteLine();
                    }
                }

                Debug.Log($"[SecurityTransactionLogger] 事务日志成功导出到: {filePath}");
                return true;
            }
            catch (Exception ex)
            {
                _mErrorHandler.LogException(ErrorType.FileWriteError, ex, $"导出事务日志失败: {ex.Message}");
                return false;
            }
        }

        #region 数据类型

        /// <summary>
        /// 文件操作类型
        /// </summary>
        public enum FileOperationType
        {
            Create,
            Modify,
            Delete,
            CreateDirectory
        }

        /// <summary>
        /// 文件操作记录
        /// </summary>
        public class FileOperation
        {
            /// <summary>
            /// 操作类型
            /// </summary>
            public FileOperationType OperationType { get; set; }

            /// <summary>
            /// 文件路径
            /// </summary>
            public string FilePath { get; set; }

            /// <summary>
            /// 操作时间戳
            /// </summary>
            public DateTime Timestamp { get; set; }

            /// <summary>
            /// 备份文件路径（如果适用）
            /// </summary>
            public string BackupPath { get; set; }
        }

        /// <summary>
        /// 事务记录
        /// </summary>
        public class TransactionRecord
        {
            /// <summary>
            /// 事务ID
            /// </summary>
            public string TransactionId { get; set; }

            /// <summary>
            /// 事务描述
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime StartTime { get; set; }

            /// <summary>
            /// 结束时间
            /// </summary>
            public DateTime EndTime { get; set; }

            /// <summary>
            /// 是否已完成
            /// </summary>
            public bool IsCompleted { get; set; }

            /// <summary>
            /// 是否已回滚
            /// </summary>
            public bool IsRolledBack { get; set; }

            /// <summary>
            /// 操作列表
            /// </summary>
            public List<FileOperation> Operations { get; set; }
        }

        #endregion
    }
}
