using System.Collections.Generic;
using TBydFramework.Runtime.Log;
using TBydFramework.Runtime.Singleton;
using UnityEditor;
using UnityEngine;

namespace TBydFramework.Editor.FolderComment
{
    /// <summary>
    /// 编辑器文件夹绘制使用的相关数据管理器
    /// </summary>
    [InitializeOnLoad]
    public class ProjectFolderAssetDataManager : Singleton<ProjectFolderAssetDataManager>
    {
        static ProjectFolderAssetDataManager()
        {
            EditorApplication.projectWindowItemOnGUI += ProjectWindowItemOnGUI;
        }

        /// <summary>
        /// 单例应有一个空构造
        /// </summary>
        private ProjectFolderAssetDataManager()
        {
        }

        public override void OnSingletonInit()
        {
            LoadFromFile(SAVE_DATA_PATH);
        }

        public class ProjectFolderAssetData
        {
            //该路径仅仅作为参考(方便查看配置文本)，并无实际意义
            public string assetPath;
            public string comment;
        }

        //保存路径
        private string SAVE_DATA_PATH => Application.dataPath + "/../ProjectSettings/ProjectFolderCommentsData.txt";

        //文件夹注释字典
        private Dictionary<string, ProjectFolderAssetData> _folderDatas = new Dictionary<string, ProjectFolderAssetData>();

        /// <summary>
        /// 获取文件夹注释
        /// <param name="assetPath">文件夹路径</param>
        /// <return>文件夹注释，如果没有则返回空字符串</return>
        /// </summary>
        public string AssetPathToComment(string assetPath)
        {
            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            return AssetGuidToComment(guid);
        }

        /// <summary>
        /// 获取文件夹注释
        /// <param name="guid">文件夹唯一id</param>
        /// <return>文件夹注释，如果没有则返回空字符串</return>
        /// </summary>
        public string AssetGuidToComment(string guid)
        {
            return _folderDatas.TryGetValue(guid, out var findData) ? findData.comment : string.Empty;
        }

        /// <summary>
        /// 获取文件夹数据
        /// <param name="guid">文件夹唯一id</param>
        /// <return>文件夹数据，没有则返回空</return>
        /// </summary>
        public ProjectFolderAssetData GetFolderData(string guid)
        {
            _folderDatas.TryGetValue(guid, out var findData);
            return findData;
        }

        /// <summary>
        /// 设置文件夹注释，并自动保存到文件
        /// <param name="assetPath">文件夹路径</param>
        /// <param name="comment">文件夹注释</param>
        /// </summary>
        public void SetComment(string assetPath, string comment)
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                XLogger.Log("ProjectFolderAssetDataManager SetComment error: assetPath is empty");
                return;
            }

            if (assetPath.StartsWith("Assets") == false)
            {
                XLogger.Log("ProjectFolderAssetDataManager SetComment error: not Unity project path=" + assetPath);
                return;
            }

            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            if (!_folderDatas.TryGetValue(guid, out var findData))
            {
                findData = new ProjectFolderAssetData
                {
                    assetPath = assetPath,
                    comment = comment
                };

                _folderDatas.Add(guid, findData);
            }
            else
            {
                if (string.IsNullOrEmpty(comment))
                {
                    _folderDatas.Remove(guid);
                }
                else
                {
                    findData.assetPath = assetPath;
                    findData.comment = comment;
                }
            }
        }

        /// <summary>
        /// 保存文件夹信息到文件中
        /// </summary>
        public void SaveToFile()
        {
            SaveToFile(SAVE_DATA_PATH);
        }
        private void SaveToFile(string path)
        {
            if (_folderDatas.Count == 0)
            {
                System.IO.File.Delete(path);
                return;
            }

            var saveStr = new System.Text.StringBuilder();
            foreach (var valuePair in _folderDatas)
            {
                saveStr.Append(valuePair.Key);
                saveStr.Append('\n');
                saveStr.Append(valuePair.Value.assetPath);
                saveStr.Append('\n');
                saveStr.Append(valuePair.Value.comment);
                saveStr.Append('\n');
            }

            if (saveStr.Length > 0)
                saveStr.Remove(saveStr.Length - 1, 1);

            var directoryInfo = new System.IO.DirectoryInfo(path).Parent;
            if (directoryInfo?.Exists == false)
            {
                directoryInfo.Create();
            }

            System.IO.File.WriteAllText(path, saveStr.ToString(), System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 从文件读取文件夹信息
        /// <param name="path">保存路径</param>
        /// </summary>
        private void LoadFromFile(string path)
        {
            _folderDatas.Clear();

            if (!System.IO.File.Exists(path))
                return;

            try
            {
                using (var readStream = new System.IO.StreamReader(path))
                {
                    while (true)
                    {
                        var guid = readStream.ReadLine();
                        if (string.IsNullOrEmpty(guid))
                            break;

                        var assetPath = readStream.ReadLine();
                        if (string.IsNullOrEmpty(assetPath))
                        {
                            throw new System.Exception("ProjectFolderAssetDataManager LoadFromFile error: not found assetPath, guid=" + guid);
                        }

                        var comment = readStream.ReadLine();
                        if (string.IsNullOrEmpty(comment))
                        {
                            throw new System.Exception("ProjectFolderAssetDataManager LoadFromFile error: not found comment, name=" + assetPath);
                        }

                        var folderAssetsData = new ProjectFolderAssetData
                        {
                            assetPath = assetPath,
                            comment = comment
                        };

                        _folderDatas.Add(guid, folderAssetsData);
                    }
                    readStream.Close();
                }
            }
            catch (System.Exception e)
            {
                XLogger.Log("ProjectFolderAssetDataManager LoadFromFile exception: e=" + e);
                System.IO.File.Delete(path);
            }
        }

        private static GUIStyle _guiStyleLabelTree;
        private static GUIStyle _guiStyleLabelNotTree;

        private static void ProjectWindowItemOnGUI(string guid, Rect selectionRect)
        {
            var folderData = Instance.GetFolderData(guid);
            if (null == folderData || string.IsNullOrEmpty(folderData.comment))
                return;

            if (null == _guiStyleLabelTree)
            {
                _guiStyleLabelTree = EditorStyles.label;
                _guiStyleLabelNotTree = EditorStyles.label;

                _guiStyleLabelTree.fontSize = 12;
                _guiStyleLabelNotTree.fontSize = 10;
            }

            var aliasContent = new GUIContent(folderData.comment);
            var isTree = IsTreeView(selectionRect);
            var labelStyle = isTree ? _guiStyleLabelTree : _guiStyleLabelNotTree;
            var labelSize = labelStyle.CalcSize(aliasContent);
            float offsetYWhenCurrentSelectAsset = 0;

            if (!isTree)
            {
                IsIconSmall(ref selectionRect);

                if (null != Selection.objects)
                {
                    if (null != System.Array.Find(Selection.objects, v => AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(v)) == guid))
                        offsetYWhenCurrentSelectAsset = -labelSize.y * 0.167f;
                }
            }

            var textRect = new Rect(
                selectionRect.x + Mathf.Max(0, (selectionRect.width - labelSize.x) * 0.5f),
                selectionRect.yMax + (isTree ? -labelSize.y - labelSize.y * 0.167f : labelSize.y * 0.33f - labelSize.y) + offsetYWhenCurrentSelectAsset,
                labelSize.x, labelSize.y);

            var cropWidth = selectionRect.width;

            if (isTree)
            {
                textRect.width = System.Math.Min(labelSize.x, selectionRect.width / 3);
                cropWidth = textRect.width;
                textRect.x = selectionRect.xMax - textRect.width;
                selectionRect.y = selectionRect.y;
            }

            aliasContent.text = CropText(labelStyle, aliasContent.text, cropWidth);
            EditorGUI.LabelField(textRect, aliasContent, labelStyle);
        }

        private static bool IsTreeView(Rect rect)
        {
            return rect.height <= 21f;
        }

        private static bool IsIconSmall(ref Rect rect)
        {
            var isSmall = rect.width > rect.height;

            if (isSmall)
                rect.width = rect.height;
            else
                rect.height = rect.width;
            return isSmall;
        }

        private static System.Reflection.MethodInfo _getNumCharactersThatFitWithinWidth;

        /// <summary>
        /// UnityEditor.ObjectListArea - GetCroppedLabelText (G:1211)
        /// </summary>
        /// <param name="self"></param>
        /// <param name="text"></param>
        /// <param name="cropWidth"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static string CropText(GUIStyle self, string text, in float cropWidth, string symbol = "…")
        {
            if (null == _getNumCharactersThatFitWithinWidth)
            {
                _getNumCharactersThatFitWithinWidth = typeof(GUIStyle).GetMethod("GetNumCharactersThatFitWithinWidth",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            }

            if (_getNumCharactersThatFitWithinWidth != null)
            {
                var thatFitWithinWidth = (int)_getNumCharactersThatFitWithinWidth.Invoke(self, new object[] { text, cropWidth });

                int num;
                switch (thatFitWithinWidth)
                {
                    case -1:
                        return text;
                    case 0:
                    case 1:
                        num = 0;
                        break;
                    default:
                        num = thatFitWithinWidth != text.Length ? 1 : 0;
                        break;
                }
                text = num == 0 ? text : text.Substring(0, thatFitWithinWidth - 1) + symbol;
            }

            return text;
        }
    }
}