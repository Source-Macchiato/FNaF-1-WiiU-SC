using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class MainMenu : MonoBehaviour {

    public float WichNight = 1;
    public GameObject WichNightShower;
    public GameObject Music;

    public GameObject ExtraNight;
    public bool ExtraNightEnabled = false;

    public GameObject CostumNight;
    public bool CostumNightEnabled = false;

    WiiU.GamePad gamePad;

    void Start()
    {
        SceneManager.UnloadSceneAsync("GameOver");
        SceneManager.UnloadSceneAsync("6AM");
        SceneManager.UnloadSceneAsync("NextNight");
        SceneManager.UnloadSceneAsync("Controlls");
        SceneManager.UnloadSceneAsync("Office");
        SceneManager.UnloadSceneAsync("Advertisement");
        SceneManager.UnloadSceneAsync("PowerOut");
        SceneManager.UnloadSceneAsync("TheEnd");
        SceneManager.UnloadSceneAsync("CostumNight");

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
                Music.SetActive(false);
                WichNight = 1;
                PlayerPrefs.SetFloat("WichNight", WichNight);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Advertisement");
            }

            if (gamePadState.IsPressed(WiiU.GamePadButton.A))
            {
                Music.SetActive(false);
                if (WichNight == 1)
                {
                    WichNight = 1;
                    PlayerPrefs.SetFloat("WichNight", WichNight);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Advertisement");
                }
                if (WichNight >= 2)
                {
                    SceneManager.LoadScene("Office");
                }
            }

            if (gamePadState.IsPressed(WiiU.GamePadButton.Y))
            {
                Music.SetActive(false);
                SceneManager.LoadScene("Controlls");
            }

            if (gamePadState.IsPressed(WiiU.GamePadButton.X))
            {
                if (ExtraNightEnabled)
                {
                    WichNight = 6;
                    PlayerPrefs.SetFloat("WichNight", WichNight);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Office");
                }
            }
        }

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                Music.SetActive(false);
                WichNight = 1;
                PlayerPrefs.SetFloat("WichNight", WichNight);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Advertisement");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Music.SetActive(false);
                if (WichNight == 1)
                {
                    WichNight = 1;
                    PlayerPrefs.SetFloat("WichNight", WichNight);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Advertisement");
                }
                if (WichNight >= 2)
                {
                    SceneManager.LoadScene("Office");
                }
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                Music.SetActive(false);
                SceneManager.LoadScene("Controlls");
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (ExtraNightEnabled)
                {
                    WichNight = 6;
                    PlayerPrefs.SetFloat("WichNight", WichNight);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Office");
                }
            }
        }

        if (WichNight >= 5)
        {
            WichNight = 5;
            ExtraNightEnabled = true;
            CostumNightEnabled = true;
        }

        if (ExtraNightEnabled)
        {
            ExtraNight.SetActive(true);
        }
        if (CostumNightEnabled)
        {
            CostumNight.SetActive(true);
        }     
    }

    public void CostumNightEnter()
    {
        SceneManager.LoadScene("CostumNight");
    }
}
