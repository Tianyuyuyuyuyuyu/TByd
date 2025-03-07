using System.Collections.Generic;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Core;
using TByd.PackageCreator.Tests.Editor;

namespace TByd.PackageCreator.Tests.Editor.Core.Models
{
    /// <summary>
    /// 验证结果测试类
    /// </summary>
    public class ValidationResultTests
    {
        [Test]
        public void ValidationMessage_Constructor_ShouldSetPropertiesCorrectly()
        {
            // 安排
            var message = "包名不能为空";
            var level = ValidationMessageLevel.k_Error;
            var field = "Name";

            // 执行
            var validationMessage = new ValidationMessage(message, level, field);

            // 断言
            Assert.AreEqual(message, validationMessage.Message);
            Assert.AreEqual(level, validationMessage.Level);
            Assert.AreEqual(field, validationMessage.Field);
        }

        [Test]
        public void ValidationResult_Constructor_ShouldInitializePropertiesCorrectly()
        {
            // 执行
            var result = new ValidationResult();

            // 断言
            Assert.IsNotNull(result.Messages);
            Assert.AreEqual(0, result.Messages.Count);
            Assert.IsFalse(result.HasErrors);
            Assert.IsFalse(result.HasWarnings);
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void ValidationResult_AddInfo_ShouldAddInfoMessage()
        {
            // 安排
            var result = new ValidationResult();
            var message = "这是一条信息";
            var field = "Info";

            // 执行
            result.AddInfo(message, field);

            // 断言
            Assert.AreEqual(1, result.Messages.Count);
            Assert.AreEqual(message, result.Messages[0].Message);
            Assert.AreEqual(ValidationMessageLevel.k_Info, result.Messages[0].Level);
            Assert.AreEqual(field, result.Messages[0].Field);
            Assert.IsFalse(result.HasErrors);
            Assert.IsFalse(result.HasWarnings);
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void ValidationResult_AddWarning_ShouldAddWarningMessage()
        {
            // 安排
            var result = new ValidationResult();
            var message = "这是一条警告";
            var field = "Warning";

            // 执行
            result.AddWarning(message, field);

            // 断言
            Assert.AreEqual(1, result.Messages.Count);
            Assert.AreEqual(message, result.Messages[0].Message);
            Assert.AreEqual(ValidationMessageLevel.k_Warning, result.Messages[0].Level);
            Assert.AreEqual(field, result.Messages[0].Field);
            Assert.IsFalse(result.HasErrors);
            Assert.IsTrue(result.HasWarnings);
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void ValidationResult_AddError_ShouldAddErrorMessage()
        {
            // 安排
            var result = new ValidationResult();
            var message = "这是一条错误";
            var field = "Error";

            // 执行
            result.AddError(message, field);

            // 断言
            Assert.AreEqual(1, result.Messages.Count);
            Assert.AreEqual(message, result.Messages[0].Message);
            Assert.AreEqual(ValidationMessageLevel.k_Error, result.Messages[0].Level);
            Assert.AreEqual(field, result.Messages[0].Field);
            Assert.IsTrue(result.HasErrors);
            Assert.IsFalse(result.HasWarnings);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void ValidationResult_Merge_ShouldMergeMessages()
        {
            // 安排
            var result1 = new ValidationResult();
            result1.AddInfo("信息1");
            result1.AddWarning("警告1");

            var result2 = new ValidationResult();
            result2.AddError("错误1");
            result2.AddInfo("信息2");

            // 执行
            result1.Merge(result2);

            // 断言
            Assert.AreEqual(4, result1.Messages.Count);
            Assert.IsTrue(result1.HasErrors);
            Assert.IsTrue(result1.HasWarnings);
            Assert.IsFalse(result1.IsValid);
        }

        [Test]
        public void ValidationResult_GetMessages_ShouldReturnMessagesWithSpecificLevel()
        {
            // 安排
            var result = new ValidationResult();
            result.AddInfo("信息1");
            result.AddInfo("信息2");
            result.AddWarning("警告1");
            result.AddError("错误1");

            // 执行
            var infoMessages = result.GetMessages(ValidationMessageLevel.k_Info);
            var warningMessages = result.GetMessages(ValidationMessageLevel.k_Warning);
            var errorMessages = result.GetMessages(ValidationMessageLevel.k_Error);

            // 断言
            Assert.AreEqual(2, infoMessages.Count);
            Assert.AreEqual(1, warningMessages.Count);
            Assert.AreEqual(1, errorMessages.Count);
        }

        [Test]
        public void ValidationResult_GetMessagesForField_ShouldReturnMessagesForSpecificField()
        {
            // 安排
            var result = new ValidationResult();
            result.AddInfo("信息1", "Field1");
            result.AddWarning("警告1", "Field1");
            result.AddError("错误1", "Field2");

            // 执行
            var field1Messages = result.GetMessagesForField("Field1");
            var field2Messages = result.GetMessagesForField("Field2");

            // 断言
            Assert.AreEqual(2, field1Messages.Count);
            Assert.AreEqual(1, field2Messages.Count);
        }
    }
}
