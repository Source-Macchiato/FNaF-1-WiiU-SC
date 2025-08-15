using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RenderMovie : MonoBehaviour
{
    public MovieTexture movTexture;
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        movTexture.Play();
        audioSource.Play();

        yield return new WaitForSeconds(8f);

        SceneManager.LoadSceneAsync("Warning");
    }
}