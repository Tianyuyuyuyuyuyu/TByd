using UnityEngine;

namespace TBydFramework.XLua.Runtime.Views
{
    public enum ScriptReferenceType
    {
        TextAsset,
        Filename
    }

    [System.Serializable]
    public class ScriptReference : ISerializationCallbackReceiver
    {
#if UNITY_EDITOR
        [SerializeField]
        private Object cachedAsset;
#endif
        [SerializeField]
        protected TextAsset text;

        [SerializeField]
        protected string filename;

        [SerializeField]
        protected ScriptReferenceType type = ScriptReferenceType.TextAsset;

        public virtual ScriptReferenceType Type
        {
            get { return this.type; }
        }

        public virtual TextAsset Text
        {
            get { return this.text; }
        }

        public virtual string Filename
        {
            get { return this.filename; }
        }

        public void OnAfterDeserialize()
        {
            Clear();
        }

        public void OnBeforeSerialize()
        {
            Clear();
        }

        protected virtual void Clear()
        {
#if !UNITY_EDITOR
            switch (type)
            {
                case ScriptReferenceType.TextAsset:
                    this.filename = null;
                    break;
                case ScriptReferenceType.Filename:
                    this.text = null;
                    break;
            }
#endif
        }
    }
}