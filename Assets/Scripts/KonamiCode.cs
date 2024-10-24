using UnityEngine;
using WiiU = UnityEngine.WiiU;
using UnityEngine.SceneManagement;

public class KonamiCode : MonoBehaviour
{
    WiiU.GamePad gamePad;

    private WiiU.GamePadButton[] KonamiCodeWiiU = {
        WiiU.GamePadButton.Up, WiiU.GamePadButton.Up, WiiU.GamePadButton.Down, WiiU.GamePadButton.Down,
        WiiU.GamePadButton.Left, WiiU.GamePadButton.Right, WiiU.GamePadButton.Left, WiiU.GamePadButton.Right
    };

    private KeyCode[] konamiCodePC = {
        KeyCode.UpArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow,
        KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.LeftArrow, KeyCode.RightArrow
    };

    private int konamiIndexWiiU = 0;
    private int konamiIndexPC = 0;

    void Start()
    {
        gamePad = WiiU.GamePad.access;
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;

        if (gamePadState.IsPressed(KonamiCodeWiiU[konamiIndexWiiU]))
        {
            konamiIndexWiiU++;

            if (konamiIndexWiiU == KonamiCodeWiiU.Length)
            {
                Debug.Log("Konami Code activé sur WiiU!");
                LoadEasterEggScene();
                konamiIndexWiiU = 0;
            }
        }
        else if (AnyWrongButtonPressed(gamePadState))
        {
            konamiIndexWiiU = 0;
        }

        if (Input.GetKeyDown(konamiCodePC[konamiIndexPC]))
        {
            konamiIndexPC++;

            if (konamiIndexPC == konamiCodePC.Length)
            {
                Debug.Log("Konami Code activé sur PC!");
                LoadEasterEggScene();
                konamiIndexPC = 0;
            }
        }
        else if (Input.anyKeyDown)
        {
            konamiIndexPC = 0;
        }
    }

    private bool AnyWrongButtonPressed(WiiU.GamePadState gamePadState)
    {
        WiiU.GamePadButton[] allButtons = {
            WiiU.GamePadButton.Up, WiiU.GamePadButton.Down, WiiU.GamePadButton.Left, WiiU.GamePadButton.Right,
            WiiU.GamePadButton.A, WiiU.GamePadButton.B, WiiU.GamePadButton.X, WiiU.GamePadButton.Y,
            WiiU.GamePadButton.L, WiiU.GamePadButton.R, WiiU.GamePadButton.ZL, WiiU.GamePadButton.ZR,
            WiiU.GamePadButton.Plus, WiiU.GamePadButton.Minus
        };

        foreach (WiiU.GamePadButton button in allButtons)
        {
            if (gamePadState.IsPressed(button) && button != KonamiCodeWiiU[konamiIndexWiiU])
            {
                return true;
            }
        }
        return false;
    }

    void LoadEasterEggScene()
    {
        SceneManager.LoadScene("EasterEggScene");
    }
}
