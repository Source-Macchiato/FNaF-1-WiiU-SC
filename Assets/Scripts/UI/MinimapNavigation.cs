using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using WiiU = UnityEngine.WiiU;

public class MinimapNavigation : MonoBehaviour
{
    public Button defaultSelectedButton;

    // Get current selected button
    public GameObject selectedGameObject;
    private Button selectedButton;

    // References to WiiU controllers
    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    void Start()
    {
        // Access the WiiU GamePad and Remote
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        NavigateTo(defaultSelectedButton);
    }

    void Update()
    {
        // Get the current state of the GamePad and Remote
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        // Check inputs and navigate
        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsReleased(WiiU.GamePadButton.Up))
            {
                UpdateSelection();

                NavigateTo(selectedButton.navigation.selectOnUp);
            }
            else if (gamePadState.IsReleased(WiiU.GamePadButton.Down))
            {
                UpdateSelection();

                NavigateTo(selectedButton.navigation.selectOnDown);
            }
            else if (gamePadState.IsReleased(WiiU.GamePadButton.Left))
            {
                UpdateSelection();

                NavigateTo(selectedButton.navigation.selectOnLeft);
            }
            else if (gamePadState.IsReleased(WiiU.GamePadButton.Right))
            {
                UpdateSelection();

                NavigateTo(selectedButton.navigation.selectOnRight);
            }
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
