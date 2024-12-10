using System;

namespace TBydFramework.Pool.Runtime.Diagnostics
{
    /// <summary>
    /// 对象池统计信息
    /// </summary>
    public class PoolStatistics
    {
        public int CurrentSize { get; set; }
        public int PeakSize { get; set; }
        public int TotalCreated { get; set; }
        public int TotalReturned { get; set; }
        public int MissCount { get; set; }
        public float AverageLatency { get; set; }
        public TimeSpan Uptime { get; set; }
        public DateTime StartTime { get; set; }

        public PoolStatistics()
        {
            StartTime = DateTime.UtcNow;
            Reset();
        }

        public void Reset()
        {
            CurrentSize = 0;
            PeakSize = 0;
            TotalCreated = 0;
            TotalReturned = 0;
            MissCount = 0;
            AverageLatency = 0;
            Uptime = TimeSpan.Zero;
        }

        public void UpdateLatency(float latency)
        {
            if (TotalCreated == 0)
            {
                AverageLatency = latency;
            }
            else
            {
                AverageLatency = (AverageLatency * TotalCreated + latency) / (TotalCreated + 1);
            }
        }

        public void UpdateUptime()
        {
            Uptime = DateTime.UtcNow - StartTime;
        }
    }
} 