using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.OnInspectorDispose
{
    public class Example2 : MonoBehaviour
    {
        [OnInspectorDispose("Cleanup")]
        public int myField;

        private void Cleanup()
        {
            Debug.Log("Cleaning up...");
            // 在此处执行资源释放或者其他清理工作
        }
    }
}