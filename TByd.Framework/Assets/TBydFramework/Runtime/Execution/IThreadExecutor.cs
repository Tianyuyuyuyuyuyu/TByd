using System;
using TBydFramework.Runtime.Asynchronous;

namespace TBydFramework.Runtime.Execution
{
    public interface IThreadExecutor
    {
        Asynchronous.IAsyncResult Execute(Action action);

        IAsyncResult<TResult> Execute<TResult>(Func<TResult> func);

        Asynchronous.IAsyncResult Execute(Action<Asynchronous.IPromise> action);

        IProgressResult<TProgress> Execute<TProgress>(Action<IProgressPromise<TProgress>> action);

        IAsyncResult<TResult> Execute<TResult>(Action<Asynchronous.IPromise<TResult>> action);

        IProgressResult<TProgress, TResult> Execute<TProgress, TResult>(Action<IProgressPromise<TProgress, TResult>> action);
    }
}
