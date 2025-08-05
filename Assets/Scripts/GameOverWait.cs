using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWait : MonoBehaviour
{
    private bool skipRequested = false;

    void Start()
	{
        if (MedalsManager.medalsManager != null)
        {
            MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.NOHIDING);
        }

        StartCoroutine(InitCoroutine());	
	}

    void Update()
    {
        if (Input.anyKeyDown && !skipRequested)
        {
            skipRequested = true;
        }
    }

    IEnumerator InitCoroutine()
	{
        //hi, it's shiro-sata. if you read this, it mean you're gay
        // hi, it's alyx and I want to say that shiro is gay (1 year later it's still true)
        // for real my bbg

        yield return new WaitForSeconds(10f);

        // Wait 11 seconds or until skip is requested
        float elapsedTime = 0f;
        while (elapsedTime < 11f && !skipRequested)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

		SceneManager.LoadSceneAsync("MainMenu");
	}
}
