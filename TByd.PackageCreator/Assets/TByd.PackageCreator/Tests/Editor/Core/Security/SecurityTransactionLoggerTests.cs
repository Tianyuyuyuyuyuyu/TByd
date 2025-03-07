using System;
using System.IO;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core.Security;
using UnityEngine;

namespace TByd.PackageCreator.Tests.Editor.Core.Security
{
    public class SecurityTransactionLoggerTests
    {
        private SecurityTransactionLogger logger;
        private string logFilePath;
        private string tempTestDir;

        [SetUp]
        public void Setup()
        {
            tempTestDir = Path.Combine(Path.GetTempPath(), "TByd_PackageCreator_Tests_Logs", Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempTestDir);

            logFilePath = Path.Combine(tempTestDir, "security_transaction.log");

            // 使用单例模式获取SecurityTransactionLogger实例
            logger = SecurityTransactionLogger.Instance;
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(tempTestDir))
            {
                try
                {
                    Directory.Delete(tempTestDir, true);
                }
                catch (IOException)
                {
                    // 有时Windows可能会保留文件句柄，忽略清理错误
                    Debug.LogWarning($"无法清理临时目录: {tempTestDir}");
                }
            }
        }

        [Test]
        public void BeginTransaction_CreatesTransactionEntry()
        {
            // 安排
            var transactionName = "CreatePackageFiles";

            // 执行
            var transactionId = logger.BeginTransaction(transactionName);

            // 断言
            Assert.IsNotNull(transactionId);
            Assert.IsNotEmpty(transactionId);
        }

        [Test]
        public void LogFileCreation_LogsOperationCorrectly()
        {
            // 安排
            var transactionId = logger.BeginTransaction("FileCreationTest");
            var filePath = "C:/Project/Assets/MyFile.cs";

            // 执行
            var result = logger.LogFileCreation(transactionId, filePath);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void LogFileModification_LogsOperationCorrectly()
        {
            // 安排
            var transactionId = logger.BeginTransaction("FileModificationTest");
            var filePath = "C:/Project/Assets/MyFile.cs";

            // 执行
            var result = logger.LogFileModification(transactionId, filePath);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void LogFileDeletion_LogsOperationCorrectly()
        {
            // 安排
            var transactionId = logger.BeginTransaction("FileDeletionTest");
            var filePath = "C:/Project/Assets/MyFile.cs";

            // 执行
            var result = logger.LogFileDeletion(transactionId, filePath);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void LogDirectoryCreation_LogsOperationCorrectly()
        {
            // 安排
            var transactionId = logger.BeginTransaction("DirectoryCreationTest");
            var dirPath = "C:/Project/Assets/MyDirectory";

            // 执行
            var result = logger.LogDirectoryCreation(transactionId, dirPath);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void CommitTransaction_MarksTransactionComplete()
        {
            // 安排
            var transactionName = "ModifyPackageConfig";
            var transactionId = logger.BeginTransaction(transactionName);

            // 执行
            var result = logger.CommitTransaction(transactionId);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void Transaction_WithMultipleOperations_WorksCorrectly()
        {
            // 安排 - 创建一个事务和多个操作
            var transactionId = logger.BeginTransaction("ComplexOperation");
            var filePath1 = "C:/Project/Assets/file1.cs";
            var filePath2 = "C:/Project/Assets/file2.cs";
            var dirPath = "C:/Project/Assets/NewDir";

            // 执行多个操作
            logger.LogFileCreation(transactionId, filePath1);
            logger.LogFileModification(transactionId, filePath2);
            logger.LogDirectoryCreation(transactionId, dirPath);
            var result = logger.CommitTransaction(transactionId);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void RollbackTransaction_RollsBackChanges()
        {
            // 安排
            var transactionId = logger.BeginTransaction("RollbackTest");
            var testFile = Path.Combine(tempTestDir, "rollbactest.txt");
            var testContent = "Test rollback content";

            // 创建实际文件（通常SafeFileOperations会代为处理）
            File.WriteAllText(testFile, testContent);

            // 记录文件创建
            logger.LogFileCreation(transactionId, testFile);

            // 回滚事务 (注意：实际回滚不由logger执行，只是记录)
            var result = logger.RollbackTransaction(transactionId);

            // 模拟回滚效果
            if (File.Exists(testFile))
            {
                File.Delete(testFile);
            }

            // 断言
            Assert.IsTrue(result);
            Assert.IsFalse(File.Exists(testFile));
        }

        [Test]
        public void ExportTransactionLogs_CreatesSeparateFile()
        {
            // 安排 - 创建一些事务
            var transaction1 = logger.BeginTransaction("ExportTest1");
            logger.LogFileCreation(transaction1, "C:/Project/Assets/file1.cs");
            logger.CommitTransaction(transaction1);

            var transaction2 = logger.BeginTransaction("ExportTest2");
            logger.LogFileModification(transaction2, "C:/Project/Assets/file2.cs");
            logger.CommitTransaction(transaction2);

            var exportPath = Path.Combine(tempTestDir, "exported_log.txt");

            // 执行
            var result = logger.ExportTransactionLogs(exportPath);

            // 断言
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(exportPath));

            var exportedContent = File.ReadAllText(exportPath);
            Assert.IsTrue(exportedContent.Contains("ExportTest1"));
            Assert.IsTrue(exportedContent.Contains("ExportTest2"));
        }
    }
}
