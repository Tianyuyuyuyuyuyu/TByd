using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Tests
{
    public class CachePoolStressTests
    {
        private CachePool<string, object> _pool;

        [SetUp]
        public void Setup()
        {
            _pool = new CachePool<string, object>(
                capacity: 10000,
                defaultExpiration: TimeSpan.FromSeconds(5)
            );
        }

        [TearDown]
        public void Teardown()
        {
            _pool.Dispose();
        }

        [Test]
        public async Task ConcurrentAccess_ShouldHandleMultipleThreads()
        {
            const int threadCount = 10;
            const int operationsPerThread = 1000;

            var tasks = new Task[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                int threadId = i;
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < operationsPerThread; j++)
                    {
                        string key = $"key_{threadId}_{j}";
                        _pool.Set(key, new object());
                        
                        if (j % 2 == 0)
                        {
                            _pool.Remove(key);
                        }
                    }
                });
            }

            await Task.WhenAll(tasks);
            Assert.LessOrEqual(_pool.Count, threadCount * operationsPerThread / 2);
        }

        [Test]
        public void RapidAccessAndRemoval_ShouldMaintainConsistency()
        {
            const int iterations = 10000;

            for (int i = 0; i < iterations; i++)
            {
                string key = $"key_{i}";
                _pool.Set(key, new object());

                if (i % 3 == 0)
                {
                    _pool.Remove(key);
                }
                else if (i % 3 == 1)
                {
                    _pool.TryGet(key, out _);
                }
            }

            Assert.LessOrEqual(_pool.Count, iterations * 2 / 3);
        }
    }
} 