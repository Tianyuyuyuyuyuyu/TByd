using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TBydFramework.Pool.Runtime.Core;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace TBydFramework.Pool.Tests
{
    public class PoolPersistenceTests
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
            PoolPersistenceManager.ClearSavedStates();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_poolManager.gameObject);
        }

        [UnityTest]
        public IEnumerator SaveAndLoadState_WorksCorrectly() => UniTask.ToCoroutine(async () =>
        {
            // 准备测试数据
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            const int instanceCount = 5;
            
            for (int i = 0; i < instanceCount; i++)
            {
                var instance = pool.Get();
                pool.Return(instance);
            }
            
            // 保存状态
            await _poolManager.SavePoolStatesAsync();
            Assert.True(PoolPersistenceManager.HasSavedStates());

            // 清理当前状态
            _poolManager.ClearAllPools();
            Assert.AreEqual(0, pool.Count);

            // 加载状态
            var success = await _poolManager.LoadPoolStatesAsync();
            Assert.True(success);

            // 验证恢复的状态
            pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            Assert.AreEqual(instanceCount, pool.Count);
        });

        [Test]
        public void PersistenceManager_HandlesInvalidData()
        {
            Assert.DoesNotThrow(() => PoolPersistenceManager.ClearSavedStates());
            Assert.False(PoolPersistenceManager.HasSavedStates());
        }

        [UnityTest]
        public IEnumerator AutoSave_WorksCorrectly() => UniTask.ToCoroutine(async () =>
        {
            _poolManager.EnableAutoSave(true, 0.1f); // 设置较短的保存间隔用于测试
            
            var pool = _poolManager.GetGameObjectPool("TestPool", _testPrefab);
            var instance = pool.Get();
            pool.Return(instance);

            // 等待自动保存
            await Task.Delay(200);
            
            Assert.True(PoolPersistenceManager.HasSavedStates());
        });
    }
} 