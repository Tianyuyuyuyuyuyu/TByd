using NUnit.Framework;
using TBydFramework.Pool.Runtime.Core;
using UnityEngine;

namespace TBydFramework.Pool.Tests
{
    public class SharedGameObjectPoolBasicTests
    {
        private GameObject _prefab;
        private Transform _parent;

        [SetUp]
        public void Setup()
        {
            _prefab = new GameObject("TestPrefab");
            _prefab.AddComponent<PooledObjectExample>();
            _parent = new GameObject("PoolParent").transform;
        }

        [TearDown]
        public void Teardown()
        {
            if (_prefab != null) Object.Destroy(_prefab);
            if (_parent != null) Object.Destroy(_parent.gameObject);
            SharedGameObjectPool.ClearAll();
        }

        [Test]
        public void Rent_ShouldCreateNewInstance()
        {
            var instance = SharedGameObjectPool.Rent(_prefab);
            
            Assert.NotNull(instance);
            Assert.IsTrue(instance.activeSelf);
            Assert.AreEqual("TestPrefab(Clone)", instance.name);
            
            SharedGameObjectPool.Return(instance);
        }

        [Test]
        public void Return_ShouldDeactivateAndStore()
        {
            var instance = SharedGameObjectPool.Rent(_prefab);
            SharedGameObjectPool.Return(instance);
            
            Assert.IsFalse(instance.activeSelf);
            Assert.AreEqual(1, SharedGameObjectPool.GetPoolSize(_prefab));
        }

        [Test]
        public void Prewarm_ShouldCreateSpecifiedInstances()
        {
            const int count = 5;
            SharedGameObjectPool.Prewarm(_prefab, count);
            
            Assert.AreEqual(count, SharedGameObjectPool.GetPoolSize(_prefab));
        }

        [Test]
        public void RentWithTransform_ShouldSetCorrectTransform()
        {
            var position = new Vector3(1, 2, 3);
            var rotation = Quaternion.Euler(30, 60, 90);

            var instance = SharedGameObjectPool.Rent(_prefab, position, rotation, _parent);
            
            Assert.AreEqual(position, instance.transform.position);
            Assert.AreEqual(rotation, instance.transform.rotation);
            Assert.AreEqual(_parent, instance.transform.parent);
            
            SharedGameObjectPool.Return(instance);
        }
    }
} 