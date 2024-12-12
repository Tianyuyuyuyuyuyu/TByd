using NUnit.Framework;
using UnityEngine;
using TBydFramework.Pool.Runtime.Core;
using TBydFramework.Pool.Runtime.Diagnostics;
using System.Collections;
using UnityEngine.TestTools;
using System.Linq;

namespace TBydFramework.Pool.Tests
{
    public class PoolDiagnosticsTests
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
            _poolManager.EnableDiagnostics(true);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_poolManager.gameObject);
        }

        [Test]
        public void DiagnosticsManager_TracksPoolOperations()
        {
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            const int operationCount = 5;

            for (int i = 0; i < operationCount; i++)
            {
                var instance = pool.Get();
                pool.Return(instance);
            }

            var info = _poolManager.GetPoolDiagnostics("TestPool");
            Assert.AreEqual(operationCount, info.TotalCreated);
            Assert.Greater(info.AverageGetTime, 0f);
            Assert.Greater(info.AverageReturnTime, 0f);
        }

        [Test]
        public void DiagnosticsManager_DetectsCapacityIssues()
        {
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            pool.MaxSize = 3;

            // 触发容量警告
            for (int i = 0; i < 5; i++)
            {
                pool.Get();
            }

            var events = _poolManager.GetDiagnosticEvents();
            Assert.True(events.Any(e => e.EventType == "Capacity"));
        }

        [UnityTest]
        public IEnumerator DiagnosticsManager_TracksMemoryUsage()
        {
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            const int instanceCount = 10;

            // 创建多个实例以增加内存使用
            var instances = new GameObject[instanceCount];
            for (int i = 0; i < instanceCount; i++)
            {
                instances[i] = pool.Get();
                // 添加一些组件以增加内存使用
                instances[i].AddComponent<MeshFilter>();
                instances[i].AddComponent<MeshRenderer>();
            }

            yield return new WaitForSeconds(0.1f); // 等待诊断更新

            var info = _poolManager.GetPoolDiagnostics("TestPool");
            Assert.Greater(info.MemoryUsage, 0);

            foreach (var instance in instances)
            {
                pool.Return(instance);
            }
        }

        [Test]
        public void DiagnosticsManager_HandlesResetAndClear()
        {
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            var instance = pool.Get();
            pool.Return(instance);

            var info = _poolManager.GetPoolDiagnostics("TestPool");
            Assert.Greater(info.TotalCreated, 0);

            PoolDiagnosticsManager.ClearDiagnostics();
            info = _poolManager.GetPoolDiagnostics("TestPool");
            
            Assert.AreEqual(0, info.TotalCreated);
            Assert.AreEqual(0, info.TotalDestroyed);
            Assert.AreEqual(0, info.AverageGetTime);
        }
    }
} 