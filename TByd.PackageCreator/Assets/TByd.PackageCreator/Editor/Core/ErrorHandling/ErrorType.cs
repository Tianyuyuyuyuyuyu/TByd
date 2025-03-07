namespace TByd.PackageCreator.Editor.Core.ErrorHandling
{
    /// <summary>
    /// 错误类型枚举，用于分类不同来源和性质的错误
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// 无错误，通常用于信息级别的日志
        /// </summary>
        k_None = -1,

        /// <summary>
        /// 未知错误类型
        /// </summary>
        k_Unknown = 0,

        /// <summary>
        /// 验证错误，如配置验证失败
        /// </summary>
        k_Validation = 1,

        /// <summary>
        /// 文件错误，如文件IO操作失败
        /// </summary>
        k_FileOperation = 2,

        /// <summary>
        /// 模板错误，如模板解析或应用失败
        /// </summary>
        k_Template = 3,

        /// <summary>
        /// 配置错误，如包配置问题
        /// </summary>
        k_Configuration = 4,

        /// <summary>
        /// Unity编辑器相关错误
        /// </summary>
        k_Editor = 5,

        /// <summary>
        /// 权限错误，如没有足够权限执行操作
        /// </summary>
        k_Permission = 6,

        /// <summary>
        /// 网络错误，如下载资源失败
        /// </summary>
        k_Network = 7,

        /// <summary>
        /// 用户操作错误，如用户取消操作
        /// </summary>
        k_UserOperation = 8,

        /// <summary>
        /// 依赖错误，如包依赖解析失败
        /// </summary>
        k_Dependency = 9,

        /// <summary>
        /// 安全性错误，如违反安全策略
        /// </summary>
        k_Security = 10,

        /// <summary>
        /// 系统错误，如内存不足
        /// </summary>
        k_System = 11,

        /// <summary>
        /// 无效参数错误
        /// </summary>
        k_InvalidArgument = 12,

        /// <summary>
        /// 操作失败错误
        /// </summary>
        k_OperationFailed = 13,

        /// <summary>
        /// 无效数据错误
        /// </summary>
        k_InvalidData = 14,

        /// <summary>
        /// 文件未找到错误
        /// </summary>
        k_FileNotFound = 15,

        /// <summary>
        /// 文件读取错误
        /// </summary>
        k_FileReadError = 16,

        /// <summary>
        /// 资源未找到错误
        /// </summary>
        k_ResourceNotFound = 17,

        /// <summary>
        /// 重复资源错误
        /// </summary>
        k_DuplicateResource = 18,

        /// <summary>
        /// 反序列化错误
        /// </summary>
        k_DeserializationError = 19,

        /// <summary>
        /// 序列化错误
        /// </summary>
        k_SerializationError = 20,

        /// <summary>
        /// 文件写入错误
        /// </summary>
        k_FileWriteError = 21,

        /// <summary>
        /// 文件复制错误
        /// </summary>
        k_FileCopyError = 22,

        /// <summary>
        /// 文件删除错误
        /// </summary>
        k_FileDeleteError = 23
    }
}
