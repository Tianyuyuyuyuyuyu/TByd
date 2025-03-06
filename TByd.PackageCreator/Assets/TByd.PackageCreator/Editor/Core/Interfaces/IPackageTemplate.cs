using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core
{
    /// <summary>
    /// 包模板接口，定义了包模板的基本属性和行为
    /// </summary>
    public interface IPackageTemplate
    {
        /// <summary>
        /// 模板唯一标识符
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 模板名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 模板描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 模板版本
        /// </summary>
        string Version { get; }

        /// <summary>
        /// 模板作者
        /// </summary>
        string Author { get; }

        /// <summary>
        /// 模板图标
        /// </summary>
        Texture2D Icon { get; }

        /// <summary>
        /// 模板目录结构
        /// </summary>
        IReadOnlyList<TemplateDirectory> Directories { get; }

        /// <summary>
        /// 模板文件
        /// </summary>
        IReadOnlyList<TemplateFile> Files { get; }

        /// <summary>
        /// 模板选项
        /// </summary>
        IReadOnlyList<TemplateOption> Options { get; }

        /// <summary>
        /// 验证包配置是否符合模板要求
        /// </summary>
        /// <param name="config">包配置</param>
        /// <returns>验证结果</returns>
        ValidationResult ValidateConfig(PackageConfig config);

        /// <summary>
        /// 根据配置生成包结构（同步方法，为兼容保留）
        /// </summary>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>操作是否成功</returns>
        bool Generate(PackageConfig config, string targetPath);

        /// <summary>
        /// 根据配置异步生成包结构
        /// </summary>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="fileGenerator">文件生成器</param>
        /// <returns>生成结果</returns>
        Task<ValidationResult> GenerateAsync(PackageConfig config, string targetPath, FileGenerator fileGenerator = null);

        /// <summary>
        /// 获取模板预览信息
        /// </summary>
        /// <returns>预览信息</returns>
        TemplatePreviewInfo GetPreviewInfo();
    }
}
