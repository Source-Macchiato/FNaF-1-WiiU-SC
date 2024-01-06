using UnityEngine;

public class RenderMovie : MonoBehaviour
{
    public MovieTexture movTexture;

    void Start()
    {
        GetComponent<MeshRenderer>().material.mainTexture = movTexture;

        movTexture.Play();
    }
}