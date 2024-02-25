using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverWait : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(InitCoroutine());
		
	}
	
	// Update is called once per frame

	IEnumerator InitCoroutine()
	{
		//hi, it's shiro-sata. if you read this, it mean you're gay
		// hi, it's alyx and I want to say that shiro is gay
		yield return new WaitForSeconds(21);
		SceneManager.LoadScene("MainMenu");

	}
}
