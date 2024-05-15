using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using TBydFramework.Runtime.Asynchronous;
using TBydFramework.Runtime.Localizations;
using TBydFramework.Runtime.Log;
using UnityEngine;
using UnityEngine.Networking;

namespace TBydFramework.LocalizationsForCsv.Runtime.Localizations.Csv
{
    public class AssetBundleCsvDataProvider : IDataProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DefaultCsvDataProvider));

        private string assetBundleUrl;
        private IDocumentParser parser;

        public AssetBundleCsvDataProvider(string assetBundleUrl) : this(assetBundleUrl, new CsvDocumentParser())
        {
        }

        public AssetBundleCsvDataProvider(string assetBundleUrl, IDocumentParser parser)
        {
            if (string.IsNullOrEmpty(assetBundleUrl))
                throw new ArgumentNullException("assetBundleUrl");
            this.parser = parser ?? throw new ArgumentNullException("parser");
            this.assetBundleUrl = assetBundleUrl;
        }

        public async Task<Dictionary<string, object>> Load(CultureInfo cultureInfo)
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
                    TextAsset[] texts = bundle.LoadAllAssets<TextAsset>();
                    FillData(dict, texts, cultureInfo);
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
            }
            return dict;
        }

        private void FillData(Dictionary<string, object> dict, TextAsset[] texts, CultureInfo cultureInfo)
        {
            try
            {
                if (texts == null || texts.Length <= 0)
                    return;

                foreach (TextAsset text in texts)
                {
                    try
                    {
                        using (MemoryStream stream = new MemoryStream(text.bytes))
                        {
                            var data = parser.Parse(stream, cultureInfo);
                            foreach (KeyValuePair<string, object> kv in data)
                            {
                                dict[kv.Key] = kv.Value;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (log.IsWarnEnabled)
                            log.WarnFormat("An error occurred when loading localized data from \"{0}\".Error:{1}", text.name, e);
                    }
                }
            }
            catch (Exception) { }
        }
    }
}
