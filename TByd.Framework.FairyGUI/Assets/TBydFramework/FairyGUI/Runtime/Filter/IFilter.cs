using TBydFramework.FairyGUI.Runtime.Core;

namespace TBydFramework.FairyGUI.Runtime.Filter
{
    public interface IFilter
    {
        /// <summary>
        /// 
        /// </summary>
        DisplayObject target { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void Update();

        /// <summary>
        /// 
        /// </summary>
        void Dispose();
    }
}
