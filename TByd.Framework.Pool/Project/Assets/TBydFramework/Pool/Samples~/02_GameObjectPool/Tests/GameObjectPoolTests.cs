using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TBydFramework.Pool.Samples.GameObjectPool;

namespace TBydFramework.Pool.Samples.GameObjectPool.Tests
{
    public class GameObjectPoolTests
    {
        private GameObject _testObject;
        private GameObject _prefab;
        private GameObjectPoolExample _poolExample;

        [SetUp]
        public void Setup()
        {
            // 创建测试预制体
            _prefab = new GameObject("Test Prefab");
            _prefab.AddComponent<MeshRenderer>();
            _prefab.AddComponent<BoxCollider>();

            // 创建测试对象
            _testObject = new GameObject("Test Object");
            _poolExample = _testObject.AddComponent<GameObjectPoolExample>();
            
            // 通过反射设置预制体
            var prefabField = typeof(GameObjectPoolExample).GetField("_prefab", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            prefabField.SetValue(_poolExample, _prefab);
        }

        [TearDown]
        public void Teardown()
        {
            if (_testObject != null)
            {
                Object.DestroyImmediate(_testObject);
            }
            if (_prefab != null)
            {
                Object.DestroyImmediate(_prefab);
            }
        }

        [UnityTest]
        public IEnumerator Example_InitializesPoolCorrectly()
        {
            yield return null; // 等待Start执行完成

            var poolField = typeof(GameObjectPoolExample).GetField("_pool", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var pool = poolField.GetValue(_poolExample);
            
            Assert.That(pool, Is.Not.Null, "Pool should be initialized");
        }

        [UnityTest]
        public IEnumerator Example_SpawnsObjectsCorrectly()
        {
            yield return null;

            // 获取SpawnParent
            var spawnParentField = typeof(GameObjectPoolExample).GetField("_spawnParent", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var spawnParent = (Transform)spawnParentField.GetValue(_poolExample);

            // 生成对象
            var spawnMethod = typeof(GameObjectPoolExample).GetMethod("SpawnObject", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            spawnMethod.Invoke(_poolExample, null);

            Assert.That(spawnParent.childCount, Is.EqualTo(1), "Should have spawned one object");
        }

        [UnityTest]
        public IEnumerator Example_ReturnsObjectsCorrectly()
        {
            yield return null;

            // 先生成一些对象
            var spawnMethod = typeof(GameObjectPoolExample).GetMethod("SpawnObject", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            spawnMethod.Invoke(_poolExample, null);
            spawnMethod.Invoke(_poolExample, null);

            // 返回所有对象
            var returnMethod = typeof(GameObjectPoolExample).GetMethod("ReturnAllObjects", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            returnMethod.Invoke(_poolExample, null);

            // 获取SpawnParent检查子对象数量
            var spawnParentField = typeof(GameObjectPoolExample).GetField("_spawnParent", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var spawnParent = (Transform)spawnParentField.GetValue(_poolExample);

            Assert.That(spawnParent.childCount, Is.EqualTo(0), "All objects should be returned to pool");
        }
    }
} 