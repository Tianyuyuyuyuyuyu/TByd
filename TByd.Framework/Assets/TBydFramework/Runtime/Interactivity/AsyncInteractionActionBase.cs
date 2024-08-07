﻿using System.Threading.Tasks;

namespace TBydFramework.Runtime.Interactivity
{
    public abstract class AsyncInteractionActionBase<TNotification> : IInteractionAction
    {
        public void OnRequest(object sender, InteractionEventArgs args)
        {
            AsyncInteractionEventArgs asyncArgs = args as AsyncInteractionEventArgs;
            TaskCompletionSource<object> source = asyncArgs.Source;
            TNotification notification = (TNotification)asyncArgs.Context;
            this.Action(notification).ContinueWith((t) =>
            {
                if (t.IsFaulted)
                    source.TrySetException(t.Exception);
                else if (t.IsCanceled)
                    source.TrySetCanceled();
                else
                    source.TrySetResult(null);
            }, TaskContinuationOptions.ExecuteSynchronously);
        }

        public abstract Task Action(TNotification notification);
    }

    public abstract class AsyncInteractionActionBase : IInteractionAction
    {
        public void OnRequest(object sender, InteractionEventArgs args)
        {
            AsyncInteractionEventArgs asyncArgs = args as AsyncInteractionEventArgs;
            TaskCompletionSource<object> source = asyncArgs.Source;
            this.Action().ContinueWith((t) =>
            {
                if (t.IsFaulted)
                    source.TrySetException(t.Exception);
                else if (t.IsCanceled)
                    source.TrySetCanceled();
                else
                    source.TrySetResult(null);
            }, TaskContinuationOptions.ExecuteSynchronously);
        }

        public abstract Task Action();
    }
}
