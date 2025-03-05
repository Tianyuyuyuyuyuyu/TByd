using System;
using System.Collections.Generic;
using System.IO;
using TByd.CodeStyle.Runtime.Config;
using UnityEditor;
using UnityEngine;

namespace TByd.CodeStyle.Editor.CodeCheck.EditorConfig
{
    /// <summary>
    /// EditorConfig格式化菜单
    /// </summary>
    public static class EditorConfigFormatMenu
    {
        /// <summary>
        /// 格式化选中的文件或文件夹
        /// </summary>
        [MenuItem("Assets/TByd/根据EditorConfig格式化", false, 1000)]
        public static void FormatSelected()
        {
            // 获取配置
            var enableEditorConfig = ConfigManager.GetConfig().EnableEditorConfig;

            // 如果未启用EditorConfig，则显示提示
            if (!enableEditorConfig)
            {
                EditorUtility.DisplayDialog(
                    "EditorConfig未启用",
                    "请在TByd代码风格工具中启用EditorConfig支持。",
                    "确定");
                return;
            }

            // 如果项目中没有EditorConfig文件，则显示提示
            if (!EditorConfigManager.HasProjectEditorConfig())
            {
                var createConfig = EditorUtility.DisplayDialog(
                    "未找到EditorConfig文件",
                    "项目中未找到EditorConfig文件。是否创建默认配置？",
                    "创建默认配置",
                    "取消");

                if (createConfig)
                {
                    EditorConfigManager.CreateDefaultEditorConfig();
                }
                else
                {
                    return;
                }
            }

            // 获取选中的资源路径
            var selectedAssets = GetSelectedAssetPaths();

            if (selectedAssets.Length == 0)
            {
                return;
            }

            // 收集需要格式化的文件
            var filesToFormat = new List<string>();

            foreach (var assetPath in selectedAssets)
            {
                // 检查是否是文件夹
                if (Directory.Exists(assetPath))
                {
                    // 获取文件夹中的所有C#文件
                    var files = Directory.GetFiles(assetPath, "*.cs", SearchOption.AllDirectories);
                    filesToFormat.AddRange(files);
                }
                else if (File.Exists(assetPath) && Path.GetExtension(assetPath).ToLowerInvariant() == ".cs")
                {
                    // 添加C#文件
                    filesToFormat.Add(assetPath);
                }
            }

            if (filesToFormat.Count == 0)
            {
                EditorUtility.DisplayDialog(
                    "没有可格式化的文件",
                    "选中的资源中没有可格式化的C#文件。",
                    "确定");
                return;
            }

            // 显示确认对话框
            var confirm = EditorUtility.DisplayDialog(
                "格式化文件",
                $"将根据EditorConfig规则格式化 {filesToFormat.Count} 个C#文件。\n\n" +
                "格式化可能会修改文件的缩进、行尾、空白字符等。\n\n" +
                "建议在执行此操作前备份或提交您的更改。\n\n" +
                "是否继续？",
                "继续",
                "取消");

            if (!confirm)
            {
                return;
            }

            // 显示进度条
            EditorUtility.DisplayProgressBar("格式化文件", "正在准备格式化...", 0f);

            try
            {
                var totalFiles = filesToFormat.Count;
                var formattedFiles = 0;
                var failedFiles = 0;
                var failedFilesList = new List<string>();

                // 格式化每个文件
                for (var i = 0; i < totalFiles; i++)
                {
                    var file = filesToFormat[i];

                    // 更新进度条
                    EditorUtility.DisplayProgressBar(
                        "格式化文件",
                        $"正在格式化 {Path.GetFileName(file)}...",
                        (float)i / totalFiles);

                    // 格式化文件
                    var success = EditorConfigManager.FormatFile(file);

                    if (success)
                    {
                        formattedFiles++;
                    }
                    else
                    {
                        failedFiles++;
                        failedFilesList.Add(file);
                    }
                }

                // 刷新资源数据库
                AssetDatabase.Refresh();

                // 显示结果
                EditorUtility.ClearProgressBar();

                var message = "格式化完成！\n\n" +
                              $"总文件数: {totalFiles}\n" +
                              $"成功格式化的文件: {formattedFiles}\n" +
                              $"格式化失败的文件: {failedFiles}";

                if (failedFiles > 0)
                {
                    message += "\n\n格式化失败的文件:";

                    // 最多显示10个文件
                    var displayCount = Mathf.Min(failedFilesList.Count, 10);
                    for (var i = 0; i < displayCount; i++)
                    {
                        var relativePath = failedFilesList[i].Replace(Application.dataPath, "Assets");
                        message += $"\n- {relativePath}";
                    }

                    if (failedFilesList.Count > 10)
                    {
                        message += $"\n... 以及其他 {failedFilesList.Count - 10} 个文件";
                    }
                }

                EditorUtility.DisplayDialog("格式化结果", message, "确定");
            }
            finally
            {
                EditorUtility.ClearProgressBar();
            }
        }

        /// <summary>
        /// 验证菜单项是否可用
        /// </summary>
        [MenuItem("Assets/TByd/根据EditorConfig格式化", true)]
        public static bool ValidateFormatSelected()
        {
            // 获取选中的资源路径
            var selectedAssets = GetSelectedAssetPaths();

            if (selectedAssets.Length == 0)
            {
                return false;
            }

            // 检查是否有可格式化的文件
            foreach (var assetPath in selectedAssets)
            {
                // 检查是否是文件夹
                if (Directory.Exists(assetPath))
                {
                    return true;
                }

                if (File.Exists(assetPath) && Path.GetExtension(assetPath).ToLowerInvariant() == ".cs")
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取选中的资源路径
        /// </summary>
        /// <returns>选中的资源路径</returns>
        private static string[] GetSelectedAssetPaths()
        {
            var selectedObjects = Selection.objects;

            if (selectedObjects == null || selectedObjects.Length == 0)
            {
                return Array.Empty<string>();
            }

            var assetPaths = new List<string>();

            foreach (var obj in selectedObjects)
            {
                var assetPath = AssetDatabase.GetAssetPath(obj);

                if (!string.IsNullOrEmpty(assetPath))
                {
                    // 转换为绝对路径
                    if (assetPath.StartsWith("Assets/"))
                    {
                        var projectPath = Application.dataPath;
                        var fullPath = Path.Combine(
                            projectPath.Substring(0, projectPath.Length - 6), // 移除 "Assets" 部分
                            assetPath);

                        assetPaths.Add(fullPath);
                    }
                }
            }

            return assetPaths.ToArray();
        }
    }
}
