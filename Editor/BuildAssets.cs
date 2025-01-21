using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace litefeel.commonscripts
{

    public static class BuildAssets
    {


        [MenuItem("Assets/CommonScripts/BuildSelectAssets")]
        private static void BuildSelectAssets()
        {
            var path = Application.streamingAssetsPath;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var list = Selection.objects
                .Select(o => AssetDatabase.GetAssetPath(o))
                .Where(a => a != null)
                .ToList();

            if (list.Count == 0)
                return;

            var abbs = new AssetBundleBuild[list.Count];
            for (var i = 0; list.Count > i; i++)
            {
                abbs[i] = new AssetBundleBuild()
                {
                    assetBundleName = list[i],
                    assetNames = new string[] { list[i] }
                };
            }

            BuildPipeline.BuildAssetBundles(path, abbs, BuildAssetsOptions.Options, EditorUserBuildSettings.activeBuildTarget);
        }

        [MenuItem("Assets/CommonScripts/BuildSelectAssetsByABName")]
        private static void BuildSelectAssetsByABName()
        {
            var path = Application.streamingAssetsPath;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            var abbs = Selection.objects
                .Select(o => AssetDatabase.GetAssetPath(o))
                .Where(a => a != null)
                .Distinct()
                .GroupBy(x => AssetImporter.GetAtPath(x).assetBundleName ?? "")
                .Where(a => !string.IsNullOrWhiteSpace(a.Key))
                .Select(a => new AssetBundleBuild() { assetBundleName = a.Key, assetNames = a.ToArray() })
                .ToArray();

            BuildPipeline.BuildAssetBundles(path, abbs, BuildAssetsOptions.Options, EditorUserBuildSettings.activeBuildTarget);
            Debug.Log($"BuildAssetBundleCount:{abbs.Length}");

        }
    }
}