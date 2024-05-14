using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.ShowInInlineEditors
{
    public class Example1 : MonoBehaviour
    {
        [InfoBox("单击属性值打开一个新的检查窗口，也可以看到这些属性的不同.")]
        [InlineEditor(Expanded = true)]
        public MyInlineScriptableObject InlineObject;
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