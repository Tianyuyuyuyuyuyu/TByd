using TByd.PackageCreator.Editor.Core.Models;
using TByd.PackageCreator.Editor.UI.Styles;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Controls
{
    /// <summary>
    /// 依赖项控件，用于显示和编辑单个包依赖
    /// </summary>
    public class DependencyItem
    {
        // 依赖项数据
        private PackageDependency _dependency;

        // 是否处于编辑模式
        private bool _isEditing;

        // 是否悬停在控件上
        private bool _isHovering;

        // 临时编辑字段
        private string _editId;
        private string _editVersion;

        // 删除按钮回调
        private System.Action<PackageDependency> _onDeleteCallback;

        // 更新按钮回调
        private System.Action<PackageDependency, string, string> _onUpdateCallback;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dependency">依赖项数据</param>
        /// <param name="onDeleteCallback">删除回调</param>
        /// <param name="onUpdateCallback">更新回调</param>
        public DependencyItem(
            PackageDependency dependency,
            System.Action<PackageDependency> onDeleteCallback,
            System.Action<PackageDependency, string, string> onUpdateCallback)
        {
            _dependency = dependency;
            _onDeleteCallback = onDeleteCallback;
            _onUpdateCallback = onUpdateCallback;
            _editId = dependency.Id;
            _editVersion = dependency.Version;
        }

        /// <summary>
        /// 绘制依赖项控件
        /// </summary>
        /// <returns>是否需要重绘</returns>
        public bool Draw()
        {
            bool needsRepaint = false;

            // 开始水平布局
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

            Rect itemRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true), GUILayout.Height(40));

            // 检测鼠标悬停
            if (itemRect.Contains(Event.current.mousePosition))
            {
                if (!_isHovering)
                {
                    _isHovering = true;
                    needsRepaint = true;
                }
            }
            else if (_isHovering)
            {
                _isHovering = false;
                needsRepaint = true;
            }

            if (_isEditing)
            {
                // 编辑模式下的布局
                EditorGUILayout.BeginVertical();

                // 包ID输入
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("包ID:", PackageCreatorStyles.FieldLabel, GUILayout.Width(80));
                string newId = EditorGUILayout.TextField(_editId);
                if (newId != _editId)
                {
                    _editId = newId;
                    needsRepaint = true;
                }
                EditorGUILayout.EndHorizontal();

                // 版本输入
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("版本:", PackageCreatorStyles.FieldLabel, GUILayout.Width(80));
                string newVersion = EditorGUILayout.TextField(_editVersion);
                if (newVersion != _editVersion)
                {
                    _editVersion = newVersion;
                    needsRepaint = true;
                }
                EditorGUILayout.EndHorizontal();

                // 保存和取消按钮
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                // 保存按钮
                if (GUILayout.Button("保存", GUILayout.Width(60)))
                {
                    _isEditing = false;
                    _onUpdateCallback?.Invoke(_dependency, _editId, _editVersion);
                    needsRepaint = true;
                }

                // 取消按钮
                if (GUILayout.Button("取消", GUILayout.Width(60)))
                {
                    _isEditing = false;
                    _editId = _dependency.Id;
                    _editVersion = _dependency.Version;
                    needsRepaint = true;
                }

                EditorGUILayout.EndHorizontal();

                EditorGUILayout.EndVertical();
            }
            else
            {
                // 显示模式下的布局
                EditorGUILayout.BeginVertical();

                // 依赖项信息显示
                EditorGUILayout.BeginHorizontal();

                // 包ID显示
                EditorGUILayout.LabelField(_dependency.Id, EditorStyles.boldLabel);

                GUILayout.FlexibleSpace();

                // 版本显示
                EditorGUILayout.LabelField(_dependency.Version, GUILayout.Width(80));

                // 编辑按钮
                if (_isHovering || Event.current.type == EventType.Repaint)
                {
                    if (GUILayout.Button("编辑", GUILayout.Width(60)))
                    {
                        _isEditing = true;
                        _editId = _dependency.Id;
                        _editVersion = _dependency.Version;
                        needsRepaint = true;
                    }

                    // 删除按钮
                    if (GUILayout.Button("删除", GUILayout.Width(60)))
                    {
                        _onDeleteCallback?.Invoke(_dependency);
                        needsRepaint = true;
                    }
                }

                EditorGUILayout.EndHorizontal();

                // 显示描述性提示（如果版本规范有问题）
                bool isValidVersion = IsValidVersionExpression(_dependency.Version);
                if (!isValidVersion)
                {
                    EditorGUILayout.HelpBox("版本表达式格式不正确。推荐格式: 1.0.0, >=1.0.0, 1.0.x 等", MessageType.Warning);
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();

            return needsRepaint;
        }

        /// <summary>
        /// 验证版本表达式
        /// </summary>
        /// <param name="version">版本表达式</param>
        /// <returns>是否有效</returns>
        private bool IsValidVersionExpression(string version)
        {
            if (string.IsNullOrEmpty(version))
                return false;

            // 检查最基本的版本格式 (如 1.0.0)
            if (System.Text.RegularExpressions.Regex.IsMatch(version, @"^\d+\.\d+\.\d+$"))
                return true;

            // 检查版本范围表达式 (如 >=1.0.0)
            if (System.Text.RegularExpressions.Regex.IsMatch(version, @"^[><]=?\d+\.\d+\.\d+$"))
                return true;

            // 检查通配符版本 (如 1.0.x 或 1.x.x)
            if (System.Text.RegularExpressions.Regex.IsMatch(version, @"^\d+(\.\d+|\.\*|\.[xX])(\.\d+|\.\*|\.[xX])?$"))
                return true;

            // 检查版本区间 (如 >=1.0.0 <2.0.0)
            if (System.Text.RegularExpressions.Regex.IsMatch(version, @"^[><]=?\d+\.\d+\.\d+\s+[><]=?\d+\.\d+\.\d+$"))
                return true;

            // 检查精确匹配 (使用 Unity 特有的以 com.xxx.yyy@version 格式的依赖写法)
            if (System.Text.RegularExpressions.Regex.IsMatch(version, @"^[\w\.-]+@\d+\.\d+\.\d+$"))
                return true;

            return false;
        }
    }
}
