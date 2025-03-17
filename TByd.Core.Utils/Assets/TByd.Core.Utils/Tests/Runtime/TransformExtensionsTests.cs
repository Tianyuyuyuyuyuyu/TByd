using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TByd.Core.Utils.Runtime.Extensions;

namespace TByd.Core.Utils.Tests.Runtime
{
    public class TransformExtensionsTests
    {
        private GameObject _testObject;
        private Transform _transform;

        [SetUp]
        public void Setup()
        {
            _testObject = new GameObject("TestObject");
            _transform = _testObject.transform;
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_testObject);
        }

        [Test]
        public void ResetLocal_ResetsTransformToDefaultValues()
        {
            // Arrange
            _transform.localPosition = new Vector3(1f, 2f, 3f);
            _transform.localRotation = Quaternion.Euler(30f, 45f, 60f);
            _transform.localScale = new Vector3(2f, 3f, 4f);

            // Act
            _transform.ResetLocal();

            // Assert
            Assert.AreEqual(Vector3.zero, _transform.localPosition);
            Assert.AreEqual(Quaternion.identity, _transform.localRotation);
            Assert.AreEqual(Vector3.one, _transform.localScale);
        }

        [Test]
        public void SetLocalX_SetsXCoordinateOnly()
        {
            // Arrange
            _transform.localPosition = new Vector3(1f, 2f, 3f);
            var newX = 5f;

            // Act
            _transform.SetLocalX(newX);

            // Assert
            Assert.AreEqual(newX, _transform.localPosition.x);
            Assert.AreEqual(2f, _transform.localPosition.y);
            Assert.AreEqual(3f, _transform.localPosition.z);
        }

        [Test]
        public void SetLocalY_SetsYCoordinateOnly()
        {
            // Arrange
            _transform.localPosition = new Vector3(1f, 2f, 3f);
            var newY = 5f;

            // Act
            _transform.SetLocalY(newY);

            // Assert
            Assert.AreEqual(1f, _transform.localPosition.x);
            Assert.AreEqual(newY, _transform.localPosition.y);
            Assert.AreEqual(3f, _transform.localPosition.z);
        }

        [Test]
        public void SetLocalZ_SetsZCoordinateOnly()
        {
            // Arrange
            _transform.localPosition = new Vector3(1f, 2f, 3f);
            var newZ = 5f;

            // Act
            _transform.SetLocalZ(newZ);

            // Assert
            Assert.AreEqual(1f, _transform.localPosition.x);
            Assert.AreEqual(2f, _transform.localPosition.y);
            Assert.AreEqual(newZ, _transform.localPosition.z);
        }

        [Test]
        public void SetX_SetsXCoordinateOnly()
        {
            // Arrange
            _transform.position = new Vector3(1f, 2f, 3f);
            var newX = 5f;

            // Act
            _transform.SetX(newX);

            // Assert
            Assert.AreEqual(newX, _transform.position.x);
            Assert.AreEqual(2f, _transform.position.y);
            Assert.AreEqual(3f, _transform.position.z);
        }

        [Test]
        public void SetY_SetsYCoordinateOnly()
        {
            // Arrange
            _transform.position = new Vector3(1f, 2f, 3f);
            var newY = 5f;

            // Act
            _transform.SetY(newY);

            // Assert
            Assert.AreEqual(1f, _transform.position.x);
            Assert.AreEqual(newY, _transform.position.y);
            Assert.AreEqual(3f, _transform.position.z);
        }

        [Test]
        public void SetZ_SetsZCoordinateOnly()
        {
            // Arrange
            _transform.position = new Vector3(1f, 2f, 3f);
            var newZ = 5f;

            // Act
            _transform.SetZ(newZ);

            // Assert
            Assert.AreEqual(1f, _transform.position.x);
            Assert.AreEqual(2f, _transform.position.y);
            Assert.AreEqual(newZ, _transform.position.z);
        }

        [Test]
        public void GetAllChildren_ReturnsAllChildren()
        {
            // Arrange
            var child1 = new GameObject("Child1");
            var child2 = new GameObject("Child2");
            child1.transform.SetParent(_transform);
            child2.transform.SetParent(_transform);

            // Act
            var children = _transform.GetAllChildren();

            // Assert
            Assert.AreEqual(2, children.Count);
            Assert.That(children, Does.Contain(child1.transform));
            Assert.That(children, Does.Contain(child2.transform));

            // Cleanup
            Object.DestroyImmediate(child1);
            Object.DestroyImmediate(child2);
        }

        [Test]
        public void GetAllChildren_ExcludesInactiveChildren_WhenSpecified()
        {
            // Arrange
            var child1 = new GameObject("Child1");
            var child2 = new GameObject("Child2");
            child1.transform.SetParent(_transform);
            child2.transform.SetParent(_transform);
            child2.SetActive(false);

            // Act
            var children = _transform.GetAllChildren(includeInactive: false);

            // Assert
            Assert.AreEqual(1, children.Count);
            Assert.That(children, Does.Contain(child1.transform));
            Assert.That(children, Does.Not.Contain(child2.transform));

            // Cleanup
            Object.DestroyImmediate(child1);
            Object.DestroyImmediate(child2);
        }

        [UnityTest]
        public IEnumerator DestroyAllChildren_DestroysAllChildren()
        {
            // Arrange
            var child1 = new GameObject("Child1");
            var child2 = new GameObject("Child2");
            child1.transform.SetParent(_transform);
            child2.transform.SetParent(_transform);

            // Act
            _transform.DestroyAllChildren();

            // 等待一帧，因为Destroy是在帧结束时执行的
            yield return null;

            // Assert
            Assert.AreEqual(0, _transform.childCount);
        }

        [Test]
        public void DestroyAllChildren_Immediate_DestroysAllChildrenImmediately()
        {
            // Arrange
            var child1 = new GameObject("Child1");
            var child2 = new GameObject("Child2");
            child1.transform.SetParent(_transform);
            child2.transform.SetParent(_transform);

            // Act
            _transform.DestroyAllChildren(immediate: true);

            // Assert - 不需要等待，因为DestroyImmediate是立即执行的
            Assert.AreEqual(0, _transform.childCount);
        }

        [Test]
        public void FindOrCreateChild_FindsExistingChild()
        {
            // Arrange
            var childName = "ChildToFind";
            var child = new GameObject(childName);
            child.transform.SetParent(_transform);

            // Act
            var foundChild = _transform.FindOrCreateChild(childName);

            // Assert
            Assert.AreEqual(child.transform, foundChild);
            Assert.AreEqual(1, _transform.childCount); // 不应该创建新的子物体

            // Cleanup
            Object.DestroyImmediate(child);
        }

        [Test]
        public void FindOrCreateChild_CreatesNewChildIfNotFound()
        {
            // Arrange
            var childName = "NonExistentChild";

            // Act
            var createdChild = _transform.FindOrCreateChild(childName);

            // Assert
            Assert.IsNotNull(createdChild);
            Assert.AreEqual(childName, createdChild.name);
            Assert.AreEqual(_transform, createdChild.parent);
            Assert.AreEqual(1, _transform.childCount);
        }

        [Test]
        public void FindRecursive_FindsDirectChild()
        {
            // Arrange
            var childName = "DirectChild";
            var child = new GameObject(childName);
            child.transform.SetParent(_transform);

            // Act
            var foundChild = _transform.FindRecursive(childName);

            // Assert
            Assert.AreEqual(child.transform, foundChild);

            // Cleanup
            Object.DestroyImmediate(child);
        }

        [Test]
        public void FindRecursive_FindsNestedChild()
        {
            // Arrange
            var childName = "NestedChild";
            var intermediateChild = new GameObject("IntermediateChild");
            var nestedChild = new GameObject(childName);
            
            intermediateChild.transform.SetParent(_transform);
            nestedChild.transform.SetParent(intermediateChild.transform);

            // Act
            var foundChild = _transform.FindRecursive(childName);

            // Assert
            Assert.AreEqual(nestedChild.transform, foundChild);

            // Cleanup
            Object.DestroyImmediate(intermediateChild); // 这会同时销毁nestedChild
        }

        [Test]
        public void FindRecursive_ReturnsNullWhenChildNotFound()
        {
            // Arrange
            var childName = "NonExistentChild";

            // Act
            var foundChild = _transform.FindRecursive(childName);

            // Assert
            Assert.IsNull(foundChild);
        }
    }
} 