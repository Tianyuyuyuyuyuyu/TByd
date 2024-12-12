#if TBYD_ADDRESSABLES_SUPPORT
using UnityEngine;
using UnityEngine.AddressableAssets;
using TBydFramework.Pool.Runtime.External;

namespace TBydFramework.Pool.Samples.AddressablePool
{
    /// <summary>
    /// Addressable对象池示例
    /// 展示如何使用AddressableGameObjectPool来管理Addressable资源
    /// </summary>
    /// <remarks>
    /// 异步加载说明：
    /// 1. 使用async/await处理异步初始化
    /// 2. 异常处理确保资源加载失败时的安全性
    /// 3. 正确释放Addressable资源避免内存泄漏
    /// 4. 支持运行时动态加载和卸载
    /// </remarks>
    public class AddressablePoolExample : MonoBehaviour
    {
        [SerializeField] private AssetReferenceGameObject _prefabReference;
        private AddressableGameObjectPool _pool;
        private Transform _spawnParent;

        [Header("Spawn Settings")]
        [SerializeField] private float _spawnRadius = 5f;
        [SerializeField] private float _rotationSpeed = 30f;
        [SerializeField] private int _prewarmCount = 3;

        private async void Start()
        {
            if (_prefabReference == null)
            {
                Debug.LogError("请指定Addressable预制体引用!");
                return;
            }

            try 
            {
                // 创建父物体用于组织层级
                _spawnParent = new GameObject($"[{_prefabReference.RuntimeKey}] Pool Objects").transform;
                _spawnParent.SetParent(transform);
                
                // 创建并初始化Addressable对象池
                _pool = new AddressableGameObjectPool(_prefabReference);
                
                // 异步预热池
                await _pool.PrewarmAsync(_prewarmCount);
                LogPoolStatus("Pool initialized and prewarmed");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"初始化对象池失败: {e.Message}");
            }
        }

        private void Update()
        {
            if (_spawnParent == null) return;

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
            GUILayout.Label("Addressable Pool Example", GUI.skin.box);
            
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
            var instance = _pool.Rent();
            if (instance == null) return;

            instance.transform.SetParent(_spawnParent);
            
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
            Debug.Log($"[AddressablePool] {message} - " +
                     $"Available: {_pool.Count}, Active: {_spawnParent.childCount}");
        }

        private void OnDestroy()
        {
            if (_pool != null)
            {
                ReturnAllObjects();
                _pool.Dispose();
            }
        }
    }
}
#endif 