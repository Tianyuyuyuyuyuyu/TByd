using TByd.PackageCreator.Editor.UI.Styles;
using UnityEditor;
using UnityEngine;

namespace TByd.PackageCreator.Editor.UI.Controls
{
    /// <summary>
    /// 进度指示器控件，用于显示操作进度和状态
    /// </summary>
    public static class ProgressIndicatorControl
    {
        /// <summary>
        /// 绘制简单进度条
        /// </summary>
        /// <param name="progress">进度值（0-1）</param>
        /// <param name="width">宽度（可选）</param>
        public static void DrawProgressBar(float progress, float width = 0)
        {
            progress = Mathf.Clamp01(progress);

            Rect rect = GUILayoutUtility.GetRect(width > 0 ? width : EditorGUIUtility.currentViewWidth - 40, 20);
            EditorGUI.ProgressBar(rect, progress, $"{progress * 100:F0}%");
        }

        /// <summary>
        /// 绘制带状态文本的进度条
        /// </summary>
        /// <param name="progress">进度值（0-1）</param>
        /// <param name="status">状态文本</param>
        /// <param name="width">宽度（可选）</param>
        public static void DrawProgressBarWithStatus(float progress, string status, float width = 0)
        {
            progress = Mathf.Clamp01(progress);

            EditorGUILayout.BeginVertical();

            if (!string.IsNullOrEmpty(status))
            {
                EditorGUILayout.LabelField(status);
            }

            Rect rect = GUILayoutUtility.GetRect(width > 0 ? width : EditorGUIUtility.currentViewWidth - 40, 20);
            EditorGUI.ProgressBar(rect, progress, $"{progress * 100:F0}%");

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 绘制完整的进度指示器，包括标题、状态和进度条
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="status">状态文本</param>
        /// <param name="progress">进度值（0-1）</param>
        /// <param name="showCancelButton">是否显示取消按钮</param>
        /// <param name="width">宽度（可选）</param>
        /// <returns>如果按下取消按钮则返回true</returns>
        public static bool DrawFullProgressIndicator(string title, string status, float progress, bool showCancelButton = false, float width = 0)
        {
            bool cancelled = false;
            progress = Mathf.Clamp01(progress);

            EditorGUILayout.BeginVertical(PackageCreatorStyles.BoxStyle);

            // 标题和取消按钮
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel);

            if (showCancelButton)
            {
                GUILayout.FlexibleSpace();
                cancelled = GUILayout.Button("取消", GUILayout.Width(60));
            }

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5);

            // 状态文本
            if (!string.IsNullOrEmpty(status))
            {
                EditorGUILayout.LabelField(status);
            }

            // 进度条
            Rect rect = GUILayoutUtility.GetRect(width > 0 ? width : EditorGUIUtility.currentViewWidth - 60, 20);
            EditorGUI.ProgressBar(rect, progress, $"{progress * 100:F0}%");

            EditorGUILayout.EndVertical();

            return cancelled;
        }

        /// <summary>
        /// 绘制包含步骤的进度指示器
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="steps">步骤数组</param>
        /// <param name="currentStep">当前步骤索引</param>
        /// <param name="progress">当前步骤的进度（0-1）</param>
        /// <param name="status">状态文本</param>
        /// <param name="showCancelButton">是否显示取消按钮</param>
        /// <returns>如果按下取消按钮则返回true</returns>
        public static bool DrawSteppedProgressIndicator(string title, string[] steps, int currentStep, float progress, string status, bool showCancelButton = false)
        {
            if (steps == null || steps.Length == 0)
                return false;

            bool cancelled = false;
            progress = Mathf.Clamp01(progress);
            currentStep = Mathf.Clamp(currentStep, 0, steps.Length - 1);

            // 计算总体进度
            float totalProgress = ((float)currentStep + progress) / steps.Length;

            EditorGUILayout.BeginVertical(PackageCreatorStyles.BoxStyle);

            // 标题和取消按钮
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel);

            if (showCancelButton)
            {
                GUILayout.FlexibleSpace();
                cancelled = GUILayout.Button("取消", GUILayout.Width(60));
            }

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5);

            // 显示步骤
            EditorGUILayout.LabelField($"步骤 {currentStep + 1}/{steps.Length}: {steps[currentStep]}");

            // 状态文本
            if (!string.IsNullOrEmpty(status))
            {
                EditorGUILayout.LabelField(status, EditorStyles.miniLabel);
            }

            // 进度条
            Rect rect = GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth - 60, 20);
            EditorGUI.ProgressBar(rect, totalProgress, $"{totalProgress * 100:F0}%");

            // 显示所有步骤
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            for (int i = 0; i < steps.Length; i++)
            {
                GUIStyle style = new GUIStyle(EditorStyles.label);

                if (i < currentStep)
                {
                    // 已完成的步骤
                    style.normal.textColor = Color.green;
                    EditorGUILayout.LabelField($"✓ {steps[i]}", style);
                }
                else if (i == currentStep)
                {
                    // 当前步骤
                    style.fontStyle = FontStyle.Bold;
                    EditorGUILayout.LabelField($"▶ {steps[i]}", style);
                }
                else
                {
                    // 待完成的步骤
                    style.normal.textColor = Color.gray;
                    EditorGUILayout.LabelField($"○ {steps[i]}", style);
                }
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();

            return cancelled;
        }

        /// <summary>
        /// 绘制旋转加载指示器
        /// </summary>
        /// <param name="text">显示文本</param>
        /// <param name="size">大小</param>
        public static void DrawLoadingSpinner(string text = null, float size = 30f)
        {
            EditorGUILayout.BeginHorizontal();

            // 计算动画旋转
            float time = (float)(EditorApplication.timeSinceStartup * 3.0) % 1.0f;
            float angle = time * 360f;

            // 创建旋转的纹理
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.white);
            texture.Apply();

            GUIStyle style = new GUIStyle();
            style.normal.background = texture;

            // 创建旋转的矩形
            Rect rect = GUILayoutUtility.GetRect(size, size);
            Matrix4x4 matrix = GUI.matrix;

            // 设置旋转中心
            GUIUtility.RotateAroundPivot(angle, new Vector2(rect.x + size / 2, rect.y + size / 2));

            // 绘制旋转的线条
            Color originalColor = GUI.color;
            GUI.color = new Color(0.3f, 0.3f, 0.8f, 0.8f);

            // 绘制多条线条形成转圈效果
            for (int i = 0; i < 8; i++)
            {
                float alpha = 0.2f + (i / 7f) * 0.8f;
                GUI.color = new Color(0.3f, 0.3f, 0.8f, alpha);

                float lineWidth = 2f + (i / 7f) * 4f;
                float offset = size / 2f - lineWidth / 2f;

                GUI.Box(new Rect(rect.x + offset, rect.y + 2, lineWidth, size - 4), GUIContent.none, style);
                GUIUtility.RotateAroundPivot(45, new Vector2(rect.x + size / 2, rect.y + size / 2));
            }

            // 恢复设置
            GUI.matrix = matrix;
            GUI.color = originalColor;

            if (!string.IsNullOrEmpty(text))
            {
                GUILayout.Space(5);
                EditorGUILayout.LabelField(text, GUILayout.Height(size));
            }

            EditorGUILayout.EndHorizontal();

            // 请求重绘以保持动画
            EditorUtility.SetDirty(Selection.activeObject);
        }
    }
}
