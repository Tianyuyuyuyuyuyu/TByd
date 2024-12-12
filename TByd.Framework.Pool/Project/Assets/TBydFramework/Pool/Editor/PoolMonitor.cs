using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Core;
using TBydFramework.Pool.Runtime.Enums;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Editor
{
    [InitializeOnLoad]
    public static class PoolMonitor
    {
        private static readonly List<IPoolInfo> _activePools = new List<IPoolInfo>();
        private static readonly HashSet<string> _monitoredPools = new HashSet<string>();

        static PoolMonitor()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        private static void OnEditorUpdate()
        {
            if (!Application.isPlaying) return;
            UpdatePoolStatus();
        }

        private static void UpdatePoolStatus()
        {
            _activePools.Clear();
            foreach (var poolName in PoolRegistry.GetActivePoolNames())
            {
                var poolInfo = PoolRegistry.GetPoolInfo(poolName);
                if (poolInfo != null)
                {
                    _activePools.Add(poolInfo);
                }
            }
        }

        public static IReadOnlyList<IPoolInfo> GetActivePools()
        {
            return _activePools;
        }

        public static void StartMonitoring(string poolName)
        {
            _monitoredPools.Add(poolName);
        }

        public static void StopMonitoring(string poolName)
        {
            _monitoredPools.Remove(poolName);
        }
    }
} 