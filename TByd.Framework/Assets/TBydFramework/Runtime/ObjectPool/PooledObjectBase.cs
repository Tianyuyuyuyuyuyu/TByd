namespace TBydFramework.Runtime.ObjectPool
{
    public abstract class PooledObjectBase<T> : IPooledObject where T : PooledObjectBase<T>
    {
        private IObjectPool<T> pool;
        public PooledObjectBase(IObjectPool<T> pool)
        {
            this.pool = pool;
        }

        public virtual void Free()
        {
            this.pool.Free((T)this);
        }
    }
}
