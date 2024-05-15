using UnityEngine;
using TBydFramework.Runtime.Views.Variables;

namespace TBydFramework.ILRuntime.Runtime.Views.UI
{

    public class DatabindingVariables : MonoBehaviour
    {
        public VariableArray variablesArray;

        public T Get<T>(string name)
        {
            return variablesArray.Get<T>(name);
        }
    }
}
