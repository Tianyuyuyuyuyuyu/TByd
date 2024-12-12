using NUnit.Framework;
using UnityEngine;
using TBydFramework.Pool.Runtime.Core;
using System.Collections;
using UnityEngine.TestTools;
using System.Diagnostics;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace TBydFramework.Pool.Tests
{
    public class PoolPerformanceTests
    {
        private PoolManager _poolManager;
        private GameObject _testPrefab;
        private const int WarmupCount = 1000;
        private const int TestIterations = 10000;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testPrefab = new GameObject("TestPrefab");
            _testPrefab.AddComponent<MeshFilter>();
            _testPrefab.AddComponent<MeshRenderer>();
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
        public void PoolPerformance_GetAndReturn()
        {
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            var stopwatch = new Stopwatch();

            // 预热
            for (int i = 0; i < WarmupCount; i++)
            {
                var obj = pool.Get();
                pool.Return(obj);
            }

            // 测试Get操作
            stopwatch.Start();
            var instances = new GameObject[TestIterations];
            for (int i = 0; i < TestIterations; i++)
            {
                instances[i] = pool.Get();
            }
            stopwatch.Stop();
            var getTime = stopwatch.ElapsedMilliseconds;

            // 测试Return操作
            stopwatch.Restart();
            for (int i = 0; i < TestIterations; i++)
            {
                pool.Return(instances[i]);
            }
            stopwatch.Stop();
            var returnTime = stopwatch.ElapsedMilliseconds;

            UnityEngine.Debug.Log($"Get {TestIterations} objects: {getTime}ms");
            UnityEngine.Debug.Log($"Return {TestIterations} objects: {returnTime}ms");

            Assert.Less(getTime / TestIterations, 0.1f); // 每个操作应小于0.1ms
            Assert.Less(returnTime / TestIterations, 0.1f);
        }

        [UnityTest]
        public IEnumerator PoolPerformance_ConcurrentOperations() => UniTask.ToCoroutine(async () =>
        {
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            const int concurrentTasks = 10;
            const int operationsPerTask = 1000;

            var tasks = new Task[concurrentTasks];
            for (int i = 0; i < concurrentTasks; i++)
            {
                tasks[i] = Task.Run(async () =>
                {
                    for (int j = 0; j < operationsPerTask; j++)
                    {
                        var obj = pool.Get();
                        await Task.Delay(1);
                        pool.Return(obj);
                    }
                });
            }

            await Task.WhenAll(tasks);

            Assert.AreEqual(0, pool.ActiveCount);
            Assert.Greater(pool.Count, 0);
        });

        [Test]
        public void PoolPerformance_MemoryAllocation()
        {
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            pool.Prewarm(WarmupCount);

            // 记录初始内存
            var initialMemory = System.GC.GetTotalMemory(true);

            // 执行大量操作
            for (int i = 0; i < TestIterations; i++)
            {
                var obj = pool.Get();
                pool.Return(obj);
            }

            // 强制GC
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            var finalMemory = System.GC.GetTotalMemory(true);

            // 检查内存增长
            var memoryGrowth = finalMemory - initialMemory;
            UnityEngine.Debug.Log($"Memory growth after {TestIterations} operations: {memoryGrowth / 1024}KB");

            Assert.Less(memoryGrowth, 1024 * 1024); // 内存增长应小于1MB
        }
    }
} 