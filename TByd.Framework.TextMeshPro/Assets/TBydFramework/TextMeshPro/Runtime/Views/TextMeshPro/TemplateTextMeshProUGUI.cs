using TBydFramework.TextFormatting.Runtime;
using TMPro;
using UnityEngine;
using static TBydFramework.TextMeshPro.Runtime.Views.TextMeshPro.IFormattableText;

namespace TBydFramework.TextMeshPro.Runtime.Views.TextMeshPro
{
    public class TemplateTextMeshProUGUI : TextMeshProUGUI
    {
        [SerializeField]
        [TextArea(5, 10)]
        private string m_Template;
        private object data;
        private TextTemplateBinding templateBinding;

        protected TextTemplateBinding Binding
        {
            get
            {
                if (templateBinding == null)
                    templateBinding = new TextTemplateBinding(SetText);
                return templateBinding;
            }
        }

        public string Template
        {
            get { return this.m_Template; }
            set
            {
                if (string.Equals(this.m_Template, value))
                    return;

                this.m_Template = value;
                Binding.Template = this.m_Template;
            }
        }
        public object Data
        {
            get { return this.data; }
            set
            {
                if (Equals(this.data, value))
                    return;

                this.data = value;
                Binding.Data = this.data;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Initialize();
        }

        public override void SetAllDirty()
        {
            base.SetAllDirty();
            Initialize();
        }

        protected virtual void Initialize()
        {
            if (this.data == null)
                SetText(BUFFER.Clear().Append(m_Template));
        }

        protected override void OnDestroy()
        {
            if (templateBinding != null)
            {
                templateBinding.Dispose();
                templateBinding = null;
            }
            base.OnDestroy();
        }
    }
}
