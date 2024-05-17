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
    public class XFrameworkRuntimeAsynchronousAsyncResult_1_XFrameworkRuntimeViewModelsIViewModel_Wrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel>);
			Utils.BeginObjectRegister(type, L, translator, 0, 3, 1, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetResult", _m_SetResult);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Callbackable", _m_Callbackable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Synchronized", _m_Synchronized);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Result", _g_get_Result);
            
			
			
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
					
					var gen_ret = new TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel>();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2))
				{
					bool _cancelable = LuaAPI.lua_toboolean(L, 2);
					
					var gen_ret = new TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel>(_cancelable);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel> constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetResult(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel> gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    TBydFramework.Runtime.ViewModels.IViewModel _result = (TBydFramework.Runtime.ViewModels.IViewModel)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.ViewModels.IViewModel));
                    
                    gen_to_be_invoked.SetResult( _result );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Callbackable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel> gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.Callbackable(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Synchronized(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel> gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel>)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.Synchronized(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Result(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel> gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel>)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.Result);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
