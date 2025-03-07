using TByd.PackageCreator.Editor.Core.Models;

namespace TByd.PackageCreator.Editor.Core.Interfaces
{
    /// <summary>
    /// 验证规则接口，用于实现可扩展的验证规则系统
    /// </summary>
    public interface IValidationRule
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        string RuleName { get; }

        /// <summary>
        /// 规则描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 规则优先级（数字越小优先级越高）
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// 验证包配置
        /// </summary>
        /// <param name="config">包配置</param>
        /// <param name="template">可选的模板参数，如果指定则根据模板进行验证</param>
        /// <returns>验证结果</returns>
        ValidationResult Validate(PackageConfig config, IPackageTemplate template = null);
    }
}
