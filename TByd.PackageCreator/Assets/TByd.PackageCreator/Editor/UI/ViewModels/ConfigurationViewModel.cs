using System.Linq;
using System.Text.RegularExpressions;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.UI.Utils;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.ViewModels
{
    /// <summary>
    /// 配置页面视图模型，处理包配置数据的绑定和验证
    /// </summary>
    public class ConfigurationViewModel : IViewModel
    {
        // 配置管理器
        private readonly IConfigManager _configManager;

        // 包配置引用
        private PackageConfig _packageConfig;

        // 默认的许可证选项
        private readonly string[] _licenseOptions = new string[]
        {
            "MIT",
            "Apache-2.0",
            "BSD-3-Clause",
            "BSD-2-Clause",
            "GPL-2.0",
            "GPL-3.0",
            "LGPL-2.1",
            "LGPL-3.0",
            "MPL-2.0",
            "CDDL-1.0",
            "EPL-2.0",
            "CC0-1.0",
            "Unlicense",
            "Custom"
        };

        // 包名验证正则表达式 - 符合UPM包名规范
        private readonly Regex _packageNameRegex = new Regex(@"^[a-z][a-z0-9\.\-]*$");

        // 版本号验证正则表达式 - 符合语义化版本规范
        private readonly Regex _versionRegex = new Regex(@"^\d+\.\d+\.\d+(-[0-9A-Za-z-]+(\.[0-9A-Za-z-]+)*)?(\+[0-9A-Za-z-]+(\.[0-9A-Za-z-]+)*)?$");

        /// <summary>
        /// 许可证选项列表
        /// </summary>
        public string[] LicenseOptions => _licenseOptions;

        /// <summary>
        /// 包配置对象
        /// </summary>
        public PackageConfig PackageConfig => _packageConfig;

        /// <summary>
        /// 包名称是否有效
        /// </summary>
        public bool IsPackageNameValid => !string.IsNullOrEmpty(PackageName) && _packageNameRegex.IsMatch(PackageName);

        /// <summary>
        /// 版本号是否有效
        /// </summary>
        public bool IsVersionValid => !string.IsNullOrEmpty(Version) && _versionRegex.IsMatch(Version);

        /// <summary>
        /// 页面是否有效，用于确定是否可以进入下一页
        /// </summary>
        public bool IsValid
        {
            get
            {
                // 验证必填字段
                if (string.IsNullOrEmpty(PackageName) || !IsPackageNameValid)
                    return false;

                if (string.IsNullOrEmpty(DisplayName))
                    return false;

                if (string.IsNullOrEmpty(Version) || !IsVersionValid)
                    return false;

                if (string.IsNullOrEmpty(Description))
                    return false;

                if (string.IsNullOrEmpty(AuthorName))
                    return false;

                return true;
            }
        }

        #region 包配置属性

        /// <summary>
        /// 包名称
        /// </summary>
        public string PackageName
        {
            get => _packageConfig?.Name ?? string.Empty;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.Name = value;

                    // 如果根命名空间为空，自动设置根命名空间
                    if (string.IsNullOrEmpty(_packageConfig.RootNamespace))
                    {
                        // 将包名转换为有效的命名空间
                        string namespaceSuggestion = value
                            .Replace("-", ".")
                            .Split('.')
                            .Select(part => part.Length > 0 ? char.ToUpper(part[0]) + part.Substring(1) : part)
                            .Aggregate((a, b) => a + "." + b);

                        _packageConfig.RootNamespace = namespaceSuggestion;
                    }
                }
            }
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName
        {
            get => _packageConfig?.DisplayName ?? string.Empty;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.DisplayName = value;
                }
            }
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get => _packageConfig?.Version ?? "0.1.0";
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.Version = value;
                }
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get => _packageConfig?.Description ?? string.Empty;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.Description = value;
                }
            }
        }

        /// <summary>
        /// 根命名空间
        /// </summary>
        public string RootNamespace
        {
            get => _packageConfig?.RootNamespace ?? string.Empty;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.RootNamespace = value;
                }
            }
        }

        /// <summary>
        /// 作者名称
        /// </summary>
        public string AuthorName
        {
            get => _packageConfig?.Author?.Name ?? string.Empty;
            set
            {
                if (_packageConfig?.Author != null)
                {
                    _packageConfig.Author.Name = value;
                }
                else if (_packageConfig != null)
                {
                    _packageConfig.Author = new PackageAuthor(value);
                }
            }
        }

        /// <summary>
        /// 作者邮箱
        /// </summary>
        public string AuthorEmail
        {
            get => _packageConfig?.Author?.Email ?? string.Empty;
            set
            {
                if (_packageConfig?.Author != null)
                {
                    _packageConfig.Author.Email = value;
                }
                else if (_packageConfig != null)
                {
                    _packageConfig.Author = new PackageAuthor("", value);
                }
            }
        }

        /// <summary>
        /// 作者URL
        /// </summary>
        public string AuthorUrl
        {
            get => _packageConfig?.Author?.Url ?? string.Empty;
            set
            {
                if (_packageConfig?.Author != null)
                {
                    _packageConfig.Author.Url = value;
                }
                else if (_packageConfig != null)
                {
                    _packageConfig.Author = new PackageAuthor("", "", value);
                }
            }
        }

        /// <summary>
        /// 公司/组织名称
        /// </summary>
        public string Company
        {
            get => _packageConfig?.Company ?? string.Empty;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.Company = value;
                }
            }
        }

        /// <summary>
        /// Unity版本
        /// </summary>
        public string UnityVersion
        {
            get => _packageConfig?.UnityVersion ?? "2021.3";
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.UnityVersion = value;
                }
            }
        }

        /// <summary>
        /// 许可证
        /// </summary>
        public string License
        {
            get => _packageConfig?.License ?? "MIT";
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.License = value;
                }
            }
        }

        /// <summary>
        /// 文档URL
        /// </summary>
        public string DocumentationUrl
        {
            get => _packageConfig?.DocumentationUrl ?? string.Empty;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.DocumentationUrl = value;
                }
            }
        }

        /// <summary>
        /// 变更日志URL
        /// </summary>
        public string ChangelogUrl
        {
            get => _packageConfig?.ChangelogUrl ?? string.Empty;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.ChangelogUrl = value;
                }
            }
        }

        /// <summary>
        /// 许可证URL
        /// </summary>
        public string LicenseUrl
        {
            get => _packageConfig?.LicenseUrl ?? string.Empty;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.LicenseUrl = value;
                }
            }
        }

        /// <summary>
        /// 包含Tests
        /// </summary>
        public bool IncludeTests
        {
            get => _packageConfig?.IncludeTests ?? true;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.IncludeTests = value;
                }
            }
        }

        /// <summary>
        /// 包含Samples
        /// </summary>
        public bool IncludeSamples
        {
            get => _packageConfig?.IncludeSamples ?? true;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.IncludeSamples = value;
                }
            }
        }

        /// <summary>
        /// 包含Documentation
        /// </summary>
        public bool IncludeDocumentation
        {
            get => _packageConfig?.IncludeDocumentation ?? true;
            set
            {
                if (_packageConfig != null)
                {
                    _packageConfig.IncludeDocumentation = value;
                }
            }
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configManager">配置管理器</param>
        public ConfigurationViewModel(IConfigManager configManager)
        {
            _configManager = configManager;
        }

        /// <summary>
        /// 初始化视图模型
        /// </summary>
        public void Initialize()
        {
            // 从全局状态获取包配置
            var state = UIStateManager.Instance.CreationState;
            _packageConfig = state.PackageConfig;

            // 如果包配置为空，创建一个新的
            if (_packageConfig == null)
            {
                _packageConfig = new PackageConfig();
                state.PackageConfig = _packageConfig;
            }

            // 确保包有作者信息
            if (_packageConfig.Author == null)
            {
                _packageConfig.Author = new PackageAuthor("");
            }
        }

        /// <summary>
        /// 清理视图模型
        /// </summary>
        public void Cleanup()
        {
            // 确保包配置不为空
            if (_packageConfig == null)
            {
                Debug.LogWarning("ConfigurationViewModel.Cleanup: 包配置为空");
                return;
            }

            // 保存修改到全局状态
            UIStateManager.Instance.UpdateState(state =>
            {
                state.PackageConfig = _packageConfig;
            });

            // 添加调试日志
            Debug.Log($"ConfigurationViewModel保存配置: 包名={_packageConfig.Name}, 显示名称={_packageConfig.DisplayName}, 作者={_packageConfig?.Author?.Name}");
        }
    }
}
