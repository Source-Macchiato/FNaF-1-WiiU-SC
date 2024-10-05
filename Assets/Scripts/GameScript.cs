using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    private float nightNumber;
    public float Time = 535; //fixed i hope -- who wrote this ? who are you ?
    public float Usage = 1;

    public GameObject nightNumberDisplayer;
    public GameObject powerDisplayer;
    public GameObject timeDisplayer;

    public float PowerUsage = 1;
    public float PowerDrain = 1;
    public float PowerLeft = 100;
    
    public GameObject Bar1, Bar2, Bar3, Bar4, Bar5;

    public AudioSource Call1;
    public AudioSource Call2;
    public AudioSource Call3;
    public AudioSource Call4;
    public AudioSource Call5;

    private Text textNightNumber;
    private Text textPowerDisplayer;
    private Text textTimeDisplayer;
    private TMP_Text tmpTextNightNumber;
    private TMP_Text tmpTextPowerDisplayer;
    private TMP_Text tmpTextTimeDisplayer;

    SaveGameState saveGameState;
    SaveManager saveManager;

    void Start()
    {
        textNightNumber = nightNumberDisplayer.GetComponent<Text>();
        textPowerDisplayer = powerDisplayer.GetComponent<Text>();
        textTimeDisplayer = timeDisplayer.GetComponent<Text>();
        tmpTextNightNumber = nightNumberDisplayer.GetComponent<TextMeshProUGUI>();
        tmpTextPowerDisplayer = powerDisplayer.GetComponent<TextMeshProUGUI>();
        tmpTextTimeDisplayer = timeDisplayer.GetComponent<TextMeshProUGUI>();

        nightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

        if (textNightNumber != null)
        {
            textNightNumber.text = nightNumber.ToString();
        }

        if (tmpTextNightNumber != null)
        {
            tmpTextNightNumber.text = nightNumber.ToString();
        }

        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        //--------------------------------------CallAndNight----------------------//
        if (nightNumber == 1)
        {
            Call1.Play();
        }

        if (nightNumber == 2)
        {
            Call2.Play();
        }

        if (nightNumber == 3)
        {
            Call3.Play();
        }

        if (nightNumber == 4)
        {
            Call4.Play();
        }

        if (nightNumber == 5)
        {
            Call5.Play();
        }
        //-------------------------------CallAndNight-----------------------------//
    }

    public void DebugTwelveAMButton()
    {
        Time = 536;
    }
    public void DebugOneAMButton()
    {
        Time = 446;
    }
    public void DebugTwoAMButton()
    {
        Time = 355;
    }
    public void DebugThreeAMButton()
    {
        Time = 268;
    }
    public void DebugFourAMButton()
    {
        Time = 179;
    }
    public void DebugFiveAMButton()
    {
        Time = 90;
    }
    public void DebugSixAMButton()
    {
        Time = 1;
    }
    void Update () //(max) wow this function proves how much you hate else ifs
    {
        //---------------------------------------TIME-------------------------------------//
        Time -= UnityEngine.Time.deltaTime;

        switch((int)Time)
        {
            case 535:
                if (textTimeDisplayer != null)
                {
                    textTimeDisplayer.text = "12 AM";
                }

                if (tmpTextTimeDisplayer != null)
                {
                    tmpTextTimeDisplayer.text = "12 AM";
                }
            break;
                
            case 445:
                if (textTimeDisplayer != null)
                {
                    textTimeDisplayer.text = "1 AM";
                }

                if (tmpTextTimeDisplayer != null)
                {
                    tmpTextTimeDisplayer.text = "1 AM";
                }
                break;

            case 354:
                if (textTimeDisplayer != null)
                {
                    textTimeDisplayer.text = "2 AM";
                }

                if (tmpTextTimeDisplayer != null)
                {
                    tmpTextTimeDisplayer.text = "2 AM";
                }
                break;
            
            case 267:
                if (textTimeDisplayer != null)
                {
                    textTimeDisplayer.text = "3 AM";
                }

                if (tmpTextTimeDisplayer != null)
                {
                    tmpTextTimeDisplayer.text = "3 AM";
                }
                break;

            case 178:
                if (textTimeDisplayer != null)
                {
                    textTimeDisplayer.text = "4 AM";
                }

                if (tmpTextTimeDisplayer != null)
                {
                    tmpTextTimeDisplayer.text = "4 AM";
                }
                break;

            case 89:
                if (textTimeDisplayer != null)
                {
                    textTimeDisplayer.text = "5 AM";
                }

                if (tmpTextTimeDisplayer != null)
                {
                    tmpTextTimeDisplayer.text = "5 AM";
                }
                break;
            
            case 0:
                if (textTimeDisplayer != null)
                {
                    textTimeDisplayer.text = "6 AM";
                }

                if (tmpTextTimeDisplayer != null)
                {
                    tmpTextTimeDisplayer.text = "6 AM";
                }

                saveManager.SaveNightNumber(nightNumber + 1);
                bool saveResult = saveGameState.DoSave();

                SceneManager.LoadScene("6AM");
            break;
        }
        //--------------------------------------TIME------------------------------//

        //-------------------------------PowerUsage-------------------------------//
        if (PowerUsage <= 0)
        {
            PowerUsage = 1;
        }

        if (PowerUsage >= 5)
        {
            PowerUsage = 5;
        }

        if (PowerUsage == 1)
        {
            PowerDrain -= UnityEngine.Time.deltaTime;

            if (PowerDrain <= 0)
            {
                PowerLeft -= 1;
               
                if (textPowerDisplayer != null)
                {
                    textPowerDisplayer.text = PowerLeft.ToString();
                }

                if (tmpTextPowerDisplayer != null)
                {
                    tmpTextPowerDisplayer.text = PowerLeft.ToString();
                }

                PowerDrain = 10;
            }

            Bar1.SetActive(true);
            Bar2.SetActive(false);
            Bar3.SetActive(false);
            Bar4.SetActive(false);
            Bar5.SetActive(false);
        }

        if (PowerUsage == 2)
        {
            PowerDrain -= UnityEngine.Time.deltaTime;

            if (PowerDrain <= 0)
            {
                PowerLeft -= 1;

                if (textPowerDisplayer != null)
                {
                    textPowerDisplayer.text = PowerLeft.ToString();
                }

                if (tmpTextPowerDisplayer != null)
                {
                    tmpTextPowerDisplayer.text = PowerLeft.ToString();
                }

                PowerDrain = 4.5f;
            }

            Bar1.SetActive(false);
            Bar2.SetActive(true);
            Bar3.SetActive(false);
            Bar4.SetActive(false);
            Bar5.SetActive(false);
        }

        if (PowerUsage == 3)
        {
            PowerDrain -= UnityEngine.Time.deltaTime;

            if (PowerDrain <= 0)
            {
                PowerLeft -= 1;

                if (textPowerDisplayer != null)
                {
                    textPowerDisplayer.text = PowerLeft.ToString();
                }

                if (tmpTextPowerDisplayer != null)
                {
                    tmpTextPowerDisplayer.text = PowerLeft.ToString();
                }

                PowerDrain = 2.3f;
            }

            Bar1.SetActive(false);
            Bar2.SetActive(false);
            Bar3.SetActive(true);
            Bar4.SetActive(false);
            Bar5.SetActive(false);
        }

        if (PowerUsage == 4)
        {
            PowerDrain -= UnityEngine.Time.deltaTime;

            if (PowerDrain <= 0)
            {
                PowerLeft -= 1;

                if (textPowerDisplayer != null)
                {
                    textPowerDisplayer.text = PowerLeft.ToString();
                }

                if (tmpTextPowerDisplayer != null)
                {
                    tmpTextPowerDisplayer.text = PowerLeft.ToString();
                }

                PowerDrain = 1.1f;
            }

            Bar1.SetActive(false);
            Bar2.SetActive(false);
            Bar3.SetActive(false);
            Bar4.SetActive(true);
            Bar5.SetActive(false);
        }

        if (PowerUsage == 5)
        {
            PowerDrain -= UnityEngine.Time.deltaTime;

            if (PowerDrain <= 0)
            {
                PowerLeft -= 1;

                if (textPowerDisplayer != null)
                {
                    textPowerDisplayer.text = PowerLeft.ToString();
                }

                if (tmpTextPowerDisplayer != null)
                {
                    tmpTextPowerDisplayer.text = PowerLeft.ToString();
                }

                PowerDrain = 0.6f;
            }

            Bar1.SetActive(false);
            Bar2.SetActive(false);
            Bar3.SetActive(false);
            Bar4.SetActive(false);
            Bar5.SetActive(true);
        }

        if (PowerLeft <= 0)
        {
            SceneManager.LoadScene("PowerOut");
        }
        //--------------------------------PowerUsage---------------------------//

    }
}
