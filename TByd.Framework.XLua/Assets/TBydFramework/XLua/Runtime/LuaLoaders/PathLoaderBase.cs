using System;

namespace TBydFramework.XLua.Runtime.LuaLoaders
{
    public abstract class PathLoaderBase : LoaderBase, IDisposable
    {
        protected string prefix = "";
        protected string suffix = ".lua.txt";

        public PathLoaderBase(string prefix, string suffix)
        {
            this.prefix = prefix;
            if (!string.IsNullOrEmpty(this.prefix))
                this.prefix = this.prefix.Replace(@"\", "/");
            if (!this.prefix.EndsWith("/"))
                this.prefix += "/";

            this.suffix = suffix;
        }

        protected virtual string GetFullname(string className)
        {
            return string.Format("{0}{1}{2}", prefix, className.Replace(".", "/"), suffix);
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
        }

        ~PathLoaderBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}