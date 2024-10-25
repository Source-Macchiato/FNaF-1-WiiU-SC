using UnityEngine;
using WiiU = UnityEngine.WiiU;
using UnityEngine.SceneManagement;

public class KonamiCode : MonoBehaviour
{
    // References to WiiU controllers
    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    private WiiU.GamePadButton[] konamiCodeGamepad =
    {
        WiiU.GamePadButton.Up, WiiU.GamePadButton.Up, WiiU.GamePadButton.Down, WiiU.GamePadButton.Down,
        WiiU.GamePadButton.Left, WiiU.GamePadButton.Right, WiiU.GamePadButton.Left, WiiU.GamePadButton.Right,
        WiiU.GamePadButton.B, WiiU.GamePadButton.A
    };

    private WiiU.ProControllerButton[] konamiCodeProController =
    {
        WiiU.ProControllerButton.Up, WiiU.ProControllerButton.Up, WiiU.ProControllerButton.Down, WiiU.ProControllerButton.Down,
        WiiU.ProControllerButton.Left, WiiU.ProControllerButton.Right, WiiU.ProControllerButton.Left,
        WiiU.ProControllerButton.Right, WiiU.ProControllerButton.B, WiiU.ProControllerButton.A
    };

    private WiiU.ClassicButton[] konamiCodeClassicController =
    {
        WiiU.ClassicButton.Up, WiiU.ClassicButton.Up, WiiU.ClassicButton.Down, WiiU.ClassicButton.Down,
        WiiU.ClassicButton.Left, WiiU.ClassicButton.Right, WiiU.ClassicButton.Left, WiiU.ClassicButton.Right,
        WiiU.ClassicButton.B, WiiU.ClassicButton.A
    };

    private WiiU.RemoteButton[] konamiCodeRemote =
    {
        WiiU.RemoteButton.Up, WiiU.RemoteButton.Up, WiiU.RemoteButton.Down, WiiU.RemoteButton.Down,
        WiiU.RemoteButton.Left, WiiU.RemoteButton.Right, WiiU.RemoteButton.Left, WiiU.RemoteButton.Right,
        WiiU.RemoteButton.B, WiiU.RemoteButton.A
    };

    private KeyCode[] konamiCodePC =
    {
        KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.LeftArrow,
        KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.B, KeyCode.A
    };

    private int konamiIndexGamepad = 0;
    private int konamiIndexProController = 0;
    private int konamiIndexClassicController = 0;
    private int konamiIndexRemote = 0;
    private int konamiIndexPC = 0;

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

        // Gamepad Konami code combo
        if (gamePadState.IsTriggered(konamiCodeGamepad[konamiIndexGamepad]))
        {
            konamiIndexGamepad++;

            if (konamiIndexGamepad == konamiCodeGamepad.Length)
            {
                Debug.Log("Konami code activated with Gamepad !");

                LoadEasterEggScene();
            }
        }
        else if (Input.anyKeyDown)
        {
            konamiIndexGamepad = 0;
        }

        // Remote Konami code combo
        switch(remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsTriggered(konamiCodeProController[konamiIndexProController]))
                {
                    konamiIndexProController++;

                    if (konamiIndexProController == konamiCodeProController.Length)
                    {
                        Debug.Log("Konami code activated with Pro Controller !");

                        LoadEasterEggScene();
                    }
                }
                else if (Input.anyKeyDown)
                {
                    konamiIndexProController = 0;
                }
                break;
            case WiiU.RemoteDevType.Classic:
                if (remoteState.classic.IsTriggered(konamiCodeClassicController[konamiIndexClassicController]))
                {
                    konamiIndexClassicController++;

                    if (konamiIndexClassicController == konamiCodeClassicController.Length)
                    {
                        Debug.Log("Konami code activated with Classic Controller");

                        LoadEasterEggScene();
                    }
                }
                else if (Input.anyKeyDown)
                {
                    konamiIndexClassicController = 0;
                }
                break;
            default:
                if (remoteState.IsTriggered(konamiCodeRemote[konamiIndexRemote]))
                {
                    konamiIndexRemote++;

                    if (konamiIndexRemote == konamiCodeRemote.Length)
                    {
                        Debug.Log("Konami code activated with Wiimote");

                        LoadEasterEggScene();
                    }
                }
                else if (Input.anyKeyDown)
                {
                    konamiIndexRemote = 0;
                }
                break;
        }

        // Keyboard Konami code combo
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(konamiCodePC[konamiIndexPC]))
            {
                konamiIndexPC++;

                if (konamiIndexPC == konamiCodePC.Length)
                {
                    Debug.Log("Konami code activated with keyboard !");

                    LoadEasterEggScene();
                }
            }
            else if (Input.anyKeyDown)
            {
                konamiIndexPC = 0;
            }
        }
    }

    /*private bool AnyWrongButtonPressed(WiiU.GamePadState gamePadState)
    {
        WiiU.GamePadButton[] allButtons = {
            WiiU.GamePadButton.Up, WiiU.GamePadButton.Down, WiiU.GamePadButton.Left, WiiU.GamePadButton.Right,
            WiiU.GamePadButton.A, WiiU.GamePadButton.B, WiiU.GamePadButton.X, WiiU.GamePadButton.Y,
            WiiU.GamePadButton.L, WiiU.GamePadButton.R, WiiU.GamePadButton.ZL, WiiU.GamePadButton.ZR,
            WiiU.GamePadButton.Plus, WiiU.GamePadButton.Minus
        };

        foreach (WiiU.GamePadButton button in allButtons)
        {
            if (gamePadState.IsPressed(button) && button != KonamiCodeGamepad[konamiIndexGamepad])
            {
                return true;
            }
        }
        return false;
    }*/

    void LoadEasterEggScene()
    {
        SceneManager.LoadScene("EasterEgg");
    }
}
