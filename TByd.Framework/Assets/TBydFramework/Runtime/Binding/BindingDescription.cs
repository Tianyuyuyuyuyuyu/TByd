using System;
using System.Text;
using TBydFramework.Runtime.Binding.Converters;
using TBydFramework.Runtime.Binding.Paths;
using TBydFramework.Runtime.Binding.Proxy.Sources;
using TBydFramework.Runtime.Binding.Proxy.Sources.Object;

namespace TBydFramework.Runtime.Binding
{
    [Serializable]
    public class BindingDescription
    {
        public string TargetName { get; set; }

        public Type TargetType { get; set; }

        public string UpdateTrigger { get; set; }

        public IConverter Converter { get; set; }

        public BindingMode Mode { get; set; }

        public SourceDescription Source { get; set; }

        public object CommandParameter { get; set; }

        public BindingDescription()
        {
        }

        public BindingDescription(string targetName, Path path, IConverter converter = null, BindingMode mode = BindingMode.Default)
        {
            this.TargetName = targetName;
            this.Mode = mode;
            this.Converter = converter;
            this.Source = new ObjectSourceDescription
            {
                Path = path
            };
        }

        public override string ToString()
        {
            StringBuilder buf = new StringBuilder();
            buf.Append("{binding ").Append(this.TargetName);

            if (!string.IsNullOrEmpty(this.UpdateTrigger))
                buf.Append(" UpdateTrigger:").Append(this.UpdateTrigger);

            if (this.Converter != null)
                buf.Append(" Converter:").Append(this.Converter.GetType().Name);

            if (this.Source != null)
                buf.Append(" ").Append(this.Source.ToString());

            if (this.CommandParameter != null)
                buf.Append(" CommandParameter:").Append(this.CommandParameter);

            buf.Append(" Mode:").Append(this.Mode.ToString());
            buf.Append(" }");
            return buf.ToString();
        }
    }
}