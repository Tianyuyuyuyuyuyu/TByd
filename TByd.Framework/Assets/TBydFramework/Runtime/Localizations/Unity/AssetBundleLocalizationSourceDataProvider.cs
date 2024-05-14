using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TBydFramework.Runtime.Asynchronous;
using TBydFramework.Runtime.Log;
using UnityEngine;
using UnityEngine.Networking;

namespace TBydFramework.Runtime.Localizations.Unity
{
    public class AssetBundleLocalizationSourceDataProvider : IDataProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AssetBundleLocalizationSourceDataProvider));

        protected string[] filenames;
        protected string assetBundleUrl;

        public AssetBundleLocalizationSourceDataProvider(string assetBundleUrl, params string[] filenames)
        {
            if (string.IsNullOrEmpty(assetBundleUrl))
                throw new ArgumentNullException("assetBundleUrl");

            this.assetBundleUrl = assetBundleUrl;
            this.filenames = filenames;
        }

        public virtual async Task<Dictionary<string, object>> Load(CultureInfo cultureInfo)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(this.assetBundleUrl))
            {
                await www.SendWebRequest();

                DownloadHandlerAssetBundle handler = (DownloadHandlerAssetBundle)www.downloadHandler;
                AssetBundle bundle = handler.assetBundle;
                if (bundle == null)
                {
                    if (log.IsWarnEnabled)
                        log.WarnFormat("Failed to load Assetbundle from \"{0}\".", this.assetBundleUrl);
                    return dict;
                }
                try
                {
                    List<string> assetNames = new List<string>(bundle.GetAllAssetNames());
                    foreach (string filename in filenames)
                    {
                        try
                        {
                            string defaultPath = assetNames.Find(p => p.Contains(string.Format("/default/{0}", filename)));//eg:default
                            string twoLetterISOpath = assetNames.Find(p => p.Contains(string.Format("/{0}/{1}", cultureInfo.TwoLetterISOLanguageName, filename)));//eg:zh  en
                            string path = cultureInfo.Name.Equals(cultureInfo.TwoLetterISOLanguageName) ? null : assetNames.Find(p => p.Contains(string.Format("/{0}/{1}", cultureInfo.Name, filename)));//eg:zh-CN  en-US

                            FillData(dict, bundle, defaultPath);
                            FillData(dict, bundle, twoLetterISOpath);
                            FillData(dict, bundle, path);
                        }
                        catch (Exception e)
                        {
                            if (log.IsWarnEnabled)
                                log.WarnFormat("An error occurred when loading localized data from \"{0}\".Error:{1}", filename, e);
                        }
                    }
                }
                finally
                {
                    try
                    {
                        if (bundle != null)
                            bundle.Unload(true);
                    }
                    catch (Exception) { }
                }
                return dict;
            }
        }

        private void FillData(Dictionary<string, object> dict, AssetBundle bundle, string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            LocalizationSourceAsset sourceAsset = bundle.LoadAsset<LocalizationSourceAsset>(path);
            if (sourceAsset == null)
                return;

            MonolingualSource source = sourceAsset.Source;
            if (source == null)
                return;

            foreach (KeyValuePair<string, object> kv in source.GetData())
            {
                dict[kv.Key] = kv.Value;
            }
        }
    }
}
