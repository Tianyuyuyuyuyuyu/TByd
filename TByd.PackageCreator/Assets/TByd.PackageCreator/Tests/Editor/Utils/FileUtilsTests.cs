using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core.ErrorHandling;
using TByd.PackageCreator.Editor.Utils;
using UnityEngine;
using UnityEngine.TestTools;

namespace TByd.PackageCreator.Tests.Editor.Utils
{
    /// <summary>
    /// FileUtils工具类的测试
    /// </summary>
    public class FileUtilsTests
    {
        private string _testDirectory;
        private string _testFile;
        private string _testContent;

        [SetUp]
        public void Setup()
        {
            // 创建测试目录和文件
            _testDirectory = Path.Combine(Application.temporaryCachePath, "FileUtilsTests");
            _testFile = Path.Combine(_testDirectory, "testfile.txt");
            _testContent = "测试内容\n第二行\n第三行";

            // 确保测试开始前目录不存在
            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // 测试结束后清理
            if (Directory.Exists(_testDirectory))
            {
                Directory.Delete(_testDirectory, true);
            }
        }

        [Test]
        public void EnsureDirectoryExists_CreatesDirectory_WhenNotExists()
        {
            // 执行
            bool result = FileUtils.EnsureDirectoryExists(_testDirectory);

            // 验证
            Assert.IsTrue(result, "应返回true表示目录创建成功");
            Assert.IsTrue(Directory.Exists(_testDirectory), "目录应该被创建");
        }

        [Test]
        public void EnsureDirectoryExists_ReturnsTrue_WhenDirectoryAlreadyExists()
        {
            // 准备
            Directory.CreateDirectory(_testDirectory);

            // 执行
            bool result = FileUtils.EnsureDirectoryExists(_testDirectory);

            // 验证
            Assert.IsTrue(result, "应返回true表示目录已存在");
            Assert.IsTrue(Directory.Exists(_testDirectory), "目录应该存在");
        }

        [Test]
        public void WriteTextFile_WritesContent_WhenFileDoesNotExist()
        {
            // 准备
            TestHelpers.ExpectLogMessage(LogType.Log, $"写入文件: {_testFile}");

            // 执行
            bool result = FileUtils.WriteTextFile(_testFile, _testContent);

            // 验证
            Assert.IsTrue(result, "应返回true表示写入成功");
            Assert.IsTrue(File.Exists(_testFile), "文件应该被创建");
            Assert.AreEqual(_testContent, File.ReadAllText(_testFile), "文件内容应该匹配");
        }

        [Test]
        public void WriteTextFile_OverwritesContent_WhenFileExists()
        {
            // 准备
            Directory.CreateDirectory(_testDirectory);
            File.WriteAllText(_testFile, "旧内容");
            TestHelpers.ExpectLogMessage(LogType.Log, $"写入文件: {_testFile}");

            // 执行
            bool result = FileUtils.WriteTextFile(_testFile, _testContent);

            // 验证
            Assert.IsTrue(result, "应返回true表示写入成功");
            Assert.AreEqual(_testContent, File.ReadAllText(_testFile), "文件内容应该被覆盖");
        }

        [Test]
        public void WriteTextFile_CreatesDirectory_WhenDirectoryDoesNotExist()
        {
            // 准备
            TestHelpers.ExpectLogMessage(LogType.Log, $"写入文件: {_testFile}");

            // 执行
            bool result = FileUtils.WriteTextFile(_testFile, _testContent);

            // 验证
            Assert.IsTrue(result, "应返回true表示写入成功");
            Assert.IsTrue(Directory.Exists(_testDirectory), "父目录应该被创建");
            Assert.IsTrue(File.Exists(_testFile), "文件应该被创建");
        }

        [Test]
        public void ReadTextFile_ReadsContent_WhenFileExists()
        {
            // 准备
            Directory.CreateDirectory(_testDirectory);
            File.WriteAllText(_testFile, _testContent);

            // 执行
            bool success = FileUtils.ReadTextFile(_testFile, out string result);

            // 验证
            Assert.IsTrue(success, "读取文件应成功返回true");
            Assert.AreEqual(_testContent, result, "读取的内容应该匹配写入的内容");
        }

        [Test]
        public void ReadTextFile_ReturnsFalse_WhenFileDoesNotExist()
        {
            // 准备
            TestHelpers.ExpectLogMessage(LogType.Warning, $"文件不存在: {_testFile}");

            // 执行
            bool success = FileUtils.ReadTextFile(_testFile, out string result);

            // 验证
            Assert.IsFalse(success, "文件不存在时应返回false");
            Assert.AreEqual(string.Empty, result, "文件不存在时内容应为空");
        }

        [Test]
        public void SafeDeleteFile_DeletesFile_WhenFileExists()
        {
            // 准备
            Directory.CreateDirectory(_testDirectory);
            File.WriteAllText(_testFile, _testContent);
            TestHelpers.ExpectLogMessage(LogType.Log, $"删除文件: {_testFile}");

            // 执行
            bool result = FileUtils.SafeDeleteFile(_testFile);

            // 验证
            Assert.IsTrue(result, "应返回true表示删除成功");
            Assert.IsFalse(File.Exists(_testFile), "文件应该被删除");
        }

        [Test]
        public void SafeDeleteFile_ReturnsTrue_WhenFileDoesNotExist()
        {
            // 准备 - 文件不存在时SafeDeleteFile不记录日志，所以不需要期望日志消息

            // 执行
            bool result = FileUtils.SafeDeleteFile(_testFile);

            // 验证
            Assert.IsTrue(result, "文件不存在时应返回true");
        }

        [Test]
        public void CopyFile_CopiesFile_WhenSourceFileExists()
        {
            // 准备
            string destinationFile = Path.Combine(_testDirectory, "destination.txt");
            Directory.CreateDirectory(_testDirectory);
            File.WriteAllText(_testFile, _testContent);
            TestHelpers.ExpectLogMessage(LogType.Log, $"复制文件: {_testFile} -> {destinationFile}");

            // 执行
            bool result = FileUtils.CopyFile(_testFile, destinationFile);

            // 验证
            Assert.IsTrue(result, "应返回true表示复制成功");
            Assert.IsTrue(File.Exists(destinationFile), "目标文件应该存在");
            Assert.AreEqual(_testContent, File.ReadAllText(destinationFile), "目标文件内容应该匹配源文件");
        }

        [Test]
        public void CopyFile_CreatesDestinationDirectory_WhenItDoesNotExist()
        {
            // 准备
            string subDirectory = Path.Combine(_testDirectory, "subdir");
            string destinationFile = Path.Combine(subDirectory, "destination.txt");
            Directory.CreateDirectory(_testDirectory);
            File.WriteAllText(_testFile, _testContent);
            TestHelpers.ExpectLogMessage(LogType.Log, $"复制文件: {_testFile} -> {destinationFile}");

            // 执行
            bool result = FileUtils.CopyFile(_testFile, destinationFile);

            // 验证
            Assert.IsTrue(result, "应返回true表示复制成功");
            Assert.IsTrue(Directory.Exists(subDirectory), "目标目录应该被创建");
            Assert.IsTrue(File.Exists(destinationFile), "目标文件应该存在");
        }

        [Test]
        public void CopyFile_ReturnsFalse_WhenSourceFileDoesNotExist()
        {
            // 准备
            string destinationFile = Path.Combine(_testDirectory, "destination.txt");
            TestHelpers.ExpectLogMessage(LogType.Warning, $"源文件不存在: {_testFile}");

            // 执行
            bool result = FileUtils.CopyFile(_testFile, destinationFile);

            // 验证
            Assert.IsFalse(result, "源文件不存在时应返回false");
            Assert.IsFalse(File.Exists(destinationFile), "目标文件不应该被创建");
        }

        [Test]
        public void SafeDeleteDirectory_DeletesDirectory_WhenDirectoryExists()
        {
            // 准备
            Directory.CreateDirectory(_testDirectory);
            TestHelpers.ExpectLogMessage(LogType.Log, $"删除目录: {_testDirectory}");

            // 执行
            bool result = FileUtils.SafeDeleteDirectory(_testDirectory);

            // 验证
            Assert.IsTrue(result, "应返回true表示删除成功");
            Assert.IsFalse(Directory.Exists(_testDirectory), "目录应该被删除");
        }

        [Test]
        public void SafeDeleteDirectory_DeletesDirectoryRecursively_WhenDirectoryContainsFiles()
        {
            // 准备
            Directory.CreateDirectory(_testDirectory);
            File.WriteAllText(_testFile, _testContent);
            TestHelpers.ExpectLogMessage(LogType.Log, $"删除目录: {_testDirectory}");

            // 执行
            bool result = FileUtils.SafeDeleteDirectory(_testDirectory);

            // 验证
            Assert.IsTrue(result, "应返回true表示删除成功");
            Assert.IsFalse(Directory.Exists(_testDirectory), "目录应该被删除");
        }

        [Test]
        public void SafeDeleteDirectory_ReturnsTrue_WhenDirectoryDoesNotExist()
        {
            // 准备 - 目录不存在时SafeDeleteDirectory不记录日志，所以不需要期望日志消息

            // 执行
            bool result = FileUtils.SafeDeleteDirectory(_testDirectory);

            // 验证
            Assert.IsTrue(result, "目录不存在时应返回true");
        }
    }
}
