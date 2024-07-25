using System;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCredits : MonoBehaviour
{
    public GameObject creditsContent;
    public TextAsset creditsFile;

    // Markdown prefabs
    public GameObject h1Prefab;
    public GameObject h2Prefab;
    public GameObject h3Prefab;
    public GameObject textPrefab;

    private const float lineSpacing = 20f; // 5 pixels space for empty lines

    void Start()
    {
        if (creditsFile != null)
        {
            ParseAndDisplayCredits(creditsFile.text);
        }
        else
        {
            Debug.LogError("Credits file not found");
        }
    }

    void ParseAndDisplayCredits(string markdown)
    {
        string[] lines = markdown.Split('\n');
        RectTransform lastElement = null;
        float currentPosY = 0f;

        foreach (string line in lines)
        {
            GameObject newTextObject = null;
            RectTransform rectTransform = null;

            if (line.StartsWith("# "))
            {
                newTextObject = Instantiate(h1Prefab, creditsContent.transform);
                newTextObject.GetComponent<Text>().text = line.Substring(2).Trim();
            }
            else if (line.StartsWith("## "))
            {
                newTextObject = Instantiate(h2Prefab, creditsContent.transform);
                newTextObject.GetComponent<Text>().text = line.Substring(3).Trim();
            }
            else if (line.StartsWith("### "))
            {
                newTextObject = Instantiate(h3Prefab, creditsContent.transform);
                newTextObject.GetComponent<Text>().text = line.Substring(4).Trim();
            }
            else if (!IsNullOrWhiteSpace(line))
            {
                newTextObject = Instantiate(textPrefab, creditsContent.transform);
                newTextObject.GetComponent<Text>().text = line.Trim();
            }

            if (newTextObject != null)
            {
                rectTransform = newTextObject.GetComponent<RectTransform>();

                if (lastElement != null)
                {
                    // Adjust position Y with an additional line spacing if previous line was empty
                    currentPosY -= lastElement.rect.height + (IsNullOrWhiteSpace(lines[Array.IndexOf(lines, line) - 1]) ? lineSpacing : 0);
                    rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, currentPosY);
                }
                else
                {
                    // First element, just place it at the starting position
                    rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, currentPosY);
                }

                // Update the last element reference
                lastElement = rectTransform;
            }
        }

        if (lastElement != null)
        {
            RectTransform contentRectTransform = creditsContent.GetComponent<RectTransform>();
            float newHeight = Mathf.Abs(currentPosY) + lastElement.rect.height;
            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, newHeight);
        }
    }

    bool IsNullOrWhiteSpace(string value)
    {
        return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
    }
}
