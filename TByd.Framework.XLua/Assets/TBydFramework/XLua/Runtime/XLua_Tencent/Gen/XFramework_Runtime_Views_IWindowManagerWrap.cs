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
    public class XFrameworkRuntimeViewsIWindowManagerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TBydFramework.Runtime.Views.IWindowManager);
			Utils.BeginObjectRegister(type, L, translator, 0, 12, 3, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Visibles", _m_Visibles);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Get", _m_Get);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Add", _m_Add);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Remove", _m_Remove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveAt", _m_RemoveAt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Contains", _m_Contains);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IndexOf", _m_IndexOf);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Find", _m_Find);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clear", _m_Clear);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Show", _m_Show);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Hide", _m_Hide);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Dismiss", _m_Dismiss);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Activated", _g_get_Activated);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Current", _g_get_Current);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Count", _g_get_Count);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Activated", _s_set_Activated);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "TBydFramework.Runtime.Views.IWindowManager does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Visibles(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.Visibles(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Get(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.Get( _index );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
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
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    TBydFramework.Runtime.Views.IWindow _window = (TBydFramework.Runtime.Views.IWindow)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Views.IWindow));
                    
                    gen_to_be_invoked.Add( _window );
                    
                    
                    
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
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    TBydFramework.Runtime.Views.IWindow _window = (TBydFramework.Runtime.Views.IWindow)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Views.IWindow));
                    
                        var gen_ret = gen_to_be_invoked.Remove( _window );
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
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.RemoveAt( _index );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
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
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    TBydFramework.Runtime.Views.IWindow _window = (TBydFramework.Runtime.Views.IWindow)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Views.IWindow));
                    
                        var gen_ret = gen_to_be_invoked.Contains( _window );
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
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    TBydFramework.Runtime.Views.IWindow _window = (TBydFramework.Runtime.Views.IWindow)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Views.IWindow));
                    
                        var gen_ret = gen_to_be_invoked.IndexOf( _window );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Find(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _visible = LuaAPI.lua_toboolean(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.Find( _visible );
                        translator.Push(L, gen_ret);
                    
                    
                    
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
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Clear(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Show(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    TBydFramework.Runtime.Views.IWindow _window = (TBydFramework.Runtime.Views.IWindow)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Views.IWindow));
                    
                        var gen_ret = gen_to_be_invoked.Show( _window );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Hide(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    TBydFramework.Runtime.Views.IWindow _window = (TBydFramework.Runtime.Views.IWindow)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Views.IWindow));
                    
                        var gen_ret = gen_to_be_invoked.Hide( _window );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Dismiss(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    TBydFramework.Runtime.Views.IWindow _window = (TBydFramework.Runtime.Views.IWindow)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Views.IWindow));
                    
                        var gen_ret = gen_to_be_invoked.Dismiss( _window );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Activated(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.Activated);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Current(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.Current);
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
			
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Count);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Activated(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Views.IWindowManager gen_to_be_invoked = (TBydFramework.Runtime.Views.IWindowManager)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Activated = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
