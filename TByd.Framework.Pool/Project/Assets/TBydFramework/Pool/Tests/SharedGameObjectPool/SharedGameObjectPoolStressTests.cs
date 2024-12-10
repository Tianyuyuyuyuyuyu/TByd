using NUnit.Framework;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Core;
using UnityEngine;

namespace TBydFramework.Pool.Tests
{
    public class SharedGameObjectPoolStressTests
    {
        private GameObject _prefabA;
        private GameObject _prefabB;
        private List<GameObject> _rentedObjects;

        [SetUp]
        public void Setup()
        {
            _prefabA = new GameObject("PrefabA");
            _prefabB = new GameObject("PrefabB");
            _rentedObjects = new List<GameObject>();
        }

        [TearDown]
        public void Teardown()
        {
            foreach (var obj in _rentedObjects)
            {
                if (obj != null)
                {
                    SharedGameObjectPool.Return(obj);
                }
            }
            
            if (_prefabA != null) Object.Destroy(_prefabA);
            if (_prefabB != null) Object.Destroy(_prefabB);
            
            SharedGameObjectPool.ClearAll();
        }

        [Test]
        public void MultiplePoolsRentReturn_ShouldNotInterfere()
        {
            const int iterationsPerPrefab = 100;

            // 交替租用两种预制体
            for (int i = 0; i < iterationsPerPrefab; i++)
            {
                var instanceA = SharedGameObjectPool.Rent(_prefabA);
                var instanceB = SharedGameObjectPool.Rent(_prefabB);
                
                _rentedObjects.Add(instanceA);
                _rentedObjects.Add(instanceB);
                
                if (i % 2 == 0)
                {
                    SharedGameObjectPool.Return(instanceA);
                    _rentedObjects.Remove(instanceA);
                }
                if (i % 3 == 0)
                {
                    SharedGameObjectPool.Return(instanceB);
                    _rentedObjects.Remove(instanceB);
                }
            }

            Assert.AreEqual(SharedGameObjectPool.GetPoolSize(_prefabA), iterationsPerPrefab / 2);
            Assert.AreEqual(SharedGameObjectPool.GetPoolSize(_prefabB), iterationsPerPrefab / 3);
        }

        [Test]
        public void RapidRentReturn_ShouldMaintainPoolIntegrity()
        {
            const int iterations = 1000;
            SharedGameObjectPool.Prewarm(_prefabA, 10);

            for (int i = 0; i < iterations; i++)
            {
                var instance = SharedGameObjectPool.Rent(_prefabA);
                _rentedObjects.Add(instance);
                
                if (_rentedObjects.Count > 20)
                {
                    var objToReturn = _rentedObjects[0];
                    SharedGameObjectPool.Return(objToReturn);
                    _rentedObjects.RemoveAt(0);
                }
            }

            foreach (var obj in _rentedObjects)
            {
                SharedGameObjectPool.Return(obj);
            }
            
            Assert.Greater(SharedGameObjectPool.GetPoolSize(_prefabA), 0);
            Assert.LessOrEqual(SharedGameObjectPool.GetPoolSize(_prefabA), 30);
        }
    }
} 