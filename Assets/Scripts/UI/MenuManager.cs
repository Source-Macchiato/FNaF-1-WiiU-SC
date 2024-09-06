using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using WiiU = UnityEngine.WiiU;

public class MenuManager : MonoBehaviour
{
    // Prefab for creating buttons dynamically
    public GameObject buttonPrefab;
    public GameObject selectionPrefab;

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

    private int currentMenuId = 0;

    // Instantiate selection cursor
    private GameObject currentSelection;

    // Elements to keep in memory
    public ScrollRect currentScrollRect;

    // Stick navigation
    private float stickNavigationCooldown = 0.2f;
    public float lastNavigationTime;

    // References to WiiU controllers
    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    private float stickDeadzone = 0.19f;

    void Start()
    {
        // Access the WiiU GamePad and Remote
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        GameObject canvasTV = GameObject.Find("CanvaTV");
        currentSelection = Instantiate(selectionPrefab, canvasTV.transform);
        currentSelection.SetActive(false);
    }

    void Update()
    {
        // Get the current state of the GamePad and Remote
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        // Handle GamePad input
        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            // Stick
            Vector2 leftStickGamepad = gamePadState.lStick;

            if (Mathf.Abs(leftStickGamepad.y) > stickDeadzone)
            {
                if (currentScrollRect == null)
                {
                    if (Time.time - lastNavigationTime > stickNavigationCooldown)
                    {
                        Navigate(new Vector2(0, -leftStickGamepad.y), currentMenuId);

                        lastNavigationTime = Time.time;
                    }
                }
                else
                {
                    Navigate(new Vector2(0, leftStickGamepad.y), currentMenuId);
                }
            }

            // Is Released
            if (gamePadState.IsReleased(WiiU.GamePadButton.Up))
            {
                if (currentScrollRect == null)
                {
                    Navigate(new Vector2(0, -1), currentMenuId);
                }
            }
            else if (gamePadState.IsReleased(WiiU.GamePadButton.Down))
            {
                if (currentScrollRect == null)
                {
                    Navigate(new Vector2(0, 1), currentMenuId);
                }
            }
            else if (gamePadState.IsReleased(WiiU.GamePadButton.A))
            {
                ClickSelectedButton();
            }
            else if (gamePadState.IsReleased(WiiU.GamePadButton.B))
            {
                GoBack();
            }

            // Is Pressed
            if (gamePadState.IsPressed(WiiU.GamePadButton.Up))
            {
                if (currentScrollRect != null)
                {
                    Navigate(new Vector2(0, 1), currentMenuId);
                }
            }
            else if (gamePadState.IsPressed(WiiU.GamePadButton.Down))
            {
                if (currentScrollRect != null)
                {
                    Navigate(new Vector2(0, -1), currentMenuId);
                }
            }
        }

        // Handle Remote input based on the device type
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                // Stick
                Vector2 leftStickProController = remoteState.pro.leftStick;

                if (Mathf.Abs(leftStickProController.y) > stickDeadzone)
                {
                    if (currentScrollRect == null)
                    {
                        if (Time.time - lastNavigationTime > stickNavigationCooldown)
                        {
                            Debug.Log(leftStickProController.y);

                            Navigate(new Vector2(0, -leftStickProController.y), currentMenuId);

                            lastNavigationTime = Time.time;
                        }
                    }
                    else
                    {
                        Navigate(new Vector2(0, leftStickProController.y), currentMenuId);
                    }
                }

                // Is Released
                if (remoteState.pro.IsReleased(WiiU.ProControllerButton.Up))
                {
                    if (currentScrollRect == null)
                    {
                        Navigate(new Vector2(0, -1), currentMenuId);
                    }
                }
                else if (remoteState.pro.IsReleased(WiiU.ProControllerButton.Down))
                {
                    if (currentScrollRect == null)
                    {
                        Navigate(new Vector2(0, 1), currentMenuId);
                    }
                }
                else if (remoteState.pro.IsReleased(WiiU.ProControllerButton.A))
                {
                    ClickSelectedButton();
                }
                else if (remoteState.pro.IsReleased(WiiU.ProControllerButton.B))
                {
                    GoBack();
                }

                // Is Pressed
                if (remoteState.pro.IsPressed(WiiU.ProControllerButton.Up))
                {
                    if (currentScrollRect != null)
                    {
                        Navigate(new Vector2(0, 1), currentMenuId);
                    }
                }
                else if (remoteState.pro.IsPressed(WiiU.ProControllerButton.Down))
                {
                    if (currentScrollRect != null)
                    {
                        Navigate(new Vector2(0, -1), currentMenuId);
                    }
                }
                break;
            case WiiU.RemoteDevType.Classic:
                // Stick
                Vector2 leftStickClassicController = remoteState.classic.leftStick;

                if (Mathf.Abs(leftStickClassicController.y) > stickDeadzone)
                {
                    if (currentScrollRect == null)
                    {
                        if (Time.time - lastNavigationTime > stickNavigationCooldown)
                        {
                            Navigate(new Vector2(0, -leftStickClassicController.y), currentMenuId);

                            lastNavigationTime = Time.time;
                        }
                    }
                    else
                    {
                        Navigate(new Vector2(0, leftStickClassicController.y), currentMenuId);
                    }
                }

                // Is Released
                if (remoteState.classic.IsReleased(WiiU.ClassicButton.Up))
                {
                    if (currentScrollRect == null)
                    {
                        Navigate(new Vector2(0, -1), currentMenuId);
                    }
                }
                else if (remoteState.classic.IsReleased(WiiU.ClassicButton.Down))
                {
                    if (currentScrollRect == null)
                    {
                        Navigate(new Vector2(0, 1), currentMenuId);
                    }
                }
                else if (remoteState.classic.IsReleased(WiiU.ClassicButton.A))
                {
                    ClickSelectedButton();
                }
                else if (remoteState.classic.IsReleased(WiiU.ClassicButton.B))
                {
                    GoBack();
                }

                // Is Pressed
                if (remoteState.classic.IsPressed(WiiU.ClassicButton.Up))
                {
                    if (currentScrollRect != null)
                    {
                        Navigate(new Vector2(0, 1), currentMenuId);
                    }
                }
                else if (remoteState.classic.IsPressed(WiiU.ClassicButton.Down))
                {
                    if (currentScrollRect != null)
                    {
                        Navigate(new Vector2(0, -1), currentMenuId);
                    }
                }
                break;
            default:
                // Stick
                Vector2 stickNunchuk = remoteState.nunchuk.stick;

                if (Mathf.Abs(stickNunchuk.y) > stickDeadzone)
                {
                    if (currentScrollRect == null)
                    {
                        if (Time.time - lastNavigationTime > stickNavigationCooldown)
                        {
                            Navigate(new Vector2(0, -stickNunchuk.y), currentMenuId);

                            lastNavigationTime = Time.time;
                        }
                    }
                    else
                    {
                        Navigate(new Vector2(0, stickNunchuk.y), currentMenuId);
                    }
                }

                // Is Released
                if (remoteState.IsReleased(WiiU.RemoteButton.Up))
                {
                    if (currentScrollRect == null)
                    {
                        Navigate(new Vector2(0, -1), currentMenuId);
                    }
                }
                else if (remoteState.IsReleased(WiiU.RemoteButton.Down))
                {
                    if (currentScrollRect == null)
                    {
                        Navigate(new Vector2(0, 1), currentMenuId);
                    }
                }
                else if (remoteState.IsReleased(WiiU.RemoteButton.A))
                {
                    ClickSelectedButton();
                }
                else if (remoteState.IsReleased(WiiU.RemoteButton.B))
                {
                    GoBack();
                }

                // Is Pressed
                if (remoteState.IsPressed(WiiU.RemoteButton.Up))
                {
                    if (currentScrollRect != null)
                    {
                        Navigate(new Vector2(0, 1), currentMenuId);
                    }
                }
                else if (remoteState.IsPressed(WiiU.RemoteButton.Down))
                {
                    if (currentScrollRect != null)
                    {
                        Navigate(new Vector2(0, -1), currentMenuId);
                    }
                }
                break;
        }

        // Handle keyboard input, useful for testing in the editor
        if (Application.isEditor)
        {
            // Key Down
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentScrollRect == null)
                {
                    Navigate(new Vector2(0, -1), currentMenuId);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentScrollRect == null)
                {
                    Navigate(new Vector2(0, 1), currentMenuId);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                ClickSelectedButton();
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                GoBack();
            }

            // Key
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (currentScrollRect != null)
                {
                    Navigate(new Vector2(0, 1), currentMenuId);
                }
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                if (currentScrollRect != null)
                {
                    Navigate(new Vector2(0, -1), currentMenuId);
                }
            }

            float vertical = Input.GetAxis("LeftStickY");

            if (Mathf.Abs(vertical) > stickDeadzone)
            {
                if (currentScrollRect == null)
                {
                    if (lastNavigationTime > stickNavigationCooldown)
                    {
                        Debug.Log(vertical);

                        if (vertical > 0)
                        {
                            Navigate(new Vector2(0, 1), currentMenuId);
                        }
                        else
                        {
                            Navigate(new Vector2(0, 0), currentMenuId);
                        }

                        lastNavigationTime = 0f;
                    }
                }
                else
                {
                    Navigate(new Vector2(0, vertical), currentMenuId);
                }
            }
        }

        // Calculate stick last navigation time
        lastNavigationTime += Time.deltaTime;
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

        // Add the button to the correct menu list in the dictionary
        if (!menuButtons.ContainsKey(menuId))
        {
            menuButtons[menuId] = new List<GameObject>();
        }
        menuButtons[menuId].Add(newButton);
    }

    // Navigates through the menu buttons based on the direction
    public void Navigate(Vector2 direction, int menuId)
    {
        if (currentScrollRect == null)
        {
            if (!menuButtons.ContainsKey(menuId) || menuButtons[menuId].Count == 0) return;

            List<GameObject> currentMenuButtons = menuButtons[menuId];
            GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

            if (currentSelected == null)
            {
                currentSelected = currentMenuButtons[0];
            }

            int currentIndex = currentMenuButtons.IndexOf(currentSelected);
            int nextIndex = (currentIndex + (int)Mathf.Round(direction.y) + currentMenuButtons.Count) % currentMenuButtons.Count;

            EventSystem.current.SetSelectedGameObject(currentMenuButtons[nextIndex]);

            // Update the selectionPrefab position to the left of the selected button
            UpdateSelectionPosition(currentMenuButtons[nextIndex]);
        }
        else if (currentScrollRect != null)
        {
            float scrollAmount = direction.y * 0.5f * Time.deltaTime;
            Vector2 newPosition = currentScrollRect.normalizedPosition + new Vector2(0f, scrollAmount);
            newPosition.y = Mathf.Clamp01(newPosition.y);
            currentScrollRect.normalizedPosition = newPosition;
        }
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

    private void UpdateSelectionPosition(GameObject selectedButton)
    {
        if (currentSelection != null)
        {
            // Activate the selectionPrefab if it was deactivated
            if (!currentSelection.activeInHierarchy)
            {
                currentSelection.SetActive(true);
            }

            // Move the selectionPrefab to the left of the selected button
            RectTransform buttonRect = selectedButton.GetComponent<RectTransform>();
            RectTransform selectionRect = currentSelection.GetComponent<RectTransform>();

            // Adjust the position to the left of the button
            Vector2 newPos = new Vector2(buttonRect.transform.position.x - selectionRect.rect.width, buttonRect.transform.position.y);
            currentSelection.transform.position = newPos;
        }
    }

    // Disables visual elements for the deselected button
    private void DisableSelection()
    {
        if (currentSelection != null)
        {
            currentSelection.SetActive(false);
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

            // Update the selectionPrefab position to the first button
            UpdateSelectionPosition(menuButtons[menuId][0]);
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

            // Hide the selection when going back
            DisableSelection();

            // Change to the previous menu
            ChangeMenu(previousMenuId);
        }
    }

    public GameObject GetCurrentMenu()
    {
        if (currentMenuId >= 0 && currentMenuId < menus.Length)
        {
            return menus[currentMenuId].gameObject;
        }
        return null;
    }
}