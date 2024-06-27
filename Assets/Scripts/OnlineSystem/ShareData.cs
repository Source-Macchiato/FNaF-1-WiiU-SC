using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class ShareData : MonoBehaviour
{
	private const string url = "https://api.sourcemacchiato.com/v1/fnaf/analytics";

    public float canShareData = -1;

    private bool isSent = false;
    public bool activateShareDataPanel = false;

    SaveGameState saveGameState;
    SaveManager saveManager;

    private string localUsername;
    private string localVersion;

    [System.Serializable]
    private class ShareDataResponse
    {
        public string username;
        public string version;
    }

    void Start ()
    {
        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        canShareData = SaveManager.LoadShareData();
    }
	
	void Update ()
    {
        if (!isSent)
        {
            CanShareData();
        }
    }

	private void GetData()
	{
        // Get username
        localUsername = WiiU.Core.accountName;

        // Get game version
        TextAsset versionAsset = Resources.Load<TextAsset>("Meta/version");
        localVersion = versionAsset.text;
    }

    private void CanShareData()
    {
        GetData();

        if (localUsername == "")
        {
            localUsername = "Unknown";
        }

        if (canShareData == -1)
        {
            activateShareDataPanel = true;
        }
        else if (canShareData == 1)
        {
            StartCoroutine(SendData(localUsername, localVersion));
            isSent = true;
        }
    }

    private IEnumerator SendData(string username, string version)
    {
        string jsonString = "{\"username\":\"" + username + "\",\"version\":\"" + version + "\"}";
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonString);

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        using (WWW www = new WWW(url, postData, headers))
        {
            yield return www;
        }
    }
}
