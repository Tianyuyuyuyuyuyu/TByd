using TBydFramework.Runtime.Binding.Registry;

namespace TBydFramework.Runtime.Binding.Converters
{
    public interface IConverterRegistry : IKeyValueRegistry<string, IConverter>
    {
    }
}
