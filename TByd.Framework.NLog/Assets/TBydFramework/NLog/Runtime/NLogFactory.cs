using System;
using System.IO;
using System.Xml;
using NLog;
using NLog.Config;
using TBydFramework.NLog.Runtime.Directories;
using TBydFramework.NLog.Runtime.Targets;
using TBydFramework.Runtime.Log;
using UnityEngine;
using UnityEngine.Networking;

namespace TBydFramework.NLog.Runtime
{
    public class NLogFactory : ILogFactory
    {
        static NLogFactory()
        {
            ConfigurationItemFactory configurationFactory = ConfigurationItemFactory.Default;
            configurationFactory.LayoutRendererFactory.RegisterType<PersistentDataPathLayoutRenderer>("persistent-data-path");
            configurationFactory.LayoutRendererFactory.RegisterType<TemporaryCachePathLayoutRenderer>("temporary-cache-path");
            configurationFactory.TargetFactory.RegisterType<UnityConsoleTarget>("UnityConsole");
        }

        public static NLogFactory Load(XmlReader reader)
        {
            try
            {
                XmlLoggingConfiguration configuration = new XmlLoggingConfiguration(reader);
                global::NLog.LogManager.Configuration = configuration;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogErrorFormat("Failed to load NLog configuration file, default configuration will be used.exception:{0}", e);
                InitializeDefaultConfiguration();
            }
            return new NLogFactory(global::NLog.LogManager.LogFactory);
        }

        public static NLogFactory Load(string filename)
        {
            try
            {
                using (UnityWebRequest www = UnityWebRequest.Get(filename))
                {
                    www.SendWebRequest();
                    while (!www.isDone) { }
                    if (www.isNetworkError || www.isHttpError)
                        throw new Exception(www.error);

                    string text = www.downloadHandler.text;
                    using (XmlReader reader = XmlReader.Create(new StringReader(text)))
                    {
                        XmlLoggingConfiguration configuration = new XmlLoggingConfiguration(reader);
                        global::NLog.LogManager.Configuration = configuration;
                    }
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogErrorFormat("Failed to load NLog configuration file from \"{0}\", default configuration will be used.exception:{1}", filename, e);
                InitializeDefaultConfiguration();
            }

            return new NLogFactory(global::NLog.LogManager.LogFactory);
        }

        public static NLogFactory LoadInResources(string filename)
        {
            try
            {
                string path = filename;
                TextAsset configText = Resources.Load<TextAsset>(path);
                if (configText == null)
                {
                    string extension = Path.GetExtension(path);
                    if (!string.IsNullOrEmpty(extension))
                        path = path.Replace(extension, "");
                    configText = Resources.Load<TextAsset>(path);
                }

                using (XmlReader reader = XmlReader.Create(new StringReader(configText.text)))
                {
                    return Load(reader);
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogErrorFormat("Failed to load NLog configuration file from \"{0}\", default configuration will be used.exception:{1}", filename, e);
                InitializeDefaultConfiguration();
                return new NLogFactory(global::NLog.LogManager.LogFactory);
            }
        }

        private static void InitializeDefaultConfiguration()
        {
            LoggingConfiguration configuration = new LoggingConfiguration();
            var consoleTarget = new UnityConsoleTarget();
            consoleTarget.Layout = "${longdate} [${uppercase:${level}}] ${callsite}(${callsite-filename:includeSourcePath=False}:${callsite-linenumber}) - ${message} ${exception:format=ToString}";
            configuration.AddTarget("logconsole", consoleTarget);

            var rule = new LoggingRule("*", LogLevel.Info, consoleTarget);
            configuration.LoggingRules.Add(rule);
            global::NLog.LogManager.Configuration = configuration;
        }

        public static void Shutdown()
        {
            global::NLog.LogManager.Shutdown();
        }

        private readonly LogFactory logFactory;
        public NLogFactory(LogFactory logFactory)
        {
            this.logFactory = logFactory;
        }

        public ILog GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }

        public ILog GetLogger(Type type)
        {
            return logFactory.GetLogger<NLogLogImpl>(type.FullName);
        }

        public ILog GetLogger(string name)
        {
            return logFactory.GetLogger<NLogLogImpl>(name);
        }
    }
}
