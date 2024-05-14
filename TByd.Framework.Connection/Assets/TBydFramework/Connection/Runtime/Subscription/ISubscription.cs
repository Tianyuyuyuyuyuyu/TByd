using System;
using System.Threading;

namespace TBydFramework.Connection.Runtime.Subscription
{
    public interface ISubscription<T> : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        ISubscription<T> Filter(Predicate<T> filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        ISubscription<T> ObserveOn(SynchronizationContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        ISubscription<T> Subscribe(Action<T> action);
    }
}
