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

        if (nightNumber >= 0 && nightNumber <= 4) // Current night is between 1 and 4
        {
            SceneManager.LoadScene("NextNight");
        }
        else if (nightNumber == 5) // Current night is 5
        {
            // When night 5 is finished enable the first star
            saveManager.SaveStars(1);
            bool saveResult = saveGameState.DoSave();

            SceneManager.LoadScene("TheEnd");
        }
    }
}