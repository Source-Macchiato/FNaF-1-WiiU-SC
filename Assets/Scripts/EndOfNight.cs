using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfNight : MonoBehaviour {

    public GameObject Children;
    public float NightNumber = 0;

    void Start () {
        StartCoroutine(InitCoroutine());

        NightNumber = PlayerPrefs.GetFloat("NightNumber", NightNumber);

        //SceneManager.UnloadSceneAsync("MainMenu");
        //SceneManager.UnloadSceneAsync("GameOver");
        //SceneManager.UnloadSceneAsync("NextNight");
        //SceneManager.UnloadSceneAsync("Controlls");
        //SceneManager.UnloadSceneAsync("Office");
        //SceneManager.UnloadSceneAsync("Advertisement");
        //SceneManager.UnloadSceneAsync("PowerOut");
        //SceneManager.UnloadSceneAsync("TheEnd");
        //SceneManager.UnloadSceneAsync("CostumNight");
    }

    IEnumerator InitCoroutine() {
        yield return new WaitForSeconds(6);

        Children.SetActive(true);

        yield return new WaitForSeconds(5);

        if (NightNumber < 5) {
            SceneManager.LoadScene("Office");
        } else if (NightNumber > 5) {
            SceneManager.LoadScene("TheEnd");
        }
    }
}
