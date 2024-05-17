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
using TBydFramework.XLua.Runtime.Binding.Builder;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class XFrameworkXLuaRuntimeBindingBuilderLuaBindingBuilderWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LuaBindingBuilder);
			Utils.BeginObjectRegister(type, L, translator, 0, 12, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "For", _m_For);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "To", _m_To);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToExpression", _m_ToExpression);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToStatic", _m_ToStatic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToValue", _m_ToValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TwoWay", _m_TwoWay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OneWay", _m_OneWay);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OneWayToSource", _m_OneWayToSource);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OneTime", _m_OneTime);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CommandParameter", _m_CommandParameter);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WithConversion", _m_WithConversion);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WithScopeKey", _m_WithScopeKey);
			
			
			
			
			
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
				if(LuaAPI.lua_gettop(L) == 3 && translator.Assignable<TBydFramework.Runtime.Binding.Contexts.IBindingContext>(L, 2) && translator.Assignable<object>(L, 3))
				{
					TBydFramework.Runtime.Binding.Contexts.IBindingContext _context = (TBydFramework.Runtime.Binding.Contexts.IBindingContext)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Binding.Contexts.IBindingContext));
					object _target = translator.GetObject(L, 3, typeof(object));
					
					var gen_ret = new LuaBindingBuilder(_context, _target);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.XLua.Runtime.Binding.Builder.LuaBindingBuilder constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_For(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _targetName = LuaAPI.lua_tostring(L, 2);
                    string _updateTrigger = LuaAPI.lua_tostring(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.For( _targetName, _updateTrigger );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _targetName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.For( _targetName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.XLua.Runtime.Binding.Builder.LuaBindingBuilder.For!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_To(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.To( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToExpression(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    XLua.LuaFunction _expression = (XLua.LuaFunction)translator.GetObject(L, 2, typeof(XLua.LuaFunction));
                    string[] _paths = translator.GetParams<string>(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.ToExpression( _expression, _paths );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToStatic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.ToStatic( _path );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    
                        var gen_ret = gen_to_be_invoked.ToValue( _value );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TwoWay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.TwoWay(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OneWay(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.OneWay(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OneWayToSource(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.OneWayToSource(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OneTime(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.OneTime(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CommandParameter(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _parameter = translator.GetObject(L, 2, typeof(object));
                    
                        var gen_ret = gen_to_be_invoked.CommandParameter( _parameter );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WithConversion(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _converterName = LuaAPI.lua_tostring(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.WithConversion( _converterName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<TBydFramework.Runtime.Binding.Converters.IConverter>(L, 2)) 
                {
                    TBydFramework.Runtime.Binding.Converters.IConverter _converter = (TBydFramework.Runtime.Binding.Converters.IConverter)translator.GetObject(L, 2, typeof(TBydFramework.Runtime.Binding.Converters.IConverter));
                    
                        var gen_ret = gen_to_be_invoked.WithConversion( _converter );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to TBydFramework.XLua.Runtime.Binding.Builder.LuaBindingBuilder.WithConversion!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WithScopeKey(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaBindingBuilder gen_to_be_invoked = (LuaBindingBuilder)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    object _scopeKey = translator.GetObject(L, 2, typeof(object));
                    
                        var gen_ret = gen_to_be_invoked.WithScopeKey( _scopeKey );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
