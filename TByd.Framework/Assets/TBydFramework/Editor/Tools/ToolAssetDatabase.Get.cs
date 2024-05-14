using System.Collections.Generic;
using TBydFramework.Runtime.Res;
using UnityEditor;
using Object = UnityEngine.Object;

namespace TBydFramework.Editor.Tools
{
    /// <summary>
    /// AssetDatabase相关工具封装
    /// </summary>
    public static partial class ToolAssetDatabase
    {
        /// <summary>
        /// 通过AssetDatabase获得资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceType"></param>
        /// <param name="path">"Assets/Resource"</param>
        /// <returns></returns>
        public static List<T> GetRes<T>(EnumResType resourceType, params string[] path) where T : Object
        {
            return GetRes<T>($"t:{resourceType}", path);
        }

        /// <summary>
        /// 通过AssetDatabase获得资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resourceType"></param>
        /// <param name="nameContain"></param>
        /// <param name="path">"Assets/Resource"</param>
        /// <returns></returns>
        public static List<T> GetRes<T>(EnumResType resourceType, string nameContain,
            params string[] path) where T : Object
        {
            return GetRes<T>($"t:{resourceType} l:{nameContain}", path);
        }

        /// <summary>
        /// 通过AssetDatabase获得资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchPattern"></param>
        /// <param name="path">"Assets/Resource"</param>
        /// <returns></returns>
        public static List<T> GetRes<T>(string searchPattern, string[] path) where T : Object
        {
            var retList = new List<T>();

            var guilds = AssetDatabase.FindAssets(searchPattern, path);

            foreach (var guid in guilds)
            {
                var prefabPath = AssetDatabase.GUIDToAssetPath(guid);
                var prefab = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(T)) as T;
                retList.Add(prefab);
            }

            return retList;
        }
    }
}