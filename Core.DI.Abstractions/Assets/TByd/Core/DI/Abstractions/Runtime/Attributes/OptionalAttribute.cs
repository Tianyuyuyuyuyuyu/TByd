using System;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 标记一个依赖为可选的。
    /// 如果依赖无法解析，将注入null而不是抛出异常。
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class OptionalAttribute : Attribute
    {
    }
} 