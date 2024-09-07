using System.Collections;
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class VersionCheck : MonoBehaviour
{
    public GameObject popupPrefab;

    private bool canChangeButton = false;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    // Scripts
    public MenuManager menuManager;

    [System.Serializable]
    public class VersionData
    {
        public string version;
    }

    private void Start()
    {
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        popupPrefab.SetActive(false);

        StartCoroutine(CheckVersion());
    }

    private void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        if (popupPrefab.activeSelf)
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
                    popupPrefab.SetActive(false);
                    menuManager.currentPopup = null;
                }
            }

            // Remote
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.A))
                    {
                        popupPrefab.SetActive(false);
                        menuManager.currentPopup = null;
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
                    popupPrefab.SetActive(false);
                    menuManager.currentPopup = null;
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
                    popupPrefab.SetActive(false);
                    Debug.Log("Same version number");
                }
                else
                {
                    popupPrefab.SetActive(true);
                    menuManager.currentPopup = popupPrefab;
                    Debug.Log("Different version number");
                }
            }
            else
            {
                popupPrefab.SetActive(false);
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