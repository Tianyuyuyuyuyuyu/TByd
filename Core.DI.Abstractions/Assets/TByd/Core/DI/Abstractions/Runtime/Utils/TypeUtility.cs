using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TByd.Core.DI.Abstractions.Runtime
{
    /// <summary>
    /// 提供类型相关的辅助方法。
    /// </summary>
    public static class TypeUtility
    {
        /// <summary>
        /// 获取类型实现的所有接口。
        /// </summary>
        /// <param name="type">要检查的类型</param>
        /// <returns>类型实现的接口集合</returns>
        public static IEnumerable<Type> GetImplementedInterfaces(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.GetInterfaces();
        }

        /// <summary>
        /// 检查类型是否实现了指定接口。
        /// </summary>
        /// <param name="type">要检查的类型</param>
        /// <param name="interfaceType">接口类型</param>
        /// <returns>如果类型实现了指定接口则返回true</returns>
        public static bool ImplementsInterface(Type type, Type interfaceType)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (interfaceType == null)
                throw new ArgumentNullException(nameof(interfaceType));

            return interfaceType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 获取类型的所有构造函数，按参数数量降序排序。
        /// </summary>
        /// <param name="type">要检查的类型</param>
        /// <returns>构造函数信息集合</returns>
        public static IEnumerable<ConstructorInfo> GetConstructors(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .OrderByDescending(c => c.GetParameters().Length);
        }

        /// <summary>
        /// 获取类型的生命周期接口。
        /// </summary>
        /// <param name="type">要检查的类型</param>
        /// <returns>类型实现的生命周期接口集合</returns>
        public static IEnumerable<Type> GetLifecycleInterfaces(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var lifecycleInterfaces = new[]
            {
                typeof(IInitializable),
                typeof(IAsyncInitializable),
                typeof(IStartable),
                typeof(IAsyncStartable),
                typeof(IDisposable)
            };

            return type.GetInterfaces().Intersect(lifecycleInterfaces);
        }
    }
} 