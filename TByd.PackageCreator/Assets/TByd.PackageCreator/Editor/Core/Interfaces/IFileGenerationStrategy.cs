using System.Threading.Tasks;

namespace TByd.PackageCreator.Editor.Core
{
    /// <summary>
    /// 文件生成策略接口，用于支持不同的文件生成机制
    /// </summary>
    public interface IFileGenerationStrategy
    {
        /// <summary>
        /// 生成策略的名称
        /// </summary>
        string StrategyName { get; }

        /// <summary>
        /// 策略支持的文件类型（扩展名，例如：".cs", ".json"等）
        /// </summary>
        string[] SupportedFileExtensions { get; }

        /// <summary>
        /// 生成文件内容
        /// </summary>
        /// <param name="templateFile">模板文件定义</param>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns>生成结果</returns>
        Task<ValidationResult> GenerateFileAsync(TemplateFile templateFile, PackageConfig config, string targetPath);

        /// <summary>
        /// 检查此策略是否支持指定的文件类型
        /// </summary>
        /// <param name="fileExtension">文件扩展名</param>
        /// <returns>是否支持</returns>
        bool SupportsFileType(string fileExtension);
    }
}
