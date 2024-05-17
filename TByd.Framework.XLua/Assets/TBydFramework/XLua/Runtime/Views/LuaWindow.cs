﻿using System;
using XLua;
using TBydFramework.Runtime.Views;
using TBydFramework.Runtime.Views.Variables;

namespace TBydFramework.XLua.Runtime.Views
{
    [LuaCallCSharp]
    public class LuaWindow : Window, ILuaExtendable
    {
        public ScriptReference script;
        public VariableArray variables;

        protected LuaTable scriptEnv;
        protected LuaTable metatable;
        protected Action<LuaWindow, IBundle> onCreate;
        protected Action<LuaWindow> onShow;
        protected Action<LuaWindow> onHide;
        protected Action<LuaWindow> onDismiss;

        private bool initialized = false;

        public virtual LuaTable GetMetatable()
        {
            return this.metatable;
        }

        protected virtual void Initialize()
        {
            if (initialized)
                return;

            initialized = true;

            var luaEnv = LuaEnvironment.LuaEnv;
            scriptEnv = luaEnv.NewTable();

            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            scriptEnv.Set("target", this);

            string scriptText = (script.Type == ScriptReferenceType.TextAsset) ? script.Text.text : string.Format("require(\"framework.System\");local cls = require(\"{0}\");return extends(target,cls);", script.Filename);
            object[] result = luaEnv.DoString(scriptText, string.Format("{0}({1})", "LuaWindow", this.name), scriptEnv);

            if (result.Length != 1 || !(result[0] is LuaTable))
                throw new Exception("");

            metatable = (LuaTable)result[0];

            if (variables != null && variables.Variables != null)
            {
                foreach (var variable in variables.Variables)
                {
                    var name = variable.Name.Trim();
                    if (string.IsNullOrEmpty(name))
                        continue;

                    metatable.Set(name, variable.GetValue());
                }
            }

            onCreate = metatable.Get<Action<LuaWindow, IBundle>>("onCreate");
            onShow = metatable.Get<Action<LuaWindow>>("onShow");
            onHide = metatable.Get<Action<LuaWindow>>("onHide");
            onDismiss = metatable.Get<Action<LuaWindow>>("onDismiss");
        }

        protected override void OnCreate(IBundle bundle)
        {
            if (!initialized)
                Initialize();

            if (onCreate != null)
                onCreate(this, bundle);
        }

        protected override void OnShow()
        {
            base.OnShow();

            if (onShow != null)
                onShow(this);
        }

        protected override void OnHide()
        {
            base.OnHide();

            if (onHide != null)
                onHide(this);
        }

        protected override void OnDismiss()
        {
            base.OnDismiss();

            if (onDismiss != null)
                onDismiss(this);

            onCreate = null;
            onShow = null;
            onHide = null;
            onDismiss = null;

            if (metatable != null)
            {
                metatable.Dispose();
                metatable = null;
            }

            if (scriptEnv != null)
            {
                scriptEnv.Dispose();
                scriptEnv = null;
            }
        }
    }
}