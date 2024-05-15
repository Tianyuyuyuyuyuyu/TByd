#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TBydFramework.GuidRegenerator.Editor
{
    public class AssetGuidRegeneratorMenu
    {
        public const string Version = "0.0.1";

        [MenuItem("X/Tool/Regenerate GUID/Files Only", true)]
        public static bool RegenerateGUID_Validation()
        {
            return DoValidation();
        }

        [MenuItem("X/Tool/Regenerate GUID/Files and Folders", true)]
        public static bool RegenerateGUIDWithFolders_Validation()
        {
            return DoValidation();
        }

        private static bool DoValidation()
        {
            var bAreSelectedAssetsValid = true;

            foreach (var guid in Selection.assetGUIDs)
            {
                bAreSelectedAssetsValid = !string.IsNullOrEmpty(guid) && guid != "0";
            }

            return bAreSelectedAssetsValid;
        }

        [MenuItem("X/Tool/Regenerate GUID/Files Only")]
        public static void RegenerateGUID_Implementation()
        {
            DoImplementation(false);
        }

        [MenuItem("X/Tool/Regenerate GUID/Files and Folders")]
        public static void RegenerateGUIDWithFolders_Implementation()
        {
            DoImplementation(true);
        }

        private static void DoImplementation(bool includeFolders)
        {
            var assetGuids = AssetGuidRegenerator.ExtractGUIDs(Selection.assetGUIDs, includeFolders);

            var option = EditorUtility.DisplayDialogComplex($"为 {assetGuids.Length} 个 assets 重新生成 GUID",
                "免责声明: 除非遇到某些问题，否则不建议故意修改资产GUID. " +
                "\n\n确保您有备份或正在使用版本控制系统. \n\n对于较大的项目，此操作可能需要很长时间. 您想继续吗?",
                "继续", "终止", "了解更多");

            if (option == 0)
            {
                AssetDatabase.StartAssetEditing();
                AssetGuidRegenerator.RegenerateGUIDs(assetGuids);
                AssetDatabase.StopAssetEditing();
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            else if (option == 2)
            {
                Application.OpenURL("https://github.com/Tianyuyuyuyuyuyu/PackageGithubRepo/blob/main/CustomPackages/X.Framework.GuidRegenerator/Packages/GuidRegenerator/README.md");
            }
        }
    }

    internal abstract class AssetGuidRegenerator
    {
        // 基本上，我们想限制类型 (例如 "t:GameObject t:Scene t:Material").
        // 但为了动态支持 ScriptableObjects，我们只需包含所有资源的基础，即“t:Object”
        private const string SearchFilter = "t:Object";

        // 仅设置为“Assets/”文件夹。 我们不想包含根文件夹的其他目录
        private static readonly string[] SearchDirectories = { "Assets" };

        public static void RegenerateGUIDs(string[] selectedGUIDs)
        {
            var assetGUIDs = AssetDatabase.FindAssets(SearchFilter, SearchDirectories);

            var updatedAssets = new Dictionary<string, int>();
            var skippedAssets = new List<string>();

            var inverseReferenceMap = new Dictionary<string, HashSet<string>>();

            /*
            * 准备第 1 部分 - 初始化map以存储引用我们选择的GUID的所有路径
            */
            foreach (var selectedGuid in selectedGUIDs)
            {
                inverseReferenceMap[selectedGuid] = new HashSet<string>();
            }

            /*
             * 准备第 2 部分 - 扫描所有资产并存储反向引用（如果包含对任何selectedGUI 的引用）...
             */
            var scanProgress = 0;
            var referencesCount = 0;
            foreach (var guid in assetGUIDs)
            {
                scanProgress++;
                var path = AssetDatabase.GUIDToAssetPath(guid);
                if (IsDirectory(path)) continue;

                var dependencies = AssetDatabase.GetDependencies(path);
                foreach (var dependency in dependencies)
                {
                    EditorUtility.DisplayProgressBar("正在扫描引用的Guid:", path, (float) scanProgress / assetGUIDs.Length);

                    var dependencyGuid = AssetDatabase.AssetPathToGUID(dependency);
                    if (inverseReferenceMap.ContainsKey(dependencyGuid))
                    {
                        inverseReferenceMap[dependencyGuid].Add(path);
                        
                        // 还包括 .meta 路径。 这修复了 FBX 使用外部材质时损坏的引用
                        var metaPath = AssetDatabase.GetTextMetaFilePathFromAssetPath(path);
                        inverseReferenceMap[dependencyGuid].Add(metaPath);
                        
                        referencesCount++;
                    }
                }
            }

            var countProgress = 0;

            foreach (var selectedGuid in selectedGUIDs)
            {
                var newGUID = GUID.Generate().ToString();
                try
                {
                    /*
                     * 第 1 部分 - 替换所选资产本身的 GUID。 如果 .meta 文件不存在或与 guid 不匹配（这种情况不应该发生），不继续执行第 2 部分
                     */
                    var assetPath = AssetDatabase.GUIDToAssetPath(selectedGuid);
                    var metaPath = AssetDatabase.GetTextMetaFilePathFromAssetPath(assetPath);

                    if (!File.Exists(metaPath))
                    {
                        skippedAssets.Add(assetPath);
                        throw new FileNotFoundException($"找不到所选资产的元文件. Asset: {assetPath}");
                    }

                    var metaContents = File.ReadAllText(metaPath);

                    // 检查 .meta 文件中的 guid 是否与所选资源的 guid 匹配
                    if (!metaContents.Contains(selectedGuid))
                    {
                        skippedAssets.Add(assetPath);
                        throw new ArgumentException($"[{assetPath}] 的 GUID 与其元文件中的 GUID 不匹配.");
                    }

                    // 允许重新生成文件夹的 guid，因为修改它似乎没有害处
                    // if (IsDirectory(assetPath)) continue;

                    // 跳过场景文件
                    if (assetPath.EndsWith(".unity"))
                    {
                        skippedAssets.Add(assetPath);
                        continue;
                    }

                    var metaAttributes = File.GetAttributes(metaPath);
                    var bIsInitiallyHidden = false;

                    // 如果 .meta 文件被隐藏，暂时取消隐藏
                    if (metaAttributes.HasFlag(FileAttributes.Hidden))
                    {
                        bIsInitiallyHidden = true;
                        HideFile(metaPath, metaAttributes);
                    }

                    metaContents = metaContents.Replace(selectedGuid, newGUID);
                    File.WriteAllText(metaPath, metaContents);

                    if (bIsInitiallyHidden) UnhideFile(metaPath, metaAttributes);

                    if (IsDirectory(assetPath))
                    {
                        // 跳过目录的第 2 部分，因为它们不应在资产或场景中具有任何引用
                        updatedAssets.Add(AssetDatabase.GUIDToAssetPath(selectedGuid), 0);
                        continue;
                    }

                    /*
                     * 第 2 部分 - 更新引用所选 GUID 的所有资产的 GUID
                     */
                    var countReplaced = 0;
                    var referencePaths = inverseReferenceMap[selectedGuid];
                    foreach(var referencePath in referencePaths)
                    {
                        countProgress++;

                        EditorUtility.DisplayProgressBar($"正在重新生成GUID: {assetPath}", referencePath, (float) countProgress / referencesCount);

                        if (IsDirectory(referencePath)) continue;

                        var contents = File.ReadAllText(referencePath);

                        if (!contents.Contains(selectedGuid)) continue;

                        contents = contents.Replace(selectedGuid, newGUID);
                        File.WriteAllText(referencePath, contents);

                        countReplaced++;
                    }

                    updatedAssets.Add(AssetDatabase.GUIDToAssetPath(selectedGuid), countReplaced);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
                finally
                {
                    EditorUtility.ClearProgressBar();
                }
            }

            if (EditorUtility.DisplayDialog("重新生成 GUID",
                $"为 {updatedAssets.Count} 个资源重新生成 GUID. \n请参阅控制台日志以获取详细报告.", "完成"))
            {
                var message = $"<b>GUID Regenerator {AssetGuidRegeneratorMenu.Version}</b>\n";

                if (updatedAssets.Count > 0) message += $"<b><color=green>{updatedAssets.Count} 个 Assets 已更新</color></b>\t选择此日志以获取更多信息\n";
                message = updatedAssets.Aggregate(message, (current, kvp) => current + $"{kvp.Value} references\t{kvp.Key}\n");

                if (skippedAssets.Count > 0) message += $"\n<b><color=red>{skippedAssets.Count} Skipped Asset/s</color></b>\n";
                message = skippedAssets.Aggregate(message, (current, skipped) => current + $"{skipped}\n");

                Debug.Log($"{message}");
            }
        }

        //使用 AssetDatabase.FindAssets 搜索目录并提取其中的所有 asset guids
        public static string[] ExtractGUIDs(string[] selectedGUIDs, bool includeFolders)
        {
            var finalGuids = new List<string>();
            foreach (var guid in selectedGUIDs)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (IsDirectory(assetPath))
                {
                    string[] searchDirectory = {assetPath};

                    if (includeFolders) finalGuids.Add(guid);
                    finalGuids.AddRange(AssetDatabase.FindAssets(SearchFilter, searchDirectory));
                }
                else
                {
                    finalGuids.Add(guid);
                }
            }

            return finalGuids.ToArray();
        }

        private static void HideFile(string path, FileAttributes attributes)
        {
            attributes &= ~FileAttributes.Hidden;
            File.SetAttributes(path, attributes);
        }

        private static void UnhideFile(string path, FileAttributes attributes)
        {
            attributes |= FileAttributes.Hidden;
            File.SetAttributes(path, attributes);
        }

        private static bool IsDirectory(string path) => File.GetAttributes(path).HasFlag(FileAttributes.Directory);
    }
}

#endif
