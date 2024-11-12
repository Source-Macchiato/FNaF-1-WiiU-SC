using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSetup : MonoBehaviour
{
    // Reference to the main and sub menus
    public MenuManager menuManager;
    public MenuData menuData;

    void Start()
    {
        // Adding buttons to the main menu with corresponding actions
        menuManager.AddButton(0, 0, NewGame, "mainmenu.newgame");
        menuManager.AddButton(0, 0, Continue, "mainmenu.continue");

        if (SaveManager.LoadStarsId() >= 1)
        {
            menuManager.AddButton(0, 0, SixthNight, "nextnight.sixthnight");
        }
        
        if (SaveManager.LoadStarsId() >= 2)
        {
            menuManager.AddButton(0, 0, CustomNight, "mainmenu.customnight");
        }
        
        menuManager.AddButton(0, 0, Options, "mainmenu.options", true);
        menuManager.AddButton(0, 0, Credits, "mainmenu.credits", true);

        menuManager.AddButton(1, 0, Audio, "mainmenu.audio");
        menuManager.AddButton(1, 0, Language, "mainmenu.language");
        menuManager.AddButton(1, 0, Layout, "mainmenu.layout");
        menuManager.AddButton(1, 0, Online, "mainmenu.online");

        menuManager.AddSwitcher(2, new string[] { "English", "French", "Spanish", "Italian", "German", "Arabic" }, "switcher.translation");

        menuManager.AddButton(4, 0, Analytics, "mainmenu.analyticdata");        

        menuManager.AddCard(5, "TV only", menuData.tvOnly);
        menuManager.AddCard(5, "TV + Gamepad", menuData.tvGamepad);
        menuManager.AddCard(5, "Gamepad only", menuData.gamepadOnly);
        
        menuManager.AddSwitcher(6, new string[] { "No", "Yes" }, "switcher.analyticdata");        

        menuManager.AddCardSwitcher(7, "Freddy", menuData.freddyPicture, "customnight.ailevel", 0, 20, 1);
        menuManager.AddCardSwitcher(7, "Bonnie", menuData.bonniePicture, "customnight.ailevel", 0, 20, 3);
        menuManager.AddCardSwitcher(7, "Chica", menuData.chicaPicture, "customnight.ailevel", 0, 20, 3);
        menuManager.AddCardSwitcher(7, "Foxy", menuData.foxyPicture, "customnight.ailevel", 0, 20, 1);
        menuManager.AddButton(7, 1, StartCustomNight, "customnight.ready", true);

        if (SaveManager.LoadGoldenFreddyStatus() == 1)
        {
            menuData.AddGoldenFreddy();
        }

        menuManager.AddDescription(8, "mainmenu.generalvolume");
        menuManager.AddSwitcher(8, new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, "switcher.generalvolume");
        menuManager.AddDescription(8, "mainmenu.musicvolume");
        menuManager.AddSwitcher(8, new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, "switcher.musicvolume");
        menuManager.AddDescription(8, "mainmenu.voicevolume");
        menuManager.AddSwitcher(8, new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, "switcher.voicevolume");
        menuManager.AddDescription(8, "mainmenu.sfxvolume");
        menuManager.AddSwitcher(8, new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, "switcher.sfxvolume");

        menuManager.AddButton(9, 0, Volume, "mainmenu.volume");

        // Set back callbacks for specific menus
        menuManager.SetBackCallback(3, OnBackFromCredits);
        menuManager.SetBackCallback(2, OnBackFromLanguage);
        menuManager.SetBackCallback(5, OnBackFromLayout);
        menuManager.SetBackCallback(6, OnBackFromAnalyticData);
        menuManager.SetBackCallback(7, OnBackFromCustomNight);
        menuManager.SetBackCallback(8, OnBackFromVolume);

        // Display main menu after loaded all buttons
        menuManager.ChangeMenu(0);

        // Some actions to do
        menuData.GenerateNightNumber();
    }

    void Update()
    {
        // Display night number if Continue button selected
        if (menuManager.currentButton == menuManager.menuButtons[0][1].GetComponent<Button>() && !menuData.nightNumberGameObject.activeSelf)
        {
            menuData.nightNumberGameObject.SetActive(true);
        }
        else if (menuManager.currentButton != menuManager.menuButtons[0][1].GetComponent<Button>() && menuData.nightNumberGameObject.activeSelf)
        {
            menuData.nightNumberGameObject.SetActive(false);
        }
    }

    // Buttons functions
    void NewGame()
    {
        menuManager.canNavigate = false;

        // Reset night number and save it
        menuData.nightNumber = 0;
        menuData.SaveNightNumber();

        menuData.LoadAdvertisement();
    }

    void Continue()
    {
        menuManager.canNavigate = false;

        if (menuData.nightNumber == 0) // Night is 1
        {
            menuData.LoadAdvertisement();
        }
        else if (menuData.nightNumber >= 1 && menuData.nightNumber <= 4) // Night is between 2 and 5
        {
            SceneManager.LoadScene("NextNight");
        }
        else if (menuData.nightNumber >= 5)
        {
            // Reset night number to 4 and save it
            menuData.nightNumber = 4;
            menuData.SaveNightNumber();

            SceneManager.LoadScene("NextNight");
        }
    }

    void SixthNight()
    {
        menuManager.canNavigate = false;

        menuData.nightNumber = 5;
        menuData.SaveNightNumber();

        SceneManager.LoadScene("NextNight");
    }

    void CustomNight()
    {
        menuData.CustomNightBackgroundStatus(true);

        menuManager.ChangeMenu(7);
    }

    void Options()
    {
        menuManager.ChangeMenu(1);
    }

    void Audio()
    {
        menuManager.ChangeMenu(9);
    }

    void Language()
    {
        menuManager.ChangeMenu(2);

        menuData.LoadLanguageAndUpdateSwitcher();
    }

    void Credits()
    {
        menuManager.ChangeMenu(3);

        if (menuManager.GetCurrentMenu() != null)
        {
            Transform creditsChild = menuManager.GetCurrentMenu().transform.GetChild(0);
            menuManager.currentScrollRect = creditsChild.GetComponent<ScrollRect>();
        }
    }

    void Layout()
    {
        menuManager.ChangeMenu(5);

        menuData.layoutButtons = menuManager.GetCurrentMenu().transform.GetComponentsInChildren<Button>();

        Button newButton = menuData.layoutButtons[menuData.layoutId];

        newButton.Select();

        menuManager.currentButton = newButton;

        menuData.UpdateCursorSize(false, menuManager.currentSelection);
    }

    void Online()
    {
        menuManager.ChangeMenu(4);
    }

    void Volume()
    {
        menuManager.ChangeMenu(8);
    }

    void Analytics()
    {
        menuManager.ChangeMenu(6);

        menuData.LoadShareDataAndUpdateSwitcher();
    }

    void StartCustomNight()
    {
        menuData.nightNumber = 6;
        menuData.SaveNightNumber();

        menuData.SaveCustomNightValues();

        float freddyDifficulty = PlayerPrefs.GetFloat("FreddyDifficulty", 0);
        float bonnieDifficulty = PlayerPrefs.GetFloat("BonnieDifficulty", 0);
        float chicaDifficulty = PlayerPrefs.GetFloat("ChicaDifficulty", 0);
        float foxyDifficulty = PlayerPrefs.GetFloat("FoxyDifficulty", 0);

        if (freddyDifficulty == 1 && bonnieDifficulty == 9 && chicaDifficulty == 8 && foxyDifficulty == 7 && SaveManager.LoadGoldenFreddyStatus() == 0)
        {
            menuData.CustomNightBackgroundStatus(false);

            menuData.SaveGoldenFreddy();

            menuData.ActivateGoldenFreddyJumpscare();
        }
        else
        {
            menuManager.canNavigate = false;

            SceneManager.LoadScene("NextNight");
        }
    }

    // Callback functions
    void OnBackFromLanguage()
    {
        menuData.SaveAndUpdateLanguage();
    }

    void OnBackFromCredits()
    {
        menuManager.currentScrollRect = null;
    }

    void OnBackFromLayout()
    {
        int index = 0;

        foreach (Button layoutButton in menuData.layoutButtons)
        {
            if (layoutButton == menuManager.currentButton)
            {
                menuData.layoutId = index;
                menuData.SaveLayout();
                break;
            }

            index++;
        }

        menuData.UpdateCursorSize(true, menuManager.currentSelection);
    }

    void OnBackFromAnalyticData()
    {
        menuData.SaveShareData();
    }

    void OnBackFromCustomNight()
    {
        menuData.CustomNightBackgroundStatus(false);
    }

    void OnBackFromVolume()
    {

    }
}