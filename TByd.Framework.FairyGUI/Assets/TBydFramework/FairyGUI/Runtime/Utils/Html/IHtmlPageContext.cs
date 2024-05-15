
using TBydFramework.FairyGUI.Runtime.Core;
using TBydFramework.FairyGUI.Runtime.Core.Text;

namespace TBydFramework.FairyGUI.Runtime.Utils.Html
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHtmlPageContext
    {
        IHtmlObject CreateObject(RichTextField owner, HtmlElement element);
        void FreeObject(IHtmlObject obj);

        NTexture GetImageTexture(HtmlImage image);
        void FreeImageTexture(HtmlImage image, NTexture texture);
    }
}
