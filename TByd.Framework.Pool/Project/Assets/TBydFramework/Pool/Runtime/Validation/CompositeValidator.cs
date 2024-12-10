using System.Collections.Generic;
using UnityEngine;

namespace TBydFramework.Pool.Runtime.Validation
{
    public class CompositeValidator<T> : IPoolObjectValidator<T> where T : class
    {
        private readonly List<IPoolObjectValidator<T>> _validators = new();
        private readonly bool _failFast;

        public CompositeValidator(bool failFast = true)
        {
            _failFast = failFast;
        }

        public void AddValidator(IPoolObjectValidator<T> validator)
        {
            _validators.Add(validator);
        }

        public bool Validate(T obj)
        {
            foreach (var validator in _validators)
            {
                if (!validator.Validate(obj) && _failFast)
                {
                    return false;
                }
            }
            return true;
        }

        public ValidationResult GetValidationDetails(T obj)
        {
            var results = new List<ValidationResult>();
            var maxSeverity = ValidationSeverity.None;

            foreach (var validator in _validators)
            {
                var result = validator.GetValidationDetails(obj);
                if (!result.IsValid)
                {
                    results.Add(result);
                    if (result.Severity > maxSeverity)
                    {
                        maxSeverity = result.Severity;
                    }
                    if (_failFast)
                    {
                        return result;
                    }
                }
            }

            if (results.Count > 0)
            {
                return ValidationResult.Failure(
                    string.Join("\n", results.ConvertAll(r => r.Message)),
                    maxSeverity
                );
            }

            return ValidationResult.Success();
        }
    }
} 