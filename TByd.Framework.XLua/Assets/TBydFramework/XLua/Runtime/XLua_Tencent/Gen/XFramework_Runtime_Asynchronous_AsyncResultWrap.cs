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
using TBydFramework.Runtime.Asynchronous;

namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class XFrameworkRuntimeAsynchronousAsyncResultWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(TBydFramework.Runtime.Asynchronous.AsyncResult);
			Utils.BeginObjectRegister(type, L, translator, 0, 8, 5, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetException", _m_SetException);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetResult", _m_SetResult);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCancelled", _m_SetCancelled);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Cancel", _m_Cancel);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Callbackable", _m_Callbackable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Synchronized", _m_Synchronized);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WaitForDone", _m_WaitForDone);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetAwaiter", _m_GetAwaiter);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Exception", _g_get_Exception);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsDone", _g_get_IsDone);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Result", _g_get_Result);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsCancellationRequested", _g_get_IsCancellationRequested);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsCancelled", _g_get_IsCancelled);
            
			
			
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
					
					var gen_ret = new TBydFramework.Runtime.Asynchronous.AsyncResult();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2))
				{
					bool _cancelable = LuaAPI.lua_toboolean(L, 2);
					
					var gen_ret = new TBydFramework.Runtime.Asynchronous.AsyncResult(_cancelable);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Asynchronous.AsyncResult constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetException(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _error = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetException( _error );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Exception>(L, 2)) 
                {
                    System.Exception _exception = (System.Exception)translator.GetObject(L, 2, typeof(System.Exception));
                    
                    gen_to_be_invoked.SetException( _exception );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Asynchronous.AsyncResult.SetException!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetResult(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _result = translator.GetObject(L, 2, typeof(object));
                    
                    gen_to_be_invoked.SetResult( _result );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.SetResult(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.Runtime.Asynchronous.AsyncResult.SetResult!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCancelled(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.SetCancelled(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Cancel(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.Cancel(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
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
            
            
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
            
            
                
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
            
            
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
            
            
                
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
        static int _m_WaitForDone(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.WaitForDone(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetAwaiter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetAwaiter(  );
                        translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Exception(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Exception);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsDone(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsDone);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Result(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
                translator.PushAny(L, gen_to_be_invoked.Result);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsCancellationRequested(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsCancellationRequested);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsCancelled(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                TBydFramework.Runtime.Asynchronous.AsyncResult gen_to_be_invoked = (TBydFramework.Runtime.Asynchronous.AsyncResult)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsCancelled);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
