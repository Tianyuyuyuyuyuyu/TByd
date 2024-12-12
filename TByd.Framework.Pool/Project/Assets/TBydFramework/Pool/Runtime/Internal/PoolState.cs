using TBydFramework.Pool.Runtime.Enums;

namespace TBydFramework.Pool.Runtime.Internal
{
    public class PoolState
    {
        public int Count { get; set; }
        public int MaxSize { get; set; }
        public PoolType Type { get; set; }
        public bool EnablePrewarm { get; set; }
        public int PrewarmSize { get; set; }
    }
} 