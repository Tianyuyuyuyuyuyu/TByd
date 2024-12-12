using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TBydFramework.Pool.Samples.SharedPool;

namespace TBydFramework.Pool.Samples.SharedPool.Tests
{
    public class SharedPoolTests
    {
        private GameObject _prefab;
        private GameObject _container;
        private SharedPoolExample[] _poolUsers;

        [SetUp]
        public void Setup()
        {
            // 创建测试预制体
            _prefab = new GameObject("Test Prefab");
            _prefab.AddComponent<MeshRenderer>();
            _prefab.AddComponent<BoxCollider>();

            // 创建容器对象和多个使用者
            _container = new GameObject("Pool Users Container");
            _poolUsers = new SharedPoolExample[3];
            
            for (int i = 0; i < _poolUsers.Length; i++)
            {
                var userObj = new GameObject($"Pool User {i}");
                userObj.transform.SetParent(_container.transform);
                _poolUsers[i] = userObj.AddComponent<SharedPoolExample>();
                
                // 通过反射设置预制体
                var prefabField = typeof(SharedPoolExample).GetField("_prefab", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prefabField.SetValue(_poolUsers[i], _prefab);
            }
        }

        [TearDown]
        public void Teardown()
        {
            if (_container != null)
            {
                Object.DestroyImmediate(_container);
            }
            if (_prefab != null)
            {
                Object.DestroyImmediate(_prefab);
            }
        }

        [UnityTest]
        public IEnumerator SharedPool_InitializesOnceForAllUsers()
        {
            yield return null; // 等待所有Start执行完成

            // 获取静态池字段
            var poolField = typeof(SharedPoolExample).GetField("_sharedPool", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            var pool = poolField.GetValue(null);
            
            Assert.That(pool, Is.Not.Null, "Shared pool should be initialized");

            // 验证所有用户使用同一个池
            foreach (var user in _poolUsers)
            {
                var spawnMethod = typeof(SharedPoolExample).GetMethod("SpawnObject", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                spawnMethod.Invoke(user, null);
            }

            // 验证池的使用情况
            var spawnParentField = typeof(SharedPoolExample).GetField("_spawnParent", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int totalSpawned = 0;
            foreach (var user in _poolUsers)
            {
                var userSpawnParent = (Transform)spawnParentField.GetValue(user);
                totalSpawned += userSpawnParent.childCount;
            }

            Assert.That(totalSpawned, Is.EqualTo(3), "Each user should have spawned one object");
        }

        [UnityTest]
        public IEnumerator SharedPool_HandlesMultipleUsersCorrectly()
        {
            yield return null;

            // 让每个用户生成和返回对象
            foreach (var user in _poolUsers)
            {
                // 生成对象
                var spawnMethod = typeof(SharedPoolExample).GetMethod("SpawnObject", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                spawnMethod.Invoke(user, null);
                spawnMethod.Invoke(user, null);

                // 返回对象
                var returnMethod = typeof(SharedPoolExample).GetMethod("ReturnAllObjects", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                returnMethod.Invoke(user, null);
            }

            // 验证所有对象都已正确返回
            var spawnParentField = typeof(SharedPoolExample).GetField("_spawnParent", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            foreach (var user in _poolUsers)
            {
                var userSpawnParent = (Transform)spawnParentField.GetValue(user);
                Assert.That(userSpawnParent.childCount, Is.EqualTo(0), 
                    "All objects should be returned to pool");
            }
        }
    }
} 