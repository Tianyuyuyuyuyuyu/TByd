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
            var current = 0f;
            var target = 10f;
            var velocity = 0f;
            var smoothTime = 0.1f;
            
            // Act - 模拟多次更新
            for (var i = 0; i < 100; i++)
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
            var current = Vector2.zero;
            var target = new Vector2(10f, 5f);
            var velocity = Vector2.zero;
            var smoothTime = 0.1f;
            
            // Act - 模拟多次更新
            for (var i = 0; i < 100; i++)
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
            var current = Vector3.zero;
            var target = new Vector3(10f, 5f, 3f);
            var velocity = Vector3.zero;
            var smoothTime = 0.1f;
            
            // Act - 模拟多次更新
            for (var i = 0; i < 100; i++)
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
            var current = 0f;
            var target = 10f;
            var velocity = 0f;
            var smoothTime = 0.1f;
            var maxSpeed = 0.5f; // 限制最大速度
            
            // Act - 单次更新
            var newValue = MathUtils.SmoothDamp(current, target, ref velocity, smoothTime, maxSpeed, 1f);
            
            // Assert - 单次更新的变化不应超过maxSpeed
            Assert.That(Mathf.Abs(newValue - current), Is.LessThanOrEqualTo(maxSpeed));
        }
        
        [Test]
        public void Remap_MapsValueCorrectly()
        {
            // Arrange
            var value = 5f;
            var fromMin = 0f;
            var fromMax = 10f;
            var toMin = 0f;
            var toMax = 100f;
            
            // Act
            var result = MathUtils.Remap(value, fromMin, fromMax, toMin, toMax);
            
            // Assert
            Assert.That(result, Is.EqualTo(50f).Within(Epsilon));
        }
        
        [Test]
        public void Remap_HandlesInvertedRanges()
        {
            // Arrange
            var value = 5f;
            var fromMin = 10f;
            var fromMax = 0f; // 反转的输入范围
            var toMin = 100f;
            var toMax = 0f; // 反转的输出范围
            
            // Act
            var result = MathUtils.Remap(value, fromMin, fromMax, toMin, toMax);
            
            // Assert
            Assert.That(result, Is.EqualTo(50f).Within(Epsilon));
        }
        
        [Test]
        public void Remap_HandlesZeroRangeInput()
        {
            // Arrange
            var value = 5f;
            var fromMin = 5f;
            var fromMax = 5f; // 零范围输入
            var toMin = 0f;
            var toMax = 10f;
            
            // Act
            var result = MathUtils.Remap(value, fromMin, fromMax, toMin, toMax);
            
            // Assert - 当输入范围为零时，应返回输出范围的中点
            var expectedMidpoint = (toMin + toMax) / 2f; // 明确计算中点值为5.0f
            Assert.That(result, Is.EqualTo(expectedMidpoint).Within(Epsilon));
        }
        
        [Test]
        public void DirectionToRotation_ForwardDirection_ReturnsIdentity()
        {
            // Arrange
            var direction = Vector3.forward;
            
            // Act
            var rotation = MathUtils.DirectionToRotation(direction);
            
            // Assert
            Assert.That(rotation, Is.EqualTo(Quaternion.identity).Using<Quaternion>((q1, q2) => 
                Quaternion.Angle(q1, q2) < Epsilon ? 0 : 1));
        }
        
        [Test]
        public void DirectionToRotation_UpDirection_ReturnsCorrectRotation()
        {
            // Arrange
            var direction = Vector3.up;
            
            // Act
            var rotation = MathUtils.DirectionToRotation(direction);
            
            // Assert - 向上的方向应该是绕X轴旋转90度
            var expected = Quaternion.Euler(90f, 0f, 0f);
            var angle = Quaternion.Angle(rotation, expected);
            Assert.That(angle, Is.LessThan(Epsilon));
        }
        
        [Test]
        public void DirectionToRotation_ZeroVector_ReturnsIdentity()
        {
            // Arrange
            var direction = Vector3.zero;
            
            // Act
            var rotation = MathUtils.DirectionToRotation(direction);
            
            // Assert - 零向量应该返回单位四元数
            Assert.That(rotation, Is.EqualTo(Quaternion.identity).Using<Quaternion>((q1, q2) => 
                Quaternion.Angle(q1, q2) < Epsilon ? 0 : 1));
        }
        
        [Test]
        public void IsPointInPolygon_PointInside_ReturnsTrue()
        {
            // Arrange
            var polygon = new[]
            {
                new Vector2(0, 0),
                new Vector2(10, 0),
                new Vector2(10, 10),
                new Vector2(0, 10)
            };
            
            var point = new Vector2(5, 5);
            
            // Act
            var result = MathUtils.IsPointInPolygon(point, polygon);
            
            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsPointInPolygon_PointOutside_ReturnsFalse()
        {
            // Arrange
            var polygon = new[]
            {
                new Vector2(0, 0),
                new Vector2(10, 0),
                new Vector2(10, 10),
                new Vector2(0, 10)
            };
            
            var point = new Vector2(15, 15);
            
            // Act
            var result = MathUtils.IsPointInPolygon(point, polygon);
            
            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void IsPointInPolygon_PointOnEdge_ReturnsTrue()
        {
            // Arrange
            var polygon = new[]
            {
                new Vector2(0, 0),
                new Vector2(10, 0),
                new Vector2(10, 10),
                new Vector2(0, 10)
            };
            
            var point = new Vector2(5, 0); // 在多边形的边上
            
            // Act
            var result = MathUtils.IsPointInPolygon(point, polygon);
            
            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsPointInPolygon_NullPolygon_ThrowsArgumentNullException()
        {
            // Arrange
            var point = new Vector2(5, 5);
            
            // Act & Assert
            Assert.That(() => MathUtils.IsPointInPolygon(point, null), Throws.ArgumentNullException);
        }
        
        [Test]
        public void IsPointInPolygon_TooFewVertices_ThrowsArgumentException()
        {
            // Arrange
            var polygon = new[] { new Vector2(0, 0), new Vector2(10, 0) }; // 只有两个顶点
            var point = new Vector2(5, 5);
            
            // Act & Assert
            Assert.That(() => MathUtils.IsPointInPolygon(point, polygon), Throws.ArgumentException);
        }
    }
} 