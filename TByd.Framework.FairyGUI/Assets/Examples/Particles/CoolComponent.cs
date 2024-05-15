using TBydFramework.FairyGUI.Runtime.Core;
using TBydFramework.FairyGUI.Runtime.UI;
using TBydFramework.FairyGUI.Runtime.Utils;
using UnityEngine;

public class CoolComponent : GComponent
{
    public override void ConstructFromXML(XML cxml)
    {
        base.ConstructFromXML(cxml);

        GGraph graph = this.GetChild("effect").asGraph;

        Object prefab = Resources.Load("Flame");
        GameObject go = (GameObject)Object.Instantiate(prefab);
        graph.SetNativeObject(new GoWrapper(go));
    }
}
