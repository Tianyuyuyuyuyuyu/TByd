using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TByd.CodeStyle.Editor.CodeCheck.IDE;

namespace TByd.CodeStyle.Tests.Editor.IDE
{
    public class IdeConfigBackupManagerTests
    {
        private string m_TestDirectory;
        private string m_ConfigDirectory;
        private string m_BackupDirectory;
        private string m_OriginalBackupRoot;

        [SetUp]
        public void Setup()
        {
            // 创建测试目录
            m_TestDirectory = Path.Combine(Application.temporaryCachePath, "IDEConfigBackupTests_" + Guid.NewGuid().ToString("N"));
            m_ConfigDirectory = Path.Combine(m_TestDirectory, "Config");
            m_BackupDirectory = Path.Combine(m_TestDirectory, "Backups");

            Directory.CreateDirectory(m_ConfigDirectory);
            Directory.CreateDirectory(m_BackupDirectory);

            // 创建测试配置文件
            CreateTestConfigFiles();

            // 保存原始备份根路径并设置测试用的备份路径
            m_OriginalBackupRoot = GetPrivateStaticMethodResult<string>("GetBackupRootPath");
            SetBackupRootPath(m_BackupDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            // 恢复原始备份根路径
            if (m_OriginalBackupRoot != null)
            {
                SetBackupRootPath(m_OriginalBackupRoot);
            }

            // 清理测试目录
            if (Directory.Exists(m_TestDirectory))
            {
                try
                {
                    Directory.Delete(m_TestDirectory, true);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"清理测试目录失败：{ex.Message}");
                }
            }
        }

        private void CreateTestConfigFiles()
        {
            // 创建.editorconfig
            var editorConfigContent = @"root = true
            [*]
            charset = utf-8
            end_of_line = lf
            indent_style = space
            indent_size = 4";
            File.WriteAllText(Path.Combine(m_ConfigDirectory, ".editorconfig"), editorConfigContent);

            // 创建VS Code配置
            var vscodeDir = Path.Combine(m_ConfigDirectory, ".vscode");
            Directory.CreateDirectory(vscodeDir);
            var settingsContent = @"{
                ""editor.formatOnSave"": true,
                ""editor.formatOnType"": true
            }";
            File.WriteAllText(Path.Combine(vscodeDir, "settings.json"), settingsContent);
        }

        // 使用反射设置备份根路径
        private void SetBackupRootPath(string path)
        {
            var methodInfo = typeof(IdeConfigBackupManager).GetMethod("GetBackupRootPath",
                BindingFlags.NonPublic | BindingFlags.Static);

            if (methodInfo != null)
            {
                var fieldInfo = typeof(IdeConfigBackupManager).GetField("s_BackupRootPathForTesting",
                    BindingFlags.NonPublic | BindingFlags.Static);

                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(null, path);
                }
            }
        }

        // 使用反射获取私有静态方法的结果
        private T GetPrivateStaticMethodResult<T>(string methodName, params object[] parameters)
        {
            var methodInfo = typeof(IdeConfigBackupManager).GetMethod(methodName,
                BindingFlags.NonPublic | BindingFlags.Static);

            if (methodInfo != null)
            {
                return (T)methodInfo.Invoke(null, parameters);
            }

            return default(T);
        }

        [Test]
        public void CreateBackup_ValidConfig_ReturnsBackupId()
        {
            // Arrange
            var description = "Test Backup";

            // Act
            var backupId = IdeConfigBackupManager.CreateBackup(IdeType.k_VSCode, description);

            // Assert
            Assert.IsNotNull(backupId);
            Assert.IsNotEmpty(backupId);
        }

        [Test]
        public void CreateBackup_EmptyDescription_CreatesBackup()
        {
            // Act
            var backupId = IdeConfigBackupManager.CreateBackup(IdeType.k_VSCode);

            // Assert
            Assert.IsNotNull(backupId);
            var backups = IdeConfigBackupManager.GetBackups();
            Assert.IsTrue(backups.Any(b => b.id == backupId));
        }

        [Test]
        public void GetBackups_NoBackups_ReturnsEmptyList()
        {
            // 确保没有备份文件
            if (File.Exists(Path.Combine(m_BackupDirectory, "backup_config.json")))
            {
                File.Delete(Path.Combine(m_BackupDirectory, "backup_config.json"));
            }

            // Act
            var backups = IdeConfigBackupManager.GetBackups();

            // Assert
            Assert.IsNotNull(backups);
            Assert.AreEqual(0, backups.Count);
        }

        [Test]
        public void GetBackups_HasBackups_ReturnsOrderedList()
        {
            // 确保没有备份文件
            if (File.Exists(Path.Combine(m_BackupDirectory, "backup_config.json")))
            {
                File.Delete(Path.Combine(m_BackupDirectory, "backup_config.json"));
            }

            // Arrange - 创建两个备份
            IdeConfigBackupManager.CreateBackup(IdeType.k_VSCode, "First");
            System.Threading.Thread.Sleep(100); // 确保时间戳不同
            IdeConfigBackupManager.CreateBackup(IdeType.k_VSCode, "Second");

            // Act
            var backups = IdeConfigBackupManager.GetBackups();

            // Assert
            Assert.AreEqual(2, backups.Count);
            Assert.IsTrue(backups[0].timestamp >= backups[1].timestamp);
        }

        [Test]
        public void RestoreBackup_ValidBackup_ReturnsTrue()
        {
            // 确保正确的配置路径被使用
            var mockPathMethod = typeof(IdeConfigBackupManager).GetMethod("GetConfigPath",
                BindingFlags.NonPublic | BindingFlags.Static);

            if (mockPathMethod == null)
            {
                return;
            }

            // 创建备份
            var backupId = IdeConfigBackupManager.CreateBackup(IdeType.k_VSCode, "Test");

            // 修改原始文件
            File.WriteAllText(Path.Combine(m_ConfigDirectory, ".editorconfig"), "modified content");

            // Act
            var result = IdeConfigBackupManager.RestoreBackup(backupId);

            // Assert
            Assert.IsTrue(result);
            var restoredContent = File.ReadAllText(Path.Combine(m_ConfigDirectory, ".editorconfig"));
            Assert.IsTrue(restoredContent.Contains("root = true"));
        }

        [Test]
        public void RestoreBackup_InvalidBackupId_ReturnsFalse()
        {
            // 预期错误日志
            LogAssert.Expect(LogType.Error, "[TByd.CodeStyle] 未找到备份: invalid_id");

            // Act
            var result = IdeConfigBackupManager.RestoreBackup("invalid_id");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteBackup_ValidBackup_ReturnsTrue()
        {
            // Arrange
            var backupId = IdeConfigBackupManager.CreateBackup(IdeType.k_VSCode, "Test");

            // Act
            var result = IdeConfigBackupManager.DeleteBackup(backupId);

            // Assert
            Assert.IsTrue(result);
            var backups = IdeConfigBackupManager.GetBackups();
            Assert.IsFalse(backups.Any(b => b.id == backupId));
        }

        [Test]
        public void DeleteBackup_InvalidBackupId_ReturnsFalse()
        {
            // 预期错误日志
            LogAssert.Expect(LogType.Error, "[TByd.CodeStyle] 未找到备份: invalid_id");

            // Act
            var result = IdeConfigBackupManager.DeleteBackup("invalid_id");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CreateBackup_MultipleBackups_MaintainsOrder()
        {
            // 确保没有备份文件
            if (File.Exists(Path.Combine(m_BackupDirectory, "backup_config.json")))
            {
                File.Delete(Path.Combine(m_BackupDirectory, "backup_config.json"));
            }

            // Arrange
            var firstId = IdeConfigBackupManager.CreateBackup(IdeType.k_VSCode, "First");
            System.Threading.Thread.Sleep(100); // 确保时间戳不同
            var secondId = IdeConfigBackupManager.CreateBackup(IdeType.k_VSCode, "Second");

            // Act
            var backups = IdeConfigBackupManager.GetBackups();

            // Assert
            Assert.AreEqual(2, backups.Count);
            Assert.AreEqual(secondId, backups[0].id);
            Assert.AreEqual(firstId, backups[1].id);
        }

        [Test]
        public void RestoreBackup_DeletedFiles_RecreatesFiles()
        {
            // Arrange
            var backupId = IdeConfigBackupManager.CreateBackup(IdeType.k_VSCode, "Test");

            // 删除原始文件
            File.Delete(Path.Combine(m_ConfigDirectory, ".editorconfig"));
            if (Directory.Exists(Path.Combine(m_ConfigDirectory, ".vscode")))
            {
                Directory.Delete(Path.Combine(m_ConfigDirectory, ".vscode"), true);
            }

            // Act
            var result = IdeConfigBackupManager.RestoreBackup(backupId);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(Path.Combine(m_ConfigDirectory, ".editorconfig")));
            Assert.IsTrue(Directory.Exists(Path.Combine(m_ConfigDirectory, ".vscode")));
        }

        [Test]
        public void CreateBackup_LargeFiles_HandlesCorrectly()
        {
            // Arrange
            var largePath = Path.Combine(m_ConfigDirectory, "large_file.txt");
            var largeData = new byte[1024 * 1024]; // 1MB
            File.WriteAllBytes(largePath, largeData);

            // Act
            var backupId = IdeConfigBackupManager.CreateBackup(IdeType.k_VSCode, "Large File Test");

            // Assert
            Assert.IsNotNull(backupId);

            // 验证备份目录中有大文件
            var backupFiles = Directory.GetFiles(Path.Combine(m_BackupDirectory, backupId), "*", SearchOption.AllDirectories);
            Assert.IsTrue(backupFiles.Length > 0);

            // 检查有大文件被备份
            var hasLargeFile = backupFiles.Any(f => new FileInfo(f).Length > 1024 * 1024 * 0.9); // 允许有些差异
            Assert.IsTrue(hasLargeFile);
        }
    }
}
