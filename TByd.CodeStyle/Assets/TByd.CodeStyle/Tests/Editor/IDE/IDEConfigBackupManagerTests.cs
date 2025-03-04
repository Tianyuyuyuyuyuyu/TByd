using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using TByd.CodeStyle.Editor.CodeCheck.IDE;

namespace TByd.CodeStyle.Tests.Editor.IDE
{
    public class IDEConfigBackupManagerTests
    {
        private string m_TestDirectory;
        private string m_ConfigDirectory;

        [SetUp]
        public void Setup()
        {
            // 创建测试目录
            m_TestDirectory = Path.Combine(Application.temporaryCachePath, "IDEConfigBackupTests");
            m_ConfigDirectory = Path.Combine(m_TestDirectory, "Config");
            Directory.CreateDirectory(m_ConfigDirectory);

            // 创建测试配置文件
            CreateTestConfigFiles();
        }

        [TearDown]
        public void TearDown()
        {
            // 清理测试目录
            if (Directory.Exists(m_TestDirectory))
            {
                Directory.Delete(m_TestDirectory, true);
            }
        }

        private void CreateTestConfigFiles()
        {
            // 创建.editorconfig
            string editorConfigContent = @"root = true
            [*]
            charset = utf-8
            end_of_line = lf
            indent_style = space
            indent_size = 4";
            File.WriteAllText(Path.Combine(m_ConfigDirectory, ".editorconfig"), editorConfigContent);

            // 创建VS Code配置
            string vscodeDir = Path.Combine(m_ConfigDirectory, ".vscode");
            Directory.CreateDirectory(vscodeDir);
            string settingsContent = @"{
                ""editor.formatOnSave"": true,
                ""editor.formatOnType"": true
            }";
            File.WriteAllText(Path.Combine(vscodeDir, "settings.json"), settingsContent);
        }

        [Test]
        public void CreateBackup_ValidConfig_ReturnsBackupId()
        {
            // Arrange
            string description = "Test Backup";

            // Act
            string backupId = IDEConfigBackupManager.CreateBackup(IDEType.VSCode, description);

            // Assert
            Assert.IsNotNull(backupId);
            Assert.IsNotEmpty(backupId);
        }

        [Test]
        public void CreateBackup_EmptyDescription_CreatesBackup()
        {
            // Act
            string backupId = IDEConfigBackupManager.CreateBackup(IDEType.VSCode);

            // Assert
            Assert.IsNotNull(backupId);
            var backups = IDEConfigBackupManager.GetBackups();
            Assert.IsTrue(backups.Any(b => b.Id == backupId));
        }

        [Test]
        public void GetBackups_NoBackups_ReturnsEmptyList()
        {
            // Act
            var backups = IDEConfigBackupManager.GetBackups();

            // Assert
            Assert.IsNotNull(backups);
            Assert.AreEqual(0, backups.Count);
        }

        [Test]
        public void GetBackups_HasBackups_ReturnsOrderedList()
        {
            // Arrange
            IDEConfigBackupManager.CreateBackup(IDEType.VSCode, "First");
            IDEConfigBackupManager.CreateBackup(IDEType.VSCode, "Second");

            // Act
            var backups = IDEConfigBackupManager.GetBackups();

            // Assert
            Assert.AreEqual(2, backups.Count);
            Assert.IsTrue(backups[0].Timestamp >= backups[1].Timestamp);
        }

        [Test]
        public void RestoreBackup_ValidBackup_ReturnsTrue()
        {
            // Arrange
            string backupId = IDEConfigBackupManager.CreateBackup(IDEType.VSCode, "Test");

            // 修改原始文件
            File.WriteAllText(Path.Combine(m_ConfigDirectory, ".editorconfig"), "modified content");

            // Act
            bool result = IDEConfigBackupManager.RestoreBackup(backupId);

            // Assert
            Assert.IsTrue(result);
            string restoredContent = File.ReadAllText(Path.Combine(m_ConfigDirectory, ".editorconfig"));
            Assert.IsTrue(restoredContent.Contains("root = true"));
        }

        [Test]
        public void RestoreBackup_InvalidBackupId_ReturnsFalse()
        {
            // Act
            bool result = IDEConfigBackupManager.RestoreBackup("invalid_id");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteBackup_ValidBackup_ReturnsTrue()
        {
            // Arrange
            string backupId = IDEConfigBackupManager.CreateBackup(IDEType.VSCode, "Test");

            // Act
            bool result = IDEConfigBackupManager.DeleteBackup(backupId);

            // Assert
            Assert.IsTrue(result);
            var backups = IDEConfigBackupManager.GetBackups();
            Assert.IsFalse(backups.Any(b => b.Id == backupId));
        }

        [Test]
        public void DeleteBackup_InvalidBackupId_ReturnsFalse()
        {
            // Act
            bool result = IDEConfigBackupManager.DeleteBackup("invalid_id");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CreateBackup_MultipleBackups_MaintainsOrder()
        {
            // Arrange
            string firstId = IDEConfigBackupManager.CreateBackup(IDEType.VSCode, "First");
            System.Threading.Thread.Sleep(1000); // 确保时间戳不同
            string secondId = IDEConfigBackupManager.CreateBackup(IDEType.VSCode, "Second");

            // Act
            var backups = IDEConfigBackupManager.GetBackups();

            // Assert
            Assert.AreEqual(2, backups.Count);
            Assert.AreEqual(secondId, backups[0].Id);
            Assert.AreEqual(firstId, backups[1].Id);
        }

        [Test]
        public void RestoreBackup_DeletedFiles_RecreatesFiles()
        {
            // Arrange
            string backupId = IDEConfigBackupManager.CreateBackup(IDEType.VSCode, "Test");

            // 删除原始文件
            File.Delete(Path.Combine(m_ConfigDirectory, ".editorconfig"));
            Directory.Delete(Path.Combine(m_ConfigDirectory, ".vscode"), true);

            // Act
            bool result = IDEConfigBackupManager.RestoreBackup(backupId);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(Path.Combine(m_ConfigDirectory, ".editorconfig")));
            Assert.IsTrue(Directory.Exists(Path.Combine(m_ConfigDirectory, ".vscode")));
        }

        [Test]
        public void CreateBackup_LargeFiles_HandlesCorrectly()
        {
            // Arrange
            string largeContent = new string('x', 1024 * 1024); // 1MB
            File.WriteAllText(Path.Combine(m_ConfigDirectory, "large_file.txt"), largeContent);

            // Act
            string backupId = IDEConfigBackupManager.CreateBackup(IDEType.VSCode, "Large File Test");

            // Assert
            Assert.IsNotNull(backupId);
            var backups = IDEConfigBackupManager.GetBackups();
            Assert.IsTrue(backups.Any(b => b.Id == backupId));
        }
    }
} 