using System.Collections;
using UnityEngine;

public class Warning : MonoBehaviour
{
	public GameObject loadingScreen;
	private LevelLoader levelLoader;

	void Start()
	{
		loadingScreen.SetActive(false);
		levelLoader = FindObjectOfType<LevelLoader>();

        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        yield return new WaitForSeconds(4);
        loadingScreen.SetActive(true);
        levelLoader.LoadLevel("MainMenu");
    }
}
