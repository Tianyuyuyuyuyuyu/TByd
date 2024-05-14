using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TBydFramework.Editor.Tools
{
    public static partial class ToolAssetDatabase
    {
        /// <summary>
        /// 打开脚本
        /// </summary>
        /// <param name="relativePath"></param>
        public static void OpenScript(string relativePath)
        {
            AssetDatabase.OpenAsset(
                AssetDatabase.LoadAssetAtPath<Object>($"{Application.dataPath}/{relativePath}.cs"));
        }
    }
}