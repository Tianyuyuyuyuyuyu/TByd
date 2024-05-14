using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Serialize.SerializationDebugger
{
    public class Example1 : MonoBehaviour
    {

        public string UnityString = "Unity_TYY";
        public List<int> UnityStringList = new List<int>();

        [NonSerialized][OdinSerialize]
        public string OdinStringInvalid= "错误序列化";

        public TempUnitySerializationData tempUnitySerializationData = new TempUnitySerializationData();
        public TempOdinSerializationData tempOdinSerializationData = new TempOdinSerializationData();

        public List<int> UnityList = new List<int>();

        public Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

        void Start()
        {

        }

    }
    [Serializable]
    public class TempUnitySerializationData
    {
        public string UnityString = "菜鸟TYY";

    }

    public class TempOdinSerializationData
    {
        public string UnityString = "菜鸟TYY";
    }
}