using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public Text subtitleText;
    public List<string> subtitleIdentifiers; // Utilisez des identifiants au lieu de texte
    public List<float> displayDurations;

    public float startDelay = 0.0f;
    private float displayStartTime;
    private int currentIndex = 0;
    private bool isDelayOver = false;

    void Start()
    {
        if (subtitleIdentifiers.Count != displayDurations.Count)
        {
            Debug.LogError("Subtitle identifiers and display durations lists must have the same size!");
            return;
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
        // Utilisez l'identifiant pour récupérer le texte traduit
        string translatedText = GetTranslatedText(subtitleIdentifiers[currentIndex]);
        subtitleText.text = translatedText;
        displayStartTime = Time.timeSinceLevelLoad;
    }

    // Fonction pour récupérer le texte traduit à partir de l'identifiant
    string GetTranslatedText(string identifier)
    {
        // Vérifiez si l'identifiant existe dans le dictionnaire de traduction
        if (I18n.Texts.ContainsKey(identifier))
        {
            return I18n.Texts[identifier];
        }
        else
        {
            // Si l'identifiant n'est pas trouvé, retournez l'identifiant lui-même
            Debug.LogWarning("Translation not found for identifier: " + identifier);
            return identifier;
        }
    }
}
