using System.Collections;
using System.Collections.Generic;
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
		yield return new WaitForSeconds(21);
		SceneManager.LoadScene("MainMenu");

	}
}
