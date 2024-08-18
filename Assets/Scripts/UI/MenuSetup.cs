using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSetup : MonoBehaviour
{
    // Reference to the main and sub menus
    public MenuManager menuManager;
    public PlayerData playerData;

    void Start()
    {
        // Adding buttons to the main menu with corresponding actions
        menuManager.AddButton("New Game", NewGame, 0, "mainmenu.newgame");
        menuManager.AddButton("Continue", Continue, 0, "mainmenu.continue");
        menuManager.AddButton("Options", Options, 0, "mainmenu.options");

        menuManager.AddButton("Language", Language, 1, "mainmenu.language");
        menuManager.AddButton("Credits", Credits, 1, "mainmenu.credits");

        // Set back callbacks for specific menus
        menuManager.SetBackCallback(2, OnBackFromLanguage);

        // Display main menu after loaded all buttons
        menuManager.ChangeMenu(0);
    }

    // Buttons functions
    void NewGame()
    {
        playerData.NightNumber = 1;
        PlayerPrefs.SetFloat("NightNumber", playerData.NightNumber);
        PlayerPrefs.Save();
        playerData.LoadAdvertisement();
    }

    void Continue()
    {
        playerData.NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

        if (playerData.NightNumber == 1)
        {
            playerData.LoadAdvertisement();
        }
        else if (playerData.NightNumber > 1 && playerData.NightNumber < 6)
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
    }

    // Callback functions
    void OnBackFromLanguage()
    {
        Debug.Log("Test");
    }
}