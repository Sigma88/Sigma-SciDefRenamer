using UnityEngine;


namespace SigmaSciDefRenamer
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class Version : MonoBehaviour
    {
        public static readonly System.Version number = new System.Version("0.3.2");

        void Awake()
        {
            Debug.Log("[SigmaLog] Version Check:   Sigma SciDefRenamer v" + number);
        }
    }
}
