using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoadBundles : MonoBehaviour
{
    [SerializeField]
    private string _sceneAssetBundleName = "mainscene";
    [SerializeField]
    private string _assetBundleWithAssetBundleManifestName = "AssetBundles";

    private void Start()
    {
        StartCoroutine(LoadAssetBundleViaWebRequest(_sceneAssetBundleName, _assetBundleWithAssetBundleManifestName));
    }

    private void LoadAssetBundleViaFileSystem(string assetBundleName, string assetBundleWithAssetBundleManifestName)
    {
        // get manifest
        var assetBundleWithAssetBundleManifest = LoadAssetBundleFromFile(assetBundleWithAssetBundleManifestName);
        var assetBundleManifest = assetBundleWithAssetBundleManifest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        // get dependent asset bundles
        var dependenciesNames = assetBundleManifest.GetAllDependencies(assetBundleName);
        var dependentAssetBundles = new List<AssetBundle>();
        foreach (var dependencyName in dependenciesNames)
        {
            var dependentAssetBundle = LoadAssetBundleFromFile(dependencyName);
            dependentAssetBundles.Add(dependentAssetBundle);
        }

        // get asset bundle
        var assetBundle = LoadAssetBundleFromFile(assetBundleName);

        if (assetBundle.isStreamedSceneAssetBundle) // its a scene
        {
            var scenePaths = assetBundle.GetAllScenePaths();
            var sceneName = Path.GetFileNameWithoutExtension(scenePaths[0]);
            SceneManager.LoadScene(sceneName);
        }
    }

    private AssetBundle LoadAssetBundleFromFile(string assetBundleName)
    {
        var assetBundlePath = Path.Combine(Application.dataPath + "/AssetBundles/" + assetBundleName);
        var assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
        return assetBundle;
    }

    private IEnumerator LoadAssetBundleViaWebRequest(string assetBundleName, string assetBundleWithAssetBundleManifestName)
    {
        // get manifest
        var uri = BuildUriForAssetBundleName(assetBundleWithAssetBundleManifestName);
        var request = UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);
        yield return request.SendWebRequest();

        var assetBundleWithAssetBundleManifest = DownloadHandlerAssetBundle.GetContent(request);
        var assetBundleManifest = assetBundleWithAssetBundleManifest.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        yield return null; // ?

        //get dependent asset bundles
        var dependenciesNames = assetBundleManifest.GetAllDependencies(assetBundleName);
        var dependentAssetBundles = new List<AssetBundle>();
        foreach (var dependencyName in dependenciesNames)
        {
            var uri1 = BuildUriForAssetBundleName(dependencyName);
            var request1 = UnityWebRequestAssetBundle.GetAssetBundle(uri1, 0);
            yield return request1.SendWebRequest();
            var dependentAssetBundle = DownloadHandlerAssetBundle.GetContent(request1);
            dependentAssetBundles.Add(dependentAssetBundle);
        }
        yield return null; // ?

        // get asset bundle
        uri = BuildUriForAssetBundleName(assetBundleName);
        request = UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);
        yield return request.SendWebRequest();
        var assetBundle = DownloadHandlerAssetBundle.GetContent(request);

        if (assetBundle.isStreamedSceneAssetBundle) // its a scene
        {
            var scenePaths = assetBundle.GetAllScenePaths();
            var sceneName = Path.GetFileNameWithoutExtension(scenePaths[0]);
            SceneManager.LoadScene(sceneName);
        }
    }

    private string BuildUriForAssetBundleName(string assetBundleName)
    {
        var result = "file:///" + Application.dataPath + "/AssetBundles/" + assetBundleName;

        return result;
    }
}
