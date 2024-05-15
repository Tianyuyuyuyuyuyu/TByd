using TBydFramework.TextFormatting.Runtime.Views.UGUI;
using UnityEditor;
using UnityEditor.UI;

namespace TBydFramework.TextFormatting.Editor.Views.UGUI
{
    [CustomEditor(typeof(TemplateText), true)]
    [CanEditMultipleObjects]
    public class TemplateTextEditor : GraphicEditor
    {
        SerializedProperty m_Template;
        SerializedProperty m_FontData;
        protected override void OnEnable()
        {
            base.OnEnable();
            m_Template = serializedObject.FindProperty("m_Template");
            m_FontData = serializedObject.FindProperty("m_FontData");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Template);
            EditorGUILayout.PropertyField(m_FontData);

            AppearanceControlsGUI();
            RaycastControlsGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}