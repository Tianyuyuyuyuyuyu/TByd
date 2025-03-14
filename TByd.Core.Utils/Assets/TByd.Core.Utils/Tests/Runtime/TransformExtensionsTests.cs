using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TByd.Core.Utils.Runtime.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace TByd.Core.Utils.Tests.Runtime
{
    public class TransformExtensionsTests
    {
        private GameObject _testObject;
        private Transform _transform;

        [SetUp]
        public void SetUp()
        {
            _testObject = new GameObject("TestObject");
            _transform = _testObject.transform;
            
            // 设置一些非默认值
            _transform.position = new Vector3(1, 2, 3);
            _transform.rotation = Quaternion.Euler(30, 45, 60);
            _transform.localScale = new Vector3(2, 3, 4);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_testObject);
        }

        [Test]
        public void ResetLocal_ResetsLocalTransform()
        {
            // Act
            _transform.ResetLocal();
            
            // Assert
            Assert.That(_transform.localPosition, Is.EqualTo(Vector3.zero));
            Assert.That(_transform.localRotation, Is.EqualTo(Quaternion.identity));
            Assert.That(_transform.localScale, Is.EqualTo(Vector3.one));
        }
        
        [Test]
        public void SetLocalX_SetsXComponentOnly()
        {
            // Arrange
            Vector3 originalPosition = _transform.localPosition;
            float newX = 10f;
            
            // Act
            _transform.SetLocalX(newX);
            
            // Assert
            Assert.That(_transform.localPosition.x, Is.EqualTo(newX));
            Assert.That(_transform.localPosition.y, Is.EqualTo(originalPosition.y));
            Assert.That(_transform.localPosition.z, Is.EqualTo(originalPosition.z));
        }
        
        [Test]
        public void SetLocalY_SetsYComponentOnly()
        {
            // Arrange
            Vector3 originalPosition = _transform.localPosition;
            float newY = 10f;
            
            // Act
            _transform.SetLocalY(newY);
            
            // Assert
            Assert.That(_transform.localPosition.x, Is.EqualTo(originalPosition.x));
            Assert.That(_transform.localPosition.y, Is.EqualTo(newY));
            Assert.That(_transform.localPosition.z, Is.EqualTo(originalPosition.z));
        }
        
        [Test]
        public void SetLocalZ_SetsZComponentOnly()
        {
            // Arrange
            Vector3 originalPosition = _transform.localPosition;
            float newZ = 10f;
            
            // Act
            _transform.SetLocalZ(newZ);
            
            // Assert
            Assert.That(_transform.localPosition.x, Is.EqualTo(originalPosition.x));
            Assert.That(_transform.localPosition.y, Is.EqualTo(originalPosition.y));
            Assert.That(_transform.localPosition.z, Is.EqualTo(newZ));
        }
        
        [Test]
        public void FindOrCreateChild_ChildExists_ReturnsExistingChild()
        {
            // Arrange
            string childName = "ExistingChild";
            GameObject existingChild = new GameObject(childName);
            existingChild.transform.SetParent(_transform);
            
            // Act
            Transform result = _transform.FindOrCreateChild(childName);
            
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.name, Is.EqualTo(childName));
            Assert.That(result.gameObject, Is.EqualTo(existingChild));
        }
        
        [Test]
        public void FindOrCreateChild_ChildDoesNotExist_CreatesAndReturnsNewChild()
        {
            // Arrange
            string childName = "NewChild";
            
            // Act
            Transform result = _transform.FindOrCreateChild(childName);
            
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.name, Is.EqualTo(childName));
            Assert.That(result.parent, Is.EqualTo(_transform));
        }
        
        [Test]
        public void GetAllChildren_NoChildren_ReturnsEmptyList()
        {
            // Act
            List<Transform> children = _transform.GetAllChildren();
            
            // Assert
            Assert.That(children, Is.Empty);
        }
        
        [Test]
        public void GetAllChildren_WithChildren_ReturnsAllChildren()
        {
            // Arrange
            GameObject child1 = new GameObject("Child1");
            child1.transform.SetParent(_transform);
            
            GameObject child2 = new GameObject("Child2");
            child2.transform.SetParent(_transform);
            
            // Act
            List<Transform> children = _transform.GetAllChildren();
            
            // Assert
            Assert.That(children, Has.Count.EqualTo(2));
            Assert.That(children, Has.Member(child1.transform));
            Assert.That(children, Has.Member(child2.transform));
        }

        [Test]
        public void GetAllChildren_ExcludesInactiveChildren_WhenSpecified()
        {
            // Arrange
            GameObject child1 = new GameObject("Child1");
            GameObject child2 = new GameObject("Child2");
            
            // 确保子对象正确设置父级关系
            child1.transform.SetParent(_transform);
            child2.transform.SetParent(_transform);
            
            // 确保激活状态正确设置，使用SetActive方法
            child1.SetActive(true);
            child2.SetActive(false);

            // 确认设置成功
            Assert.That(child1.activeSelf, Is.True, "准备阶段：child1必须处于激活状态");
            Assert.That(child2.activeSelf, Is.False, "准备阶段：child2必须处于非激活状态");

            // Act - 不包含非激活的子对象
            List<Transform> children = _transform.GetAllChildren(includeInactive: false);

            // 调试输出
            Debug.Log($"返回的子对象数量: {children.Count}");
            for (int i = 0; i < children.Count; i++)
            {
                var child = children[i];
                Debug.Log($"子对象[{i}]: {(child != null ? child.name : "null")} - 激活状态: {(child != null && child.gameObject != null ? child.gameObject.activeSelf.ToString() : "N/A")}");
            }

            // Assert
            // 1. 列表必须非空
            Assert.That(children, Is.Not.Null, "返回的列表不应为null");
            
            // 2. 列表中不能有null元素
            Assert.That(children.All(t => t != null), Is.True, "列表中不应包含null元素");
            
            // 3. 只包含激活的子对象
            Assert.That(children.Count, Is.EqualTo(1), "应只包含一个激活的子对象");
            Assert.That(children[0], Is.EqualTo(child1.transform), "唯一的元素应该是激活的child1");
            
            // 4. 不包含非激活对象 - 使用Contains而非Has.No.Member
            Assert.That(children.Contains(child2.transform), Is.False, "不应包含非激活的子对象");
        }

        [UnityTest]
        public IEnumerator DestroyAllChildren_DestroysAllChildren()
        {
            // Arrange
            GameObject child1 = new GameObject("Child1");
            GameObject child2 = new GameObject("Child2");
            child1.transform.SetParent(_transform);
            child2.transform.SetParent(_transform);

            // Act
            _transform.DestroyAllChildren();

            // 等待一帧，因为Destroy是在帧结束时执行的
            yield return null;

            // Assert
            Assert.That(_transform.childCount, Is.EqualTo(0));
        }

        [Test]
        public void DestroyAllChildren_Immediate_DestroysAllChildrenImmediately()
        {
            // Arrange
            GameObject child1 = new GameObject("Child1");
            GameObject child2 = new GameObject("Child2");
            child1.transform.SetParent(_transform);
            child2.transform.SetParent(_transform);

            // Act
            _transform.DestroyAllChildren(immediate: true);

            // Assert - 不需要等待，因为DestroyImmediate是立即执行的
            Assert.That(_transform.childCount, Is.EqualTo(0));
        }

        [Test]
        public void FindRecursive_FindsDirectChild()
        {
            // Arrange
            string childName = "DirectChild";
            GameObject child = new GameObject(childName);
            child.transform.SetParent(_transform);

            // Act
            Transform foundChild = _transform.FindRecursive(childName);

            // Assert
            Assert.That(foundChild, Is.EqualTo(child.transform));
        }

        [Test]
        public void FindRecursive_FindsNestedChild()
        {
            // Arrange
            string childName = "NestedChild";
            GameObject intermediateChild = new GameObject("IntermediateChild");
            GameObject nestedChild = new GameObject(childName);
            
            intermediateChild.transform.SetParent(_transform);
            nestedChild.transform.SetParent(intermediateChild.transform);

            // Act
            Transform foundChild = _transform.FindRecursive(childName);

            // Assert
            Assert.That(foundChild, Is.EqualTo(nestedChild.transform));
        }

        [Test]
        public void FindRecursive_ReturnsNullWhenChildNotFound()
        {
            // Arrange
            string childName = "NonExistentChild";

            // Act
            Transform foundChild = _transform.FindRecursive(childName);

            // Assert
            Assert.That(foundChild, Is.Null);
        }
    }
} 