namespace TByd.PackageCreator.Editor.UI.ViewModels
{
    /// <summary>
    /// 视图模型接口，所有视图模型必须实现此接口
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// 初始化视图模型
        /// </summary>
        void Initialize();

        /// <summary>
        /// 清理视图模型资源
        /// </summary>
        void Cleanup();
    }
}
