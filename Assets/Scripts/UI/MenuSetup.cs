/************************************************************************
 *                                                                      *
 * This script was created by Source Macchiato                          *
 * and auto-commented by ChatGPT 3.5 for better understanding.          *
 *                                                                      *
 * Feel free to modify and enhance this code,                           *
 * but please retain this notice.                                       *
 *                                                                      *
 * Visit us at: https://www.sourcemacchiato.com                         *
 *                                                                      *
 ************************************************************************/

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
    }

    // Function for start a new game
    void NewGame()
    {
        playerData.NightNumber = 1;
        PlayerPrefs.SetFloat("NightNumber", playerData.NightNumber);
        PlayerPrefs.Save();
        playerData.LoadAdvertisement();
    }

    // Function for continue
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

    // Function for open the "Options" sub-menu
    void Options()
    {
        menuManager.ChangeMenu(1);
    }

    void Language()
    {
        Debug.Log("Language");
    }

    void Credits()
    {
        Debug.Log("Credits");
    }
}