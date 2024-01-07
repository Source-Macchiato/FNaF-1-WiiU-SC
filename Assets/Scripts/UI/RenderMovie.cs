using UnityEngine;
using UnityEngine.SceneManagement;

public class RenderMovie : MonoBehaviour
{
    public MovieTexture movTexture;
    bool hasStarted = false;
    public GameObject loadingScreenPanel;

    void Start()
    {
        loadingScreenPanel.SetActive(false);
        movTexture.Play();
    }

    void Update()
    {
        if (!hasStarted && movTexture.isPlaying)
        {
            hasStarted = true;
        }

        if (hasStarted && !movTexture.isPlaying)
        {
            loadingScreenPanel.SetActive(true);
            GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevel("MainMenu");
        }
    }
}