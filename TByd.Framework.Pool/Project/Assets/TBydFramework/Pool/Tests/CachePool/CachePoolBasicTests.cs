using NUnit.Framework;
using System;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Tests
{
    public class CachePoolBasicTests
    {
        private CachePool<string, object> _pool;

        [SetUp]
        public void Setup()
        {
            _pool = new CachePool<string, object>(
                capacity: 1000,
                defaultExpiration: TimeSpan.FromSeconds(1),
                onRemove: (key, value) => { }
            );
        }

        [TearDown]
        public void Teardown()
        {
            _pool.Dispose();
        }

        [Test]
        public void GetOrAdd_ShouldAddNewItem()
        {
            var value = _pool.GetOrAdd("key1", k => new object());
            Assert.NotNull(value);
            Assert.IsTrue(_pool.Contains("key1"));
        }

        [Test]
        public void TryGet_ShouldReturnExistingItem()
        {
            var originalValue = new object();
            _pool.Set("key1", originalValue);

            bool success = _pool.TryGet("key1", out var value);
            Assert.IsTrue(success);
            Assert.AreSame(originalValue, value);
        }

        [Test]
        public void Remove_ShouldRemoveItem()
        {
            _pool.Set("key1", new object());
            bool removed = _pool.Remove("key1");

            Assert.IsTrue(removed);
            Assert.IsFalse(_pool.Contains("key1"));
        }

        [Test]
        public void Capacity_ShouldLimitSize()
        {
            const int capacity = 3;
            var limitedPool = new CachePool<string, object>(capacity);

            for (int i = 0; i < capacity + 2; i++)
            {
                limitedPool.Set($"key{i}", new object());
            }

            Assert.LessOrEqual(limitedPool.Count, capacity);
        }

        [Test]
        public void ExpiredItems_ShouldBeRemoved()
        {
            var shortExpirationPool = new CachePool<string, object>(
                defaultExpiration: TimeSpan.FromMilliseconds(50)
            );

            shortExpirationPool.Set("key1", new object());
            System.Threading.Thread.Sleep(100);

            Assert.IsFalse(shortExpirationPool.Contains("key1"));
        }
    }
} 