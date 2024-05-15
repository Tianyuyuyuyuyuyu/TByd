#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Samples.ILRuntime._1._6._7.Demo.Editor
{
    [System.Reflection.Obfuscation(Exclude = true)]
    public class ILRuntimeMenu
    {
        [MenuItem("X/ILRuntime/安装VS调试插件")]
        static void InstallDebugger()
        {   
            EditorUtility.OpenWithDefaultApp("Assets/Samples/ILRuntime/1.6.7/Demo/Debugger~/ILRuntimeDebuggerLauncher.vsix");
        }

        [MenuItem("X/ILRuntime/打开ILRuntime中文文档")]
        static void OpenDocumentation()
        {
            Application.OpenURL("https://ourpalm.github.io/ILRuntime/");
        }

        [MenuItem("X/ILRuntime/打开ILRuntime Github项目")]
        static void OpenGithub()
        {
            Application.OpenURL("https://github.com/Ourpalm/ILRuntime");
        }
    }
}
#endif
