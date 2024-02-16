using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class MainMenu : MonoBehaviour {

    public GameObject  advertisementImage;
    private float startTime;
    private float waitTime = 10f;
    private bool advertisementIsActive;
    public float NightNumber;
    public Text NightNumberDisplayer;
    public GameObject UpdatePanel;
    public GameObject MainMenuNavigationPanel;
    public GameObject OptionsMenuNavigationPanel;
    public GameObject AudioMenuPanel;
    public GameObject CreditsMenuPanel;
    public Text setLanguageText;

    WiiU.GamePad gamePad;

    MenuNavigation menuNavigation;

    void Start()
    {
        OptionsMenuNavigationPanel.SetActive(false);
        AudioMenuPanel.SetActive(false);
        CreditsMenuPanel.SetActive(false);
        advertisementIsActive = false;

        advertisementImage.SetActive(false);

        menuNavigation = FindObjectOfType<MenuNavigation>();

        NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

        NightNumberDisplayer.text = NightNumber.ToString();

        gamePad = WiiU.GamePad.access;
    }

    void Update()
    {
        if (Time.time - startTime >= waitTime && advertisementIsActive == true)
        {
            // load scene after advertisementload is called
            SceneManager.LoadScene("NextNight");
        }

        WiiU.GamePadState gamePadState = gamePad.state;

        if (!UpdatePanel.activeSelf)
        {
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
                {
                    MainMenuNavigation();
                }
                else if (gamePadState.IsTriggered(WiiU.GamePadButton.B))
                {
                    if (menuNavigation.menuId == 1)
                    {
                        OptionsMenuNavigationPanel.SetActive(false);
                        MainMenuNavigationPanel.SetActive(true);
                        menuNavigation.menuId = 0;
                        menuNavigation.selectedIndex = 0;
                        menuNavigation.UpdateSelectionTexts();
                    }
                    else if (menuNavigation.menuId == 2)
                    {
                        PlayerPrefs.SetString("Language", setLanguageText.text);
                        PlayerPrefs.Save();
                        I18n.ReloadLanguage();
                        AudioMenuPanel.SetActive(false);
                        OptionsMenuNavigationPanel.SetActive(true);
                        menuNavigation.menuId = 1;
                        menuNavigation.selectedIndex = 0;
                        menuNavigation.UpdateSelectionTexts();
                    }
                }
            }

            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    MainMenuNavigation();
                }
                else if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    if (menuNavigation.menuId == 1)
                    {
                        OptionsMenuNavigationPanel.SetActive(false);
                        MainMenuNavigationPanel.SetActive(true);
                        menuNavigation.menuId = 0;
                        menuNavigation.selectedIndex = 0;
                        menuNavigation.UpdateSelectionTexts();
                    }
                    else if (menuNavigation.menuId == 2)
                    {
                        PlayerPrefs.SetString("Language", setLanguageText.text);
                        PlayerPrefs.Save();
                        I18n.ReloadLanguage();
                        AudioMenuPanel.SetActive(false);
                        OptionsMenuNavigationPanel.SetActive(true);
                        menuNavigation.menuId = 1;
                        menuNavigation.selectedIndex = 0;
                        menuNavigation.UpdateSelectionTexts();
                    }
                    else if (menuNavigation.menuId == 3)
                    {
                        CreditsMenuPanel.SetActive(false);
                        OptionsMenuNavigationPanel.SetActive(true);
                        menuNavigation.menuId = 1;
                        menuNavigation.selectedIndex = 0;
                    }
                }
            }
        }
        else
        {
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
                {
                    UpdatePanel.SetActive(false);
                }
            }

            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    UpdatePanel.SetActive(false);
                }
            }
        }
    }

    void MainMenuNavigation()
    {
        if (menuNavigation.menuId == 0)
        {
            if (menuNavigation.selectedIndex == 0)
            {
                NightNumber = 1;
                PlayerPrefs.SetFloat("NightNumber", NightNumber);
                PlayerPrefs.Save();
                AdvertisementLoaded();
            }
            else if (menuNavigation.selectedIndex == 1)
            {
                NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

                if (NightNumber == 1)
                {
                    AdvertisementLoaded();
                }
                else if (NightNumber > 1 && NightNumber < 6)
                {
                    SceneManager.LoadScene("NextNight");
                }
            }
            else if (menuNavigation.selectedIndex == 2)
            {
                MainMenuNavigationPanel.SetActive(false);
                OptionsMenuNavigationPanel.SetActive(true);
                menuNavigation.menuId = 1;
                menuNavigation.selectedIndex = 0;
                menuNavigation.UpdateSelectionTexts();
            }
        }
        else if (menuNavigation.menuId == 1)
        {
            if (menuNavigation.selectedIndex == 0)
            {
                OptionsMenuNavigationPanel.SetActive(false);
                AudioMenuPanel.SetActive(true);
                menuNavigation.menuId = 2;
                menuNavigation.selectedIndex = 0;
            }
            else if (menuNavigation.selectedIndex == 1)
            {
                OptionsMenuNavigationPanel.SetActive(false);
                CreditsMenuPanel.SetActive(true);
                menuNavigation.menuId = 3;
                menuNavigation.selectedIndex = 0;
            }
        }
    }

    private void AdvertisementLoaded()
    {
        advertisementIsActive = true;
        startTime = Time.time;
        advertisementImage.SetActive(true);
    }
}