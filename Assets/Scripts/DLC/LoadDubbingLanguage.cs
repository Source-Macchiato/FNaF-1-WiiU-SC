using System.Collections;
using UnityEngine;

public class LoadDubbingLanguage : MonoBehaviour
{
    public AudioSource phoneCallAudio;
    private string bundleName;
    private string audioName;
    private float nightNumber;
    private string dubbingLanguage;

	void Start()
	{
        nightNumber = SaveManager.LoadNightNumber();        

        if (nightNumber >= 0 && nightNumber <= 4)
        {
            // Get dubbing language
            dubbingLanguage = SaveManager.LoadDubbingLanguage();

            // Assign bundleName and audioName variables
            if (dubbingLanguage == null || dubbingLanguage == "en")
            {
                bundleName = "vo-language-pack";

                audioName = "VO-Call" + (nightNumber + 1);
            }
            else
            {
                bundleName = dubbingLanguage + "-language-pack";

                audioName = dubbingLanguage.ToUpper() + "-Call" + (nightNumber + 1);
            }

            // Load and play the dubbing
            StartCoroutine(LoadAndPlayAudio(bundleName, audioName));
        } 
    }

    IEnumerator LoadAndPlayAudio(string assetBundleName, string objectNameToLoad)
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