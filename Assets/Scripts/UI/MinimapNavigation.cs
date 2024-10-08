using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class MinimapNavigation : MonoBehaviour
{
    public Button defaultSelectedButton;
    public GameObject minimapPanel;

    // Get current selected button
    private GameObject selectedGameObject;
    private Button selectedButton;
    private Button blinkingButton;

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
                if (gamePadState.IsTriggered(WiiU.GamePadButton.Up))
                {
                    NavigateTo(selectedButton.navigation.selectOnUp);
                }
                else if (gamePadState.IsTriggered(WiiU.GamePadButton.Down))
                {
                    NavigateTo(selectedButton.navigation.selectOnDown);
                }
                else if (gamePadState.IsTriggered(WiiU.GamePadButton.Left))
                {
                    NavigateTo(selectedButton.navigation.selectOnLeft);
                }
                else if (gamePadState.IsTriggered(WiiU.GamePadButton.Right))
                {
                    NavigateTo(selectedButton.navigation.selectOnRight);
                }
            }

            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Up))
                    {
                        NavigateTo(selectedButton.navigation.selectOnUp);
                    }
                    else if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Down))
                    {
                        NavigateTo(selectedButton.navigation.selectOnDown);
                    }
                    else if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Left))
                    {
                        NavigateTo(selectedButton.navigation.selectOnLeft);
                    }
                    else if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Right))
                    {
                        NavigateTo(selectedButton.navigation.selectOnRight);
                    }
                    break;
                case WiiU.RemoteDevType.Classic:
                    if (remoteState.classic.IsTriggered(WiiU.ClassicButton.Up))
                    {
                        NavigateTo(selectedButton.navigation.selectOnUp);
                    }
                    else if (remoteState.classic.IsTriggered(WiiU.ClassicButton.Down))
                    {
                        NavigateTo(selectedButton.navigation.selectOnDown);
                    }
                    else if (remoteState.classic.IsTriggered(WiiU.ClassicButton.Left))
                    {
                        NavigateTo(selectedButton.navigation.selectOnLeft);
                    }
                    else if (remoteState.classic.IsTriggered(WiiU.ClassicButton.Right))
                    {
                        NavigateTo(selectedButton.navigation.selectOnRight);
                    }
                    break;
                default:
                    if (remoteState.IsTriggered(WiiU.RemoteButton.Up))
                    {
                        NavigateTo(selectedButton.navigation.selectOnUp);
                    }
                    else if (remoteState.IsTriggered(WiiU.RemoteButton.Down))
                    {
                        NavigateTo(selectedButton.navigation.selectOnDown);
                    }
                    else if (remoteState.IsTriggered(WiiU.RemoteButton.Left))
                    {
                        NavigateTo(selectedButton.navigation.selectOnLeft);
                    }
                    else if (remoteState.IsTriggered(WiiU.RemoteButton.Right))
                    {
                        NavigateTo(selectedButton.navigation.selectOnRight);
                    }
                    break;
            }

            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    NavigateTo(selectedButton.navigation.selectOnUp);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    NavigateTo(selectedButton.navigation.selectOnDown);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    NavigateTo(selectedButton.navigation.selectOnLeft);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    NavigateTo(selectedButton.navigation.selectOnRight);
                }
            }
        }
    }

    void NavigateTo(Selectable nextSelectable)
    {
        nextSelectable.Select();

        selectedButton = nextSelectable.GetComponent<Button>();

        selectedButton.onClick.Invoke();
    }
}
