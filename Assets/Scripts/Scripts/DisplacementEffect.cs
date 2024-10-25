using UnityEngine;

public class DisplacementEffect : MonoBehaviour
{
    public Shader displacementShader;
    private Material displacementMaterial;
    public Texture2D displacementMap;
    [Range(-1, 1)] public float intensity = 0.5f;

    void Start()
    {
        displacementMaterial = new Material(displacementShader);
        displacementMaterial.SetTexture("_DispTex", displacementMap);
    }

    void Update()
    {
        displacementMaterial.SetFloat("_Intensity", intensity);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, displacementMaterial);
    }
}
