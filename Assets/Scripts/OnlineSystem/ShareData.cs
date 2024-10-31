using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class ShareData : MonoBehaviour
{
	private const string url = "https://api.sourcemacchiato.com/v1/fnaf/analytics";

    private float canShareData = -1;

    private bool isSent = false;
    private bool popupDisplayed = false;

    private string localUsername;
    private string localVersion;
    private string localAuthKey;

    [System.Serializable]
    private class ShareDataResponse
    {
        public string username;
        public string version;
        public string authKey;
    }

    // Scripts
    public MenuManager menuManager;
    SaveGameState saveGameState;
    SaveManager saveManager;

    void Start()
    {
        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        canShareData = SaveManager.LoadShareData();
    }

    void Update()
    {
        if (!isSent)
        {
            CanShareData();
        }

        if (menuManager.currentPopup != null && menuManager.currentPopup.popupId == "sharedata" && menuManager.currentPopup.optionId != -1)
        {
            if (menuManager.currentPopup.optionId == 0) // The data can be shared
            {
                canShareData = 1;
                saveManager.SaveShareData(canShareData);
                bool saveResult = saveGameState.DoSave();
            }
            else if (menuManager.currentPopup.optionId == 1) // The data can't be shared
            {
                canShareData = 0;
                saveManager.SaveShareData(canShareData);
                bool saveResult = saveGameState.DoSave();
            }

            menuManager.currentPopup.optionId = -1;

            menuManager.CloseCurrentPopup();
        }
    }

	private void GetData()
	{
        // Get username
        localUsername = WiiU.Core.accountName;

        // Get game version
        TextAsset versionAsset = Resources.Load<TextAsset>("Meta/version");
        localVersion = versionAsset.text;

        // Authorization key
        localAuthKey = "Ts8ntiM40JeDmoxQUNlPBfc7w3EdAFpy";
    }

    private void CanShareData()
    {
        GetData();

        // If the username is empty replace it with "Unknown"
        if (localUsername == "")
        {
            localUsername = "Unknown";
        }

        if (canShareData == -1)
        {
            if (!popupDisplayed)
            {
                menuManager.AddPopup("mainmenu.sharedata", 1, "sharedata");

                popupDisplayed = true;
            }
        }
        else if (canShareData == 1)
        {
            StartCoroutine(SendData(localUsername, localVersion, localAuthKey));
            isSent = true;
        }
    }

    private IEnumerator SendData(string username, string version, string authKey)
    {
        string jsonString = "{\"username\":\"" + username + "\",\"version\":\"" + version + "\",\"authKey\":\"" + authKey + "\"}";
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonString);

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        using (WWW www = new WWW(url, postData, headers))
        {
            yield return www;
        }
    }
}