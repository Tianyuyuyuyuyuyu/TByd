using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideReferenceObjectPicker
{
    public class Example1 : MonoBehaviour
    {
        [Title("Hidden Object Pickers")]
        [ShowInInspector]
        [HideReferenceObjectPicker]
        public MyCustomReferenceType OdinSerializedProperty1 = new MyCustomReferenceType();
        [ShowInInspector]
        [HideReferenceObjectPicker]
        public MyCustomReferenceType OdinSerializedProperty2 = new MyCustomReferenceType();
        [ShowInInspector]
        [PropertySpace(40)]
        [Title("Shown Object Pickers")]
        public MyCustomReferenceType OdinSerializedProperty3 = new MyCustomReferenceType();
        [ShowInInspector]
        public MyCustomReferenceType OdinSerializedProperty4 = new MyCustomReferenceType();

        // Protip: 您还可以将HideInInspector属性放在类定义本身上，以便为所有成员全局隐藏它。
        //[HideReferenceObjectPicker]
        public class MyCustomReferenceType
        {
            public int A;
            public int B;
            public int C;
        }
    }
}