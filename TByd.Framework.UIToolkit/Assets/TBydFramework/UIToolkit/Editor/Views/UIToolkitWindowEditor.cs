using System;
using TBydFramework.UIToolkit.Runtime;
using UnityEditor;
using UnityEngine;
using WindowType = TBydFramework.Runtime.Views.WindowType;

namespace TBydFramework.UIToolkit.Editor.Views
{
    [CustomEditor(typeof(UIToolkitWindow), true)]

    public class UIToolkitWindowEditor : UnityEditor.Editor
    {
        [SerializeField]
        private bool foldout = true;
        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();
            SerializedProperty property = serializedObject.GetIterator();
            var windowTypeProperty = serializedObject.FindProperty("windowType");

            WindowType windowType = (WindowType)windowTypeProperty.enumValueIndex;
            foldout = EditorGUILayout.Foldout(foldout, new GUIContent("Window Settings", ""));

            string[] windowSettings = new string[] { "panelSettings", "sourceAsset", "styleSheet", "windowType", "windowPriority" };

            bool expanded = true;
            while (property.NextVisible(expanded))
            {
                using (new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
                {
                    if ("m_Script" == property.propertyPath)
                        continue;

                    if (Array.IndexOf(windowSettings, property.propertyPath) >= 0)
                    {
                        if (foldout)
                        {
                            if ("windowPriority" == property.propertyPath && windowType != WindowType.QUEUED_POPUP)
                                continue;

                            EditorGUI.indentLevel++;
                            if ("windowPriority" == property.propertyPath)
                                EditorGUILayout.PropertyField(property, new GUIContent(property.displayName, "When pop-up windows are queued to open, windows with higher priority will be opened first."));
                            else
                                EditorGUILayout.PropertyField(property, new GUIContent(property.displayName));
                            EditorGUI.indentLevel--;
                        }
                        continue;
                    }

                    EditorGUILayout.PropertyField(property, true);
                }
                expanded = false;
            }

            this.serializedObject.ApplyModifiedProperties();
        }
    }
}