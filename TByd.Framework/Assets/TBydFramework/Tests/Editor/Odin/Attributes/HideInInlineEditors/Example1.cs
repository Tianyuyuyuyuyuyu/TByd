using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Examples;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideInInlineEditors
{
    public class Example1 : MonoBehaviour
    {
        [InfoBox("Click the pen icon to open a new inspector window for the InlineObject too see the differences these attributes make.")]
        [InlineEditor(Expanded = true)]
        public MyInlineScriptableObject InlineObject;

        private void Start()
        {
            InlineObject =  ExampleHelper.GetScriptableObject<MyInlineScriptableObject>("MyInlineScriptableObject");
        }
    }
    
    [CreateAssetMenu(fileName = "MyInlineScriptableObject_ScriptableObject", menuName = "CreatScriptableObject/MyInlineScriptableObject", order = 100)]
    public class MyInlineScriptableObject  : ScriptableObject
    {
        [ShowInInlineEditors]
        public string ShownInInlineEditor;

        [HideInInlineEditors]//在绘制的里面不显示
        public string HiddenInInlineEditor;
    }
}