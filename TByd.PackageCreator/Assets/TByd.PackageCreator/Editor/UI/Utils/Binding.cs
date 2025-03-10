using System;
using System.Collections.Generic;

namespace TByd.PackageCreator.Editor.UI.Utils
{
    /// <summary>
    /// 数据绑定类，提供简单的数据绑定功能
    /// </summary>
    /// <typeparam name="T">绑定数据类型</typeparam>
    public class Binding<T>
    {
        private T _value;

        /// <summary>
        /// 绑定的值
        /// </summary>
        public T Value
        {
            get => _value;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    T oldValue = _value;
                    _value = value;
                    OnValueChanged?.Invoke(oldValue, value);
                }
            }
        }

        /// <summary>
        /// 值变更事件，参数为旧值和新值
        /// </summary>
        public event Action<T, T> OnValueChanged;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initialValue">初始值</param>
        public Binding(T initialValue = default)
        {
            _value = initialValue;
        }

        /// <summary>
        /// 绑定到另一个Binding对象
        /// </summary>
        /// <param name="target">目标Binding对象</param>
        /// <param name="twoWay">是否双向绑定</param>
        /// <returns>当前Binding对象，用于链式调用</returns>
        public Binding<T> BindTo(Binding<T> target, bool twoWay = false)
        {
            if (target == null)
                return this;

            // 将当前值同步到目标
            target.Value = this.Value;

            // 监听当前值变化，更新目标值
            this.OnValueChanged += (oldValue, newValue) => target.Value = newValue;

            // 如果是双向绑定，则也监听目标值变化
            if (twoWay)
            {
                target.OnValueChanged += (oldValue, newValue) => this.Value = newValue;
            }

            return this;
        }

        /// <summary>
        /// 将值转换为字符串
        /// </summary>
        /// <returns>值的字符串表示</returns>
        public override string ToString()
        {
            return _value?.ToString() ?? "null";
        }
    }

    /// <summary>
    /// 集合绑定类，提供对集合类型的数据绑定功能
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    public class CollectionBinding<T> : Binding<IList<T>>
    {
        /// <summary>
        /// 项目添加事件
        /// </summary>
        public event Action<T> OnItemAdded;

        /// <summary>
        /// 项目移除事件
        /// </summary>
        public event Action<T> OnItemRemoved;

        /// <summary>
        /// 集合清空事件
        /// </summary>
        public event Action OnCollectionCleared;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initialValue">初始集合</param>
        public CollectionBinding(IList<T> initialValue = null) : base(initialValue ?? new List<T>())
        {
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="item">要添加的项目</param>
        public void Add(T item)
        {
            Value.Add(item);
            OnItemAdded?.Invoke(item);
        }

        /// <summary>
        /// 移除项目
        /// </summary>
        /// <param name="item">要移除的项目</param>
        /// <returns>是否成功移除</returns>
        public bool Remove(T item)
        {
            bool result = Value.Remove(item);
            if (result)
            {
                OnItemRemoved?.Invoke(item);
            }
            return result;
        }

        /// <summary>
        /// 清空集合
        /// </summary>
        public void Clear()
        {
            Value.Clear();
            OnCollectionCleared?.Invoke();
        }

        /// <summary>
        /// 获取项目数量
        /// </summary>
        public int Count => Value.Count;

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>对应索引的项目</returns>
        public T this[int index]
        {
            get => Value[index];
            set => Value[index] = value;
        }
    }
}
