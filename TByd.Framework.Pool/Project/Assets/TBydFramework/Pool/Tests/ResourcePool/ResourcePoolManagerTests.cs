using NUnit.Framework;
using TBydFramework.Pool.Runtime.Core;
using UnityEngine;

namespace TBydFramework.Pool.Tests
{
    public class ResourcePoolManagerTests
    {
        private ResourcePoolManager _manager;
        private Material _originalMaterial;
        private AudioClip _originalClip;

        [SetUp]
        public void Setup()
        {
            var go = new GameObject("[ResourcePoolManager]");
            _manager = go.AddComponent<ResourcePoolManager>();
            
            _originalMaterial = new Material(Shader.Find("Standard"));
            _originalClip = AudioClip.Create("TestClip", 44100, 1, 44100, false);
        }

        [TearDown]
        public void Teardown()
        {
            if (_originalMaterial != null) Object.Destroy(_originalMaterial);
            if (_originalClip != null) Object.Destroy(_originalClip);
            if (_manager != null) Object.Destroy(_manager.gameObject);
        }

        [Test]
        public void GetPool_ShouldReturnCorrectTypePool()
        {
            var materialPool = _manager.GetPool<Material>();
            var audioPool = _manager.GetPool<AudioClip>();

            Assert.NotNull(materialPool);
            Assert.NotNull(audioPool);
            Assert.AreNotSame(materialPool, audioPool);
        }

        [Test]
        public void GetPool_ShouldReturnSameInstanceForSameType()
        {
            var pool1 = _manager.GetPool<Material>();
            var pool2 = _manager.GetPool<Material>();

            Assert.AreSame(pool1, pool2);
        }

        [Test]
        public void ClearPool_ShouldClearSpecificTypePool()
        {
            var materialPool = _manager.GetPool<Material>();
            var instance = materialPool.Get();
            materialPool.Return(instance);

            _manager.ClearPool<Material>();
            Assert.AreEqual(0, materialPool.Count);
        }

        [Test]
        public void ClearAllPools_ShouldClearAllPools()
        {
            var materialPool = _manager.GetPool<Material>();
            var audioPool = _manager.GetPool<AudioClip>();

            var matInstance = materialPool.Get();
            var audioInstance = audioPool.Get();

            materialPool.Return(matInstance);
            audioPool.Return(audioInstance);

            _manager.ClearAllPools();

            Assert.AreEqual(0, materialPool.Count);
            Assert.AreEqual(0, audioPool.Count);
        }
    }
} 