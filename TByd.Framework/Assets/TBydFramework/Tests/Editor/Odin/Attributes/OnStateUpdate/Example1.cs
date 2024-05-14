using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.OnStateUpdate
{
    public class Example1 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        public List<int> list_0;
        [OnStateUpdate("@#(list_0).State.Expanded = $value")]
        public bool ExpandList;//此 bool 值通过上述表达式控制 list_0 列表

        [OnStateUpdate("@$property.State.Expanded = ExpandList_1")]
        public List<int> list_1;
        public bool ExpandList_1;

        [OnStateUpdate("@$property.State.Visible = ToggleMyInt")]
        public int MyInt;
        public bool ToggleMyInt;

        [OnStateUpdate("CustomPropertyUpdateCallBack")]
        public string MyString;

        private void CustomPropertyUpdateCallBack(string tempMyString)
        {
            if (!string.IsNullOrEmpty(tempMyString))
            {
                Debug.Log(tempMyString);
            }
        }
    }
}