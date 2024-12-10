using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PoolMetrics
{
    public long Allocations { get; set; }
    public long Reuses { get; set; }
    public long Destroys { get; set; }
    public long CurrentSize { get; set; }
    public long PeakSize { get; set; }
    public long TotalOperations { get; set; }
    public DateTime LastAccessTime { get; set; }
    public TimeSpan AverageAcquisitionTime { get; set; }
    public int MissCount { get; set; }
    public int InvalidReturnCount { get; set; }

    public float ReuseRatio => Allocations == 0 ? 0 : (float)Reuses / Allocations;
    public float HitRatio => TotalOperations == 0 ? 0 : (float)(TotalOperations - MissCount) / TotalOperations;
    
    public void Reset()
    {
        Allocations = 0;
        Reuses = 0;
        Destroys = 0;
        CurrentSize = 0;
        PeakSize = 0;
        TotalOperations = 0;
        MissCount = 0;
        InvalidReturnCount = 0;
        LastAccessTime = DateTime.MinValue;
        AverageAcquisitionTime = TimeSpan.Zero;
    }
} 