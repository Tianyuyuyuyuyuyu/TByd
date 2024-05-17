using System;
using TBydFramework.Runtime.Binding.Parameters;
using XLua;

namespace TBydFramework.XLua.Runtime.Binding.Parameters
{
    public class LuaFunctionCommandParameter : ICommandParameter
    {
        private LuaFunction function;
        public LuaFunctionCommandParameter(LuaFunction function)
        {
            this.function = function;
        }
        public object GetValue()
        {
            object[] results = function.Call();
            if (results == null || results.Length <= 0)
                return null;
            return results[0];
        }

        public Type GetValueType()
        {
            return typeof(object);
        }
    }
}
