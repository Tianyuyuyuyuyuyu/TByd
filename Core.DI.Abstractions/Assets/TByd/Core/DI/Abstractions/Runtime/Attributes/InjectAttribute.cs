using System;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 依赖注入特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class InjectAttribute : Attribute
    {
        /// <summary>
        /// 注入ID
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 是否必需
        /// </summary>
        public bool Required { get; set; } = true;

        public InjectAttribute(string id = null)
        {
            Id = id;
        }
    }
} 