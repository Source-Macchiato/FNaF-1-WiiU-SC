using System;
using System.Collections.Generic;
using UnityEngine;

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

    public static void LoadLanguage()
    {
        if (Texts == null)
        {
            Texts = new Dictionary<string, string>();
        }
            

        Texts.Clear();

        string lang = GetLanguage();

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
        string loadedLanguage = SaveManager.saveData.settings.language;

        if (loadedLanguage != string.Empty)
        {
            return loadedLanguage;
        }
        else
        {
            return Get2LetterISOCodeFromSystemLanguage();
        }
    }

    public static string Get2LetterISOCodeFromSystemLanguage()
    {
        SystemLanguage lang = Application.systemLanguage;
        string res = "en";
        switch (lang)
        {
            case SystemLanguage.English: res = "en"; break;
            case SystemLanguage.French: res = "fr"; break;
            case SystemLanguage.Spanish: res = "es"; break;
            case SystemLanguage.Italian: res = "it"; break;
            case SystemLanguage.German: res = "de"; break;
            case SystemLanguage.Slovak: res = "sk"; break;
            case SystemLanguage.Arabic: res = "ar"; break;
            case SystemLanguage.Catalan: res = "ca"; break;
            case SystemLanguage.Turkish: res = "tr"; break;
        }
        return res;
    }
}