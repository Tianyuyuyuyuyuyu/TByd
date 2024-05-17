#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;
using System.Collections.Generic;
using System.Reflection;
using TBydFramework.XLua.Runtime;
using TBydFramework.XLua.Runtime.Asynchronous;
using TBydFramework.XLua.Runtime.Binding;
using TBydFramework.XLua.Runtime.Binding.Builder;
using TBydFramework.XLua.Runtime.Binding.Proxy.Sources.Object;
using TBydFramework.XLua.Runtime.Views;
using TBydFramework.XLua.Runtime.Views.Animations;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
	{
        
        
        static void wrapInit0(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(object), SystemObjectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Object), UnityEngineObjectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Vector2), UnityEngineVector2Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Vector3), UnityEngineVector3Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Vector4), UnityEngineVector4Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Quaternion), UnityEngineQuaternionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Color), UnityEngineColorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Ray), UnityEngineRayWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Bounds), UnityEngineBoundsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Ray2D), UnityEngineRay2DWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Time), UnityEngineTimeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.GameObject), UnityEngineGameObjectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Component), UnityEngineComponentWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Behaviour), UnityEngineBehaviourWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Transform), UnityEngineTransformWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Resources), UnityEngineResourcesWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.TextAsset), UnityEngineTextAssetWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Keyframe), UnityEngineKeyframeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AnimationCurve), UnityEngineAnimationCurveWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AnimationClip), UnityEngineAnimationClipWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.MonoBehaviour), UnityEngineMonoBehaviourWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.ParticleSystem), UnityEngineParticleSystemWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.SkinnedMeshRenderer), UnityEngineSkinnedMeshRendererWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Renderer), UnityEngineRendererWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Light), UnityEngineLightWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Mathf), UnityEngineMathfWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.List<int>), SystemCollectionsGenericList_1_SystemInt32_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Debug), UnityEngineDebugWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.BaseClass), TutorialBaseClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.TestEnum), TutorialTestEnumWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass), TutorialDerivedClassWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.ICalc), TutorialICalcWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClassExtensions), TutorialDerivedClassExtensionsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.LuaBehaviour), XLuaTestLuaBehaviourWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.Pedding), XLuaTestPeddingWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.MyStruct), XLuaTestMyStructWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.MyEnum), XLuaTestMyEnumWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.NoGc), XLuaTestNoGcWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.WaitForSeconds), UnityEngineWaitForSecondsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.BaseTest), XLuaTestBaseTestWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.Foo1Parent), XLuaTestFoo1ParentWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.Foo2Parent), XLuaTestFoo2ParentWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.Foo1Child), XLuaTestFoo1ChildWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.Foo2Child), XLuaTestFoo2ChildWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.Foo), XLuaTestFooWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaTest.FooExtension), XLuaTestFooExtensionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityObjectExtensions), XFrameworkXLuaRuntimeUnityObjectExtensionsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaBehaviour), XFrameworkXLuaRuntimeViewsLuaBehaviourWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaUIView), XFrameworkXLuaRuntimeViewsLuaUIViewWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaView), XFrameworkXLuaRuntimeViewsLuaViewWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaWindow), XFrameworkXLuaRuntimeViewsLuaWindowWrap.__Register);
        
        }
        
        static void wrapInit1(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(GenericUIAnimation), XFrameworkXLuaRuntimeViewsAnimationsGenericUIAnimationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaUIAnimation), XFrameworkXLuaRuntimeViewsAnimationsLuaUIAnimationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaBehaviourBindingExtension), XFrameworkXLuaRuntimeBindingLuaBehaviourBindingExtensionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaGameObjectBindingExtension), XFrameworkXLuaRuntimeBindingLuaGameObjectBindingExtensionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaBindingBuilder), XFrameworkXLuaRuntimeBindingBuilderLuaBindingBuilderWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(LuaBindingSet), XFrameworkXLuaRuntimeBindingBuilderLuaBindingSetWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Tutorial.DerivedClass.TestEnumInner), TutorialDerivedClassTestEnumInnerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Execution.Executors), XFrameworkRuntimeExecutionExecutorsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Contexts.Context), XFrameworkRuntimeContextsContextWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Contexts.ApplicationContext), XFrameworkRuntimeContextsApplicationContextWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Contexts.PlayerContext), XFrameworkRuntimeContextsPlayerContextWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Prefs.Preferences), XFrameworkRuntimePrefsPreferencesWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Localizations.ILocalization), XFrameworkRuntimeLocalizationsILocalizationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Localizations.Localization), XFrameworkRuntimeLocalizationsLocalizationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Messaging.Messenger), XFrameworkRuntimeMessagingMessengerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Commands.SimpleCommand), XFrameworkRuntimeCommandsSimpleCommandWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Asynchronous.AsyncResult), XFrameworkRuntimeAsynchronousAsyncResultWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Asynchronous.AsyncResult<TBydFramework.Runtime.ViewModels.IViewModel>), XFrameworkRuntimeAsynchronousAsyncResult_1_XFrameworkRuntimeViewModelsIViewModel_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Observables.ObservableDictionary<object, object>), XFrameworkRuntimeObservablesObservableDictionary_2_SystemObjectSystemObject_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Observables.ObservableList<object>), XFrameworkRuntimeObservablesObservableList_1_SystemObject_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Specialized.NotifyCollectionChangedEventArgs), SystemCollectionsSpecializedNotifyCollectionChangedEventArgsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Views.ITransition), XFrameworkRuntimeViewsITransitionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Views.WindowContainer), XFrameworkRuntimeViewsWindowContainerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Asynchronous.ProgressResult<float, TBydFramework.Runtime.Views.IWindow>), XFrameworkRuntimeAsynchronousProgressResult_2_SystemSingleXFrameworkRuntimeViewsIWindow_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Asynchronous.ProgressResult<float, TBydFramework.Runtime.Views.IView>), XFrameworkRuntimeAsynchronousProgressResult_2_SystemSingleXFrameworkRuntimeViewsIView_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Views.IView), XFrameworkRuntimeViewsIViewWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Views.IWindow), XFrameworkRuntimeViewsIWindowWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Views.Window), XFrameworkRuntimeViewsWindowWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Views.IWindowManager), XFrameworkRuntimeViewsIWindowManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Views.WindowManager), XFrameworkRuntimeViewsWindowManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Views.Locators.IUIViewLocator), XFrameworkRuntimeViewsLocatorsIUIViewLocatorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Views.Locators.DefaultUIViewLocator), XFrameworkRuntimeViewsLocatorsDefaultUIViewLocatorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Type), SystemTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions), XFrameworkRuntimeAsynchronousCoroutineAwaiterExtensionsWrap.__Register);
        
        
        
        }
        
        static void Init(LuaEnv luaenv, ObjectTranslator translator)
        {
            
            wrapInit0(luaenv, translator);
            
            wrapInit1(luaenv, translator);
            
            
            translator.AddInterfaceBridgeCreator(typeof(System.Collections.IEnumerator), SystemCollectionsIEnumeratorBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(XLuaTest.IExchanger), XLuaTestIExchangerBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(ILuaObservableObject), XFrameworkXLuaRuntimeBindingProxySourcesObjectILuaObservableObjectBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(ILuaTask), XFrameworkXLuaRuntimeAsynchronousILuaTaskBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(Tutorial.CSCallLua.ItfD), TutorialCSCallLuaItfDBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(XLuaTest.InvokeLua.ICalc), XLuaTestInvokeLuaICalcBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(TBydFramework.Runtime.ViewModels.IViewModel), XFrameworkRuntimeViewModelsIViewModelBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(TBydFramework.Runtime.Asynchronous.IAwaiter), XFrameworkRuntimeAsynchronousIAwaiterBridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(TBydFramework.Runtime.Asynchronous.IAwaiter<object>), XFrameworkRuntimeAsynchronousIAwaiter_1_SystemObject_Bridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(TBydFramework.Runtime.Asynchronous.IAwaiter<int>), XFrameworkRuntimeAsynchronousIAwaiter_1_SystemInt32_Bridge.__Create);
            
            translator.AddInterfaceBridgeCreator(typeof(ILuaTask<int>), XFrameworkXLuaRuntimeAsynchronousILuaTask_1_SystemInt32_Bridge.__Create);
            
        }
        
	    static XLua_Gen_Initer_Register__()
        {
		    XLua.LuaEnv.AddIniter(Init);
		}
		
		
	}
	
}
namespace XLua
{
	public partial class ObjectTranslator
	{
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ s_gen_reg_dumb_obj = new XLua.CSObjectWrap.XLua_Gen_Initer_Register__();
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ gen_reg_dumb_obj {get{return s_gen_reg_dumb_obj;}}
	}
	
	internal partial class InternalGlobals
    {
	    
		delegate bool __GEN_DELEGATE0( UnityEngine.Object o);
		
		delegate TBydFramework.Runtime.Asynchronous.IAwaiter __GEN_DELEGATE1( System.Collections.IEnumerator coroutine);
		
		delegate TBydFramework.Runtime.Asynchronous.IAwaiter __GEN_DELEGATE2( UnityEngine.YieldInstruction instruction);
		
		delegate TBydFramework.Runtime.Asynchronous.IAwaiter __GEN_DELEGATE3( TBydFramework.Runtime.Asynchronous.WaitForMainThread instruction);
		
		delegate TBydFramework.Runtime.Asynchronous.IAwaiter __GEN_DELEGATE4( TBydFramework.Runtime.Asynchronous.WaitForBackgroundThread instruction);
		
		delegate TBydFramework.Runtime.Asynchronous.IAwaiter<UnityEngine.CustomYieldInstruction> __GEN_DELEGATE5( UnityEngine.CustomYieldInstruction instruction);
		
		delegate TBydFramework.Runtime.Asynchronous.IAwaiter __GEN_DELEGATE6( UnityEngine.AsyncOperation target);
		
		delegate TBydFramework.Runtime.Asynchronous.IAwaiter<UnityEngine.Object> __GEN_DELEGATE7( UnityEngine.ResourceRequest target);
		
		delegate TBydFramework.Runtime.Asynchronous.IAwaiter<UnityEngine.Object> __GEN_DELEGATE8( UnityEngine.AssetBundleRequest target);
		
		delegate TBydFramework.Runtime.Asynchronous.IAwaiter<UnityEngine.AssetBundle> __GEN_DELEGATE9( UnityEngine.AssetBundleCreateRequest target);
		
		delegate TBydFramework.Runtime.Asynchronous.IAwaiter<UnityEngine.Networking.UnityWebRequest> __GEN_DELEGATE10( UnityEngine.Networking.UnityWebRequestAsyncOperation target);
		
		delegate TBydFramework.Runtime.Asynchronous.IAwaiter<object> __GEN_DELEGATE11( TBydFramework.Runtime.Asynchronous.IAsyncResult target);
		
	    static InternalGlobals()
		{
		    extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
				{typeof(UnityEngine.Object), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE0(UnityObjectExtensions.IsDestroyed)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(System.Collections.IEnumerator), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE1(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.YieldInstruction), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE2(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(TBydFramework.Runtime.Asynchronous.WaitForMainThread), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE3(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(TBydFramework.Runtime.Asynchronous.WaitForBackgroundThread), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE4(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.CustomYieldInstruction), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE5(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.AsyncOperation), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE6(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.ResourceRequest), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE7(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.AssetBundleRequest), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE8(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.AssetBundleCreateRequest), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE9(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(UnityEngine.Networking.UnityWebRequestAsyncOperation), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE10(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
				{typeof(TBydFramework.Runtime.Asynchronous.IAsyncResult), new List<MethodInfo>(){
				
				  new __GEN_DELEGATE11(TBydFramework.Runtime.Asynchronous.CoroutineAwaiterExtensions.GetAwaiter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},
				
			};
			
			genTryArrayGetPtr = StaticLuaCallbacks.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks.__tryArraySet;
		}
	}
}
