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
    public class XFrameworkRuntimeObservablesObservableDictionary_2_SystemObjectSystemObject_Wrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TBydFramework.Runtime.Observables.ObservableDictionary<object, object>);
			Utils.BeginObjectRegister(type, L, translator, 0, 13, 4, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "get_Item", _m_get_Item);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "set_Item", _m_set_Item);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Add", _m_Add);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Remove", _m_Remove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TryGetValue", _m_TryGetValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ContainsKey", _m_ContainsKey);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clear", _m_Clear);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Contains", _m_Contains);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CopyTo", _m_CopyTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEnumerator", _m_GetEnumerator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddRange", _m_AddRange);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PropertyChanged", _e_PropertyChanged);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CollectionChanged", _e_CollectionChanged);
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Keys", _g_get_Keys);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Values", _g_get_Values);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Count", _g_get_Count);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsReadOnly", _g_get_IsReadOnly);
            
			
			
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
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new TBydFramework.Runtime.Observables.ObservableDictionary<object, object>();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<System.Collections.Generic.IDictionary<object, object>>(L, 2))
				{
					System.Collections.Generic.IDictionary<object, object> _dictionary = (System.Collections.Generic.IDictionary<object, object>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IDictionary<object, object>));
					
					var gen_ret = new TBydFramework.Runtime.Observables.ObservableDictionary<object, object>(_dictionary);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<System.Collections.Generic.IEqualityComparer<object>>(L, 2))
				{
					System.Collections.Generic.IEqualityComparer<object> _comparer = (System.Collections.Generic.IEqualityComparer<object>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IEqualityComparer<object>));
					
					var gen_ret = new TBydFramework.Runtime.Observables.ObservableDictionary<object, object>(_comparer);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					int _capacity = LuaAPI.xlua_tointeger(L, 2);
					
					var gen_ret = new TBydFramework.Runtime.Observables.ObservableDictionary<object, object>(_capacity);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && translator.Assignable<System.Collections.Generic.IDictionary<object, object>>(L, 2) && translator.Assignable<System.Collections.Generic.IEqualityComparer<object>>(L, 3))
				{
					System.Collections.Generic.IDictionary<object, object> _dictionary = (System.Collections.Generic.IDictionary<object, object>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IDictionary<object, object>));
					System.Collections.Generic.IEqualityComparer<object> _comparer = (System.Collections.Generic.IEqualityComparer<object>)translator.GetObject(L, 3, typeof(System.Collections.Generic.IEqualityComparer<object>));
					
					var gen_ret = new TBydFramework.Runtime.Observables.ObservableDictionary<object, object>(_dictionary, _comparer);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && translator.Assignable<System.Collections.Generic.IEqualityComparer<object>>(L, 3))
				{
					int _capacity = LuaAPI.xlua_tointeger(L, 2);
					System.Collections.Generic.IEqualityComparer<object> _comparer = (System.Collections.Generic.IEqualityComparer<object>)translator.GetObject(L, 3, typeof(System.Collections.Generic.IEqualityComparer<object>));
					
					var gen_ret = new TBydFramework.Runtime.Observables.ObservableDictionary<object, object>(_capacity, _comparer);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Observables.ObservableDictionary<object, object> constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_get_Item(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
					object key = translator.GetObject(L, 2, typeof(object));
					translator.PushAny(L, gen_to_be_invoked[key]);
					
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_set_Item(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
					object key = translator.GetObject(L, 2, typeof(object));
					object gen_value = translator.GetObject(L, 3, typeof(object));
                    gen_to_be_invoked[key] = gen_value;
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Add(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.KeyValuePair<object, object>>(L, 2)) 
                {
                    System.Collections.Generic.KeyValuePair<object, object> _item;translator.Get(L, 2, out _item);
                    
                    gen_to_be_invoked.Add( _item );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<object>(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    object _key = translator.GetObject(L, 2, typeof(object));
                    object _value = translator.GetObject(L, 3, typeof(object));
                    
                    gen_to_be_invoked.Add( _key, _value );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Observables.ObservableDictionary<object, object>.Add!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Remove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _key = translator.GetObject(L, 2, typeof(object));
                    
                        var gen_ret = gen_to_be_invoked.Remove( _key );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.KeyValuePair<object, object>>(L, 2)) 
                {
                    System.Collections.Generic.KeyValuePair<object, object> _item;translator.Get(L, 2, out _item);
                    
                        var gen_ret = gen_to_be_invoked.Remove( _item );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Observables.ObservableDictionary<object, object>.Remove!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TryGetValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _key = translator.GetObject(L, 2, typeof(object));
                    object _value;
                    
                        var gen_ret = gen_to_be_invoked.TryGetValue( _key, out _value );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    translator.PushAny(L, _value);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ContainsKey(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _key = translator.GetObject(L, 2, typeof(object));
                    
                        var gen_ret = gen_to_be_invoked.ContainsKey( _key );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
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
            
            
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Clear(  );
                    
                    
                    
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
            
            
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.KeyValuePair<object, object> _item;translator.Get(L, 2, out _item);
                    
                        var gen_ret = gen_to_be_invoked.Contains( _item );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
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
            
            
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.KeyValuePair<object, object>[] _array = (System.Collections.Generic.KeyValuePair<object, object>[])translator.GetObject(L, 2, typeof(System.Collections.Generic.KeyValuePair<object, object>[]));
                    int _arrayIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.CopyTo( _array, _arrayIndex );
                    
                    
                    
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
            
            
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_AddRange(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.IDictionary<object, object> _items = (System.Collections.Generic.IDictionary<object, object>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IDictionary<object, object>));
                    
                    gen_to_be_invoked.AddRange( _items );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Keys(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.Keys);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Values(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.Values);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Count(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Count);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsReadOnly(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsReadOnly);
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
			TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
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
			LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Observables.ObservableDictionary<object, object>.PropertyChanged!");
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _e_CollectionChanged(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    int gen_param_count = LuaAPI.lua_gettop(L);
			TBydFramework.Runtime.Observables.ObservableDictionary<object, object> gen_to_be_invoked = (TBydFramework.Runtime.Observables.ObservableDictionary<object, object>)translator.FastGetCSObj(L, 1);
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
			LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Observables.ObservableDictionary<object, object>.CollectionChanged!");
            return 0;
        }
        
		
		
    }
}
