using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TBydFramework.Pool.Runtime.Core;
using System.Threading.Tasks;

namespace TBydFramework.Pool.Tests
{
    public class PoolManagerTests
    {
        private PoolManager _poolManager;
        private GameObject _testPrefab;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testPrefab = new GameObject("TestPrefab");
            Object.DontDestroyOnLoad(_testPrefab);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Object.DestroyImmediate(_testPrefab);
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

        [Test]
        public void GetPool_ReturnsValidPool()
        {
            var pool = _poolManager.GetPool<TestPoolObject>();
            Assert.NotNull(pool);
        }

        [UnityTest]
        public IEnumerator GetGameObject_ReturnsValidInstance()
        {
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            var instance = pool.Get();
            
            Assert.NotNull(instance);
            Assert.AreEqual(1, pool.ActiveCount);
            Assert.AreEqual(0, pool.Count);

            pool.Return(instance);
            
            Assert.AreEqual(0, pool.ActiveCount);
            Assert.AreEqual(1, pool.Count);

            yield return null;
        }

        [UnityTest]
        public IEnumerator PrewarmPool_CreatesCorrectAmount()
        {
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            const int prewarmCount = 5;
            
            pool.Prewarm(prewarmCount);
            
            Assert.AreEqual(prewarmCount, pool.Count);
            Assert.AreEqual(0, pool.ActiveCount);

            yield return null;
        }

        [Test]
        public void PoolCapacity_RespectsMaxSize()
        {
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            const int maxSize = 3;
            pool.MaxSize = maxSize;

            var instances = new GameObject[maxSize + 1];
            for (int i = 0; i < maxSize + 1; i++)
            {
                instances[i] = pool.Get();
            }

            Assert.Null(instances[maxSize]);
            Assert.AreEqual(maxSize, pool.ActiveCount);
        }

        [UnityTest]
        public IEnumerator PoolCleanup_WorksCorrectly()
        {
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            const int initialCount = 5;
            
            // 创建并返回一些对象
            var instances = new GameObject[initialCount];
            for (int i = 0; i < initialCount; i++)
            {
                instances[i] = pool.Get();
            }
            
            foreach (var instance in instances)
            {
                pool.Return(instance);
            }
            
            Assert.AreEqual(initialCount, pool.Count);
            
            // 执行清理
            pool.Cleanup();
            
            Assert.Less(pool.Count, initialCount);

            yield return null;
        }

        private class TestPoolObject { }
    }
} 