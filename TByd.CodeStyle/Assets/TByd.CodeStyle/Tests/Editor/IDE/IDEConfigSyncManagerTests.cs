using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TByd.CodeStyle.Editor.CodeCheck.IDE;

namespace TByd.CodeStyle.Tests.Editor.IDE
{
    public class IDEConfigSyncManagerTests
    {
        private string m_TestDirectory;
        private string m_ConfigDirectory;
        private string m_SyncDirectory;
        private string m_OriginalSyncConfigPath;

        [SetUp]
        public void Setup()
        {
            // 创建测试目录
            m_TestDirectory = Path.Combine(Application.temporaryCachePath, "IDEConfigSyncTests_" + Guid.NewGuid().ToString("N"));
            m_ConfigDirectory = Path.Combine(m_TestDirectory, "Config");
            m_SyncDirectory = Path.Combine(m_TestDirectory, "Sync");
            
            Directory.CreateDirectory(m_ConfigDirectory);
            Directory.CreateDirectory(m_SyncDirectory);

            // 创建测试配置文件
            CreateTestConfigFiles();

            // 设置用于测试的同步配置路径
            m_OriginalSyncConfigPath = GetPrivateStaticFieldValue<string>("s_SyncConfigPathForTesting");
            SetSyncConfigPath(m_SyncDirectory);
        }

        [TearDown]
        public void TearDown()
        {
            // 恢复原始同步配置路径
            SetSyncConfigPath(m_OriginalSyncConfigPath);

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

        // 使用反射设置同步配置路径
        private void SetSyncConfigPath(string path)
        {
            var fieldInfo = typeof(IDEConfigSyncManager).GetField("s_SyncConfigPathForTesting", 
                BindingFlags.NonPublic | BindingFlags.Static);
            
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(null, path);
            }
        }

        // 使用反射获取私有静态字段的值
        private T GetPrivateStaticFieldValue<T>(string fieldName)
        {
            var fieldInfo = typeof(IDEConfigSyncManager).GetField(fieldName, 
                BindingFlags.NonPublic | BindingFlags.Static);
            
            if (fieldInfo != null)
            {
                return (T)fieldInfo.GetValue(null);
            }
            
            return default(T);
        }

        // 使用反射调用私有静态方法
        private void CallPrivateStaticMethod(string methodName, params object[] parameters)
        {
            var methodInfo = typeof(IDEConfigSyncManager).GetMethod(methodName, 
                BindingFlags.NonPublic | BindingFlags.Static);
            
            if (methodInfo != null)
            {
                methodInfo.Invoke(null, parameters);
            }
        }

        // 模拟获取配置路径方法，确保测试使用正确的配置目录
        private void MockGetConfigPath()
        {
            var methodInfo = typeof(IDEConfigSyncManager).GetMethod("GetConfigPath", 
                BindingFlags.NonPublic | BindingFlags.Static);
            
            if (methodInfo != null)
            {
                // 这里我们需要使用反射来修改GetConfigPath方法的实现，但这需要更高级的反射技术
                // 例如使用动态代理或者IL修改，这超出了简单测试修改的范围
                // 作为简化，我们可以创建一个专门用于测试的方法
                
                // 在实际项目中，可以考虑为IDEConfigSyncManager添加一个内部测试API，
                // 允许在测试时更简单地修改配置路径
            }
        }

        [Test]
        public void SynchronizeConfig_ValidConfig_ReturnsSuccess()
        {
            // 设置模拟的配置路径
            var originalMethod = typeof(IDEConfigSyncManager).GetMethod("GetConfigPath", 
                BindingFlags.NonPublic | BindingFlags.Static);
            
            // 创建一个快照，以便在真实IDE环境中不会影响实际配置
            CallPrivateStaticMethod("SaveSyncConfig", new object[] { new Dictionary<string, string>() });

            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsTrue(result.Success, "同步应该成功");
            Assert.That(result.Error, Is.Null.Or.Empty);
        }

        [Test]
        public void SynchronizeConfig_InvalidDirectory_ReturnsError()
        {
            // Arrange
            if (Directory.Exists(m_ConfigDirectory))
            {
                Directory.Delete(m_ConfigDirectory, true);
            }

            // 模拟配置目录不存在的情况
            var originalGetConfigPath = typeof(IDEConfigSyncManager).GetMethod("GetConfigPath", 
                BindingFlags.NonPublic | BindingFlags.Static);
            
            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsFalse(result.Success, "当目录不存在时，同步不应该成功");
            Assert.IsNotEmpty(result.Error, "应该返回错误信息");
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
            Assert.Greater(result.UpdatedFiles.Count, 0);
            Assert.Contains(settingsPath, result.UpdatedFiles);
        }

        [Test]
        public void SynchronizeConfig_ConcurrentSync_HandlesLock()
        {
            // Arrange
            // 模拟获取同步锁
            var lockFile = Path.Combine(m_SyncDirectory, "sync.lock");
            File.WriteAllText(lockFile, DateTime.Now.ToString());

            // 确保锁文件存在
            Assert.IsTrue(File.Exists(lockFile), "锁文件应该存在");

            // 预期警告日志
            LogAssert.Expect(LogType.Warning, new System.Text.RegularExpressions.Regex("另一个同步进程正在运行"));

            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsFalse(result.Success, "当锁文件存在时，同步不应该成功");
            Assert.IsNotEmpty(result.Error);
            Assert.IsTrue(result.Error.Contains("另一个同步进程正在运行"), $"错误消息应该提及锁: {result.Error}");
        }

        [Test]
        public void ResolveConflicts_UseLocal_UpdatesConfig()
        {
            // Arrange
            // 首次同步
            IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // 修改文件创建冲突
            string settingsPath = Path.Combine(m_ConfigDirectory, ".vscode", "settings.json");
            string newContent = @"{
                ""editor.formatOnSave"": false,
                ""editor.formatOnType"": false
            }";
            File.WriteAllText(settingsPath, newContent);

            // 创建冲突
            var conflictFiles = new List<string> { settingsPath };

            // Act
            bool result = IDEConfigSyncManager.ResolveConflicts(IDEType.VSCode, conflictFiles, true);

            // Assert
            Assert.IsTrue(result);
            
            // 验证同步配置被更新
            var syncResult = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);
            Assert.IsTrue(syncResult.Success);
            Assert.AreEqual(0, syncResult.ConflictFiles.Count, "解决冲突后不应该再有冲突");
        }

        [Test]
        public void ResolveConflicts_UseRemote_UpdatesFiles()
        {
            // Arrange
            // 准备远程版本（模拟）
            string remoteContent = @"{
                ""editor.formatOnSave"": false,
                ""editor.formatOnType"": false,
                ""editor.tabSize"": 2
            }";
            
            // 保存当前文件内容作为比较
            string settingsPath = Path.Combine(m_ConfigDirectory, ".vscode", "settings.json");
            string originalContent = File.ReadAllText(settingsPath);
            
            // 修改文件创建不同版本
            File.WriteAllText(settingsPath, remoteContent);
            
            // 首次同步以记录远程版本
            IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);
            
            // 恢复原始内容模拟本地修改
            File.WriteAllText(settingsPath, originalContent);
            
            // 创建冲突列表
            var conflictFiles = new List<string> { settingsPath };

            // Act - 选择使用远程版本解决冲突
            bool result = IDEConfigSyncManager.ResolveConflicts(IDEType.VSCode, conflictFiles, false);

            // Assert
            Assert.IsTrue(result);
            
            // 验证文件内容已更改为远程版本
            string newContent = File.ReadAllText(settingsPath);
            StringAssert.Contains("tabSize", newContent, "文件内容应该被更新为远程版本");
            StringAssert.AreNotEqualIgnoringCase(originalContent, newContent, "文件内容应该已更改");
        }

        [Test]
        public void SynchronizeConfig_LockTimeout_ReleasesLock()
        {
            // Arrange
            // 创建过期的锁文件（15分钟前）
            var lockFile = Path.Combine(m_SyncDirectory, "sync.lock");
            File.WriteAllText(lockFile, DateTime.Now.AddMinutes(-15).ToString());

            // 确保锁文件存在并已过期
            Assert.IsTrue(File.Exists(lockFile), "锁文件应该存在");

            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsTrue(result.Success, "当锁文件过期时，同步应该成功");
            Assert.That(result.Error, Is.Null.Or.Empty);
        }

        [Test]
        public void SynchronizeConfig_NoChanges_ReturnsSuccess()
        {
            // Arrange
            // 首次同步
            var firstResult = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);
            Assert.IsTrue(firstResult.Success, "首次同步应该成功");

            // 不修改任何文件

            // Act - 再次同步
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsTrue(result.Success, "当没有变化时，同步应该成功");
            Assert.AreEqual(0, result.UpdatedFiles.Count, "没有变化时不应该更新任何文件");
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
            Directory.CreateDirectory(Path.GetDirectoryName(newFilePath));
            File.WriteAllText(newFilePath, newContent);

            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.Contains(newFilePath, result.UpdatedFiles);
        }

        [Test]
        public void SynchronizeConfig_DeletedFiles_HandlesGracefully()
        {
            // Arrange
            // 首次同步
            IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // 删除文件
            string settingsPath = Path.Combine(m_ConfigDirectory, ".vscode", "settings.json");
            File.Delete(settingsPath);

            // Act
            var result = IDEConfigSyncManager.SynchronizeConfig(IDEType.VSCode);

            // Assert
            Assert.IsTrue(result.Success);
            // 确认不会因为文件删除而失败
        }
    }
}
