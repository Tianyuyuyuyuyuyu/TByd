using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace TBydFramework.Runtime.Localizations
{
    public interface IDataProvider
    {
        Task<Dictionary<string, object>> Load(CultureInfo cultureInfo);
    }
}
