using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TByd.PackageCreator.Editor.Core
{
    /// <summary>
    /// 包模板抽象基类，实现了IPackageTemplate接口的基本功能
    /// </summary>
    public abstract class AbstractPackageTemplate : IPackageTemplate
    {
        /// <summary>
        /// 模板唯一标识符
        /// </summary>
        public abstract string Id { get; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 模板描述
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// 模板版本
        /// </summary>
        public abstract string Version { get; }

        /// <summary>
        /// 模板作者
        /// </summary>
        public abstract string Author { get; }

        /// <summary>
        /// 模板图标
        /// </summary>
        public abstract Texture2D Icon { get; }

        /// <summary>
        /// 模板目录结构
        /// </summary>
        public abstract IReadOnlyList<TemplateDirectory> Directories { get; }

        /// <summary>
        /// 模板文件
        /// </summary>
        public abstract IReadOnlyList<TemplateFile> Files { get; }

        /// <summary>
        /// 模板选项
        /// </summary>
        public abstract IReadOnlyList<TemplateOption> Options { get; }

        /// <summary>
        /// 验证包配置是否符合模板要求
        /// </summary>
        /// <param name="config">包配置</param>
        /// <returns>验证结果</returns>
        public virtual ValidationResult ValidateConfig(PackageConfig config)
        {
            var result = new ValidationResult();

            // 基本验证
            if (config == null)
            {
                result.AddError("包配置不能为空");
                return result;
            }

            // 验证必填字段
            if (string.IsNullOrEmpty(config.Name))
            {
                result.AddError("包名称不能为空", "Name");
            }
            else if (!ValidatePackageName(config.Name))
            {
                result.AddError("包名称格式无效，应使用反向域名格式，例如：com.company.package", "Name");
            }

            if (string.IsNullOrEmpty(config.DisplayName))
            {
                result.AddError("包显示名称不能为空", "DisplayName");
            }

            if (string.IsNullOrEmpty(config.Version))
            {
                result.AddError("包版本不能为空", "Version");
            }
            else if (!ValidatePackageVersion(config.Version))
            {
                result.AddError("包版本格式无效，应使用语义化版本格式，例如：1.0.0", "Version");
            }

            if (string.IsNullOrEmpty(config.Description))
            {
                result.AddWarning("包描述为空，建议添加描述", "Description");
            }

            // 验证依赖项
            if (config.Dependencies != null)
            {
                foreach (var dependency in config.Dependencies)
                {
                    if (string.IsNullOrEmpty(dependency.Id))
                    {
                        result.AddError("依赖项包ID不能为空");
                    }
                    else if (!ValidatePackageName(dependency.Id))
                    {
                        result.AddError($"依赖项包ID格式无效：{dependency.Id}");
                    }

                    if (string.IsNullOrEmpty(dependency.Version))
                    {
                        result.AddWarning($"依赖项{dependency.Id}的版本为空，将使用最新版本");
                    }
                    else if (!ValidatePackageVersion(dependency.Version))
                    {
                        result.AddWarning($"依赖项{dependency.Id}的版本格式无效：{dependency.Version}");
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 根据配置生成包结构（同步方法）
        /// </summary>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标路径</param>
        /// <returns>操作是否成功</returns>
        public virtual bool Generate(PackageConfig config, string targetPath)
        {
            // 创建文件生成器
            var fileGenerator = new FileGenerator();

            // 注册标准策略
            fileGenerator.RegisterStrategy(new JsonFileGenerationStrategy());
            fileGenerator.RegisterStrategy(new CSharpFileGenerationStrategy());

            // 执行异步生成并等待结果
            var task = GenerateAsync(config, targetPath, fileGenerator);
            task.Wait();

            // 返回生成结果
            return task.Result.IsValid;
        }

        /// <summary>
        /// 根据配置异步生成包结构
        /// </summary>
        /// <param name="config">包配置</param>
        /// <param name="targetPath">目标路径</param>
        /// <param name="fileGenerator">文件生成器</param>
        /// <returns>生成结果</returns>
        public virtual async Task<ValidationResult> GenerateAsync(PackageConfig config, string targetPath, FileGenerator fileGenerator = null)
        {
            // 创建默认文件生成器（如果未提供）
            if (fileGenerator == null)
            {
                fileGenerator = new FileGenerator();
                fileGenerator.RegisterStrategy(new JsonFileGenerationStrategy());
                fileGenerator.RegisterStrategy(new CSharpFileGenerationStrategy());
            }

            // 验证配置
            var validationResult = ValidateConfig(config);
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            // 使用文件生成器生成目录和文件
            return await fileGenerator.GenerateAsync(this, config, targetPath);
        }

        /// <summary>
        /// 获取模板预览信息
        /// </summary>
        /// <returns>预览信息</returns>
        public virtual TemplatePreviewInfo GetPreviewInfo()
        {
            var previewInfo = new TemplatePreviewInfo(Name, Description);

            // 添加基本信息
            previewInfo.AddFeature($"版本: {Version}");
            previewInfo.AddFeature($"作者: {Author}");

            // 添加目录和文件统计
            previewInfo.AddFeature($"目录数: {CountTotalDirectories(Directories)}");
            previewInfo.AddFeature($"文件数: {Files.Count}");

            return previewInfo;
        }

        /// <summary>
        /// 验证包名称是否合法
        /// </summary>
        /// <param name="name">包名称</param>
        /// <returns>是否合法</returns>
        protected virtual bool ValidatePackageName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            // 包名应该是反向域名格式，例如：com.company.package
            string[] parts = name.Split('.');
            return parts.Length >= 2;
        }

        /// <summary>
        /// 验证包版本是否为有效的语义化版本
        /// </summary>
        /// <param name="version">版本字符串</param>
        /// <returns>是否为有效的语义化版本</returns>
        protected virtual bool ValidatePackageVersion(string version)
        {
            if (string.IsNullOrEmpty(version))
                return false;

            // 简单的语义化版本验证，如：1.0.0, 1.0.0-preview.1
            string[] parts = version.Split('-');
            string[] versionParts = parts[0].Split('.');

            // 至少需要有主版本号和次版本号
            return versionParts.Length >= 2;
        }

        /// <summary>
        /// 计算总目录数（包括子目录）
        /// </summary>
        /// <param name="directories">目录列表</param>
        /// <returns>目录总数</returns>
        protected int CountTotalDirectories(IReadOnlyList<TemplateDirectory> directories)
        {
            int count = 0;

            foreach (var directory in directories)
            {
                count++; // 计数当前目录

                // 递归计数子目录
                if (directory.Subdirectories != null && directory.Subdirectories.Count > 0)
                {
                    count += CountTotalDirectories(directory.Subdirectories);
                }
            }

            return count;
        }
    }
}
