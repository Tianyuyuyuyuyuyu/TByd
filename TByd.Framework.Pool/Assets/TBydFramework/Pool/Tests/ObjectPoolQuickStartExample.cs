using TBydFramework.Pool.Runtime.Core;
using UnityEngine;

namespace TBydFramework.Pool.Tests
{
    public class ObjectPoolQuickStartExample : MonoBehaviour
    {
        private ObjectPool<GameObject> _cubePool;

        void Start()
        {
            // 初始化对象池
            _cubePool = new ObjectPool<GameObject>(
                createFunc: CreateCube,
                onRent: cube => cube.SetActive(true),
                onReturn: cube => cube.SetActive(false),
                onDestroy: Destroy
            );

            // 预热对象池
            _cubePool.Prewarm(10);

            // 从池中租用对象
            for (int i = 0; i < 5; i++)
            {
                GameObject cube = _cubePool.Rent();
                cube.transform.position = new Vector3(i * 2, 0, 0);
            }

            // 模拟延迟后归还对象
            Invoke(nameof(ReturnObjects), 3f);
        }

        private GameObject CreateCube()
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.AddComponent<PooledObjectExample>();
            return cube;
        }

        private void ReturnObjects()
        {
            PooledObjectExample[] pooledObjects = FindObjectsOfType<PooledObjectExample>();
            foreach (var pooledObject in pooledObjects)
            {
                _cubePool.Return(pooledObject.gameObject);
            }

            Debug.Log($"对象池中的对象数量: {_cubePool.Count}");
        }

        void OnDestroy()
        {
            _cubePool.Dispose();
        }
    }
}
