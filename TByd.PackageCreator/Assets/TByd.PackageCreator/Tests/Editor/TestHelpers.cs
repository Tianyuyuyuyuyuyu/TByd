using System;
using System.IO;
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
            string directory = Path.Combine(Application.temporaryCachePath, "PackageCreator", "Tests");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return Path.Combine(directory, fileName);
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
        public static void EnsureTestDirectoryExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
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
        /// 预期错误处理器产生的日志消息
        /// </summary>
        /// <param name="errorLevel">错误级别</param>
        /// <param name="message">错误消息</param>
        public static void ExpectLogMessages(ErrorLevel errorLevel, string message)
        {
            switch (errorLevel)
            {
                case ErrorLevel.Critical:
                case ErrorLevel.Error:
                    LogAssert.Expect(LogType.Error, $"[PackageCreator] {(errorLevel == ErrorLevel.Critical ? "严重错误" : "错误")}: {message}");
                    break;
                case ErrorLevel.Warning:
                    LogAssert.Expect(LogType.Warning, $"[PackageCreator] 警告: {message}");
                    break;
                case ErrorLevel.Info:
                    LogAssert.Expect(LogType.Log, $"[PackageCreator] 信息: {message}");
                    break;
            }
        }
    }
}
