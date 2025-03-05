using System.IO;
using NUnit.Framework;
using UnityEngine;
using TByd.CodeStyle.Editor.Git;
using TByd.CodeStyle.Runtime.Git;

namespace TByd.CodeStyle.Tests.Editor
{
    /// <summary>
    /// Git钩子监控器测试类
    /// </summary>
    public class GitHookMonitorTests
    {
        // 测试Git钩子目录
        private string m_TestGitHooksDir;

        // 测试Git钩子文件
        private string m_TestCommitMsgHook;
        private string m_TestPreCommitHook;

        [SetUp]
        public void Setup()
        {
            // 创建测试Git钩子目录
            m_TestGitHooksDir = Path.Combine(Application.temporaryCachePath, "TestGitHooks");
            Directory.CreateDirectory(m_TestGitHooksDir);

            // 设置测试Git钩子文件路径
            m_TestCommitMsgHook = Path.Combine(m_TestGitHooksDir, "commit-msg");
            m_TestPreCommitHook = Path.Combine(m_TestGitHooksDir, "pre-commit");
        }

        [TearDown]
        public void TearDown()
        {
            // 删除测试Git钩子目录
            if (Directory.Exists(m_TestGitHooksDir))
            {
                Directory.Delete(m_TestGitHooksDir, true);
            }
        }

        [Test]
        public void GetHookPath_ValidType_ReturnsCorrectPath()
        {
            // 获取钩子路径
            var commitMsgPath = GitHookMonitor.GetHookPath(GitHookType.CommitMsg, m_TestGitHooksDir);
            var preCommitPath = GitHookMonitor.GetHookPath(GitHookType.PreCommit, m_TestGitHooksDir);

            // 验证路径
            Assert.AreEqual(m_TestCommitMsgHook, commitMsgPath);
            Assert.AreEqual(m_TestPreCommitHook, preCommitPath);
        }

        [Test]
        public void IsHookInstalled_HookNotExists_ReturnsFalse()
        {
            // 检查钩子是否已安装
            var isCommitMsgInstalled = GitHookMonitor.IsHookInstalled(GitHookType.CommitMsg, m_TestGitHooksDir);
            var isPreCommitInstalled = GitHookMonitor.IsHookInstalled(GitHookType.PreCommit, m_TestGitHooksDir);

            // 验证结果
            Assert.IsFalse(isCommitMsgInstalled);
            Assert.IsFalse(isPreCommitInstalled);
        }

        [Test]
        public void IsHookInstalled_HookExists_ReturnsTrue()
        {
            // 创建测试钩子文件
            File.WriteAllText(m_TestCommitMsgHook, "#!/bin/sh\n# TByd.CodeStyle commit-msg hook");

            // 检查钩子是否已安装
            var isCommitMsgInstalled = GitHookMonitor.IsHookInstalled(GitHookType.CommitMsg, m_TestGitHooksDir);
            var isPreCommitInstalled = GitHookMonitor.IsHookInstalled(GitHookType.PreCommit, m_TestGitHooksDir);

            // 验证结果
            Assert.IsTrue(isCommitMsgInstalled);
            Assert.IsFalse(isPreCommitInstalled);
        }

        [Test]
        public void InstallHook_HookNotExists_InstallsHook()
        {
            // 安装钩子
            var result = GitHookMonitor.InstallHook(GitHookType.CommitMsg, m_TestGitHooksDir);

            // 验证结果
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(m_TestCommitMsgHook));

            // 验证钩子内容
            var hookContent = File.ReadAllText(m_TestCommitMsgHook);
            Assert.IsTrue(hookContent.Contains("TByd.CodeStyle"));
        }

        [Test]
        public void UninstallHook_HookExists_UninstallsHook()
        {
            // 创建测试钩子文件
            File.WriteAllText(m_TestCommitMsgHook, "#!/bin/sh\n# TByd.CodeStyle commit-msg hook");

            // 卸载钩子
            var result = GitHookMonitor.UninstallHook(GitHookType.CommitMsg, m_TestGitHooksDir);

            // 验证结果
            Assert.IsTrue(result);
            Assert.IsFalse(File.Exists(m_TestCommitMsgHook));
        }
    }
}
