using System;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core.ErrorHandling;

namespace TByd.PackageCreator.Tests.Editor.Core.ErrorHandling
{
    /// <summary>
    /// 错误信息类测试
    /// </summary>
    public class ErrorInfoTests
    {
        [Test]
        public void Constructor_ShouldSetDefaultValues()
        {
            // 执行
            var errorInfo = new ErrorInfo();

            // 断言
            Assert.IsNotNull(errorInfo.ErrorId);
            Assert.IsNotNull(errorInfo.Timestamp);
            // 时间戳应该接近当前时间
            Assert.LessOrEqual((DateTime.Now - errorInfo.Timestamp).TotalSeconds, 1);
        }

        [Test]
        public void Create_ShouldSetPropertiesCorrectly()
        {
            // 安排
            var errorType = ErrorType.k_Validation;
            var message = "测试消息";
            var level = ErrorLevel.k_Warning;

            // 执行
            var errorInfo = ErrorInfo.Create(errorType, message, level);

            // 断言
            Assert.AreEqual(errorType, errorInfo.ErrorType);
            Assert.AreEqual(message, errorInfo.Message);
            Assert.AreEqual(level, errorInfo.Level);
            Assert.IsNotNull(errorInfo.ErrorId);
            Assert.IsNotNull(errorInfo.Timestamp);
        }

        [Test]
        public void FromException_ShouldCreateErrorInfoFromException()
        {
            // 安排
            Exception exception = new InvalidOperationException("测试异常");
            var errorType = ErrorType.k_Configuration;
            var level = ErrorLevel.k_Error;

            // 执行
            var errorInfo = ErrorInfo.FromException(exception, errorType, level);

            // 断言
            Assert.AreEqual(errorType, errorInfo.ErrorType);
            Assert.AreEqual("测试异常", errorInfo.Message);
            Assert.AreEqual(level, errorInfo.Level);
            Assert.AreEqual(exception, errorInfo.Exception);
        }

        [Test]
        public void FromException_WithDefaultValues_ShouldUseDefaultTypeAndLevel()
        {
            // 安排
            Exception exception = new InvalidOperationException("测试异常");

            // 执行
            var errorInfo = ErrorInfo.FromException(exception);

            // 断言
            Assert.AreEqual(ErrorType.k_Unknown, errorInfo.ErrorType);
            Assert.AreEqual(ErrorLevel.k_Error, errorInfo.Level);
            Assert.AreEqual(exception, errorInfo.Exception);
        }

        [Test]
        public void ToString_ShouldFormatCorrectly()
        {
            // 安排
            var errorInfo = new ErrorInfo
            {
                ErrorType = ErrorType.k_FileOperation,
                Message = "文件操作失败",
                Level = ErrorLevel.k_Error,
                Timestamp = new DateTime(2023, 1, 1, 12, 0, 0)
            };

            // 执行
            var result = errorInfo.ToString();

            // 断言
            Assert.IsTrue(result.Contains("[2023-01-01 12:00:00]"));
            Assert.IsTrue(result.Contains("[Error]"));
            Assert.IsTrue(result.Contains("[FileOperation]"));
            Assert.IsTrue(result.Contains("文件操作失败"));
        }

        [Test]
        public void ToDetailedString_WithoutException_ShouldFormatBasicInfo()
        {
            // 安排
            var errorInfo = new ErrorInfo
            {
                ErrorType = ErrorType.k_FileOperation,
                Message = "文件操作失败",
                Level = ErrorLevel.k_Error,
                Timestamp = new DateTime(2023, 1, 1, 12, 0, 0)
            };

            // 执行
            var result = errorInfo.ToDetailedString();

            // 断言
            Assert.IsTrue(result.Contains("[2023-01-01 12:00:00]"));
            Assert.IsTrue(result.Contains("[Error]"));
            Assert.IsTrue(result.Contains("[FileOperation]"));
            Assert.IsTrue(result.Contains("文件操作失败"));
            // 没有异常信息
            Assert.IsFalse(result.Contains("异常类型:"));
        }

        [Test]
        public void ToDetailedString_WithException_ShouldIncludeExceptionDetails()
        {
            // 安排
            Exception exception = new InvalidOperationException("测试异常");
            var errorInfo = new ErrorInfo
            {
                ErrorType = ErrorType.k_FileOperation,
                Message = "文件操作失败",
                Level = ErrorLevel.k_Error,
                Timestamp = new DateTime(2023, 1, 1, 12, 0, 0),
                Exception = exception
            };

            // 执行
            var result = errorInfo.ToDetailedString();

            // 断言
            Assert.IsTrue(result.Contains("异常类型: InvalidOperationException"));
            Assert.IsTrue(result.Contains("异常消息: 测试异常"));
            Assert.IsTrue(result.Contains("堆栈跟踪:"));
        }
    }
}
