using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace TBydFramework.Editor.Windows
{
    public class MyOdinMenuEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("X/MagicBox")]
        private static void OpenWindow()
        {
            var window = GetWindow<MyOdinMenuEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(supportsMultiSelect: false);
            
            // const string projectsettingsAssetPath = "ProjectSettings/ProjectSettings.asset";
            // var ps = AssetDatabase.LoadAllAssetsAtPath(projectsettingsAssetPath);
            // var pso = new SerializedObject(ps[0]);
            tree.Add("Player Settings", Resources.FindObjectsOfTypeAll<PlayerSettings>().FirstOrDefault(), EditorIcons.SettingsCog);
            tree.Add("AOT Generation", AOTGenerationConfig.Instance, EditorIcons.SmartPhone);

            tree.AddAllAssetsAtPath("Databases", "Configs/Database", typeof(ScriptableObject), true)
                .AddThumbnailIcons();

            // tree.SortMenuItemsByName();
            return tree;
        }
    }
}