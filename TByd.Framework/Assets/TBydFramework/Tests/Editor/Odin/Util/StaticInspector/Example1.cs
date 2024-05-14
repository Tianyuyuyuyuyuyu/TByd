using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Util.StaticInspector
{
    public class Example1 : MonoBehaviour
    {
        public enum TempEnum
        {
            One,Two,Three
        }
        public static TempEnum tempEnum;
        public static string tempStr;
        public static int tempInt;
        public static List<int> staticInspectorTutorials_Ones = new List<int>();

        [Button(ButtonSizes.Large)]
        public static void TestStaticFunction()
        {
            Debug.Log("TestFunction");
        }
        [Button(ButtonSizes.Large, ButtonStyle.FoldoutButton)]
        public static void TestStaticFunction(string str)
        {
            Debug.Log($"TestFunction:{str}");
        }

        [Button(ButtonSizes.Large, ButtonStyle.FoldoutButton)]
        public static void TestStaticFunction(List<int> tempList)
        {
            for (int i = 0; i < tempList.Count; i++)
            {
                Debug.Log($"List Index :{i}---value:{tempList[i]}");
            }
        }

        public void NoStaticFunction()
        {
            Debug.Log("NoStaticFunction");
        }
    }

    public  class StaticInspectorTutorials_One
    {
        public static string tempStr;
    }
}