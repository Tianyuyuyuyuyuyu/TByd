using System;
using System.Collections.Generic;
using System.IO;
using TByd.PackageCreator.Editor.Core.Interfaces;
using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.Core.Services;
using TByd.PackageCreator.Editor.UI.Utils;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.ViewModels
{
    /// <summary>
    /// 结果页面的视图模型，处理包创建结果的显示和后续操作
    /// </summary>
    public class ResultViewModel : IViewModel
    {
        // 服务引用
        private readonly IConfigManager _configManager;

        // 包配置引用
        private PackageConfig _packageConfig;

        // 创建结果
        private ValidationResult _creationResult;

        // 创建状态
        private bool _isCreationSuccessful;
        private string _errorMessage;
        private string _packagePath;
        private List<string> _createdFiles = new List<string>();

        /// <summary>
        /// 获取包配置
        /// </summary>
        public PackageConfig PackageConfig => _packageConfig;

        /// <summary>
        /// 获取创建结果
        /// </summary>
        public ValidationResult CreationResult => _creationResult;

        /// <summary>
        /// 判断创建是否成功
        /// </summary>
        public bool IsCreationSuccessful => _isCreationSuccessful;

        /// <summary>
        /// 获取错误信息
        /// </summary>
        public string ErrorMessage => _errorMessage;

        /// <summary>
        /// 获取包路径
        /// </summary>
        public string PackagePath => _packagePath;

        /// <summary>
        /// 获取创建的文件列表
        /// </summary>
        public IReadOnlyList<string> CreatedFiles => _createdFiles;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configManager">配置管理器</param>
        public ResultViewModel(IConfigManager configManager)
        {
            _configManager = configManager ?? throw new ArgumentNullException(nameof(configManager));
        }

        /// <summary>
        /// 初始化视图模型
        /// </summary>
        public void Initialize()
        {
            _packageConfig = _configManager.CurrentConfig;

            // 从UIStateManager获取创建状态
            var state = UIStateManager.Instance.CreationState;
            _isCreationSuccessful = state.IsCreationSuccessful;
            _errorMessage = state.ErrorMessage;
            _packagePath = state.PackagePath;
            _creationResult = state.CreationResult;

            // 如果创建成功，获取创建的文件列表
            if (_isCreationSuccessful && !string.IsNullOrEmpty(_packagePath) && Directory.Exists(_packagePath))
            {
                CollectCreatedFiles(_packagePath);
            }
        }

        /// <summary>
        /// 清理视图模型
        /// </summary>
        public void Cleanup()
        {
            // 暂无需要清理的资源
        }

        /// <summary>
        /// 收集创建的文件列表
        /// </summary>
        /// <param name="rootPath">根路径</param>
        private void CollectCreatedFiles(string rootPath)
        {
            _createdFiles.Clear();

            try
            {
                // 获取所有文件
                var files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories);

                // 转换为相对路径
                foreach (var file in files)
                {
                    string relativePath = file.Replace(rootPath, "").TrimStart('\\', '/');
                    _createdFiles.Add(relativePath);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"获取创建的文件列表时出错: {ex.Message}");
            }
        }

        /// <summary>
        /// 在文件资源管理器中打开包文件夹
        /// </summary>
        public void OpenPackageFolder()
        {
            if (!string.IsNullOrEmpty(_packagePath) && Directory.Exists(_packagePath))
            {
                EditorUtility.RevealInFinder(_packagePath);
            }
        }

        /// <summary>
        /// 复制包路径到剪贴板
        /// </summary>
        public void CopyPackagePathToClipboard()
        {
            if (!string.IsNullOrEmpty(_packagePath))
            {
                EditorGUIUtility.systemCopyBuffer = _packagePath;
            }
        }

        /// <summary>
        /// 重新开始创建流程
        /// </summary>
        public void RestartCreationProcess()
        {
            // 重置UI状态
            UIStateManager.Instance.ResetState();
        }

        /// <summary>
        /// 获取包信息摘要
        /// </summary>
        /// <returns>包信息摘要</returns>
        public string GetPackageSummary()
        {
            if (_packageConfig == null)
            {
                return "无包配置信息";
            }

            return $"包名称: {_packageConfig.Name}\n" +
                   $"显示名称: {_packageConfig.DisplayName}\n" +
                   $"版本: {_packageConfig.Version}\n" +
                   $"描述: {_packageConfig.Description}";
        }

        /// <summary>
        /// 获取验证消息
        /// </summary>
        /// <param name="level">消息级别，null表示获取所有级别</param>
        /// <returns>验证消息列表</returns>
        public List<ValidationMessage> GetValidationMessages(ValidationMessageLevel? level = null)
        {
            if (_creationResult == null || _creationResult.Messages == null)
            {
                return new List<ValidationMessage>();
            }

            if (level.HasValue)
            {
                return _creationResult.GetMessages(level.Value);
            }
            else
            {
                return _creationResult.Messages;
            }
        }
    }
}
