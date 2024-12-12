using System;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Diagnostics
{
    /// <summary>
    /// 对象池统计信息
    /// </summary>
    public class PoolStatistics
    {
        private DateTime _startTime;
        private float _uptime;
        private float _latency;
        
        // 修改为可写属性
        public int TotalCreated { get; set; }
        public int TotalDestroyed { get; set; }
        public int TotalGets { get; set; }
        public int TotalReturns { get; set; }
        public int TotalReturned { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int TotalSize { get; set; }
        public int CurrentSize { get; set; }
        public int PeakSize { get; set; }
        public int MissCount { get; set; }
        public float Uptime => _uptime;
        
        public PoolStatistics()
        {
            Reset();
        }

        public void UpdateUptime()
        {
            _uptime = (float)(DateTime.Now - _startTime).TotalSeconds;
        }

        public void UpdateLatency(float latency)
        {
            _latency = latency;
        }

        public void Reset()
        {
            TotalCreated = 0;
            TotalDestroyed = 0;
            TotalGets = 0;
            TotalReturns = 0;
            TotalReturned = 0;
            ActiveCount = 0;
            InactiveCount = 0;
            TotalSize = 0;
            CurrentSize = 0;
            PeakSize = 0;
            MissCount = 0;
            _startTime = DateTime.Now;
            _uptime = 0;
            _latency = 0;
        }

        public void IncrementGet()
        {
            TotalGets++;
            ActiveCount++;
            CurrentSize = ActiveCount + InactiveCount;
            PeakSize = Mathf.Max(PeakSize, CurrentSize);
        }

        public void IncrementCreated()
        {
            TotalCreated++;
            CurrentSize++;
            PeakSize = Mathf.Max(PeakSize, CurrentSize);
        }

        public void IncrementReturn()
        {
            TotalReturns++;
            TotalReturned++;
            ActiveCount--;
            InactiveCount++;
        }
    }
} 