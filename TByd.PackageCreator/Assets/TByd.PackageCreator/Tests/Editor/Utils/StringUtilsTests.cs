using System.Collections.Generic;
using NUnit.Framework;
using TByd.PackageCreator.Editor.Utils;
using UnityEngine;

namespace TByd.PackageCreator.Tests.Editor.Utils
{
    /// <summary>
    /// StringUtils工具类的测试
    /// </summary>
    public class StringUtilsTests
    {
        [Test]
        public void ToCamelCase_ConvertsString_ToCorrectFormat()
        {
            // 测试各种情况
            Assert.AreEqual("testString", StringUtils.ToCamelCase("TestString"), "Pascal case转换");
            Assert.AreEqual("testString", StringUtils.ToCamelCase("test_string"), "Snake case转换");
            Assert.AreEqual("testString", StringUtils.ToCamelCase("test-string"), "Kebab case转换");
            Assert.AreEqual("testString", StringUtils.ToCamelCase("test string"), "空格分隔转换");
            Assert.AreEqual("testString", StringUtils.ToCamelCase("TEST_STRING"), "全大写转换");
            Assert.AreEqual("test", StringUtils.ToCamelCase("Test"), "单词首字母小写");
            Assert.AreEqual("t", StringUtils.ToCamelCase("T"), "单个字母");
            Assert.AreEqual("", StringUtils.ToCamelCase(""), "空字符串");
            Assert.AreEqual(null, StringUtils.ToCamelCase(null), "null值");
        }

        [Test]
        public void ToPascalCase_ConvertsString_ToCorrectFormat()
        {
            // 测试各种情况
            Assert.AreEqual("TestString", StringUtils.ToPascalCase("testString"), "Camel case转换");
            Assert.AreEqual("TestString", StringUtils.ToPascalCase("test_string"), "Snake case转换");
            Assert.AreEqual("TestString", StringUtils.ToPascalCase("test-string"), "Kebab case转换");
            Assert.AreEqual("TestString", StringUtils.ToPascalCase("test string"), "空格分隔转换");
            Assert.AreEqual("TestString", StringUtils.ToPascalCase("TEST_STRING"), "全大写转换");
            Assert.AreEqual("Test", StringUtils.ToPascalCase("test"), "单词首字母大写");
            Assert.AreEqual("T", StringUtils.ToPascalCase("t"), "单个字母");
            Assert.AreEqual("", StringUtils.ToPascalCase(""), "空字符串");
            Assert.AreEqual(null, StringUtils.ToPascalCase(null), "null值");
        }

        [Test]
        public void ToSnakeCase_ConvertsString_ToCorrectFormat()
        {
            // 测试各种情况
            Assert.AreEqual("test_string", StringUtils.ToSnakeCase("testString"), "Camel case转换");
            Assert.AreEqual("test_string", StringUtils.ToSnakeCase("TestString"), "Pascal case转换");
            Assert.AreEqual("test_string", StringUtils.ToSnakeCase("test-string"), "Kebab case转换");
            Assert.AreEqual("test_string", StringUtils.ToSnakeCase("test string"), "空格分隔转换");
            Assert.AreEqual("test_string", StringUtils.ToSnakeCase("TEST_STRING"), "全大写转换");
            Assert.AreEqual("test", StringUtils.ToSnakeCase("Test"), "单词");
            Assert.AreEqual("t", StringUtils.ToSnakeCase("T"), "单个字母");
            Assert.AreEqual("", StringUtils.ToSnakeCase(""), "空字符串");
            Assert.AreEqual(null, StringUtils.ToSnakeCase(null), "null值");
        }

        [Test]
        public void ToKebabCase_ConvertsString_ToCorrectFormat()
        {
            // 测试各种情况
            Assert.AreEqual("test-string", StringUtils.ToKebabCase("testString"), "Camel case转换");
            Assert.AreEqual("test-string", StringUtils.ToKebabCase("TestString"), "Pascal case转换");
            Assert.AreEqual("test-string", StringUtils.ToKebabCase("test_string"), "Snake case转换");
            Assert.AreEqual("test-string", StringUtils.ToKebabCase("test string"), "空格分隔转换");
            Assert.AreEqual("test-string", StringUtils.ToKebabCase("TEST_STRING"), "全大写转换");
            Assert.AreEqual("test", StringUtils.ToKebabCase("Test"), "单词");
            Assert.AreEqual("t", StringUtils.ToKebabCase("T"), "单个字母");
            Assert.AreEqual("", StringUtils.ToKebabCase(""), "空字符串");
            Assert.AreEqual(null, StringUtils.ToKebabCase(null), "null值");
        }

        [Test]
        public void Truncate_LimitsString_ToSpecifiedLength()
        {
            // 测试基本功能：截断超出长度的字符串并添加省略号
            Assert.AreEqual("测试...", StringUtils.Truncate("测试文本示例", 5));
            Assert.AreEqual("abcd...", StringUtils.Truncate("abcdefghijk", 7));

            // 测试自定义省略号
            Assert.AreEqual("测试文***", StringUtils.Truncate("测试文本示例", 6, "***"));
            Assert.AreEqual("abc---", StringUtils.Truncate("abcdefg", 6, "---"));

            // 测试边界：刚好等于长度的字符串
            Assert.AreEqual("abcde", StringUtils.Truncate("abcde", 5));

            // 测试短于最大长度的字符串不应被截断
            Assert.AreEqual("abc", StringUtils.Truncate("abc", 5));

            // 测试包含多字节字符且长度小于最大长度不会被截断
            Assert.AreEqual("测试", StringUtils.Truncate("测试", 5));

            // 空值处理
            Assert.AreEqual("", StringUtils.Truncate("", 5));
            Assert.AreEqual(null, StringUtils.Truncate(null, 5));
        }

        [Test]
        public void Truncate_HandlesEdgeCases_Correctly()
        {
            // 测试特殊值：maxLength为0或负数
            Assert.AreEqual("", StringUtils.Truncate("任何文本", 0));
            Assert.AreEqual("", StringUtils.Truncate("任何文本", -1));

            // 测试maxLength小于省略号长度的情况
            Assert.AreEqual(".", StringUtils.Truncate("任何文本", 1, "..."));
            Assert.AreEqual("..", StringUtils.Truncate("任何文本", 2, "..."));

            // 测试maxLength等于省略号长度的情况
            Assert.AreEqual("...", StringUtils.Truncate("任何文本", 3, "..."));

            // 测试不同类型的混合字符
            Assert.AreEqual("你好a...", StringUtils.Truncate("你好abc123", 6, "..."));
            Assert.AreEqual("こん...", StringUtils.Truncate("こんにちは", 5, "..."));
            Assert.AreEqual("Привет...", StringUtils.Truncate("Привет мир", 9, "..."));

            // 测试空省略号
            Assert.AreEqual("测试文", StringUtils.Truncate("测试文本", 3, ""));

            // 测试极长字符串
            string longString = new string('a', 1000);
            string expected = longString.Substring(0, 97) + "...";
            Assert.AreEqual(expected, StringUtils.Truncate(longString, 100));

            // 测试省略号自身超长
            string longEllipsis = new string('-', 10);
            Assert.AreEqual("abcdefg", StringUtils.Truncate("abcdefg", 11, longEllipsis));
            Assert.AreEqual("abcdefg", StringUtils.Truncate("abcdefg", 10, longEllipsis));

            // 测试省略号为null
            Assert.AreEqual("abcdefg", StringUtils.Truncate("abcdefg", 10, null));
        }

        [Test]
        public void ReplaceParameters_SubstitutesVariables_InTemplateString()
        {
            // 准备
            var template = "您好，{name}。今天是{date}，天气{weather}。";
            var parameters = new Dictionary<string, string>
            {
                { "name", "张三" },
                { "date", "2023年10月1日" },
                { "weather", "晴朗" }
            };

            // 执行
            var result = StringUtils.ReplaceParameters(template, parameters);

            // 验证
            Assert.AreEqual("您好，张三。今天是2023年10月1日，天气晴朗。", result, "应正确替换所有参数");
        }

        [Test]
        public void ReplaceParameters_HandlesNonExistentKeys()
        {
            // 准备
            var template = "您好，{name}。今天天气{weather}。";
            var parameters = new Dictionary<string, string>
            {
                { "name", "张三" }
                // 缺少weather参数
            };

            // 执行
            var result = StringUtils.ReplaceParameters(template, parameters);

            // 验证
            Assert.AreEqual("您好，张三。今天天气{weather}。", result, "不存在的参数应保留原样");
        }

        [Test]
        public void ReplaceParameters_HandlesEmptyAndNullValues()
        {
            // 准备 - 空模板
            var emptyTemplate = "";
            var parameters = new Dictionary<string, string>
            {
                { "name", "张三" }
            };

            // 执行
            var emptyResult = StringUtils.ReplaceParameters(emptyTemplate, parameters);
            var nullResult = StringUtils.ReplaceParameters(null, parameters);

            // 验证
            Assert.AreEqual("", emptyResult, "空模板应返回空字符串");
            Assert.AreEqual(null, nullResult, "null模板应返回null");
        }

        [Test]
        public void IsValidIdentifier_ReturnsTrue_ForValidIdentifiers()
        {
            // 测试有效标识符
            Assert.IsTrue(StringUtils.IsValidIdentifier("validName"), "小写字母开头");
            Assert.IsTrue(StringUtils.IsValidIdentifier("ValidName"), "大写字母开头");
            Assert.IsTrue(StringUtils.IsValidIdentifier("_validName"), "下划线开头");
            Assert.IsTrue(StringUtils.IsValidIdentifier("valid_name"), "包含下划线");
            Assert.IsTrue(StringUtils.IsValidIdentifier("validName123"), "包含数字");
            Assert.IsTrue(StringUtils.IsValidIdentifier("汉字标识符"), "Unicode字符");
        }

        [Test]
        public void IsValidIdentifier_ReturnsFalse_ForInvalidIdentifiers()
        {
            // 测试无效标识符
            Assert.IsFalse(StringUtils.IsValidIdentifier("123name"), "数字开头");
            Assert.IsFalse(StringUtils.IsValidIdentifier("invalid-name"), "包含连字符");
            Assert.IsFalse(StringUtils.IsValidIdentifier("invalid name"), "包含空格");
            Assert.IsFalse(StringUtils.IsValidIdentifier(""), "空字符串");
            Assert.IsFalse(StringUtils.IsValidIdentifier(null), "null值");
            Assert.IsFalse(StringUtils.IsValidIdentifier("name!"), "包含特殊字符");
            Assert.IsFalse(StringUtils.IsValidIdentifier("class"), "关键字");
        }

        [Test]
        public void ToValidIdentifier_ConvertsString_ToValidIdentifier()
        {
            // 测试各种情况
            Assert.AreEqual("validName", StringUtils.ToValidIdentifier("validName"), "已有效的标识符");
            Assert.AreEqual("_123name", StringUtils.ToValidIdentifier("123name"), "数字开头");
            Assert.AreEqual("invalid_name", StringUtils.ToValidIdentifier("invalid-name"), "包含连字符");
            Assert.AreEqual("invalid_name", StringUtils.ToValidIdentifier("invalid name"), "包含空格");
            Assert.AreEqual("_", StringUtils.ToValidIdentifier(""), "空字符串");
            Assert.AreEqual("_", StringUtils.ToValidIdentifier(null), "null值");
            Assert.AreEqual("name_", StringUtils.ToValidIdentifier("name!"), "包含特殊字符");
            Assert.AreEqual("_class", StringUtils.ToValidIdentifier("class"), "关键字");
        }

        [Test]
        public void TruncateByWidth_HandlesInternationalCharacters_Correctly()
        {
            // 测试中文字符的宽度处理
            // 在等宽字体环境中，中文字符的宽度通常是英文字符的两倍

            // 1. 中文字符串 - "测试字符串"的显示宽度为10 (每个字2个宽度单位)
            // 如果maxWidth=7，省略号宽度=3，那么应该保留"测"(宽度2)并添加省略号
            Assert.AreEqual("测...", StringUtils.TruncateByWidth("测试字符串", 5), "中文字符串截断");

            // 2. 混合字符串 - "测试abc"的显示宽度为7 (中文4 + 英文3)
            // 如果maxWidth=6，省略号宽度=3，那么应该保留"测"(宽度2)并添加省略号
            Assert.AreEqual("测...", StringUtils.TruncateByWidth("测试abc", 5), "中英混合字符串截断");

            // 3. 英文保留多个字符 - "Testing"的显示宽度为7
            // 如果maxWidth=6，省略号宽度=3，那么应该保留"Tes"(宽度3)并添加省略号
            Assert.AreEqual("Tes...", StringUtils.TruncateByWidth("Testing", 6), "英文字符串截断");

            // 4. 日文平假名 - "ひらがな"的显示宽度为8 (每个字2个宽度单位)
            // 如果maxWidth=5，省略号宽度=3，那么应该保留"ひ"(宽度2)并添加省略号
            Assert.AreEqual("ひ...", StringUtils.TruncateByWidth("ひらがな", 5), "日文平假名截断");

            // 5. 各种Unicode字符混合
            string mixedUnicode = "测试abc한글ひらがな";
            // 显示宽度：中文4 + 英文3 + 韩文4 + 日文8 = 19
            // 如果maxWidth=10，省略号宽度=3，那么应该保留"测试abc"(宽度8)并添加省略号
            Assert.AreEqual("测试abc...", StringUtils.TruncateByWidth(mixedUnicode, 10), "Unicode混合字符串截断");

            // 6. 全角字符
            string fullWidth = "ｆｕｌｌ　ｗｉｄｔｈ"; // 全角字母和空格
            // 如果maxWidth=8，省略号宽度=3，那么应该保留"ｆｕｌ"(宽度6)并添加省略号
            Assert.AreEqual("ｆｕｌ...", StringUtils.TruncateByWidth(fullWidth, 9), "全角字符截断");

            // 7. 自定义省略号
            Assert.AreEqual("测…", StringUtils.TruncateByWidth("测试字符串", 4, "…"), "自定义省略号(单字符)");
            Assert.AreEqual("测--", StringUtils.TruncateByWidth("测试字符串", 4, "--"), "自定义省略号(双字符)");

            // 8. 边界情况测试
            // 当maxWidth小于省略号宽度时
            Assert.AreEqual("", StringUtils.TruncateByWidth("测试", 1, "..."), "maxWidth小于省略号宽度");
            // 当maxWidth等于省略号宽度时
            Assert.AreEqual("...", StringUtils.TruncateByWidth("测试", 3, "..."), "maxWidth等于省略号宽度");
        }
    }
}
