using UnityEditor;
using UnityEngine;
using TByd.CodeStyle.Editor.UI.Utils;

namespace TByd.CodeStyle.Editor.UI.Windows
{
    /// <summary>
    /// 代码风格工具的主窗口
    /// </summary>
    public class CodeStyleWindow : EditorWindow
    {
        // 窗口标题
        private const string c_WindowTitle = "TByd 代码风格";
        
        // 窗口实例
        private static CodeStyleWindow s_Instance;
        
        // 滚动位置
        private Vector2 m_ScrollPosition;
        
        // 当前选中的标签页索引
        private int m_SelectedTabIndex;
        
        // 标签页名称
        private readonly string[] m_TabNames = { "概览", "Git提交规范", "代码检查", "设置" };
        
        /// <summary>
        /// 打开窗口的菜单项
        /// </summary>
        [MenuItem("Tools/TByd/代码风格工具", false, 100)]
        public static void ShowWindow()
        {
            s_Instance = GetWindow<CodeStyleWindow>(false, c_WindowTitle, true);
            s_Instance.minSize = new Vector2(600, 400);
            s_Instance.Show();
            
            // 显示欢迎通知
            NotificationSystem.ShowNotification("欢迎使用TByd代码风格工具！", NotificationType.Info);
        }
        
        /// <summary>
        /// 窗口初始化
        /// </summary>
        private void OnEnable()
        {
            // 加载窗口图标
            // titleContent.image = EditorGUIUtility.FindTexture("d_UnityEditor.ConsoleWindow");
        }
        
        /// <summary>
        /// 绘制窗口内容
        /// </summary>
        private void OnGUI()
        {
            // 绘制通知
            NotificationSystem.DrawNotification();
            
            DrawToolbar();
            
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            
            switch (m_SelectedTabIndex)
            {
                case 0:
                    DrawOverviewTab();
                    break;
                case 1:
                    DrawGitCommitTab();
                    break;
                case 2:
                    DrawCodeCheckTab();
                    break;
                case 3:
                    DrawSettingsTab();
                    break;
            }
            
            EditorGUILayout.EndScrollView();
        }
        
        /// <summary>
        /// 绘制工具栏
        /// </summary>
        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            
            m_SelectedTabIndex = GUILayout.Toolbar(m_SelectedTabIndex, m_TabNames, EditorStyles.toolbarButton);
            
            EditorGUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// 绘制概览标签页
        /// </summary>
        private void DrawOverviewTab()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            GUILayout.Label("TByd 代码风格工具", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("版本:", "0.0.1");
            EditorGUILayout.LabelField("状态:", "开发中");
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("这个工具帮助您维护代码质量和提交规范。");
            
            EditorGUILayout.Space();
            
            if (GUILayout.Button("显示测试通知"))
            {
                NotificationSystem.ShowNotification("这是一条测试通知", NotificationType.Info);
            }
            
            if (GUILayout.Button("显示测试警告"))
            {
                NotificationSystem.ShowNotification("这是一条测试警告", NotificationType.Warning);
            }
            
            if (GUILayout.Button("显示测试错误"))
            {
                NotificationSystem.ShowNotification("这是一条测试错误", NotificationType.Error);
            }
            
            if (GUILayout.Button("显示测试成功"))
            {
                NotificationSystem.ShowNotification("这是一条测试成功消息", NotificationType.Success);
            }
            
            if (GUILayout.Button("显示测试进度"))
            {
                ShowTestProgress();
            }
            
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// 显示测试进度
        /// </summary>
        private void ShowTestProgress()
        {
            EditorApplication.delayCall += () =>
            {
                for (int i = 0; i <= 10; i++)
                {
                    float progress = i / 10f;
                    NotificationSystem.ShowProgress("测试进度", $"正在处理... {i * 10}%", progress);
                    
                    // 模拟处理时间
                    System.Threading.Thread.Sleep(200);
                }
                
                NotificationSystem.HideProgress();
                NotificationSystem.ShowNotification("进度测试完成", NotificationType.Success);
            };
        }
        
        /// <summary>
        /// 绘制Git提交规范标签页
        /// </summary>
        private void DrawGitCommitTab()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            GUILayout.Label("Git提交规范", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("此功能正在开发中...");
            
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// 绘制代码检查标签页
        /// </summary>
        private void DrawCodeCheckTab()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            GUILayout.Label("代码检查", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("此功能正在开发中...");
            
            EditorGUILayout.EndVertical();
        }
        
        /// <summary>
        /// 绘制设置标签页
        /// </summary>
        private void DrawSettingsTab()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            GUILayout.Label("设置", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            if (GUILayout.Button("打开项目设置"))
            {
                SettingsService.OpenProjectSettings("Project/TByd/代码风格");
            }
            
            EditorGUILayout.EndVertical();
        }
    }
} 