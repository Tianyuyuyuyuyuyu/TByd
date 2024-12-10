#if TBYD_UNITASK_SUPPORT
using NUnit.Framework;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TBydFramework.Pool.Runtime.External;

namespace TBydFramework.Pool.Tests
{
    public class AsyncObjectPoolTests
    {
        private class TestObject
        {
            public int Value { get; set; }
            public bool IsInitialized { get; set; }
        }

        private AsyncObjectPool<TestObject> _pool;

        [SetUp]
        public void Setup()
        {
            _pool = new AsyncObjectPool<TestObject>(
                createFunc: async ct =>
                {
                    await UniTask.Delay(100, cancellationToken: ct);
                    return new TestObject { IsInitialized = true };
                },
                onRent: obj => obj.Value = 0,
                onReturn: obj => obj.Value = -1
            );
        }

        [TearDown]
        public void Teardown()
        {
            _pool.Dispose();
        }

        [Test]
        public async UniTask RentAsync_ShouldCreateNewObject()
        {
            var obj = await _pool.RentAsync(CancellationToken.None);
            
            Assert.NotNull(obj);
            Assert.IsTrue(obj.IsInitialized);
            Assert.AreEqual(0, obj.Value);
            
            _pool.Return(obj);
        }

        [Test]
        public async UniTask Return_ShouldResetAndStore()
        {
            var obj = await _pool.RentAsync(CancellationToken.None);
            obj.Value = 42;
            
            _pool.Return(obj);
            
            var reusedObj = await _pool.RentAsync(CancellationToken.None);
            Assert.AreSame(obj, reusedObj);
            Assert.AreEqual(0, reusedObj.Value);
        }

        [Test]
        public async UniTask PrewarmAsync_ShouldCreateSpecifiedNumber()
        {
            await _pool.PrewarmAsync(5);
            Assert.AreEqual(5, _pool.Count);
        }

        [Test]
        public void Cancellation_ShouldThrowOperationCanceledException()
        {
            var cts = new CancellationTokenSource();
            var task = _pool.RentAsync(cts.Token);
            
            cts.Cancel();
            
            Assert.ThrowsAsync<OperationCanceledException>(async () => 
                await task
            );
        }
    }
}
#endif 