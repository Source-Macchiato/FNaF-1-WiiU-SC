using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public GameObject progression;

    private Text textComponent;
    private TMP_Text tmpTextComponent;

    void Start()
    {
        textComponent = progression.GetComponent<Text>();
        tmpTextComponent = progression.GetComponent<TextMeshProUGUI>();
    }

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

            if (textComponent != null)
            {
                textComponent.text = (progress * 100).ToString("F2") + "%";
            }
            
            if (tmpTextComponent != null)
            {
                tmpTextComponent.text = (progress * 100).ToString("F2") + "%";
            }

            yield return null;
        }
    }
}