using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfNight : MonoBehaviour {

    public GameObject Children;
    public float nightNumber;
    public Sprite[] achievementsIcons;

    private float freddyDifficulty;
    private float bonnieDifficulty;
    private float chicaDifficulty;
    private float foxyDifficulty;

    SaveGameState saveGameState;
    SaveManager saveManager;

    void Start()
    {
        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        nightNumber = SaveManager.LoadNightNumber();

        // Get difficulty
        freddyDifficulty = PlayerPrefs.GetFloat("FreddyDifficulty", 0);
        bonnieDifficulty = PlayerPrefs.GetFloat("BonnieDifficulty", 0);
        chicaDifficulty = PlayerPrefs.GetFloat("ChicaDifficulty", 0);
        foxyDifficulty = PlayerPrefs.GetFloat("FoxyDifficulty", 0);

        // Unlock achievements
        if (nightNumber == 1)
        {
            MedalsManager.medalsManager.ShowAchievement("One Night at Freddy's", "Survived the 1st night.", achievementsIcons[0]);
        }
        else if (nightNumber == 2)
        {
            MedalsManager.medalsManager.ShowAchievement("Two Nights at Freddy's", "Survived the 2nd night.", achievementsIcons[1]);
        }
        else if (nightNumber == 3)
        {
            MedalsManager.medalsManager.ShowAchievement("Three Nights at Freddy's", "Survived the 3rd night.", achievementsIcons[2]);
        }
        else if (nightNumber == 4)
        {
            MedalsManager.medalsManager.ShowAchievement("Four Nights at Freddy's", "Survived the 4th night.", achievementsIcons[3]);
        }
        else if (nightNumber == 5)
        {
            MedalsManager.medalsManager.ShowAchievement("Five Nights at Freddy's", "Survived the 5th night.", achievementsIcons[4]);
        }
        else if (nightNumber == 6)
        {
            MedalsManager.medalsManager.ShowAchievement("Overtime", "Survived the 6th night.", achievementsIcons[5]);
        }
        else if (nightNumber == 7)
        {
            if (freddyDifficulty == 20 && bonnieDifficulty == 20 && chicaDifficulty == 20 && foxyDifficulty == 20)
            {
                MedalsManager.medalsManager.ShowAchievement("No Tampering", "Completed the custom night on hardest difficulty.", achievementsIcons[6]);
            }
        }

        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        yield return new WaitForSeconds(6);

        Children.SetActive(true);

        yield return new WaitForSeconds(5);

        if (nightNumber >= 1 && nightNumber <= 4)
        {
            SceneManager.LoadScene("NextNight");
        }
        else if (nightNumber == 5)
        {
            // When night 5 is finished enable the first star
            if (SaveManager.LoadStarsId() == 0)
            {
                saveManager.SaveStars(1);
                bool saveResult = saveGameState.DoSave();
            }

            SceneManager.LoadScene("TheEnd");
        }
        else if (nightNumber == 6)
        {
            // When night 6 is finished enable the second star
            if (SaveManager.LoadStarsId() <= 1)
            {
                saveManager.SaveStars(2);
                bool saveResult = saveGameState.DoSave();
            }

            SceneManager.LoadScene("TheEnd");
        }
        else if (nightNumber == 7)
        {
            if (freddyDifficulty == 20 && bonnieDifficulty == 20 && chicaDifficulty == 20 && foxyDifficulty == 20)
            {
                // When custom night is finished enable the third star
                if (SaveManager.LoadStarsId() <= 2)
                {
                    saveManager.SaveStars(3);
                    bool saveResult = saveGameState.DoSave();
                }
            }

            SceneManager.LoadScene("TheEnd");
        }
    }
}