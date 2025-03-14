using System;
using NUnit.Framework;
using TByd.Core.Utils.Runtime;

namespace TByd.Core.Utils.Tests.Runtime
{
    public class StringUtilsTests
    {
        [Test]
        public void IsNullOrWhiteSpace_NullString_ReturnsTrue()
        {
            // Arrange
            string input = null;
            
            // Act
            bool result = StringUtils.IsNullOrWhiteSpace(input);
            
            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsNullOrWhiteSpace_EmptyString_ReturnsTrue()
        {
            // Arrange
            string input = string.Empty;
            
            // Act
            bool result = StringUtils.IsNullOrWhiteSpace(input);
            
            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsNullOrWhiteSpace_WhitespaceString_ReturnsTrue()
        {
            // Arrange
            string input = "   ";
            
            // Act
            bool result = StringUtils.IsNullOrWhiteSpace(input);
            
            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void IsNullOrWhiteSpace_NonEmptyString_ReturnsFalse()
        {
            // Arrange
            string input = "Hello";
            
            // Act
            bool result = StringUtils.IsNullOrWhiteSpace(input);
            
            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void GenerateRandom_NegativeLength_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            Assert.That(() => StringUtils.GenerateRandom(-1), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        
        [Test]
        public void GenerateRandom_ZeroLength_ReturnsEmptyString()
        {
            // Act
            string result = StringUtils.GenerateRandom(0);
            
            // Assert
            Assert.That(result, Is.Empty);
        }
        
        [Test]
        public void GenerateRandom_PositiveLength_ReturnsStringWithCorrectLength()
        {
            // Arrange
            int length = 10;
            
            // Act
            string result = StringUtils.GenerateRandom(length);
            
            // Assert
            Assert.That(result.Length, Is.EqualTo(length));
        }
        
        [Test]
        public void GenerateRandom_CalledTwice_ReturnsDifferentStrings()
        {
            // Arrange
            int length = 10;
            
            // Act
            string result1 = StringUtils.GenerateRandom(length);
            string result2 = StringUtils.GenerateRandom(length);
            
            // Assert
            Assert.That(result1, Is.Not.EqualTo(result2));
        }
        
        [Test]
        public void ToSlug_NullString_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.That(() => StringUtils.ToSlug(null), Throws.TypeOf<ArgumentNullException>());
        }
        
        [Test]
        public void ToSlug_EmptyString_ReturnsEmptyString()
        {
            // Arrange
            string value = string.Empty;
            
            // Act
            string result = StringUtils.ToSlug(value);
            
            // Assert
            Assert.That(result, Is.Empty);
        }
        
        [Test]
        public void ToSlug_StringWithSpaces_ReturnsSlugWithHyphens()
        {
            // Arrange
            string value = "Hello World";
            
            // Act
            string result = StringUtils.ToSlug(value);
            
            // Assert
            Assert.That(result, Is.EqualTo("hello-world"));
        }
        
        [Test]
        public void ToSlug_StringWithSpecialChars_ReturnsSlugWithoutSpecialChars()
        {
            // Arrange
            string value = "Hello, World! This is a Test.";
            
            // Act
            string result = StringUtils.ToSlug(value);
            
            // Assert
            Assert.That(result, Is.EqualTo("hello-world-this-is-a-test"));
        }
        
        [Test]
        public void Truncate_NullString_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.That(() => StringUtils.Truncate(null, 5), Throws.TypeOf<ArgumentNullException>());
        }
        
        [Test]
        public void Truncate_NegativeMaxLength_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            Assert.That(() => StringUtils.Truncate("Hello", -1), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
        
        [Test]
        public void Truncate_StringShorterThanMaxLength_ReturnsOriginalString()
        {
            // Arrange
            string input = "Hello";
            int maxLength = 10;
            
            // Act
            string result = StringUtils.Truncate(input, maxLength);
            
            // Assert
            Assert.That(result, Is.EqualTo(input));
        }
        
        [Test]
        public void Truncate_StringLongerThanMaxLength_ReturnsTruncatedString()
        {
            // Arrange
            string input = "Hello World";
            int maxLength = 5;
            
            // Act
            string result = StringUtils.Truncate(input, maxLength);
            
            // Assert
            Assert.That(result, Is.EqualTo("He..."));
        }
        
        [Test]
        public void Truncate_CustomSuffix_ReturnsTruncatedStringWithCustomSuffix()
        {
            // Arrange
            string input = "Hello World";
            int maxLength = 5;
            string suffix = "***";
            
            // Act
            string result = StringUtils.Truncate(input, maxLength, suffix);
            
            // Assert
            Assert.That(result, Is.EqualTo("He***"));
        }
        
        [Test]
        public void Split_NullString_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.That(() => StringUtils.Split(null, ','), Throws.TypeOf<ArgumentNullException>());
        }
        
        [Test]
        public void Split_EmptyString_ReturnsEmptyEnumeration()
        {
            // Arrange
            string value = string.Empty;
            
            // Act
            var enumerator = StringUtils.Split(value, ',');
            
            // Assert
            Assert.That(enumerator.MoveNext(), Is.False);
        }
        
        [Test]
        public void Split_StringWithSeparator_ReturnsCorrectParts()
        {
            // Arrange
            string value = "a,b,c";
            
            // Act & Assert
            var enumerator = StringUtils.Split(value, ',');
            
            Assert.That(enumerator.MoveNext(), Is.True);
            Assert.That(enumerator.Current, Is.EqualTo("a"));
            
            Assert.That(enumerator.MoveNext(), Is.True);
            Assert.That(enumerator.Current, Is.EqualTo("b"));
            
            Assert.That(enumerator.MoveNext(), Is.True);
            Assert.That(enumerator.Current, Is.EqualTo("c"));
            
            Assert.That(enumerator.MoveNext(), Is.False);
        }
        
        [Test]
        public void Split_StringWithoutSeparator_ReturnsSinglePart()
        {
            // Arrange
            string value = "abc";
            
            // Act & Assert
            var enumerator = StringUtils.Split(value, ',');
            
            Assert.That(enumerator.MoveNext(), Is.True);
            Assert.That(enumerator.Current, Is.EqualTo("abc"));
            
            Assert.That(enumerator.MoveNext(), Is.False);
        }
    }
} 