using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class MenuNavigation : MonoBehaviour
{
    public int selectedIndex = 0;
    public int menuId = 0;
    public GameObject UpdatePanel;
    private GameObject ShareDataPanel;
    public GameObject LoginPanel;
    public GameObject CreditsMenu;
    public ScrollRect CreditsScrollView;

    public Button[] MainMenuButtons;
    public Text[] MainMenuSelectionTexts;
    public Button[] OptionsMenuButtons;
    public Text[] OptionsMenuSelectionTexts;

    private float joystickThreshold = 0.5f;
    private float buttonChangeDelay = 0.2f;
    private bool canChangeButton = false;
    private float lastChangeTime;
    private float scrollSpeed = 0.5f;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    void Awake()
    {
        ShareDataPanel = GameObject.Find("ShareDataPanel");
    }

    void Start()
    {
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        UpdateSelectionTexts();
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        float leftVerticalInput = Input.GetAxis("LeftStickY");

        if (!LoginPanel.activeSelf)
        {
            if (UpdatePanel.activeSelf && ShareDataPanel.activeSelf)
            {
                canChangeButton = false;
            }
            else if (UpdatePanel.activeSelf && !ShareDataPanel.activeSelf)
            {
                canChangeButton = false;
            }
            else if (!UpdatePanel.activeSelf && ShareDataPanel.activeSelf)
            {
                canChangeButton = false;
            }
            else
            {
                StartCoroutine(EnableButtonChangeAfterDelay());
            }

            if (canChangeButton)
            {
                if (Mathf.Abs(leftVerticalInput) > joystickThreshold)
                {
                    if (Time.time - lastChangeTime >= buttonChangeDelay)
                    {
                        int direction = leftVerticalInput > 0 ? -1 : 1;

                        selectedIndex = (selectedIndex + direction + GetCurrentMenuButtons().Length) % GetCurrentMenuButtons().Length;
                        UpdateSelectionTexts();

                        lastChangeTime = Time.time;
                    }

                    if (CreditsMenu.activeSelf)
                    {
                        float scrollAmount = leftVerticalInput * scrollSpeed * Time.deltaTime;
                        ScrollRect creditsScrollRect = CreditsScrollView.GetComponent<ScrollRect>();
                        Vector2 newPosition = creditsScrollRect.normalizedPosition + new Vector2(0f, scrollAmount);
                        newPosition.y = Mathf.Clamp01(newPosition.y);
                        creditsScrollRect.normalizedPosition = newPosition;
                    }
                }

                // Gamepad
                if (gamePadState.gamePadErr == WiiU.GamePadError.None)
                {
                    if (gamePadState.IsReleased(WiiU.GamePadButton.Up))
                    {
                        selectedIndex = (selectedIndex - 1 + GetCurrentMenuButtons().Length) % GetCurrentMenuButtons().Length;
                        UpdateSelectionTexts();
                    }

                    if (gamePadState.IsReleased(WiiU.GamePadButton.Down))
                    {
                        selectedIndex = (selectedIndex + 1) % GetCurrentMenuButtons().Length;
                        UpdateSelectionTexts();
                    }

                    if (gamePadState.IsPressed(WiiU.GamePadButton.Up))
                    {
                        ScrollCreditsUp();
                    }

                    if (gamePadState.IsPressed(WiiU.GamePadButton.Down))
                    {
                        ScrollCreditsDown();
                    }
                }

                // Remote
                switch (remoteState.devType)
                {
                    case WiiU.RemoteDevType.ProController:
                        if (remoteState.pro.IsReleased(WiiU.ProControllerButton.Up))
                        {
                            selectedIndex = (selectedIndex - 1 + GetCurrentMenuButtons().Length) % GetCurrentMenuButtons().Length;
                            UpdateSelectionTexts();
                        }

                        if (remoteState.pro.IsReleased(WiiU.ProControllerButton.Down))
                        {
                            selectedIndex = (selectedIndex + 1) % GetCurrentMenuButtons().Length;
                            UpdateSelectionTexts();
                        }

                        if (remoteState.pro.IsPressed(WiiU.ProControllerButton.Up))
                        {
                            ScrollCreditsUp();
                        }

                        if (remoteState.pro.IsPressed(WiiU.ProControllerButton.Down))
                        {
                            ScrollCreditsDown();
                        }
                        break;
                    default:
                        break;
                }

                // Keyboard
                if (Application.isEditor)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        selectedIndex = (selectedIndex - 1 + GetCurrentMenuButtons().Length) % GetCurrentMenuButtons().Length;
                        UpdateSelectionTexts();
                    }

                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        ScrollCreditsUp();
                    }

                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        selectedIndex = (selectedIndex + 1) % GetCurrentMenuButtons().Length;
                        UpdateSelectionTexts();
                    }

                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        ScrollCreditsDown();
                    }
                }
            }
        }
    }

    public void UpdateSelectionTexts()
    {
        Text[] currentSelectionTexts = GetCurrentMenuSelectionTexts();

        for (int i = 0; i < GetCurrentMenuButtons().Length; i++)
        {
            currentSelectionTexts[i].gameObject.SetActive(i == selectedIndex);
        }
    }

    Button[] GetCurrentMenuButtons()
    {
        return menuId == 0 ? MainMenuButtons : OptionsMenuButtons;
    }

    Text[] GetCurrentMenuSelectionTexts()
    {
        return menuId == 0 ? MainMenuSelectionTexts : OptionsMenuSelectionTexts;
    }

    IEnumerator EnableButtonChangeAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        canChangeButton = true;
    }

    private void ScrollCreditsUp()
    {
        if (CreditsMenu.activeSelf)
        {
            float scrollAmount = 1 * scrollSpeed * Time.deltaTime;
            ScrollRect creditsScrollRect = CreditsScrollView.GetComponent<ScrollRect>();
            Vector2 newPosition = creditsScrollRect.normalizedPosition + new Vector2(0f, scrollAmount);
            newPosition.y = Mathf.Clamp01(newPosition.y);
            creditsScrollRect.normalizedPosition = newPosition;
        }
    }

    private void ScrollCreditsDown()
    {
        if (CreditsMenu.activeSelf)
        {
            float scrollAmount = -1 * scrollSpeed * Time.deltaTime;
            ScrollRect creditsScrollRect = CreditsScrollView.GetComponent<ScrollRect>();
            Vector2 newPosition = creditsScrollRect.normalizedPosition + new Vector2(0f, scrollAmount);
            newPosition.y = Mathf.Clamp01(newPosition.y);
            creditsScrollRect.normalizedPosition = newPosition;
        }
    }
}