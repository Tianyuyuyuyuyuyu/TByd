#if TBYD_ADDRESSABLES_SUPPORT
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using TBydFramework.Pool.Runtime.External;

namespace TBydFramework.Pool.Tests
{
    public class AddressableGameObjectPoolTests
    {
        private AssetReferenceGameObject _prefabReference;
        private AddressableGameObjectPool _pool;
        private List<GameObject> _spawnedObjects;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // 注意：这里需要一个有效的Addressable资源引用
            _prefabReference = new AssetReferenceGameObject("TestPrefabGUID");
        }

        [SetUp]
        public void Setup()
        {
            _pool = new AddressableGameObjectPool(_prefabReference);
            _spawnedObjects = new List<GameObject>();
        }

        [TearDown]
        public void Teardown()
        {
            foreach (var obj in _spawnedObjects)
            {
                if (obj != null)
                {
                    _pool.Return(obj);
                }
            }
            _pool.Dispose();
        }

        [Test]
        public void Rent_ShouldCreateNewInstance()
        {
            var instance = _pool.Rent();
            _spawnedObjects.Add(instance);

            Assert.NotNull(instance);
            Assert.IsTrue(instance.activeSelf);
        }

        [Test]
        public void Return_ShouldDeactivateAndStore()
        {
            var instance = _pool.Rent();
            _pool.Return(instance);

            Assert.IsFalse(instance.activeSelf);
            Assert.AreEqual(1, _pool.Count);
        }

        [Test]
        public void Prewarm_ShouldCreateSpecifiedInstances()
        {
            const int count = 5;
            _pool.Prewarm(count);

            Assert.AreEqual(count, _pool.Count);
        }

        [Test]
        public void Clear_ShouldReleaseAllInstances()
        {
            const int count = 3;
            _pool.Prewarm(count);

            _pool.Clear();
            Assert.AreEqual(0, _pool.Count);
        }
    }
}
#endif 