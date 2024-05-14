using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using UnityEngine.ResourceManagement.AsyncOperations;
using TBydFramework.Runtime.Asynchronous;

namespace TBydFramework.Addressable.Runtime.Asynchronous
{
    public static class AsyncOperationHandleAwaiterExtensions
    {
        public static IAwaiter<object> GetAwaiter(this AsyncOperationHandle target)
        {
            return new AsyncOperationHandleAwaiter(target);
        }

        public static IAwaiter<TResult> GetAwaiter<TResult>(this AsyncOperationHandle<TResult> target)
        {
            return new AsyncOperationHandleAwaiter<TResult>(target);
        }


        public struct AsyncOperationHandleAwaiter : IAwaiter<object>, ICriticalNotifyCompletion
        {
            private AsyncOperationHandle asyncOperation;
            private Action<AsyncOperationHandle> continuationAction;

            public AsyncOperationHandleAwaiter(AsyncOperationHandle asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                this.continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.IsDone;

            public object GetResult()
            {
                if (!IsCompleted)
                    throw new Exception("The task is not finished yet");

                if (continuationAction != null)
                    asyncOperation.Completed -= continuationAction;
                continuationAction = null;

                if (asyncOperation.OperationException != null)
                    ExceptionDispatchInfo.Capture(asyncOperation.OperationException).Throw();

                return asyncOperation.Result;
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                if (continuation == null)
                    throw new ArgumentNullException("continuation");

                if (asyncOperation.IsDone)
                {
                    continuation();
                }
                else
                {
                    continuationAction = (ao) => { continuation(); };
                    asyncOperation.Completed += continuationAction;
                }
            }
        }

        public struct AsyncOperationHandleAwaiter<TResult> : IAwaiter<TResult>, ICriticalNotifyCompletion
        {
            private AsyncOperationHandle<TResult> asyncOperation;
            private Action<AsyncOperationHandle<TResult>> continuationAction;

            public AsyncOperationHandleAwaiter(AsyncOperationHandle<TResult> asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                this.continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.IsDone;

            public TResult GetResult()
            {
                if (!IsCompleted)
                    throw new Exception("The task is not finished yet");

                if (continuationAction != null)
                {
                    asyncOperation.Completed -= continuationAction;
                    continuationAction = null;
                }

                if (asyncOperation.OperationException != null)
                    ExceptionDispatchInfo.Capture(asyncOperation.OperationException).Throw();

                return asyncOperation.Result;
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                if (continuation == null)
                    throw new ArgumentNullException("continuation");

                if (asyncOperation.IsDone)
                {
                    continuation();
                }
                else
                {
                    continuationAction = (ao) => { continuation(); };
                    asyncOperation.Completed += continuationAction;
                }
            }
        }
    }
}
