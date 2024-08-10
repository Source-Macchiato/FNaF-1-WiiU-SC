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

    public GameObject SubOptionPanelDev;
    public GameObject SubOptionPanel;
    private bool SubOptionPanelDevState;
    private bool SubOptionPanelState;

    void Start()
    {
        // Adding buttons to the main menu with corresponding actions
        menuManager.AddButton("New Game", NewGame, "mainmenu.newgame");
        menuManager.AddButton("Continue", Continue, "mainmenu.continue");
        menuManager.AddButton("Options", Options, "mainmenu.options");

        // Uncomment to add a debug button for mounting the SD card
        //menuManager.AddButton("[DEBUG] MOUNT SD CARD", Mount);

        // Uncomment to add a sub-menu to the main menu
        // menuManager.AddSubMenu("SubMenu 1", subMenu1);

        // Uncomment to add buttons to the sub-menu
        // subMenu1.AddButton("Sub Option 1", SubOption1);
        // subMenu1.AddButton("Back", subMenu1.HideMenu);
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
        Debug.Log("Options");
    }

    //function for accessing options submenu
    void OptionsSubMenu()
    {
        if (SubOptionPanelDevState != true && SubOptionPanelState != true)
        {
            SubOptionPanel.SetActive(true);
            SubOptionPanelState = true;
        }
        
    }

    void TempDevOption()
    {
        if(SubOptionPanelDevState !=true && SubOptionPanelState != true)
        {
            SubOptionPanelDev.SetActive(true);
            SubOptionPanelDevState = true;
        }
    }
}