using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.HideIfGroup
{
    public class Example1 : MonoBehaviour
    {
        public bool Toggle = true;

        [HideIfGroup("Toggle")]
        [BoxGroup("Toggle/Shown Box")]
        public int A, B;

        [BoxGroup("Box")]
        public InfoMessageType EnumField = InfoMessageType.Info;

        [BoxGroup("Box")]
        [HideIfGroup("Box/Toggle")]
        public Vector3 X, Y;

        //与常规if属性一样，HideIfGroup也支持指定值。
        //您还可以将多个HideIfGroup属性链接在一起，以实现更复杂的行为。
        [HideIfGroup("Box/Toggle/EnumField", Value = InfoMessageType.Info)]
        [BoxGroup("Box/Toggle/EnumField/Border", ShowLabel = true)]
        public string Name;

        [BoxGroup("Box/Toggle/EnumField/Border")]
        public Vector3 Vector;

        //要在隐藏组时使用的成员的名称。默认为组的名称，
        //可以通过设置此属性来覆盖。
        [ShowIfGroup("RectGroup", MemberName = "Toggle")]
        public Rect Rect;
    }
}