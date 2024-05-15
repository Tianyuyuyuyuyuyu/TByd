using System.Text;

namespace TBydFramework.TextMeshPro.Runtime.Views.TextMeshPro
{
    public interface IFormattableText
    {
        internal static StringBuilder BUFFER = new StringBuilder();
        string Format { get; set; }

        int ParameterCount { get; set; }

        Parameters Parameters { get; set; }
    }
}
