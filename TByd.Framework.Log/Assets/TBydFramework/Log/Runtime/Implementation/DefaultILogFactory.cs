using System;
using System.Collections.Generic;
using TBydFramework.Log.Runtime.Enum;
using TBydFramework.Log.Runtime.Interface;

namespace TBydFramework.Log.Runtime.Implementation
{
    /// <summary>
    /// 默认日志工厂实现类,用于创建和管理日志实例。
    /// </summary>
    public class DefaultILogFactory : ILogFactory
    {
        private readonly Dictionary<string, ILog> _repositories = new Dictionary<string, ILog>();
        private bool _inUnity = true;
        private Level _level = Level.ALL;

        /// <summary>
        /// 获取或设置日志级别。
        /// </summary>
        public Level Level
        {
            get => _level;
            set => _level = value;
        }

        /// <summary>
        /// 获取或设置是否在Unity环境中运行。
        /// </summary>
        public bool InUnity
        {
            get => _inUnity;
            set => _inUnity = value;
        }

        /// <summary>
        /// 获取指定类型的日志记录器。
        /// </summary>
        /// <typeparam name="T">要获取日志记录器的类型。</typeparam>
        /// <returns>对应类型的日志记录器实例。</returns>
        public ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }

        /// <summary>
        /// 获取指定类型的日志记录器。
        /// </summary>
        /// <param name="type">要获取日志记录器的类型。</param>
        /// <returns>对应类型的日志记录器实例。</returns>
        public ILog GetLogger(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            string key = type.FullName ?? type.Name;
            return GetOrCreateLogger(key, type.Name);
        }

        /// <summary>
        /// 获取指定名称的日志记录器。
        /// </summary>
        /// <param name="name">日志记录器的名称。</param>
        /// <returns>对应名称的日志记录器实例。</returns>
        public ILog GetLogger(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("日志记录器名称不能为空", nameof(name));
            }

            return GetOrCreateLogger(name, name);
        }

        /// <summary>
        /// 获取现有的日志记录器或创建新的日志记录器。
        /// </summary>
        /// <param name="key">用于存储日志记录器的键。</param>
        /// <param name="name">日志记录器的名称。</param>
        /// <returns>日志记录器实例。</returns>
        private ILog GetOrCreateLogger(string key, string name)
        {
            if (_repositories.TryGetValue(key, out ILog log))
            {
                return log;
            }

            log = new LogImpl(name, this);
            _repositories[key] = log;
            return log;
        }
    }
}
