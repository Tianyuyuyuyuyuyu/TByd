using UnityEngine;
using Object = UnityEngine.Object;

namespace TBydFramework.Pool.Runtime.Validation
{
    public class UnityObjectValidator<T> : IPoolObjectValidator<T> where T : class
    {
        private readonly bool _checkEnabled;
        private readonly bool _checkActiveInHierarchy;
        private readonly bool _checkDestroyed;

        public UnityObjectValidator(bool checkEnabled = true, 
            bool checkActiveInHierarchy = false, 
            bool checkDestroyed = true)
        {
            _checkEnabled = checkEnabled;
            _checkActiveInHierarchy = checkActiveInHierarchy;
            _checkDestroyed = checkDestroyed;
        }

        public bool Validate(T obj)
        {
            if (obj == null) return false;

            // 检查是否为Unity对象
            if (obj is Object unityObj && _checkDestroyed)
            {
                if (unityObj == null) return false;
            }

            // GameObject特定检查
            if (obj is GameObject go)
            {
                if (_checkEnabled && !go.activeSelf) return false;
                if (_checkActiveInHierarchy && !go.activeInHierarchy) return false;
            }
            // Component特定检查
            else if (obj is Component comp)
            {
                if (comp.gameObject == null) return false;
                if (_checkEnabled && !comp.gameObject.activeSelf) return false;
                if (_checkActiveInHierarchy && !comp.gameObject.activeInHierarchy) return false;
            }

            return true;
        }

        public ValidationResult GetValidationDetails(T obj)
        {
            if (obj == null)
                return ValidationResult.Failure("对象为空", ValidationSeverity.Critical);

            if (obj is Object unityObj && _checkDestroyed && unityObj == null)
                return ValidationResult.Failure("Unity对象已被销毁", ValidationSeverity.Critical);

            if (obj is GameObject go)
            {
                if (_checkEnabled && !go.activeSelf)
                    return ValidationResult.Failure("GameObject未启用", ValidationSeverity.Warning);
                if (_checkActiveInHierarchy && !go.activeInHierarchy)
                    return ValidationResult.Failure("GameObject在层级中未激活", ValidationSeverity.Warning);
            }
            else if (obj is Component comp)
            {
                if (comp.gameObject == null)
                    return ValidationResult.Failure("组件所属GameObject已被销毁", ValidationSeverity.Critical);
                if (_checkEnabled && !comp.gameObject.activeSelf)
                    return ValidationResult.Failure("组件所属GameObject未启用", ValidationSeverity.Warning);
                if (_checkActiveInHierarchy && !comp.gameObject.activeInHierarchy)
                    return ValidationResult.Failure("组件所属GameObject在层级中未激活", ValidationSeverity.Warning);
            }

            return ValidationResult.Success();
        }
    }
} 