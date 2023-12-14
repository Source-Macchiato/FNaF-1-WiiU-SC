using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    // Use this for initialization
    void Start () {
        StartCoroutine(InitCoroutine());
        
        // Unload unnecessary scenes (you may want to adjust this based on your actual scene names)
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.UnloadSceneAsync("6AM");
        SceneManager.UnloadSceneAsync("NextNight");
        SceneManager.UnloadSceneAsync("Controlls");
        SceneManager.UnloadSceneAsync("Office");
        SceneManager.UnloadSceneAsync("Advertisement");
        SceneManager.UnloadSceneAsync("PowerOut");
        SceneManager.UnloadSceneAsync("TheEnd");
        SceneManager.UnloadSceneAsync("CostumNight");
    }
    
    IEnumerator InitCoroutine() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}
