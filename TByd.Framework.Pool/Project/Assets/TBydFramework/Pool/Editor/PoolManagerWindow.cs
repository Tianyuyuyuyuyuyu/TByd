using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Editor
{
    public class PoolManagerWindow : EditorWindow
    {
        private Vector2 _scrollPosition;
        private bool _showGameObjectPools = true;
        private bool _showAddressablePools = true;
        private bool _showSettings = false;
        private GUIStyle _headerStyle;

        [MenuItem("Tools/TByd Framework/Pool Manager")]
        public static void ShowWindow()
        {
            GetWindow<PoolManagerWindow>("Pool Manager");
        }

        private void OnEnable()
        {
            _headerStyle = new GUIStyle();
            _headerStyle.fontStyle = FontStyle.Bold;
            _headerStyle.fontSize = 14;
            _headerStyle.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
            _headerStyle.margin = new RectOffset(5, 5, 10, 10);
        }

        private void OnGUI()
        {
            DrawToolbar();

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            if (_showSettings)
            {
                DrawSettings();
            }
            
            if (_showGameObjectPools)
            {
                DrawGameObjectPools();
            }

            if (_showAddressablePools)
            {
                DrawAddressablePools();
            }

            EditorGUILayout.EndScrollView();
        }

        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            
            _showSettings = GUILayout.Toggle(_showSettings, "Settings", EditorStyles.toolbarButton);
            _showGameObjectPools = GUILayout.Toggle(_showGameObjectPools, "GameObject Pools", EditorStyles.toolbarButton);
            _showAddressablePools = GUILayout.Toggle(_showAddressablePools, "Addressable Pools", EditorStyles.toolbarButton);
            
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button("Refresh", EditorStyles.toolbarButton))
            {
                Repaint();
            }
            
            EditorGUILayout.EndHorizontal();
        }

        private void DrawSettings()
        {
            EditorGUILayout.LabelField("Pool Settings", _headerStyle);
            EditorGUILayout.Space();

            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField("Default Settings", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                
                PoolSettings.DefaultCapacity = EditorGUILayout.IntField("Default Capacity", PoolSettings.DefaultCapacity);
                PoolSettings.MaxSize = EditorGUILayout.IntField("Max Size", PoolSettings.MaxSize);
                PoolSettings.EnableLogging = EditorGUILayout.Toggle("Enable Logging", PoolSettings.EnableLogging);
                
                EditorGUI.indentLevel--;
            }
        }

        private void DrawGameObjectPools()
        {
            EditorGUILayout.LabelField("GameObject Pools", _headerStyle);
            EditorGUILayout.Space();

            using (new EditorGUILayout.VerticalScope("box"))
            {
                // 这里需要实现获取和显示活动的GameObject池
                // 可以通过静态注册表或反射来获取
                EditorGUILayout.LabelField("Active Pools", EditorStyles.boldLabel);
                EditorGUILayout.HelpBox("No active GameObject pools found.", MessageType.Info);
            }
        }

        private void DrawAddressablePools()
        {
            EditorGUILayout.LabelField("Addressable Pools", _headerStyle);
            EditorGUILayout.Space();

            using (new EditorGUILayout.VerticalScope("box"))
            {
                #if TBYD_ADDRESSABLES_SUPPORT
                // 这里需要实现获取和显示活动的Addressable池
                EditorGUILayout.LabelField("Active Pools", EditorStyles.boldLabel);
                EditorGUILayout.HelpBox("No active Addressable pools found.", MessageType.Info);
                #else
                EditorGUILayout.HelpBox("Addressables package is not installed.", MessageType.Warning);
                if (GUILayout.Button("Install Addressables Package"))
                {
                    // 打开Package Manager并选中Addressables包
                    EditorApplication.ExecuteMenuItem("Window/Package Manager");
                }
                #endif
            }
        }
    }

    // 池设置类
    public static class PoolSettings
    {
        private const string DefaultCapacityKey = "TBydPool_DefaultCapacity";
        private const string MaxSizeKey = "TBydPool_MaxSize";
        private const string EnableLoggingKey = "TBydPool_EnableLogging";

        public static int DefaultCapacity
        {
            get => EditorPrefs.GetInt(DefaultCapacityKey, 10);
            set => EditorPrefs.SetInt(DefaultCapacityKey, value);
        }

        public static int MaxSize
        {
            get => EditorPrefs.GetInt(MaxSizeKey, 1000);
            set => EditorPrefs.SetInt(MaxSizeKey, value);
        }

        public static bool EnableLogging
        {
            get => EditorPrefs.GetBool(EnableLoggingKey, true);
            set => EditorPrefs.SetBool(EnableLoggingKey, value);
        }
    }
} 