using System.Collections.Generic;
using UnityEngine;

namespace TByd.Core.Utils.Runtime.Extensions
{
    /// <summary>
    /// Transform组件的扩展方法
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// 重置Transform的本地位置、旋转和缩放
        /// </summary>
        /// <param name="transform">要重置的Transform</param>
        public static void ResetLocal(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// 设置本地位置的x坐标，保持y和z不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="x">新的x坐标值</param>
        public static void SetLocalX(this Transform transform, float x)
        {
            Vector3 position = transform.localPosition;
            position.x = x;
            transform.localPosition = position;
        }

        /// <summary>
        /// 设置本地位置的y坐标，保持x和z不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="y">新的y坐标值</param>
        public static void SetLocalY(this Transform transform, float y)
        {
            Vector3 position = transform.localPosition;
            position.y = y;
            transform.localPosition = position;
        }

        /// <summary>
        /// 设置本地位置的z坐标，保持x和y不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="z">新的z坐标值</param>
        public static void SetLocalZ(this Transform transform, float z)
        {
            Vector3 position = transform.localPosition;
            position.z = z;
            transform.localPosition = position;
        }

        /// <summary>
        /// 设置世界位置的x坐标，保持y和z不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="x">新的x坐标值</param>
        public static void SetX(this Transform transform, float x)
        {
            Vector3 position = transform.position;
            position.x = x;
            transform.position = position;
        }

        /// <summary>
        /// 设置世界位置的y坐标，保持x和z不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="y">新的y坐标值</param>
        public static void SetY(this Transform transform, float y)
        {
            Vector3 position = transform.position;
            position.y = y;
            transform.position = position;
        }

        /// <summary>
        /// 设置世界位置的z坐标，保持x和y不变
        /// </summary>
        /// <param name="transform">要修改的Transform</param>
        /// <param name="z">新的z坐标值</param>
        public static void SetZ(this Transform transform, float z)
        {
            Vector3 position = transform.position;
            position.z = z;
            transform.position = position;
        }

        /// <summary>
        /// 获取所有子物体
        /// </summary>
        /// <param name="transform">父Transform</param>
        /// <param name="includeInactive">是否包含非激活的子物体</param>
        /// <returns>子物体列表</returns>
        public static List<Transform> GetAllChildren(this Transform transform, bool includeInactive = true)
        {
            if (transform == null)
                return new List<Transform>();
                
            List<Transform> children = new List<Transform>(transform.childCount);
            
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                
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
        /// <param name="immediate">是否立即销毁</param>
        public static void DestroyAllChildren(this Transform transform, bool immediate = false)
        {
            // 从后向前遍历，避免索引变化问题
            for (int i = transform.childCount - 1; i >= 0; i--)
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
        }

        /// <summary>
        /// 查找子物体，如果不存在则创建
        /// </summary>
        /// <param name="transform">父Transform</param>
        /// <param name="name">子物体名称</param>
        /// <returns>找到或创建的子物体</returns>
        public static Transform FindOrCreateChild(this Transform transform, string name)
        {
            Transform child = transform.Find(name);
            
            if (child == null)
            {
                GameObject childObject = new GameObject(name);
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
        public static Transform FindRecursive(this Transform transform, string name)
        {
            // 首先在直接子物体中查找
            Transform child = transform.Find(name);
            if (child != null)
                return child;
            
            // 递归查找所有子物体
            for (int i = 0; i < transform.childCount; i++)
            {
                child = transform.GetChild(i).FindRecursive(name);
                if (child != null)
                    return child;
            }
            
            return null;
        }
    }
} 