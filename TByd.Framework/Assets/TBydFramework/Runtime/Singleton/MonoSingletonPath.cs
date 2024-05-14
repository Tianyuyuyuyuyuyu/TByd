using System;

namespace TBydFramework.Runtime.Singleton
{
    /// <summary>
    /// MonoSingleton路径
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)] //这个特性只能标记在Class上
    internal class MonoSingletonPath : System.Attribute
    {
        private string mPathInHierarchy;

        public MonoSingletonPath(string pathInHierarchy)
        {
            mPathInHierarchy = pathInHierarchy;
        }

        public string PathInHierarchy
        {
            get { return mPathInHierarchy; }
        }
    }
}