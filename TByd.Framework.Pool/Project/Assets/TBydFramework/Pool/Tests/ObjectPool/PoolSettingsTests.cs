using NUnit.Framework;
using TBydFramework.Pool.Runtime.Config;
using UnityEngine;

namespace TBydFramework.Pool.Tests
{
    public class PoolSettingsTests
    {
        private PoolSettings _settings;

        [SetUp]
        public void Setup()
        {
            _settings = ScriptableObject.CreateInstance<PoolSettings>();
        }

        [TearDown]
        public void Teardown()
        {
            if (_settings != null)
            {
                Object.Destroy(_settings);
            }
        }

        [Test]
        public void DefaultValues_ShouldBeValid()
        {
            Assert.Greater(_settings.DefaultPoolSize, 0, "Default pool size should be positive");
            Assert.GreaterOrEqual(_settings.PrewarmSize, 0, "Prewarm size should be non-negative");
            Assert.Greater(_settings.MaintenanceInterval, 0, "Maintenance interval should be positive");
            Assert.IsTrue(_settings.EnablePooling, "Pooling should be enabled by default");
        }

        [Test]
        public void PrewarmSize_ShouldNotExceedDefaultPoolSize()
        {
            Assert.LessOrEqual(_settings.PrewarmSize, _settings.DefaultPoolSize, 
                "Prewarm size should not exceed default pool size");
        }

        [Test]
        public void Clone_ShouldCreateIndependentCopy()
        {
            var clone = Object.Instantiate(_settings);
            
            Assert.AreEqual(_settings.DefaultPoolSize, clone.DefaultPoolSize);
            Assert.AreEqual(_settings.PrewarmSize, clone.PrewarmSize);
            Assert.AreEqual(_settings.MaintenanceInterval, clone.MaintenanceInterval);
            Assert.AreEqual(_settings.EnablePooling, clone.EnablePooling);
            Assert.AreEqual(_settings.EnableDiagnostics, clone.EnableDiagnostics);
            Assert.AreEqual(_settings.EnableAutoPrewarm, clone.EnableAutoPrewarm);

            Object.Destroy(clone);
        }

        [Test]
        public void Settings_ShouldBeSerializable()
        {
            var json = JsonUtility.ToJson(_settings);
            var deserialized = ScriptableObject.CreateInstance<PoolSettings>();
            JsonUtility.FromJsonOverwrite(json, deserialized);

            Assert.AreEqual(_settings.DefaultPoolSize, deserialized.DefaultPoolSize);
            Assert.AreEqual(_settings.PrewarmSize, deserialized.PrewarmSize);
            Assert.AreEqual(_settings.MaintenanceInterval, deserialized.MaintenanceInterval);

            Object.Destroy(deserialized);
        }
    }
} 