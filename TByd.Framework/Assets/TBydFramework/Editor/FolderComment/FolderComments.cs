using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TBydFramework.Editor.FolderComment
{
    [CanEditMultipleObjects, CustomEditor(typeof(DefaultAsset))]
    public class ProjectFolderCommentsInspector : UnityEditor.Editor
    {
        private List<string> _assetsPath = new List<string>();
        private string _comment = string.Empty;
        private bool _isCommentChanged = false;

        void OnEnable()
        {
            _assetsPath.Clear();
            foreach (var obj in targets)
            {
                var assetPathTmp = AssetDatabase.GetAssetPath(obj);
                if (!AssetDatabase.IsValidFolder(assetPathTmp))
                    continue;

                _assetsPath.Add(assetPathTmp);
            }

            if (_assetsPath.Count > 0)
                _comment = ProjectFolderAssetDataManager.Instance.AssetPathToComment(_assetsPath[0]);
        }

        /// <summary>
        /// inspector的OnDestroy调用时机是切换到其他的inspector时
        /// </summary>
        void OnDestroy()
        {
            if (_isCommentChanged)
            {
                ProjectFolderAssetDataManager.Instance.SaveToFile();
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //只处理文件夹显示
            if (0 == _assetsPath.Count)
            {
                return;
            }

            var enabled = GUI.enabled;
            GUI.enabled = true;

            DrawFolder();

            GUI.enabled = enabled;
        }

        private void DrawFolder()
        {
            EditorGUILayout.PrefixLabel("Comments");

            GUI.changed = false;
            _comment = EditorGUILayout.TextArea(_comment, GUILayout.MinHeight(48));
            if (GUI.changed)
            {
                for (int i = 0; i < _assetsPath.Count; ++i)
                    ProjectFolderAssetDataManager.Instance.SetComment(_assetsPath[i], _comment);

                _isCommentChanged = true;
                EditorUtility.SetDirty(target);

                EditorApplication.RepaintProjectWindow();
            }
        }
    }
}