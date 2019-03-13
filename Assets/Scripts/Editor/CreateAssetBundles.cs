using System.IO;
using UnityEditor;

public class AssetBundles
{
    [MenuItem("AssetBundles/BuildAssetBundles")]
    private static void BuildAssetBundles()
    {
        var assetBundleDirectory = "Assets/AssetBundles";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows);
    }
}
