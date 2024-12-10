using NUnit.Framework;
using System;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Tests
{
    public class EventPoolBasicTests
    {
        private class TestEventArgs : EventArgs
        {
            public int Value { get; set; }
            public string Message { get; set; }
        }

        private EventPool<TestEventArgs> _pool;

        [SetUp]
        public void Setup()
        {
            _pool = new EventPool<TestEventArgs>(
                maxSize: 32,
                resetAction: args =>
                {
                    args.Value = 0;
                    args.Message = null;
                }
            );
        }

        [TearDown]
        public void Teardown()
        {
            _pool.Dispose();
        }

        [Test]
        public void Get_ShouldReturnResetInstance()
        {
            var args = _pool.Get();
            Assert.NotNull(args);
            Assert.AreEqual(0, args.Value);
            Assert.IsNull(args.Message);
        }

        [Test]
        public void Return_ShouldResetAndStore()
        {
            var args = _pool.Get();
            args.Value = 42;
            args.Message = "Test";
            
            _pool.Return(args);
            
            var reusedArgs = _pool.Get();
            Assert.AreSame(args, reusedArgs);
            Assert.AreEqual(0, reusedArgs.Value);
            Assert.IsNull(reusedArgs.Message);
        }

        [Test]
        public void MaxSize_ShouldLimitPoolSize()
        {
            const int maxSize = 32;
            const int createCount = 50;

            var instances = new TestEventArgs[createCount];
            for (int i = 0; i < createCount; i++)
            {
                instances[i] = _pool.Get();
            }

            foreach (var instance in instances)
            {
                _pool.Return(instance);
            }

            Assert.LessOrEqual(_pool.Count, maxSize);
        }
    }
} 