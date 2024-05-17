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
    public class XFrameworkRuntimeObservablesObservableList_1_SystemObject_Wrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TBydFramework.Runtime.Observables.ObservableList<object>);
			Utils.BeginObjectRegister(type, L, translator, 0, 15, 1, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Add", _m_Add);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clear", _m_Clear);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CopyTo", _m_CopyTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Contains", _m_Contains);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IndexOf", _m_IndexOf);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Insert", _m_Insert);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Remove", _m_Remove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveAt", _m_RemoveAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Move", _m_Move);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddRange", _m_AddRange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InsertRange", _m_InsertRange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveRange", _m_RemoveRange);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEnumerator", _m_GetEnumerator);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PropertyChanged", _e_PropertyChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CollectionChanged", _e_CollectionChanged);
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Count", _g_get_Count);
            
			
			
			Utils.EndObjectRegister(type, L, translator, __CSIndexer, __NewIndexer,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new TBydFramework.Runtime.Observables.ObservableList<object>();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<System.Collections.Generic.List<object>>(L, 2))
				{
					System.Collections.Generic.List<object> _list = (System.Collections.Generic.List<object>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<object>));
					
					var gen_ret = new TBydFramework.Runtime.Observables.ObservableList<object>(_list);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Observables.ObservableList<object> constructor!");
            
        }
        
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int __CSIndexer(RealStatePtr L)
        {
			try {
			    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				
				if (translator.Assignable<TBydFramework.Runtime.Observables.ObservableList<object>>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					
					TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
					int index = LuaAPI.xlua_tointeger(L, 2);
					LuaAPI.lua_pushboolean(L, true);
					translator.PushAny(L, gen_to_be_invoked[index]);
					return 2;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
			
            LuaAPI.lua_pushboolean(L, false);
			return 1;
        }
		
        
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int __NewIndexer(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			try {
				
				if (translator.Assignable<TBydFramework.Runtime.Observables.ObservableList<object>>(L, 1) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && translator.Assignable<object>(L, 3))
				{
					
					TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
					int key = LuaAPI.xlua_tointeger(L, 2);
					gen_to_be_invoked[key] = translator.GetObject(L, 3, typeof(object));
					LuaAPI.lua_pushboolean(L, true);
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
			
			LuaAPI.lua_pushboolean(L, false);
            return 1;
        }
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Add(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _item = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.Add( _item );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clear(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Clear(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object[] _array = (object[])translator.GetObject(L, 2, typeof(object[]));
                    int _index = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.CopyTo( _array, _index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Contains(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _item = translator.GetObject(L, 2, typeof(object));
                    
                        var gen_ret = gen_to_be_invoked.Contains( _item );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IndexOf(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _item = translator.GetObject(L, 2, typeof(object));
                    
                        var gen_ret = gen_to_be_invoked.IndexOf( _item );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Insert(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    object _item = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.Insert( _index, _item );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Remove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _item = translator.GetObject(L, 2, typeof(object));
                    
                        var gen_ret = gen_to_be_invoked.Remove( _item );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveAt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.RemoveAt( _index );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Move(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _oldIndex = LuaAPI.xlua_tointeger(L, 2);
                    int _newIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.Move( _oldIndex, _newIndex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddRange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.IEnumerable<object> _collection = (System.Collections.Generic.IEnumerable<object>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IEnumerable<object>));
                    
                    gen_to_be_invoked.AddRange( _collection );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InsertRange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    System.Collections.Generic.IEnumerable<object> _collection = (System.Collections.Generic.IEnumerable<object>)translator.GetObject(L, 3, typeof(System.Collections.Generic.IEnumerable<object>));
                    
                    gen_to_be_invoked.InsertRange( _index, _collection );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveRange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.RemoveRange( _index, _count );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEnumerator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetEnumerator(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Count);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_PropertyChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
                System.ComponentModel.PropertyChangedEventHandler gen_delegate = translator.GetDelegate<System.ComponentModel.PropertyChangedEventHandler>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.ComponentModel.PropertyChangedEventHandler!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.PropertyChanged += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.PropertyChanged -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Observables.ObservableList<object>.PropertyChanged!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_CollectionChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			TBydFramework.Runtime.Observables.ObservableList<object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableList<object>)translator.FastGetCSObj(L, 1);
                System.Collections.Specialized.NotifyCollectionChangedEventHandler gen_delegate = translator.GetDelegate<System.Collections.Specialized.NotifyCollectionChangedEventHandler>(L, 3);
                if (gen_delegate == null) {
                    return LuaAPI.luaL_error(L, "#3 need System.Collections.Specialized.NotifyCollectionChangedEventHandler!");
                }
				
				if (gen_param_count == 3)
				{
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "+")) {
						gen_to_be_invoked.CollectionChanged += gen_delegate;
						return 0;
					} 
					
					
					if (LuaAPI.xlua_is_eq_str(L, 2, "-")) {
						gen_to_be_invoked.CollectionChanged -= gen_delegate;
						return 0;
					} 
					
				}
			} catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
			LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Observables.ObservableList<object>.CollectionChanged!");
            return 0;
        }
        
		
		
    }
}
