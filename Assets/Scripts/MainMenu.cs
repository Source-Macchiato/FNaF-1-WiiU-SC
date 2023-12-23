using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class MainMenu : MonoBehaviour {

    public float NightNumber;
    public Text NightNumberDisplayer;

    WiiU.GamePad gamePad;

    MainMenuNavigation mainMenuNavigation;

    void Start()
    {
        mainMenuNavigation = FindObjectOfType<MainMenuNavigation>();

        NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

        NightNumberDisplayer.text = NightNumber.ToString();

        gamePad = WiiU.GamePad.access;
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;

        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
            {
                MainMenuNavigation();
            }
        }

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                MainMenuNavigation();
            }
        }  
    }

    void MainMenuNavigation()
    {
        if (mainMenuNavigation.selectedIndex == 0)
        {
            NightNumber = 1;
            PlayerPrefs.SetFloat("NightNumber", NightNumber);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Advertisement");
        }
        else if (mainMenuNavigation.selectedIndex == 1)
        {
            NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

            if (NightNumber == 1)
            {
                SceneManager.LoadScene("Advertisement");
            }
            else if (NightNumber > 1 && NightNumber < 6)
            {
                SceneManager.LoadScene("Office");
            }
        }
    }
}
