using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public Text subtitleText;
    private List<string> subtitleIdentifiers;
    public List<float> displayDurations;

    public float startDelay = 0.0f;
    private float displayStartTime;
    private int currentIndex = 0;
    private bool isDelayOver = false;

    void Start()
    {
        subtitleIdentifiers = new List<string>();
        displayDurations = new List<float>();

        TextAsset subtitleFile = Resources.Load<TextAsset>("Data/night1");

        string[] lines = subtitleFile.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split(new char[] { ';' });
            if (parts.Length == 2)
            {
                subtitleIdentifiers.Add(parts[0]);
                displayDurations.Add(float.Parse(parts[1]));
            }
        }

        displayStartTime = Time.timeSinceLevelLoad + startDelay;
    }

    void Update()
    {
        if (!isDelayOver)
        {
            if (Time.timeSinceLevelLoad >= displayStartTime)
            {
                isDelayOver = true;
                DisplaySubtitle();
            }
            return;
        }

        if (currentIndex >= subtitleIdentifiers.Count)
        {
            return;
        }

        if (Time.timeSinceLevelLoad >= displayStartTime + displayDurations[currentIndex])
        {
            currentIndex++;

            if (currentIndex < subtitleIdentifiers.Count)
            {
                DisplaySubtitle();
            }
            else
            {
                subtitleText.text = "";
                subtitleText.gameObject.SetActive(false);
            }
        }
    }

    void DisplaySubtitle()
    {
        string translatedText = GetTranslatedText(subtitleIdentifiers[currentIndex]);
        subtitleText.text = translatedText;
        displayStartTime = Time.timeSinceLevelLoad; // Utilisation directe de Time.timeSinceLevelLoad sans addition
    }

    string GetTranslatedText(string identifier)
    {
        if (I18n.Texts.ContainsKey(identifier))
        {
            return I18n.Texts[identifier];
        }
        else
        {
            Debug.LogWarning("Translation not found for identifier: " + identifier);
            return identifier;
        }
    }
}