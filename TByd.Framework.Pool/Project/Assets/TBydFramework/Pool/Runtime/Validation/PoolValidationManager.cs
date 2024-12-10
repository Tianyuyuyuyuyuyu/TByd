using System;
using System.Collections.Generic;
using TBydFramework.Pool.Runtime.Core;
using UnityEngine;
using TBydFramework.Pool.Runtime.Diagnostics;
using Object = UnityEngine.Object;

namespace TBydFramework.Pool.Runtime.Validation
{
    public class PoolValidationManager : SingletonBehaviour<PoolValidationManager>
    {
        private readonly Dictionary<Type, object> _validators = new();
        
        [SerializeField] private bool _enableValidationLogging = true;
        [SerializeField] private ValidationSeverity _minimumLogSeverity = ValidationSeverity.Warning;

        public IPoolObjectValidator<T> GetValidator<T>() where T : class
        {
            var type = typeof(T);
            if (!_validators.TryGetValue(type, out var validator))
            {
                validator = CreateDefaultValidator<T>();
                _validators[type] = validator;
            }
            return (IPoolObjectValidator<T>)validator;
        }

        private IPoolObjectValidator<T> CreateDefaultValidator<T>() where T : class
        {
            var validator = new CompositeValidator<T>();
            
            // 添加基本验证器
            validator.AddValidator(new BasicObjectValidator<T>());

            // 如果是Unity对象，添加Unity特定的验证器
            if (typeof(Object).IsAssignableFrom(typeof(T)))
            {
                validator.AddValidator(new UnityObjectValidator<T>());
            }
            
            return validator;
        }

        public void LogValidationResult<T>(T obj, ValidationResult result) where T : class
        {
            if (!_enableValidationLogging || result.Severity < _minimumLogSeverity)
                return;

            var message = $"对象验证 [{obj}]: {result.Message}";
            
            switch (result.Severity)
            {
                case ValidationSeverity.Warning:
                    Debug.LogWarning(message);
                    break;
                case ValidationSeverity.Error:
                case ValidationSeverity.Critical:
                    Debug.LogError(message);
                    PoolDiagnosticEvents.RaisePoolError(obj.GetType().Name, 
                        new ValidationException(result.Message));
                    break;
            }
        }
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
} 