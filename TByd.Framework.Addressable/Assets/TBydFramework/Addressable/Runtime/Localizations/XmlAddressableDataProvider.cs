using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using TBydFramework.Addressable.Runtime.Asynchronous;
using TBydFramework.Runtime.Localizations;
using TBydFramework.Runtime.Log;
using static UnityEngine.AddressableAssets.Addressables;

namespace TBydFramework.Addressable.Runtime.Localizations
{
    /// <summary>
    /// Addressable data provider.
    /// It supports localized resources in xml format.
    /// dir:
    /// root/default/
    /// 
    /// root/zh/
    /// root/zh-CN/
    /// root/zh-TW/
    /// root/zh-HK/
    /// 
    /// root/en/
    /// root/en-US/
    /// root/en-CA/
    /// root/en-AU/
    /// </summary>
    public class XmlAddressableDataProvider : IDataProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(XmlAddressableDataProvider));

        private IList<object> keys;
        private IDocumentParser parser;

        /// <summary>
        /// Load localized resources based on asset's label or address.
        /// </summary>
        /// <param name="key">The label of asset in the AssetBundle.</param>
        public XmlAddressableDataProvider(object key) : this(new List<object>() { key }, new XmlDocumentParser())
        {
        }

        /// <summary>
        /// Load localized resources based on asset's label or address.
        /// </summary>
        /// <param name="key">The label of asset in the AssetBundle.</param>
        /// <param name="parser">XML document parser</param>
        public XmlAddressableDataProvider(object key, IDocumentParser parser) : this(new List<object>() { key }, parser)
        {
        }

        /// <summary>
        /// Load localized resources based on asset's label or address.
        /// </summary>
        /// <param name="keys">The label of asset in the AssetBundle.</param>
        public XmlAddressableDataProvider(IList<object> keys) : this(keys, new XmlDocumentParser())
        {
        }

        /// <summary>
        /// Load localized resources based on asset's label or address.
        /// </summary>
        /// <param name="keys">The label of asset in the AssetBundle.</param>
        /// <param name="parser">XML document parser</param>
        public XmlAddressableDataProvider(IList<object> keys, IDocumentParser parser)
        {
            this.keys = keys ?? throw new ArgumentNullException("keys");
            this.parser = parser ?? throw new ArgumentNullException("parser");
        }

        public virtual async Task<Dictionary<string, object>> Load(CultureInfo cultureInfo)
        {
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                var locations = await Addressables.LoadResourceLocationsAsync(this.keys, MergeMode.Union, typeof(TextAsset));
                List<IResourceLocation> list = locations.Where(l => l.InternalId.EndsWith(".xml", StringComparison.OrdinalIgnoreCase)).ToList();
                List<IResourceLocation> defaultPaths = list.FindAll(l => l.InternalId.Contains("/default/"));//eg:default
                List<IResourceLocation> twoLetterISOpaths = list.FindAll(l => l.InternalId.Contains(string.Format("/{0}/", cultureInfo.TwoLetterISOLanguageName)));//eg:zh  en
                List<IResourceLocation> paths = cultureInfo.Name.Equals(cultureInfo.TwoLetterISOLanguageName) ? null : list.FindAll(l => l.InternalId.Contains(string.Format("/{0}/", cultureInfo.Name)));//eg:zh-CN  en-US

                await FillData(dict, defaultPaths, cultureInfo);
                await FillData(dict, twoLetterISOpaths, cultureInfo);
                await FillData(dict, paths, cultureInfo);
                return dict;
            }
            catch (Exception e)
            {
                if (log.IsWarnEnabled)
                    log.WarnFormat("An error occurred when loading localized data.Error:{0}", e);
                throw;
            }
        }

        protected virtual async Task FillData(Dictionary<string, object> dict, IList<IResourceLocation> paths, CultureInfo cultureInfo)
        {
            try
            {
                if (paths == null || paths.Count <= 0)
                    return;

                var result = Addressables.LoadAssetsAsync<TextAsset>(paths, null);
                IList<TextAsset> texts = await result;
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
                Addressables.Release(result);
            }
            catch (Exception) { }
        }
    }
}

