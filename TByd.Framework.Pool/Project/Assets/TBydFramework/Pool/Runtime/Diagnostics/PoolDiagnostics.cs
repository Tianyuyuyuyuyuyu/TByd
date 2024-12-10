using System;
using System.Collections.Generic;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Diagnostics
{
    public sealed class PoolDiagnostics
    {
        private readonly Dictionary<string, PoolMetrics> _poolMetrics = new();
        private readonly object _syncRoot = new object();

        public void TrackAllocation(string poolName)
        {
            lock (_syncRoot)
            {
                GetOrCreateMetrics(poolName).Allocations++;
            }
        }

        public void TrackReuse(string poolName)
        {
            lock (_syncRoot)
            {
                GetOrCreateMetrics(poolName).Reuses++;
            }
        }

        public PoolMetrics GetMetrics(string poolName)
        {
            lock (_syncRoot)
            {
                return GetOrCreateMetrics(poolName);
            }
        }

        private PoolMetrics GetOrCreateMetrics(string poolName)
        {
            if (!_poolMetrics.TryGetValue(poolName, out var metrics))
            {
                metrics = new PoolMetrics();
                _poolMetrics[poolName] = metrics;
            }
            return metrics;
        }
    }

    public class PoolMetrics
    {
        public long Allocations { get; set; }
        public long Reuses { get; set; }
        public float ReuseRatio => Allocations == 0 ? 0 : (float)Reuses / Allocations;
    }
} 