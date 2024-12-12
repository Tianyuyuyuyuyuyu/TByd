using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Core;

namespace TBydFramework.Pool.Editor
{
    public class PoolPreviewWindow : EditorWindow
    {
        private Vector2 _scrollPosition;
        private GameObject _selectedPrefab;
        private int _previewCount = 5;
        private float _spacing = 2f;
        private List<GameObject> _previewInstances = new List<GameObject>();
        private PreviewRenderUtility _previewUtility;

        public static void ShowWindow(GameObject prefab = null)
        {
            var window = GetWindow<PoolPreviewWindow>("Pool Preview");
            window._selectedPrefab = prefab;
            window.Show();
        }

        private void OnEnable()
        {
            _previewUtility = new PreviewRenderUtility();
            _previewUtility.camera.transform.position = new Vector3(0, 5, -10);
            _previewUtility.camera.transform.rotation = Quaternion.Euler(30, 0, 0);
            _previewUtility.lights[0].transform.rotation = Quaternion.Euler(30, 30, 0);
            _previewUtility.ambientColor = new Color(.1f, .1f, .1f, 1f);
        }

        private void OnDisable()
        {
            ClearPreview();
            if (_previewUtility != null)
            {
                _previewUtility.Cleanup();
                _previewUtility = null;
            }
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            DrawToolbar();
            DrawSettings();
            DrawPreview();

            EditorGUILayout.EndVertical();
        }

        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            
            if (GUILayout.Button("Refresh", EditorStyles.toolbarButton))
            {
                UpdatePreview();
            }
            
            if (GUILayout.Button("Clear", EditorStyles.toolbarButton))
            {
                ClearPreview();
            }
            
            EditorGUILayout.EndHorizontal();
        }

        private void DrawSettings()
        {
            EditorGUILayout.Space();
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                _selectedPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab", _selectedPrefab, typeof(GameObject), false);
                _previewCount = EditorGUILayout.IntSlider("Preview Count", _previewCount, 1, 10);
                _spacing = EditorGUILayout.Slider("Spacing", _spacing, 0.5f, 5f);

                if (check.changed)
                {
                    UpdatePreview();
                }
            }
        }

        private void DrawPreview()
        {
            if (_previewUtility == null) return;

            var renderTexture = _previewUtility.camera.targetTexture;

            if (_selectedPrefab == null)
            {
                EditorGUILayout.HelpBox("Select a prefab to preview", MessageType.Info);
                return;
            }

            var previewRect = GUILayoutUtility.GetRect(position.width, 200);
            _previewUtility.BeginPreview(previewRect, GUIStyle.none);

            // 绘制网格
            Handles.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
            float gridSize = 1f;
            int gridLines = 10;
            for (int i = -gridLines; i <= gridLines; i++)
            {
                Handles.DrawLine(new Vector3(i * gridSize, 0, -gridLines * gridSize),
                               new Vector3(i * gridSize, 0, gridLines * gridSize));
                Handles.DrawLine(new Vector3(-gridLines * gridSize, 0, i * gridSize),
                               new Vector3(gridLines * gridSize, 0, i * gridSize));
            }

            // 更新相机
            if (Event.current.type == EventType.MouseDrag && Event.current.button == 1)
            {
                var delta = Event.current.delta;
                _previewUtility.camera.transform.RotateAround(Vector3.zero, Vector3.up, delta.x);
                _previewUtility.camera.transform.RotateAround(Vector3.zero, _previewUtility.camera.transform.right, -delta.y);
                Repaint();
            }

            _previewUtility.camera.Render();
            GUI.DrawTexture(previewRect, renderTexture);
            _previewUtility.EndPreview();

            // 处理鼠标滚轮缩放
            if (Event.current.type == EventType.ScrollWheel && previewRect.Contains(Event.current.mousePosition))
            {
                var zoom = Event.current.delta.y * 0.5f;
                var camTrans = _previewUtility.camera.transform;
                camTrans.position += camTrans.forward * zoom;
                Repaint();
                Event.current.Use();
            }
        }

        private void UpdatePreview()
        {
            ClearPreview();
            if (_selectedPrefab == null) return;

            float startX = -(_previewCount - 1) * _spacing * 0.5f;
            for (int i = 0; i < _previewCount; i++)
            {
                var instance = (GameObject)PrefabUtility.InstantiatePrefab(_selectedPrefab);
                instance.transform.position = new Vector3(startX + i * _spacing, 0, 0);
                _previewInstances.Add(instance);
            }
        }

        private void ClearPreview()
        {
            foreach (var instance in _previewInstances)
            {
                DestroyImmediate(instance);
            }
            _previewInstances.Clear();
        }
    }
} 