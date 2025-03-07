using NUnit.Framework;
using TByd.PackageCreator.Editor.Core.Models;

namespace TByd.PackageCreator.Tests.Editor.Core.Models
{
    /// <summary>
    /// 模板选项测试类
    /// </summary>
    public class TemplateOptionTests
    {
        [Test]
        public void TemplateOption_Constructor_ShouldSetPropertiesCorrectly()
        {
            // 安排
            var key = "includeTests";
            var displayName = "包含测试";
            var description = "是否包含测试目录";
            var type = TemplateOptionType.Boolean;
            var defaultValue = "true";
            var isRequired = true;

            // 执行
            var option = new TemplateOption(key, displayName, description, type, defaultValue, isRequired);

            // 断言
            Assert.AreEqual(key, option.Key);
            Assert.AreEqual(displayName, option.DisplayName);
            Assert.AreEqual(description, option.Description);
            Assert.AreEqual(type, option.Type);
            Assert.AreEqual(defaultValue, option.DefaultValue);
            Assert.AreEqual(isRequired, option.IsRequired);
            Assert.IsNotNull(option.PossibleValues);
            Assert.AreEqual(0, option.PossibleValues.Count);
        }

        [Test]
        public void TemplateOption_AddPossibleValue_ShouldAddValueToList()
        {
            // 安排
            var option = new TemplateOption("theme", "主题", "选择主题", TemplateOptionType.Enum);
            var value = "dark";

            // 执行
            option.AddPossibleValue(value);

            // 断言
            Assert.AreEqual(1, option.PossibleValues.Count);
            Assert.AreEqual(value, option.PossibleValues[0]);
        }

        [Test]
        public void TemplateOption_ValidateValue_ShouldReturnValidForValidString()
        {
            // 安排
            var option = new TemplateOption("name", "名称", "包名称", TemplateOptionType.String);

            // 执行
            var result = option.ValidateValue("test-package");

            // 断言
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.Messages.Count);
        }

        [Test]
        public void TemplateOption_ValidateValue_ShouldFailForEmptyRequiredValue()
        {
            // 安排
            var option = new TemplateOption("name", "名称", "包名称", TemplateOptionType.String);

            // 执行
            var result = option.ValidateValue("");

            // 断言
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Messages.Count);
            Assert.AreEqual(ValidationMessageLevel.Error, result.Messages[0].Level);
        }

        [Test]
        public void TemplateOption_ValidateValue_ShouldPassForEmptyOptionalValue()
        {
            // 安排
            var option = new TemplateOption("description", "描述", "包描述", TemplateOptionType.String, "", false);

            // 执行
            var result = option.ValidateValue("");

            // 断言
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.Messages.Count);
        }

        [Test]
        public void TemplateOption_ValidateValue_ShouldFailForInvalidBoolean()
        {
            // 安排
            var option = new TemplateOption("includeTests", "包含测试", "是否包含测试", TemplateOptionType.Boolean);

            // 执行
            var result = option.ValidateValue("notABoolean");

            // 断言
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Messages.Count);
        }

        [Test]
        public void TemplateOption_ValidateValue_ShouldPassForValidBoolean()
        {
            // 安排
            var option = new TemplateOption("includeTests", "包含测试", "是否包含测试", TemplateOptionType.Boolean);

            // 执行
            var result = option.ValidateValue("true");

            // 断言
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.Messages.Count);
        }

        [Test]
        public void TemplateOption_ValidateValue_ShouldFailForInvalidInteger()
        {
            // 安排
            var option = new TemplateOption("count", "数量", "项目数量", TemplateOptionType.Integer);

            // 执行
            var result = option.ValidateValue("notAnInteger");

            // 断言
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Messages.Count);
        }

        [Test]
        public void TemplateOption_ValidateValue_ShouldPassForValidInteger()
        {
            // 安排
            var option = new TemplateOption("count", "数量", "项目数量", TemplateOptionType.Integer);

            // 执行
            var result = option.ValidateValue("123");

            // 断言
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.Messages.Count);
        }

        [Test]
        public void TemplateOption_ValidateValue_ShouldFailForInvalidEnum()
        {
            // 安排
            var option = new TemplateOption("theme", "主题", "选择主题", TemplateOptionType.Enum);
            option.AddPossibleValue("light");
            option.AddPossibleValue("dark");

            // 执行
            var result = option.ValidateValue("blue");

            // 断言
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(1, result.Messages.Count);
        }

        [Test]
        public void TemplateOption_ValidateValue_ShouldPassForValidEnum()
        {
            // 安排
            var option = new TemplateOption("theme", "主题", "选择主题", TemplateOptionType.Enum);
            option.AddPossibleValue("light");
            option.AddPossibleValue("dark");

            // 执行
            var result = option.ValidateValue("dark");

            // 断言
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0, result.Messages.Count);
        }
    }
}
