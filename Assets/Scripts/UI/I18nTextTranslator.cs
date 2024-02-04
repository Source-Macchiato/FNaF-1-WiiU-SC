using UnityEngine;
using UnityEngine.UI;

public class I18nTextTranslator : MonoBehaviour
{
    public string textId;
    private Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();

        if (textComponent == null)
        {
            return;
        }

        if (I18n.Texts == null)
        {
            return;
        }

        UpdateText();
    }

    void UpdateText()
    {
        if (string.IsNullOrEmpty(textId))
        {
            return;
        }

        string translatedText = GetTranslatedText();
        textComponent.text = translatedText;
    }

    string GetTranslatedText()
    {
        string language = I18n.GetLanguage();
        string translatedText;

        if (I18n.Texts.TryGetValue(textId + "_" + language, out translatedText))
        {
            return translatedText;
        }
        else if (I18n.Texts.TryGetValue(textId, out translatedText))
        {
            return translatedText;
        }
        else
        {
            return textId;
        }
    }
}