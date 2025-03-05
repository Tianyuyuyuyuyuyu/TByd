using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;

namespace TByd.CodeStyle.Editor.CodeCheck.IDE
{
    /// <summary>
    /// IDE集成接口
    /// </summary>
    public interface IDEIntegration
    {
        /// <summary>
        /// IDE名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 是否已安装
        /// </summary>
        bool IsInstalled { get; }

        /// <summary>
        /// 导出配置到IDE
        /// </summary>
        /// <param name="_rules">EditorConfig规则列表</param>
        /// <returns>是否成功</returns>
        bool ExportConfig(List<EditorConfigRule> _rules);
    }

    /// <summary>
    /// IDE集成基类
    /// </summary>
    public abstract class IDEIntegrationBase : IDEIntegration
    {
        /// <summary>
        /// IDE名称
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// IDE图标
        /// </summary>
        public virtual Texture2D Icon => null;

        /// <summary>
        /// 是否已安装
        /// </summary>
        public abstract bool IsInstalled { get; }

        /// <summary>
        /// 导出配置到IDE
        /// </summary>
        /// <param name="_rules">EditorConfig规则</param>
        /// <returns>是否成功</returns>
        public abstract bool ExportConfig(List<EditorConfigRule> _rules);

        /// <summary>
        /// 获取项目根目录
        /// </summary>
        /// <returns>项目根目录</returns>
        protected string GetProjectRootPath()
        {
            return Path.GetDirectoryName(Application.dataPath);
        }
    }
} 