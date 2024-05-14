using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.OnInspectorInit
{
    public class Example1 : MonoBehaviour
    {
        [OnInspectorInit("Initialize")]
        public int myField;

        private void Initialize()
        {
            Debug.Log("Initializing...");
            // 在此处执行初始化工作
            myField = 42; // 设置字段默认值为42
        }
    }
}