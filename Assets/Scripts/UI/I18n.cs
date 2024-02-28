using System;
using System.Collections.Generic;
using UnityEngine;

public class I18n
{
    public static Dictionary<string, string> Texts { get; private set; }

    public static Dictionary<string, string> ENTexts { get; private set; } //(max la menace) THIS IS A TERRIBLE SOLUTION TO THIS PROBLEM BUT IT WORKS

    static I18n()
    {
        LoadLanguage();
        ENLoadLanguage();
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

        TextAsset textAsset = Resources.Load<TextAsset>(filePath);

        if (textAsset == null)
        {
            Debug.LogError("File not found for I18n: Assets/Resources/" + filePath + ".json");

            filePath = "I18n/en";
            textAsset = Resources.Load<TextAsset>(filePath);

            if (textAsset == null)
            {
                Debug.LogError("Default file not found for I18n: Assets/Resources/" + filePath + ".json");
                return;
            }
        }

        string allTexts = textAsset.text;
        var translations = JsonUtility.FromJson<Dictionary<string, string>>(allTexts);

        foreach (var translation in translations)
        {
            Debug.Log("key" + translation.Key + ", value" + translation.Value);
            Texts.Add(translation.Key, translation.Value.Replace("\\n", Environment.NewLine));
        }
    }

    private static void ENLoadLanguage()
    {
        if (ENTexts == null)
            ENTexts = new Dictionary<string, string>();

        ENTexts.Clear();

        string filePath = "I18n/en";

        TextAsset textAsset = Resources.Load<TextAsset>(filePath);

        if (textAsset == null)
        {
            Debug.LogError("File not found for I18n: Assets/Resources/" + filePath + ".json");

            filePath = "I18n/en";
            textAsset = Resources.Load<TextAsset>(filePath);

            if (textAsset == null)
            {
                Debug.LogError("Default file not found for I18n: Assets/Resources/" + filePath + ".json");
                return;
            }
        }

        string allTexts = textAsset.text;
        var translations = JsonUtility.FromJson<Dictionary<string, string>>(allTexts);

        foreach (var translation in translations)
        {
            Debug.Log("key" + translation.Key + ", value" + translation.Value);
            ENTexts.Add(translation.Key, translation.Value.Replace("\\n", Environment.NewLine));
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