using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.ReadOnly
{
    public class Example1 : MonoBehaviour
    {
        [ReadOnly]
        public string MyString = "这将显示为文本";

        [ReadOnly]
        public int MyInt = 9001;

        [ReadOnly]
        public int[] MyIntList = new int[] { 1, 2, 3, 4, 5, 6, 7, };
    }
}