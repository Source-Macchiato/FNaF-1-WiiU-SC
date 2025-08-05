using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfNight : MonoBehaviour {

    public GameObject Children;
    public float nightNumber;
    public Sprite[] achievementsIcons;

    SaveGameState saveGameState;
    SaveManager saveManager;

    void Start()
    {
        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        nightNumber = SaveManager.saveData.game.nightNumber;

        // Unlock achievements
        if (nightNumber == 1)
        {
            MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.ONENIGHTATFREDDYS);
        }
        else if (nightNumber == 2)
        {
            MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.TWONIGHTSATFREDDYS);
        }
        else if (nightNumber == 3)
        {
            MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.THREENIGHTSATFREDDYS);
        }
        else if (nightNumber == 4)
        {
            MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.FOURNIGHTSATFREDDYS);
        }
        else if (nightNumber == 5)
        {
            MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.FIVENIGHTSATFREDDYS);
        }
        else if (nightNumber == 6)
        {
            MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.OVERTIME);
        }
        else if (nightNumber == 7)
        {
            if (Movement.freddyDifficulty == 20 && Movement.bonnieDifficulty == 20 && Movement.chicaDifficulty == 20 && Movement.foxyDifficulty == 20)
            {
                MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.NOTAMPERING);
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
            SceneManager.LoadSceneAsync("NextNight");
        }
        else if (nightNumber == 5)
        {
            // When night 5 is finished enable the first star
            if (SaveManager.saveData.game.starsId == 0)
            {
                SaveManager.saveData.game.starsId = 1;
                SaveManager.Save();
            }

            SceneManager.LoadSceneAsync("TheEnd");
        }
        else if (nightNumber == 6)
        {
            // When night 6 is finished enable the second star
            if (SaveManager.saveData.game.starsId <= 1)
            {
                SaveManager.saveData.game.starsId = 2;
                SaveManager.Save();
            }

            SceneManager.LoadSceneAsync("TheEnd");
        }
        else if (nightNumber == 7)
        {
            if (Movement.freddyDifficulty == 20 && Movement.bonnieDifficulty == 20 && Movement.chicaDifficulty == 20 && Movement.foxyDifficulty == 20)
            {
                // When custom night is finished enable the third star
                if (SaveManager.saveData.game.starsId <= 2)
                {
                    SaveManager.saveData.game.starsId = 3;
                    SaveManager.Save();
                }
            }

            SceneManager.LoadSceneAsync("TheEnd");
        }
    }
}