using UnityEngine;
using UnityEngine.SceneManagement;
using WiiU = UnityEngine.WiiU;

public class TheEnd : MonoBehaviour {

    WiiU.GamePad gamePad;

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
    }

	void Update ()
    {
        WiiU.GamePadState gamePadState = gamePad.state;

        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsPressed(WiiU.GamePadButton.A))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
	}
}
