using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager : MonoBehaviour
{
    private Dictionary<string, AssetBundle> loadedAssetBundles = new Dictionary<string, AssetBundle>();

    void Start()
	{
		StartCoroutine(LoadAssetBundle("vo-language-pack"));
	}
	
	private IEnumerator LoadAssetBundle(string assetBundleName)
	{
        // Check if asset bundle has already been loaded
        if (loadedAssetBundles.ContainsKey(assetBundleName))
        {
            yield break;
        }

        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        filePath = System.IO.Path.Combine(filePath, assetBundleName);

        var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
        yield return assetBundleCreateRequest;

        AssetBundle assetBundle = assetBundleCreateRequest.assetBundle;

        if (assetBundle != null)
        {
            loadedAssetBundles[assetBundleName] = assetBundle;
        }
    }

    public AssetBundle GetAssetBundle(string assetBundleName)
    {
        AssetBundle assetBundle;

        if (loadedAssetBundles.TryGetValue(assetBundleName, out assetBundle))
        {
            return assetBundle;
        }
        else
        {
            return null;
        }
    }
}