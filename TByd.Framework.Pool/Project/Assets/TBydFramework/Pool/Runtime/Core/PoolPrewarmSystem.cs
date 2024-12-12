using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Runtime.Core
{
    public class PoolPrewarmSystem : MonoBehaviour
    {
        private static PoolPrewarmSystem _instance;
        private readonly Dictionary<IPoolInfo, PrewarmOperation> _operations = new();
        private const int _itemsPerFrame = 5;

        private class PrewarmOperation
        {
            public IPoolInfo Pool;
            public int TargetCount;
            public int CurrentCount;
            public Action<float> OnProgressChanged;
            public TaskCompletionSource<bool> CompletionSource;
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

        /// <summary>
        /// 预热对象池
        /// </summary>
        public static Task<bool> PrewarmPoolAsync(IPoolInfo pool, int count, Action<float> onProgressChanged = null)
        {
            if (_instance == null)
            {
                var go = new GameObject("PoolPrewarmSystem");
                _instance = go.AddComponent<PoolPrewarmSystem>();
            }

            var operation = new PrewarmOperation
            {
                Pool = pool,
                TargetCount = count,
                CurrentCount = 0,
                OnProgressChanged = onProgressChanged,
                CompletionSource = new TaskCompletionSource<bool>()
            };

            _instance._operations[pool] = operation;

            return operation.CompletionSource.Task;
        }

        private void Update()
        {
            var completedPools = new List<IPoolInfo>();

            foreach (var operation in _operations.Values)
            {
                for (int i = 0; i < _itemsPerFrame; i++)
                {
                    if (operation.CurrentCount >= operation.TargetCount)
                    {
                        operation.CompletionSource.SetResult(true);
                        completedPools.Add(operation.Pool);
                        break;
                    }

                    try
                    {
                        // 预热逻辑
                        operation.Pool.Prewarm(1);
                        operation.CurrentCount++;
                        float progress = operation.CurrentCount / (float)operation.TargetCount;
                        operation.OnProgressChanged?.Invoke(progress);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Error during pool prewarm: {e.Message}");
                        operation.CompletionSource.SetResult(false);
                        completedPools.Add(operation.Pool);
                        break;
                    }
                }
            }

            foreach (var pool in completedPools)
            {
                _operations.Remove(pool);
            }
        }
    }
} 