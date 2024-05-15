using TBydFramework.TextMeshPro.Runtime.Views.TextMeshPro;
using UnityEditor;
using UnityEngine;

namespace TBydFramework.TextMeshPro.Editor
{
    [CustomPropertyDrawer(typeof(Parameters), true)]
    public class ParametersDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty textProperty = property.FindPropertyRelative("m_Text");
            return base.GetPropertyHeight(textProperty, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            SerializedProperty textProperty = property.FindPropertyRelative("m_Text");
            SerializedProperty capacityProperty = property.FindPropertyRelative("m_Capacity");
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, textProperty, label);
            if (EditorGUI.EndChangeCheck())
            {
                if (textProperty.objectReferenceValue != null)
                {
                    IFormattableText formableText = (IFormattableText)textProperty.objectReferenceValue;
                    if (capacityProperty != null)
                    {
                        int count = formableText.ParameterCount;
                        capacityProperty.intValue = count;
                    }
                }
            }
            EditorGUI.EndProperty();
        }
    }
}