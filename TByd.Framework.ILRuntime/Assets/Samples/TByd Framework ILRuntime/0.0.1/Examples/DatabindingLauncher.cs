using System;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;
using TBydFramework.ILRuntime.Runtime.ILRuntime;

namespace Samples.X_Framework_ILRuntime._0._0._1.Examples
{
    public class DatabindingLauncher : MonoBehaviour
    {
        public global::ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        public GameObject window;

        async void Start()
        {
            appdomain = ILRuntimeEnvironment.AppDomain;

            await ILRuntimeEnvironment.LoadAssembly(new Uri(Application.streamingAssetsPath + "/netstandard2.0/TBydFramework.Runtime.ILScript.dll"), new Uri(Application.streamingAssetsPath + "/netstandard2.0/TBydFramework.Runtime.ILScript.pdb"));
            await ILRuntimeEnvironment.LoadAssembly(new Uri(Application.streamingAssetsPath + "/netstandard2.0/TBydFramework.Runtime.ILScriptExamples.dll"), new Uri(Application.streamingAssetsPath + "/netstandard2.0/TBydFramework.Runtime.ILScriptExamples.pdb"));

            OnHotFixLoaded();
        }

        void OnHotFixLoaded()
        {
            //
            // 示例项目位置：Assets\Samples\X Framework ILRuntime\2.0.0\Examples\ILScriptExamples~
            // 打开此文件夹编译项目，编译后的DLL会自动导出到 StreamAssets\netstandard2.0 目录下
            //
            appdomain.Invoke("TBydFramework.Runtime.ILScriptExamples.DatabindingExample", "Run", null, window);
        }

        protected Type GetType(object source)
        {
            ILTypeInstance typeInstance = source as ILTypeInstance;
            if (typeInstance != null)
                return typeInstance.Type.ReflectionType;

            CrossBindingAdaptorType adaptor = source as CrossBindingAdaptorType;
            if (adaptor != null)
                return adaptor.ILInstance.Type.ReflectionType;

            return source.GetType();
        }
    }
}
