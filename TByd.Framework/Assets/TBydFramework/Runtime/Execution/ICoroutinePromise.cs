using TBydFramework.Runtime.Asynchronous;
using UnityEngine;

namespace TBydFramework.Runtime.Execution
{
    public interface ICoroutinePromise : Asynchronous.IPromise
    {
        void AddCoroutine(Coroutine coroutine);
    }

    public interface ICoroutinePromise<TResult> : Asynchronous.IPromise<TResult>, ICoroutinePromise
    {
    }

    public interface ICoroutineProgressPromise<TProgress> : IProgressPromise<TProgress>, ICoroutinePromise
    {
    }

    public interface ICoroutineProgressPromise<TProgress, TResult> : IProgressPromise<TProgress, TResult>, ICoroutineProgressPromise<TProgress>
    {
    }
}
