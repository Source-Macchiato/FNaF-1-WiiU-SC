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
    public GameObject SubOptionPanelDev;
    public GameObject SubOptionPanel;
    private bool SubOptionPanelDevState;
    private bool SubOptionPanelState;

    void Start()
    {
        // Adding buttons to the main menu with corresponding actions
        menuManager.AddButton("New Game", NewGame);
        menuManager.AddButton("Continue", Continue);
        menuManager.AddButton("Options", Options);

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
        SceneManager.LoadScene("PortalDemo");
    }

    // Function for continue
    void Continue()
    {
        Debug.Log("Continue");
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