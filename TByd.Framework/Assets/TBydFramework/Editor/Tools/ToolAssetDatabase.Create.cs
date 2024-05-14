using System;
using TBydFramework.Runtime.Log;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TBydFramework.Editor.Tools
{
    public static partial class ToolAssetDatabase
    {
        /// <summary>
        /// 安全创建资产
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CreateAssetSafe(Object asset, string path)
        {
            if (AssetDatabase.IsValidFolder(path))
            {
                XLogger.LogError("Error! Attempted to write an asset over a folder!");
                return false;
            }

            var folderPath = path.Substring(0, path.LastIndexOf("/", StringComparison.Ordinal));

            if (!GenerateFolderStructureAt(folderPath)) return false;

            AssetDatabase.CreateAsset(asset, path);
            return true;
        }
        
        /// <summary>
        /// 如果文件夹结构不存在，则将其生成到指定路径。会优先执行检查自身
        /// </summary>
        /// <param name="folderPath">文件夹路径，不应该包括任何文件名</param>
        /// <param name="ask">询问是否要生成文件夹结构</param>
        /// <returns>如果用户取消操作，则为False, 如果不需要生成任何东西或操作成功，则为True</returns>
        public static bool GenerateFolderStructureAt(string folderPath, bool ask = true)
        {
            //转换斜杠，以便与其他文件系统操作一起使用Equals操作符
            folderPath = folderPath.Replace("/", "\\");

            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                var existingPath = "Assets";
                var unknownPath = folderPath.Remove(0, existingPath.Length + 1);

                //删除路径名开头的“Assets/”
                var folderName = (unknownPath.Contains("\\"))
                    ? unknownPath.Substring(0, (unknownPath.IndexOf("\\", StringComparison.Ordinal)))
                    : unknownPath;

                do
                {
                    var newPath = System.IO.Path.Combine(existingPath, folderName);

                    //开始检查文件路径，看看它是否有效
                    if (!AssetDatabase.IsValidFolder(newPath))
                    {
                        var createFolder = true;
                        if (ask)
                        {
                            createFolder = EditorUtility.DisplayDialog("Path does not exist!",
                                "The folder " + "\"" + newPath +
                                "\" does not exist! Would you like to create this folder?", "Yes", "No");
                        }

                        if (createFolder)
                        {
                            AssetDatabase.CreateFolder(existingPath, folderName);
                        }
                        else return false;
                    }

                    existingPath = newPath;

                    //完整路径仍然不存在
                    if (!existingPath.Equals(folderPath))
                    {
                        unknownPath = unknownPath.Remove(0, folderName.Length + 1);
                        folderName = (unknownPath.Contains("\\"))
                            ? unknownPath.Substring(0, (unknownPath.IndexOf("\\", StringComparison.Ordinal)))
                            : unknownPath;
                    }
                } while (!AssetDatabase.IsValidFolder(folderPath));
            }

            return true;
        }

        /// <summary>
        /// 打开智能保存文件对话框
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultName"></param>
        /// <param name="startingPath"></param>
        public static void OpenSmartSaveFileDialog<T>(string defaultName = "New Object", string startingPath = "Assets")
            where T : ScriptableObject
        {
            var savePath = EditorUtility.SaveFilePanel("Designate save path", startingPath, defaultName, "asset");

            //保证没有点"Cancel"
            if (savePath != "")
            {
                var asset = ScriptableObject.CreateInstance<T>();
                savePath = savePath.Remove(0, savePath.IndexOf("Assets/", StringComparison.Ordinal));
                CreateAssetSafe(asset, savePath);
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = asset;
            }
        }
    }
}