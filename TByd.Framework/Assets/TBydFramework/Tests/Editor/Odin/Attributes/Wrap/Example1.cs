using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.Wrap
{
    public class Example1 : MonoBehaviour
    {
        [Wrap(0f, 100f)]
        public int IntWrapFrom0To100;

        [Wrap(0f, 100f)]
        public float FloatWrapFrom0To100;

        [Wrap(0f, 100f)]
        public Vector3 Vector3WrapFrom0To100;

        [Wrap(0f, 360)]
        public float AngleWrap;

        [Wrap(0f, Mathf.PI * 2)]
        public float RadianWrap;
    }
}