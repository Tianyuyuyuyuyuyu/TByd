using TBydFramework.TextFormatting.Runtime.Views.UGUI;
using UnityEditor;
using UnityEditor.UI;

namespace TBydFramework.TextFormatting.Editor.Views.UGUI
{
    [CustomEditor(typeof(FormattableText), true)]
    [CanEditMultipleObjects]
    public class FormattableTextEditor : GraphicEditor
    {
        SerializedProperty m_Format;
        SerializedProperty m_FontData;
        SerializedProperty m_ParameterCount;
        protected override void OnEnable()
        {
            base.OnEnable();
            m_Format = serializedObject.FindProperty("m_Format");
            m_FontData = serializedObject.FindProperty("m_FontData");
            m_ParameterCount = serializedObject.FindProperty("m_ParameterCount");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_Format);
            EditorGUILayout.PropertyField(m_ParameterCount);
            EditorGUILayout.PropertyField(m_FontData);

            AppearanceControlsGUI();
            RaycastControlsGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}