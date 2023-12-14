using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextNight : MonoBehaviour {

    public float WichNight;
    public GameObject WichNightShower;

    void Start () {
        WichNight = PlayerPrefs.GetFloat("WichNight", WichNight);

        StartCoroutine(InitCoroutine());
        
        // Unload unnecessary scenes (you may want to adjust this based on your actual scene names)
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.UnloadSceneAsync("GameOver");
        SceneManager.UnloadSceneAsync("6AM");
        SceneManager.UnloadSceneAsync("Controlls");
        SceneManager.UnloadSceneAsync("Office");
        SceneManager.UnloadSceneAsync("Advertisement");
        SceneManager.UnloadSceneAsync("PowerOut");
        SceneManager.UnloadSceneAsync("TheEnd");
        SceneManager.UnloadSceneAsync("CostumNight");
    }
    
    void Update () {
        Resources.UnloadUnusedAssets();
        WichNightShower.GetComponent<Text>().text = WichNight.ToString();
    }

    IEnumerator InitCoroutine() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Office");
    }
}
