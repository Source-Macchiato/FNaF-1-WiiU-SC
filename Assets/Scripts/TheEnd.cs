using UnityEngine;
using UnityEngine.SceneManagement;
using WiiU = UnityEngine.WiiU;

public class TheEnd : MonoBehaviour {

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    void Start()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.UnloadSceneAsync("GameOver");
        SceneManager.UnloadSceneAsync("6AM");
        SceneManager.UnloadSceneAsync("NextNight");
        SceneManager.UnloadSceneAsync("Controlls");
        SceneManager.UnloadSceneAsync("Office");
        SceneManager.UnloadSceneAsync("Advertisement");
        SceneManager.UnloadSceneAsync("PowerOut");
        SceneManager.UnloadSceneAsync("CostumNight");

        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);
    }

	void Update ()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        // Gamepad
        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsPressed(WiiU.GamePadButton.A))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        // Rmote
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.A))
                {
                    SceneManager.LoadScene("MainMenu");
                }
                break;

            default:
                break;
        }

        // Keyboard
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
	}
}
