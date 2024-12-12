using System;

namespace TBydFramework.Pool.Runtime.Diagnostics
{
    public interface IPoolDiagnostics
    {
        string Name { get; }
        PoolDiagnosticInfo GetDiagnosticInfo();
        void ResetStatistics();
    }

    public struct PoolDiagnosticInfo
    {
        public int TotalCreated;
        public int TotalDestroyed;
        public int CurrentActive;
        public int CurrentInactive;
        public int PeakActive;
        public float AverageGetTime;
        public float AverageReturnTime;
        public long MemoryUsage;
        public DateTime LastAccessTime;
        public int MissCount;
        public int OverflowCount;
    }
} 