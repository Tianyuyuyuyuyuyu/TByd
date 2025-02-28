using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TByd.CodeStyle.Runtime.Git;

namespace TByd.CodeStyle.Tests.Runtime
{
    /// <summary>
    /// Git仓库测试类
    /// </summary>
    public class GitRepositoryTests
    {
        // 测试Git目录
        private string m_TestGitDir;
        
        [SetUp]
        public void Setup()
        {
            // 创建测试Git目录
            m_TestGitDir = Path.Combine(Application.temporaryCachePath, "TestGit");
            Directory.CreateDirectory(m_TestGitDir);
            
            // 创建.git目录
            Directory.CreateDirectory(Path.Combine(m_TestGitDir, ".git"));
            
            // 创建hooks目录
            Directory.CreateDirectory(Path.Combine(m_TestGitDir, ".git", "hooks"));
        }
        
        [TearDown]
        public void TearDown()
        {
            // 删除测试Git目录
            if (Directory.Exists(m_TestGitDir))
            {
                Directory.Delete(m_TestGitDir, true);
            }
        }
        
        [Test]
        public void IsGitRepository_ValidRepository_ReturnsTrue()
        {
            // 检查是否是Git仓库
            bool isGitRepo = GitRepository.IsGitRepository(m_TestGitDir);
            
            // 验证结果
            Assert.IsTrue(isGitRepo);
        }
        
        [Test]
        public void IsGitRepository_InvalidRepository_ReturnsFalse()
        {
            // 创建非Git目录
            string nonGitDir = Path.Combine(Application.temporaryCachePath, "NonGit");
            Directory.CreateDirectory(nonGitDir);
            
            try
            {
                // 检查是否是Git仓库
                bool isGitRepo = GitRepository.IsGitRepository(nonGitDir);
                
                // 验证结果
                Assert.IsFalse(isGitRepo);
            }
            finally
            {
                // 删除非Git目录
                if (Directory.Exists(nonGitDir))
                {
                    Directory.Delete(nonGitDir, true);
                }
            }
        }
        
        [Test]
        public void GetGitDirectory_ValidRepository_ReturnsGitDir()
        {
            // 获取Git目录
            string gitDir = GitRepository.GetGitDirectory(m_TestGitDir);
            
            // 验证结果
            Assert.AreEqual(Path.Combine(m_TestGitDir, ".git"), gitDir);
        }
        
        [Test]
        public void GetGitDirectory_InvalidRepository_ReturnsEmpty()
        {
            // 创建非Git目录
            string nonGitDir = Path.Combine(Application.temporaryCachePath, "NonGit");
            Directory.CreateDirectory(nonGitDir);
            
            try
            {
                // 获取Git目录
                string gitDir = GitRepository.GetGitDirectory(nonGitDir);
                
                // 验证结果
                Assert.AreEqual(string.Empty, gitDir);
            }
            finally
            {
                // 删除非Git目录
                if (Directory.Exists(nonGitDir))
                {
                    Directory.Delete(nonGitDir, true);
                }
            }
        }
        
        [Test]
        public void GetHooksDirectory_ValidRepository_ReturnsHooksDir()
        {
            // 获取Hooks目录
            string hooksDir = GitRepository.GetHooksDirectory(m_TestGitDir);
            
            // 验证结果
            Assert.AreEqual(Path.Combine(m_TestGitDir, ".git", "hooks"), hooksDir);
        }
        
        [Test]
        public void GetHooksDirectory_InvalidRepository_ReturnsEmpty()
        {
            // 创建非Git目录
            string nonGitDir = Path.Combine(Application.temporaryCachePath, "NonGit");
            Directory.CreateDirectory(nonGitDir);
            
            try
            {
                // 获取Hooks目录
                string hooksDir = GitRepository.GetHooksDirectory(nonGitDir);
                
                // 验证结果
                Assert.AreEqual(string.Empty, hooksDir);
            }
            finally
            {
                // 删除非Git目录
                if (Directory.Exists(nonGitDir))
                {
                    Directory.Delete(nonGitDir, true);
                }
            }
        }
    }
} 