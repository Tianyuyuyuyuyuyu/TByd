using NUnit.Framework;
using TBydFramework.Pool.Runtime.Core;
using TBydFramework.Pool.Runtime.Config;
using UnityEngine;

namespace TBydFramework.Pool.Tests
{
    public class ObjectPoolBasicTests
    {
        private class TestObject
        {
            public int Value { get; set; }
        }

        private ObjectPool<TestObject> _pool;
        private PoolSettings _settings;

        [SetUp]
        public void Setup()
        {
            _settings = ScriptableObject.CreateInstance<PoolSettings>();
            _pool = new ObjectPool<TestObject>(
                () => new TestObject(),
                _settings
            );
        }

        [TearDown]
        public void Teardown()
        {
            _pool.Clear();
            if (_settings != null)
            {
                Object.Destroy(_settings);
            }
        }

        [Test]
        public void Get_ShouldCreateNewObject()
        {
            var obj = _pool.Get();
            Assert.NotNull(obj);
            Assert.AreEqual(0, obj.Value);
        }

        [Test]
        public void Return_ShouldStoreObject()
        {
            var obj = _pool.Get();
            obj.Value = 42;
            _pool.Return(obj);
            
            Assert.AreEqual(1, _pool.Count);
            
            var reusedObj = _pool.Get();
            Assert.AreSame(obj, reusedObj);
            Assert.AreEqual(0, reusedObj.Value); // 确保对象被重置
        }

        [Test]
        public void Prewarm_ShouldCreateSpecifiedNumber()
        {
            const int prewarmCount = 5;
            _pool.Prewarm(prewarmCount);
            Assert.AreEqual(prewarmCount, _pool.Count);
        }

        [Test]
        public void Clear_ShouldRemoveAllObjects()
        {
            _pool.Prewarm(5);
            _pool.Clear();
            Assert.AreEqual(0, _pool.Count);
        }
    }
} 