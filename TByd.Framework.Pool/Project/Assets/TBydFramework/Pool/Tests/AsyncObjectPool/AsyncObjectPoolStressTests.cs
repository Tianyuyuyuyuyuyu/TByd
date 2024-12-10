#if TBYD_UNITASK_SUPPORT
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TBydFramework.Pool.Runtime.External;

namespace TBydFramework.Pool.Tests
{
    public class AsyncObjectPoolStressTests
    {
        private class TestObject
        {
            public int Value { get; set; }
            public bool IsInitialized { get; set; }
        }

        private AsyncObjectPool<TestObject> _pool;
        private List<TestObject> _activeObjects;

        [SetUp]
        public void Setup()
        {
            _pool = new AsyncObjectPool<TestObject>(
                createFunc: async ct =>
                {
                    await UniTask.Delay(10, cancellationToken: ct);
                    return new TestObject { IsInitialized = true };
                },
                onRent: obj => obj.Value = 0,
                onReturn: obj => obj.Value = -1
            );
            _activeObjects = new List<TestObject>();
        }

        [TearDown]
        public void Teardown()
        {
            _pool.Dispose();
        }

        [Test]
        public async UniTask MultipleOperations_ShouldHandleConcurrentRequests()
        {
            const int taskCount = 10;
            const int operationsPerTask = 20;

            var tasks = new UniTask[taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = UniTask.Run(async () =>
                {
                    for (int j = 0; j < operationsPerTask; j++)
                    {
                        var obj = await _pool.RentAsync(CancellationToken.None);
                        lock (_activeObjects)
                        {
                            _activeObjects.Add(obj);
                        }

                        await UniTask.Delay(5);

                        if (j % 2 == 0)
                        {
                            lock (_activeObjects)
                            {
                                _activeObjects.Remove(obj);
                            }
                            _pool.Return(obj);
                        }
                    }
                });
            }

            await UniTask.WhenAll(tasks);

            foreach (var obj in _activeObjects.ToArray())
            {
                _pool.Return(obj);
            }

            Assert.LessOrEqual(_pool.Count, taskCount * operationsPerTask / 2);
        }

        [Test]
        public async UniTask CancellationHandling_ShouldNotLeakObjects()
        {
            const int iterations = 100;
            var cts = new CancellationTokenSource();
            var rentedObjects = new List<TestObject>();

            try
            {
                for (int i = 0; i < iterations; i++)
                {
                    if (i == iterations / 2)
                    {
                        cts.Cancel();
                    }

                    try
                    {
                        var obj = await _pool.RentAsync(cts.Token);
                        rentedObjects.Add(obj);
                    }
                    catch (OperationCanceledException)
                    {
                        // Expected for half of the operations
                    }
                }
            }
            finally
            {
                foreach (var obj in rentedObjects)
                {
                    _pool.Return(obj);
                }
            }

            Assert.Greater(_pool.Count, 0);
            Assert.LessOrEqual(_pool.Count, iterations / 2);
        }
    }
}
#endif 