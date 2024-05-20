using UnityEngine;
using Zenject;

namespace TBydFramework.IOCZenject.Tests.HelloWorld
{
    public class HelloWorldExample : MonoBehaviour
    {
        [Inject] private HelloWorldService _helloWorldService;

        private void Start()
        {
            _helloWorldService.HelloWorld();
        }
    }
}