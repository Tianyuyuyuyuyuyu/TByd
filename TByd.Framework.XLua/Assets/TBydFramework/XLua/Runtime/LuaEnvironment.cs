using System;
using System.Collections;
using TBydFramework.Runtime.Execution;
using TBydFramework.Runtime.Log;
using UnityEngine;
using XLua;
using IAsyncResult = TBydFramework.Runtime.Asynchronous.IAsyncResult;

namespace TBydFramework.XLua.Runtime
{
    public static class LuaEnvironment
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LuaEnvironment));

        private static float interval = 2;
        private static WaitForSecondsRealtime wait;
        private static LuaEnv luaEnv;
        private static IAsyncResult result;

        public static float Interval
        {
            get { return interval; }
            set
            {
                if (interval <= 0)
                    return;

                interval = value;
                wait = new WaitForSecondsRealtime(interval);
            }
        }

        public static LuaEnv LuaEnv
        {
            get
            {
                if (luaEnv == null)
                {
                    luaEnv = new LuaEnv();
                    if (result != null)
                        result.Cancel();

                    wait = new WaitForSecondsRealtime(interval);
                    result = Executors.RunOnCoroutine(DoTick());
                }

                return luaEnv;
            }
        }

        public static void Dispose()
        {
            if (result != null)
            {
                result.Cancel();
                result = null;
            }

            if (luaEnv != null)
            {
                luaEnv.Dispose();
                luaEnv = null;
            }

            wait = null;
        }

        private static IEnumerator DoTick()
        {
            while (true)
            {
                yield return wait;
                try
                {
                    luaEnv.Tick();
                }
                catch (Exception e)
                {
                    if (log.IsWarnEnabled)
                        log.WarnFormat("LuaEnv.Tick Error:{0}", e);
                }
            }
        }
    }
}