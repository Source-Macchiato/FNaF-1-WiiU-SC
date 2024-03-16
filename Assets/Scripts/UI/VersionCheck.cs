using System.Collections;
using UnityEngine;

public class VersionCheck : MonoBehaviour
{
    public GameObject objectToActivate;

    [System.Serializable]
    public class VersionData
    {
        public string latest;
    }

    private void Start()
    {
        objectToActivate.SetActive(false);

        StartCoroutine(CheckVersion());
    }

    IEnumerator CheckVersion()
    {
        string url = "https://itch.io/api/1/x/wharf/latest?game_id=2435336&channel_name=wup";

        using (WWW www = new WWW(url))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                VersionData data = JsonUtility.FromJson<VersionData>(www.text);
                string onlineVersion = data.latest;

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
                objectToActivate.SetActive(false);
            }
        }
    }
}