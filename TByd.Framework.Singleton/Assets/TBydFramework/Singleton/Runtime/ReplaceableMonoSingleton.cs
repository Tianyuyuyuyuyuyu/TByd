using UnityEngine;

namespace TBydFramework.Singleton.Runtime
{
    /// <summary>
    /// 如果跳转到新的场景里已经有了实例，则删除已有示例，再创建新的实例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReplaceableMonoSingleton<T> : MonoBehaviour where T : Component
    {
        protected static T mInstance;
        public float InitializationTime;

        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = FindObjectOfType<T>();
                    if (mInstance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        mInstance = obj.AddComponent<T>();
                    }
                }

                return mInstance;
            }
        }

        /// <summary>
        /// On awake, 检查场景中是否已有该对象的副本。如果有，就销毁它。
        /// </summary>
        protected virtual void Awake()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            InitializationTime = Time.time;

            DontDestroyOnLoad(this.gameObject);
            //检查是否存在相同类型的对象
            T[] check = FindObjectsOfType<T>();
            foreach (T searched in check)
            {
                if (searched != this)
                {
                    //如果发现另一个相同类型的对象（不是这个），并且如果它比当前的对象更旧，就销毁它。
                    if (searched.GetComponent<ReplaceableMonoSingleton<T>>().InitializationTime < InitializationTime)
                    {
                        Destroy(searched.gameObject);
                    }
                }
            }

            if (mInstance == null)
            {
                mInstance = this as T;
            }
        }
    }
}