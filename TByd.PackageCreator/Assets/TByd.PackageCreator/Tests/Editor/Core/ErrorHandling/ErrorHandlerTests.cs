using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using UnityEngine;

namespace TByd.PackageCreator.Tests.Editor.Core.ErrorHandling
{
    /// <summary>
    /// 错误处理器测试类
    /// </summary>
    public class ErrorHandlerTests
    {
        private string _testFilePath;
        private string _testDirectoryPath;

        [SetUp]
        public void Setup()
        {
            // 创建测试用的临时文件和目录路径
            _testFilePath = Path.Combine(Application.temporaryCachePath, "PackageCreator", "Tests", "TestFile.txt");
            _testDirectoryPath = Path.Combine(Application.temporaryCachePath, "PackageCreator", "Tests", "TestDir");

            // 确保测试目录存在
            Directory.CreateDirectory(Path.GetDirectoryName(_testFilePath));

            // 确保每次测试前清空错误日志和停止操作记录
            ErrorHandler.Instance.ClearErrorLog();
            ErrorHandler.Instance.StopRecordingOperations();
        }

        [TearDown]
        public void TearDown()
        {
            // 清理测试文件和目录
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }

            if (Directory.Exists(_testDirectoryPath))
            {
                Directory.Delete(_testDirectoryPath, true);
            }

            // 清理错误日志和停止操作记录
            ErrorHandler.Instance.ClearErrorLog();
            ErrorHandler.Instance.StopRecordingOperations();
        }

        [Test]
        public void LogError_ShouldAddErrorToLog()
        {
            // 预期日志消息
            TestHelpers.ExpectLogMessages(ErrorLevel.Warning, "测试错误消息");

            // 安排
            string errorMessage = "测试错误消息";
            ErrorType errorType = ErrorType.Validation;
            ErrorLevel errorLevel = ErrorLevel.Warning;

            // 执行
            ErrorInfo errorInfo = ErrorHandler.Instance.LogError(errorType, errorMessage, errorLevel);
            List<ErrorInfo> errorLog = ErrorHandler.Instance.GetErrorLog();

            // 断言
            Assert.IsNotNull(errorInfo);
            Assert.AreEqual(1, errorLog.Count);
            Assert.AreEqual(errorMessage, errorInfo.Message);
            Assert.AreEqual(errorType, errorInfo.ErrorType);
            Assert.AreEqual(errorLevel, errorInfo.Level);
        }

        [Test]
        public void ExportErrorLog_ShouldCreateLogFile()
        {
            // 预期日志消息
            TestHelpers.ExpectLogMessages(ErrorLevel.Info, "测试配置错误");
            TestHelpers.ExpectLogMessages(ErrorLevel.Error, "测试文件错误");

            // 安排
            ErrorHandler.Instance.LogError(ErrorType.Configuration, "测试配置错误", ErrorLevel.Info);
            ErrorHandler.Instance.LogError(ErrorType.FileOperation, "测试文件错误", ErrorLevel.Error);

            // 执行
            string exportPath = Path.Combine(Application.temporaryCachePath, "PackageCreator", "Tests", "ErrorLog.txt");
            string filePath = ErrorHandler.Instance.ExportErrorLog(exportPath);

            // 断言
            Assert.IsTrue(File.Exists(filePath));
            string content = File.ReadAllText(filePath);
            Assert.IsTrue(content.Contains("测试配置错误"));
            Assert.IsTrue(content.Contains("测试文件错误"));

            // 清理
            File.Delete(filePath);
        }

        [Test]
        public void RecordAndRollbackFileCreation_ShouldWorkCorrectly()
        {
            // 安排
            ErrorHandler.Instance.StartRecordingOperations();

            // 执行
            ErrorHandler.Instance.RecordFileCreation(_testFilePath);
            File.WriteAllText(_testFilePath, "测试内容");
            bool fileCreated = File.Exists(_testFilePath);

            bool rollbackResult = ErrorHandler.Instance.RollbackOperations();
            bool fileExists = File.Exists(_testFilePath);

            // 断言
            Assert.IsTrue(fileCreated, "文件应该已创建");
            Assert.IsTrue(rollbackResult, "回滚应该成功");
            Assert.IsFalse(fileExists, "回滚后文件应该不存在");
        }

        [Test]
        public void RecordAndRollbackDirectoryCreation_ShouldWorkCorrectly()
        {
            // 安排
            ErrorHandler.Instance.StartRecordingOperations();

            // 执行
            ErrorHandler.Instance.RecordDirectoryCreation(_testDirectoryPath);
            Directory.CreateDirectory(_testDirectoryPath);
            bool dirCreated = Directory.Exists(_testDirectoryPath);

            bool rollbackResult = ErrorHandler.Instance.RollbackOperations();
            bool dirExists = Directory.Exists(_testDirectoryPath);

            // 断言
            Assert.IsTrue(dirCreated, "目录应该已创建");
            Assert.IsTrue(rollbackResult, "回滚应该成功");
            Assert.IsFalse(dirExists, "回滚后目录应该不存在");
        }

        [Test]
        public void RecordAndRollbackFileModification_ShouldWorkCorrectly()
        {
            // 安排
            string originalContent = "原始内容";
            string newContent = "新内容";
            File.WriteAllText(_testFilePath, originalContent);

            ErrorHandler.Instance.StartRecordingOperations();

            // 执行
            ErrorHandler.Instance.RecordFileModification(_testFilePath);
            File.WriteAllText(_testFilePath, newContent);
            string contentAfterModify = File.ReadAllText(_testFilePath);

            bool rollbackResult = ErrorHandler.Instance.RollbackOperations();
            string contentAfterRollback = File.ReadAllText(_testFilePath);

            // 断言
            Assert.AreEqual(newContent, contentAfterModify, "修改后内容应该是新内容");
            Assert.IsTrue(rollbackResult, "回滚应该成功");
            Assert.AreEqual(originalContent, contentAfterRollback, "回滚后内容应该是原始内容");
        }

        [Test]
        public void ClearErrorLog_ShouldRemoveAllErrors()
        {
            // 预期日志消息
            TestHelpers.ExpectLogMessages(ErrorLevel.Info, "错误1");
            TestHelpers.ExpectLogMessages(ErrorLevel.Error, "错误2");

            // 安排
            ErrorHandler.Instance.LogError(ErrorType.Configuration, "错误1", ErrorLevel.Info);
            ErrorHandler.Instance.LogError(ErrorType.FileOperation, "错误2", ErrorLevel.Error);

            // 执行
            ErrorHandler.Instance.ClearErrorLog();
            List<ErrorInfo> errorLog = ErrorHandler.Instance.GetErrorLog();

            // 断言
            Assert.AreEqual(0, errorLog.Count, "错误日志应该为空");
        }
    }
}
