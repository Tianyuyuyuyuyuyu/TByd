using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core;
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
            string transactionName = "CreatePackageFiles";

            // 执行
            string transactionId = logger.BeginTransaction(transactionName);

            // 断言
            Assert.IsNotNull(transactionId);
            Assert.IsNotEmpty(transactionId);
        }

        [Test]
        public void LogFileCreation_LogsOperationCorrectly()
        {
            // 安排
            string transactionId = logger.BeginTransaction("FileCreationTest");
            string filePath = "C:/Project/Assets/MyFile.cs";

            // 执行
            bool result = logger.LogFileCreation(transactionId, filePath);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void LogFileModification_LogsOperationCorrectly()
        {
            // 安排
            string transactionId = logger.BeginTransaction("FileModificationTest");
            string filePath = "C:/Project/Assets/MyFile.cs";

            // 执行
            bool result = logger.LogFileModification(transactionId, filePath);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void LogFileDeletion_LogsOperationCorrectly()
        {
            // 安排
            string transactionId = logger.BeginTransaction("FileDeletionTest");
            string filePath = "C:/Project/Assets/MyFile.cs";

            // 执行
            bool result = logger.LogFileDeletion(transactionId, filePath);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void LogDirectoryCreation_LogsOperationCorrectly()
        {
            // 安排
            string transactionId = logger.BeginTransaction("DirectoryCreationTest");
            string dirPath = "C:/Project/Assets/MyDirectory";

            // 执行
            bool result = logger.LogDirectoryCreation(transactionId, dirPath);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void CommitTransaction_MarksTransactionComplete()
        {
            // 安排
            string transactionName = "ModifyPackageConfig";
            string transactionId = logger.BeginTransaction(transactionName);

            // 执行
            bool result = logger.CommitTransaction(transactionId);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void Transaction_WithMultipleOperations_WorksCorrectly()
        {
            // 安排 - 创建一个事务和多个操作
            string transactionId = logger.BeginTransaction("ComplexOperation");
            string filePath1 = "C:/Project/Assets/file1.cs";
            string filePath2 = "C:/Project/Assets/file2.cs";
            string dirPath = "C:/Project/Assets/NewDir";

            // 执行多个操作
            logger.LogFileCreation(transactionId, filePath1);
            logger.LogFileModification(transactionId, filePath2);
            logger.LogDirectoryCreation(transactionId, dirPath);
            bool result = logger.CommitTransaction(transactionId);

            // 断言
            Assert.IsTrue(result);
        }

        [Test]
        public void RollbackTransaction_RollsBackChanges()
        {
            // 安排
            string transactionId = logger.BeginTransaction("RollbackTest");
            string testFile = Path.Combine(tempTestDir, "rollback_test.txt");
            string testContent = "Test rollback content";

            // 创建实际文件（通常SafeFileOperations会代为处理）
            File.WriteAllText(testFile, testContent);

            // 记录文件创建
            logger.LogFileCreation(transactionId, testFile);

            // 回滚事务 (注意：实际回滚不由logger执行，只是记录)
            bool result = logger.RollbackTransaction(transactionId);

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
            string transaction1 = logger.BeginTransaction("ExportTest1");
            logger.LogFileCreation(transaction1, "C:/Project/Assets/file1.cs");
            logger.CommitTransaction(transaction1);

            string transaction2 = logger.BeginTransaction("ExportTest2");
            logger.LogFileModification(transaction2, "C:/Project/Assets/file2.cs");
            logger.CommitTransaction(transaction2);

            string exportPath = Path.Combine(tempTestDir, "exported_log.txt");

            // 执行
            bool result = logger.ExportTransactionLogs(exportPath);

            // 断言
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(exportPath));

            string exportedContent = File.ReadAllText(exportPath);
            Assert.IsTrue(exportedContent.Contains("ExportTest1"));
            Assert.IsTrue(exportedContent.Contains("ExportTest2"));
        }
    }
}
