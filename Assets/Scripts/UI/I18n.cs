using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class I18n
{
    public static Dictionary<string, string> Texts { get; private set; }

    [Serializable]
    public class TranslationDictionary
    {
        public List<TranslationItem> items;
    }

    [Serializable]
    public class TranslationItem
    {
        public string key;
        public string value;
    }

    static I18n()
    {
        LoadLanguage();
    }

    public static void ReloadLanguage()
    {
        LoadLanguage();
    }

    private static void LoadLanguage()
    {
        if (Texts == null)
            Texts = new Dictionary<string, string>();

        Texts.Clear();

        string lang;

        string languagePlayerPrefs = PlayerPrefs.GetString("Language", "None");

        if (languagePlayerPrefs == "English")
        {
            lang = "en";
        }
        else if (languagePlayerPrefs == "French")
        {
            lang = "fr";
        }
        else if (languagePlayerPrefs == "Spanish")
        {
            lang = "es";
        }
        else if (languagePlayerPrefs == "Italian")
        {
            lang = "it";
        }
        else
        {
            lang = Get2LetterISOCodeFromSystemLanguage().ToLower();
        }

        string filePath = "I18n/" + lang;

        TextAsset jsonFile = Resources.Load<TextAsset>(filePath);
        string allTexts = jsonFile.text;
        TranslationDictionary translations = JsonUtility.FromJson<TranslationDictionary>(allTexts);

        foreach (TranslationItem item in translations.items)
        {
            Texts.Add(item.key, item.value);
        }
    }

    public static string GetLanguage()
    {
        string languagePlayerPrefs = PlayerPrefs.GetString("Language", "None");

        if (languagePlayerPrefs == "English")
        {
            return "en";
        }
        else if (languagePlayerPrefs == "French")
        {
            return "fr";
        }
        else if (languagePlayerPrefs == "Spanish")
        {
            return "es";
        }
        else if (languagePlayerPrefs == "Italian")
        {
            return "it";
        }
        else
        {
            return Get2LetterISOCodeFromSystemLanguage().ToLower();
        }
    }

    public static string Get2LetterISOCodeFromSystemLanguage()
    {
        SystemLanguage lang = Application.systemLanguage;
        string res = "EN";
        switch (lang)
        {
            case SystemLanguage.English: res = "EN"; break;
            case SystemLanguage.French: res = "FR"; break;
            case SystemLanguage.Spanish: res = "ES"; break;
            case SystemLanguage.Italian: res = "IT"; break;
        }
        return res;
    }
}