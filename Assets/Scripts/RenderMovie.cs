using UnityEngine;

public class RenderMovie : MonoBehaviour
{
    public MovieTexture movTexture;
    public AudioSource audio;

    void Start()
    {
        GetComponent<MeshRenderer>().material.mainTexture = movTexture;
        GetComponent<AudioSource>().clip = movTexture.audioClip;

        movTexture.Play();
        audio.Play();
    }
}