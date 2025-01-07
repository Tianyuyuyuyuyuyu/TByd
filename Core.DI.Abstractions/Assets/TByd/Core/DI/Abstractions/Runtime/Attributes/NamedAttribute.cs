using System;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 标记一个依赖使用命名注入。
    /// 用于区分同一类型的不同实现。
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class NamedAttribute : Attribute
    {
        /// <summary>
        /// 获取注入名称。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 初始化NamedAttribute的新实例。
        /// </summary>
        /// <param name="name">注入名称</param>
        public NamedAttribute(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("注入名称不能为空", nameof(name));

            Name = name;
        }
    }
} 