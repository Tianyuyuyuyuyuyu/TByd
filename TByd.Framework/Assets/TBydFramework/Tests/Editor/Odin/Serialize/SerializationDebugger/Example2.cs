using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Serialize.SerializationDebugger
{
    public class Example2 : SerializedMonoBehaviour
    {
        public string UnityString = "Unity_TYY";

        [OdinSerialize]
        public string OdinAndUnityString = "OdinAndUnity_TYY";
        [OdinSerialize][NonSerialized]
        public string OdinString = "Odin_TYY";

        public List<int> OdinList = new List<int>();

        [SerializeField]
        public TempUnitySerializationData tempUnitySerializationData = new TempUnitySerializationData();

        public TempOdinSerializationData tempOdinSerializationData = new TempOdinSerializationData();

        public Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
        void Start()
        {

        }
    }
}