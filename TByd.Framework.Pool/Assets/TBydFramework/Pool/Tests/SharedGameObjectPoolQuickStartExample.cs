using UnityEngine;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Tests
{
    public class SharedGameObjectPoolQuickStartExample : MonoBehaviour
    {
        public GameObject cubePrefab;

        void Start()
        {
            // 预热共享对象池
            SharedGameObjectPool.Prewarm(cubePrefab, 10);

            // 从共享池中租用对象
            for (int i = 0; i < 5; i++)
            {
                GameObject cube = SharedGameObjectPool.Rent(cubePrefab);
                cube.transform.position = new Vector3(i * 2, 0, 0);
                cube.AddComponent<PooledObjectExample>();
            }

            // 模拟延迟后归还对象
            Invoke(nameof(ReturnObjects), 3f);
        }

        private void ReturnObjects()
        {
            PooledObjectExample[] pooledObjects = FindObjectsOfType<PooledObjectExample>();
            foreach (var pooledObject in pooledObjects)
            {
                SharedGameObjectPool.Return(pooledObject.gameObject);
            }

            // 使用GetPoolSize方法获取池中对象的数量
            Debug.Log($"共享对象池中的对象数量: {SharedGameObjectPool.GetPoolSize(cubePrefab)}");
        }
    }
}
