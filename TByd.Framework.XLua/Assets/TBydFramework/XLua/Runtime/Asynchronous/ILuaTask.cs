using System;
using XLua;

namespace TBydFramework.XLua.Runtime.Asynchronous
{
    [CSharpCallLua]
    public interface ILuaTask
    {
        /// <summary>
        /// Gets the result of the asynchronous operation.
        /// </summary>
        object GetResult();

        /// <summary>
        /// Gets the cause of the asynchronous operation.
        /// </summary>
        string GetException();

        /// <summary>
        /// Returns <code>true</code> if the asynchronous operation is finished.
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="continuation"></param>
        void OnCompleted(Action continuation);
    }

    [CSharpCallLua]
    public interface ILuaTask<TResult>
    {
        /// <summary>
        /// Gets the result of the asynchronous operation.
        /// </summary>
        TResult GetResult();

        /// <summary>
        /// Gets the cause of the asynchronous operation.
        /// </summary>
        string GetException();

        /// <summary>
        /// Returns <code>true</code> if the asynchronous operation is finished.
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="continuation"></param>
        void OnCompleted(Action continuation);
    }
}
