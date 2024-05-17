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
    public class XFrameworkRuntimeViewsLocatorsIUIViewLocatorWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TBydFramework.Runtime.Views.Locators.IUIViewLocator);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadView", _m_LoadView);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadWindow", _m_LoadWindow);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadViewAsync", _m_LoadViewAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadWindowAsync", _m_LoadWindowAsync);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "TBydFramework.Runtime.Views.Locators.IUIViewLocator does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadView(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Views.Locators.IUIViewLocator gen_to_be_invoked = (TBydFramework.Runtime.Views.Locators.IUIViewLocator)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.LoadView( _name );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadWindow(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Views.Locators.IUIViewLocator gen_to_be_invoked = (TBydFramework.Runtime.Views.Locators.IUIViewLocator)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.LoadWindow( _name );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<TBydFramework.Runtime.Views.IWindowManager>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    TBydFramework.Runtime.Views.IWindowManager _windowManager = (TBydFramework.Runtime.Views.IWindowManager)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Views.IWindowManager));
                    string _name = LuaAPI.lua_tostring(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.LoadWindow( _windowManager, _name );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Views.Locators.IUIViewLocator.LoadWindow!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadViewAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Views.Locators.IUIViewLocator gen_to_be_invoked = (TBydFramework.Runtime.Views.Locators.IUIViewLocator)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.LoadViewAsync( _name );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadWindowAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Views.Locators.IUIViewLocator gen_to_be_invoked = (TBydFramework.Runtime.Views.Locators.IUIViewLocator)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.LoadWindowAsync( _name );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<TBydFramework.Runtime.Views.IWindowManager>(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    TBydFramework.Runtime.Views.IWindowManager _windowManager = (TBydFramework.Runtime.Views.IWindowManager)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Views.IWindowManager));
                    string _name = LuaAPI.lua_tostring(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.LoadWindowAsync( _windowManager, _name );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Views.Locators.IUIViewLocator.LoadWindowAsync!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
