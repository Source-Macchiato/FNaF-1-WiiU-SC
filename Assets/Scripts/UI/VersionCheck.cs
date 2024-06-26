using System.Collections;
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class VersionCheck : MonoBehaviour
{
    private GameObject updatePanel;
    private GameObject shareDataPanel;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    MainMenu mainMenu;

    [System.Serializable]
    public class VersionData
    {
        public string version;
    }

    private void Awake()
    {
        updatePanel = GameObject.Find("UpdatePanel");
        shareDataPanel = GameObject.Find("ShareDataPanel");
    }

    private void Start()
    {
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        updatePanel.SetActive(false);

        mainMenu = FindObjectOfType<MainMenu>();

        StartCoroutine(CheckVersion());
    }

    private void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        if (updatePanel.activeSelf && !shareDataPanel.activeSelf)
        {
            // Gamepad
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
                {
                    updatePanel.SetActive(false);

                    mainMenu.canChangeButton = true;
                }
            }

            // Remote
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.A))
                    {
                        updatePanel.SetActive(false);

                        mainMenu.canChangeButton = true;
                    }
                    break;

                default:
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyUp(KeyCode.Return))
                {
                    updatePanel.SetActive(false);

                    mainMenu.canChangeButton = true;
                }
            }
        }
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
                    updatePanel.SetActive(false);
                    Debug.Log("Same version number");
                }
                else
                {
                    updatePanel.SetActive(true);
                    Debug.Log("Different version number");
                }
            }
            else
            {
                updatePanel.SetActive(false);
                Debug.Log("Network error: " + www.error);
            }
        }
    }
}