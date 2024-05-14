using System;

namespace TBydFramework.Runtime.Configuration
{
    public interface ITypeConverter
    {
        bool Support(Type type);

        object Convert(Type type, object value);
    }
}
