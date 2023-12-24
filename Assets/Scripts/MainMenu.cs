using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class MainMenu : MonoBehaviour {

    public GameObject  advertisementImage;
    private float startTime;
    private float waitTime = 10f;
    private bool advertisementIsActive;
    public float NightNumber;
    public Text NightNumberDisplayer;

    WiiU.GamePad gamePad;

    MainMenuNavigation mainMenuNavigation;

    void Start()
    {
        advertisementIsActive = false;

        advertisementImage.SetActive(false);
        mainMenuNavigation = FindObjectOfType<MainMenuNavigation>();

        NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

        NightNumberDisplayer.text = NightNumber.ToString();

        gamePad = WiiU.GamePad.access;
    }

    void Update()
    {
        if (Time.time - startTime >= waitTime && advertisementIsActive == true)
        {
            // load scene after advertisementload is called
            SceneManager.LoadScene("NextNight");
        }

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
            AdvertisementLoaded();
        }
        else if (mainMenuNavigation.selectedIndex == 1)
        {
            NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

            if (NightNumber == 1)
            {
                AdvertisementLoaded();
            }
            else if (NightNumber > 1 && NightNumber < 6)
            {
                SceneManager.LoadScene("NextNight");
            }
        }
    }

    private void AdvertisementLoaded()
    {
        advertisementIsActive = true;
        startTime = Time.time;
        advertisementImage.SetActive(true);
    }
}
