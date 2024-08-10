using UnityEngine;
using UnityEngine.UI;

public class MarkdownParser : MonoBehaviour
{
    // Markdown prefabs
    public GameObject blankPrefab;
    public GameObject h1Prefab;
    public GameObject h2Prefab;
    public GameObject h3Prefab;
    public GameObject textPrefab;

    public void ParseAndDisplayMarkdown(GameObject container, string markdown)
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
                newTextObject = Instantiate(h1Prefab, container.transform);
                newTextObject.GetComponent<Text>().text = line.Substring(2).Trim();
            }
            else if (line.StartsWith("## "))
            {
                newTextObject = Instantiate(h2Prefab, container.transform);
                newTextObject.GetComponent<Text>().text = line.Substring(3).Trim();
            }
            else if (line.StartsWith("### "))
            {
                newTextObject = Instantiate(h3Prefab, container.transform);
                newTextObject.GetComponent<Text>().text = line.Substring(4).Trim();
            }
            else if (!IsNullOrWhiteSpace(line))
            {
                newTextObject = Instantiate(textPrefab, container.transform);
                newTextObject.GetComponent<Text>().text = line.Trim();
            }
            else
            {
                // If line is empty, instantiate blankPrefab to add a blank space
                newTextObject = Instantiate(blankPrefab, container.transform);
                rectTransform = newTextObject.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 20f);
            }

            if (newTextObject != null)
            {
                rectTransform = newTextObject.GetComponent<RectTransform>();

                if (lastElement != null)
                {
                    // Adjust position Y based on the height of the previous element
                    currentPosY -= lastElement.rect.height;
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
    }

    bool IsNullOrWhiteSpace(string value)
    {
        return string.IsNullOrEmpty(value) || value.Trim().Length == 0;
    }
}