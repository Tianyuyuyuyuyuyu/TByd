namespace TByd.PackageCreator.Editor.Core.ErrorHandling
{
    /// <summary>
    /// 操作类型枚举，用于定义可以回滚的操作类型
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 创建文件操作
        /// </summary>
        k_CreateFile = 0,

        /// <summary>
        /// 修改文件操作
        /// </summary>
        k_ModifyFile = 1,

        /// <summary>
        /// 删除文件操作
        /// </summary>
        k_DeleteFile = 2,

        /// <summary>
        /// 创建目录操作
        /// </summary>
        k_CreateDirectory = 3,

        /// <summary>
        /// 删除目录操作
        /// </summary>
        k_DeleteDirectory = 4,

        /// <summary>
        /// 重命名文件或目录操作
        /// </summary>
        k_Rename = 5,

        /// <summary>
        /// 移动文件或目录操作
        /// </summary>
        k_Move = 6,

        /// <summary>
        /// 复制文件或目录操作
        /// </summary>
        k_Copy = 7,

        /// <summary>
        /// 导入资源操作
        /// </summary>
        k_ImportAsset = 8,

        /// <summary>
        /// 修改资源设置操作
        /// </summary>
        k_ModifyAssetSettings = 9,

        /// <summary>
        /// 自定义操作（需要提供自定义回滚逻辑）
        /// </summary>
        k_Custom = 100
    }
}
