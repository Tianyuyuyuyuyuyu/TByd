using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Runtime.Core
{
    public class PoolPrewarmManager : MonoBehaviour
    {
        private static PoolPrewarmManager _instance;
        private readonly Dictionary<string, PrewarmTask> _prewarmTasks = new Dictionary<string, PrewarmTask>();

        private class PrewarmTask
        {
            public int TargetCount;
            public int CurrentCount;
            public bool IsCompleted;
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static async Task PrewarmPoolAsync(string poolName, int count)
        {
            if (_instance == null)
            {
                var go = new GameObject("PoolPrewarmManager");
                _instance = go.AddComponent<PoolPrewarmManager>();
            }

            var task = new PrewarmTask
            {
                TargetCount = count,
                CurrentCount = 0,
                IsCompleted = false
            };

            _instance._prewarmTasks[poolName] = task;

            while (!task.IsCompleted)
            {
                await Task.Yield();
            }
        }

        private void Update()
        {
            foreach (var kvp in _prewarmTasks)
            {
                var poolName = kvp.Key;
                var task = kvp.Value;

                if (task.IsCompleted) continue;

                var pool = PoolRegistry.GetPoolInfo(poolName);
                if (pool == null) continue;

                // 每帧预热一定数量的对象
                const int itemsPerFrame = 5;
                for (int i = 0; i < itemsPerFrame; i++)
                {
                    if (task.CurrentCount >= task.TargetCount)
                    {
                        task.IsCompleted = true;
                        break;
                    }

                    // 执行预热逻辑
                    pool.Prewarm(1);
                    task.CurrentCount++;
                }
            }
        }
    }
} 