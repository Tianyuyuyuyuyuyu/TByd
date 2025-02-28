using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TByd.CodeStyle.Editor.UI.Settings
{
    /// <summary>
    /// 代码风格设置提供者，用于在Project Settings窗口中显示设置
    /// </summary>
    public class CodeStyleSettingsProvider : SettingsProvider
    {
        // 设置路径
        private const string c_SettingsPath = "Project/TByd/代码风格";
        
        // 关键字
        private static readonly string[] s_Keywords = new string[] 
        { 
            "TByd", "代码风格", "Code", "Style", "Git", "Commit", "EditorConfig" 
        };
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_path">设置路径</param>
        /// <param name="_scopes">设置范围</param>
        /// <param name="_keywords">关键字</param>
        public CodeStyleSettingsProvider(string _path, SettingsScope _scopes, IEnumerable<string> _keywords = null) 
            : base(_path, _scopes, _keywords)
        {
        }
        
        /// <summary>
        /// 绘制设置UI
        /// </summary>
        /// <param name="_searchContext">搜索上下文</param>
        public override void OnGUI(string _searchContext)
        {
            EditorGUILayout.Space();
            
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("TByd 代码风格设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            DrawGeneralSettings();
            DrawGitCommitSettings();
            DrawCodeCheckSettings();
            
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// 绘制通用设置
        /// </summary>
        private void DrawGeneralSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("通用设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            // 这里添加通用设置项
            EditorGUILayout.LabelField("设置项正在开发中...");
            
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// 绘制Git提交设置
        /// </summary>
        private void DrawGitCommitSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Git提交设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            // 这里添加Git提交设置项
            EditorGUILayout.LabelField("设置项正在开发中...");
            
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// 绘制代码检查设置
        /// </summary>
        private void DrawCodeCheckSettings()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("代码检查设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            // 这里添加代码检查设置项
            EditorGUILayout.LabelField("设置项正在开发中...");
            
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// 创建设置提供者
        /// </summary>
        /// <returns>设置提供者实例</returns>
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new CodeStyleSettingsProvider(c_SettingsPath, SettingsScope.Project, s_Keywords);
        }
    }
} 