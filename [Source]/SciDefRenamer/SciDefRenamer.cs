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
                    if (node.name == "Copy")
                    {
                        string OLD = node.GetValue("OLD");
                        string NEW = node.GetValue("NEW");
                        Copy(OLD, NEW);
                    }
                    if (node.name == "Delete")
                    {
                        string NAME = node.GetValue("NAME");
                        Delete(OLD);
                    }
                    if (node.name == "Rename")
                    {
                        string OLD = node.GetValue("OLD");
                        string NEW = node.GetValue("NEW");
                        Copy(OLD, NEW);
                        Delete(OLD);
                    }
                    if (node.name == "Replace")
                    {
                        string OLD = node.GetValue("OLD");
                        string NEW = node.GetValue("NEW");
                        Replace(OLD, NEW);
                    }
                }
            }
        }
        void Copy(string OLD, string NEW)
        {
            foreach (ConfigNode config in GameDatabase.Instance.GetConfigNodes("EXPERIMENT_DEFINITION"))
            {
                ConfigNode results = config.GetNode("RESULTS");
                ConfigNode data = new ConfigNode();
                foreach (ConfigNode.Value key in results.values)
                {
                    if (key.name.StartsWith(OLD))
                    {
                        data.AddValue( NEW + key.name.Replace(0, OLD.Length), key.value);
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
        void Replace(string OLD, string NEW)
        {
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
