using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.Config;
using TByd.CodeStyle.Editor.UI.Utils;
using TByd.CodeStyle.Runtime.Config;
using TByd.CodeStyle.Runtime.Git;
using TByd.CodeStyle.Runtime.Git.Commit;

namespace TByd.CodeStyle.Editor.Git.Commit
{
    /// <summary>
    /// 提交消息检查器，用于在编辑器中管理提交消息检查
    /// </summary>
    [InitializeOnLoad]
    public static class CommitMessageChecker
    {
        // 环境变量名，用于标识在Unity编辑器中运行
        private const string c_UnityEditorEnvVar = "UNITY_EDITOR";
        
        // 是否已初始化
        private static bool s_Initialized;
        
        // 提交消息验证器
        private static CommitMessageValidator s_Validator;
        
        // 提交消息修复器
        private static CommitMessageFixer s_Fixer;
        
        /// <summary>
        /// 静态构造函数，在编辑器加载时初始化
        /// </summary>
        static CommitMessageChecker()
        {
            // 延迟初始化，确保配置已加载
            EditorApplication.delayCall += Initialize;
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        private static void Initialize()
        {
            if (s_Initialized)
                return;
                
            // 订阅配置变更事件
            ConfigProvider.ConfigChanged += OnConfigChanged;
            
            // 初始化验证器和修复器
            InitializeValidatorAndFixer();
            
            // 设置环境变量，标识在Unity编辑器中运行
            SetUnityEditorEnvironmentVariable();
            
            s_Initialized = true;
            
            Debug.Log("[TByd.CodeStyle] 提交消息检查器初始化成功");
        }
        
        /// <summary>
        /// 配置变更事件处理
        /// </summary>
        private static void OnConfigChanged()
        {
            // 重新初始化验证器和修复器
            InitializeValidatorAndFixer();
        }
        
        /// <summary>
        /// 初始化验证器和修复器
        /// </summary>
        private static void InitializeValidatorAndFixer()
        {
            // 获取当前配置
            CodeStyleConfig config = ConfigProvider.GetConfig();
            
            // 初始化验证器
            s_Validator = new CommitMessageValidator(config.GitCommitConfig);
            
            // 初始化修复器
            s_Fixer = new CommitMessageFixer(config.GitCommitConfig);
        }
        
        /// <summary>
        /// 设置环境变量，标识在Unity编辑器中运行
        /// </summary>
        private static void SetUnityEditorEnvironmentVariable()
        {
            try
            {
                // 设置环境变量
                Environment.SetEnvironmentVariable(c_UnityEditorEnvVar, "true");
                
                Debug.Log("[TByd.CodeStyle] 已设置环境变量: UNITY_EDITOR=true");
            }
            catch (Exception e)
            {
                Debug.LogError($"[TByd.CodeStyle] 设置环境变量失败: {e.Message}");
            }
        }
        
        /// <summary>
        /// 验证提交消息
        /// </summary>
        /// <param name="_messageText">提交消息文本</param>
        /// <returns>验证结果</returns>
        public static CommitMessageValidationResult ValidateMessage(string _messageText)
        {
            if (s_Validator == null)
            {
                InitializeValidatorAndFixer();
            }
            
            return s_Validator.ValidateText(_messageText);
        }
        
        /// <summary>
        /// 验证提交消息文件
        /// </summary>
        /// <param name="_filePath">提交消息文件路径</param>
        /// <returns>验证结果</returns>
        public static CommitMessageValidationResult ValidateMessageFile(string _filePath)
        {
            if (s_Validator == null)
            {
                InitializeValidatorAndFixer();
            }
            
            return s_Validator.ValidateFile(_filePath);
        }
        
        /// <summary>
        /// 修复提交消息
        /// </summary>
        /// <param name="_messageText">提交消息文本</param>
        /// <returns>修复后的提交消息文本</returns>
        public static string FixMessage(string _messageText)
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }
            
            return s_Fixer.FixText(_messageText);
        }
        
        /// <summary>
        /// 修复提交消息文件
        /// </summary>
        /// <param name="_filePath">提交消息文件路径</param>
        /// <returns>是否修复成功</returns>
        public static bool FixMessageFile(string _filePath)
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }
            
            return s_Fixer.FixFile(_filePath);
        }
        
        /// <summary>
        /// 生成提交消息模板
        /// </summary>
        /// <returns>提交消息模板</returns>
        public static string GenerateTemplate()
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }
            
            return s_Fixer.GenerateTemplate();
        }
        
        /// <summary>
        /// 将模板写入提交消息文件
        /// </summary>
        /// <param name="_filePath">提交消息文件路径</param>
        /// <returns>是否写入成功</returns>
        public static bool WriteTemplateToFile(string _filePath)
        {
            if (s_Fixer == null)
            {
                InitializeValidatorAndFixer();
            }
            
            return s_Fixer.WriteTemplateToFile(_filePath);
        }
        
        /// <summary>
        /// 从命令行参数验证提交消息文件
        /// </summary>
        /// <param name="_args">命令行参数</param>
        /// <returns>验证结果</returns>
        public static int ValidateFromCommandLine(string[] _args)
        {
            try
            {
                if (_args.Length < 1)
                {
                    Console.Error.WriteLine("错误: 缺少提交消息文件路径参数");
                    return 1;
                }
                
                string filePath = _args[0];
                
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"错误: 提交消息文件不存在: {filePath}");
                    return 1;
                }
                
                // 初始化验证器
                if (s_Validator == null)
                {
                    InitializeValidatorAndFixer();
                }
                
                // 验证提交消息
                CommitMessageValidationResult result = s_Validator.ValidateFile(filePath);
                
                if (!result.IsValid)
                {
                    Console.Error.WriteLine(result.GetFormattedErrorMessage());
                    return 1;
                }
                
                Console.WriteLine("提交消息验证通过");
                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"错误: {e.Message}");
                return 1;
            }
        }
    }
} 