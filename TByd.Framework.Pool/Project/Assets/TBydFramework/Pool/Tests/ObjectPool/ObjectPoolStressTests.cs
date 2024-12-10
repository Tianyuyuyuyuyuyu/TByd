using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBydFramework.Pool.Runtime.Core;
using TBydFramework.Pool.Runtime.Config;
using TBydFramework.Pool.Runtime.Exceptions;
using UnityEngine;

namespace TBydFramework.Pool.Tests
{
    public class ObjectPoolStressTests
    {
        private ObjectPool<GameObject> _pool;
        private List<GameObject> _rentedObjects;
        private PoolSettings _settings;

        [SetUp]
        public void Setup()
        {
            _settings = ScriptableObject.CreateInstance<PoolSettings>();
            _pool = new ObjectPool<GameObject>(
                () => new GameObject("PooledObject"),
                _settings
            );
            _rentedObjects = new List<GameObject>();
        }

        [TearDown]
        public void Teardown()
        {
            foreach (var obj in _rentedObjects)
            {
                if (obj != null)
                {
                    _pool.Return(obj);
                    Object.Destroy(obj);
                }
            }
            _rentedObjects.Clear();
            _pool.Clear();
            
            if (_settings != null)
            {
                Object.Destroy(_settings);
            }
        }

        [Test]
        public void RapidGetReturn_ShouldHandleHighLoad()
        {
            const int iterations = 1000;
            
            for (int i = 0; i < iterations; i++)
            {
                var obj = _pool.Get();
                Assert.NotNull(obj);
                _rentedObjects.Add(obj);
                
                if (i % 2 == 0)
                {
                    _pool.Return(obj);
                    _rentedObjects.Remove(obj);
                }
            }

            Assert.AreEqual(iterations / 2, _rentedObjects.Count);
        }

        [Test]
        public void PoolSize_ShouldRespectDefaultSize()
        {
            // 获取超过默认池大小的对象
            for (int i = 0; i < _settings.DefaultPoolSize + 10; i++)
            {
                var obj = _pool.Get();
                Assert.NotNull(obj, $"Failed to get object at iteration {i}");
                _rentedObjects.Add(obj);
            }

            // 返回所有对象
            foreach (var obj in _rentedObjects)
            {
                _pool.Return(obj);
            }

            // 验证池大小不超过默认值
            Assert.LessOrEqual(_pool.Count, _settings.DefaultPoolSize);
        }

        [Test]
        public void ReturnInvalidObject_ShouldHandleGracefully()
        {
            var invalidObj = new GameObject("InvalidObject");
            Assert.Throws<InvalidPoolObjectException>(() => _pool.Return(invalidObj));
            Object.Destroy(invalidObj);
        }

        [Test]
        public void StressTest_WithMixedOperations()
        {
            const int operationCount = 1000;
            var random = new System.Random();

            for (int i = 0; i < operationCount; i++)
            {
                if (random.NextDouble() < 0.7) // 70% 获取操作
                {
                    var obj = _pool.Get();
                    Assert.NotNull(obj);
                    _rentedObjects.Add(obj);
                }
                else if (_rentedObjects.Count > 0) // 30% 返回操作
                {
                    int index = random.Next(_rentedObjects.Count);
                    var obj = _rentedObjects[index];
                    _pool.Return(obj);
                    _rentedObjects.RemoveAt(index);
                }
            }

            Assert.LessOrEqual(_rentedObjects.Count, operationCount);
        }

        [Test]
        public async Task ParallelStressTest_WithMixedOperations()
        {
            const int taskCount = 8;
            const int operationsPerTask = 200;
            var tasks = new Task[taskCount];
            var random = new System.Random();
            var localLists = new List<GameObject>[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                var localList = new List<GameObject>();
                localLists[i] = localList;

                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < operationsPerTask; j++)
                    {
                        if (random.NextDouble() < 0.7)
                        {
                            var obj = _pool.Get();
                            Assert.NotNull(obj);
                            lock (localList)
                            {
                                localList.Add(obj);
                            }
                        }
                        else if (localList.Count > 0)
                        {
                            GameObject obj = null;
                            lock (localList)
                            {
                                if (localList.Count > 0)
                                {
                                    obj = localList[0];
                                    localList.RemoveAt(0);
                                }
                            }
                            if (obj != null)
                            {
                                _pool.Return(obj);
                            }
                        }
                    }
                });
            }

            await Task.WhenAll(tasks);

            // 清理剩余对象
            foreach (var list in localLists)
            {
                foreach (var obj in list)
                {
                    _pool.Return(obj);
                }
            }
        }

        [Test]
        public void PrewarmAndClear_ShouldMaintainConsistency()
        {
            // 使用默认的预热大小
            int prewarmCount = _settings.PrewarmSize;
            
            // 预热
            _pool.Prewarm(prewarmCount);
            Assert.AreEqual(prewarmCount, _pool.Count);

            // 获取一些对象
            for (int i = 0; i < prewarmCount / 2; i++)
            {
                var obj = _pool.Get();
                _rentedObjects.Add(obj);
            }

            // 返回一些对象
            for (int i = 0; i < _rentedObjects.Count / 2; i++)
            {
                _pool.Return(_rentedObjects[i]);
            }

            // 清理
            _pool.Clear();
            Assert.AreEqual(0, _pool.Count);
        }
    }
} 