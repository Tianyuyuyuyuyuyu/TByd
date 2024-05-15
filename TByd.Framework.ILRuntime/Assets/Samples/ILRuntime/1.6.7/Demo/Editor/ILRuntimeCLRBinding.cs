#if UNITY_EDITOR
using Samples.ILRuntime._1._6._7.Demo.Scripts.Examples._04_Inheritance;
using Samples.ILRuntime._1._6._7.Demo.Scripts.Examples._07_Coroutine;
using Samples.ILRuntime._1._6._7.Demo.Scripts.Examples._08_MonoBehaviour;
using Samples.ILRuntime._1._6._7.Demo.Scripts.Examples._11_ValueTypeBinding;
using UnityEditor;
using UnityEngine;

namespace Samples.ILRuntime._1._6._7.Demo.Editor
{
    [System.Reflection.Obfuscation(Exclude = true)]
    public class ILRuntimeCLRBinding
    {
        [MenuItem("X/ILRuntime/通过自动分析热更DLL生成CLR绑定")]
        static void GenerateCLRBindingByAnalysis()
        {
            //用新的分析热更dll调用引用来生成绑定代码
            global::ILRuntime.Runtime.Enviorment.AppDomain domain = new global::ILRuntime.Runtime.Enviorment.AppDomain();
            using (System.IO.FileStream fs = new System.IO.FileStream("Assets/StreamingAssets/HotFix_Project.dll", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                domain.LoadAssembly(fs);

                //Crossbind Adapter is needed to generate the correct binding code
                InitILRuntime(domain);
                global::ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, "Assets/Samples/ILRuntime/Generated");
            }

            AssetDatabase.Refresh();
        }

        static void InitILRuntime(global::ILRuntime.Runtime.Enviorment.AppDomain domain)
        {
            //这里需要注册所有热更DLL中用到的跨域继承Adapter，否则无法正确抓取引用
            domain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
            domain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
            domain.RegisterCrossBindingAdaptor(new TestClassBaseAdapter());
            domain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
        }
    }
}
#endif
