using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideInEditorMode
{
    public class Example1 : MonoBehaviour
    {
        [Title("Hidden in editor mode")]//在editor下隐藏属性，运行时显示属性
        [HideInEditorMode]
        public int C;

        [HideInEditorMode]
        public int D;
    }
}