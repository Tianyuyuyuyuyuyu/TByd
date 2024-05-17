using XLua;

namespace TBydFramework.XLua.Runtime
{
    public interface ILuaExtendable
    {
        LuaTable GetMetatable();
    }
}