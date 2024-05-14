using Sirenix.OdinInspector;
using UnityEngine;

namespace TBydFramework.Tests.Editor.Odin.Attributes.ChildGameObjectsOnly
{
    public class Example1 : MonoBehaviour
    {
        [ChildGameObjectsOnly(IncludeSelf = false)]//是否包含顶层定节点
        public Transform ChildOrSelfTransform;

        [ChildGameObjectsOnly]
        public GameObject ChildGameObject;

        [ChildGameObjectsOnly(IncludeSelf = false)]
        public Light[] Lights;

        public void Start()
        {
            Debug.Log(ChildOrSelfTransform);
            Debug.Log(ChildGameObject);
        }
    }
}