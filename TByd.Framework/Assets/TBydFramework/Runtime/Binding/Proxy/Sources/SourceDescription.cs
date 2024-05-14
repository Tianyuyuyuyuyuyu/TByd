using System;

namespace TBydFramework.Runtime.Binding.Proxy.Sources
{
    [Serializable]
    public abstract class SourceDescription
    {
        private bool isStatic = false;
        public virtual bool IsStatic
        {
            get { return this.isStatic; }
            set { this.isStatic = value; }
        }
    }
}