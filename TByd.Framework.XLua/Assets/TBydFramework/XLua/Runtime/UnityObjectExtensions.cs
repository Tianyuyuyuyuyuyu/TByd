using UnityEngine;
using XLua;

namespace TBydFramework.XLua.Runtime
{
    [LuaCallCSharp]
    [ReflectionUse]
    public static class UnityObjectExtensions
    {
        public static bool IsDestroyed(this Object o)
        {
            return o == null;
        }
    }
}
