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
        menuData.GoldenFreddyEnabled(SaveManager.saveData.game.goldenFreddyUnlocked);

        // Set back callbacks for specific menus
        menuManager.SetBackCallback(3, OnBackFromCredits);
        menuManager.SetBackCallback(5, OnBackFromLanguage);
        menuManager.SetBackCallback(7, OnBackFromBrewConnect);
        menuManager.SetBackCallback(8, OnBackFromVolume);
        menuManager.SetBackCallback(9, OnBackFromControls);
        menuManager.SetBackCallback(10, OnBackFromVideo);
        menuManager.SetBackCallback(11, OnBackFromSubtitles);
    }

    // Buttons functions
    public void NewGame()
    {
        if (menuManager.canNavigate)
        {
            menuManager.canNavigate = false;

            // Reset night number and save it
            menuData.nightNumber = 0;
            menuData.SaveNightNumber();

            StartCoroutine(menuData.LoadAdvertisement());
        }
    }

    public void Continue()
    {
        if (menuManager.canNavigate)
        {
            menuManager.canNavigate = false;

            if (menuData.nightNumber >= 0 && menuData.nightNumber <= 4) // Night is between 1 and 5
            {
                SceneManager.LoadSceneAsync("NextNight");
            }
            else if (menuData.nightNumber >= 5)
            {
                // Reset night number to 4 and save it
                menuData.nightNumber = 4;
                menuData.SaveNightNumber();

                SceneManager.LoadSceneAsync("NextNight");
            }
        }
    }

    public void SixthNight()
    {
        if (menuManager.canNavigate)
        {
            menuManager.canNavigate = false;

            menuData.nightNumber = 5;
            menuData.SaveNightNumber();

            SceneManager.LoadSceneAsync("NextNight");
        }
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

    public void BrewConnect()
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

    public void Video()
    {
        menuManager.ChangeMenu(10);

        menuData.LoadPanoramaEffect();
    }

    public void Subtitles()
    {
        menuManager.ChangeMenu(11);

        menuData.LoadSubtitlesStatus();
    }

    public void StartCustomNight()
    {
        if (menuManager.canNavigate)
        {
            menuManager.canNavigate = false;

            menuData.nightNumber = 6;
            menuData.SaveNightNumber();

            menuData.ApplyCustomNightValues();

            if (Movement.freddyDifficulty == 1 && Movement.bonnieDifficulty == 9 && Movement.chicaDifficulty == 8 && Movement.foxyDifficulty == 7 && SaveManager.saveData.game.goldenFreddyUnlocked == false)
            {
                menuData.CustomNightBackgroundStatus(false);

                StartCoroutine(menuData.ActivateGoldenFreddyJumpscare());
            }
            else
            {
                SceneManager.LoadSceneAsync("NextNight");
            }
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

    void OnBackFromBrewConnect()
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

    void OnBackFromVideo()
    {
        menuData.SavePanoramaEffect();
    }

    void OnBackFromSubtitles()
    {
        menuData.SaveSubtitlesStatus();
    }
}