using NUnit.Framework;
using TByd.CodeStyle.Runtime.CodeCheck;
using TByd.CodeStyle.Runtime.CodeCheck.Rules;
using TByd.CodeStyle.Runtime.Config;

namespace TByd.CodeStyle.Tests.Editor.CodeCheck.Rules
{
    /// <summary>
    /// Unity特定规则测试
    /// </summary>
    public class UnityRulesTests
    {
        private CodeCheckConfig m_Config;

        [SetUp]
        public void Setup()
        {
            m_Config = new CodeCheckConfig();
        }

        [TearDown]
        public void TearDown()
        {
            // 不需要销毁非ScriptableObject
        }

        /// <summary>
        /// 测试避免使用GameObject.Find规则
        /// </summary>
        [Test]
        public void AvoidGameObjectFindRule_ValidAndInvalidCases_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new AvoidGameObjectFindRule();
            rule.Enabled = true;

            // 有效的代码示例，不使用GameObject.Find
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField] private GameObject m_Target;

    void Start()
    {
        // 使用缓存的引用
        if (m_Target != null)
        {
            m_Target.SetActive(true);
        }
    }
}";

            // 无效的代码示例，使用GameObject.Find
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    void Start()
    {
        // 使用GameObject.Find
        GameObject target = GameObject.Find(""Player"");
        if (target != null)
        {
            target.SetActive(true);
        }
    }
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "不使用GameObject.Find的代码应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "使用GameObject.Find的代码不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "应该检测到GameObject.Find的使用");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("GameObject.Find"), "问题描述应该提到GameObject.Find");
            }
        }

        /// <summary>
        /// 测试避免在Update中使用Find规则
        /// </summary>
        [Test]
        public void AvoidFindInUpdateRule_ValidAndInvalidCases_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new AvoidFindInUpdateRule();
            rule.Enabled = true;

            // 有效的代码示例，不在Update中使用Find
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    private GameObject m_Target;

    void Start()
    {
        // 在Start中使用Find是可以的
        m_Target = GameObject.Find(""Player"");
    }

    void Update()
    {
        // 使用缓存的引用
        if (m_Target != null)
        {
            float distance = Vector3.Distance(transform.position, m_Target.transform.position);
            Debug.Log(distance);
        }
    }
}";

            // 无效的代码示例，在Update中使用Find
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    void Update()
    {
        // 在Update中使用Find，性能问题
        GameObject target = GameObject.Find(""Player"");
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            Debug.Log(distance);
        }
    }
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "不在Update中使用Find的代码应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "在Update中使用Find的代码不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "应该检测到在Update中使用Find");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("Update") && issue.Message.Contains("Find"),
                    "问题描述应该提到在Update中使用Find的问题");
            }
        }

        /// <summary>
        /// 测试避免使用SendMessage规则
        /// </summary>
        [Test]
        public void AvoidSendMessageRule_ValidAndInvalidCases_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new AvoidSendMessageRule();
            rule.Enabled = true;

            // 有效的代码示例，不使用SendMessage
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    private IMessageReceiver m_Receiver;

    void Start()
    {
        m_Receiver = GetComponent<IMessageReceiver>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 直接调用方法而不是使用SendMessage
            if (m_Receiver != null)
                m_Receiver.OnMessageReceived(""Jump"");
        }
    }
}

public interface IMessageReceiver
{
    void OnMessageReceived(string message);
}";

            // 无效的代码示例，使用SendMessage
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 使用SendMessage，性能问题
            gameObject.SendMessage(""OnJump"", SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnJump()
    {
        Debug.Log(""Jumping"");
    }
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "不使用SendMessage的代码应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "使用SendMessage的代码不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "应该检测到SendMessage的使用");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("SendMessage"), "问题描述应该提到SendMessage");
            }
        }

        /// <summary>
        /// 测试避免在Update中使用GetComponent规则
        /// </summary>
        [Test]
        public void AvoidGetComponentInUpdate_ValidAndInvalidCases_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new AvoidGetComponentInUpdateRule();
            rule.Enabled = true;

            // 有效的代码示例，不在Update中使用GetComponent
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    void Start()
    {
        // 在Start中缓存组件引用
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 使用缓存的组件引用
        if (m_Rigidbody != null)
        {
            m_Rigidbody.AddForce(Vector3.up * 10f);
        }
    }
}";

            // 无效的代码示例，在Update中使用GetComponent
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    void Update()
    {
        // 在Update中使用GetComponent，性能问题
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * 10f);
        }
    }
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "不在Update中使用GetComponent的代码应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "在Update中使用GetComponent的代码不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "应该检测到在Update中使用GetComponent");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("GetComponent") && issue.Message.Contains("Update"),
                    "问题描述应该提到在Update中使用GetComponent的问题");
            }
        }

        /// <summary>
        /// 测试避免硬编码路径规则
        /// </summary>
        [Test]
        public void AvoidHardcodedPathRule_ValidAndInvalidCases_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new AvoidHardcodedPathRule();
            rule.Enabled = true;

            // 有效的代码示例，不使用硬编码路径
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    private const string c_ResourcePath = ""Prefabs/Player"";

    void Start()
    {
        // 使用常量而不是硬编码路径
        GameObject prefab = Resources.Load<GameObject>(c_ResourcePath);
        Instantiate(prefab);
    }
}";

            // 无效的代码示例，使用硬编码路径
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    void Start()
    {
        // 使用硬编码路径
        GameObject prefab = Resources.Load<GameObject>(""Prefabs/Player"");
        Instantiate(prefab);
    }
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "不使用硬编码路径的代码应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "使用硬编码路径的代码不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "应该检测到硬编码路径的使用");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("硬编码") || issue.Message.Contains("路径"),
                    "问题描述应该提到硬编码路径问题");
            }
        }

        /// <summary>
        /// 测试避免空的MonoBehaviour方法规则
        /// </summary>
        [Test]
        public void AvoidEmptyMonoBehaviourMethodsRule_ValidAndInvalidCases_ReturnsCorrectResults()
        {
            // 创建规则
            var rule = new AvoidEmptyMonoBehaviourMethodsRule();
            rule.Enabled = true;

            // 有效的代码示例，没有空的MonoBehaviour方法
            var validCode = @"
using UnityEngine;

public class Example : MonoBehaviour
{
    // 方法中有实现，不是空的
    void Start()
    {
        Debug.Log(""Starting..."");
    }

    // 方法中有实现，不是空的
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime);
    }
}";

            // 无效的代码示例，有空的MonoBehaviour方法
            var invalidCode = @"
using UnityEngine;

public class BadExample : MonoBehaviour
{
    // 空的Start方法
    void Start()
    {
    }

    // 空的Update方法
    void Update()
    {
    }
}";

            // 测试有效代码
            var result1 = rule.Check(validCode, "Example.cs", m_Config);
            Assert.IsTrue(result1.IsValid, "没有空的MonoBehaviour方法的代码应该通过检查");
            Assert.AreEqual(0, result1.Issues.Count, "有效代码不应该有问题");

            // 测试无效代码
            var result2 = rule.Check(invalidCode, "BadExample.cs", m_Config);
            Assert.IsFalse(result2.IsValid, "有空的MonoBehaviour方法的代码不应该通过检查");
            Assert.Greater(result2.Issues.Count, 0, "应该检测到空的MonoBehaviour方法");

            // 验证问题描述包含预期的信息
            foreach (var issue in result2.Issues)
            {
                Assert.IsTrue(issue.Message.Contains("空") || issue.Message.Contains("empty"),
                    "问题描述应该提到空方法问题");
            }
        }

        /// <summary>
        /// 测试综合Unity规则检查
        /// </summary>
        [Test]
        public void MultipleUnityRules_ComplexCode_DetectsAllIssues()
        {
            // 创建代码检查器
            var checker = new CodeChecker(m_Config);

            // 复杂代码示例，包含多种Unity相关问题
            var complexCode = @"
using UnityEngine;

public class ComplexExample : MonoBehaviour
{
    // 空的Unity方法
    void Awake()
    {
    }

    void Start()
    {
        // 在Start中使用Find是可以接受的
        GameObject player = GameObject.Find(""Player"");
    }

    void Update()
    {
        // 在Update中使用Find
        GameObject enemy = GameObject.Find(""Enemy"");

        // 在Update中使用GetComponent
        Rigidbody rb = GetComponent<Rigidbody>();

        // 使用SendMessage
        gameObject.SendMessage(""OnHit"");

        // 硬编码路径
        Texture2D texture = Resources.Load<Texture2D>(""Textures/Grass"");
    }

    // 另一个空的Unity方法
    void OnDestroy()
    {
    }
}";

            // 执行检查
            var result = checker.CheckCode(complexCode, "ComplexExample.cs");

            // 验证结果
            Assert.IsFalse(result.IsValid, "包含多种Unity问题的代码不应该通过检查");
            Assert.Greater(result.Issues.Count, 0, "应该检测到多个Unity相关问题");

            // 验证检测到了不同类型的Unity问题
            var foundFindInUpdateIssue = false;
            var foundGetComponentInUpdateIssue = false;
            var foundSendMessageIssue = false;
            var foundHardcodedPathIssue = false;
            var foundEmptyMethodIssue = false;

            foreach (var issue in result.Issues)
            {
                if (issue.Message.Contains("Update") && issue.Message.Contains("Find"))
                    foundFindInUpdateIssue = true;
                if (issue.Message.Contains("Update") && issue.Message.Contains("GetComponent"))
                    foundGetComponentInUpdateIssue = true;
                if (issue.Message.Contains("SendMessage"))
                    foundSendMessageIssue = true;
                if (issue.Message.Contains("硬编码") || issue.Message.Contains("路径"))
                    foundHardcodedPathIssue = true;
                if (issue.Message.Contains("空") || issue.Message.Contains("empty"))
                    foundEmptyMethodIssue = true;
            }

            Assert.IsTrue(foundFindInUpdateIssue, "应该检测到在Update中使用Find的问题");
            Assert.IsTrue(foundGetComponentInUpdateIssue, "应该检测到在Update中使用GetComponent的问题");
            Assert.IsTrue(foundSendMessageIssue, "应该检测到使用SendMessage的问题");
            Assert.IsTrue(foundHardcodedPathIssue, "应该检测到硬编码路径的问题");
            Assert.IsTrue(foundEmptyMethodIssue, "应该检测到空的Unity方法问题");
        }
    }
}
