using System;
using System.Globalization;
using System.IO;
using TBydFramework.XLua.Runtime;
using TBydFramework.XLua.Runtime.Binding;
using TBydFramework.XLua.Runtime.LuaLoaders;
using TBydFramework.XLua.Runtime.Views;
using UnityEngine;
using TBydFramework.Runtime.Contexts;
using TBydFramework.Runtime.Localizations;
using TBydFramework.Runtime.Services;
using TBydFramework.Runtime.Views;
using TBydFramework.Runtime.Views.Locators;
using XLua;

namespace Samples.X_Framework_XLua._0._0._1.Examples
{
    public class LuaLauncher : MonoBehaviour
    {
        //private static readonly ILog log = LogManager.GetLogger(typeof(LuaLauncher));

        public ScriptReference script;

        private LuaTable scriptEnv;
        private LuaTable metatable;
        private Action<MonoBehaviour> onAwake;
        private Action<MonoBehaviour> onEnable;
        private Action<MonoBehaviour> onDisable;
        private Action<MonoBehaviour> onStart;
        private Action<MonoBehaviour> onDestroy;

        private ApplicationContext context;
        void Awake()
        {
            GlobalWindowManager windowManager = FindObjectOfType<GlobalWindowManager>();
            if (windowManager == null)
                throw new NotFoundException("Not found the GlobalWindowManager.");

            context = Context.GetApplicationContext();

            IServiceContainer container = context.GetContainer();

            /* Initialize the data binding service */
            LuaBindingServiceBundle bundle = new LuaBindingServiceBundle(context.GetContainer());
            bundle.Start();

            /* Initialize the ui view locator and register UIViewLocator */
            container.Register<IUIViewLocator>(new DefaultUIViewLocator());

            /* Initialize the localization service */
            //CultureInfo cultureInfo = Locale.GetCultureInfoByLanguage (SystemLanguage.English);
            CultureInfo cultureInfo = Locale.GetCultureInfo();
            var localization = Localization.Current;
            localization.CultureInfo = cultureInfo;
            localization.AddDataProvider(new DefaultDataProvider("LuaLocalizations", new XmlDocumentParser()));

            /* register Localization */
            container.Register<Localization>(Localization.Current);

            var luaEnv = LuaEnvironment.LuaEnv;
#if UNITY_EDITOR
            foreach (string dir in Directory.GetDirectories(Application.dataPath, "LuaScripts", SearchOption.AllDirectories))
            {
                luaEnv.AddLoader(new FileLoader(dir, ".lua"));
                luaEnv.AddLoader(new FileLoader(dir, ".lua.txt"));
            }
#else
            /* Pre-compiled and encrypted */
            //var decryptor = new RijndaelCryptograph(128,Encoding.ASCII.GetBytes("E4YZgiGQ0aqe5LEJ"), Encoding.ASCII.GetBytes("5Hh2390dQlVh0AqC"));
            //luaEnv.AddLoader( new DecodableLoader(new FileLoader(Application.streamingAssetsPath + "/LuaScripts/", ".bytes"), decryptor));
            //luaEnv.AddLoader( new DecodableLoader(new FileLoader(Application.persistentDataPath + "/LuaScripts/", ".bytes"), decryptor));

            /* Lua source code */
            luaEnv.AddLoader(new FileLoader(Application.streamingAssetsPath + "/LuaScripts/", ".bytes"));
            luaEnv.AddLoader(new FileLoader(Application.persistentDataPath + "/LuaScripts/", ".bytes"));
#endif



            InitLuaEnv();

            if (onAwake != null)
                onAwake(this);

        }

        void InitLuaEnv()
        {
            var luaEnv = LuaEnvironment.LuaEnv;
            scriptEnv = luaEnv.NewTable();

            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            scriptEnv.Set("target", this);

            string scriptText = (script.Type == ScriptReferenceType.TextAsset) ? script.Text.text : string.Format("require(\"framework.System\");local cls = require(\"{0}\");return extends(target,cls);", script.Filename);
            object[] result = luaEnv.DoString(scriptText, string.Format("{0}({1})", "Launcher", this.name), scriptEnv);

            if (result.Length != 1 || !(result[0] is LuaTable))
                throw new Exception();

            metatable = (LuaTable)result[0];

            onAwake = metatable.Get<Action<MonoBehaviour>>("awake");
            onEnable = metatable.Get<Action<MonoBehaviour>>("enable");
            onDisable = metatable.Get<Action<MonoBehaviour>>("disable");
            onStart = metatable.Get<Action<MonoBehaviour>>("start");
            onDestroy = metatable.Get<Action<MonoBehaviour>>("destroy");
        }

        void OnEnable()
        {
            if (onEnable != null)
                onEnable(this);
        }

        void OnDisable()
        {
            if (onDisable != null)
                onDisable(this);
        }

        void Start()
        {
            if (onStart != null)
                onStart(this);
        }

        void OnDestroy()
        {
            if (onDestroy != null)
                onDestroy(this);

            onDestroy = null;
            onStart = null;
            onEnable = null;
            onDisable = null;
            onAwake = null;

            if (metatable != null)
            {
                metatable.Dispose();
                metatable = null;
            }

            if (scriptEnv != null)
            {
                scriptEnv.Dispose();
                scriptEnv = null;
            }
        }
    }
}