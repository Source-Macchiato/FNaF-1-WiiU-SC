using UnityEngine;
using UnityEngine.SceneManagement;

public class RenderMovie : MonoBehaviour
{
    public MovieTexture movTexture;
    public string nextSceneName;
    bool hasStarted = false;

    void Start()
    {
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
            SceneManager.LoadScene(nextSceneName);
        }
    }
}