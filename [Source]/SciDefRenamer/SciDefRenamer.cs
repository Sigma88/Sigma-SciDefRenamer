using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using System.IO;
using System.Collections.Generic;


namespace SigmaSciDefRenamer
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class SigmaSciDefRenamer : MonoBehaviour
    {
        void Start()
        {
            foreach (ConfigNode SSDR in GameDatabase.Instance.GetConfigNodes("SigmaSciDefRenamer"))
            {
                foreach (ConfigNode rename in SSDR.GetNodes("Rename"))
                {
                    string OLD = rename.GetValue("OLD");
                    string NEW = rename.GetValue("NEW");
                    foreach (ConfigNode config in GameDatabase.Instance.GetConfigNodes("EXPERIMENT_DEFINITION"))
                    {
                        ConfigNode results = config.GetNode("RESULTS");
                        ConfigNode data = new ConfigNode();
                        foreach (ConfigNode.Value key in results.values)
                        {
                            if (key.name.StartsWith(OLD))
                            {
                                data.AddValue(key.name.Replace(OLD, NEW), key.value);
                            }
                        }
                        results.RemoveValuesStartWith(OLD);
                        results.AddData(data);
                    }
                }
            }
        }
    }
}
