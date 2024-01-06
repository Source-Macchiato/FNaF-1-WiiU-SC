using UnityEngine;
using UnityEngine.UI;

public class RenderMovie : MonoBehaviour
{
    public MovieTexture videoMovieTexture;
    public AudioSource videoAudioSource;

    void Start()
    {
        RawImage rawImage = GetComponent<RawImage>();

        rawImage.texture = videoMovieTexture;

        if (videoAudioSource != null)
        {
            videoAudioSource.clip = videoMovieTexture.audioClip;
            videoAudioSource.Play();
        }

        videoMovieTexture.Play();
    }
}