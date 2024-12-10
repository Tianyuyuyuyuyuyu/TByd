using UnityEngine;

namespace TBydFramework.Pool.Runtime.Core
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        private static T _instance;
        private static readonly object _lock = new();
        private static bool _isQuitting;

        public static T Instance
        {
            get
            {
                if (_isQuitting)
                {
                    Debug.LogWarning($"[{typeof(T).Name}] 在应用退出时访问单例实例。");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();
                        if (_instance == null)
                        {
                            var go = new GameObject($"[{typeof(T).Name}]");
                            _instance = go.AddComponent<T>();
                            DontDestroyOnLoad(go);
                        }
                    }
                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnApplicationQuit()
        {
            _isQuitting = true;
        }
    }
} 