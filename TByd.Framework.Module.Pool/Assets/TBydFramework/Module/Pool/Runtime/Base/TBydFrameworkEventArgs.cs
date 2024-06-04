﻿using System;
using TBydFramework.Module.Pool.Runtime.Base.Interface;

namespace TBydFramework.Module.Pool.Runtime.Base
{
    /// <summary>
    /// 游戏框架中包含事件数据的类的基类。
    /// </summary>
    public abstract class TBydFrameworkEventArgs : EventArgs, IReference
    {
        /// <summary>
        /// 初始化游戏框架中包含事件数据的类的新实例。
        /// </summary>
        public TBydFrameworkEventArgs()
        {
        }

        /// <summary>
        /// 清理引用。
        /// </summary>
        public abstract void Clear();
    }
}
