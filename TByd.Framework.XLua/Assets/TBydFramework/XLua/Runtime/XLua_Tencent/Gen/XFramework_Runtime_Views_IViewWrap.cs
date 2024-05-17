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
    public class XFrameworkRuntimeViewsIViewWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TBydFramework.Runtime.Views.IView);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 6, 2);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Name", _g_get_Name);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Parent", _g_get_Parent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Owner", _g_get_Owner);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Transform", _g_get_Transform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Visibility", _g_get_Visibility);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ExtraAttributes", _g_get_ExtraAttributes);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "Name", _s_set_Name);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Visibility", _s_set_Visibility);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "TBydFramework.Runtime.Views.IView does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Views.IView gen_to_be_invoked = (TBydFramework.Runtime.Views.IView)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushstring(L, gen_to_be_invoked.Name);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Parent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Views.IView gen_to_be_invoked = (TBydFramework.Runtime.Views.IView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Parent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Owner(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Views.IView gen_to_be_invoked = (TBydFramework.Runtime.Views.IView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Owner);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Transform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Views.IView gen_to_be_invoked = (TBydFramework.Runtime.Views.IView)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Transform);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Visibility(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Views.IView gen_to_be_invoked = (TBydFramework.Runtime.Views.IView)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.Visibility);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ExtraAttributes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Views.IView gen_to_be_invoked = (TBydFramework.Runtime.Views.IView)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.ExtraAttributes);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Views.IView gen_to_be_invoked = (TBydFramework.Runtime.Views.IView)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Name = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Visibility(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Views.IView gen_to_be_invoked = (TBydFramework.Runtime.Views.IView)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Visibility = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
