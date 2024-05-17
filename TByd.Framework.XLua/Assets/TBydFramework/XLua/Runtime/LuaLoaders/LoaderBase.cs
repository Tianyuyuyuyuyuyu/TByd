using XLua;

namespace TBydFramework.XLua.Runtime.LuaLoaders
{
    public abstract class LoaderBase
    {
        protected abstract byte[] Load(ref string path);

        public static implicit operator LuaEnv.CustomLoader(LoaderBase loader)
        {
            return loader.Load;
        }
    }
}