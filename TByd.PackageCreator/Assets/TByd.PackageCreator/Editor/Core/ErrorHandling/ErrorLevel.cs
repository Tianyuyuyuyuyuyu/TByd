namespace TByd.PackageCreator.Editor.Core.ErrorHandling
{
    /// <summary>
    /// 错误级别枚举，用于定义错误的严重程度
    /// </summary>
    public enum ErrorLevel
    {
        /// <summary>
        /// 信息级别，仅提供参考信息，不影响操作
        /// </summary>
        Info = 0,

        /// <summary>
        /// 警告级别，可能会影响结果，但不阻止操作继续
        /// </summary>
        Warning = 1,

        /// <summary>
        /// 错误级别，表示操作遇到问题，但系统仍能继续运行
        /// </summary>
        Error = 2,

        /// <summary>
        /// 严重错误级别，表示发生了致命错误，系统无法继续运行
        /// </summary>
        Critical = 3
    }
}
