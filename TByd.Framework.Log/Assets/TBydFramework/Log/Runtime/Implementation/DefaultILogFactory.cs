using System;
using System.Collections.Generic;
using TBydFramework.Log.Runtime.Enum;
using TBydFramework.Log.Runtime.Interface;

namespace TBydFramework.Log.Runtime.Implementation
{
    public class DefaultILogFactory : ILogFactory
    {
        private Dictionary<string, ILog> repositories = new Dictionary<string, ILog>();
        private bool inUnity = true;
        private Level _level = Level.ALL;

        public DefaultILogFactory()
        {
        }

        public Level Level
        {
            get { return this._level; }
            set { this._level = value; }
        }

        public bool InUnity
        {
            get { return this.inUnity; }
            set { this.inUnity = value; }
        }

        public ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }

        public ILog GetLogger(Type type)
        {
            ILog log;
            if (repositories.TryGetValue(type.FullName, out log))
                return log;

            log = new LogImpl(type.Name, this);
            repositories[type.FullName] = log;
            return log;
        }

        public ILog GetLogger(string name)
        {
            ILog log;
            if (repositories.TryGetValue(name, out log))
                return log;

            log = new LogImpl(name, this);
            repositories[name] = log;
            return log;
        }
    }
}