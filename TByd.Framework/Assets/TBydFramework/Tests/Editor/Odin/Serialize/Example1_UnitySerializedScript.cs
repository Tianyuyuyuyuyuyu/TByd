using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Serialize
{
    public class Example1_UnitySerializedScript : MonoBehaviour
    {
        public Dictionary<int, string> keyValuePairs_0 = new Dictionary<int, string>();

        [ShowInInspector]
        public Dictionary<int, string> keyValuePairs_1 = new Dictionary<int, string>();
    }
}