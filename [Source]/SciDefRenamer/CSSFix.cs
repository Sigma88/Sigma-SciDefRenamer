using UnityEngine;

namespace CSSFix
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class CSSFix: MonoBehaviour
    {
        void Start()
        {
            foreach (ConfigNode config in GameDatabase.Instance.GetConfigNodes("EXPERIMENT_DEFINITION"))
            {
                ConfigNode results = config.GetNode("RESULTS");
                ConfigNode data = new ConfigNode();
                foreach (ConfigNode.Value key in results.values)
                {
                    data.AddValue(key.name + (key.name != "default" ? "*" : ""), key.value);
                }
                results.ClearData();
                results.AddData(data);
            }
        }
    }
}
