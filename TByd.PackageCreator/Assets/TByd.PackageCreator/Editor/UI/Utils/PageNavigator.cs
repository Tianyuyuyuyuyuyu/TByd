using System.Collections.Generic;

namespace TByd.PackageCreator.Editor.UI.Utils
{
    /// <summary>
    /// 页面接口，所有页面组件必须实现此接口
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// 页面标题
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 页面是否有效，用于确定是否可以进入下一页
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// 进入页面时调用
        /// </summary>
        void OnEnter();

        /// <summary>
        /// 离开页面时调用
        /// </summary>
        void OnExit();

        /// <summary>
        /// 绘制页面内容
        /// </summary>
        void Draw();
    }

    /// <summary>
    /// 页面导航器，管理页面切换和数据传递
    /// </summary>
    public class PageNavigator
    {
        private List<IPage> _pages = new List<IPage>();
        private int _currentPageIndex = 0;

        /// <summary>
        /// 当前页面
        /// </summary>
        public IPage CurrentPage => _pages.Count > 0 ? _pages[_currentPageIndex] : null;

        /// <summary>
        /// 当前页面索引
        /// </summary>
        public int CurrentPageIndex => _currentPageIndex;

        /// <summary>
        /// 是否可以前进到下一页
        /// </summary>
        public bool CanGoNext => _pages.Count > 0 && _currentPageIndex < _pages.Count - 1 && CurrentPage.IsValid;

        /// <summary>
        /// 是否可以返回上一页
        /// </summary>
        public bool CanGoPrevious => _pages.Count > 0 && _currentPageIndex > 0;

        /// <summary>
        /// 前进到下一页
        /// </summary>
        public void GoNext()
        {
            if (CanGoNext)
            {
                CurrentPage.OnExit();
                _currentPageIndex++;
                CurrentPage.OnEnter();
            }
        }

        /// <summary>
        /// 返回上一页
        /// </summary>
        public void GoPrevious()
        {
            if (CanGoPrevious)
            {
                CurrentPage.OnExit();
                _currentPageIndex--;
                CurrentPage.OnEnter();
            }
        }

        /// <summary>
        /// 跳转到指定页面
        /// </summary>
        /// <param name="index">页面索引</param>
        public void GoToPage(int index)
        {
            if (_pages.Count > 0 && index >= 0 && index < _pages.Count && index != _currentPageIndex)
            {
                CurrentPage?.OnExit();
                _currentPageIndex = index;
                CurrentPage?.OnEnter();
            }
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <param name="page">页面实例</param>
        public void AddPage(IPage page)
        {
            if (page != null)
            {
                _pages.Add(page);
            }
        }

        /// <summary>
        /// 清空所有页面
        /// </summary>
        public void ClearPages()
        {
            CurrentPage?.OnExit();
            _pages.Clear();
            _currentPageIndex = 0;
        }

        /// <summary>
        /// 获取页面总数
        /// </summary>
        public int PageCount => _pages.Count;

        /// <summary>
        /// 获取指定索引的页面
        /// </summary>
        /// <param name="index">页面索引</param>
        /// <returns>页面实例，如果索引无效则返回null</returns>
        public IPage GetPageAt(int index)
        {
            if (index >= 0 && index < _pages.Count)
            {
                return _pages[index];
            }
            return null;
        }
    }
}
