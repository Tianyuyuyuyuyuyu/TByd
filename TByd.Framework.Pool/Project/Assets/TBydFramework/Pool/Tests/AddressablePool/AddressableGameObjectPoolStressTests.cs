#if TBYD_ADDRESSABLES_SUPPORT
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using TBydFramework.Pool.Runtime.External;

namespace TBydFramework.Pool.Tests
{
    public class AddressableGameObjectPoolStressTests
    {
        private AssetReferenceGameObject _prefabReferenceA;
        private AssetReferenceGameObject _prefabReferenceB;
        private AddressableGameObjectPool _poolA;
        private AddressableGameObjectPool _poolB;
        private List<GameObject> _spawnedObjects;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // 注意：这里需要有效的Addressable资源引用
            _prefabReferenceA = new AssetReferenceGameObject("PrefabAGUID");
            _prefabReferenceB = new AssetReferenceGameObject("PrefabBGUID");
        }

        [SetUp]
        public void Setup()
        {
            _poolA = new AddressableGameObjectPool(_prefabReferenceA);
            _poolB = new AddressableGameObjectPool(_prefabReferenceB);
            _spawnedObjects = new List<GameObject>();
        }

        [TearDown]
        public void Teardown()
        {
            foreach (var obj in _spawnedObjects)
            {
                if (obj != null)
                {
                    var pool = obj.name.Contains("A") ? _poolA : _poolB;
                    pool.Return(obj);
                }
            }
            _poolA.Dispose();
            _poolB.Dispose();
        }

        [Test]
        public void MultiplePoolsRentReturn_ShouldNotInterfere()
        {
            const int iterationsPerPool = 50;

            for (int i = 0; i < iterationsPerPool; i++)
            {
                var instanceA = _poolA.Rent();
                var instanceB = _poolB.Rent();

                _spawnedObjects.Add(instanceA);
                _spawnedObjects.Add(instanceB);

                if (i % 2 == 0)
                {
                    _poolA.Return(instanceA);
                    _spawnedObjects.Remove(instanceA);
                }
                if (i % 3 == 0)
                {
                    _poolB.Return(instanceB);
                    _spawnedObjects.Remove(instanceB);
                }
            }

            Assert.AreEqual(_poolA.Count, iterationsPerPool / 2);
            Assert.AreEqual(_poolB.Count, iterationsPerPool / 3);
        }

        [Test]
        public void RapidRentReturn_ShouldMaintainPoolIntegrity()
        {
            const int iterations = 100;
            _poolA.Prewarm(5);

            for (int i = 0; i < iterations; i++)
            {
                var instance = _poolA.Rent();
                _spawnedObjects.Add(instance);

                if (_spawnedObjects.Count > 10)
                {
                    var objToReturn = _spawnedObjects[0];
                    _poolA.Return(objToReturn);
                    _spawnedObjects.RemoveAt(0);
                }
            }

            foreach (var obj in _spawnedObjects.ToArray())
            {
                _poolA.Return(obj);
                _spawnedObjects.Remove(obj);
            }

            Assert.Greater(_poolA.Count, 0);
            Assert.LessOrEqual(_poolA.Count, 15);
        }
    }
}
#endif 