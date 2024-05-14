using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using TBydFramework.Runtime.Log;
using UnityEngine;

namespace TBydFramework.Runtime.Localizations.Unity
{
    [AddComponentMenu("X/Localization/LocalizationSource")]
    [DefaultExecutionOrder(-100)]
    public class LocalizationSourceBehaviour : MonoBehaviour
    {
        [SerializeField]
        public MultilingualSource Source = new MultilingualSource();

        [NonSerialized]
        protected MultilingualSourceDataProvider provider;

        protected virtual async void OnEnable()
        {
            if (provider == null)
                provider = new MultilingualSourceDataProvider(this.name, this.Source);

            await Localization.Current.AddDataProvider(provider);
        }

        protected virtual void OnDisable()
        {
            if (provider != null)
                Localization.Current.RemoveDataProvider(provider);
        }

        protected class MultilingualSourceDataProvider : IDataProvider
        {
            private static readonly ILog log = LogManager.GetLogger(typeof(MultilingualSourceDataProvider));

            protected string name;
            protected MultilingualSource source;

            public MultilingualSourceDataProvider(MultilingualSource source) : this("", source)
            {
            }

            public MultilingualSourceDataProvider(string name, MultilingualSource source)
            {
                if (source == null)
                    throw new ArgumentNullException("source");

                this.name = name;
                this.source = source;
            }

            public virtual Task<Dictionary<string, object>> Load(CultureInfo cultureInfo)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                try
                {
                    if (source.Languages == null || source.Entries == null || source.Languages.Count <= 0 || source.Entries.Count <= 0)
                        return Task.FromResult(dict);

                    List<string> languages = source.Languages;
                    List<MultilingualEntry> entries = source.Entries;

                    string defaultName = "default";
                    string cultureISOName = cultureInfo.TwoLetterISOLanguageName;//eg:zh  en
                    string cultureName = cultureInfo.Name;//eg:zh-CN  en-US 

                    /* If the default column is not configured, the first data column is used as the default column */
                    if (!languages.Contains(defaultName))
                        defaultName = languages[0];

                    int defaultIndex = languages.IndexOf(defaultName);
                    if (defaultIndex >= 0)
                        FillData(dict, entries, defaultIndex);

                    int cultureISOIndex = languages.IndexOf(cultureISOName);
                    if (cultureISOIndex >= 0 && cultureISOIndex != defaultIndex)
                        FillData(dict, entries, cultureISOIndex);

                    int cultureIndex = languages.IndexOf(cultureName);
                    if (cultureIndex >= 0 && cultureIndex != defaultIndex && cultureIndex != cultureISOIndex)
                        FillData(dict, entries, cultureIndex);

                    return Task.FromResult(dict);
                }
                catch (Exception e)
                {
                    if (log.IsWarnEnabled)
                        log.WarnFormat("An error occurred when loading localized data from LocalizationSource \"{0}\".Error:{1}", this.name, e);
                    return Task.FromException<Dictionary<string, object>>(e);
                }
            }

            private void FillData(Dictionary<string, object> dict, List<MultilingualEntry> entries, int index)
            {
                try
                {
                    foreach (var entry in entries)
                    {
                        string key = entry.Key;
                        object value = entry.GetValue(index);
                        if (string.IsNullOrEmpty(key) || value == null)
                            continue;

                        dict[key] = value;
                    }
                }
                catch (Exception e)
                {
                    if (log.IsWarnEnabled)
                        log.WarnFormat("An error occurred when loading localized data from LocalizationSource \"{0}\".Error:{1}", this.name, e);
                }
            }
        }
    }
}
