using UnityEngine;

public class DisplayCredits : MonoBehaviour
{
    public GameObject creditsContainer;
    public TextAsset creditsFile;

    private const float lineSpacing = 20f; // Spaces height

    MarkdownParser markdownParser;

    void Start()
    {
        markdownParser = FindObjectOfType<MarkdownParser>();

        if (creditsFile != null)
        {
            markdownParser.ParseAndDisplayMarkdown(creditsContainer, creditsFile.text);
        }
        else
        {
            Debug.LogError("Credits file not found");
        }
    }
}
