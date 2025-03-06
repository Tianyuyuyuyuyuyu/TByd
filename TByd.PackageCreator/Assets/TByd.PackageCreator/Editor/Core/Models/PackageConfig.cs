using System;
using System.Collections.Generic;

namespace TByd.PackageCreator.Editor.Core
{
    /// <summary>
    /// 包依赖定义
    /// </summary>
    [Serializable]
    public class PackageDependency
    {
        /// <summary>
        /// 包ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 版本表达式
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 创建一个新的包依赖
        /// </summary>
        /// <param name="id">包ID</param>
        /// <param name="version">版本表达式</param>
        public PackageDependency(string id, string version)
        {
            Id = id;
            Version = version;
        }
    }

    /// <summary>
    /// 包作者信息
    /// </summary>
    [Serializable]
    public class PackageAuthor
    {
        /// <summary>
        /// 作者名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 作者邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 作者URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 创建一个新的包作者信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="email">邮箱</param>
        /// <param name="url">URL</param>
        public PackageAuthor(string name, string email = "", string url = "")
        {
            Name = name;
            Email = email;
            Url = url;
        }
    }

    /// <summary>
    /// 包配置，定义包的基本信息和依赖
    /// </summary>
    [Serializable]
    public class PackageConfig
    {
        /// <summary>
        /// 包名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 包显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 包版本
        /// </summary>
        public string Version { get; set; } = "0.1.0";

        /// <summary>
        /// 包描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Unity最低兼容版本
        /// </summary>
        public string UnityVersion { get; set; } = "2021.3";

        /// <summary>
        /// 最低Unity版本要求，用于向后兼容性校验
        /// </summary>
        public string MinUnityVersion { get; set; } = "2021.3";

        /// <summary>
        /// 根命名空间
        /// </summary>
        public string RootNamespace { get; set; }

        /// <summary>
        /// 包依赖
        /// </summary>
        public List<PackageDependency> Dependencies { get; set; } = new List<PackageDependency>();

        /// <summary>
        /// 包关键字
        /// </summary>
        public List<string> Keywords { get; set; } = new List<string>();

        /// <summary>
        /// 包作者
        /// </summary>
        public PackageAuthor Author { get; set; }

        /// <summary>
        /// 包许可证
        /// </summary>
        public string License { get; set; } = "MIT";

        /// <summary>
        /// 文档URL
        /// </summary>
        public string DocumentationUrl { get; set; }

        /// <summary>
        /// 变更日志URL
        /// </summary>
        public string ChangelogUrl { get; set; }

        /// <summary>
        /// 许可证URL
        /// </summary>
        public string LicenseUrl { get; set; }

        /// <summary>
        /// 是否包含Tests
        /// </summary>
        public bool IncludeTests { get; set; } = true;

        /// <summary>
        /// 是否包含Samples
        /// </summary>
        public bool IncludeSamples { get; set; } = true;

        /// <summary>
        /// 是否包含Documentation
        /// </summary>
        public bool IncludeDocumentation { get; set; } = true;

        /// <summary>
        /// 自定义选项
        /// </summary>
        public Dictionary<string, string> CustomOptions { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 公司/组织名称
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 自定义变量字典，用于模板中的变量替换
        /// </summary>
        public Dictionary<string, string> CustomVariables { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 创建一个新的包配置
        /// </summary>
        /// <param name="name">包名称</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="version">版本</param>
        /// <param name="description">描述</param>
        public PackageConfig(string name, string displayName, string version = "0.1.0", string description = "")
        {
            Name = name;
            DisplayName = displayName;
            Version = version;
            Description = description;
            Author = new PackageAuthor("", "", "");
        }

        /// <summary>
        /// 添加依赖
        /// </summary>
        /// <param name="id">包ID</param>
        /// <param name="version">版本表达式</param>
        public void AddDependency(string id, string version)
        {
            Dependencies.Add(new PackageDependency(id, version));
        }

        /// <summary>
        /// 添加关键字
        /// </summary>
        /// <param name="keyword">关键字</param>
        public void AddKeyword(string keyword)
        {
            Keywords.Add(keyword);
        }

        /// <summary>
        /// 设置作者信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="email">邮箱</param>
        /// <param name="url">URL</param>
        public void SetAuthor(string name, string email = "", string url = "")
        {
            Author = new PackageAuthor(name, email, url);
        }

        /// <summary>
        /// 添加自定义选项
        /// </summary>
        /// <param name="key">选项键</param>
        /// <param name="value">选项值</param>
        public void AddCustomOption(string key, string value)
        {
            CustomOptions[key] = value;
        }
    }
}
