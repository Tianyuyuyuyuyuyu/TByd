using UnityEngine;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Samples.BasicObjectPool
{
    /// <summary>
    /// 基础对象池示例
    /// 展示如何使用ObjectPool<T>来管理普通C#对象
    /// </summary>
    /// <remarks>
    /// 性能优化要点：
    /// 1. 使用对象池避免频繁的内存分配
    /// 2. 实现对象重置以确保状态清理
    /// 3. 预热池以减少运行时分配
    /// 4. 合理配置池容量避免自动扩容
    /// </remarks>
    public class BasicObjectPoolExample : MonoBehaviour
    {
        private ObjectPool<PooledItem> _pool;
        private PooledItem[] _activeItems = new PooledItem[0];
        
        [System.Serializable]
        private class PooledItem
        {
            public int Id { get; set; }
            public float CreationTime { get; private set; }
            
            public void Initialize()
            {
                CreationTime = Time.time;
            }
            
            public void Reset()
            {
                Id = 0;
            }
            
            public override string ToString() => 
                $"Item {Id} (Created: {CreationTime:F2}s)";
        }

        private void Start()
        {
            // 配置并创建对象池
            _pool = new ObjectPool<PooledItem>(
                createFunc: () => 
                {
                    var item = new PooledItem();
                    item.Initialize();
                    return item;
                },
                actionOnGet: item => Debug.Log($"Getting {item}"),
                actionOnReturn: item => 
                {
                    item.Reset();
                    Debug.Log($"Returning {item}");
                },
                actionOnDestroy: item => Debug.Log($"Destroying {item}"),
                defaultCapacity: 10,
                maxSize: 20
            );

            // 预热池
            _pool.Prewarm(5);
            LogPoolStatus("Pool initialized and prewarmed");
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, Screen.height - 20));
            GUILayout.Label("Basic Object Pool Example", GUI.skin.box);
            
            GUILayout.Label($"Pool Status: {_pool.Count} available, " +
                          $"{_activeItems.Length} active");

            if (GUILayout.Button("Get Item"))
            {
                GetNewItem();
            }

            if (GUILayout.Button("Return All Items"))
            {
                ReturnAllItems();
            }

            if (GUILayout.Button("Clear Pool"))
            {
                ClearPool();
            }

            // 显示活动对象列表
            GUILayout.Label("Active Items:", GUI.skin.box);
            foreach (var item in _activeItems)
            {
                GUILayout.Label(item.ToString());
            }

            GUILayout.EndArea();
        }

        private void GetNewItem()
        {
            var item = _pool.Get();
            item.Id = Random.Range(1000, 10000);
            
            var list = new System.Collections.Generic.List<PooledItem>(_activeItems)
            {
                item
            };
            _activeItems = list.ToArray();
            
            LogPoolStatus($"Got item {item}");
        }

        private void ReturnAllItems()
        {
            foreach (var item in _activeItems)
            {
                _pool.Return(item);
            }
            _activeItems = new PooledItem[0];
            LogPoolStatus("Returned all items");
        }

        private void ClearPool()
        {
            ReturnAllItems();
            _pool.Clear();
            LogPoolStatus("Pool cleared");
        }

        private void LogPoolStatus(string message)
        {
            Debug.Log($"[BasicObjectPool] {message} - " +
                     $"Available: {_pool.Count}, Active: {_activeItems.Length}");
        }

        private void OnDestroy()
        {
            if (_pool != null)
            {
                ReturnAllItems();
                _pool.Clear();
            }
        }
    }
} 