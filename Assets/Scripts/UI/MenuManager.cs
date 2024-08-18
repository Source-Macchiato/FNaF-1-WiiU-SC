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
    public Transform[] menus;

    // List to keep track of all menu buttons
    private Dictionary<int, List<GameObject>> menuButtons = new Dictionary<int, List<GameObject>>();

    // List to keep track of generated callbacks
    private Dictionary<int, UnityEngine.Events.UnityAction> backCallbacks = new Dictionary<int, UnityEngine.Events.UnityAction>();

    // Store menu history
    private Stack<int> menuHistory = new Stack<int>();

    // Flag to check if the user is navigating back
    private bool isNavigatingBack = false;

    int currentMenuId = 0;

    // References to WiiU controllers
    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    void Start()
    {
        // Access the WiiU GamePad and Remote
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);
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
                Navigate(-1, currentMenuId);
            }
            else if (gamePadState.IsReleased(WiiU.GamePadButton.Down))
            {
                Navigate(1, currentMenuId);
            }
            else if (gamePadState.IsReleased(WiiU.GamePadButton.A))
            {
                ClickSelectedButton();
            }
            else if (gamePadState.IsReleased(WiiU.GamePadButton.B))
            {
                GoBack();
            }
        }

        // Handle Remote input based on the device type
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsReleased(WiiU.ProControllerButton.Up))
                {
                    Navigate(-1, currentMenuId);
                }
                else if (remoteState.pro.IsReleased(WiiU.ProControllerButton.Down))
                {
                    Navigate(1, currentMenuId);
                }
                else if (remoteState.pro.IsReleased(WiiU.ProControllerButton.A))
                {
                    ClickSelectedButton();
                }
                else if (remoteState.pro.IsReleased(WiiU.ProControllerButton.B))
                {
                    GoBack();
                }
                break;
            case WiiU.RemoteDevType.Classic:
                if (remoteState.classic.IsReleased(WiiU.ClassicButton.Up))
                {
                    Navigate(-1, currentMenuId);
                }
                else if (remoteState.classic.IsReleased(WiiU.ClassicButton.Down))
                {
                    Navigate(1, currentMenuId);
                }
                else if (remoteState.classic.IsReleased(WiiU.ClassicButton.A))
                {
                    ClickSelectedButton();
                }
                else if (remoteState.classic.IsReleased(WiiU.ClassicButton.B))
                {
                    GoBack();
                }
                break;
            default:
                if (remoteState.IsReleased(WiiU.RemoteButton.Up))
                {
                    Navigate(-1, currentMenuId);
                }
                else if (remoteState.IsReleased(WiiU.RemoteButton.Down))
                {
                    Navigate(1, currentMenuId);
                }
                else if (remoteState.IsReleased(WiiU.RemoteButton.A))
                {
                    ClickSelectedButton();
                }
                else if (remoteState.IsReleased(WiiU.RemoteButton.B))
                {
                    GoBack();
                }
                break;
        }

        // Handle keyboard input, useful for testing in the editor
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Navigate(-1, currentMenuId);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Navigate(1, currentMenuId);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                ClickSelectedButton();
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                GoBack();
            }
        }
    }

    // Adds a button to the menu with the given text and click action
    public void AddButton(string buttonText, UnityEngine.Events.UnityAction onClickAction, int menuId, string translationId)
    {
        // Instantiate the button prefab
        GameObject newButton = Instantiate(buttonPrefab, menus[menuId]);

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

        // Add the button to the correct menu list in the dictionary
        if (!menuButtons.ContainsKey(menuId))
        {
            menuButtons[menuId] = new List<GameObject>();
        }
        menuButtons[menuId].Add(newButton);
    }

    // Navigates through the menu buttons based on the direction
    public void Navigate(int direction, int menuId)
    {
        if (!menuButtons.ContainsKey(menuId) || menuButtons[menuId].Count == 0) return;

        List<GameObject> currentMenuButtons = menuButtons[menuId];
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        if (currentSelected == null)
        {
            currentSelected = currentMenuButtons[0];
        }

        int currentIndex = currentMenuButtons.IndexOf(currentSelected);
        int nextIndex = (currentIndex + direction + currentMenuButtons.Count) % currentMenuButtons.Count;

        DisableButtonVisual(currentMenuButtons[currentIndex]);

        EventSystem.current.SetSelectedGameObject(currentMenuButtons[nextIndex]);

        EnableButtonVisual(currentMenuButtons[nextIndex]);
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

    public void ChangeMenu(int menuId)
    {
        // add current menu ID to history
        if (currentMenuId != menuId && !isNavigatingBack)
        {
            menuHistory.Push(currentMenuId);
        }

        foreach (Transform menu in menus)
        {
            if (menu != menus[menuId])
            {
                menu.gameObject.SetActive(false);
            }
        }

        menus[menuId].gameObject.SetActive(true);

        currentMenuId = menuId;

        if (menuButtons.ContainsKey(menuId) && menuButtons[menuId].Count > 0)
        {
            // Enable first button visual
            EventSystem.current.SetSelectedGameObject(menuButtons[menuId][0]);
            EnableButtonVisual(menuButtons[menuId][0]);

            // Disable visual for other buttons
            for (int i = 1; i < menuButtons[menuId].Count; i++)
            {
                DisableButtonVisual(menuButtons[menuId][i]);
            }
        }

        isNavigatingBack = false;
    }

    public void SetBackCallback(int menuId, UnityEngine.Events.UnityAction callback)
    {
        if (backCallbacks.ContainsKey(menuId))
        {
            backCallbacks[menuId] = callback;
        }
        else
        {
            backCallbacks.Add(menuId, callback);
        }
    }

    public void GoBack()
    {
        if (menuHistory.Count > 0)
        {
            // Set the navigation back flag to true
            isNavigatingBack = true;

            // Execute the callback for the current menu, if it exists
            if (backCallbacks.ContainsKey(currentMenuId) && backCallbacks[currentMenuId] != null)
            {
                backCallbacks[currentMenuId].Invoke();
            }

            // Retrieve the previous menu ID from the history stack
            int previousMenuId = menuHistory.Pop();

            // Change to the previous menu
            ChangeMenu(previousMenuId);
        }
    }
}