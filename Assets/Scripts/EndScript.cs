using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WiiU = UnityEngine.WiiU;

public class EndScript : MonoBehaviour
{
    public Image theEndImage;

    public Sprite theEndNight5;
    public Sprite theEndNight6;
    public Sprite theEndCustomNight;

    private float nightNumber;

    // References to WiiU controllers
    WiiU.GamePad gamePad;
    WiiU.Remote remote;
    
	void Start()
	{
        // Access the WiiU GamePad and Remote
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        nightNumber = SaveManager.saveData.game.nightNumber;

        if (nightNumber == 5)
        {
            theEndImage.sprite = theEndNight5;
        }
        else if (nightNumber == 6)
        {
            theEndImage.sprite = theEndNight6;
        }
        else if (nightNumber == 7)
        {
            theEndImage.sprite = theEndCustomNight;
        }
    }
	
	void Update()
	{
        // Get the current state of the GamePad and Remote
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        // Gamepad
        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        // Remotes
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.A))
                {
                    SceneManager.LoadScene("MainMenu");
                }
                break;
            case WiiU.RemoteDevType.Classic:
                if (remoteState.classic.IsTriggered(WiiU.ClassicButton.A))
                {
                    SceneManager.LoadScene("MainMenu");
                }
                break;
            default:
                if (remoteState.IsTriggered(WiiU.RemoteButton.A))
                {
                    SceneManager.LoadScene("MainMenu");
                }
                break;
        }

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
