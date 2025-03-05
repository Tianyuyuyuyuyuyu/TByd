using NUnit.Framework;
using TByd.CodeStyle.Runtime.CodeCheck;
using TByd.CodeStyle.Runtime.CodeCheck.Rules;
using TByd.CodeStyle.Runtime.Config;

namespace TByd.CodeStyle.Tests.Editor.CodeCheck.Rules
{
    /// <summary>
    /// 命名规则测试
    /// </summary>
    public class NamingRulesTests
    {
        private CodeCheckConfig m_Config;

        [SetUp]
        public void Setup()
        {
            m_Config = new CodeCheckConfig();

            // 确保所有规则都已启用
            m_Config.AddRule(new CodeCheckConfig.CodeRule("CS0003", "私有成员变量使用m_前缀", "私有成员变量应该使用m_前缀"));
            m_Config.AddRule(new CodeCheckConfig.CodeRule("CS0004", "常量使用c_前缀", "常量应该使用c_前缀"));
            m_Config.AddRule(new CodeCheckConfig.CodeRule("CS0005", "静态变量使用s_前缀", "静态变量应该使用s_前缀"));
            m_Config.AddRule(new CodeCheckConfig.CodeRule("CS0006", "参数使用_前缀", "参数应该使用_前缀"));
            m_Config.AddRule(new CodeCheckConfig.CodeRule("CS0002", "方法名使用PascalCase", "方法名应该使用PascalCase命名法"));
            m_Config.AddRule(new CodeCheckConfig.CodeRule("CS0007", "属性使用PascalCase", "属性应该使用PascalCase命名法"));
        }

        [TearDown]
        public void TearDown()
        {
            // 不需要销毁非ScriptableObject
        }

        /// <summary>
        /// 测试私有成员变量命名规则
        /// </summary>
        [Test]
        public void PrivateFieldNamingRule_ValidAndInvalidNames_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new PrivateFieldNamingRule { Enabled = true };

            // 有效的代码示例，使用m_前缀
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    private int m_Count;
    private string m_Name;
    [SerializeField] private float m_Speed;
}";

            // 无效的代码示例，没有使用m_前缀
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    private int count;
    private string name;
    [SerializeField] private float speed;
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "有效的命名约定应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "无效的命名约定不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "无效代码应该有问题");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("m_"), "问题描述应该提到需要使用m_前缀");
            }
        }

        /// <summary>
        /// 测试静态变量命名规则
        /// </summary>
        [Test]
        public void StaticFieldNamingRule_ValidAndInvalidNames_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new StaticFieldNamingRule { Enabled = true };

            // 有效的代码示例，使用s_前缀
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    private static int s_Count;
    private static string s_Name;
    public static float s_Speed;
}";

            // 无效的代码示例，没有使用s_前缀
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    private static int count;
    private static string name;
    public static float speed;
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "有效的命名约定应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "无效的命名约定不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "无效代码应该有问题");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("s_"), "问题描述应该提到需要使用s_前缀");
            }
        }

        /// <summary>
        /// 测试常量命名规则
        /// </summary>
        [Test]
        public void ConstantNamingRule_ValidAndInvalidNames_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new ConstantNamingRule { Enabled = true };

            // 有效的代码示例，使用c_前缀
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    private const int c_MaxCount = 100;
    private const string c_DefaultName = ""Player"";
    public const float c_MaxSpeed = 10f;
}";

            // 无效的代码示例，没有使用c_前缀
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    private const int MAX_COUNT = 100;
    private const string DefaultName = ""Player"";
    public const float MaxSpeed = 10f;
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "有效的命名约定应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "无效的命名约定不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "无效代码应该有问题");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("c_"), "问题描述应该提到需要使用c_前缀");
            }
        }

        /// <summary>
        /// 测试属性命名规则
        /// </summary>
        [Test]
        public void PropertyNamingRule_ValidAndInvalidNames_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new PropertyNamingRule { Enabled = true };

            // 有效的代码示例，使用PascalCase
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    public int Count { get; set; }
    public string Name { get; private set; }
    private float Speed { get; set; }
}";

            // 无效的代码示例，没有使用PascalCase
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    public int count { get; set; }
    public string name { get; private set; }
    private float speed { get; set; }
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "有效的命名约定应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "无效的命名约定不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "无效代码应该有问题");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("Pascal"), "问题描述应该提到需要使用PascalCase");
            }
        }

        /// <summary>
        /// 测试方法命名规则
        /// </summary>
        [Test]
        public void MethodNamingRule_ValidAndInvalidNames_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new MethodNamingRule { Enabled = true };

            // 有效的代码示例，使用PascalCase
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    public void Start() { }
    private void Update() { }
    protected void CalculateDistance() { }
}";

            // 无效的代码示例，没有使用PascalCase
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    public void start() { }
    private void update() { }
    protected void calculateDistance() { }
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "有效的命名约定应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "无效的命名约定不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "无效代码应该有问题");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("Pascal"), "问题描述应该提到需要使用PascalCase");
            }
        }

        /// <summary>
        /// 测试参数命名规则
        /// </summary>
        [Test]
        public void ParameterNamingRule_ValidAndInvalidNames_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new ParameterNamingRule { Enabled = true };

            // 有效的代码示例，使用_前缀
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    public void Move(float _speed, Vector3 _direction) { }
    private int Calculate(int _value1, int _value2) { return _value1 + _value2; }
}";

            // 无效的代码示例，没有使用_前缀
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    public void Move(float speed, Vector3 direction) { }
    private int Calculate(int value1, int value2) { return value1 + value2; }
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "有效的命名约定应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "无效的命名约定不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "无效代码应该有问题");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("_"), "问题描述应该提到需要使用_前缀");
            }
        }

        /// <summary>
        /// 测试复杂场景中的多个规则
        /// </summary>
        [Test]
        public void MultipleNamingRules_ComplexCode_DetectsAllIssues()
        {
            // 创建代码检查器
            var checker = new CodeChecker(m_Config);

            // 复杂代码示例，包含多种命名问题
            var complexCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    // 私有成员变量命名问题
    private int count;

    // 静态变量命名问题
    private static string name;

    // 常量命名问题
    private const float MAX_SPEED = 10f;

    // 属性命名问题
    public bool isActive { get; set; }

    // 方法命名问题，但方法内部使用了正确的参数命名
    public void calculateDistance(Vector3 _start, Vector3 _end)
    {
        float distance = Vector3.Distance(_start, _end);
        Debug.Log(distance);
    }

    // 方法命名正确，但参数命名问题
    public void Update(float deltaTime)
    {
        // 实现
    }
}";

            // 执行检查
            var result = checker.CheckCode(complexCode, "Example.cs");

            // 验证结果
            Assert.IsFalse(result.IsValid, "包含多种命名问题的代码不应该通过检查");
            Assert.Greater(result.Issues.Count, 0, "应该检测到多个命名问题");

            // 验证检测到了不同类型的命名问题
            var foundPrivateFieldIssue = false;
            var foundStaticFieldIssue = false;
            var foundConstantIssue = false;
            var foundPropertyIssue = false;
            var foundMethodIssue = false;
            var foundParameterIssue = false;

            foreach (var issue in result.Issues)
            {
                if (issue.RuleId == "CS0003")
                {
                    foundPrivateFieldIssue = true;
                }

                if (issue.RuleId == "CS0005")
                {
                    foundStaticFieldIssue = true;
                }

                if (issue.RuleId == "CS0004")
                {
                    foundConstantIssue = true;
                }

                if (issue.RuleId == "CS0007")
                {
                    foundPropertyIssue = true;
                }

                if (issue.RuleId == "CS0002")
                {
                    foundMethodIssue = true;
                }

                if (issue.RuleId == "CS0006")
                {
                    foundParameterIssue = true;
                }
            }

            Assert.IsTrue(foundPrivateFieldIssue, "应该检测到私有成员变量命名问题");
            Assert.IsTrue(foundStaticFieldIssue, "应该检测到静态变量命名问题");
            Assert.IsTrue(foundConstantIssue, "应该检测到常量命名问题");
            Assert.IsTrue(foundPropertyIssue, "应该检测到属性命名问题");
            Assert.IsTrue(foundMethodIssue, "应该检测到方法命名问题");
            Assert.IsTrue(foundParameterIssue, "应该检测到参数命名问题");
        }
    }
}
