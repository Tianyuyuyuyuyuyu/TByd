using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TBydFramework.Pool.Samples.BasicObjectPool;

namespace TBydFramework.Pool.Samples.BasicObjectPool.Tests
{
    [TestFixture]
    public class BasicObjectPoolTests
    {
        private GameObject _testObject;
        private BasicObjectPoolExample _poolExample;

        [SetUp]
        public void Setup()
        {
            _testObject = new GameObject("Test Object");
            _poolExample = _testObject.AddComponent<BasicObjectPoolExample>();
        }

        [TearDown]
        public void Teardown()
        {
            if (_testObject != null)
            {
                Object.DestroyImmediate(_testObject);
            }
        }

        [UnityTest]
        public IEnumerator Example_InitializesCorrectly()
        {
            yield return null; // 等待Start执行完成

            // 通过反射获取私有字段进行测试
            var poolField = typeof(BasicObjectPoolExample).GetField("_pool", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var pool = poolField.GetValue(_poolExample);
            
            Assert.That(pool, Is.Not.Null, "Pool should be initialized");
        }

        [UnityTest]
        public IEnumerator Example_HandlesItemsCorrectly()
        {
            yield return null;

            // 模拟获取新项目的操作
            var getItemMethod = typeof(BasicObjectPoolExample).GetMethod("GetNewItem", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            getItemMethod.Invoke(_poolExample, null);

            // 获取活动项目列表
            var activeItemsField = typeof(BasicObjectPoolExample).GetField("_activeItems", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var activeItems = (object[])activeItemsField.GetValue(_poolExample);

            Assert.That(activeItems.Length, Is.EqualTo(1), "Should have one active item");
        }

        [UnityTest]
        public IEnumerator Example_ClearsPoolCorrectly()
        {
            yield return null;

            // 先添加一些项目
            var getItemMethod = typeof(BasicObjectPoolExample).GetMethod("GetNewItem", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            getItemMethod.Invoke(_poolExample, null);
            getItemMethod.Invoke(_poolExample, null);

            // 清理池
            var clearPoolMethod = typeof(BasicObjectPoolExample).GetMethod("ClearPool", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            clearPoolMethod.Invoke(_poolExample, null);

            // 验证活动项目已被清理
            var activeItemsField = typeof(BasicObjectPoolExample).GetField("_activeItems", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var activeItems = (object[])activeItemsField.GetValue(_poolExample);

            Assert.That(activeItems.Length, Is.EqualTo(0), "Should have no active items after clear");
        }
    }
} 