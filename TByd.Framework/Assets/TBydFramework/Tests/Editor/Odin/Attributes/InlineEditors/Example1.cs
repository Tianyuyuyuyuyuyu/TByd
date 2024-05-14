using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.InlineEditors
{
    public class Example1 : MonoBehaviour
    {
        [InfoBox("Click the pen icon to open a new inspector window for the InlineObject too see the difference this attribute make.")]
        [InlineEditor(Expanded = true)]
        public MyInlineScriptableObject InlineObject ;
    }

    [CreateAssetMenu(fileName = "MyInline_ScriptableObject", menuName = "CreatScriptableObject/MyInlineScriptableObject")]
    public class MyInlineScriptableObject : ScriptableObject
    {
        [ShowInInlineEditors]
        public string ShownInInlineEditor;

        [HideInInlineEditors]//在绘制的里面不显示
        public string HiddenInInlineEditor;

        [DisableInInlineEditors]//显示但是是灰态
        public string DisabledInInlineEditor;
    }
}