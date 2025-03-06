using System;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core.ErrorHandling;

namespace TByd.PackageCreator.Tests.Editor.Core.ErrorHandling
{
    /// <summary>
    /// 操作记录类测试
    /// </summary>
    public class OperationRecordTests
    {
        [Test]
        public void Constructor_ShouldSetDefaultValues()
        {
            // 执行
            OperationRecord record = new OperationRecord();

            // 断言
            Assert.IsNotNull(record.OperationId);
            Assert.IsNotNull(record.Timestamp);
            // 时间戳应该接近当前时间
            Assert.LessOrEqual((DateTime.Now - record.Timestamp).TotalSeconds, 1);
        }

        [Test]
        public void Create_ShouldSetBasicProperties()
        {
            // 安排
            OperationType operationType = OperationType.CreateFile;
            string targetPath = "test/path.txt";

            // 执行
            OperationRecord record = OperationRecord.Create(operationType, targetPath);

            // 断言
            Assert.AreEqual(operationType, record.OperationType);
            Assert.AreEqual(targetPath, record.TargetPath);
            Assert.IsNotNull(record.OperationId);
            Assert.IsNotNull(record.Timestamp);
        }

        [Test]
        public void CreateMoveOrCopy_WithMoveType_ShouldSetProperties()
        {
            // 安排
            OperationType operationType = OperationType.Move;
            string sourcePath = "source/path.txt";
            string targetPath = "target/path.txt";

            // 执行
            OperationRecord record = OperationRecord.CreateMoveOrCopy(operationType, sourcePath, targetPath);

            // 断言
            Assert.AreEqual(operationType, record.OperationType);
            Assert.AreEqual(sourcePath, record.SourcePath);
            Assert.AreEqual(targetPath, record.TargetPath);
        }

        [Test]
        public void CreateMoveOrCopy_WithCopyType_ShouldSetProperties()
        {
            // 安排
            OperationType operationType = OperationType.Copy;
            string sourcePath = "source/path.txt";
            string targetPath = "target/path.txt";

            // 执行
            OperationRecord record = OperationRecord.CreateMoveOrCopy(operationType, sourcePath, targetPath);

            // 断言
            Assert.AreEqual(operationType, record.OperationType);
            Assert.AreEqual(sourcePath, record.SourcePath);
            Assert.AreEqual(targetPath, record.TargetPath);
        }

        [Test]
        public void CreateMoveOrCopy_WithInvalidType_ShouldThrowException()
        {
            // 安排
            OperationType operationType = OperationType.CreateFile; // 不是Move或Copy
            string sourcePath = "source/path.txt";
            string targetPath = "target/path.txt";

            // 断言与执行
            Assert.Throws<ArgumentException>(() =>
                OperationRecord.CreateMoveOrCopy(operationType, sourcePath, targetPath));
        }

        [Test]
        public void CreateWithCustomHandler_ShouldSetProperties()
        {
            // 安排
            OperationType operationType = OperationType.Custom;
            string targetPath = "test/path.txt";
            Func<OperationRecord, bool> handler = (record) => true;

            // 执行
            OperationRecord record = OperationRecord.CreateWithCustomHandler(operationType, targetPath, handler);

            // 断言
            Assert.AreEqual(operationType, record.OperationType);
            Assert.AreEqual(targetPath, record.TargetPath);
            Assert.AreEqual(handler, record.CustomRollbackHandler);
        }

        [Test]
        public void CreateWithCustomHandler_WithNullHandler_ShouldThrowException()
        {
            // 安排
            OperationType operationType = OperationType.Custom;
            string targetPath = "test/path.txt";

            // 断言与执行
            Assert.Throws<ArgumentNullException>(() =>
                OperationRecord.CreateWithCustomHandler(operationType, targetPath, null));
        }

        [Test]
        public void ToString_WithoutSourcePath_ShouldFormatCorrectly()
        {
            // 安排
            OperationRecord record = new OperationRecord
            {
                OperationType = OperationType.CreateFile,
                TargetPath = "test/path.txt",
                Timestamp = new DateTime(2023, 1, 1, 12, 0, 0)
            };

            // 执行
            string result = record.ToString();

            // 断言
            Assert.IsTrue(result.Contains("[2023-01-01 12:00:00]"));
            Assert.IsTrue(result.Contains("[CreateFile]"));
            Assert.IsTrue(result.Contains("Target: test/path.txt"));
            Assert.IsFalse(result.Contains("Source:"));
        }

        [Test]
        public void ToString_WithSourcePath_ShouldIncludeSourcePath()
        {
            // 安排
            OperationRecord record = new OperationRecord
            {
                OperationType = OperationType.Move,
                SourcePath = "source/path.txt",
                TargetPath = "target/path.txt",
                Timestamp = new DateTime(2023, 1, 1, 12, 0, 0)
            };

            // 执行
            string result = record.ToString();

            // 断言
            Assert.IsTrue(result.Contains("[2023-01-01 12:00:00]"));
            Assert.IsTrue(result.Contains("[Move]"));
            Assert.IsTrue(result.Contains("Target: target/path.txt"));
            Assert.IsTrue(result.Contains("Source: source/path.txt"));
        }
    }
}
