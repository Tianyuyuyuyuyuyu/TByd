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
using TBydFramework.XLua.Runtime.Binding;

namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class UnityEngineBehaviourWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Behaviour);
			Utils.BeginObjectRegister(type, L, translator, 0, 7, 2, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "BindingContext", _m_BindingContext);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateBindingSet", _m_CreateBindingSet);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetDataContext", _m_SetDataContext);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddBinding", _m_AddBinding);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddBindings", _m_AddBindings);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearBindings", _m_ClearBindings);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearAllBindings", _m_ClearAllBindings);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "enabled", _g_get_enabled);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isActiveAndEnabled", _g_get_isActiveAndEnabled);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "enabled", _s_set_enabled);
            
			
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
					
					var gen_ret = new UnityEngine.Behaviour();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Behaviour constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_BindingContext(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Behaviour gen_to_be_invoked = (UnityEngine.Behaviour)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.BindingContext(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateBindingSet(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Behaviour gen_to_be_invoked = (UnityEngine.Behaviour)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.CreateBindingSet(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetDataContext(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Behaviour gen_to_be_invoked = (UnityEngine.Behaviour)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _dataContext = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.SetDataContext( _dataContext );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddBinding(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Behaviour gen_to_be_invoked = (UnityEngine.Behaviour)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<TBydFramework.Runtime.Binding.BindingDescription>(L, 2)) 
                {
                    TBydFramework.Runtime.Binding.BindingDescription _bindingDescription = (TBydFramework.Runtime.Binding.BindingDescription)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Binding.BindingDescription));
                    
                    gen_to_be_invoked.AddBinding( _bindingDescription );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<object>(L, 2)&& translator.Assignable<TBydFramework.Runtime.Binding.BindingDescription>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    object _target = translator.GetObject(L, 2, typeof(object));
                    TBydFramework.Runtime.Binding.BindingDescription _bindingDescription = (TBydFramework.Runtime.Binding.BindingDescription)translator.GetObject(L, 3, typeof(TBydFramework.Runtime.Binding.BindingDescription));
                    object _key = translator.GetObject(L, 4, typeof(object));
                    
                    gen_to_be_invoked.AddBinding( _target, _bindingDescription, _key );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<object>(L, 2)&& translator.Assignable<TBydFramework.Runtime.Binding.BindingDescription>(L, 3)) 
                {
                    object _target = translator.GetObject(L, 2, typeof(object));
                    TBydFramework.Runtime.Binding.BindingDescription _bindingDescription = (TBydFramework.Runtime.Binding.BindingDescription)translator.GetObject(L, 3, typeof(TBydFramework.Runtime.Binding.BindingDescription));
                    
                    gen_to_be_invoked.AddBinding( _target, _bindingDescription );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Behaviour.AddBinding!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddBindings(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Behaviour gen_to_be_invoked = (UnityEngine.Behaviour)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription>>(L, 2)) 
                {
                    System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription> _bindingDescriptions = (System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription>));
                    
                    gen_to_be_invoked.AddBindings( _bindingDescriptions );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<object>(L, 2)&& translator.Assignable<System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription>>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    object _target = translator.GetObject(L, 2, typeof(object));
                    System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription> _bindingDescriptions = (System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription>)translator.GetObject(L, 3, typeof(System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription>));
                    object _key = translator.GetObject(L, 4, typeof(object));
                    
                    gen_to_be_invoked.AddBindings( _target, _bindingDescriptions, _key );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<object>(L, 2)&& translator.Assignable<System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription>>(L, 3)) 
                {
                    object _target = translator.GetObject(L, 2, typeof(object));
                    System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription> _bindingDescriptions = (System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription>)translator.GetObject(L, 3, typeof(System.Collections.Generic.IEnumerable<TBydFramework.Runtime.Binding.BindingDescription>));
                    
                    gen_to_be_invoked.AddBindings( _target, _bindingDescriptions );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Behaviour.AddBindings!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearBindings(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Behaviour gen_to_be_invoked = (UnityEngine.Behaviour)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _key = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.ClearBindings( _key );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearAllBindings(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Behaviour gen_to_be_invoked = (UnityEngine.Behaviour)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ClearAllBindings(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_enabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Behaviour gen_to_be_invoked = (UnityEngine.Behaviour)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.enabled);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isActiveAndEnabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Behaviour gen_to_be_invoked = (UnityEngine.Behaviour)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isActiveAndEnabled);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_enabled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Behaviour gen_to_be_invoked = (UnityEngine.Behaviour)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.enabled = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
