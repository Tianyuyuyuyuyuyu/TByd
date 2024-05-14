using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.EnumPaging
{
    public class Example1 : MonoBehaviour
    {
        [EnumPaging]
        public SomeEnum SomeEnumField;

        public enum SomeEnum
        {
            A, B, C
        }

        [ShowInInspector]
        [EnumPaging, OnValueChanged("SetCurrentTool")]
        [InfoBox("Changing this property will change the current selected tool in the Unity editor.")]
        private UnityEditor.Tool sceneTool;

        private void SetCurrentTool()
        {
            UnityEditor.Tools.current = this.sceneTool;
        }
    }
}