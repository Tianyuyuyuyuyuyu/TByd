using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.AddressableAssets;
using TBydFramework.Pool.Runtime.Core;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace TBydFramework.Pool.Tests
{
    public class AddressablePoolTests
    {
        private PoolManager _poolManager;
        private const string TestAddressableKey = "TestPrefab";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // 设置测试用的Addressable资源
            var testPrefab = new GameObject("TestPrefab");
            // 这里需要根据实际项目设置Addressable资源
        }

        [SetUp]
        public void SetUp()
        {
            var go = new GameObject("[PoolManager]");
            _poolManager = go.AddComponent<PoolManager>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_poolManager.gameObject);
        }

        [UnityTest]
        public IEnumerator AddressablePool_LoadsAndInstantiates() => UniTask.ToCoroutine(async () =>
        {
            var pool = await _poolManager.GetAddressablePoolAsync("TestPool", TestAddressableKey);
            Assert.NotNull(pool);

            var instance = await pool.GetAsync();
            Assert.NotNull(instance);
            Assert.AreEqual(1, pool.ActiveCount);

            pool.Return(instance);
            Assert.AreEqual(0, pool.ActiveCount);
            Assert.AreEqual(1, pool.Count);
        });

        [UnityTest]
        public IEnumerator AddressablePool_HandlesMultipleInstances() => UniTask.ToCoroutine(async () =>
        {
            var pool = await _poolManager.GetAddressablePoolAsync("TestPool", TestAddressableKey);
            const int instanceCount = 3;
            
            var instances = new GameObject[instanceCount];
            for (int i = 0; i < instanceCount; i++)
            {
                instances[i] = await pool.GetAsync();
                Assert.NotNull(instances[i]);
            }
            
            Assert.AreEqual(instanceCount, pool.ActiveCount);

            foreach (var instance in instances)
            {
                pool.Return(instance);
            }
            
            Assert.AreEqual(0, pool.ActiveCount);
            Assert.AreEqual(instanceCount, pool.Count);
        });
    }
} 