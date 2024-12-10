namespace TBydFramework.Pool.Runtime.Validation
{
    public class BasicObjectValidator<T> : IPoolObjectValidator<T> where T : class
    {
        public bool Validate(T obj)
        {
            return obj != null;
        }

        public ValidationResult GetValidationDetails(T obj)
        {
            if (obj == null)
                return ValidationResult.Failure("对象为空", ValidationSeverity.Critical);

            return ValidationResult.Success();
        }
    }
} 