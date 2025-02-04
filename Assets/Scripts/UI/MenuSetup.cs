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

        if (SaveManager.LoadStarsId() >= 1)
        {
            //menuManager.AddButton(0, 0, SixthNight, "nextnight.sixthnight");
        }
        
        if (SaveManager.LoadStarsId() >= 2)
        {
            //menuManager.AddButton(0, 0, CustomNight, "mainmenu.customnight");
        }

        /*


        menuManager.AddButton(4, 0, Analytics, "mainmenu.analyticdata");
        
        menuManager.AddSwitcher(6, new string[] { "No", "Yes" }, "switcher.analyticdata");*/

        menuData.GoldenFreddy(SaveManager.LoadGoldenFreddyStatus() == 1);

        /*menuManager.AddDescription(8, "mainmenu.generalvolume");
        menuManager.AddSwitcher(8, new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, "switcher.generalvolume");
        menuManager.AddDescription(8, "mainmenu.musicvolume");
        menuManager.AddSwitcher(8, new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, "switcher.musicvolume");
        menuManager.AddDescription(8, "mainmenu.voicevolume");
        menuManager.AddSwitcher(8, new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, "switcher.voicevolume");
        menuManager.AddDescription(8, "mainmenu.sfxvolume");
        menuManager.AddSwitcher(8, new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, "switcher.sfxvolume");

        menuManager.AddButton(9, 0, Volume, "mainmenu.volume");*/

        // Set back callbacks for specific menus
        menuManager.SetBackCallback(3, OnBackFromCredits);
        menuManager.SetBackCallback(5, OnBackFromLanguage);
        //menuManager.SetBackCallback(5, OnBackFromLayout);
        //menuManager.SetBackCallback(6, OnBackFromAnalyticData);
        //menuManager.SetBackCallback(7, OnBackFromCustomNight);
        //menuManager.SetBackCallback(8, OnBackFromVolume);
    }

    // Buttons functions
    public void NewGame()
    {
        menuManager.canNavigate = false;

        // Reset night number and save it
        menuData.nightNumber = 0;
        menuData.SaveNightNumber();

        menuData.LoadAdvertisement();
    }

    public void Continue()
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

    public void SixthNight()
    {
        menuManager.canNavigate = false;

        menuData.nightNumber = 5;
        menuData.SaveNightNumber();

        SceneManager.LoadScene("NextNight");
    }

    public void CustomNight()
    {
        menuData.CustomNightBackgroundStatus(true);

        menuManager.ChangeMenu(2);
    }

    public void Credits()
    {
        menuManager.ChangeMenu(3);

        if (menuManager.GetCurrentMenu() != null)
        {
            Transform creditsChild = menuManager.GetCurrentMenu().transform.GetChild(0);
            menuManager.currentScrollRect = creditsChild.GetComponent<ScrollRect>();
        }
    }

    public void Language()
    {
        menuManager.ChangeMenu(5);

        menuData.LoadLanguageAndUpdateSwitcher();
    }

    public void Layout()
    {
        menuManager.ChangeMenu(6);

        menuData.DisplaySelectedLayoutButton();
    }

    void Volume()
    {
        menuManager.ChangeMenu(8);

        menuData.UpdateVolumeSwitchers();
    }

    void Analytics()
    {
        menuManager.ChangeMenu(6);

        menuData.LoadShareDataAndUpdateSwitcher();
    }

    public void StartCustomNight()
    {
        menuData.nightNumber = 6;
        menuData.SaveNightNumber();

        menuData.SaveCustomNightValues();

        float freddyDifficulty = PlayerPrefs.GetInt("FreddyDifficulty", 0);
        float bonnieDifficulty = PlayerPrefs.GetInt("BonnieDifficulty", 0);
        float chicaDifficulty = PlayerPrefs.GetInt("ChicaDifficulty", 0);
        float foxyDifficulty = PlayerPrefs.GetInt("FoxyDifficulty", 0);

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
        menuData.ToggleGameTitle(true);
    }

    void OnBackFromLayout()
    {
        
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
        menuData.SaveAndUpdateVolume();
    }
}