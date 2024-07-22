using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class CostumNight : MonoBehaviour {

    public float FreddyAmount;
    public float BonnieAmount;
    public float ChicaAmount;
    public float FoxyAmount;

    public float WhichNight;

    public GameObject FreddyShowerTop;
    public GameObject FreddyShowerBottom;

    public GameObject BonnieShowerTop;
    public GameObject BonnieShowerBottom;

    public GameObject ChicaShowerTop;
    public GameObject ChicaShowerBottom;

    public GameObject FoxyShowerTop;
    public GameObject FoxyShowerBottom;

    WiiU.GamePad gamePad;

    void Start ()
    {
        FreddyAmount = 1;
        BonnieAmount = 1;
        ChicaAmount = 1;
        FoxyAmount = 1;

        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.UnloadSceneAsync("GameOver");
        SceneManager.UnloadSceneAsync("6AM");
        SceneManager.UnloadSceneAsync("NextNight");
        SceneManager.UnloadSceneAsync("Controlls");
        SceneManager.UnloadSceneAsync("Office");
        SceneManager.UnloadSceneAsync("Advertisement");
        SceneManager.UnloadSceneAsync("PowerOut");
        SceneManager.UnloadSceneAsync("TheEnd");

        gamePad = WiiU.GamePad.access;
    }
	
	void Update ()
    {
        WiiU.GamePadState gamePadState = gamePad.state;

        PlayerPrefs.SetFloat("BonnieDifficulty", FreddyAmount);
        PlayerPrefs.SetFloat("ChicaDifficulty", BonnieAmount);
        PlayerPrefs.SetFloat("FreddyDifficulty", ChicaAmount);
        PlayerPrefs.SetFloat("FoxyDifficulty", FoxyAmount);
        PlayerPrefs.Save();

        FreddyShowerTop.GetComponent<Text>().text = FreddyAmount.ToString();
        BonnieShowerTop.GetComponent<Text>().text = BonnieAmount.ToString();
        ChicaShowerTop.GetComponent<Text>().text = ChicaAmount.ToString();
        FoxyShowerTop.GetComponent<Text>().text = FoxyAmount.ToString();

        FreddyShowerBottom.GetComponent<Text>().text = FreddyAmount.ToString();
        BonnieShowerBottom.GetComponent<Text>().text = BonnieAmount.ToString();
        ChicaShowerBottom.GetComponent<Text>().text = ChicaAmount.ToString();
        FoxyShowerBottom.GetComponent<Text>().text = FoxyAmount.ToString();

        if (FreddyAmount >= 20)
        {
            FreddyAmount = 20;
        }
        if (FreddyAmount <= 0)
        {
            FreddyAmount = 0;
        }

        if (BonnieAmount >= 20)
        {
            BonnieAmount = 20;
        }
        if (BonnieAmount <= 0)
        {
            BonnieAmount = 0;
        }

        if (ChicaAmount >= 20)
        {
            ChicaAmount = 20;
        }
        if (ChicaAmount <= 0)
        {
            ChicaAmount = 0;
        }

        if (FoxyAmount >= 20)
        {
            FoxyAmount = 20;
        }
        if (FoxyAmount <= 0)
        {
            FoxyAmount = 0;
        }

        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsPressed(WiiU.GamePadButton.A))
            {
                WhichNight = 7;
                SceneManager.LoadScene("Office");
            }

            if (gamePadState.IsPressed(WiiU.GamePadButton.B))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                WhichNight = 7;
                SceneManager.LoadScene("Office");
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        PlayerPrefs.SetFloat("WhichNight", WhichNight);
        PlayerPrefs.Save();
    }

    public void PlusFreddy()
    {
        FreddyAmount += 1;
    }

    public void MinFreddy()
    {
        FreddyAmount -= 1;
    }


    public void PlusBonnie()
    {
        BonnieAmount += 1;
    }

    public void MinBonnie()
    {
        BonnieAmount -= 1;
    }


    public void PlusChica()
    {
        ChicaAmount += 1;
    }

    public void MinChica()
    {
        ChicaAmount -= 1;
    }


    public void PlusFoxy()
    {
        FoxyAmount += 1;
    }

    public void MinFoxy()
    {
        FoxyAmount -= 1;
    }
}
