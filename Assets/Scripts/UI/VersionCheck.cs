using System.Collections;
using UnityEngine;

public class VersionCheck : MonoBehaviour
{
    public GameObject objectToActivate;

    private void Start()
    {
        StartCoroutine(CheckVersion());
    }

    IEnumerator CheckVersion()
    {
        string url = "https://api.portal-wiiu-edition.com/version.txt";

        WWW www = new WWW(url);

        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            string onlineVersion = www.text;

            TextAsset localVersionAsset = Resources.Load<TextAsset>("Meta/version");
            string localVersion = localVersionAsset.text;

            if (onlineVersion.Trim() == localVersion.Trim())
            {
                objectToActivate.SetActive(false);
            }
            else
            {
                objectToActivate.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Error connecting to URL : " + www.error);
        }

        www.Dispose();
    }
}