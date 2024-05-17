#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class SystemCollectionsSpecializedNotifyCollectionChangedEventArgsWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.Collections.Specialized.NotifyCollectionChangedEventArgs);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 5, 0);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Action", _g_get_Action);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NewItems", _g_get_NewItems);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OldItems", _g_get_OldItems);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NewStartingIndex", _g_get_NewStartingIndex);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "OldStartingIndex", _g_get_OldStartingIndex);
            
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<System.Collections.Specialized.NotifyCollectionChangedAction>(L, 2))
				{
					System.Collections.Specialized.NotifyCollectionChangedAction _action;translator.Get(L, 2, out _action);
					
					var gen_ret = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(_action);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && translator.Assignable<System.Collections.Specialized.NotifyCollectionChangedAction>(L, 2) && translator.Assignable<object>(L, 3))
				{
					System.Collections.Specialized.NotifyCollectionChangedAction _action;translator.Get(L, 2, out _action);
					object _changedItem = translator.GetObject(L, 3, typeof(object));
					
					var gen_ret = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(_action, _changedItem);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<System.Collections.Specialized.NotifyCollectionChangedAction>(L, 2) && translator.Assignable<object>(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4))
				{
					System.Collections.Specialized.NotifyCollectionChangedAction _action;translator.Get(L, 2, out _action);
					object _changedItem = translator.GetObject(L, 3, typeof(object));
					int _index = LuaAPI.xlua_tointeger(L, 4);
					
					var gen_ret = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(_action, _changedItem, _index);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && translator.Assignable<System.Collections.Specialized.NotifyCollectionChangedAction>(L, 2) && translator.Assignable<System.Collections.IList>(L, 3))
				{
					System.Collections.Specialized.NotifyCollectionChangedAction _action;translator.Get(L, 2, out _action);
					System.Collections.IList _changedItems = (System.Collections.IList)translator.GetObject(L, 3, typeof(System.Collections.IList));
					
					var gen_ret = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(_action, _changedItems);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<System.Collections.Specialized.NotifyCollectionChangedAction>(L, 2) && translator.Assignable<System.Collections.IList>(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4))
				{
					System.Collections.Specialized.NotifyCollectionChangedAction _action;translator.Get(L, 2, out _action);
					System.Collections.IList _changedItems = (System.Collections.IList)translator.GetObject(L, 3, typeof(System.Collections.IList));
					int _startingIndex = LuaAPI.xlua_tointeger(L, 4);
					
					var gen_ret = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(_action, _changedItems, _startingIndex);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<System.Collections.Specialized.NotifyCollectionChangedAction>(L, 2) && translator.Assignable<object>(L, 3) && translator.Assignable<object>(L, 4))
				{
					System.Collections.Specialized.NotifyCollectionChangedAction _action;translator.Get(L, 2, out _action);
					object _newItem = translator.GetObject(L, 3, typeof(object));
					object _oldItem = translator.GetObject(L, 4, typeof(object));
					
					var gen_ret = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(_action, _newItem, _oldItem);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && translator.Assignable<System.Collections.Specialized.NotifyCollectionChangedAction>(L, 2) && translator.Assignable<object>(L, 3) && translator.Assignable<object>(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5))
				{
					System.Collections.Specialized.NotifyCollectionChangedAction _action;translator.Get(L, 2, out _action);
					object _newItem = translator.GetObject(L, 3, typeof(object));
					object _oldItem = translator.GetObject(L, 4, typeof(object));
					int _index = LuaAPI.xlua_tointeger(L, 5);
					
					var gen_ret = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(_action, _newItem, _oldItem, _index);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<System.Collections.Specialized.NotifyCollectionChangedAction>(L, 2) && translator.Assignable<System.Collections.IList>(L, 3) && translator.Assignable<System.Collections.IList>(L, 4))
				{
					System.Collections.Specialized.NotifyCollectionChangedAction _action;translator.Get(L, 2, out _action);
					System.Collections.IList _newItems = (System.Collections.IList)translator.GetObject(L, 3, typeof(System.Collections.IList));
					System.Collections.IList _oldItems = (System.Collections.IList)translator.GetObject(L, 4, typeof(System.Collections.IList));
					
					var gen_ret = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(_action, _newItems, _oldItems);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && translator.Assignable<System.Collections.Specialized.NotifyCollectionChangedAction>(L, 2) && translator.Assignable<System.Collections.IList>(L, 3) && translator.Assignable<System.Collections.IList>(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5))
				{
					System.Collections.Specialized.NotifyCollectionChangedAction _action;translator.Get(L, 2, out _action);
					System.Collections.IList _newItems = (System.Collections.IList)translator.GetObject(L, 3, typeof(System.Collections.IList));
					System.Collections.IList _oldItems = (System.Collections.IList)translator.GetObject(L, 4, typeof(System.Collections.IList));
					int _startingIndex = LuaAPI.xlua_tointeger(L, 5);
					
					var gen_ret = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(_action, _newItems, _oldItems, _startingIndex);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && translator.Assignable<System.Collections.Specialized.NotifyCollectionChangedAction>(L, 2) && translator.Assignable<object>(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5))
				{
					System.Collections.Specialized.NotifyCollectionChangedAction _action;translator.Get(L, 2, out _action);
					object _changedItem = translator.GetObject(L, 3, typeof(object));
					int _index = LuaAPI.xlua_tointeger(L, 4);
					int _oldIndex = LuaAPI.xlua_tointeger(L, 5);
					
					var gen_ret = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(_action, _changedItem, _index, _oldIndex);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 5 && translator.Assignable<System.Collections.Specialized.NotifyCollectionChangedAction>(L, 2) && translator.Assignable<System.Collections.IList>(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5))
				{
					System.Collections.Specialized.NotifyCollectionChangedAction _action;translator.Get(L, 2, out _action);
					System.Collections.IList _changedItems = (System.Collections.IList)translator.GetObject(L, 3, typeof(System.Collections.IList));
					int _index = LuaAPI.xlua_tointeger(L, 4);
					int _oldIndex = LuaAPI.xlua_tointeger(L, 5);
					
					var gen_ret = new System.Collections.Specialized.NotifyCollectionChangedEventArgs(_action, _changedItems, _index, _oldIndex);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to System.Collections.Specialized.NotifyCollectionChangedEventArgs constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Action(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Specialized.NotifyCollectionChangedEventArgs gen_to_be_invoked = (System.Collections.Specialized.NotifyCollectionChangedEventArgs)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Action);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NewItems(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Specialized.NotifyCollectionChangedEventArgs gen_to_be_invoked = (System.Collections.Specialized.NotifyCollectionChangedEventArgs)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.NewItems);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OldItems(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Specialized.NotifyCollectionChangedEventArgs gen_to_be_invoked = (System.Collections.Specialized.NotifyCollectionChangedEventArgs)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.OldItems);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NewStartingIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Specialized.NotifyCollectionChangedEventArgs gen_to_be_invoked = (System.Collections.Specialized.NotifyCollectionChangedEventArgs)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.NewStartingIndex);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OldStartingIndex(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                System.Collections.Specialized.NotifyCollectionChangedEventArgs gen_to_be_invoked = (System.Collections.Specialized.NotifyCollectionChangedEventArgs)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.OldStartingIndex);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
