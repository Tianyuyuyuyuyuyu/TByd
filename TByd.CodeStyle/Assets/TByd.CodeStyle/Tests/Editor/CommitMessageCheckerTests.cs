using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.Git.Commit;
using TByd.CodeStyle.Runtime.Git.Commit;

namespace TByd.CodeStyle.Tests.Editor
{
    /// <summary>
    /// 提交消息检查器测试类
    /// </summary>
    public class CommitMessageCheckerTests
    {
        [Test]
        public void ValidateCommitMessage_ValidMessage_ReturnsValid()
        {
            // 有效的提交消息
            string validMessage = "feat(core): 添加新功能";
            
            // 验证提交消息
            var result = CommitMessageChecker.ValidateCommitMessage(validMessage);
            
            // 验证结果
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.Errors.Count);
        }
        
        [Test]
        public void ValidateCommitMessage_InvalidType_ReturnsInvalid()
        {
            // 无效类型的提交消息
            string invalidMessage = "invalid(core): 添加新功能";
            
            // 验证提交消息
            var result = CommitMessageChecker.ValidateCommitMessage(invalidMessage);
            
            // 验证结果
            Assert.IsFalse(result.IsValid);
            Assert.Greater(result.Errors.Count, 0);
            Assert.IsTrue(result.Errors.Exists(e => e.Contains("类型")));
        }
        
        [Test]
        public void ValidateCommitMessage_MissingColon_ReturnsInvalid()
        {
            // 缺少冒号的提交消息
            string invalidMessage = "feat(core) 添加新功能";
            
            // 验证提交消息
            var result = CommitMessageChecker.ValidateCommitMessage(invalidMessage);
            
            // 验证结果
            Assert.IsFalse(result.IsValid);
            Assert.Greater(result.Errors.Count, 0);
            Assert.IsTrue(result.Errors.Exists(e => e.Contains("冒号")));
        }
        
        [Test]
        public void ValidateCommitMessage_EmptySubject_ReturnsInvalid()
        {
            // 主题为空的提交消息
            string invalidMessage = "feat(core): ";
            
            // 手动解析提交消息，用于调试
            var message = TByd.CodeStyle.Runtime.Git.Commit.CommitMessageParser.Parse(invalidMessage);
            Debug.Log($"[TByd.CodeStyle.Tests] 解析结果: Type={message.Type}, Scope={message.Scope}, Subject='{message.Subject}'");
            
            // 验证提交消息
            var result = CommitMessageChecker.ValidateCommitMessage(invalidMessage);
            
            // 输出验证结果，用于调试
            Debug.Log($"[TByd.CodeStyle.Tests] 验证结果: IsValid={result.IsValid}, Errors.Count={result.Errors.Count}");
            foreach (var error in result.Errors)
            {
                Debug.Log($"[TByd.CodeStyle.Tests] 错误: {error}");
            }
            
            // 验证结果
            Assert.IsFalse(result.IsValid);
            Assert.Greater(result.Errors.Count, 0);
            Assert.IsTrue(result.Errors.Exists(e => e.Contains("简短描述")));
        }
        
        [Test]
        public void FormatCommitMessage_ValidComponents_ReturnsFormattedMessage()
        {
            // 提交组件
            string type = "feat";
            string scope = "core";
            string subject = "添加新功能";
            string body = "详细描述新功能的实现";
            string footer = "BREAKING CHANGE: 此更改不向后兼容";
            bool isBreakingChange = true;
            
            // 格式化提交消息
            string message = CommitMessageChecker.FormatCommitMessage(
                type, scope, subject, body, footer, isBreakingChange);
            
            // 验证格式化结果
            Assert.IsTrue(message.StartsWith("feat(core)!: 添加新功能"));
            Assert.IsTrue(message.Contains(body));
            Assert.IsTrue(message.Contains(footer));
        }
        
        [Test]
        public void ParseCommitMessage_ValidMessage_ReturnsComponents()
        {
            // 有效的提交消息
            string message = "feat(core)!: 添加新功能\n\n详细描述新功能的实现\n\nBREAKING CHANGE: 此更改不向后兼容";
            
            // 解析提交消息
            bool success = CommitMessageChecker.ParseCommitMessage(
                message,
                out string type,
                out string scope,
                out string subject,
                out string body,
                out string footer,
                out bool isBreakingChange);
            
            // 验证解析结果
            Assert.IsTrue(success);
            Assert.AreEqual("feat", type);
            Assert.AreEqual("core", scope);
            Assert.AreEqual("添加新功能", subject);
            Assert.AreEqual("详细描述新功能的实现", body);
            Assert.AreEqual("BREAKING CHANGE: 此更改不向后兼容", footer);
            Assert.IsTrue(isBreakingChange);
        }
    }
} 