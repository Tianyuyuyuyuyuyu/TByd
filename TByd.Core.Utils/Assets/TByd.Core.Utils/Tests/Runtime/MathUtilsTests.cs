using System;
using NUnit.Framework;
using UnityEngine;
using TByd.Core.Utils.Runtime;

namespace TByd.Core.Utils.Tests.Runtime
{
    public class MathUtilsTests
    {
        private const float Epsilon = 0.0001f;

        [Test]
        public void SmoothDamp_Float_ApproachesTargetValue()
        {
            // Arrange
            float current = 0f;
            float target = 10f;
            float velocity = 0f;
            float smoothTime = 0.1f;
            
            // Act - 模拟多次更新
            for (int i = 0; i < 100; i++)
            {
                current = MathUtils.SmoothDamp(current, target, ref velocity, smoothTime, Mathf.Infinity, 0.01f);
            }
            
            // Assert - 经过足够的时间后，应该非常接近目标值
            Assert.That(Mathf.Abs(current - target), Is.LessThan(Epsilon));
        }
        
        [Test]
        public void SmoothDamp_Vector2_ApproachesTargetValue()
        {
            // Arrange
            Vector2 current = Vector2.zero;
            Vector2 target = new Vector2(10f, 5f);
            Vector2 velocity = Vector2.zero;
            float smoothTime = 0.1f;
            
            // Act - 模拟多次更新
            for (int i = 0; i < 100; i++)
            {
                current = MathUtils.SmoothDamp(current, target, ref velocity, smoothTime, Mathf.Infinity, 0.01f);
            }
            
            // Assert - 经过足够的时间后，应该非常接近目标值
            Assert.That(Vector2.Distance(current, target), Is.LessThan(Epsilon));
        }
        
        [Test]
        public void SmoothDamp_Vector3_ApproachesTargetValue()
        {
            // Arrange
            Vector3 current = Vector3.zero;
            Vector3 target = new Vector3(10f, 5f, 3f);
            Vector3 velocity = Vector3.zero;
            float smoothTime = 0.1f;
            
            // Act - 模拟多次更新
            for (int i = 0; i < 100; i++)
            {
                current = MathUtils.SmoothDamp(current, target, ref velocity, smoothTime, Mathf.Infinity, 0.01f);
            }
            
            // Assert - 经过足够的时间后，应该非常接近目标值
            Assert.That(Vector3.Distance(current, target), Is.LessThan(Epsilon));
        }
        
        [Test]
        public void SmoothDamp_WithMaxSpeed_RespectsMaxSpeed()
        {
            // Arrange
            float current = 0f;
            float target = 10f;
            float velocity = 0f;
            float smoothTime = 0.1f;
            float maxSpeed = 0.5f; // 限制最大速度
            
            // Act - 单次更新
            float newValue = MathUtils.SmoothDamp(current, target, ref velocity, smoothTime, maxSpeed, 1f);
            
            // Assert - 单次更新的变化不应超过maxSpeed
            Assert.That(Mathf.Abs(newValue - current), Is.LessThanOrEqualTo(maxSpeed));
        }
        
        [Test]
        public void Remap_MapsValueCorrectly()
        {
            // Arrange
            float value = 5f;
            float fromMin = 0f;
            float fromMax = 10f;
            float toMin = 0f;
            float toMax = 100f;
            
            // Act
            float result = MathUtils.Remap(value, fromMin, fromMax, toMin, toMax);
            
            // Assert
            Assert.That(result, Is.EqualTo(50f).Within(Epsilon));
        }
        
        [Test]
        public void Remap_HandlesInvertedRanges()
        {
            // Arrange
            float value = 5f;
            float fromMin = 10f;
            float fromMax = 0f; // 反转的输入范围
            float toMin = 100f;
            float toMax = 0f; // 反转的输出范围
            
            // Act
            float result = MathUtils.Remap(value, fromMin, fromMax, toMin, toMax);
            
            // Assert
            Assert.That(result, Is.EqualTo(50f).Within(Epsilon));
        }
        
        [Test]
        public void Remap_HandlesZeroRangeInput()
        {
            // Arrange
            float value = 5f;
            float fromMin = 5f;
            float fromMax = 5f; // 零范围输入
            float toMin = 0f;
            float toMax = 10f;
            
            // Act
            float result = MathUtils.Remap(value, fromMin, fromMax, toMin, toMax);
            
            // Assert - 当输入范围为零时，应返回输出范围的中点
            float expectedMidpoint = (toMin + toMax) / 2f; // 明确计算中点值为5.0f
            Assert.That(result, Is.EqualTo(expectedMidpoint).Within(Epsilon));
        }
        
        [Test]
        public void DirectionToRotation_ForwardDirection_ReturnsIdentity()
        {
            // Arrange
            Vector3 direction = Vector3.forward;
            
            // Act
            Quaternion rotation = MathUtils.DirectionToRotation(direction);
            
            // Assert
            Assert.That(rotation, Is.EqualTo(Quaternion.identity).Using<Quaternion>((q1, q2) => 
                Quaternion.Angle(q1, q2) < Epsilon ? 0 : 1));
        }
        
        [Test]
        public void DirectionToRotation_UpDirection_ReturnsCorrectRotation()
        {
            // Arrange
            Vector3 direction = Vector3.up;
            
            // Act
            Quaternion rotation = MathUtils.DirectionToRotation(direction);
            
            // Assert - 向上的方向应该是绕X轴旋转90度
            Quaternion expected = Quaternion.Euler(90f, 0f, 0f);
            float angle = Quaternion.Angle(rotation, expected);
            Assert.That(angle, Is.LessThan(Epsilon));
        }
        
        [Test]
        public void DirectionToRotation_ZeroVector_ReturnsIdentity()
        {
            // Arrange
            Vector3 direction = Vector3.zero;
            
            // Act
            Quaternion rotation = MathUtils.DirectionToRotation(direction);
            
            // Assert - 零向量应该返回单位四元数
            Assert.That(rotation, Is.EqualTo(Quaternion.identity).Using<Quaternion>((q1, q2) => 
                Quaternion.Angle(q1, q2) < Epsilon ? 0 : 1));
        }
        
        [Test]
        public void IsPointInPolygon_PointInside_ReturnsTrue()
        {
            // Arrange
            Vector2[] polygon = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(10, 0),
                new Vector2(10, 10),
                new Vector2(0, 10)
            };
            
            Vector2 point = new Vector2(5, 5);
            
            // Act
            bool result = MathUtils.IsPointInPolygon(point, polygon);
            
            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsPointInPolygon_PointOutside_ReturnsFalse()
        {
            // Arrange
            Vector2[] polygon = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(10, 0),
                new Vector2(10, 10),
                new Vector2(0, 10)
            };
            
            Vector2 point = new Vector2(15, 15);
            
            // Act
            bool result = MathUtils.IsPointInPolygon(point, polygon);
            
            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void IsPointInPolygon_PointOnEdge_ReturnsTrue()
        {
            // Arrange
            Vector2[] polygon = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(10, 0),
                new Vector2(10, 10),
                new Vector2(0, 10)
            };
            
            Vector2 point = new Vector2(5, 0); // 在多边形的边上
            
            // Act
            bool result = MathUtils.IsPointInPolygon(point, polygon);
            
            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsPointInPolygon_NullPolygon_ThrowsArgumentNullException()
        {
            // Arrange
            Vector2[] polygon = null;
            Vector2 point = new Vector2(5, 5);
            
            // Act & Assert
            Assert.That(() => MathUtils.IsPointInPolygon(point, polygon), Throws.ArgumentNullException);
        }
        
        [Test]
        public void IsPointInPolygon_TooFewVertices_ThrowsArgumentException()
        {
            // Arrange
            Vector2[] polygon = new Vector2[] { new Vector2(0, 0), new Vector2(10, 0) }; // 只有两个顶点
            Vector2 point = new Vector2(5, 5);
            
            // Act & Assert
            Assert.That(() => MathUtils.IsPointInPolygon(point, polygon), Throws.ArgumentException);
        }
    }
} 