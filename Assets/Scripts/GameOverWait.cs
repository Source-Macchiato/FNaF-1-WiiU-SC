using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWait : MonoBehaviour
{
	public Sprite achievementIcon;

	void Start()
	{
        MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.NOHIDING);

        StartCoroutine(InitCoroutine());	
	}

	IEnumerator InitCoroutine()
	{
		//hi, it's shiro-sata. if you read this, it mean you're gay
		// hi, it's alyx and I want to say that shiro is gay (1 year later it's still true)
		// for real my bbg
		yield return new WaitForSeconds(21);
		SceneManager.LoadScene("MainMenu");
	}
}
