using UnityEditor;

namespace litefeel.commonscripts
{
	public class BuildAssetsOptions
	{
        public static BuildAssetBundleOptions Options { get; private set; } = BuildAssetBundleOptions.ForceRebuildAssetBundle | BuildAssetBundleOptions.ChunkBasedCompression;


        const string Menu_BuildOptions_ForceRebuildAssetBundle = "Assets/CommonScripts/BuildOptions/ForceRebuildAssetBundle";
        const string Menu_BuildOptions_ChunkBasedCompression = "Assets/CommonScripts/BuildOptions/ChunkBasedCompression";


        [MenuItem(Menu_BuildOptions_ForceRebuildAssetBundle)]
        private static void BuildOptions_ForceRebuildAssetBundle()
        {
            var isChecked = !Menu.GetChecked(Menu_BuildOptions_ForceRebuildAssetBundle);
            Menu.SetChecked(Menu_BuildOptions_ForceRebuildAssetBundle, isChecked);
            if (isChecked)
                Options |= BuildAssetBundleOptions.ForceRebuildAssetBundle;
            else
                Options ^= ~BuildAssetBundleOptions.ForceRebuildAssetBundle;
        }

        [MenuItem(Menu_BuildOptions_ChunkBasedCompression)]
        private static void BuildOptions_ChunkBasedCompression()
        {
            var isChecked = !Menu.GetChecked(Menu_BuildOptions_ChunkBasedCompression);
            Menu.SetChecked(Menu_BuildOptions_ChunkBasedCompression, isChecked);
            if (isChecked)
                Options |= BuildAssetBundleOptions.ChunkBasedCompression;
            else
                Options ^= ~BuildAssetBundleOptions.ChunkBasedCompression;
        }


        [InitializeOnLoadMethod]
        public static void Init()
        {
            Menu.SetChecked(Menu_BuildOptions_ChunkBasedCompression, (Options & BuildAssetBundleOptions.ChunkBasedCompression) != 0);
            Menu.SetChecked(Menu_BuildOptions_ForceRebuildAssetBundle, (Options & BuildAssetBundleOptions.ForceRebuildAssetBundle) != 0);

        }
    }

	
}