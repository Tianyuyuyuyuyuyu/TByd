using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.LabelWidth
{
    public class Example1 : MonoBehaviour
    {
        public int DefaultWidth;

        [LabelWidth(50)]
        public int Thin;

        [LabelWidth(250)]
        public int Wide;
    }
}