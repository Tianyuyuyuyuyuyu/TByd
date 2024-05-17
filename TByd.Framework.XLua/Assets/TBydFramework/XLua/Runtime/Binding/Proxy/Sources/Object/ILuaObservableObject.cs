using System;
using XLua;

namespace TBydFramework.XLua.Runtime.Binding.Proxy.Sources.Object
{
    [CSharpCallLua]
    public interface ILuaObservableObject
    {
        void subscribe(string key, Action action);

        void unsubscribe(string key, Action action);

        void subscribe(int key, Action action);

        void unsubscribe(int key, Action action);
    }
}