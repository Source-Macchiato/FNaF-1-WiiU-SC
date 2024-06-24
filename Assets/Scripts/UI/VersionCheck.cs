using System.Collections;
using UnityEngine;

public class VersionCheck : MonoBehaviour
{
    public GameObject objectToActivate;

    [System.Serializable]
    public class VersionData
    {
        public string version;
    }

    private void Start()
    {
        objectToActivate.SetActive(false);

        StartCoroutine(CheckVersion());
    }

    IEnumerator CheckVersion()
    {
        string url = "https://api.sourcemacchiato.com/v1/fnaf/metadata";

        using (WWW www = new WWW(url))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                VersionData data = JsonUtility.FromJson<VersionData>(www.text);
                string onlineVersion = data.version;

                TextAsset localVersionAsset = Resources.Load<TextAsset>("Meta/version");
                string localVersion = localVersionAsset.text;

                if (onlineVersion.Trim() == localVersion.Trim())
                {
                    objectToActivate.SetActive(false);
                    Debug.Log("Same version number");
                }
                else
                {
                    objectToActivate.SetActive(true);
                    Debug.Log("Different version number");
                }
            }
            else
            {
                objectToActivate.SetActive(false);
                Debug.Log("Network error: " + www.error);
            }
        }
    }
}