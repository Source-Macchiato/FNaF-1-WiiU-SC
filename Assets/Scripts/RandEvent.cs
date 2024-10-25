using UnityEngine;

public class RandEvent : MonoBehaviour
{
    public GameObject ItsMeObj;
    public AudioSource ItsMeSound;
    public AudioSource circusSound;
    public float countdown; 
    public bool secondConditionMet; 
    public bool ItsMeConditionMet;
    public const float minCountdown = 10f;
    public const float maxCountdown = 130f;
    public const int ItsMeRandEvent = 70;
    public const int SecondRandEvent = 15;
    public bool ItsMePlayed = false;
    public bool CircusPlayed = false;

    void Start()
    {
        ResetCountdown();
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            //play circus
            if (secondConditionMet)
            {
                circus(); 
                ResetCountdown(); 
            }
            else
            {
                ResetCountdown(); 
            }

            //play It's me
            if (ItsMeConditionMet)
            {
                ItsMe();
                ResetCountdown();
            }
            else
            {
                ResetCountdown();
            }
        }
    }
    void ItsMe()
    {
        if(!ItsMePlayed)
        {
            ItsMeObj.SetActive(true);
            ItsMeSound.Play();
            ItsMePlayed = true;
        }
        
    }
    void circus()
    {
        if(!CircusPlayed)
        {
            circusSound.Play();
            CircusPlayed = true;
        }
        
    }
    void ResetCountdown()
    {
        countdown = Random.Range(minCountdown, maxCountdown);
        ItsMeConditionMet = Random.Range(1, ItsMeRandEvent + 1) == 1;
        secondConditionMet = Random.Range(1, SecondRandEvent + 1) == 1;
    }
}
