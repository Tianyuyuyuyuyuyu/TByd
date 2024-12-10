using NUnit.Framework;
using TBydFramework.Pool.Runtime.Core;
using TBydFramework.Pool.Runtime.Config;
using UnityEngine;
using System.Threading.Tasks;

namespace TBydFramework.Pool.Tests
{
    public class ResourcePoolBasicTests
    {
        private ResourcePool<Material> _pool;
        private Material _originalMaterial;
        private PoolSettings _settings;

        [SetUp]
        public void Setup()
        {
            _settings = ScriptableObject.CreateInstance<PoolSettings>();
            _pool = new ResourcePool<Material>("TestMaterialPath", _settings);
            _originalMaterial = new Material(Shader.Find("Standard"));
        }

        [TearDown]
        public void Teardown()
        {
            if (_originalMaterial != null)
            {
                Object.Destroy(_originalMaterial);
            }
            _pool.Clear();
            if (_settings != null)
            {
                Object.Destroy(_settings);
            }
        }

        [Test]
        public void Get_ShouldCreateNewInstance()
        {
            var instance = _pool.Get();
            Assert.NotNull(instance);
            Assert.AreNotEqual(_originalMaterial.GetInstanceID(), instance.GetInstanceID());
            _pool.Return(instance);
        }

        [Test]
        public void Return_ShouldStoreInstance()
        {
            var instance = _pool.Get();
            _pool.Return(instance);
            Assert.AreEqual(1, _pool.Count);
            
            var reusedInstance = _pool.Get();
            Assert.AreSame(instance, reusedInstance);
        }

        [Test]
        public void PoolSize_ShouldRespectDefaultSize()
        {
            const int createCount = 20;
            var instances = new Material[createCount];

            for (int i = 0; i < createCount; i++)
            {
                instances[i] = _pool.Get();
            }

            foreach (var instance in instances)
            {
                _pool.Return(instance);
            }

            Assert.LessOrEqual(_pool.Count, _settings.DefaultPoolSize);
        }

        [Test]
        public async Task ConcurrentOperations_ShouldBeThreadSafe()
        {
            const int taskCount = 4;
            const int operationsPerTask = 100;
            var tasks = new Task[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < operationsPerTask; j++)
                    {
                        var instance = _pool.Get();
                        Assert.NotNull(instance);
                        _pool.Return(instance);
                    }
                });
            }

            await Task.WhenAll(tasks);
            Assert.LessOrEqual(_pool.Count, _settings.DefaultPoolSize);
        }
    }
} 