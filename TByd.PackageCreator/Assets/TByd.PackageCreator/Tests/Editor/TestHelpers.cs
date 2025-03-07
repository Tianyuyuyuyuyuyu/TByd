using System;
using System.IO;
using System.Text.RegularExpressions;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using UnityEngine;
using UnityEngine.TestTools;

namespace TByd.PackageCreator.Tests.Editor
{
    /// <summary>
    /// 测试辅助工具类，提供测试的通用功能
    /// </summary>
    public static class TestHelpers
    {
        /// <summary>
        /// 获取测试临时文件路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>完整的临时文件路径</returns>
        public static string GetTestFilePath(string fileName)
        {
            // 使用标准的临时目录而不是Application.temporaryCachePath
            var tempPath = Path.GetTempPath();
            var directory = Path.Combine(tempPath, "tbyd", "packagecreator", "tests");

            // 确保目录存在，并获取实际创建的目录路径
            directory = EnsureTestDirectoryExists(directory);

            // 使用Path.Combine确保路径分隔符的一致性
            var fullPath = Path.Combine(directory, fileName);

            // 如果文件已存在，先删除它
            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"清理测试文件失败: {fullPath}, 错误: {ex.Message}");
                }
            }

            return fullPath;
        }

        /// <summary>
        /// 创建测试错误信息
        /// </summary>
        /// <param name="errorLevel">错误级别</param>
        /// <param name="errorType">错误类型</param>
        /// <param name="message">错误消息</param>
        /// <returns>错误信息对象</returns>
        public static ErrorInfo CreateTestErrorInfo(ErrorLevel errorLevel, ErrorType errorType, string message)
        {
            return new ErrorInfo
            {
                Level = errorLevel,
                ErrorType = errorType,
                Message = message,
                Timestamp = DateTime.Now
            };
        }

        /// <summary>
        /// 创建测试异常
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <returns>异常对象</returns>
        public static Exception CreateTestException(string message)
        {
            return new InvalidOperationException(message);
        }

        /// <summary>
        /// 清理测试文件和目录
        /// </summary>
        /// <param name="path">文件或目录路径</param>
        public static void CleanupTestPath(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"清理测试路径时出错: {ex.Message}");
            }
        }

        /// <summary>
        /// 确保测试目录存在
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        /// <returns>实际使用的目录路径</returns>
        public static string EnsureTestDirectoryExists(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    // 创建目录，包括所有必需的父目录
                    Directory.CreateDirectory(directoryPath);

                    // 验证目录已创建
                    if (!Directory.Exists(directoryPath))
                    {
                        throw new DirectoryNotFoundException($"无法创建目录: {directoryPath}");
                    }

                    Debug.Log($"已创建测试目录: {directoryPath}");
                }

                return directoryPath;
            }
            catch (Exception ex)
            {
                // 如果创建失败，尝试使用应用程序的临时目录
                var fallbackDir = Path.Combine(Application.temporaryCachePath, "PackageCreatorTests");
                Debug.LogError($"创建目录失败: {directoryPath}, 错误: {ex.Message}. 使用备用目录: {fallbackDir}");

                if (!Directory.Exists(fallbackDir))
                {
                    Directory.CreateDirectory(fallbackDir);
                }

                return fallbackDir;
            }
        }

        /// <summary>
        /// 创建具有指定内容的测试文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="content">文件内容</param>
        public static void CreateTestFile(string filePath, string content)
        {
            EnsureTestDirectoryExists(Path.GetDirectoryName(filePath));
            File.WriteAllText(filePath, content);
        }

        /// <summary>
        /// 重置错误处理器状态
        /// </summary>
        public static void ResetErrorHandler()
        {
            ErrorHandler.Instance.ClearErrorLog();
            ErrorHandler.Instance.StopRecordingOperations();
        }

        /// <summary>
        /// 期望特定的日志消息
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="exactMessage">完整的日志消息</param>
        public static void ExpectLogMessage(LogType logType, string exactMessage)
        {
            LogAssert.Expect(logType, exactMessage);
        }

        /// <summary>
        /// 期望包含特定内容的日志消息
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="messageContent">日志消息中应包含的文本</param>
        public static void ExpectLogMessageContaining(LogType logType, string messageContent)
        {
            // 创建可以匹配任意包含指定内容的日志消息的正则表达式
            var regex = new Regex($".*{Regex.Escape(messageContent)}.*");
            LogAssert.Expect(logType, regex);
        }

        /// <summary>
        /// 期望匹配特定正则表达式的日志消息
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="pattern">正则表达式模式</param>
        public static void ExpectLogMessageMatching(LogType logType, string pattern)
        {
            LogAssert.Expect(logType, new Regex(pattern));
        }

        /// <summary>
        /// 期望来自ErrorHandler的标准格式日志消息
        /// </summary>
        /// <param name="errorLevel">错误级别</param>
        /// <param name="message">错误消息</param>
        public static void ExpectErrorHandlerMessage(ErrorLevel errorLevel, string message)
        {
            LogType logType;
            string prefix;

            switch (errorLevel)
            {
                case ErrorLevel.k_Critical:
                case ErrorLevel.k_Error:
                    logType = LogType.Error;
                    prefix = errorLevel == ErrorLevel.k_Critical ? "严重错误" : "错误";
                    break;
                case ErrorLevel.k_Warning:
                    logType = LogType.Warning;
                    prefix = "警告";
                    break;
                default:
                    logType = LogType.Log;
                    prefix = "信息";
                    break;
            }

            LogAssert.Expect(logType, $"[PackageCreator] {prefix}: {message}");
        }
    }
}
