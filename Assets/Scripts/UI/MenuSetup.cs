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
        menuData.GoldenFreddy(SaveManager.saveData.game.goldenFreddyUnlocked);

        // Set back callbacks for specific menus
        menuManager.SetBackCallback(3, OnBackFromCredits);
        menuManager.SetBackCallback(5, OnBackFromLanguage);
        menuManager.SetBackCallback(7, OnBackFromOnline);
        menuManager.SetBackCallback(8, OnBackFromVolume);
        menuManager.SetBackCallback(9, OnBackFromControls);
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

        if (menuData.nightNumber >= 0 && menuData.nightNumber <= 4) // Night is between 1 and 5
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

    public void Options()
    {
        menuManager.ChangeMenu(1);
    }

    public void CustomNight()
    {
        menuData.CustomNightBackgroundStatus(true);

        menuManager.ChangeMenu(2);
    }

    public void Credits()
    {
        menuManager.ChangeMenu(3);
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

    public void Online()
    {
        menuManager.ChangeMenu(7);

        menuData.LoadAnalyticsAndUpdateSwitcher();
    }

    public void Volume()
    {
        menuManager.ChangeMenu(8);

        menuData.UpdateVolumeSwitchers();
    }

    public void Controls()
    {
        menuManager.ChangeMenu(9);

        menuData.LoadMotionControls();
        menuData.LoadPointerVisibility();
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

        if (freddyDifficulty == 1 && bonnieDifficulty == 9 && chicaDifficulty == 8 && foxyDifficulty == 7 && SaveManager.saveData.game.goldenFreddyUnlocked == false)
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
        menuData.ToggleGameTitle(true);
    }

    void OnBackFromOnline()
    {
        menuData.SaveAndUpdateAnalytics();
    }

    void OnBackFromVolume()
    {
        menuData.SaveAndUpdateVolume();
    }

    void OnBackFromControls()
    {
        menuData.SaveMotionControls();
        menuData.SavePointerVisibility();
    }
}