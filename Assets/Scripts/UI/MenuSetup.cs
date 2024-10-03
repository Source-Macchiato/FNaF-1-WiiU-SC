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
        menuManager.AddButton("Options", Options, 0, "mainmenu.options");
        menuManager.AddButton("Credits", Credits, 0, "mainmenu.credits");

        menuManager.AddButton("Language", Language, 1, "mainmenu.language");
        menuManager.AddButton("Layout", Layout, 1, "mainmenu.layout");
        menuManager.AddButton("Online", Online, 1, "mainmenu.online");

        menuManager.AddButton("Analytic Data", Analytics, 4, "mainmenu.analyticdata");

        // Adding cards to the main menu
        menuManager.AddCard(5, "TV only", menuData.tvOnly);
        menuManager.AddCard(5, "TV + Gamepad", menuData.tvGamepad);
        menuManager.AddCard(5, "Gamepad only", menuData.gamepadOnly);

        // Set back callbacks for specific menus
        menuManager.SetBackCallback(3, OnBackFromCredits);
        menuManager.SetBackCallback(2, OnBackFromLanguage);
        menuManager.SetBackCallback(5, OnBackFromLayout);

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

        menuData.nightNumber = 1;
        PlayerPrefs.SetFloat("NightNumber", menuData.nightNumber);
        PlayerPrefs.Save();
        menuData.LoadAdvertisement();
    }

    void Continue()
    {
        menuManager.canNavigate = false;

        menuData.nightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

        if (menuData.nightNumber == 1)
        {
            menuData.LoadAdvertisement();
        }
        else if (menuData.nightNumber > 1 && menuData.nightNumber < 6)
        {
            SceneManager.LoadScene("NextNight");
        }
    }

    void Options()
    {
        menuManager.ChangeMenu(1);
    }

    void Language()
    {
        menuManager.ChangeMenu(2);
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
}