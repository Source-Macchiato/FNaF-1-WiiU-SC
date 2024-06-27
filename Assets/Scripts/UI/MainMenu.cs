using System.Collections;
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
    private GameObject ShareDataPanel;
    public GameObject LoginPanel;
    public GameObject MainMenuNavigationPanel;
    public GameObject OptionsMenuNavigationPanel;
    public GameObject AudioMenuPanel;
    public GameObject CreditsMenuPanel;
    public Text setLanguageText;
    private bool canChangeButton = false;

    // bad way to fix the issue, will have to find why the text isnt translated
    public I18nTextTranslator languageText;
    public I18nTextTranslator creditsText;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    MenuNavigation menuNavigation;
    SaveGameState saveGameState;
    SaveManager saveManager;
    I18nTextTranslator[] translators;

    void Awake()
    {
        ShareDataPanel = GameObject.Find("ShareDataPanel");
    }

    void Start()
    {
        OptionsMenuNavigationPanel.SetActive(false);
        AudioMenuPanel.SetActive(false);
        CreditsMenuPanel.SetActive(false);
        advertisementIsActive = false;

        advertisementImage.SetActive(false);

        menuNavigation = FindObjectOfType<MenuNavigation>();
        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        translators = FindObjectsOfType<I18nTextTranslator>();

        NightNumber = SaveManager.LoadNightNumber();

        NightNumberDisplayer.text = NightNumber.ToString();

        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);
    }

    void Update()
    {
        if (Time.time - startTime >= waitTime && advertisementIsActive == true)
        {
            // load scene after advertisementload is called
            SceneManager.LoadScene("NextNight");
        }

        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

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
                // Gamepad
                if (gamePadState.gamePadErr == WiiU.GamePadError.None)
                {
                    if (gamePadState.IsReleased(WiiU.GamePadButton.A))
                    {
                        MainMenuNavigation();
                    }
                    else if (gamePadState.IsReleased(WiiU.GamePadButton.B))
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
                            saveManager.SaveLanguage(setLanguageText.text);
                            bool saveResult = saveGameState.DoSave();

                            I18n.ReloadLanguage();
                            foreach (I18nTextTranslator translator in translators)
                            {
                                translator.UpdateText();
                            }

                            // This is the bad way because you can look up
                            languageText.UpdateText();
                            creditsText.UpdateText();

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

                // Remote
                switch (remoteState.devType)
                {
                    case WiiU.RemoteDevType.ProController:
                        if (remoteState.pro.IsReleased(WiiU.ProControllerButton.A))
                        {
                            MainMenuNavigation();
                        }
                        else if (remoteState.pro.IsReleased(WiiU.ProControllerButton.B))
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
                                saveManager.SaveLanguage(setLanguageText.text);
                                bool saveResult = saveGameState.DoSave();

                                I18n.ReloadLanguage();
                                foreach (I18nTextTranslator translator in translators)
                                {
                                    translator.UpdateText();
                                }

                                // This is the bad way because you can look up
                                languageText.UpdateText();
                                creditsText.UpdateText();

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
                        break;

                    default:
                        break;
                }

                // Keyboard
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
                            saveManager.SaveLanguage(setLanguageText.text);
                            bool saveResult = saveGameState.DoSave();

                            I18n.ReloadLanguage();
                            foreach (I18nTextTranslator translator in translators)
                            {
                                translator.UpdateText();
                            }

                            // This is the bad way because you can look up
                            languageText.UpdateText();
                            creditsText.UpdateText();

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

    IEnumerator EnableButtonChangeAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        canChangeButton = true;
    }
}