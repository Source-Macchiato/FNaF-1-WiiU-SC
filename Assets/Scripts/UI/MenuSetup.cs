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
        menuManager.AddButton("New Game", NewGame, 0, "mainmenu.newgame");
        menuManager.AddButton("Continue", Continue, 0, "mainmenu.continue");

        if (SaveManager.LoadStarsId() >= 1)
        {
            menuManager.AddButton("6th Night", SixthNight, 0, "nextnight.sixthnight");
        }
        
        if (SaveManager.LoadStarsId() >= 2)
        {
            menuManager.AddButton("Custom Night", CustomNight, 0, "mainmenu.customnight");
        }
        
        menuManager.AddButton("Options", Options, 0, "mainmenu.options", true);
        menuManager.AddButton("Credits", Credits, 0, "mainmenu.credits", true);

        menuManager.AddButton("Language", Language, 1, "mainmenu.language");
        menuManager.AddButton("Layout", Layout, 1, "mainmenu.layout");
        menuManager.AddButton("Online", Online, 1, "mainmenu.online");

        menuManager.AddButton("Analytic Data", Analytics, 4, "mainmenu.analyticdata");

        // Adding cards to the main menu
        menuManager.AddCard(5, "TV only", menuData.tvOnly);
        menuManager.AddCard(5, "TV + Gamepad", menuData.tvGamepad);
        menuManager.AddCard(5, "Gamepad only", menuData.gamepadOnly);

        // Adding switchers to the main menu
        menuManager.AddSwitcher(2, new string[] { "English", "French", "Spanish", "Italian", "German", "Arabic" }, "switcher.translation");
        menuManager.AddSwitcher(6, new string[] { "No", "Yes" }, "switcher.analyticdata");

        // Adding card switchers to the main menu
        menuManager.AddCardSwitcher(7, "Freddy", menuData.freddyPicture, 1, 20, 1);
        menuManager.AddCardSwitcher(7, "Bonnie", menuData.bonniePicture, 1, 20, 1);
        menuManager.AddCardSwitcher(7, "Chica", menuData.chicaPicture, 1, 20, 1);
        menuManager.AddCardSwitcher(7, "Foxy", menuData.foxyPicture, 1, 20, 1);

        // Set back callbacks for specific menus
        menuManager.SetBackCallback(3, OnBackFromCredits);
        menuManager.SetBackCallback(2, OnBackFromLanguage);
        menuManager.SetBackCallback(5, OnBackFromLayout);
        menuManager.SetBackCallback(6, OnBackFromAnalyticData);

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
        menuManager.ChangeMenu(7);
    }

    void Options()
    {
        menuManager.ChangeMenu(1);
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

    void Analytics()
    {
        menuManager.ChangeMenu(6);

        menuData.LoadShareDataAndUpdateSwitcher();
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
}