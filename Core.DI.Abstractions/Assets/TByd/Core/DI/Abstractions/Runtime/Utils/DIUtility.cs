using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 提供依赖注入相关的辅助方法。
    /// </summary>
    public static class DIUtility
    {
        /// <summary>
        /// 获取类型上所有标记了Inject特性的成员。
        /// </summary>
        /// <param name="type">要检查的类型</param>
        /// <returns>标记了Inject特性的成员集合</returns>
        public static IEnumerable<MemberInfo> GetInjectableMembers(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(member => member.GetCustomAttribute<InjectAttribute>() != null);
        }

        /// <summary>
        /// 检查类型是否可注入（是否有标记Inject特性的成员）。
        /// </summary>
        /// <param name="type">要检查的类型</param>
        /// <returns>如果类型可注入则返回true</returns>
        public static bool IsInjectable(Type type)
        {
            return type != null && GetInjectableMembers(type).Any();
        }

        /// <summary>
        /// 获取成员的注入名称（如果有）。
        /// </summary>
        /// <param name="member">要检查的成员</param>
        /// <returns>注入名称，如果没有则返回null</returns>
        public static string GetInjectName(MemberInfo member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            var namedAttr = member.GetCustomAttribute<NamedAttribute>();
            return namedAttr?.Name;
        }

        /// <summary>
        /// 检查成员是否为可选注入。
        /// </summary>
        /// <param name="member">要检查的成员</param>
        /// <returns>如果成员是可选注入则返回true</returns>
        public static bool IsOptionalInject(MemberInfo member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            return member.GetCustomAttribute<OptionalAttribute>() != null;
        }
    }
} 