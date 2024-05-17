using System.Collections.Generic;
using TBydFramework.Runtime.Binding.Paths;
using TBydFramework.Runtime.Binding.Proxy.Sources;
using TBydFramework.Runtime.Binding.Proxy.Sources.Object;

namespace TBydFramework.XLua.Runtime.Binding.Proxy.Sources.Expressions
{
    public class LuaExpressionSourceProxyFactory : TypedSourceProxyFactory<LuaExpressionSourceDescription>
    {
        private ISourceProxyFactory factory;
        private IPathParser pathParser;
        public LuaExpressionSourceProxyFactory(ISourceProxyFactory factory, IPathParser pathParser)
        {
            this.factory = factory;
            this.pathParser = pathParser;
        }

        private List<Path> FindPaths(string[] textPaths)
        {
            List<Path> paths = new List<Path>();
            if (textPaths == null)
                return paths;

            for (int i = 0; i < textPaths.Length; i++)
            {
                Path path = this.pathParser.Parse(textPaths[i]);
                if (path != null && !paths.Contains(path))
                    paths.Add(path);
            }

            return paths;
        }

        protected override bool TryCreateProxy(object source, LuaExpressionSourceDescription description, out ISourceProxy proxy)
        {
            proxy = null;
            if (source == null && !description.IsStatic)
            {
                proxy = new EmptSourceProxy(description);
                return true;
            }

            List<ISourceProxy> list = new List<ISourceProxy>();
            if (!description.IsStatic)
            {
                List<Path> paths = FindPaths(description.Paths);
                foreach (Path path in paths)
                {
                    ISourceProxy innerProxy = this.factory.CreateProxy(source, new ObjectSourceDescription() { Path = path });
                    if (innerProxy != null)
                        list.Add(innerProxy);
                }
            }
            proxy = new LuaExpressionSourceProxy(source, description.Expression, list);
            return true;
        }
    }
}