using TBydFramework.Pool.Runtime;
using TBydFramework.Pool.Runtime.Base;
using UnityEngine;

namespace TBydFramework.Pool.Tests
{
    public class PooledObjectExample : MonoBehaviour, IPoolCallbackReceiver
    {
        public void OnRent()
        {
            Debug.Log($"对象 {gameObject.name} 被租用");
        }

        public void OnReturn()
        {
            Debug.Log($"对象 {gameObject.name} 被归还");
        }
    }
}