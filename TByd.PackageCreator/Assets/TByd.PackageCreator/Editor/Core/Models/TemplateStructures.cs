using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TByd.PackageCreator.Editor.Core.Models
{
    /// <summary>
    /// 模板目录结构，定义模板包含的目录
    /// </summary>
    [Serializable]
    public class TemplateDirectory
    {
        /// <summary>
        /// 目录相对路径
        /// </summary>
        [JsonProperty("relativePath")]
        public string RelativePath { get; set; }

        /// <summary>
        /// 目录描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 是否是必需目录
        /// </summary>
        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; } = true;

        /// <summary>
        /// 子目录列表
        /// </summary>
        [JsonProperty("subdirectories")]
        public List<TemplateDirectory> Subdirectories { get; set; } = new List<TemplateDirectory>();

        /// <summary>
        /// 创建一个新的模板目录
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="description">描述</param>
        /// <param name="isRequired">是否必需</param>
        public TemplateDirectory(string relativePath, string description = "", bool isRequired = true)
        {
            RelativePath = relativePath;
            Description = description;
            IsRequired = isRequired;
        }

        /// <summary>
        /// 添加子目录
        /// </summary>
        /// <param name="subdirectory">要添加的子目录</param>
        public void AddSubdirectory(TemplateDirectory subdirectory)
        {
            Subdirectories.Add(subdirectory);
        }
    }

    /// <summary>
    /// 模板文件，定义模板包含的文件
    /// </summary>
    [Serializable]
    public class TemplateFile
    {
        /// <summary>
        /// 文件相对路径
        /// </summary>
        [JsonProperty("relativePath")]
        public string RelativePath { get; set; }

        /// <summary>
        /// 文件描述
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// 是否是必需文件
        /// </summary>
        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; } = true;

        /// <summary>
        /// 文件内容模板
        /// </summary>
        [JsonProperty("contentTemplate")]
        public string ContentTemplate { get; set; }

        /// <summary>
        /// 文件是否支持变量替换
        /// </summary>
        [JsonProperty("supportsVariableReplacement")]
        public bool SupportsVariableReplacement { get; set; } = true;

        /// <summary>
        /// 创建一个新的模板文件
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="contentTemplate">内容模板</param>
        /// <param name="description">描述</param>
        /// <param name="isRequired">是否必需</param>
        /// <param name="supportsVariableReplacement">是否支持变量替换</param>
        public TemplateFile(string relativePath, string contentTemplate = "", string description = "",
            bool isRequired = true, bool supportsVariableReplacement = true)
        {
            RelativePath = relativePath;
            ContentTemplate = contentTemplate;
            Description = description;
            IsRequired = isRequired;
            SupportsVariableReplacement = supportsVariableReplacement;
        }
    }

    /// <summary>
    /// 模板预览信息
    /// </summary>
    [Serializable]
    public class TemplatePreviewInfo
    {
        /// <summary>
        /// 预览标题
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 预览内容
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// 预览特点列表
        /// </summary>
        [JsonProperty("features")]
        public List<string> Features { get; set; } = new List<string>();

        /// <summary>
        /// 示例图
        /// </summary>
        [JsonProperty("previewImagePath")]
        public string PreviewImagePath { get; set; }

        /// <summary>
        /// 创建一个新的模板预览信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        public TemplatePreviewInfo(string title, string content)
        {
            Title = title;
            Content = content;
        }

        /// <summary>
        /// 添加特点
        /// </summary>
        /// <param name="feature">要添加的特点</param>
        public void AddFeature(string feature)
        {
            Features.Add(feature);
        }
    }
}
