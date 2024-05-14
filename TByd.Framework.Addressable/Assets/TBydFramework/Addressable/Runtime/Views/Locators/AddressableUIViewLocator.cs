using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using TBydFramework.Runtime.Asynchronous;
using TBydFramework.Runtime.Views;
using TBydFramework.Runtime.Views.Locators;

namespace TBydFramework.Addressable.Runtime.Views.Locators
{
    /// <summary>
    /// The synchronous method can only load views or windows from the Resources folder, 
    /// and the asynchronous method loads views or windows from the addressable asset system.
    /// </summary>
    public class AddressableUIViewLocator : UIViewLocatorBase
    {
        private GlobalWindowManager globalWindowManager;

        protected virtual IWindowManager GetDefaultWindowManager()
        {
            if (globalWindowManager != null)
                return globalWindowManager;

            globalWindowManager = GameObject.FindObjectOfType<GlobalWindowManager>();
            if (globalWindowManager == null)
                throw new NotFoundException("GlobalWindowManager");

            return globalWindowManager;
        }

        protected string Normalize(string name)
        {
            int index = name.IndexOf('.');
            if (index < 0)
                return name;

            return name.Substring(0, index);
        }

        public override T LoadView<T>(string name)
        {
            return DoLoadView<T>(name);
        }

        protected virtual T DoLoadView<T>(string name)
        {
            name = Normalize(name);
            GameObject viewTemplateGo = Resources.Load<GameObject>(name);
            if (viewTemplateGo == null)
                return default(T);

            viewTemplateGo.SetActive(false);
            GameObject go = GameObject.Instantiate(viewTemplateGo);
            go.name = viewTemplateGo.name;
            T view = go.GetComponent<T>();
            if (view == null && go != null)
                GameObject.Destroy(go);
            return view;
        }

        public override IProgressResult<float, T> LoadViewAsync<T>(string name)
        {
            ProgressResult<float, T> result = new ProgressResult<float, T>();
            DoLoad<T>(result, name);
            return result;
        }

        protected virtual async void DoLoad<T>(IProgressPromise<float, T> promise, string name, IWindowManager windowManager = null)
        {
            try
            {
                promise.UpdateProgress(0f);
                var result = Addressables.InstantiateAsync(name);
                while (!result.IsDone)
                {
                    await new WaitForSecondsRealtime(0.05f);
                    promise.UpdateProgress(result.PercentComplete);
                }

                if (result.OperationException != null)
                {
                    promise.SetException(result.OperationException);
                    return;
                }

                GameObject go = result.Result;
                go.SetActive(false);

                T view = go.GetComponent<T>();
                if (view == null)
                {
                    //GameObject.Destroy(go);
                    Addressables.ReleaseInstance(go);
                    promise.SetException(new NotFoundException(name));
                }

                if (windowManager != null && view is IWindow)
                    (view as IWindow).WindowManager = windowManager;

                promise.UpdateProgress(1f);
                promise.SetResult(view);
            }
            catch (Exception e)
            {
                promise.SetException(e);
            }
        }

        public override T LoadWindow<T>(string name)
        {
            return LoadWindow<T>(null, name);
        }

        public override T LoadWindow<T>(IWindowManager windowManager, string name)
        {
            if (windowManager == null)
                windowManager = this.GetDefaultWindowManager();

            T target = this.DoLoadView<T>(name);
            if (target != null)
                target.WindowManager = windowManager;

            return target;
        }

        public override IProgressResult<float, T> LoadWindowAsync<T>(string name)
        {
            return this.LoadWindowAsync<T>(null, name);
        }

        public override IProgressResult<float, T> LoadWindowAsync<T>(IWindowManager windowManager, string name)
        {
            if (windowManager == null)
                windowManager = this.GetDefaultWindowManager();

            ProgressResult<float, T> result = new ProgressResult<float, T>();
            DoLoad<T>(result, name, windowManager);
            return result;
        }
    }
}
