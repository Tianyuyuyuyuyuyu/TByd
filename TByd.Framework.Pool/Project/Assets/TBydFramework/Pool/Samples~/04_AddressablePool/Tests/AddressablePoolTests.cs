#if TBYD_ADDRESSABLES_SUPPORT
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.AddressableAssets;
using TBydFramework.Pool.Samples.AddressablePool;

namespace TBydFramework.Pool.Samples.AddressablePool.Tests
{
    public class AddressablePoolTests
    {
        private GameObject _testObject;
        private AddressablePoolExample _poolExample;
        private AssetReferenceGameObject _testReference;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // 创建测试用Addressable引用
            // 注意：在实际测试中需要有效的Addressable资源
            _testReference = new AssetReferenceGameObject("TestPrefabGUID");
        }

        [SetUp]
        public void Setup()
        {
            _testObject = new GameObject("Test Object");
            _poolExample = _testObject.AddComponent<AddressablePoolExample>();
            
            // 通过反射设置Addressable引用
            var referenceField = typeof(AddressablePoolExample).GetField("_prefabReference", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            referenceField.SetValue(_poolExample, _testReference);
        }

        [TearDown]
        public void Teardown()
        {
            if (_testObject != null)
            {
                Object.DestroyImmediate(_testObject);
            }
        }

        [UnityTest]
        public IEnumerator Example_InitializesPoolCorrectly()
        {
            yield return null; // 等待Start执行完成

            var poolField = typeof(AddressablePoolExample).GetField("_pool", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var pool = poolField.GetValue(_poolExample);
            
            Assert.That(pool, Is.Not.Null, "Pool should be initialized");
        }

        [UnityTest]
        public IEnumerator Example_HandlesSpawnAndReturn()
        {
            yield return null;

            // 获取SpawnParent
            var spawnParentField = typeof(AddressablePoolExample).GetField("_spawnParent", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var spawnParent = (Transform)spawnParentField.GetValue(_poolExample);

            // 生成对象
            var spawnMethod = typeof(AddressablePoolExample).GetMethod("SpawnObject", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            spawnMethod.Invoke(_poolExample, null);

            // 等待异步加载完成
            yield return new WaitForSeconds(0.5f);

            // 返回对象
            var returnMethod = typeof(AddressablePoolExample).GetMethod("ReturnAllObjects", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            returnMethod.Invoke(_poolExample, null);

            Assert.That(spawnParent.childCount, Is.EqualTo(0), "All objects should be returned to pool");
        }

        [UnityTest]
        public IEnumerator Example_CleansUpCorrectly()
        {
            yield return null;

            // 生成一些对象
            var spawnMethod = typeof(AddressablePoolExample).GetMethod("SpawnObject", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            spawnMethod.Invoke(_poolExample, null);
            spawnMethod.Invoke(_poolExample, null);

            yield return new WaitForSeconds(0.5f);

            // 清理池
            var clearMethod = typeof(AddressablePoolExample).GetMethod("ClearPool", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            clearMethod.Invoke(_poolExample, null);

            // 获取SpawnParent检查子对象数量
            var spawnParentField = typeof(AddressablePoolExample).GetField("_spawnParent", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var spawnParent = (Transform)spawnParentField.GetValue(_poolExample);

            Assert.That(spawnParent.childCount, Is.EqualTo(0), "Pool should be cleared");
        }
    }
}
#endif 