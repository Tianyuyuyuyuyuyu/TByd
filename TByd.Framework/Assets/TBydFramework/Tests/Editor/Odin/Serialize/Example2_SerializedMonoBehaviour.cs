using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace TBydFramework.Tests.Editor.Odin.Serialize
{
    public class Example2_SerializedMonoBehaviour : SerializedMonoBehaviour
    {
        public Dictionary<int, string> keyValuePairs_0 = new Dictionary<int, string>();

        [ShowInInspector]
        public Dictionary<int, string> keyValuePairs_1 = new Dictionary<int, string>();
    }
}