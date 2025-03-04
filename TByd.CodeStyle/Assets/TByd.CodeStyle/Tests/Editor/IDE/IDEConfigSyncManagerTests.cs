using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using TByd.CodeStyle.Editor.CodeCheck.IDE;

namespace TByd.CodeStyle.Tests.Editor.IDE
{
    public class IDEConfigSyncManagerTests
    {
        private string m_TestDirectory;
        private string m_ConfigDirectory;

        [SetUp]
        public void Setup()
        {
            // 创建测试目录
            m_TestDirectory = Path.Combine(Application.temporaryCachePath, "IDEConfigSyncTests");
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
        public void SynchronizeConfig_ValidConfig_ReturnsSuccess()
        {
            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsEmpty(result.Error);
        }

        [Test]
        public void SynchronizeConfig_InvalidDirectory_ReturnsError()
        {
            // Arrange
            Directory.Delete(m_ConfigDirectory, true);

            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.IsNotEmpty(result.Error);
        }

        [Test]
        public void SynchronizeConfig_DetectsChanges_UpdatesFiles()
        {
            // Arrange
            // 首次同步
            IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // 修改文件
            string settingsPath = Path.Combine(m_ConfigDirectory, ".vscode", "settings.json");
            string newContent = @"{
                ""editor.formatOnSave"": false,
                ""editor.formatOnType"": false
            }";
            File.WriteAllText(settingsPath, newContent);

            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.UpdatedFiles.Count > 0);
        }

        [Test]
        public void SynchronizeConfig_ConcurrentSync_HandlesLock()
        {
            // Arrange
            bool firstSuccess = false;
            bool secondSuccess = false;

            // Act
            // 启动两个并发的同步操作
            var thread1 = new Thread(() =>
            {
                var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);
                firstSuccess = result.Success;
            });

            var thread2 = new Thread(() =>
            {
                var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);
                secondSuccess = result.Success;
            });

            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            // Assert
            Assert.IsTrue(firstSuccess != secondSuccess); // 只有一个同步操作应该成功
        }

        [Test]
        public void ResolveConflicts_UseLocal_UpdatesConfig()
        {
            // Arrange
            var conflicts = new List<string> { ".vscode/settings.json" };
            string originalContent = File.ReadAllText(Path.Combine(m_ConfigDirectory, ".vscode", "settings.json"));

            // Act
            bool result = IDEConfigSyncManager.ResolveConflicts(IDEType.VSCode, conflicts, true);

            // Assert
            Assert.IsTrue(result);
            string currentContent = File.ReadAllText(Path.Combine(m_ConfigDirectory, ".vscode", "settings.json"));
            Assert.AreEqual(originalContent, currentContent);
        }

        [Test]
        public void ResolveConflicts_UseRemote_UpdatesFiles()
        {
            // Arrange
            var conflicts = new List<string> { ".vscode/settings.json" };
            string originalContent = File.ReadAllText(Path.Combine(m_ConfigDirectory, ".vscode", "settings.json"));

            // Act
            bool result = IDEConfigSyncManager.ResolveConflicts(IDEType.VSCode, conflicts, false);

            // Assert
            Assert.IsTrue(result);
            string currentContent = File.ReadAllText(Path.Combine(m_ConfigDirectory, ".vscode", "settings.json"));
            Assert.AreNotEqual(originalContent, currentContent);
        }

        [Test]
        public void SynchronizeConfig_LockTimeout_ReleasesLock()
        {
            // Arrange
            // 创建一个过期的锁文件
            string lockPath = Path.Combine(m_TestDirectory, "sync.lock");
            File.WriteAllText(lockPath, DateTime.Now.AddMinutes(-10).ToString());

            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsTrue(result.Success);
        }

        [Test]
        public void SynchronizeConfig_NoChanges_ReturnsSuccess()
        {
            // Arrange
            // 首次同步
            IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Act
            // 再次同步，没有任何更改
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.UpdatedFiles.Count);
        }

        [Test]
        public void SynchronizeConfig_NewFiles_DetectsAndSyncs()
        {
            // Arrange
            // 首次同步
            IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // 添加新文件
            string newFilePath = Path.Combine(m_ConfigDirectory, ".vscode", "launch.json");
            string newContent = @"{
                ""version"": ""0.2.0"",
                ""configurations"": []
            }";
            File.WriteAllText(newFilePath, newContent);

            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.UpdatedFiles.Contains("launch.json"));
        }

        [Test]
        public void SynchronizeConfig_DeletedFiles_HandlesGracefully()
        {
            // Arrange
            // 首次同步
            IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // 删除文件
            File.Delete(Path.Combine(m_ConfigDirectory, ".vscode", "settings.json"));

            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.UpdatedFiles.Count > 0);
        }
    }
}
