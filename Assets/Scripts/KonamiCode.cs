using UnityEngine;
using WiiU = UnityEngine.WiiU;
using UnityEngine.SceneManagement;

public class KonamiCode : MonoBehaviour
{
    // References to WiiU controllers
    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    private WiiU.GamePadButton[] KonamiCodeGamepad =
    {
        WiiU.GamePadButton.Up, WiiU.GamePadButton.Up, WiiU.GamePadButton.Down, WiiU.GamePadButton.Down,
        WiiU.GamePadButton.Left, WiiU.GamePadButton.Right, WiiU.GamePadButton.Left, WiiU.GamePadButton.Right,
        WiiU.GamePadButton.B, WiiU.GamePadButton.A
    };

    private WiiU.ProControllerButton[] KonamiCodeProController =
    {
        WiiU.ProControllerButton.Up, WiiU.ProControllerButton.Up, WiiU.ProControllerButton.Down, WiiU.ProControllerButton.Down,
        WiiU.ProControllerButton.Left, WiiU.ProControllerButton.Right, WiiU.ProControllerButton.Left,
        WiiU.ProControllerButton.Right, WiiU.ProControllerButton.B, WiiU.ProControllerButton.A
    };

    private WiiU.ClassicButton[] KonamiCodeClassicController =
    {
        WiiU.ClassicButton.Up, WiiU.ClassicButton.Up, WiiU.ClassicButton.Down, WiiU.ClassicButton.Down,
        WiiU.ClassicButton.Left, WiiU.ClassicButton.Right, WiiU.ClassicButton.Left, WiiU.ClassicButton.Right,
        WiiU.ClassicButton.B, WiiU.ClassicButton.A
    };

    private WiiU.RemoteButton[] KonamiCodeRemote =
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
        if (gamePadState.IsTriggered(KonamiCodeGamepad[konamiIndexGamepad]))
        {
            konamiIndexGamepad++;

            if (konamiIndexGamepad == KonamiCodeGamepad.Length)
            {
                Debug.Log("Konami code activated with Gamepad !");

                LoadEasterEggScene();
            }
        }
        else if (Input.anyKeyDown)
        {
            Debug.Log("Error in Konami code");

            konamiIndexGamepad = 0;
        }

        // Remote Konami code combo
        switch(remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsTriggered(KonamiCodeProController[konamiIndexProController]))
                {
                    konamiIndexProController++;

                    if (konamiIndexProController == KonamiCodeProController.Length)
                    {
                        Debug.Log("Konami code activated with Pro Controller !");

                        LoadEasterEggScene();
                    }
                }
                else if (Input.anyKeyDown)
                {
                    Debug.Log("Error in Konami code");

                    konamiIndexProController = 0;
                }
                break;
            case WiiU.RemoteDevType.Classic:
                if (remoteState.classic.IsTriggered(KonamiCodeClassicController[konamiIndexClassicController]))
                {
                    konamiIndexClassicController++;

                    if (konamiIndexClassicController == KonamiCodeClassicController.Length)
                    {
                        Debug.Log("Konami code activated with Classic Controller");

                        LoadEasterEggScene();
                    }
                }
                else if (Input.anyKeyDown)
                {
                    Debug.Log("Error in Konami code");

                    konamiIndexClassicController = 0;
                }
                break;
            default:
                if (remoteState.IsTriggered(KonamiCodeRemote[konamiIndexRemote]))
                {
                    konamiIndexRemote++;

                    if (konamiIndexRemote == KonamiCodeRemote.Length)
                    {
                        Debug.Log("Konami code activated with Wiimote");

                        LoadEasterEggScene();
                    }
                }
                else if (Input.anyKeyDown)
                {
                    Debug.Log("Error in Konami code");

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
                Debug.Log("Error in Konami code");

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
