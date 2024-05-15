using System.Globalization;
using UnityEngine;
using TBydFramework.Runtime.Asynchronous;
using TBydFramework.Runtime.Binding;
using TBydFramework.Runtime.Contexts;
using TBydFramework.Runtime.Localizations;
using TBydFramework.Runtime.Log;
using TBydFramework.Runtime.Services;
using TBydFramework.Runtime.Views;
using TBydFramework.Runtime.Views.Locators;

namespace TBydFramework.Examples
{
    public class UGUIAndUIToolkitLauncher : MonoBehaviour
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(UGUIAndUIToolkitLauncher));

        private ApplicationContext context;
        void Awake()
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            GlobalWindowManager windowManager = FindObjectOfType<GlobalWindowManager>();
            if (windowManager == null)
                throw new NotFoundException("Not found the GlobalWindowManager.");

            context = Context.GetApplicationContext();

            IServiceContainer container = context.GetContainer();

            /* Initialize the data binding service */
            BindingServiceBundle bundle = new BindingServiceBundle(context.GetContainer());
            bundle.Start();

            /* Initialize the ui view locator and register UIViewLocator */
            container.Register<IUIViewLocator>(new DefaultUIViewLocator());

            /* Initialize the localization service */
            //CultureInfo cultureInfo = Locale.GetCultureInfoByLanguage (SystemLanguage.English);
            CultureInfo cultureInfo = Locale.GetCultureInfo();
            var localization = Localization.Current;
            localization.CultureInfo = cultureInfo;
            localization.AddDataProvider(new DefaultDataProvider("LocalizationExamples", new XmlDocumentParser()));

            /* register Localization */
            container.Register<Localization>(localization);
        }

        async void Start()
        {
            /* Create a window container */
            WindowContainer winContainer = WindowContainer.Create("MAIN");

            await new WaitForEndOfFrame();

            IUIViewLocator locator = context.GetService<IUIViewLocator>();
            //IWindow window = locator.LoadWindow<IWindow>(winContainer, "UI/UGUIWindow1");
            IWindow window = locator.LoadWindow<IWindow>(winContainer, "UI/Window1");
            window.Create();
            ITransition transition = window.Show().OnStateChanged((w, state) =>
            {
                log.DebugFormat("Window:{0} State{1}", w.Name, state);
            });

            //await transition;

            //await new WaitForSeconds(6f);
            //IWindow window2 = locator.LoadWindow<IWindow>("UI/Window2");
            //window2.Create();
            //await window2.Show();

            //await new WaitForSeconds(3f);
            //AlertDialog.ShowMessage("测试", "标题", "OK", r => { });

            //await new WaitForSeconds(3f);

            //TestWindow window2 = locator.LoadWindow<TestWindow>(winContainer, "UI/TestWindow2");
            //window2.Create();
            //ITransition transition2 = window2.Show().OnStateChanged((w, state) =>
            //{
            //    //log.DebugFormat("Window:{0} State{1}",w.Name,state);
            //});

        }
    }
}