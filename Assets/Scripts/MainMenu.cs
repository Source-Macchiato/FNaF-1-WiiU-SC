using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class MainMenu : MonoBehaviour {

    public float WichNight = 1;
    public GameObject WichNightShower;

    WiiU.GamePad gamePad;

    void Start()
    {

        if (WichNight >= 5)
        {
            WichNight = 5;
        }

        gamePad = WiiU.GamePad.access;
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;

        WichNight = PlayerPrefs.GetFloat("WichNight", WichNight);

        WichNightShower.GetComponent<Text>().text = WichNight.ToString();

        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsPressed(WiiU.GamePadButton.B))
            {
                WichNight = 1;
                PlayerPrefs.SetFloat("WichNight", WichNight);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Advertisement");
            }
        }

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                
                WichNight = 1;
                PlayerPrefs.SetFloat("WichNight", WichNight);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Advertisement");
            }
        }  
    }
}
