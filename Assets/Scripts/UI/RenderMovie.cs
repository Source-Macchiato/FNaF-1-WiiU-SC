using UnityEngine;

public class RenderMovie : MonoBehaviour
{
    public MovieTexture videoMovieTexture;
    public AudioSource videoAudioSource;

    void Start()
    {
        GetComponent<MeshRenderer>().material.mainTexture = videoMovieTexture;
        GetComponent<AudioSource>().clip = videoMovieTexture.audioClip;

        videoMovieTexture.Play();
        videoAudioSource.Play();
    }
}