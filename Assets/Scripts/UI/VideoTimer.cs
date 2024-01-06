using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoTimer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            Invoke("ChangeScene", (float)videoPlayer.clip.length);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
