using System.Threading.Tasks;

namespace TBydFramework.Runtime.Interactivity
{
    public class AsyncInteractionEventArgs : InteractionEventArgs
    {
        /// <summary>
        /// Constructs a new instance of <see cref="AsyncInteractionEventArgs"/>
        /// </summary>
        /// <param name="context"></param>
        public AsyncInteractionEventArgs(TaskCompletionSource<object> source, object context) : base(context, () => { source.TrySetResult(null); })
        {
            this.Source = source;
        }

        public TaskCompletionSource<object> Source { get; }
    }
}
