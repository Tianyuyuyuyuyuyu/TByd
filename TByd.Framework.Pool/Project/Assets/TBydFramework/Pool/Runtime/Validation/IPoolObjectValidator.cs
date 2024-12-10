using System;

namespace TBydFramework.Pool.Runtime.Validation
{
    public interface IPoolObjectValidator<T> where T : class
    {
        bool Validate(T obj);
        ValidationResult GetValidationDetails(T obj);
    }

    public struct ValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public ValidationSeverity Severity { get; set; }
        public DateTime Timestamp { get; set; }

        public static ValidationResult Success() => new()
        {
            IsValid = true,
            Severity = ValidationSeverity.None,
            Timestamp = DateTime.UtcNow
        };

        public static ValidationResult Failure(string message, ValidationSeverity severity = ValidationSeverity.Error) => new()
        {
            IsValid = false,
            Message = message,
            Severity = severity,
            Timestamp = DateTime.UtcNow
        };
    }

    public enum ValidationSeverity
    {
        None,
        Warning,
        Error,
        Critical
    }
} 