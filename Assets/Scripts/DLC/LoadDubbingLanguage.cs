using System.Collections;
using UnityEngine;

public class LoadDubbingLanguage : MonoBehaviour
{
    private AudioSource phoneCallAudio;
    public string objectNameToLoad;

	void Start()
	{
        phoneCallAudio = GetComponent<AudioSource>();

        StartCoroutine(LoadAsset("vf-language-pack", objectNameToLoad));
    }

    IEnumerator LoadAsset(string assetBundleName, string objectNameToLoad)
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        filePath = System.IO.Path.Combine(filePath, assetBundleName);

        var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
        yield return assetBundleCreateRequest;

        AssetBundle asseBundle = assetBundleCreateRequest.assetBundle;

        // Load asset
        AssetBundleRequest asset = asseBundle.LoadAssetAsync<AudioSource>(objectNameToLoad);
        yield return asset;

        // Get asset
        AudioSource loadedAsset = asset.asset as AudioSource;

        // Assign asset
        phoneCallAudio.clip = loadedAsset.clip;
    }
}