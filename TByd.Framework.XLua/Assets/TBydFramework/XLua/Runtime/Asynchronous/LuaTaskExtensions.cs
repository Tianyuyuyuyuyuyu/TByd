using System;
using System.Runtime.CompilerServices;
using TBydFramework.Runtime.Asynchronous;

namespace TBydFramework.XLua.Runtime.Asynchronous
{
    public static class LuaTaskExtensions
    {
        public static IAwaiter GetAwaiter(this ILuaTask target)
        {
            return new LuaTaskAwaiter(target);
        }

        public static IAwaiter<T> GetAwaiter<T>(this ILuaTask<T> target)
        {
            return new LuaTaskAwaiter<T>(target);
        }

        public struct LuaTaskAwaiter : IAwaiter, ICriticalNotifyCompletion
        {
            private ILuaTask task;

            public LuaTaskAwaiter(ILuaTask task)
            {
                this.task = task ?? throw new ArgumentNullException("task");
            }

            public bool IsCompleted => task.IsCompleted;

            public void GetResult()
            {
                if (!IsCompleted)
                    throw new Exception("The task is not finished yet");

                if (task.GetException() != null)
                    throw new Exception(task.GetException());
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                if (continuation == null)
                    throw new ArgumentNullException("continuation");

                task.OnCompleted(continuation);
            }
        }

        public struct LuaTaskAwaiter<TResult> : IAwaiter<TResult>, ICriticalNotifyCompletion
        {
            private ILuaTask<TResult> task;

            public LuaTaskAwaiter(ILuaTask<TResult> task)
            {
                this.task = task ?? throw new ArgumentNullException("task");
            }

            public bool IsCompleted => task.IsCompleted;

            public TResult GetResult()
            {
                if (!IsCompleted)
                    throw new Exception("The task is not finished yet");

                if (task.GetException() != null)
                    throw new Exception(task.GetException());

                return task.GetResult();
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                if (continuation == null)
                    throw new ArgumentNullException("continuation");

                task.OnCompleted(continuation);
            }
        }
    }
}