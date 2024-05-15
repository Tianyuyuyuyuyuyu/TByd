using TBydFramework.TextMeshPro.Runtime.Views.TextMeshPro;
using TMPro.EditorUtilities;
using UnityEditor;
using UnityEngine;

namespace TBydFramework.TextMeshPro.Editor
{
    [CustomEditor(typeof(TemplateTextMeshProUGUI), true), CanEditMultipleObjects]
    public class TemplateTextMeshProUIEditorPanel : TMP_EditorPanelUI
    {
        static readonly GUIContent k_TemplateLabel = new GUIContent("Template", "text template");

        SerializedProperty m_TemplateProp;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_TemplateProp = serializedObject.FindProperty("m_Template");
        }

        public override void OnInspectorGUI()
        {
            // Make sure Multi selection only includes TMP Text objects.
            if (IsMixSelectionTypes()) return;

            serializedObject.Update();

            DrawTemplateInput();

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

        protected void DrawTemplateInput()
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(m_TemplateProp, k_TemplateLabel);
            if (EditorGUI.EndChangeCheck())
            {
                m_HavePropertiesChanged = true;
            }
        }
    }
}