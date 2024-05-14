using System.Collections.Generic;
using System.Linq.Expressions;

namespace TBydFramework.Runtime.Binding.Paths
{
    public interface IExpressionPathFinder
    {
        List<Path> FindPaths(LambdaExpression expression);
        
    }
}
