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
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using WiiU = UnityEngine.WiiU;

public class MenuManager : MonoBehaviour
{
    // Prefab for creating buttons dynamically
    public GameObject buttonPrefab;

    // Parent transform where menu buttons will be placed
    public Transform menuParent;

    // List to keep track of all menu buttons
    private List<GameObject> buttons = new List<GameObject>();

    // References to WiiU controllers
    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    void Start()
    {
        // Access the WiiU GamePad and Remote
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        // Set the first button as the selected button if any buttons exist
        if (buttons.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(buttons[0]);
            EnableButtonVisual(buttons[0]);
        }
    }

    void Update()
    {
        // Get the current state of the GamePad and Remote
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        // Handle GamePad input
        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsReleased(WiiU.GamePadButton.Up))
            {
                Navigate(-1);
            }
            else if (gamePadState.IsReleased(WiiU.GamePadButton.Down))
            {
                Navigate(1);
            }
            else if (gamePadState.IsReleased(WiiU.GamePadButton.A))
            {
                ClickSelectedButton();
            }
        }

        // Handle Remote input based on the device type
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsReleased(WiiU.ProControllerButton.Up))
                {
                    Navigate(-1);
                }
                else if (remoteState.pro.IsReleased(WiiU.ProControllerButton.Down))
                {
                    Navigate(1);
                }
                else if (remoteState.pro.IsReleased(WiiU.ProControllerButton.A))
                {
                    ClickSelectedButton();
                }
                break;
            case WiiU.RemoteDevType.Classic:
                if (remoteState.classic.IsReleased(WiiU.ClassicButton.Up))
                {
                    Navigate(-1);
                }
                else if (remoteState.classic.IsReleased(WiiU.ClassicButton.Down))
                {
                    Navigate(1);
                }
                else if (remoteState.classic.IsReleased(WiiU.ClassicButton.A))
                {
                    ClickSelectedButton();
                }
                break;
            default:
                if (remoteState.IsReleased(WiiU.RemoteButton.Up))
                {
                    Navigate(-1);
                }
                else if (remoteState.IsReleased(WiiU.RemoteButton.Down))
                {
                    Navigate(1);
                }
                else if (remoteState.IsReleased(WiiU.RemoteButton.A))
                {
                    ClickSelectedButton();
                }
                break;
        }

        // Handle keyboard input, useful for testing in the editor
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Navigate(-1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Navigate(1);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                ClickSelectedButton();
            }
        }
    }

    // Adds a button to the menu with the given text and click action
    public void AddButton(string buttonText, UnityEngine.Events.UnityAction onClickAction, string translationId)
    {
        // Instantiate the button prefab
        GameObject newButton = Instantiate(buttonPrefab, menuParent);

        // Set the button text
        GameObject buttonTextComponent = newButton.transform.Find("Text").gameObject;
        Text text = buttonTextComponent.GetComponent<Text>();
        text.text = buttonText;

        // Translate button text
        I18nTextTranslator translator = buttonTextComponent.GetComponent<I18nTextTranslator>();
        translator.textId = translationId;

        // Add the click action to the button
        Button buttonComponent = newButton.GetComponent<Button>();
        buttonComponent.onClick.AddListener(onClickAction);

        // Hide selection text initially
        Transform selectionText = newButton.transform.Find("Selection");

        if (selectionText != null)
        {
            selectionText.gameObject.SetActive(false);
        }

        // Add the button to the list
        buttons.Add(newButton);
    }

    // Adds a submenu button that shows the submenu when clicked
    /*public void AddSubMenu(string buttonText, MenuManager subMenuManager)
    {
        AddButton(buttonText, () => subMenuManager.ShowMenu());
        subMenuManager.HideMenu();
    }*/

    // Shows the menu
    public void ShowMenu()
    {
        menuParent.gameObject.SetActive(true);
    }

    // Hides the menu
    public void HideMenu()
    {
        menuParent.gameObject.SetActive(false);
    }

    // Navigates through the menu buttons based on the direction
    public void Navigate(int direction)
    {
        if (buttons.Count == 0) return;

        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected == null)
        {
            currentSelected = buttons[0];
        }

        int currentIndex = buttons.IndexOf(currentSelected);
        int nextIndex = (currentIndex + direction + buttons.Count) % buttons.Count;

        DisableButtonVisual(buttons[currentIndex]);

        EventSystem.current.SetSelectedGameObject(buttons[nextIndex]);

        EnableButtonVisual(buttons[nextIndex]);
    }

    // Clicks the currently selected button
    private void ClickSelectedButton()
    {
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected != null)
        {
            Button buttonComponent = currentSelected.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.Invoke();
            }
        }
    }

    // Enables visual elements for the selected button
    private void EnableButtonVisual(GameObject button)
    {
        Transform selectionText = button.transform.Find("Selection");

        if (selectionText != null)
        {
            selectionText.gameObject.SetActive(true);
        }
    }

    // Disables visual elements for the deselected button
    private void DisableButtonVisual(GameObject button)
    {
        Transform selectionText = button.transform.Find("Selection");

        if (selectionText != null)
        {
            selectionText.gameObject.SetActive(false);
        }
    }
}
