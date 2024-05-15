using TBydFramework.TextMeshPro.Runtime.Views.TextMeshPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

namespace TBydFramework.TextMeshPro.Editor
{
    [CustomEditor(typeof(FormattableTextMeshProUGUI), true), CanEditMultipleObjects]
    public class FormattableTextMeshProUIEditorPanel : TMP_EditorPanelUI
    {
        static readonly GUIContent k_FormatLabel = new GUIContent("Format", "text formatting");
        static readonly GUIContent k_ParameterCountLabel = new GUIContent("Parameter Count", "Parameter Count");

        SerializedProperty m_FormatProp;
        SerializedProperty m_ParameterCountProp;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_FormatProp = serializedObject.FindProperty("m_Format");
            m_ParameterCountProp = serializedObject.FindProperty("m_ParameterCount");
        }

        public override void OnInspectorGUI()
        {
            // Make sure Multi selection only includes TMP Text objects.
            if (IsMixSelectionTypes()) return;

            serializedObject.Update();

            DrawFormatParameters();

            DrawMainSettings();

            DrawExtraSettings();

            EditorGUILayout.Space();

            if (serializedObject.ApplyModifiedProperties() || m_HavePropertiesChanged)
            {
                m_TextComponent.havePropertiesChanged = true;
                m_HavePropertiesChanged = false;
                EditorUtility.SetDirty(target);
            }
        }

        protected void DrawFormatParameters()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(m_FormatProp, k_FormatLabel);
            if (EditorGUI.EndChangeCheck())
            {
                m_HavePropertiesChanged = true;
            }

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(m_ParameterCountProp, k_ParameterCountLabel);
            if (EditorGUI.EndChangeCheck())
            {
                m_HavePropertiesChanged = true;
            }
        }
    }
}