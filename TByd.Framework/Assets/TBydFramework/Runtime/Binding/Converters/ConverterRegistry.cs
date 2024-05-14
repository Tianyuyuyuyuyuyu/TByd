using TBydFramework.Runtime.Binding.Registry;

namespace TBydFramework.Runtime.Binding.Converters
{
    public class ConverterRegistry: KeyValueRegistry<string,IConverter>, IConverterRegistry
    {
        public ConverterRegistry()
        {
            this.Init();
        }

        protected virtual void Init()
        {
        }
    }
}
