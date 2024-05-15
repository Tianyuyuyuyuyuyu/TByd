using TBydFramework.FairyGUI.Runtime.Core;
using UnityEditor;

namespace TBydFramework.FairyGUI.Editor
{
    /// <summary>
    /// 
    /// </summary>
    [CustomEditor(typeof(StageCamera))]
    public class StageCameraEditor : UnityEditor.Editor
    {
        string[] propertyToExclude;

        void OnEnable()
        {
            propertyToExclude = new string[] { "m_Script" };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, propertyToExclude);

            if (serializedObject.ApplyModifiedProperties())
                (target as StageCamera).ApplyModifiedProperties();
        }
    }
}
