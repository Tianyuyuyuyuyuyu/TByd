using System;
using UnityEngine;

namespace TByd.Core.Utils.Runtime
{
    /// <summary>
    /// 提供扩展的数学和几何运算工具
    /// </summary>
    /// <remarks>
    /// MathUtils类包含一系列数学和几何计算工具，这些工具扩展了Unity默认的数学库功能。
    /// 主要功能包括平滑阻尼插值、值域重映射、方向向量转旋转和多边形碰撞检测等。
    /// 
    /// 所有方法均经过性能优化，适合在性能敏感场景中使用。
    /// </remarks>
    public static class MathUtils
    {
        /// <summary>
        /// 平滑阻尼插值，适用于相机跟随等场景
        /// </summary>
        /// <param name="current">当前值</param>
        /// <param name="target">目标值</param>
        /// <param name="velocity">当前速度，会被修改</param>
        /// <param name="smoothTime">平滑时间，值越小变化越快</param>
        /// <param name="maxSpeed">最大速度，默认无限制</param>
        /// <param name="deltaTime">时间增量，默认为Time.deltaTime</param>
        /// <returns>插值后的值</returns>
        /// <remarks>
        /// 这个方法实现了类似弹簧阻尼系统的平滑插值，比简单的线性插值具有更自然的效果。
        /// 通过调整smoothTime和maxSpeed参数，可以控制移动的速度和平滑程度。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// private float _velocity = 0f;
        /// void Update() {
        ///     currentValue = MathUtils.SmoothDamp(currentValue, targetValue, ref _velocity, 0.3f);
        /// }
        /// </code>
        /// </remarks>
        public static float SmoothDamp(float current, float target, ref float velocity, float smoothTime, float maxSpeed = Mathf.Infinity, float deltaTime = -1f)
        {
            if (deltaTime < 0f)
                deltaTime = Time.deltaTime;
            
            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float omega = 2f / smoothTime;

            float x = omega * deltaTime;
            float exp = 1f / (1f + x + 0.48f * x * x + 0.235f * x * x * x);
            
            float change = current - target;
            float originalTo = target;
            
            // 限制最大速度
            float maxChange = maxSpeed * smoothTime;
            change = Mathf.Clamp(change, -maxChange, maxChange);
            target = current - change;
            
            float temp = (velocity + omega * change) * deltaTime;
            velocity = (velocity - omega * temp) * exp;
            float output = target + (change + temp) * exp;
            
            // 防止过冲
            if (originalTo - current > 0f == output > originalTo)
            {
                output = originalTo;
                velocity = (output - originalTo) / deltaTime;
            }
            
            return output;
        }

        /// <summary>
        /// 平滑阻尼插值，适用于相机跟随等场景（Vector2版本）
        /// </summary>
        /// <param name="current">当前值</param>
        /// <param name="target">目标值</param>
        /// <param name="velocity">当前速度，会被修改</param>
        /// <param name="smoothTime">平滑时间，值越小变化越快</param>
        /// <param name="maxSpeed">最大速度，默认无限制</param>
        /// <param name="deltaTime">时间增量，默认为Time.deltaTime</param>
        /// <returns>插值后的Vector2值</returns>
        /// <remarks>
        /// Vector2版本的平滑阻尼插值，对x和y分量分别进行计算。
        /// 适用于2D游戏中的平滑移动和跟随效果。
        /// </remarks>
        public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 velocity, float smoothTime, float maxSpeed = Mathf.Infinity, float deltaTime = -1f)
        {
            if (deltaTime < 0f)
                deltaTime = Time.deltaTime;

            float vx = velocity.x;
            float vy = velocity.y;

            Vector2 result = new Vector2(
                SmoothDamp(current.x, target.x, ref vx, smoothTime, maxSpeed, deltaTime),
                SmoothDamp(current.y, target.y, ref vy, smoothTime, maxSpeed, deltaTime)
            );

            velocity = new Vector2(vx, vy);
            return result;
        }

        /// <summary>
        /// 平滑阻尼插值，适用于相机跟随等场景（Vector3版本）
        /// </summary>
        /// <param name="current">当前值</param>
        /// <param name="target">目标值</param>
        /// <param name="velocity">当前速度，会被修改</param>
        /// <param name="smoothTime">平滑时间，值越小变化越快</param>
        /// <param name="maxSpeed">最大速度，默认无限制</param>
        /// <param name="deltaTime">时间增量，默认为Time.deltaTime</param>
        /// <returns>插值后的Vector3值</returns>
        /// <remarks>
        /// Vector3版本的平滑阻尼插值，对x、y和z分量分别进行计算。
        /// 适用于3D场景中的平滑移动、相机跟随和物体追踪。
        /// 
        /// <para>示例（相机跟随）：</para>
        /// <code>
        /// private Vector3 _velocity = Vector3.zero;
        /// void LateUpdate() {
        ///     transform.position = MathUtils.SmoothDamp(
        ///         transform.position, 
        ///         target.position, 
        ///         ref _velocity, 
        ///         smoothTime, 
        ///         maxSpeed);
        /// }
        /// </code>
        /// </remarks>
        public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 velocity, float smoothTime, float maxSpeed = Mathf.Infinity, float deltaTime = -1f)
        {
            if (deltaTime < 0f)
                deltaTime = Time.deltaTime;

            float vx = velocity.x;
            float vy = velocity.y;
            float vz = velocity.z;

            Vector3 result = new Vector3(
                SmoothDamp(current.x, target.x, ref vx, smoothTime, maxSpeed, deltaTime),
                SmoothDamp(current.y, target.y, ref vy, smoothTime, maxSpeed, deltaTime),
                SmoothDamp(current.z, target.z, ref vz, smoothTime, maxSpeed, deltaTime)
            );

            velocity = new Vector3(vx, vy, vz);
            return result;
        }

        /// <summary>
        /// 将值重映射到新范围
        /// </summary>
        /// <param name="value">要重映射的值</param>
        /// <param name="fromMin">原始范围最小值</param>
        /// <param name="fromMax">原始范围最大值</param>
        /// <param name="toMin">目标范围最小值</param>
        /// <param name="toMax">目标范围最大值</param>
        /// <returns>重映射后的值</returns>
        /// <remarks>
        /// 将值从一个范围线性映射到另一个范围。例如，将[0,1]范围的值映射到[0,100]。
        /// 当输入范围为零时（fromMin == fromMax），返回目标范围的中点。
        /// 支持反向范围（fromMin > fromMax 或 toMin > toMax）。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 将摇杆输入(-1到1)映射为移动速度(0到100)
        /// float speed = MathUtils.Remap(joystickValue, -1f, 1f, 0f, 100f);
        /// 
        /// // 将血量(0-100)映射为UI显示比例(1-0)
        /// float healthBarScale = MathUtils.Remap(currentHealth, 0f, 100f, 1f, 0f);
        /// </code>
        /// </remarks>
        public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            // 当输入范围为零时，返回输出范围的中点
            if (Mathf.Approximately(fromMax, fromMin))
            {
                return (toMin + toMax) / 2.0f;
            }

            float normalizedValue = (value - fromMin) / (fromMax - fromMin);
            return Mathf.Lerp(toMin, toMax, normalizedValue);
        }

        /// <summary>
        /// 计算方向向量的四元数旋转
        /// </summary>
        /// <param name="direction">方向向量</param>
        /// <param name="up">上方向，默认为Vector3.up</param>
        /// <returns>旋转四元数</returns>
        /// <remarks>
        /// 该方法创建一个从Vector3.forward指向给定方向的旋转四元数。
        /// 如果方向与参考向量接近平行，会使用稳定的数学处理确保正确的旋转结果。
        /// 
        /// <para>常见用例：</para>
        /// <list type="bullet">
        ///   <item>让物体朝向特定方向</item>
        ///   <item>计算从一个点到另一个点的朝向</item>
        ///   <item>根据速度方向旋转角色</item>
        /// </list>
        /// 
        /// <para>示例：</para>
        /// <code>
        /// // 让物体朝向目标
        /// Vector3 direction = (target.position - transform.position).normalized;
        /// transform.rotation = MathUtils.DirectionToRotation(direction);
        /// </code>
        /// </remarks>
        public static Quaternion DirectionToRotation(Vector3 direction, Vector3 up = default)
        {
            // 处理零向量
            if (direction.sqrMagnitude < Mathf.Epsilon)
                return Quaternion.identity;

            // 规范化方向向量
            direction = direction.normalized;
            
            // 设置默认上向量
            if (up == default)
                up = Vector3.up;
                
            // 为了处理向上或向下的方向特殊情况，我们检查该方向是否与世界上方向接近平行
            float upDot = Vector3.Dot(direction, Vector3.up);
            
            // 如果方向几乎垂直向上或向下，我们需要特殊处理
            if (Mathf.Abs(upDot) > 0.9999f)
            {
                // 根据方向计算角度：向上为90度，向下为-90度
                float angle = upDot > 0 ? 90f : -90f;
                
                // 使用轴角表示法创建四元数：绕X轴旋转
                return Quaternion.AngleAxis(angle, Vector3.right);
            }
            
            // 标准情况：使用LookRotation构建四元数
            // 如果方向与上向量接近，使用前向量作为参考向量构建正交基
            if (Mathf.Abs(Vector3.Dot(direction, up)) > 0.9f)
            {
                // 选择一个尽可能垂直于direction的参考向量
                Vector3 reference = Math.Abs(Vector3.Dot(direction, Vector3.forward)) < 0.9f
                    ? Vector3.forward
                    : Vector3.right;
                    
                // 构建正交基
                Vector3 right = Vector3.Cross(reference, direction).normalized;
                Vector3 newUp = Vector3.Cross(direction, right);
                
                // 基于正交基构建旋转矩阵
                return Quaternion.LookRotation(direction, newUp);
            }
            
            // 常规情况
            return Quaternion.LookRotation(direction, up);
        }

        /// <summary>
        /// 检查点是否在多边形内部
        /// </summary>
        /// <param name="point">要检查的点</param>
        /// <param name="polygon">多边形顶点数组</param>
        /// <returns>如果点在多边形内部或边上，则返回true；否则返回false</returns>
        /// <exception cref="ArgumentNullException">当polygon为null时抛出</exception>
        /// <exception cref="ArgumentException">当polygon顶点数小于3时抛出</exception>
        /// <remarks>
        /// 这个方法使用射线投射法（Ray Casting Algorithm）判断点是否在多边形内部。
        /// 算法原理是从测试点向任意方向发射一条射线，计算它与多边形边界的交点数量。
        /// 如果交点数为奇数，则点在多边形内部；如果为偶数，则在外部。
        /// 在多边形边上的点被视为在多边形内。
        /// 
        /// <para>性能说明：</para>
        /// 时间复杂度为O(n)，其中n是多边形的顶点数。这个方法适用于形状不规则的多边形。
        /// 对于凸多边形，可以使用更高效的算法。
        /// 
        /// <para>示例：</para>
        /// <code>
        /// Vector2[] polygon = new Vector2[] {
        ///     new Vector2(0, 0),
        ///     new Vector2(10, 0),
        ///     new Vector2(10, 10),
        ///     new Vector2(0, 10)
        /// };
        /// 
        /// bool isInside = MathUtils.IsPointInPolygon(new Vector2(5, 5), polygon);
        /// // isInside = true
        /// </code>
        /// </remarks>
        public static bool IsPointInPolygon(Vector2 point, Vector2[] polygon)
        {
            if (polygon == null)
                throw new ArgumentNullException(nameof(polygon));

            if (polygon.Length < 3)
                throw new ArgumentException("多边形必须至少有3个顶点", nameof(polygon));

            bool result = false;
            int j = polygon.Length - 1;

            for (int i = 0; i < polygon.Length; i++)
            {
                if (((polygon[i].y > point.y) != (polygon[j].y > point.y)) &&
                    (point.x < (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) / (polygon[j].y - polygon[i].y) + polygon[i].x))
                {
                    result = !result;
                }
                j = i;
            }

            return result;
        }
    }
} 