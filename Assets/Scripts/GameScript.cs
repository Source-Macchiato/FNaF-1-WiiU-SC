using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    private float NightNumber;
    public float Time = 535; //fixed i hope
    public float Usage = 1;

    public Text NightNumberDisplayer;
    public GameObject TimeShower;

    public float PowerUsage = 1;
    public float PowerDrain = 1;
    public float PowerLeft = 100;
    public GameObject PowerShower;
    public GameObject Bar1, Bar2, Bar3, Bar4, Bar5;

    public AudioSource Call1;
    public AudioSource Call2;
    public AudioSource Call3;
    public AudioSource Call4;
    public AudioSource Call5;


	void Start ()
    {
        NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);
        NightNumberDisplayer.text = NightNumber.ToString();

        //--------------------------------------CallAndNight----------------------//
        if (NightNumber == 1)
        {
            Call1.Play();
        }

        if (NightNumber == 2)
        {
            Call2.Play();
        }

        if (NightNumber == 3)
        {
            Call3.Play();
        }

        if (NightNumber == 4)
        {
            Call4.Play();
        }

        if (NightNumber == 5)
        {
            Call5.Play();
        }
        //-------------------------------CallAndNight-----------------------------//
    }

    void Update () //(max) wow this function proves how much you hate else ifs
    {
        //---------------------------------------TIME-------------------------------------//
        Time -= UnityEngine.Time.deltaTime;

        switch((int)Time)
        {
            case 535:
                TimeShower.GetComponent<Text>().text = "12 AM";
            break;
                
            case 445:
                TimeShower.GetComponent<Text>().text = "1 AM";
            break;

            case 354:
                TimeShower.GetComponent<Text>().text = "2 AM";
            break;
            
            case 267:
                TimeShower.GetComponent<Text>().text = "3 AM";
            break;

            case 178:
                TimeShower.GetComponent<Text>().text = "4 AM";
            break;

            case 89:
                TimeShower.GetComponent<Text>().text = "5 AM";
            break;
            
            case 0:
            TimeShower.GetComponent<Text>().text = "6 AM";
            PlayerPrefs.SetFloat("NightNumber", NightNumber + 1);
            PlayerPrefs.Save();

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
                PowerShower.GetComponent<Text>().text = PowerLeft.ToString();
                PowerDrain = 8;
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
                PowerShower.GetComponent<Text>().text = PowerLeft.ToString();
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
                PowerShower.GetComponent<Text>().text = PowerLeft.ToString();
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
                PowerShower.GetComponent<Text>().text = PowerLeft.ToString();
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
                PowerShower.GetComponent<Text>().text = PowerLeft.ToString();
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
