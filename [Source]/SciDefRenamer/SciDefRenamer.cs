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
            bool debug = false;

            foreach (ConfigNode SSDR in GameDatabase.Instance.GetConfigNodes("SigmaSciDefRenamer"))
            {
                if (SSDR.HasValue("debug") && SSDR.GetValue("debug").Equals("true", StringComparison.OrdinalIgnoreCase))
                    debug = true;
            }

            if (debug) PrintLog("Before");

            foreach (ConfigNode SSDR in GameDatabase.Instance.GetConfigNodes("SigmaSciDefRenamer"))
            {
                foreach (ConfigNode node in SSDR.nodes)
                {
                    if (node.name == "Copy" && node.HasValue("SOURCE") && node.HasValue("NEW"))
                    {
                        string SOURCE = node.GetValue("SOURCE");
                        string NEW = node.GetValue("NEW");
                        Copy(SOURCE, NEW, false);
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
                        Copy(OLD, NEW, false);
                        Delete(OLD);
                    }
                    if (node.name == "Replace" && node.HasValue("FIND") && node.HasValue("REPLACE"))
                    {
                        string FIND = node.GetValue("FIND");
                        string REPLACE = node.GetValue("REPLACE");
                        string PLANET = node.HasValue("PLANET") ? node.GetValue("PLANET") : "";
                        Replace(FIND, REPLACE, PLANET);
                    }
                    if (node.name == "Swap" && node.HasValue("THIS") && node.HasValue("THAT"))
                    {
                        string THIS = node.GetValue("THIS");
                        string THAT = node.GetValue("THAT");
                        Copy(THIS, THAT, true);
                    }
                }
            }

            if (debug) PrintLog("After");


            // Fix for multiples reports

            foreach (ConfigNode config in GameDatabase.Instance.GetConfigNodes("EXPERIMENT_DEFINITION"))
            {
                if (!config.HasNode("RESULTS")) continue;
                ConfigNode results = config.GetNode("RESULTS");

                ConfigNode data = new ConfigNode();
                foreach (ConfigNode.Value key in results.values)
                {
                    if (key.name[key.name.Length - 1] != '*')
                        data.AddValue(key.name + (key.name != "default" ? "*" : ""), key.value);
                }

                results.ClearData();
                results.AddData(data);
            }
        }

        void Copy(string SOURCE, string NEW, bool SWAP)
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
                    if (SWAP && key.name.StartsWith(NEW))
                    {
                        data.AddValue(SOURCE + key.name.Remove(0, NEW.Length), key.value);
                    }
                }
                if (SWAP)
                {
                    results.RemoveValuesStartWith(SOURCE);
                    results.RemoveValuesStartWith(NEW);
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

        void PrintLog(string s)
        {
            List<string> SciDefs = new List<string>();
            foreach (ConfigNode config in GameDatabase.Instance.GetConfigNodes("EXPERIMENT_DEFINITION"))
            {
                SciDefs.Add("EXPERIMENT_DEFINITION");
                SciDefs.Add("{");
                if (config.HasValue("id"))
                    SciDefs.Add("\tid = " + config.GetValue("id"));

                if (!config.HasNode("RESULTS")) continue;
                ConfigNode results = config.GetNode("RESULTS");
                SciDefs.Add("\tRESULTS");
                SciDefs.Add("\t{");

                foreach (ConfigNode.Value key in results.values)
                {
                    SciDefs.Add("\t\t" + key.name + " = " + key.value);
                }

                SciDefs.Add("\t}");
                SciDefs.Add("}");


                Directory.CreateDirectory(KSPUtil.ApplicationRootPath + "GameData/Sigma/SciDefRenamer/Logs");
                File.WriteAllLines(KSPUtil.ApplicationRootPath + "GameData/Sigma/SciDefRenamer/Logs/SciDefRenamerLog_" + s + ".log", SciDefs.ToArray());
            }
        }
    }
}
