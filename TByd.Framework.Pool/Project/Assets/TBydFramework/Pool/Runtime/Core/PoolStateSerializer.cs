using UnityEngine;
using System;
using System.Collections.Generic;

namespace TBydFramework.Pool.Runtime.Core
{
    public static class PoolStateSerializer
    {
        [Serializable]
        private class PoolState
        {
            public string Name;
            public int Count;
            public int MaxSize;
            public DateTime LastAccessed;
        }

        [Serializable]
        private class PoolStateCollection
        {
            public List<PoolState> States = new List<PoolState>();
        }

        public static void SavePoolStates()
        {
            var states = new PoolStateCollection();
            foreach (var poolName in PoolRegistry.GetActivePoolNames())
            {
                var pool = PoolRegistry.GetPoolInfo(poolName);
                if (pool != null)
                {
                    states.States.Add(new PoolState
                    {
                        Name = pool.Name,
                        Count = pool.Count,
                        MaxSize = pool.MaxSize,
                        LastAccessed = DateTime.Now
                    });
                }
            }

            var json = JsonUtility.ToJson(states);
            PlayerPrefs.SetString("PoolStates", json);
            PlayerPrefs.Save();
        }

        public static void LoadPoolStates()
        {
            if (!PlayerPrefs.HasKey("PoolStates")) return;

            var json = PlayerPrefs.GetString("PoolStates");
            var states = JsonUtility.FromJson<PoolStateCollection>(json);

            foreach (var state in states.States)
            {
                Debug.Log($"Restored pool state: {state.Name} - Count: {state.Count}");
                // 这里可以根据需要恢复池状态
            }
        }
    }
} 