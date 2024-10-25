using System.Collections;
using UnityEngine;

public class LoadDubbingLanguage : MonoBehaviour
{
    public AudioSource phoneCallAudio;
    private string bundleName;
    private string audioName;
    private float nightNumber;
    private string dubbingLanguage;

    AssetBundleManager assetBundleManager;

	void Start()
	{
        nightNumber = SaveManager.LoadNightNumber();

        assetBundleManager = FindObjectOfType<AssetBundleManager>();

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

            // Play the dubbing
            if (assetBundleManager != null)
            {
                StartCoroutine(PlayAudio(assetBundleManager.GetAssetBundle(bundleName), audioName));
            }
        } 
    }

    private IEnumerator PlayAudio(AssetBundle assetBundle, string objectNameToLoad)
    {
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