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
    /// It supports localized resources in csv format.
    /// </summary>
    public class CsvAddressableDataProvider : IDataProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CsvAddressableDataProvider));

        private IList<object> keys;
        private IDocumentParser parser;

        /// <summary>
        /// Load localized resources based on asset's label or address.
        /// </summary>
        /// <param name="key">The label of asset in the AssetBundle.</param>
        /// <param name="parser">CSV document parser</param>
        public CsvAddressableDataProvider(object key, IDocumentParser parser) : this(new List<object>() { key }, parser)
        {
        }

        /// <summary>
        /// Load localized resources based on asset's label or address.
        /// </summary>
        /// <param name="keys">The label of asset in the AssetBundle.</param>
        /// <param name="parser">CSV document parser</param>
        public CsvAddressableDataProvider(IList<object> keys, IDocumentParser parser)
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
                List<IResourceLocation> list = locations.Where(l => l.InternalId.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)).ToList();

                await FillData(dict, list, cultureInfo);
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

