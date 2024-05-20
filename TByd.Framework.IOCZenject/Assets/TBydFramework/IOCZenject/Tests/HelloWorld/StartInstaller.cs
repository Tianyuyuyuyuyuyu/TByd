using UnityEngine;
using Zenject;

namespace TBydFramework.IOCZenject.Tests.HelloWorld
{
    public class StartInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<HelloWorldService>().AsSingle();
        }
    }
    
    public class HelloWorldService
    {
        public void HelloWorld()
        {
            Debug.LogError("Hello World");
        }
    }
}