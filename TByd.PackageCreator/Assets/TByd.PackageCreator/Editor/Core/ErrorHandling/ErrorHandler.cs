using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core.ErrorHandling
{
    /// <summary>
    /// 错误处理器，负责处理包创建过程中的各种错误和异常
    /// </summary>
    public class ErrorHandler
    {
        #region 单例实现
        private static ErrorHandler _instance;
        private static readonly object _lock = new object();

        public static ErrorHandler Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ErrorHandler();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        private List<ErrorInfo> _errorLog = new List<ErrorInfo>();
        private List<OperationRecord> _operationHistory = new List<OperationRecord>();
        private bool _isRecordingOperations = false;

        /// <summary>
        /// 错误处理器构造函数
        /// </summary>
        private ErrorHandler()
        {
            // 初始化错误处理器
        }

        /// <summary>
        /// 记录一个错误
        /// </summary>
        /// <param name="errorType">错误类型</param>
        /// <param name="errorMessage">错误消息</param>
        /// <param name="errorLevel">错误级别</param>
        /// <param name="exception">相关异常（可选）</param>
        /// <returns>记录的错误信息</returns>
        public ErrorInfo LogError(ErrorType errorType, string errorMessage, ErrorLevel errorLevel, Exception exception = null)
        {
            var errorInfo = new ErrorInfo
            {
                ErrorType = errorType,
                Message = errorMessage,
                Level = errorLevel,
                Timestamp = DateTime.Now,
                Exception = exception
            };

            _errorLog.Add(errorInfo);

            // 根据错误级别选择不同的日志记录方式
            switch (errorLevel)
            {
                case ErrorLevel.Critical:
                    Debug.LogError($"[PackageCreator] 严重错误: {errorMessage}");
                    break;
                case ErrorLevel.Error:
                    Debug.LogError($"[PackageCreator] 错误: {errorMessage}");
                    break;
                case ErrorLevel.Warning:
                    Debug.LogWarning($"[PackageCreator] 警告: {errorMessage}");
                    break;
                case ErrorLevel.Info:
                    Debug.Log($"[PackageCreator] 信息: {errorMessage}");
                    break;
            }

            return errorInfo;
        }

        /// <summary>
        /// 显示友好的错误提示
        /// </summary>
        /// <param name="errorInfo">错误信息</param>
        /// <param name="showStackTrace">是否显示堆栈跟踪</param>
        public void ShowErrorDialog(ErrorInfo errorInfo, bool showStackTrace = false)
        {
            string title;
            switch (errorInfo.Level)
            {
                case ErrorLevel.Critical:
                    title = "严重错误";
                    break;
                case ErrorLevel.Error:
                    title = "错误";
                    break;
                case ErrorLevel.Warning:
                    title = "警告";
                    break;
                default:
                    title = "提示";
                    break;
            }

            var message = errorInfo.Message;
            if (showStackTrace && errorInfo.Exception != null)
            {
                message += $"\n\n堆栈跟踪:\n{errorInfo.Exception.StackTrace}";
            }

            switch (errorInfo.Level)
            {
                case ErrorLevel.Critical:
                case ErrorLevel.Error:
                    EditorUtility.DisplayDialog(title, message, "确定");
                    break;
                case ErrorLevel.Warning:
                    var proceed = EditorUtility.DisplayDialog(title, message, "继续", "取消");
                    if (!proceed)
                    {
                        throw new OperationCanceledException("用户取消了操作");
                    }
                    break;
                case ErrorLevel.Info:
                    EditorUtility.DisplayDialog(title, message, "确定");
                    break;
            }
        }

        /// <summary>
        /// 导出错误日志到文件
        /// </summary>
        /// <param name="filePath">文件路径，如果为null将使用默认路径</param>
        /// <returns>导出文件的完整路径</returns>
        public string ExportErrorLog(string filePath = null)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                var directory = Path.Combine(Application.temporaryCachePath, "PackageCreator", "Logs");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                filePath = Path.Combine(directory, $"ErrorLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
            }

            var sb = new StringBuilder();
            sb.AppendLine("================ PackageCreator 错误日志 ================");
            sb.AppendLine($"导出时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"总记录数: {_errorLog.Count}\n");

            foreach (var error in _errorLog)
            {
                sb.AppendLine($"[{error.Timestamp:yyyy-MM-dd HH:mm:ss}] [{error.Level}] [{error.ErrorType}]");
                sb.AppendLine($"消息: {error.Message}");

                if (error.Exception != null)
                {
                    sb.AppendLine($"异常: {error.Exception.GetType().Name}");
                    sb.AppendLine($"详情: {error.Exception.Message}");
                    sb.AppendLine($"堆栈跟踪:\n{error.Exception.StackTrace}");
                }

                sb.AppendLine(new string('-', 50));
            }

            File.WriteAllText(filePath, sb.ToString());
            return filePath;
        }

        /// <summary>
        /// 清除错误日志
        /// </summary>
        public void ClearErrorLog()
        {
            _errorLog.Clear();
        }

        /// <summary>
        /// 开始记录操作历史，用于可能需要回滚的操作序列
        /// </summary>
        public void StartRecordingOperations()
        {
            _isRecordingOperations = true;
            _operationHistory.Clear();
        }

        /// <summary>
        /// 停止记录操作历史
        /// </summary>
        public void StopRecordingOperations()
        {
            _isRecordingOperations = false;
        }

        /// <summary>
        /// 记录一个操作
        /// </summary>
        /// <param name="operationType">操作类型</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="backupData">备份数据（可选）</param>
        public void RecordOperation(OperationType operationType, string targetPath, byte[] backupData = null)
        {
            if (!_isRecordingOperations) return;

            _operationHistory.Add(new OperationRecord
            {
                OperationType = operationType,
                TargetPath = targetPath,
                BackupData = backupData,
                Timestamp = DateTime.Now
            });
        }

        /// <summary>
        /// 记录文件创建操作
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void RecordFileCreation(string filePath)
        {
            RecordOperation(OperationType.CreateFile, filePath);
        }

        /// <summary>
        /// 记录文件修改操作，并备份原始内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void RecordFileModification(string filePath)
        {
            if (!_isRecordingOperations || !File.Exists(filePath)) return;

            var originalContent = File.ReadAllBytes(filePath);
            RecordOperation(OperationType.ModifyFile, filePath, originalContent);
        }

        /// <summary>
        /// 记录目录创建操作
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        public void RecordDirectoryCreation(string directoryPath)
        {
            RecordOperation(OperationType.CreateDirectory, directoryPath);
        }

        /// <summary>
        /// 执行操作回滚
        /// </summary>
        /// <returns>回滚是否成功</returns>
        public bool RollbackOperations()
        {
            if (_operationHistory.Count == 0) return true;

            var success = true;
            // 逆序遍历操作历史，执行回滚
            for (var i = _operationHistory.Count - 1; i >= 0; i--)
            {
                var operation = _operationHistory[i];
                try
                {
                    switch (operation.OperationType)
                    {
                        case OperationType.CreateFile:
                            if (File.Exists(operation.TargetPath))
                            {
                                File.Delete(operation.TargetPath);
                                Debug.Log($"已回滚：删除文件 {operation.TargetPath}");
                            }
                            break;

                        case OperationType.ModifyFile:
                            if (operation.BackupData != null && File.Exists(operation.TargetPath))
                            {
                                File.WriteAllBytes(operation.TargetPath, operation.BackupData);
                                Debug.Log($"已回滚：恢复文件 {operation.TargetPath} 的内容");
                            }
                            break;

                        case OperationType.CreateDirectory:
                            if (Directory.Exists(operation.TargetPath))
                            {
                                // 尝试删除目录，但只有在目录为空时才能成功
                                try
                                {
                                    Directory.Delete(operation.TargetPath);
                                    Debug.Log($"已回滚：删除目录 {operation.TargetPath}");
                                }
                                catch (IOException)
                                {
                                    Debug.LogWarning($"无法删除非空目录: {operation.TargetPath}");
                                }
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"回滚操作失败: {ex.Message}");
                    success = false;
                }
            }

            _operationHistory.Clear();
            return success;
        }

        /// <summary>
        /// 获取当前错误日志的副本
        /// </summary>
        /// <returns>错误日志列表的副本</returns>
        public List<ErrorInfo> GetErrorLog()
        {
            return new List<ErrorInfo>(_errorLog);
        }
    }
}
