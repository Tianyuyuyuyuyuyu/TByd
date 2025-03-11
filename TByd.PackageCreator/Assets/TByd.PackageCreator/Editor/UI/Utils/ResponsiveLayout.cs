using UnityEditor;

namespace TByd.PackageCreator.Editor.UI.Utils
{
    /// <summary>
    /// 响应式布局工具类，提供适应不同窗口尺寸的布局功能
    /// </summary>
    public static class ResponsiveLayout
    {
        // 布局断点
        private const float SmallScreenWidth = 600f;
        private const float MediumScreenWidth = 900f;
        private const float LargeScreenWidth = 1200f;

        /// <summary>
        /// 屏幕尺寸类型
        /// </summary>
        public enum ScreenSize
        {
            Small,
            Medium,
            Large,
            ExtraLarge
        }

        /// <summary>
        /// 获取当前窗口的尺寸类型
        /// </summary>
        /// <param name="window">编辑器窗口</param>
        /// <returns>尺寸类型</returns>
        public static ScreenSize GetScreenSize(EditorWindow window)
        {
            if (window == null)
                return ScreenSize.Medium;

            float width = window.position.width;

            if (width < SmallScreenWidth)
                return ScreenSize.Small;
            else if (width < MediumScreenWidth)
                return ScreenSize.Medium;
            else if (width < LargeScreenWidth)
                return ScreenSize.Large;
            else
                return ScreenSize.ExtraLarge;
        }

        /// <summary>
        /// 获取基于屏幕尺寸的列数
        /// </summary>
        /// <param name="window">编辑器窗口</param>
        /// <returns>列数</returns>
        public static int GetColumnCount(EditorWindow window)
        {
            ScreenSize size = GetScreenSize(window);

            switch (size)
            {
                case ScreenSize.Small:
                    return 1;
                case ScreenSize.Medium:
                    return 2;
                case ScreenSize.Large:
                    return 3;
                case ScreenSize.ExtraLarge:
                    return 4;
                default:
                    return 2;
            }
        }

        /// <summary>
        /// 开始响应式网格布局
        /// </summary>
        /// <param name="window">编辑器窗口</param>
        /// <param name="itemCount">项目总数</param>
        /// <param name="currentIndex">当前索引</param>
        /// <returns>是否应该开始新行</returns>
        public static bool BeginResponsiveGrid(EditorWindow window, int itemCount, int currentIndex)
        {
            int columnCount = GetColumnCount(window);
            return currentIndex % columnCount == 0;
        }

        /// <summary>
        /// 结束响应式网格布局
        /// </summary>
        /// <param name="window">编辑器窗口</param>
        /// <param name="itemCount">项目总数</param>
        /// <param name="currentIndex">当前索引</param>
        /// <returns>是否应该结束当前行</returns>
        public static bool EndResponsiveGrid(EditorWindow window, int itemCount, int currentIndex)
        {
            int columnCount = GetColumnCount(window);
            return (currentIndex + 1) % columnCount == 0 || currentIndex == itemCount - 1;
        }

        /// <summary>
        /// 获取基于屏幕尺寸的项目宽度
        /// </summary>
        /// <param name="window">编辑器窗口</param>
        /// <param name="spacing">项目间距</param>
        /// <returns>项目宽度</returns>
        public static float GetItemWidth(EditorWindow window, float spacing = 10f)
        {
            if (window == null)
                return 200f;

            int columnCount = GetColumnCount(window);
            float availableWidth = window.position.width - (columnCount + 1) * spacing;
            return availableWidth / columnCount;
        }

        /// <summary>
        /// 开始响应式水平布局
        /// </summary>
        /// <param name="screenSize">屏幕尺寸类型</param>
        /// <returns>是否应该使用水平布局</returns>
        public static bool BeginResponsiveHorizontal(ScreenSize screenSize)
        {
            bool useHorizontal = screenSize >= ScreenSize.Medium;

            if (useHorizontal)
            {
                EditorGUILayout.BeginHorizontal();
            }

            return useHorizontal;
        }

        /// <summary>
        /// 结束响应式水平布局
        /// </summary>
        /// <param name="useHorizontal">是否使用了水平布局</param>
        public static void EndResponsiveHorizontal(bool useHorizontal)
        {
            if (useHorizontal)
            {
                EditorGUILayout.EndHorizontal();
            }
        }

        /// <summary>
        /// 获取基于屏幕尺寸的标签宽度
        /// </summary>
        /// <param name="screenSize">屏幕尺寸类型</param>
        /// <returns>标签宽度</returns>
        public static float GetLabelWidth(ScreenSize screenSize)
        {
            switch (screenSize)
            {
                case ScreenSize.Small:
                    return 100f;
                case ScreenSize.Medium:
                    return 150f;
                case ScreenSize.Large:
                case ScreenSize.ExtraLarge:
                    return 200f;
                default:
                    return 150f;
            }
        }

        /// <summary>
        /// 获取基于屏幕尺寸的字体大小
        /// </summary>
        /// <param name="screenSize">屏幕尺寸类型</param>
        /// <param name="baseSize">基础字体大小</param>
        /// <returns>字体大小</returns>
        public static int GetFontSize(ScreenSize screenSize, int baseSize = 12)
        {
            switch (screenSize)
            {
                case ScreenSize.Small:
                    return baseSize;
                case ScreenSize.Medium:
                    return baseSize + 1;
                case ScreenSize.Large:
                    return baseSize + 2;
                case ScreenSize.ExtraLarge:
                    return baseSize + 3;
                default:
                    return baseSize;
            }
        }
    }
}
