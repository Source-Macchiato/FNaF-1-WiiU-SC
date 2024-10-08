using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using WiiU = UnityEngine.WiiU;

public class MinimapNavigation : MonoBehaviour
{
    public Button defaultSelectedButton;
    public GameObject minimapPanel;

    // Get current selected button
    private GameObject selectedGameObject;
    private Button selectedButton;

    // Stick navigation
    private float stickNavigationCooldown = 0.2f;
    private float lastNavigationTime;
    private float stickDeadzone = 0.19f;

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

        // Can navigate only when the minimap is active
        if (minimapPanel.activeSelf)
        {
            // Set default selected button if no button is selected
            if (selectedButton == null)
            {
                NavigateTo(defaultSelectedButton);
            }

            // Check inputs and navigate
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                Vector2 leftStickGamepad = gamePadState.lStick;

                if (Mathf.Abs(leftStickGamepad.y) > stickDeadzone)
                {
                    if (lastNavigationTime > stickNavigationCooldown)
                    {
                        if (leftStickGamepad.y > stickDeadzone)
                        {
                            UpdateSelection();

                            NavigateTo(selectedButton.navigation.selectOnUp);
                        }
                        else if (leftStickGamepad.y < -stickDeadzone)
                        {
                            UpdateSelection();

                            NavigateTo(selectedButton.navigation.selectOnDown);
                        }

                        lastNavigationTime = 0f;
                    }
                }

                if (Mathf.Abs(leftStickGamepad.x) > stickDeadzone)
                {
                    if (lastNavigationTime > stickNavigationCooldown)
                    {
                        if (leftStickGamepad.x > stickDeadzone)
                        {
                            NavigateTo(selectedButton.navigation.selectOnLeft);
                        }
                        else if (leftStickGamepad.x < -stickDeadzone)
                        {
                            NavigateTo(selectedButton.navigation.selectOnRight);
                        }

                        lastNavigationTime = 0f;
                    }
                }

                if (gamePadState.IsTriggered(WiiU.GamePadButton.Up))
                {
                    UpdateSelection();

                    NavigateTo(selectedButton.navigation.selectOnUp);
                }
                else if (gamePadState.IsTriggered(WiiU.GamePadButton.Down))
                {
                    UpdateSelection();

                    NavigateTo(selectedButton.navigation.selectOnDown);
                }
                else if (gamePadState.IsTriggered(WiiU.GamePadButton.Left))
                {
                    UpdateSelection();

                    NavigateTo(selectedButton.navigation.selectOnLeft);
                }
                else if (gamePadState.IsTriggered(WiiU.GamePadButton.Right))
                {
                    UpdateSelection();

                    NavigateTo(selectedButton.navigation.selectOnRight);
                }
            }

            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    Vector2 leftStickProController = remoteState.pro.leftStick;

                    if (Mathf.Abs(leftStickProController.y) > stickDeadzone)
                    {
                        if (lastNavigationTime > stickNavigationCooldown)
                        {
                            if (leftStickProController.y > stickDeadzone)
                            {
                                UpdateSelection();

                                NavigateTo(selectedButton.navigation.selectOnUp);
                            }
                            else if (leftStickProController.y < -stickDeadzone)
                            {
                                UpdateSelection();

                                NavigateTo(selectedButton.navigation.selectOnDown);
                            }

                            lastNavigationTime = 0f;
                        }
                    }

                    if (Mathf.Abs(leftStickProController.x) > stickDeadzone)
                    {
                        if (lastNavigationTime > stickNavigationCooldown)
                        {
                            if (leftStickProController.x > stickDeadzone)
                            {
                                NavigateTo(selectedButton.navigation.selectOnLeft);
                            }
                            else if (leftStickProController.x < -stickDeadzone)
                            {
                                NavigateTo(selectedButton.navigation.selectOnRight);
                            }

                            lastNavigationTime = 0f;
                        }
                    }

                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Up))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnUp);
                    }
                    else if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Down))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnDown);
                    }
                    else if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Left))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnLeft);
                    }
                    else if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Right))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnRight);
                    }
                    break;
                case WiiU.RemoteDevType.Classic:
                    Vector2 leftStickClassicController = remoteState.classic.leftStick;

                    if (Mathf.Abs(leftStickClassicController.y) > stickDeadzone)
                    {
                        if (lastNavigationTime > stickNavigationCooldown)
                        {
                            if (leftStickClassicController.y > stickDeadzone)
                            {
                                UpdateSelection();

                                NavigateTo(selectedButton.navigation.selectOnUp);
                            }
                            else if (leftStickClassicController.y < -stickDeadzone)
                            {
                                UpdateSelection();

                                NavigateTo(selectedButton.navigation.selectOnDown);
                            }

                            lastNavigationTime = 0f;
                        }
                    }

                    if (Mathf.Abs(leftStickClassicController.x) > stickDeadzone)
                    {
                        if (lastNavigationTime > stickNavigationCooldown)
                        {
                            if (leftStickClassicController.x > stickDeadzone)
                            {
                                NavigateTo(selectedButton.navigation.selectOnLeft);
                            }
                            else if (leftStickClassicController.x < -stickDeadzone)
                            {
                                NavigateTo(selectedButton.navigation.selectOnRight);
                            }

                            lastNavigationTime = 0f;
                        }
                    }

                    if (remoteState.classic.IsTriggered(WiiU.ClassicButton.Up))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnUp);
                    }
                    else if (remoteState.classic.IsTriggered(WiiU.ClassicButton.Down))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnDown);
                    }
                    else if (remoteState.classic.IsTriggered(WiiU.ClassicButton.Left))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnLeft);
                    }
                    else if (remoteState.classic.IsTriggered(WiiU.ClassicButton.Right))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnRight);
                    }
                    break;
                default:
                    Vector2 stickNunchuk = remoteState.nunchuk.stick;

                    if (Mathf.Abs(stickNunchuk.y) > stickDeadzone)
                    {
                        if (lastNavigationTime > stickNavigationCooldown)
                        {
                            if (stickNunchuk.y > stickDeadzone)
                            {
                                UpdateSelection();

                                NavigateTo(selectedButton.navigation.selectOnUp);
                            }
                            else if (stickNunchuk.y < -stickDeadzone)
                            {
                                UpdateSelection();

                                NavigateTo(selectedButton.navigation.selectOnDown);
                            }

                            lastNavigationTime = 0f;
                        }
                    }

                    if (Mathf.Abs(stickNunchuk.x) > stickDeadzone)
                    {
                        if (lastNavigationTime > stickNavigationCooldown)
                        {
                            if (stickNunchuk.x > stickDeadzone)
                            {
                                NavigateTo(selectedButton.navigation.selectOnLeft);
                            }
                            else if (stickNunchuk.x < -stickDeadzone)
                            {
                                NavigateTo(selectedButton.navigation.selectOnRight);
                            }

                            lastNavigationTime = 0f;
                        }
                    }

                    if (remoteState.IsTriggered(WiiU.RemoteButton.Up))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnUp);
                    }
                    else if (remoteState.IsTriggered(WiiU.RemoteButton.Down))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnDown);
                    }
                    else if (remoteState.IsTriggered(WiiU.RemoteButton.Left))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnLeft);
                    }
                    else if (remoteState.IsTriggered(WiiU.RemoteButton.Right))
                    {
                        UpdateSelection();

                        NavigateTo(selectedButton.navigation.selectOnRight);
                    }
                    break;
            }

            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    UpdateSelection();

                    NavigateTo(selectedButton.navigation.selectOnUp);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    UpdateSelection();

                    NavigateTo(selectedButton.navigation.selectOnDown);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    UpdateSelection();

                    NavigateTo(selectedButton.navigation.selectOnLeft);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    UpdateSelection();

                    NavigateTo(selectedButton.navigation.selectOnRight);
                }
            }
        }
    }

    void UpdateSelection()
    {
        selectedGameObject = EventSystem.current.currentSelectedGameObject;
        selectedButton = selectedGameObject.GetComponent<Button>();
    }

    void NavigateTo(Selectable nextSelectable)
    {
        nextSelectable.Select();

        selectedButton = nextSelectable.GetComponent<Button>();

        selectedButton.onClick.Invoke();
    }
}