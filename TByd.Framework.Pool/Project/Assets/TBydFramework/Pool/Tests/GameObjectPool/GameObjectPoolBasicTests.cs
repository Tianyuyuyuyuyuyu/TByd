using NUnit.Framework;
using TBydFramework.Pool.Runtime.Core;
using TBydFramework.Pool.Runtime.Config;
using UnityEngine;

namespace TBydFramework.Pool.Tests
{
    public class GameObjectPoolBasicTests
    {
        private GameObject _prefab;
        private GameObjectPool _pool;
        private PoolSettings _settings;

        [SetUp]
        public void Setup()
        {
            _prefab = new GameObject("TestPrefab");
            _settings = ScriptableObject.CreateInstance<PoolSettings>();
            _pool = new GameObjectPool(_prefab, _settings);
        }

        [TearDown]
        public void Teardown()
        {
            _pool.Clear();
            if (_prefab != null)
            {
                Object.Destroy(_prefab);
            }
            if (_settings != null)
            {
                Object.Destroy(_settings);
            }
        }

        [Test]
        public void Get_ShouldCreateInstance()
        {
            var instance = _pool.Get();
            Assert.NotNull(instance);
            Assert.IsTrue(instance.activeSelf);
            Assert.AreEqual("TestPrefab(Clone)", instance.name);
            _pool.Return(instance);
        }

        [Test]
        public void Return_ShouldDeactivateAndStore()
        {
            var instance = _pool.Get();
            _pool.Return(instance);
            
            Assert.IsFalse(instance.activeSelf);
            Assert.AreEqual(1, _pool.Count);
        }

        [Test]
        public void GetWithTransform_ShouldSetCorrectTransform()
        {
            var position = new Vector3(1, 2, 3);
            var rotation = Quaternion.Euler(30, 60, 90);
            var parent = new GameObject("Parent").transform;

            var instance = _pool.Get();
            instance.transform.SetPositionAndRotation(position, rotation);
            instance.transform.SetParent(parent);
            
            Assert.AreEqual(position, instance.transform.position);
            Assert.AreEqual(rotation, instance.transform.rotation);
            Assert.AreEqual(parent, instance.transform.parent);
            
            _pool.Return(instance);
            Object.Destroy(parent.gameObject);
        }

        [Test]
        public void Prewarm_ShouldCreateInactiveInstances()
        {
            const int count = 5;
            _pool.Prewarm(count);
            
            Assert.AreEqual(count, _pool.Count);
            
            var instance = _pool.Get();
            Assert.IsTrue(instance.activeSelf);
            _pool.Return(instance);
        }
    }
} 