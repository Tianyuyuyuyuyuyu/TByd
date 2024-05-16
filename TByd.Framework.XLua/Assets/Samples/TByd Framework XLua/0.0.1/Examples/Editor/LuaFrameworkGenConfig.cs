using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TBydFramework.XLua.Runtime.Asynchronous;
using TBydFramework.XLua.Runtime.Views;
using UnityEngine;
using TBydFramework.Runtime.Asynchronous;
using TBydFramework.Runtime.Commands;
using TBydFramework.Runtime.Contexts;
using TBydFramework.Runtime.Execution;
using TBydFramework.Runtime.Localizations;
using TBydFramework.Runtime.Messaging;
using TBydFramework.Runtime.Observables;
using TBydFramework.Runtime.Prefs;
using TBydFramework.Runtime.ViewModels;
using TBydFramework.Runtime.Views;
using TBydFramework.Runtime.Views.Locators;
using XLua;
using IAsyncResult = TBydFramework.Runtime.Asynchronous.IAsyncResult;

namespace Samples.X_Framework_XLua._0._0._1.Examples.Editor
{
    public static class LuaFrameworkGenConfig
    {
        [LuaCallCSharp]
        public static List<Type> lua_call_cs_list = new List<Type>()
        {
            typeof(Executors),
            typeof(Context),
            typeof(ApplicationContext),
            typeof(PlayerContext),
            typeof(Preferences),
            typeof(ILocalization),
            typeof(Localization),
            typeof(Messenger),
            typeof(SimpleCommand),
            typeof(AsyncResult),
            typeof(AsyncResult<IViewModel>),
            typeof(ObservableDictionary<object,object>),
            typeof(ObservableList<object>),
            typeof(NotifyCollectionChangedEventArgs),
            typeof(NotifyCollectionChangedEventHandler),
            typeof(ITransition),
            typeof(WindowContainer),
            typeof(ProgressResult<float, IWindow>),
            typeof(ProgressResult<float, IView>),
            typeof(ProgressResult<float, IWindow>),
            typeof(ProgressResult<float, IView>),
            typeof(IView),
            typeof(IWindow),
            typeof(Window),
            typeof(IWindowManager),
            typeof(WindowManager),
            typeof(IUIViewLocator),
            typeof(DefaultUIViewLocator),
            typeof(Type),
            typeof(CoroutineAwaiterExtensions)
        };

        [CSharpCallLua]
        public static List<Type> cs_call_lua_list = new List<Type>()
        {
            typeof(IEnumerator),
            typeof(Action),
            typeof(Action<LuaTable>),
            typeof(Action<MonoBehaviour>),
            typeof(Func<MonoBehaviour, ILuaTask>),
            typeof(Action<IWindow,WindowState>),
            typeof(Action<LuaWindow>),
            typeof(Action<LuaWindow,IBundle>),
            typeof(NotifyCollectionChangedEventHandler),
            typeof(Action<float>),
            typeof(Action<int>),
            typeof(Action<string>),
            typeof(Action<object>),
            typeof(Action<Exception>),
            typeof(Action<IAsyncResult>),
            typeof(EventHandler<WindowStateEventArgs>),
            typeof(EventHandler),
            typeof(Func<object>),
            typeof(IViewModel),
            typeof(IAwaiter),
            typeof(IAwaiter<object>),
            typeof(IAwaiter<int>),
            typeof(ILuaTask<int>)
        };

        [BlackList]
        public static List<List<string>> BlackList = new List<List<string>>()  {
               new List<string>(){"System.Type", "IsSZArray"},
               new List<string>(){"System.Type", "MakeGenericSignatureType","System.Type","System.Type[]"},
               new List<string>(){"System.Type", "IsCollectible"}
        };
    }
}