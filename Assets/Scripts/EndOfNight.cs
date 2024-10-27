using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfNight : MonoBehaviour {

    public GameObject Children;
    public float nightNumber;

    SaveGameState saveGameState;
    SaveManager saveManager;

    void Start()
    {
        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        nightNumber = SaveManager.LoadNightNumber();

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
            // When custom night is finished enable the third star
            if (SaveManager.LoadStarsId() <= 2)
            {
                saveManager.SaveStars(3);
                bool saveResult = saveGameState.DoSave();
            }

            SceneManager.LoadScene("TheEnd");
        }
    }
}