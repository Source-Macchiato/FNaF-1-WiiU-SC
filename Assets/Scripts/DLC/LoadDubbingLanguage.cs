using System.Collections;
using UnityEngine;

public class LoadDubbingLanguage : MonoBehaviour
{
    public AudioSource phoneCallAudio;
    public string objectNameToLoad;
    private float nightNumber;

	void Start()
	{
        nightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

        StartCoroutine(LoadAsset("vo-language-pack", objectNameToLoad));
    }

    IEnumerator LoadAsset(string assetBundleName, string objectNameToLoad)
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        filePath = System.IO.Path.Combine(filePath, assetBundleName);

        var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
        yield return assetBundleCreateRequest;

        AssetBundle assetBundle = assetBundleCreateRequest.assetBundle;

        if (assetBundle != null)
        {
            // Load asset
            AssetBundleRequest asset = assetBundle.LoadAssetAsync<AudioClip>(objectNameToLoad);
            yield return asset;

            // Get audio clip
            AudioClip loadedAsset = asset.asset as AudioClip;

            // Assign audio
            phoneCallAudio.clip = loadedAsset;

            // Play audio
            phoneCallAudio.Play();
        }
    }
}