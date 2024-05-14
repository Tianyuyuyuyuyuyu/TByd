namespace TBydFramework.Runtime.ObjectPool
{
    public interface IPooledObject
    {
        /// <summary>
        /// Return the object to the pool.
        /// </summary>
        void Free();
    }
}
