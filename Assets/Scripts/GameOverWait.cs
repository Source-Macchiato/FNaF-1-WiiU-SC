using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWait : MonoBehaviour
{
	void Start()
	{
		StartCoroutine(InitCoroutine());	
	}

	IEnumerator InitCoroutine()
	{
		//hi, it's shiro-sata. if you read this, it mean you're gay
		// hi, it's alyx and I want to say that shiro is gay (1 year later it's still true)
		yield return new WaitForSeconds(21);
		SceneManager.LoadScene("MainMenu");
	}
}
