using System.Collections.Generic;
using System.IO;
using TByd.CodeStyle.Editor.CodeCheck.EditorConfig;
using UnityEditor;
using UnityEngine;

namespace TByd.CodeStyle.Editor.UI.Settings
{
    /// <summary>
    /// EditorConfig设置提供者
    /// </summary>
    public class EditorConfigSettingsProvider : SettingsProvider
    {
        // 设置路径
        private const string c_SettingsPath = "Project/TByd/EditorConfig";

        // 编辑器样式
        private GUIStyle m_HeaderStyle;
        private GUIStyle m_SectionStyle;
        private GUIStyle m_RuleHeaderStyle;
        private GUIStyle m_RuleContentStyle;

        // 规则列表滚动位置
        private Vector2 m_RulesScrollPosition;

        // 是否显示规则详情
        private Dictionary<string, bool> m_ShowRuleDetails = new Dictionary<string, bool>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public EditorConfigSettingsProvider() : base(c_SettingsPath, SettingsScope.Project)
        {
            // 设置关键字，用于搜索
            keywords = new HashSet<string>(new[]
            {
                "EditorConfig", "代码风格", "缩进", "换行", "空格", "编码", "格式化"
            });
        }

        /// <summary>
        /// 初始化样式
        /// </summary>
        private void InitStyles()
        {
            if (m_HeaderStyle == null)
            {
                m_HeaderStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 14,
                    margin = new RectOffset(0, 0, 10, 10)
                };
            }

            if (m_SectionStyle == null)
            {
                m_SectionStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 12,
                    margin = new RectOffset(0, 0, 5, 5)
                };
            }

            if (m_RuleHeaderStyle == null)
            {
                m_RuleHeaderStyle = new GUIStyle(EditorStyles.foldout)
                {
                    fontStyle = FontStyle.Bold
                };
            }

            if (m_RuleContentStyle == null)
            {
                m_RuleContentStyle = new GUIStyle(EditorStyles.helpBox)
                {
                    padding = new RectOffset(10, 10, 10, 10),
                    margin = new RectOffset(20, 0, 5, 5)
                };
            }
        }

        /// <summary>
        /// 绘制设置界面
        /// </summary>
        /// <param name="_searchContext">搜索上下文</param>
        public override void OnGUI(string _searchContext)
        {
            InitStyles();

            // 绘制标题
            EditorGUILayout.LabelField("EditorConfig 设置", m_HeaderStyle);
            EditorGUILayout.Space();

            // 检查项目是否存在EditorConfig文件
            var hasEditorConfig = EditorConfigManager.HasProjectEditorConfig();

            // 绘制状态信息
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("EditorConfig 状态:", GUILayout.Width(150));

                if (hasEditorConfig)
                {
                    EditorGUILayout.LabelField("已启用", EditorStyles.boldLabel);
                }
                else
                {
                    EditorGUILayout.LabelField("未启用", EditorStyles.boldLabel);
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("配置文件路径:", GUILayout.Width(150));
                EditorGUILayout.TextField(EditorConfigManager.GetProjectEditorConfigPath());
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // 绘制操作按钮
            EditorGUILayout.BeginHorizontal();
            {
                if (hasEditorConfig)
                {
                    // 重新加载按钮
                    if (GUILayout.Button("重新加载", GUILayout.Width(100)))
                    {
                        EditorConfigManager.LoadProjectEditorConfig();
                    }

                    // 编辑按钮
                    if (GUILayout.Button("编辑文件", GUILayout.Width(100)))
                    {
                        var editorConfigPath = EditorConfigManager.GetProjectEditorConfigPath();
                        if (File.Exists(editorConfigPath))
                        {
                            // 使用系统默认编辑器打开文件
                            EditorUtility.OpenWithDefaultApp(editorConfigPath);
                        }
                    }

                    // 导出按钮
                    if (GUILayout.Button("导出", GUILayout.Width(100)))
                    {
                        var path = EditorUtility.SaveFilePanel(
                            "导出EditorConfig",
                            "",
                            ".editorconfig",
                            "");

                        if (!string.IsNullOrEmpty(path))
                        {
                            EditorConfigManager.ExportEditorConfig(path);
                        }
                    }
                }
                else
                {
                    // 创建默认配置按钮
                    if (GUILayout.Button("创建默认配置", GUILayout.Width(120)))
                    {
                        EditorConfigManager.CreateDefaultEditorConfig();
                    }

                    // 创建Unity项目配置按钮
                    if (GUILayout.Button("创建Unity项目配置", GUILayout.Width(150)))
                    {
                        EditorConfigManager.CreateUnityProjectEditorConfig();
                    }

                    // 导入按钮
                    if (GUILayout.Button("导入", GUILayout.Width(100)))
                    {
                        var path = EditorUtility.OpenFilePanel(
                            "导入EditorConfig",
                            "",
                            "");

                        if (!string.IsNullOrEmpty(path))
                        {
                            EditorConfigManager.ImportEditorConfig(path);
                        }
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // 如果存在EditorConfig文件，显示规则列表
            if (hasEditorConfig)
            {
                DrawRulesList();
            }
        }

        /// <summary>
        /// 绘制规则列表
        /// </summary>
        private void DrawRulesList()
        {
            EditorGUILayout.LabelField("规则列表", m_SectionStyle);

            var rules = EditorConfigManager.GetRules();

            if (rules.Count == 0)
            {
                EditorGUILayout.HelpBox("没有定义规则", MessageType.Info);
                return;
            }

            m_RulesScrollPosition = EditorGUILayout.BeginScrollView(m_RulesScrollPosition);
            {
                for (var i = 0; i < rules.Count; i++)
                {
                    var rule = rules[i];

                    // 确保规则在字典中有一个条目
                    if (!m_ShowRuleDetails.ContainsKey(rule.Pattern))
                    {
                        m_ShowRuleDetails[rule.Pattern] = false;
                    }

                    // 绘制规则折叠标题
                    m_ShowRuleDetails[rule.Pattern] = EditorGUILayout.Foldout(
                        m_ShowRuleDetails[rule.Pattern],
                        $"[{rule.Pattern}] ({rule.Properties.Count} 个属性)",
                        true,
                        m_RuleHeaderStyle);

                    // 如果展开，显示规则详情
                    if (m_ShowRuleDetails[rule.Pattern])
                    {
                        EditorGUILayout.BeginVertical(m_RuleContentStyle);
                        {
                            // 显示规则属性
                            foreach (var property in rule.Properties)
                            {
                                EditorGUILayout.BeginHorizontal();
                                {
                                    EditorGUILayout.LabelField(property.Key, GUILayout.Width(200));
                                    EditorGUILayout.LabelField(property.Value);
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                }
            }
            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        /// <returns>设置提供者</returns>
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            return new EditorConfigSettingsProvider();
        }
    }
}
