using System.Collections.Generic;
using UnityEngine;

namespace TByd.Core.Utils.Runtime.Extensions
{
    /// <summary>
    /// Transform组件的扩展方法集合
    /// </summary>
    /// <remarks>
    /// 这个类提供了一系列实用的Transform扩展方法，简化了常见的Transform操作，
    /// 如重置变换、单独修改坐标分量、子物体管理等。
    /// 
    /// 所有方法均采用链式设计，允许连续调用多个方法。
    /// </remarks>
    public static class TransformExtensions
    {
        /// <summary>
        /// 重置Transform的本地位置、旋转和缩放
        /// </summary>
        /// <param name="transform">要重置的Transform</param>
        /// <returns>重置后的Transform引用，用于链式调用</returns>
        /// <remarks>
        /// 此方法将Transform的本地位置设为Vector3.zero，
        /// 本地旋转设为Quaternion.identity，
        /// 本地缩放设为Vector3.one。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 重置Transform并进行其他操作
        /// transform.ResetLocal().SetLocalX(5f);
        /// </code>
        /// </remarks>
        public static Transform ResetLocal(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            return transform;
        }

        /// <summary>
        /// 设置本地位置的x坐标，保持y和z不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="x">新的x坐标值</param>
        /// <returns>修改后的Transform引用，用于链式调用</returns>
        /// <remarks>
        /// 此方法只修改Transform的本地位置的x分量，保持y和z分量不变。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 只修改x坐标
        /// transform.SetLocalX(10f);
        /// 
        /// // 链式调用设置x和y
        /// transform.SetLocalX(10f).SetLocalY(5f);
        /// </code>
        /// </remarks>
        public static Transform SetLocalX(this Transform transform, float x)
        {
            var position = transform.localPosition;
            position.x = x;
            transform.localPosition = position;
            return transform;
        }

        /// <summary>
        /// 设置本地位置的y坐标，保持x和z不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="y">新的y坐标值</param>
        /// <returns>修改后的Transform引用，用于链式调用</returns>
        /// <remarks>
        /// 此方法只修改Transform的本地位置的y分量，保持x和z分量不变。
        /// </remarks>
        public static Transform SetLocalY(this Transform transform, float y)
        {
            var position = transform.localPosition;
            position.y = y;
            transform.localPosition = position;
            return transform;
        }

        /// <summary>
        /// 设置本地位置的z坐标，保持x和y不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="z">新的z坐标值</param>
        /// <returns>修改后的Transform引用，用于链式调用</returns>
        /// <remarks>
        /// 此方法只修改Transform的本地位置的z分量，保持x和y分量不变。
        /// </remarks>
        public static Transform SetLocalZ(this Transform transform, float z)
        {
            var position = transform.localPosition;
            position.z = z;
            transform.localPosition = position;
            return transform;
        }

        /// <summary>
        /// 设置世界位置的x坐标，保持y和z不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="x">新的x坐标值</param>
        /// <returns>修改后的Transform引用，用于链式调用</returns>
        /// <remarks>
        /// 此方法只修改Transform的世界位置的x分量，保持y和z分量不变。
        /// 
        /// <para>注意：</para>
        /// 与SetLocalX不同，此方法修改的是世界坐标系中的位置。
        /// </remarks>
        public static Transform SetX(this Transform transform, float x)
        {
            var position = transform.position;
            position.x = x;
            transform.position = position;
            return transform;
        }

        /// <summary>
        /// 设置世界位置的y坐标，保持x和z不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="y">新的y坐标值</param>
        /// <returns>修改后的Transform引用，用于链式调用</returns>
        /// <remarks>
        /// 此方法只修改Transform的世界位置的y分量，保持x和z分量不变。
        /// </remarks>
        public static Transform SetY(this Transform transform, float y)
        {
            var position = transform.position;
            position.y = y;
            transform.position = position;
            return transform;
        }

        /// <summary>
        /// 设置世界位置的z坐标，保持x和y不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="z">新的z坐标值</param>
        /// <returns>修改后的Transform引用，用于链式调用</returns>
        /// <remarks>
        /// 此方法只修改Transform的世界位置的z分量，保持x和y分量不变。
        /// </remarks>
        public static Transform SetZ(this Transform transform, float z)
        {
            var position = transform.position;
            position.z = z;
            transform.position = position;
            return transform;
        }

        /// <summary>
        /// 获取所有子物体
        /// </summary>
        /// <param name="transform">父Transform</param>
        /// <param name="includeInactive">是否包含非激活的子物体，默认为true</param>
        /// <returns>子物体列表</returns>
        /// <remarks>
        /// 此方法返回父物体的所有直接子物体。如果includeInactive为false，
        /// 则只返回激活状态的子物体。
        /// 
        /// <para>注意：</para>
        /// 1. 此方法仅返回直接子物体，不包括孙子物体。
        /// 2. 返回的是新创建的列表，可以安全地修改。
        /// 3. 当父Transform为null时，返回空列表。
        /// 
        /// <para>性能考虑：</para>
        /// 此方法创建新的List&lt;Transform&gt;实例，如果在频繁调用的代码中使用，
        /// 请考虑缓存结果或使用对象池来减少GC压力。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 获取所有子物体，包括未激活的
        /// var allChildren = transform.GetAllChildren();
        /// 
        /// // 只获取激活的子物体
        /// var activeChildren = transform.GetAllChildren(includeInactive: false);
        /// </code>
        /// </remarks>
        public static List<Transform> GetAllChildren(this Transform transform, bool includeInactive = true)
        {
            if (transform == null)
                return new List<Transform>();
                
            var children = new List<Transform>(transform.childCount);
            
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                
                // 确保子对象及其gameObject非空
                if (child != null && child.gameObject != null)
                {
                    // 根据激活状态过滤
                    if (includeInactive || child.gameObject.activeSelf)
                    {
                        children.Add(child);
                    }
                }
            }
            
            return children;
        }

        /// <summary>
        /// 销毁所有子物体
        /// </summary>
        /// <param name="transform">父Transform</param>
        /// <param name="immediate">是否立即销毁，默认为false</param>
        /// <returns>原始Transform引用，用于链式调用</returns>
        /// <remarks>
        /// 此方法销毁指定Transform的所有直接子物体。
        /// 
        /// <para>当immediate为true时，使用DestroyImmediate立即销毁；
        /// 当为false时，使用Destroy在帧结束时销毁。</para>
        /// 
        /// <para>注意：</para>
        /// 1. DestroyImmediate通常只应在编辑器模式下使用。
        /// 2. 在游戏运行时，除非有特殊需求，否则应使用默认的Destroy。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 在帧结束时销毁所有子物体
        /// transform.DestroyAllChildren();
        /// 
        /// // 立即销毁所有子物体 (编辑器模式下)
        /// transform.DestroyAllChildren(immediate: true);
        /// </code>
        /// </remarks>
        public static Transform DestroyAllChildren(this Transform transform, bool immediate = false)
        {
            // 从后向前遍历，避免索引变化问题
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                if (immediate)
                {
                    Object.DestroyImmediate(transform.GetChild(i).gameObject);
                }
                else
                {
                    Object.Destroy(transform.GetChild(i).gameObject);
                }
            }
            
            return transform;
        }

        /// <summary>
        /// 查找子物体，如果不存在则创建
        /// </summary>
        /// <param name="transform">父Transform</param>
        /// <param name="name">子物体名称</param>
        /// <returns>找到或创建的子物体</returns>
        /// <remarks>
        /// 此方法首先尝试查找指定名称的直接子物体。如果找不到，将创建一个新的空GameObject并设置为子物体。
        /// 
        /// <para>新创建的子物体：</para>
        /// 1. 位置、旋转和缩放均被重置（本地位置为零点，本地旋转为单位四元数，本地缩放为一）
        /// 2. 层级继承自父物体
        /// 
        /// <para>常见用途：</para>
        /// 此方法对于UI系统、场景层次结构管理和预制体扩展特别有用。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 确保"UIContainer"子物体存在
        /// Transform uiContainer = transform.FindOrCreateChild("UIContainer");
        /// 
        /// // 链式调用，创建嵌套结构
        /// Transform nestedChild = transform
        ///     .FindOrCreateChild("Parent")
        ///     .FindOrCreateChild("Child")
        ///     .FindOrCreateChild("GrandChild");
        /// </code>
        /// </remarks>
        public static Transform FindOrCreateChild(this Transform transform, string name)
        {
            var child = transform.Find(name);
            
            if (child == null)
            {
                var childObject = new GameObject(name);
                child = childObject.transform;
                child.SetParent(transform, false);
            }
            
            return child;
        }

        /// <summary>
        /// 递归查找具有指定名称的子物体
        /// </summary>
        /// <param name="transform">父Transform</param>
        /// <param name="name">要查找的子物体名称</param>
        /// <returns>找到的子物体，如果未找到则返回null</returns>
        /// <remarks>
        /// 此方法执行深度优先搜索，查找层次结构中的子物体，不限于直接子物体。
        /// 
        /// <para>注意：</para>
        /// 1. 如果有多个同名子物体，将返回找到的第一个。
        /// 2. 搜索包括所有层级的子物体（子物体的子物体等）。
        /// 3. 搜索包括非激活的子物体。
        /// 
        /// <para>性能考虑：</para>
        /// 由于递归特性，此方法在大型层次结构中可能较慢。
        /// 对于性能关键型代码，考虑缓存结果或使用其他查找方法。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 在整个层次结构中查找名为"Player"的子物体
        /// Transform player = rootTransform.FindRecursive("Player");
        /// 
        /// if (player != null) {
        ///     // 找到了Player
        /// } else {
        ///     // 未找到Player
        /// }
        /// </code>
        /// </remarks>
        public static Transform FindRecursive(this Transform transform, string name)
        {
            // 首先在直接子物体中查找
            var child = transform.Find(name);
            if (child != null)
                return child;
            
            // 递归查找所有子物体
            for (var i = 0; i < transform.childCount; i++)
            {
                child = transform.GetChild(i).FindRecursive(name);
                if (child != null)
                    return child;
            }
            
            return null;
        }
    }
} 