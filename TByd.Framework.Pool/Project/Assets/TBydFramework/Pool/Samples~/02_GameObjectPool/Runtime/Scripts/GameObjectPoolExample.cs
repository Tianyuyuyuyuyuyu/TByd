using UnityEngine;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Samples.GameObjectPool
{
    /// <summary>
    /// GameObject对象池示例
    /// 展示如何使用GameObjectPool来管理Unity游戏对象
    /// </summary>
    /// <remarks>
    /// 性能优化要点：
    /// 1. 使用对象池减少实例化/销毁开销
    /// 2. 批量处理对象返回操作
    /// 3. 合理设置预热数量避免运行时扩容
    /// 4. 使用父物体组织层级减少场景树搜索
    /// </remarks>
    public class GameObjectPoolExample : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        private GameObjectPool _pool;
        private Transform _spawnParent;

        [Header("Spawn Settings")]
        [SerializeField] private float _spawnRadius = 5f;
        [SerializeField] private float _rotationSpeed = 30f;
        [SerializeField] private int _prewarmCount = 5;

        private void Start()
        {
            if (_prefab == null)
            {
                Debug.LogError("请指定预制体!");
                return;
            }

            // 创建父物体用于组织层级
            _spawnParent = new GameObject($"[{_prefab.name}] Pool Objects").transform;
            _spawnParent.SetParent(transform);
            
            // 配置并创建对象池
            _pool = new GameObjectPool(
                prefab: _prefab,
                parent: _spawnParent,
                defaultCapacity: _prewarmCount * 2
            );
            
            // 预热池
            _pool.Prewarm(_prewarmCount);
            LogPoolStatus("Pool initialized and prewarmed");
        }

        private void Update()
        {
            // 旋转所有活动对象
            foreach (Transform child in _spawnParent)
            {
                child.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
            }
        }

        private void OnGUI()
        {
            if (_pool == null) return;

            GUILayout.BeginArea(new Rect(10, 10, 300, Screen.height - 20));
            GUILayout.Label("GameObject Pool Example", GUI.skin.box);
            
            GUILayout.Label($"Pool Status: {_pool.Count} available, " +
                          $"{_spawnParent.childCount} active");

            if (GUILayout.Button("Spawn Object"))
            {
                SpawnObject();
            }

            if (GUILayout.Button("Return All Objects"))
            {
                ReturnAllObjects();
            }

            if (GUILayout.Button("Clear Pool"))
            {
                ClearPool();
            }

            GUILayout.EndArea();
        }

        private void SpawnObject()
        {
            var instance = _pool.Get();
            
            // 随机位置和旋转
            var randomPos = Random.insideUnitSphere * _spawnRadius;
            randomPos.y = 0;
            instance.transform.position = randomPos;
            instance.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            
            LogPoolStatus($"Spawned {instance.name}");
        }

        private void ReturnAllObjects()
        {
            var children = new Transform[_spawnParent.childCount];
            for (int i = 0; i < _spawnParent.childCount; i++)
            {
                children[i] = _spawnParent.GetChild(i);
            }

            foreach (var child in children)
            {
                _pool.Return(child.gameObject);
            }
            
            LogPoolStatus("Returned all objects");
        }

        private void ClearPool()
        {
            ReturnAllObjects();
            _pool.Clear();
            LogPoolStatus("Pool cleared");
        }

        private void LogPoolStatus(string message)
        {
            Debug.Log($"[GameObjectPool] {message} - " +
                     $"Available: {_pool.Count}, Active: {_spawnParent.childCount}");
        }

        private void OnDestroy()
        {
            if (_pool != null)
            {
                ReturnAllObjects();
                _pool.Clear();
            }
        }
    }
} 