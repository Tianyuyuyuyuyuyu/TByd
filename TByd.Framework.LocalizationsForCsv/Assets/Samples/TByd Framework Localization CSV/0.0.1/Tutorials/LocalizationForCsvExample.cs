using System.Globalization;
using TBydFramework.LocalizationsForCsv.Runtime.Localizations.Csv;
using UnityEngine;
using TBydFramework.Runtime.Localizations;

namespace TBydFramework.Samples
{
    public class LocalizationForCsvExample : MonoBehaviour
    {
        void Start()
        {
            CultureInfo cultureInfo = Locale.GetCultureInfoByLanguage(SystemLanguage.Chinese);

            var localization = Localization.Current;
            localization.CultureInfo = cultureInfo;
            localization.AddDataProvider(new DefaultCsvDataProvider("LocalizationCsv", new CsvDocumentParser()));

            Debug.LogFormat("{0}", localization.GetText("app.name"));
            Debug.LogFormat("{0}", localization.GetText("databinding.tutorials.title"));
        }
    }
}
