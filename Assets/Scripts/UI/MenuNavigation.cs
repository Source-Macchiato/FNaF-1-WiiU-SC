using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class MenuNavigation : MonoBehaviour
{
    public int selectedIndex = 0;
    public int menuId = 0;
    public GameObject UpdatePanel;
    public GameObject CreditsMenu;
    public ScrollRect CreditsScrollView;

    public Button[] MainMenuButtons;
    public Text[] MainMenuSelectionTexts;
    public Button[] OptionsMenuButtons;
    public Text[] OptionsMenuSelectionTexts;

    private float joystickThreshold = 0.5f;
    private float buttonChangeDelay = 0.2f;
    private bool canChangeButton = true;
    private float lastChangeTime;
    public float scrollSpeed = 30f;

    WiiU.GamePad gamePad;

    void Start()
    {
        gamePad = WiiU.GamePad.access;

        UpdateSelectionTexts();
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;

        float leftVerticalInput = Input.GetAxis("LeftStickY");

        if (!UpdatePanel.activeSelf)
        {
            if (Mathf.Abs(leftVerticalInput) > joystickThreshold)
            {
                if (canChangeButton && Time.time - lastChangeTime >= buttonChangeDelay)
                {
                    int direction = leftVerticalInput > 0 ? -1 : 1;

                    selectedIndex = (selectedIndex + direction + GetCurrentMenuButtons().Length) % GetCurrentMenuButtons().Length;
                    UpdateSelectionTexts();

                    lastChangeTime = Time.time;

                    if (CreditsMenu.activeSelf)
                    {
                        float scrollAmount = -leftVerticalInput * scrollSpeed * Time.deltaTime;
                        ScrollRect creditsScrollRect = CreditsScrollView.GetComponent<ScrollRect>();
                        Vector2 newPosition = creditsScrollRect.normalizedPosition + new Vector2(0f, scrollAmount);
                        newPosition.y = Mathf.Clamp01(newPosition.y);
                        creditsScrollRect.normalizedPosition = newPosition;
                    }
                }
            }

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
            }

            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    selectedIndex = (selectedIndex - 1 + GetCurrentMenuButtons().Length) % GetCurrentMenuButtons().Length;
                    UpdateSelectionTexts();
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    selectedIndex = (selectedIndex + 1) % GetCurrentMenuButtons().Length;
                    UpdateSelectionTexts();
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
}