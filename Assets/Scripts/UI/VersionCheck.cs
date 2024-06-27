using System.Collections;
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class VersionCheck : MonoBehaviour
{
    private GameObject updatePanel;
    private GameObject shareDataPanel;

    private bool canChangeButton = false;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    MainMenu mainMenu;
    MenuNavigation menuNavigation;

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
        menuNavigation = FindObjectOfType<MenuNavigation>();

        StartCoroutine(CheckVersion());
    }

    private void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        if (updatePanel.activeSelf && !shareDataPanel.activeSelf)
        {
            StartCoroutine(EnableButtonChangeAfterDelay());
        }

        if (canChangeButton)
        {
            // Gamepad
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
                {
                    updatePanel.SetActive(false);
                }
            }

            // Remote
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.A))
                    {
                        updatePanel.SetActive(false);
                    }
                    break;

                default:
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    updatePanel.SetActive(false);
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

    IEnumerator EnableButtonChangeAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        canChangeButton = true;
    }
}