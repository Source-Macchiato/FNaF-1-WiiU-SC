using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class ShareDataNavigation : MonoBehaviour
{
    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    public float joystickThreshold = 0.5f;
    public float buttonChangeDelay = 0.2f;

    public int selectedButtonIndex = 0;

    public Text[] shareDataSelectionTexts;
    public Button[] shareDataButtons;

    private GameObject shareDataPanel;

    MainMenu mainMenu;
    ShareData shareData;
    SaveGameState saveGameState;
    SaveManager saveManager;

    void Awake()
    {
        shareDataPanel = GameObject.Find("ShareDataPanel");
    }

    void Start () {
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        mainMenu = FindObjectOfType<MainMenu>();
        shareData = FindObjectOfType<ShareData>();
        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        shareDataPanel.SetActive(false);

        UpdateSelectionTexts();
    }
	
	// Update is called once per frame
	void Update ()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

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
                        shareData.canShareData = 1;
                        saveManager.SaveShareData(shareData.canShareData);
                        bool saveResult = saveGameState.DoSave();

                        shareDataPanel.SetActive(false);
                    }
                    else if (selectedButtonIndex == 1)
                    {
                        shareData.canShareData = 0;
                        saveManager.SaveShareData(shareData.canShareData);
                        bool saveResult = saveGameState.DoSave();

                        shareDataPanel.SetActive(false);
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
                            shareData.canShareData = 1;
                            saveManager.SaveShareData(shareData.canShareData);
                            bool saveResult = saveGameState.DoSave();

                            shareDataPanel.SetActive(false);
                        }
                        else if (selectedButtonIndex == 1)
                        {
                            shareData.canShareData = 0;
                            saveManager.SaveShareData(shareData.canShareData);
                            bool saveResult = saveGameState.DoSave();

                            shareDataPanel.SetActive(false);
                        }
                    }
                    break;
                default:
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    selectedButtonIndex = (selectedButtonIndex - 1 + shareDataButtons.Length) % shareDataButtons.Length;
                    UpdateSelectionTexts();
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    selectedButtonIndex = (selectedButtonIndex + 1) % shareDataButtons.Length;
                    UpdateSelectionTexts();
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (selectedButtonIndex == 0)
                    {
                        shareData.canShareData = 1;
                        saveManager.SaveShareData(shareData.canShareData);
                        bool saveResult = saveGameState.DoSave();

                        shareDataPanel.SetActive(false);
                    }
                    else if (selectedButtonIndex == 1)
                    {
                        shareData.canShareData = 0;
                        saveManager.SaveShareData(shareData.canShareData);
                        bool saveResult = saveGameState.DoSave();

                        shareDataPanel.SetActive(false);
                    }
                }
            }
        }
        else
        {
            if (shareData.activateShareDataPanel)
            {
                shareData.activateShareDataPanel = false;
                shareDataPanel.SetActive(true);
                Debug.Log("Activate");
            }
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
