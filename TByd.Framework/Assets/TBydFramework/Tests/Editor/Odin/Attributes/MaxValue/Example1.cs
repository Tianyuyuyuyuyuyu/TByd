using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.MaxValue
{
    public class Example1 : MonoBehaviour
    {
        [MaxValue(0)]
        public int IntMaxValue0;

        [MaxValue(0)]
        public float FloatMaxValue0;

        [MaxValue(0)]
        public Vector3 Vector3MaxValue0;
    }
}