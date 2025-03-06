using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TByd.PackageCreator.Editor.Utils
{
    /// <summary>
    /// Unity资源数据库工具类，提供对Unity AssetDatabase的常用操作封装
    /// </summary>
    public static class AssetDatabaseUtils
    {
        /// <summary>
        /// 将绝对路径转换为Unity项目中的相对路径（Assets/...）
        /// </summary>
        /// <param name="absolutePath">绝对路径</param>
        /// <returns>相对于项目的路径</returns>
        public static string AbsolutePathToAssetPath(string absolutePath)
        {
            if (string.IsNullOrEmpty(absolutePath))
                return string.Empty;

            var projectPath = Path.GetDirectoryName(Application.dataPath);
            if (string.IsNullOrEmpty(projectPath))
                return string.Empty;

            // 规范化路径格式
            absolutePath = absolutePath.Replace('\\', '/');
            projectPath = projectPath.Replace('\\', '/');

            if (absolutePath.StartsWith(projectPath))
            {
                return absolutePath.Substring(projectPath.Length + 1);
            }

            return string.Empty;
        }

        /// <summary>
        /// 将Unity项目中的相对路径（Assets/...）转换为绝对路径
        /// </summary>
        /// <param name="assetPath">相对于项目的路径</param>
        /// <returns>绝对路径</returns>
        public static string AssetPathToAbsolutePath(string assetPath)
        {
            if (string.IsNullOrEmpty(assetPath))
                return string.Empty;

            var projectPath = Path.GetDirectoryName(Application.dataPath);
            if (string.IsNullOrEmpty(projectPath))
                return string.Empty;

            // 规范化路径格式
            assetPath = assetPath.Replace('\\', '/');
            projectPath = projectPath.Replace('\\', '/');

            return Path.Combine(projectPath, assetPath);
        }

        /// <summary>
        /// 创建资源文件并保存到指定路径
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="asset">资源对象</param>
        /// <param name="assetPath">资源路径（相对于项目）</param>
        /// <returns>是否创建成功</returns>
        public static bool CreateAsset<T>(T asset, string assetPath) where T : Object
        {
            try
            {
                if (asset == null || string.IsNullOrEmpty(assetPath))
                    return false;

                // 确保目录存在
                var directory = Path.GetDirectoryName(assetPath);
                if (!string.IsNullOrEmpty(directory))
                {
                    EnsureDirectoryExists(directory);
                }

                AssetDatabase.CreateAsset(asset, assetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log($"创建资源: {assetPath}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"创建资源失败: {assetPath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 确保指定的资源目录存在
        /// </summary>
        /// <param name="assetDirectoryPath">目录路径（相对于项目）</param>
        /// <returns>是否创建成功或目录已存在</returns>
        public static bool EnsureDirectoryExists(string assetDirectoryPath)
        {
            try
            {
                if (string.IsNullOrEmpty(assetDirectoryPath))
                    return false;

                if (assetDirectoryPath.StartsWith("Assets") || assetDirectoryPath.StartsWith("Packages"))
                {
                    var absolutePath = AssetPathToAbsolutePath(assetDirectoryPath);
                    if (!Directory.Exists(absolutePath))
                    {
                        Directory.CreateDirectory(absolutePath);
                        AssetDatabase.Refresh();
                        Debug.Log($"创建目录: {assetDirectoryPath}");
                    }
                    return true;
                }
                else
                {
                    Debug.LogWarning($"无效的资源目录路径: {assetDirectoryPath}，路径必须以Assets或Packages开头");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"创建目录失败: {assetDirectoryPath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 加载指定路径的资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="assetPath">资源路径（相对于项目）</param>
        /// <returns>加载的资源对象，如果加载失败则返回null</returns>
        public static T LoadAsset<T>(string assetPath) where T : Object
        {
            try
            {
                if (string.IsNullOrEmpty(assetPath))
                    return null;

                return AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }
            catch (Exception ex)
            {
                Debug.LogError($"加载资源失败: {assetPath}, 错误: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 删除指定路径的资源
        /// </summary>
        /// <param name="assetPath">资源路径（相对于项目）</param>
        /// <returns>是否删除成功</returns>
        public static bool DeleteAsset(string assetPath)
        {
            try
            {
                if (string.IsNullOrEmpty(assetPath))
                    return false;

                if (AssetDatabase.DeleteAsset(assetPath))
                {
                    Debug.Log($"删除资源: {assetPath}");
                    return true;
                }
                else
                {
                    Debug.LogWarning($"删除资源失败: {assetPath}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"删除资源时发生异常: {assetPath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 获取指定目录下的所有资源
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="folderPath">目录路径（相对于项目）</param>
        /// <param name="searchSubFolders">是否搜索子目录</param>
        /// <returns>资源对象列表</returns>
        public static List<T> FindAssets<T>(string folderPath, bool searchSubFolders = true) where T : Object
        {
            var assets = new List<T>();
            try
            {
                if (string.IsNullOrEmpty(folderPath))
                    return assets;

                var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { folderPath });
                foreach (var guid in guids)
                {
                    var assetPath = AssetDatabase.GUIDToAssetPath(guid);

                    // 判断是否要包含子目录的资源
                    if (!searchSubFolders && Path.GetDirectoryName(assetPath) != folderPath)
                        continue;

                    var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                    if (asset != null)
                    {
                        assets.Add(asset);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"查找资源失败: {folderPath}, 错误: {ex.Message}");
            }
            return assets;
        }

        /// <summary>
        /// 获取指定路径下的所有子目录
        /// </summary>
        /// <param name="folderPath">目录路径（相对于项目）</param>
        /// <returns>子目录路径列表</returns>
        public static string[] GetSubFolders(string folderPath)
        {
            var subFolders = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(folderPath))
                    return subFolders.ToArray();

                if (!AssetDatabase.IsValidFolder(folderPath))
                    return subFolders.ToArray();

                var absolutePath = AssetPathToAbsolutePath(folderPath);
                if (Directory.Exists(absolutePath))
                {
                    var directories = Directory.GetDirectories(absolutePath);
                    foreach (var dir in directories)
                    {
                        var relativePath = AbsolutePathToAssetPath(dir);
                        if (!string.IsNullOrEmpty(relativePath))
                        {
                            subFolders.Add(relativePath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"获取子目录失败: {folderPath}, 错误: {ex.Message}");
            }
            return subFolders.ToArray();
        }

        /// <summary>
        /// 创建或获取嵌套的资源目录路径
        /// </summary>
        /// <param name="basePath">基础路径</param>
        /// <param name="folders">要创建的子目录名称数组</param>
        /// <returns>创建的完整路径，如果失败则返回空字符串</returns>
        public static string CreateOrGetFolder(string basePath, params string[] folders)
        {
            try
            {
                if (string.IsNullOrEmpty(basePath) || folders == null || folders.Length == 0)
                    return basePath;

                // 检查并规范化基础路径
                if (!basePath.StartsWith("Assets") && !basePath.StartsWith("Packages"))
                {
                    Debug.LogWarning($"无效的基础路径: {basePath}，路径必须以Assets或Packages开头");
                    return string.Empty;
                }

                var currentPath = basePath;
                foreach (var folder in folders)
                {
                    if (string.IsNullOrEmpty(folder))
                        continue;

                    var newPath = Path.Combine(currentPath, folder);
                    if (!AssetDatabase.IsValidFolder(newPath))
                    {
                        var guid = AssetDatabase.CreateFolder(currentPath, folder);
                        if (string.IsNullOrEmpty(guid))
                        {
                            Debug.LogError($"创建目录失败: {newPath}");
                            return string.Empty;
                        }
                        newPath = AssetDatabase.GUIDToAssetPath(guid);
                    }
                    currentPath = newPath;
                }

                return currentPath;
            }
            catch (Exception ex)
            {
                Debug.LogError($"创建嵌套目录失败，错误: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// 复制资源到新路径
        /// </summary>
        /// <param name="sourcePath">源资源路径</param>
        /// <param name="destPath">目标资源路径</param>
        /// <returns>是否复制成功</returns>
        public static bool CopyAsset(string sourcePath, string destPath)
        {
            try
            {
                if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destPath))
                    return false;

                // 确保目标目录存在
                var destDir = Path.GetDirectoryName(destPath);
                if (!string.IsNullOrEmpty(destDir))
                {
                    EnsureDirectoryExists(destDir);
                }

                if (AssetDatabase.CopyAsset(sourcePath, destPath))
                {
                    Debug.Log($"复制资源: {sourcePath} -> {destPath}");
                    return true;
                }
                else
                {
                    Debug.LogWarning($"复制资源失败: {sourcePath} -> {destPath}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"复制资源时发生异常: {sourcePath} -> {destPath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 移动资源到新路径
        /// </summary>
        /// <param name="sourcePath">源资源路径</param>
        /// <param name="destPath">目标资源路径</param>
        /// <returns>是否移动成功</returns>
        public static bool MoveAsset(string sourcePath, string destPath)
        {
            try
            {
                if (string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destPath))
                    return false;

                // 确保目标目录存在
                var destDir = Path.GetDirectoryName(destPath);
                if (!string.IsNullOrEmpty(destDir))
                {
                    EnsureDirectoryExists(destDir);
                }

                var error = AssetDatabase.MoveAsset(sourcePath, destPath);
                if (string.IsNullOrEmpty(error))
                {
                    Debug.Log($"移动资源: {sourcePath} -> {destPath}");
                    return true;
                }
                else
                {
                    Debug.LogWarning($"移动资源失败: {sourcePath} -> {destPath}, 错误: {error}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"移动资源时发生异常: {sourcePath} -> {destPath}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 重命名资源
        /// </summary>
        /// <param name="assetPath">资源路径</param>
        /// <param name="newName">新名称</param>
        /// <returns>是否重命名成功</returns>
        public static bool RenameAsset(string assetPath, string newName)
        {
            try
            {
                if (string.IsNullOrEmpty(assetPath) || string.IsNullOrEmpty(newName))
                    return false;

                var error = AssetDatabase.RenameAsset(assetPath, newName);
                if (string.IsNullOrEmpty(error))
                {
                    Debug.Log($"重命名资源: {assetPath} -> {newName}");
                    return true;
                }
                else
                {
                    Debug.LogWarning($"重命名资源失败: {assetPath} -> {newName}, 错误: {error}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"重命名资源时发生异常: {assetPath} -> {newName}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 创建新的资源文件（.asset），如果文件已存在则被覆盖
        /// </summary>
        /// <typeparam name="T">ScriptableObject类型</typeparam>
        /// <param name="assetPath">资源路径</param>
        /// <returns>创建的ScriptableObject实例，如果失败则返回null</returns>
        public static T CreateScriptableObject<T>(string assetPath) where T : ScriptableObject
        {
            try
            {
                if (string.IsNullOrEmpty(assetPath))
                    return null;

                // 确保目录存在
                var directory = Path.GetDirectoryName(assetPath);
                if (!string.IsNullOrEmpty(directory))
                {
                    EnsureDirectoryExists(directory);
                }

                // 如果资源已存在，先删除
                if (AssetDatabase.LoadAssetAtPath<T>(assetPath) != null)
                {
                    AssetDatabase.DeleteAsset(assetPath);
                }

                var asset = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(asset, assetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log($"创建ScriptableObject: {assetPath}");
                return asset;
            }
            catch (Exception ex)
            {
                Debug.LogError($"创建ScriptableObject失败: {assetPath}, 错误: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 获取资源的GUID
        /// </summary>
        /// <param name="assetPath">资源路径</param>
        /// <returns>资源的GUID，如果失败则返回空字符串</returns>
        public static string GetAssetGUID(string assetPath)
        {
            try
            {
                if (string.IsNullOrEmpty(assetPath))
                    return string.Empty;

                var guid = AssetDatabase.AssetPathToGUID(assetPath);
                return string.IsNullOrEmpty(guid) ? string.Empty : guid;
            }
            catch (Exception ex)
            {
                Debug.LogError($"获取资源GUID失败: {assetPath}, 错误: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// 通过GUID获取资源路径
        /// </summary>
        /// <param name="guid">资源GUID</param>
        /// <returns>资源路径，如果失败则返回空字符串</returns>
        public static string GetAssetPath(string guid)
        {
            try
            {
                if (string.IsNullOrEmpty(guid))
                    return string.Empty;

                var path = AssetDatabase.GUIDToAssetPath(guid);
                return string.IsNullOrEmpty(path) ? string.Empty : path;
            }
            catch (Exception ex)
            {
                Debug.LogError($"通过GUID获取资源路径失败: {guid}, 错误: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// 创建预制体资源
        /// </summary>
        /// <param name="gameObject">游戏对象</param>
        /// <param name="assetPath">预制体路径</param>
        /// <returns>创建的预制体资源，如果失败则返回null</returns>
        public static GameObject CreatePrefab(GameObject gameObject, string assetPath)
        {
            try
            {
                if (gameObject == null || string.IsNullOrEmpty(assetPath))
                    return null;

                // 确保目录存在
                var directory = Path.GetDirectoryName(assetPath);
                if (!string.IsNullOrEmpty(directory))
                {
                    EnsureDirectoryExists(directory);
                }

                // 创建预制体
                var prefab = PrefabUtility.SaveAsPrefabAsset(gameObject, assetPath);
                if (prefab != null)
                {
                    Debug.Log($"创建预制体: {assetPath}");
                    return prefab;
                }
                else
                {
                    Debug.LogWarning($"创建预制体失败: {assetPath}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"创建预制体时发生异常: {assetPath}, 错误: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 刷新资源数据库
        /// </summary>
        /// <param name="importAllAssets">是否重新导入所有资源</param>
        public static void RefreshAssetDatabase(bool importAllAssets = false)
        {
            if (importAllAssets)
            {
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
            else
            {
                AssetDatabase.Refresh();
            }
        }

        /// <summary>
        /// 获取选中的资源路径
        /// </summary>
        /// <returns>当前选中的资源路径列表</returns>
        public static List<string> GetSelectedAssetPaths()
        {
            return Selection.assetGUIDs
                .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
                .Where(path => !string.IsNullOrEmpty(path))
                .ToList();
        }
    }
}
