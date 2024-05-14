using System;
using System.Threading;
using System.Threading.Tasks;

namespace TBydFramework.Connection.Runtime
{
    public class TaskTimeoutOrCompletionSource<TResult> : TaskCompletionSource<TResult>
    {

        private long endTime;
        private CancellationToken cancellationToken;
        public TaskTimeoutOrCompletionSource(int timeoutMilliseconds, CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
            this.endTime = DateTime.Now.Ticks + timeoutMilliseconds * TimeSpan.TicksPerMillisecond;
        }

        public TimeSpan Delay { get { return TimeSpan.FromTicks(endTime - DateTime.Now.Ticks); } }

        public bool IsTimeout { get { return (endTime - DateTime.Now.Ticks) <= 0; } }

        public bool IsCanceled { get { return cancellationToken.IsCancellationRequested; } }

        public void SetTimeout()
        {
            this.SetException(new TimeoutException("The operation has timed out."));
        }

        public bool TrySetTimeout()
        {
            return this.TrySetException(new TimeoutException("The operation has timed out."));
        }

        public new bool TrySetCanceled()
        {
            return TrySetCanceled(cancellationToken);
        }
    }
}
