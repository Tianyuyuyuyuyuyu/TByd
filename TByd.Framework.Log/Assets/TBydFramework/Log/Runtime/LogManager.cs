﻿using System;
using TBydFramework.Log.Runtime.Implementation;
using TBydFramework.Log.Runtime.Interface;

namespace TBydFramework.Log.Runtime
{
    public static class LogManager
    {
        private static readonly DefaultILogFactory _defaultILogFactory = new DefaultILogFactory();
        private static ILogFactory _factory;

        public static DefaultILogFactory Default => _defaultILogFactory;

        public static ILog GetLogger(Type type)
        {
            if (_factory != null)
                return _factory.GetLogger(type);

            return _defaultILogFactory.GetLogger(type);
        }

        public static ILog GetLogger(string name)
        {
            if (_factory != null)
                return _factory.GetLogger(name);

            return _defaultILogFactory.GetLogger(name);
        }

        public static void Registry(ILogFactory factory)
        {
            if (_factory != null && _factory != factory)
                throw new Exception("Don't register log factory many times");

            _factory = factory;
        }
    }
}