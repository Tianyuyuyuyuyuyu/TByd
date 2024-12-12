using UnityEngine;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Samples.SharedPool
{
    /// <summary>
    /// 共享对象池示例
    /// 展示如何在多个组件间共享同一个对象池
    /// </summary>
    /// <remarks>
    /// 线程安全说明：
    /// 1. 使用静态锁确保池的单例初始化
    /// 2. 使用Interlocked进行原子计数
    /// 3. 每个用户维护独立的对象引用
    /// 4. 资源释放时确保线程安全清理
    /// </remarks>
    public class SharedPoolExample : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        private static GameObjectPool _sharedPool;
        private Transform _spawnParent;

        [Header("Spawn Settings")]
        [SerializeField] private float _spawnRadius = 3f;
        [SerializeField] private float _rotationSpeed = 30f;
        [SerializeField] private Color _userColor = Color.white;

        private static readonly object _lock = new object();
        private static int _instanceCount = 0;
        private int _userId;

        private void Start()
        {
            if (_prefab == null)
            {
                Debug.LogError("请指定预制体!");
                return;
            }

            // 线程安全的实例计数
            _userId = System.Threading.Interlocked.Increment(ref _instanceCount);
            
            // 创建父物体用于组织层级
            _spawnParent = new GameObject($"[User {_userId}] Pool Objects").transform;
            _spawnParent.SetParent(transform);
            
            // 线程安全的池初始化
            lock (_lock)
            {
                if (_sharedPool == null)
                {
                    _sharedPool = new GameObjectPool(
                        prefab: _prefab,
                        defaultCapacity: 20
                    );
                    _sharedPool.Prewarm(5);
                    Debug.Log($"[User {_userId}] Created new shared pool");
                }
            }
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
            if (_sharedPool == null) return;

            float yOffset = (_userId - 1) * 120f;
            GUILayout.BeginArea(new Rect(10, 10 + yOffset, 300, 110));
            
            GUI.backgroundColor = _userColor;
            GUILayout.Label($"Shared Pool User {_userId}", GUI.skin.box);
            GUI.backgroundColor = Color.white;
            
            GUILayout.Label($"Total Pool Size: {_sharedPool.Count}, " +
                          $"My Active Objects: {_spawnParent.childCount}");

            if (GUILayout.Button("Spawn Object"))
            {
                SpawnObject();
            }

            if (GUILayout.Button("Return My Objects"))
            {
                ReturnAllObjects();
            }

            GUILayout.EndArea();
        }

        private void SpawnObject()
        {
            var instance = _sharedPool.Get();
            instance.transform.SetParent(_spawnParent);
            
            // 随机位置和旋转
            var angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var position = new Vector3(
                Mathf.Cos(angle) * _spawnRadius,
                0f,
                Mathf.Sin(angle) * _spawnRadius
            );
            instance.transform.localPosition = position;
            instance.transform.localRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            // 设置颜色以区分不同用户
            var renderer = instance.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = _userColor;
            }
            
            LogStatus($"Spawned {instance.name}");
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
                _sharedPool.Return(child.gameObject);
            }
            
            LogStatus("Returned all objects");
        }

        private void LogStatus(string message)
        {
            Debug.Log($"[User {_userId}] {message} - " +
                     $"Pool Size: {_sharedPool.Count}, My Active: {_spawnParent.childCount}");
        }

        private void OnDestroy()
        {
            ReturnAllObjects();
            System.Threading.Interlocked.Decrement(ref _instanceCount);
            
            // 当最后一个用户销毁时清理共享池
            if (_instanceCount == 0)
            {
                lock (_lock)
                {
                    if (_sharedPool != null)
                    {
                        _sharedPool.Clear();
                        _sharedPool = null;
                        Debug.Log("Shared pool cleared and destroyed");
                    }
                }
            }
        }
    }
} 