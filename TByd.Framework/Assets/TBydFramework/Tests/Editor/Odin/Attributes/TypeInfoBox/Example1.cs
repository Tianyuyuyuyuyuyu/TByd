using System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Examples;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.TypeInfoBox
{
    public class Example1 : MonoBehaviour
    {
        public MyType MyObject = new MyType();

        [InfoBox("双击此此段的value值，可在inspecter中查看对应ScriptableObject信息")]
        public MyScripty Scripty = null;
        public void Awake()
        {
            Scripty = ExampleHelper.GetScriptableObject<MyScripty>("MyScripty");
        }

        [Serializable]
        [TypeInfoBox("TypeInfoBox特性可以放在类型定义上，并将导致在属性的顶端处绘制一个InfoBox。")]
        public class MyType
        {
            public int Value;
        }
    }

    [CreateAssetMenu(fileName = "MyScripty_ScriptableObject", menuName = "CreatScriptableObject/MyScripty", order = 100)]
    [TypeInfoBox("TypeInfoBox 特性 能以文本的形式显示在顶端 。例如, MonoBehaviours or ScriptableObjects.")]
    public class MyScripty : ScriptableObject
    {
        public string MyText = ExampleHelper.GetString();
        [TextArea(10, 15)]
        public string Box;
    }
}