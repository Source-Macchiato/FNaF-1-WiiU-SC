using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Canvas canvaUI;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progression;

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operationLoadLevel = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operationLoadLevel.isDone)
        {
            float progress = Mathf.Clamp01(operationLoadLevel.progress / 0.9f);

            slider.value = progress;
            progression.text = (progress * 100).ToString() + "%";

            yield return null;
        }
    }
}