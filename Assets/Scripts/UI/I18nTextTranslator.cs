using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class I18nTextTranslator : MonoBehaviour
{
    public string textId;
    private Text textComponent;
    private TMP_Text tmpTextComponent;
    private string currentLanguage;

    private ArabicFixerTMPRO arabicFixer;

    void Start()
    {
        textComponent = GetComponent<Text>();
        tmpTextComponent = GetComponent<TextMeshProUGUI>();
        arabicFixer = GetComponent<ArabicFixerTMPRO>();

        if (textComponent == null && tmpTextComponent == null)
        {
            return;
        }

        if (I18n.Texts == null)
        {
            return;
        }

        // Get language
        currentLanguage = I18n.GetLanguage();

        // Load text
        UpdateText();
    }

    void Update()
    {
        // Check if current language is not th I18n language
        if (currentLanguage != I18n.GetLanguage())
        {
            // Reload text
            UpdateText();

            // Set I18n language as current language
            currentLanguage = I18n.GetLanguage();
        }
    }

    public void UpdateText()
    {
        if (string.IsNullOrEmpty(textId))
        {
            return;
        }

        string translatedText = GetTranslatedText();

        if (textComponent != null)
        {
            textComponent.text = translatedText;
        }
        
        if (tmpTextComponent != null)
        {
            tmpTextComponent.text = translatedText;
        }
    }

    string GetTranslatedText()
    {
        string translatedText;

        if (I18n.Texts.TryGetValue(textId, out translatedText))
        {
            return translatedText;
        }
        else
        {
            return textId;
        }
    }
}