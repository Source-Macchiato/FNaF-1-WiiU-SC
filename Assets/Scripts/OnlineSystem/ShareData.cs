using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class ShareData : MonoBehaviour
{
    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    public float joystickThreshold = 0.5f;
    public float buttonChangeDelay = 0.2f;

    public int selectedButtonIndex = 0;

    public Text[] shareDataSelectionTexts;
    public Button[] shareDataButtons;

    private GameObject shareDataPanel;

	private const string url = "https://api.sourcemacchiato.com/v1/fnaf/analytics";

    private float canShareData = -1;

    private bool isSent = false;

    SaveGameState saveGameState;
    SaveManager saveManager;
    MainMenu mainMenu;

    private string localUsername;
    private string localVersion;

    [System.Serializable]
    private class ShareDataResponse
    {
        public string username;
        public string version;
    }

    void Awake ()
    {
        shareDataPanel = GameObject.Find("ShareDataPanel");
    }

    void Start ()
    {
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();
        mainMenu = FindObjectOfType<MainMenu>();

        shareDataPanel.SetActive(false);

        canShareData = SaveManager.LoadShareData();

        UpdateSelectionTexts();
    }
	
	void Update ()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        if (!isSent)
        {
            CanShareData();
        }

        if (shareDataPanel.activeSelf)
        {
            // Gamepad
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsReleased(WiiU.GamePadButton.Left))
                {
                    selectedButtonIndex = (selectedButtonIndex - 1 + shareDataButtons.Length) % shareDataButtons.Length;
                    UpdateSelectionTexts();
                }

                if (gamePadState.IsReleased(WiiU.GamePadButton.Right))
                {
                    selectedButtonIndex = (selectedButtonIndex + 1) % shareDataButtons.Length;
                    UpdateSelectionTexts();
                }

                if (gamePadState.IsReleased(WiiU.GamePadButton.A))
                {
                    if (selectedButtonIndex == 0)
                    {
                        canShareData = 1;
                        saveManager.SaveShareData(canShareData);
                        bool saveResult = saveGameState.DoSave();

                        shareDataPanel.SetActive(false);

                        mainMenu.canChangeButton = true;
                    }
                    else if (selectedButtonIndex == 1)
                    {
                        canShareData = 0;
                        saveManager.SaveShareData(canShareData);
                        bool saveResult = saveGameState.DoSave();

                        shareDataPanel.SetActive(false);

                        mainMenu.canChangeButton = true;
                    }
                }
            }

            // Remote
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsReleased(WiiU.ProControllerButton.Left))
                    {
                        selectedButtonIndex = (selectedButtonIndex - 1 + shareDataButtons.Length) % shareDataButtons.Length;
                        UpdateSelectionTexts();
                    }

                    if (remoteState.pro.IsReleased(WiiU.ProControllerButton.Right))
                    {
                        selectedButtonIndex = (selectedButtonIndex + 1) % shareDataButtons.Length;
                        UpdateSelectionTexts();
                    }

                    if (remoteState.pro.IsReleased(WiiU.ProControllerButton.A))
                    {
                        if (selectedButtonIndex == 0)
                        {
                            canShareData = 1;
                            saveManager.SaveShareData(canShareData);
                            bool saveResult = saveGameState.DoSave();

                            shareDataPanel.SetActive(false);

                            mainMenu.canChangeButton = true;
                        }
                        else if (selectedButtonIndex == 1)
                        {
                            canShareData = 0;
                            saveManager.SaveShareData(canShareData);
                            bool saveResult = saveGameState.DoSave();

                            shareDataPanel.SetActive(false);

                            mainMenu.canChangeButton = true;
                        }
                    }
                    break;
                default:
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    selectedButtonIndex = (selectedButtonIndex - 1 + shareDataButtons.Length) % shareDataButtons.Length;
                    UpdateSelectionTexts();
                }

                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    selectedButtonIndex = (selectedButtonIndex + 1) % shareDataButtons.Length;
                    UpdateSelectionTexts();
                }

                if (Input.GetKeyUp(KeyCode.Return))
                {
                    if (selectedButtonIndex == 0)
                    {
                        canShareData = 1;
                        saveManager.SaveShareData(canShareData);
                        bool saveResult = saveGameState.DoSave();

                        shareDataPanel.SetActive(false);

                        mainMenu.canChangeButton = true;
                    }
                    else if (selectedButtonIndex == 1)
                    {
                        canShareData = 0;
                        saveManager.SaveShareData(canShareData);
                        bool saveResult = saveGameState.DoSave();

                        shareDataPanel.SetActive(false);

                        mainMenu.canChangeButton = true;
                    }
                }
            }
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
            shareDataPanel.SetActive(true);
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

    public void UpdateSelectionTexts()
    {
        Text[] currentSelectionTexts = shareDataSelectionTexts;

        for (int i = 0; i < shareDataButtons.Length; i++)
        {
            currentSelectionTexts[i].gameObject.SetActive(i == selectedButtonIndex);
        }
    }
}
