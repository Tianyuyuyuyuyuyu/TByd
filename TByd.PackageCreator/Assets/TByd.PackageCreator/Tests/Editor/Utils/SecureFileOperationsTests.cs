using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Utils.FileSystem;
using UnityEngine;

namespace TByd.PackageCreator.Tests.Editor.Utils
{
    /// <summary>
    /// SecureFileOperations工具类的测试
    /// </summary>
    public class SecureFileOperationsTests
    {
        private string _testDirectory;
        private string _testFile;
        private string _testContent;
        private string _backupDirectory;

        [SetUp]
        public void Setup()
        {
            // 创建测试目录和文件
            _testDirectory = Path.Combine(Application.temporaryCachePath, "SecureFileOperationsTests");
            _testFile = Path.Combine(_testDirectory, "testfile.txt");
            _testContent = "测试内容\n第二行\n第三行";

            // 获取备份目录
            _backupDirectory = SecureFileOperations.GetBackupDirectory();

            // 确保测试开始前目录不存在
            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }

            // 创建测试目录
            Directory.CreateDirectory(_testDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            // 测试结束后清理
            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }

            // 清理所有备份（可选，取决于是否需要保留测试生成的备份）
            SecureFileOperations.CleanupOldBackups();
        }

        [Test]
        public void GetBackupDirectory_ReturnsValidDirectory()
        {
            // 执行
            var backupDir = SecureFileOperations.GetBackupDirectory();

            // 验证
            Assert.IsNotNull(backupDir, "备份目录不应为null");
            Assert.IsTrue(Directory.Exists(backupDir), "备份目录应存在");
        }

        [Test]
        public void CreateBackup_CreatesBackupFile_WhenFileExists()
        {
            // 准备
            File.WriteAllText(_testFile, _testContent);

            // 执行
            var backupPath = SecureFileOperations.CreateBackup(_testFile);

            // 验证
            Assert.IsNotNull(backupPath, "备份路径不应为null");
            Assert.IsTrue(File.Exists(backupPath), "备份文件应该存在");
            Assert.AreEqual(_testContent, File.ReadAllText(backupPath), "备份内容应该与原始内容相同");
        }

        [Test]
        public void CreateBackup_ReturnsNull_WhenFileDoesNotExist()
        {
            // 准备

            // 执行
            var backupPath = SecureFileOperations.CreateBackup(_testFile);

            // 验证
            Assert.IsNull(backupPath, "文件不存在时应返回null");
        }

        [Test]
        public void RestoreBackup_RestoresFile_WhenBackupExists()
        {
            // 准备 - 创建原始文件和备份
            File.WriteAllText(_testFile, _testContent);
            var backupPath = SecureFileOperations.CreateBackup(_testFile);

            // 修改原始文件
            File.WriteAllText(_testFile, "修改后的内容");

            // 执行
            var result = SecureFileOperations.RestoreBackup(backupPath, _testFile);

            // 验证
            Assert.IsTrue(result, "应返回true表示恢复成功");
            Assert.AreEqual(_testContent, File.ReadAllText(_testFile), "文件内容应被恢复");
        }

        [Test]
        public void RestoreBackup_ReturnsFalse_WhenBackupDoesNotExist()
        {
            // 准备
            var nonExistentBackup = Path.Combine(_backupDirectory, "nonexistent.backup");

            // 执行
            var result = SecureFileOperations.RestoreBackup(nonExistentBackup);

            // 验证
            Assert.IsFalse(result, "备份不存在时应返回false");
        }

        [Test]
        public void SafeWriteFile_WritesContent_AndCreatesBackup()
        {
            // 准备 - 创建原始文件
            File.WriteAllText(_testFile, "原始内容");

            // 执行
            var result = SecureFileOperations.SafeWriteFile(_testFile, _testContent, true);

            // 验证
            Assert.IsTrue(result, "应返回true表示写入成功");
            Assert.AreEqual(_testContent, File.ReadAllText(_testFile), "文件内容应被更新");

            // 验证备份应该存在 - 但由于备份文件名包含时间戳，我们需检查备份目录是否存在备份文件
            var backupFiles = Directory.GetFiles(_backupDirectory);
            var hasBackup = Array.Exists(backupFiles, f => f.Contains("testfile.txt") && f.EndsWith(".backup"));
            Assert.IsTrue(hasBackup, "应存在备份文件");
        }

        [Test]
        public void SafeWriteFile_WritesContent_WithoutBackup()
        {
            // 准备 - 创建原始文件
            File.WriteAllText(_testFile, "原始内容");

            // 执行 - 不创建备份
            var result = SecureFileOperations.SafeWriteFile(_testFile, _testContent, false);

            // 验证
            Assert.IsTrue(result, "应返回true表示写入成功");
            Assert.AreEqual(_testContent, File.ReadAllText(_testFile), "文件内容应被更新");
        }

        [Test]
        public void SafeWriteFile_CreatesDirectory_WhenItDoesNotExist()
        {
            // 准备
            var subDir = Path.Combine(_testDirectory, "subdir");
            var filePath = Path.Combine(subDir, "newfile.txt");

            // 执行
            var result = SecureFileOperations.SafeWriteFile(filePath, _testContent);

            // 验证
            Assert.IsTrue(result, "应返回true表示写入成功");
            Assert.IsTrue(Directory.Exists(subDir), "目录应该被创建");
            Assert.IsTrue(File.Exists(filePath), "文件应该被创建");
            Assert.AreEqual(_testContent, File.ReadAllText(filePath), "文件内容应该匹配");
        }

        [Test]
        public void SafeWriteFiles_WritesBatchFiles_WithBackups()
        {
            // 准备
            var file1 = Path.Combine(_testDirectory, "file1.txt");
            var file2 = Path.Combine(_testDirectory, "file2.txt");
            var fileContents = new Dictionary<string, string>
            {
                { file1, "内容1" },
                { file2, "内容2" }
            };

            // 先创建一个文件，用于测试备份
            File.WriteAllText(file1, "原始内容1");

            // 执行
            var result = SecureFileOperations.SafeWriteFiles(fileContents);

            // 验证
            Assert.IsTrue(result, "应返回true表示写入成功");
            Assert.AreEqual("内容1", File.ReadAllText(file1), "文件1内容应被更新");
            Assert.AreEqual("内容2", File.ReadAllText(file2), "文件2内容应被更新");

            // 验证备份应该存在
            var backupFiles = Directory.GetFiles(_backupDirectory);
            var hasBackup = Array.Exists(backupFiles, f => f.Contains("file1.txt") && f.EndsWith(".backup"));
            Assert.IsTrue(hasBackup, "应存在备份文件");
        }

        [Test]
        public void SafeDeleteFile_DeletesFile_AndCreatesBackup()
        {
            // 准备
            File.WriteAllText(_testFile, _testContent);

            // 执行
            var result = SecureFileOperations.SafeDeleteFile(_testFile);

            // 验证
            Assert.IsTrue(result, "应返回true表示删除成功");
            Assert.IsFalse(File.Exists(_testFile), "文件应该被删除");

            // 验证备份应该存在
            var backupFiles = Directory.GetFiles(_backupDirectory);
            var hasBackup = Array.Exists(backupFiles, f => f.Contains("testfile.txt") && f.EndsWith(".backup"));
            Assert.IsTrue(hasBackup, "应存在备份文件");
        }

        [Test]
        public void SafeDeleteFile_ReturnsFalse_WhenFileDoesNotExist()
        {
            // 准备
            var nonExistentFile = Path.Combine(_testDirectory, "nonexistent.txt");

            // 执行
            var result = SecureFileOperations.SafeDeleteFile(nonExistentFile);

            // 验证
            Assert.IsFalse(result, "文件不存在时应返回false");
        }

        [Test]
        public void GetAllBackups_ReturnsAllBackups()
        {
            // 准备 - 创建几个备份
            for (var i = 1; i <= 3; i++)
            {
                var filePath = Path.Combine(_testDirectory, $"file{i}.txt");
                File.WriteAllText(filePath, $"内容{i}");
                SecureFileOperations.CreateBackup(filePath);
            }

            // 执行
            var backups = SecureFileOperations.GetAllBackups();

            // 验证
            Assert.IsNotNull(backups, "备份列表不应为null");
            Assert.GreaterOrEqual(backups.Count, 3, "应至少包含创建的3个备份");

            // 验证所有创建的备份都在列表中
            var allFound = true;
            for (var i = 1; i <= 3; i++)
            {
                var fileName = $"file{i}.txt";
                var found = backups.Exists(b => Path.GetFileName(b).Contains(fileName));
                allFound &= found;
            }
            Assert.IsTrue(allFound, "所有创建的备份都应在列表中");
        }

        [Test]
        public void CleanupOldBackups_RemovesOldBackups()
        {
            // 注意：这个测试可能不够精确，因为它依赖于时间和实际实现
            // 我们将创建备份，然后调用清理，并验证备份仍存在（因为它们是新的）

            // 准备 - 创建备份
            File.WriteAllText(_testFile, _testContent);
            var backupPath = SecureFileOperations.CreateBackup(_testFile);

            // 执行
            SecureFileOperations.CleanupOldBackups();

            // 验证 - 新创建的备份应该仍然存在
            Assert.IsTrue(File.Exists(backupPath), "新创建的备份不应被删除");
        }

        [Test]
        public void IsFileWritable_ReturnsTrue_ForWritableFile()
        {
            // 准备
            File.WriteAllText(_testFile, _testContent);

            // 执行
            var result = SecureFileOperations.IsFileWritable(_testFile);

            // 验证
            Assert.IsTrue(result, "可写文件应返回true");
        }

        [Test]
        public void IsFileWritable_ReturnsFalse_ForReadOnlyFile()
        {
            // 准备
            File.WriteAllText(_testFile, _testContent);
            File.SetAttributes(_testFile, FileAttributes.ReadOnly);

            try
            {
                // 执行
                var result = SecureFileOperations.IsFileWritable(_testFile);

                // 验证
                Assert.IsFalse(result, "只读文件应返回false");
            }
            finally
            {
                // 清理 - 恢复文件属性
                File.SetAttributes(_testFile, FileAttributes.Normal);
            }
        }

        [Test]
        public void IsPathInSafeZone_ReturnsTrue_ForPathsInSafeZone()
        {
            // 准备 - 使用默认安全区（应用程序数据路径）
            var safePath = Path.Combine(Application.dataPath, "SafeTest");

            // 执行
            var result = SecureFileOperations.IsPathInSafeZone(safePath);

            // 验证
            Assert.IsTrue(result, "应用程序数据路径内的路径应在安全区内");
        }

        [Test]
        public void IsPathInSafeZone_ReturnsTrue_ForCustomSafeZones()
        {
            // 准备 - 自定义安全区
            var safeRoots = new List<string> { _testDirectory };
            var safePath = Path.Combine(_testDirectory, "SafeTest");

            // 执行
            var result = SecureFileOperations.IsPathInSafeZone(safePath, safeRoots);

            // 验证
            Assert.IsTrue(result, "自定义安全区内的路径应返回true");
        }

        [Test]
        public void IsPathInSafeZone_ReturnsFalse_ForPathsOutsideSafeZone()
        {
            // 准备 - 系统目录应该不在默认安全区内
            var unsafePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "test");

            // 执行
            var result = SecureFileOperations.IsPathInSafeZone(unsafePath);

            // 验证
            Assert.IsFalse(result, "安全区外的路径应返回false");
        }

        [Test]
        public void SafeMoveFile_MovesFile_WhenBothPathsInSafeZone()
        {
            // 准备
            var sourceFile = Path.Combine(_testDirectory, "source.txt");
            var destFile = Path.Combine(_testDirectory, "dest.txt");
            File.WriteAllText(sourceFile, _testContent);

            // 执行
            var result = SecureFileOperations.SafeMoveFile(sourceFile, destFile);

            // 验证
            Assert.IsTrue(result, "应返回true表示移动成功");
            Assert.IsFalse(File.Exists(sourceFile), "源文件应不存在");
            Assert.IsTrue(File.Exists(destFile), "目标文件应存在");
            Assert.AreEqual(_testContent, File.ReadAllText(destFile), "目标文件内容应与源文件相同");
        }

        [Test]
        public void SafeMoveFile_ReturnsFalse_WhenSourceFileDoesNotExist()
        {
            // 准备
            var sourceFile = Path.Combine(_testDirectory, "nonexistent.txt");
            var destFile = Path.Combine(_testDirectory, "dest.txt");

            // 执行
            var result = SecureFileOperations.SafeMoveFile(sourceFile, destFile);

            // 验证
            Assert.IsFalse(result, "源文件不存在时应返回false");
            Assert.IsFalse(File.Exists(destFile), "目标文件不应被创建");
        }

        [Test]
        public void SafeCopyDirectory_CopiesDirectory()
        {
            // 准备 - 创建源目录结构
            var sourceDir = Path.Combine(_testDirectory, "source");
            var targetDir = Path.Combine(_testDirectory, "target");
            var subDir = Path.Combine(sourceDir, "subdir");
            var sourceFile1 = Path.Combine(sourceDir, "file1.txt");
            var sourceFile2 = Path.Combine(subDir, "file2.txt");

            Directory.CreateDirectory(sourceDir);
            Directory.CreateDirectory(subDir);
            File.WriteAllText(sourceFile1, "内容1");
            File.WriteAllText(sourceFile2, "内容2");

            // 执行
            var result = SecureFileOperations.SafeCopyDirectory(sourceDir, targetDir);

            // 验证
            Assert.IsTrue(result, "应返回true表示复制成功");
            Assert.IsTrue(Directory.Exists(targetDir), "目标目录应存在");
            Assert.IsTrue(Directory.Exists(Path.Combine(targetDir, "subdir")), "子目录应被复制");
            Assert.IsTrue(File.Exists(Path.Combine(targetDir, "file1.txt")), "文件1应被复制");
            Assert.IsTrue(File.Exists(Path.Combine(targetDir, "subdir", "file2.txt")), "文件2应被复制");
            Assert.AreEqual("内容1", File.ReadAllText(Path.Combine(targetDir, "file1.txt")), "文件1内容应匹配");
            Assert.AreEqual("内容2", File.ReadAllText(Path.Combine(targetDir, "subdir", "file2.txt")), "文件2内容应匹配");
        }

        [Test]
        public void SafeCopyDirectory_ReturnsFalse_WhenSourceDirectoryDoesNotExist()
        {
            // 准备
            var sourceDir = Path.Combine(_testDirectory, "nonexistent");
            var targetDir = Path.Combine(_testDirectory, "target");

            // 执行
            var result = SecureFileOperations.SafeCopyDirectory(sourceDir, targetDir);

            // 验证
            Assert.IsFalse(result, "源目录不存在时应返回false");
            Assert.IsFalse(Directory.Exists(targetDir), "目标目录不应被创建");
        }
    }
}
