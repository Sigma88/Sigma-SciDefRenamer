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
                foreach (ConfigNode node in SSDR.nodes)
                {
                    if (node.name == "Copy" && node.HasValue("SOURCE") && node.HasValue("NEW"))
                    {
                        string SOURCE = node.GetValue("SOURCE");
                        string NEW = node.GetValue("NEW");
                        Copy(SOURCE, NEW);
                    }
                    if (node.name == "Delete" && node.HasValue("NAME"))
                    {
                        string NAME = node.GetValue("NAME");
                        Delete(NAME);
                    }
                    if (node.name == "Rename" && node.HasValue("OLD") && node.HasValue("NEW"))
                    {
                        string OLD = node.GetValue("OLD");
                        string NEW = node.GetValue("NEW");
                        Copy(OLD, NEW);
                        Delete(OLD);
                    }
                    if (node.name == "Replace" && node.HasValue("FIND") && node.HasValue("REPLACE"))
                    {
                        string FIND = node.GetValue("FIND");
                        string REPLACE = node.GetValue("REPLACE");
                        string PLANET = node.HasValue("PLANET") ? node.GetValue("PLANET") : "";
                        Replace(FIND, REPLACE, PLANET);
                    }
                }
            }
        }
        void Copy(string SOURCE, string NEW)
        {
            foreach (ConfigNode config in GameDatabase.Instance.GetConfigNodes("EXPERIMENT_DEFINITION"))
            {
                ConfigNode results = config.GetNode("RESULTS");
                ConfigNode data = new ConfigNode();
                foreach (ConfigNode.Value key in results.values)
                {
                    if (key.name.StartsWith(SOURCE))
                    {
                        data.AddValue(NEW + key.name.Remove(0, SOURCE.Length), key.value);
                    }
                }
                results.AddData(data);
            }
        }
        void Delete(string NAME)
        {
            foreach (ConfigNode config in GameDatabase.Instance.GetConfigNodes("EXPERIMENT_DEFINITION"))
            {
                ConfigNode results = config.GetNode("RESULTS");
                results.RemoveValuesStartWith(NAME);
            }
        }
        void Replace(string FIND, string REPLACE, string PLANET)
        {
            foreach (ConfigNode config in GameDatabase.Instance.GetConfigNodes("EXPERIMENT_DEFINITION"))
            {
                ConfigNode results = config.GetNode("RESULTS");
                ConfigNode data = new ConfigNode();
                foreach (ConfigNode.Value key in results.values)
                {
                    if (key.name.StartsWith(PLANET))
                    {
                        data.AddValue(key.name.Replace(FIND, REPLACE), key.value);
                    }
                }
                results.RemoveValuesStartWith(PLANET);
                results.AddData(data);
            }
        }
    }
}
