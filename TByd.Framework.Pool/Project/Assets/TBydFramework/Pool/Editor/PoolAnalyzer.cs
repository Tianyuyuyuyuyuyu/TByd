using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using TBydFramework.Pool.Runtime.Core;
using TBydFramework.Pool.Runtime.Interfaces;

namespace TBydFramework.Pool.Editor
{
    public class PoolAnalyzer : EditorWindow
    {
        private Vector2 _scrollPosition;
        private bool _showInactive = false;
        private bool _showPerformance = true;
        private bool _showMemory = true;
        private GUIStyle _headerStyle;

        [MenuItem("Tools/TByd Framework/Pool Analyzer")]
        public static void ShowWindow()
        {
            GetWindow<PoolAnalyzer>("Pool Analyzer");
        }

        private void OnEnable()
        {
            _headerStyle = new GUIStyle();
            _headerStyle.fontStyle = FontStyle.Bold;
            _headerStyle.fontSize = 12;
            _headerStyle.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
        }

        private void OnGUI()
        {
            DrawToolbar();
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            if (_showPerformance)
            {
                DrawPerformanceSection();
            }

            if (_showMemory)
            {
                DrawMemorySection();
            }

            DrawPoolList();

            EditorGUILayout.EndScrollView();
        }

        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            
            _showInactive = GUILayout.Toggle(_showInactive, "Show Inactive", EditorStyles.toolbarButton);
            _showPerformance = GUILayout.Toggle(_showPerformance, "Performance", EditorStyles.toolbarButton);
            _showMemory = GUILayout.Toggle(_showMemory, "Memory", EditorStyles.toolbarButton);
            
            GUILayout.FlexibleSpace();
            
            if (GUILayout.Button("Refresh", EditorStyles.toolbarButton))
            {
                Repaint();
            }
            
            EditorGUILayout.EndHorizontal();
        }

        private void DrawPerformanceSection()
        {
            EditorGUILayout.LabelField("Performance Analysis", _headerStyle);
            EditorGUILayout.Space();

            using (new EditorGUILayout.VerticalScope("box"))
            {
                // 这里添加性能分析数据的显示
                EditorGUILayout.LabelField("Average Get Time: 0.1ms");
                EditorGUILayout.LabelField("Average Return Time: 0.05ms");
                EditorGUILayout.LabelField("Pool Operations/sec: 100");
            }
        }

        private void DrawMemorySection()
        {
            EditorGUILayout.LabelField("Memory Usage", _headerStyle);
            EditorGUILayout.Space();

            using (new EditorGUILayout.VerticalScope("box"))
            {
                // 这里添加内存使用数据的显示
                EditorGUILayout.LabelField("Total Pool Memory: 10MB");
                EditorGUILayout.LabelField("Active Objects: 50");
                EditorGUILayout.LabelField("Pooled Objects: 100");
            }
        }

        private void DrawPoolList()
        {
            EditorGUILayout.LabelField("Active Pools", _headerStyle);
            EditorGUILayout.Space();

            var pools = PoolMonitor.GetActivePools();
            if (!pools.Any())
            {
                EditorGUILayout.HelpBox("No active pools found.", MessageType.Info);
                return;
            }

            foreach (var pool in pools)
            {
                DrawPoolEntry(pool);
            }
        }

        private void DrawPoolEntry(IPoolInfo pool)
        {
            using (new EditorGUILayout.VerticalScope("box"))
            {
                EditorGUILayout.LabelField($"Pool: {pool.Name}", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                
                EditorGUILayout.LabelField($"Type: {pool.Type}");
                EditorGUILayout.LabelField($"Available: {pool.Count}");
                EditorGUILayout.LabelField($"Active: {pool.ActiveCount}");
                EditorGUILayout.LabelField($"Max Size: {pool.MaxSize}");
                
                EditorGUI.indentLevel--;
            }
        }
    }
} 