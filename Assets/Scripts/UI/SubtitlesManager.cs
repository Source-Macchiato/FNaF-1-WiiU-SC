using RTLTMPro;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesManager : MonoBehaviour
{
    public GameObject[] subtitlesContainers;

    private List<string> subtitleIdentifiers;
    private List<float> displayDurations;

    private float displayStartTime;
    private int currentIndex = 0;
    private bool isDelayOver = false;

    private float NightNumber;

    private TextAsset subtitleFile;

    [Header("Fonts")]
    public TMP_FontAsset mainFont;
    public TMP_FontAsset arabicFont;

    void Start()
    {
        subtitleIdentifiers = new List<string>();
        displayDurations = new List<float>();

        NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

        if (NightNumber == 1 || NightNumber == 2 || NightNumber == 3 || NightNumber == 4 || NightNumber == 5)
        {
            subtitleFile = Resources.Load<TextAsset>("Data/night" + NightNumber);

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

            displayStartTime = Time.timeSinceLevelLoad;
        }

        if (NightNumber == 5)
        {
            foreach (GameObject subtitleContainer in subtitlesContainers)
            {
                if (subtitleContainer.activeSelf)
                {
                    subtitleContainer.transform.Rotate(0f, 180f, 0f);
                }
            }
        }

        displayStartTime = Time.timeSinceLevelLoad;
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
                foreach (GameObject subtitleContainer in subtitlesContainers)
                {
                    Text textComponent = subtitleContainer.GetComponent<Text>();
                    RTLTextMeshPro tmpTextComponent = subtitleContainer.GetComponent<RTLTextMeshPro>();

                    if (textComponent != null)
                    {
                        if (subtitleContainer.activeSelf)
                        {
                            textComponent.text = "";
                            subtitleContainer.SetActive(false);
                        }
                    }

                    if (tmpTextComponent != null)
                    {
                        if (subtitleContainer.activeSelf)
                        {
                            tmpTextComponent.text = "";
                            subtitleContainer.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    void DisplaySubtitle()
    {
        string translatedText = GetTranslatedText(subtitleIdentifiers[currentIndex]);

        foreach (GameObject subtitleContainer in subtitlesContainers)
        {
            Text textComponent = subtitleContainer.GetComponent<Text>();
            RTLTextMeshPro tmpTextComponent = subtitleContainer.GetComponent<RTLTextMeshPro>();

            if (textComponent != null)
            {
                if (subtitleContainer.activeSelf)
                {
                    textComponent.text = translatedText;
                }
            }

            if (tmpTextComponent != null)
            {
                if (subtitleContainer.activeSelf)
                {
                    tmpTextComponent.text = translatedText;

                    if (I18n.GetLanguage() == "ar")
                    {
                        if (arabicFont != null)
                        {
                            tmpTextComponent.font = arabicFont;
                        }
                    }
                    else
                    {
                        if (mainFont != null)
                        {
                            tmpTextComponent.font = mainFont;
                        }
                    }
                }
            }
        }

        displayStartTime = Time.timeSinceLevelLoad;
    }

    string GetTranslatedText(string identifier)
    {
        if (I18n.Texts.ContainsKey(identifier))
        {
            return I18n.Texts[identifier];
        }
        else
        {
            return identifier;
        }
    }
}