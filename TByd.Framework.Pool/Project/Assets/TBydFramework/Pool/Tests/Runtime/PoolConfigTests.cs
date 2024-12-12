using NUnit.Framework;
using UnityEngine;
using TBydFramework.Pool.Runtime.Config;
using TBydFramework.Pool.Runtime.Core;
using System.Collections;
using UnityEngine.TestTools;
using TBydFramework.Pool.Runtime.Enums;

namespace TBydFramework.Pool.Tests
{
    public class PoolConfigTests
    {
        private PoolSettings _settings;
        private GameObject _testPrefab;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testPrefab = new GameObject("TestPrefab");
            Object.DontDestroyOnLoad(_testPrefab);
            _settings = ScriptableObject.CreateInstance<PoolSettings>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Object.DestroyImmediate(_testPrefab);
            Object.DestroyImmediate(_settings);
        }

        [Test]
        public void PoolSettings_ValidatesValues()
        {
            var profile = new PoolSettings.PoolProfile
            {
                Key = "TestPool",
                Prefab = _testPrefab,
                InitialCapacity = -1, // 无效值
                MaxSize = 0, // 无效值
                MaintenanceInterval = -1f // 效值
            };

            _settings.AddProfile(profile);
            _settings.ValidateSettings();

            var validatedProfile = _settings.GetProfile("TestPool");
            Assert.Greater(validatedProfile.InitialCapacity, 0);
            Assert.Greater(validatedProfile.MaxSize, validatedProfile.InitialCapacity);
            Assert.Greater(validatedProfile.MaintenanceInterval, 0f);
        }

        [Test]
        public void PoolConfig_HandlesRuntimeOverrides()
        {
            PoolConfigManager.Initialize(_settings);
            
            const string testKey = "TestSetting";
            const int testValue = 42;
            
            PoolConfigManager.SetRuntimeOverride(testKey, testValue);
            var value = PoolConfigManager.GetSetting<int>(testKey);
            
            Assert.AreEqual(testValue, value);
            
            PoolConfigManager.ClearRuntimeOverrides();
            value = PoolConfigManager.GetSetting<int>(testKey);
            
            Assert.AreEqual(0, value); // 默认值
        }

        [Test]
        public void PoolConfig_HandlesAddressablePools()
        {
            var profile = new PoolSettings.PoolProfile
            {
                Key = "TestAddressablePool",
                UseAddressables = true,
                AddressableKey = "TestAsset",
                Type = PoolType.Addressable,
                MaxSize = 10
            };

            _settings.AddProfile(profile);
            var loadedProfile = _settings.GetProfile("TestAddressablePool");
            
            Assert.IsTrue(loadedProfile.UseAddressables);
            Assert.AreEqual("TestAsset", loadedProfile.AddressableKey);
            Assert.AreEqual(PoolType.Addressable, loadedProfile.Type);
        }

        [Test]
        public void PoolProfile_SupportsAddressables()
        {
            var profile = new PoolSettings.PoolProfile
            {
                Key = "AddressablePool",
                UseAddressables = true,
                AddressableKey = "TestKey",
                Type = PoolType.Addressable
            };

            _settings.AddProfile(profile);
            var loadedProfile = _settings.GetProfile("AddressablePool");
            
            Assert.True(loadedProfile.UseAddressables);
            Assert.AreEqual("TestKey", loadedProfile.AddressableKey);
            Assert.AreEqual(PoolType.Addressable, loadedProfile.Type);
        }

        [UnityTest]
        public IEnumerator PoolConfig_AppliesSettingsToPool()
        {
            var profile = new PoolSettings.PoolProfile
            {
                Key = "ConfigTest",
                Prefab = _testPrefab,
                InitialCapacity = 5,
                MaxSize = 10,
                EnablePrewarm = true
            };

            _settings.AddProfile(profile);
            PoolConfigManager.Initialize(_settings);

            var poolManager = new GameObject().AddComponent<PoolManager>();
            var pool = poolManager.GetGameObjectPool("ConfigTest", _testPrefab);

            yield return new WaitForSeconds(0.1f); // 等待预热完成

            Assert.AreEqual(5, pool.Count);
            Assert.AreEqual(10, pool.MaxSize);

            Object.DestroyImmediate(poolManager.gameObject);
        }
    }
} 