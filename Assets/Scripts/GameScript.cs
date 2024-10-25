using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    private float nightNumber;
    public float timeRemaining = 535; //fixed i hope -- who wrote this ? who are you ?
    public float Usage = 1;

    public GameObject nightNumberDisplayer;
    public GameObject powerDisplayer;
    public GameObject timeDisplayer;

    public float PowerUsage = 1;
    public float PowerDrain = 1;
    public float PowerLeft = 100;
    
    public GameObject Bar1, Bar2, Bar3, Bar4, Bar5;
    public GameObject officeContainer;

    private RectTransform officeRect;
    public static float officePositionX;

    private Text textNightNumber;
    private Text textPowerDisplayer;
    private Text textTimeDisplayer;
    private TMP_Text tmpTextNightNumber;
    private TMP_Text tmpTextPowerDisplayer;
    private TMP_Text tmpTextTimeDisplayer;

    private bool audioPlayed;

    SaveGameState saveGameState;
    SaveManager saveManager;

    void Start()
    {
        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        textNightNumber = nightNumberDisplayer.GetComponent<Text>();
        textPowerDisplayer = powerDisplayer.GetComponent<Text>();
        textTimeDisplayer = timeDisplayer.GetComponent<Text>();
        tmpTextNightNumber = nightNumberDisplayer.GetComponent<TextMeshProUGUI>();
        tmpTextPowerDisplayer = powerDisplayer.GetComponent<TextMeshProUGUI>();
        tmpTextTimeDisplayer = timeDisplayer.GetComponent<TextMeshProUGUI>();

        officeRect = officeContainer.GetComponent<RectTransform>();

        nightNumber = SaveManager.LoadNightNumber();

        if (textNightNumber != null)
        {
            textNightNumber.text = (nightNumber + 1).ToString();
        }

        if (tmpTextNightNumber != null)
        {
            tmpTextNightNumber.text = (nightNumber + 1).ToString();
        }
    }

    public void DebugTwelveAMButton()
    {
        timeRemaining = 536;
    }
    public void DebugOneAMButton()
    {
        timeRemaining = 446;
    }
    public void DebugTwoAMButton()
    {
        timeRemaining = 355;
    }
    public void DebugThreeAMButton()
    {
        timeRemaining = 268;
    }
    public void DebugFourAMButton()
    {
        timeRemaining = 179;
    }
    public void DebugFiveAMButton()
    {
        timeRemaining = 90;
    }
    public void DebugSixAMButton()
    {
        timeRemaining = 1;
    }

    void Update() //(max) wow this function proves how much you hate else ifs
    {
        //---------------------------------------TIME-------------------------------------//
        timeRemaining -= Time.deltaTime;

        switch((int)timeRemaining)
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
            PowerDrain -= Time.deltaTime;

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
            PowerDrain -= Time.deltaTime;

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
            PowerDrain -= Time.deltaTime;

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
            PowerDrain -= Time.deltaTime;

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
            PowerDrain -= Time.deltaTime;

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
            // Save the current X position in the office
            officePositionX = officeRect.anchoredPosition.x;

            SceneManager.LoadScene("PowerOut");
        }
    }
}
