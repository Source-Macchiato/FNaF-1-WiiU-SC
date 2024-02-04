using System;
using System.Collections.Generic;
using UnityEngine;

public class I18n
{
    public static Dictionary<string, string> Texts { get; private set; }

    public static Dictionary<string, string> ENTexts { get; private set; } //(max la menace) THIS IS A TERRIBLE SOLUTION TO THIS PROBLEM BUT IT WORKS

    public static bool forceEnglish = false;

    static I18n()
    {
        LoadLanguage();
        ENLoadLanguage();
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
            lang = "EN";
        }
        else if (languagePlayerPrefs == "French")
        {
            lang = "FR";
        }
        else if (languagePlayerPrefs == "Spanish")
        {
            lang = "ES";
        }
        else
        {
            lang = Get2LetterISOCodeFromSystemLanguage().ToLower();
        }

        string filePath = "I18n/" + lang;

        TextAsset textAsset = Resources.Load<TextAsset>(filePath);

        if (textAsset == null)
        {
            Debug.LogError("File not found for I18n: Assets/Resources/" + filePath + ".txt");

            filePath = "I18n/en";
            textAsset = Resources.Load<TextAsset>(filePath);

            if (textAsset == null)
            {
                Debug.LogError("Default file not found for I18n: Assets/Resources/" + filePath + ".txt");
                return;
            }
        }

        string allTexts = textAsset.text;
        string[] lines = allTexts.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#"))
            {
                string key = lines[i].Substring(0, lines[i].IndexOf("="));
                string value = lines[i].Substring(lines[i].IndexOf("=") + 1, lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);
                Texts.Add(key, value);
            }
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
            Debug.LogError("File not found for I18n: Assets/Resources/" + filePath + ".txt");

            filePath = "I18n/en";
            textAsset = Resources.Load<TextAsset>(filePath);

            if (textAsset == null)
            {
                Debug.LogError("Default file not found for I18n: Assets/Resources/" + filePath + ".txt");
                return;
            }
        }

        string allTexts = textAsset.text;
        string[] lines = allTexts.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#"))
            {
                string key = lines[i].Substring(0, lines[i].IndexOf("="));
                string value = lines[i].Substring(lines[i].IndexOf("=") + 1, lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", Environment.NewLine);
                ENTexts.Add(key, value);
            }
        }
    }
    public static string GetLanguage()
    {
        return forceEnglish ? "en" : Get2LetterISOCodeFromSystemLanguage().ToLower();
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
        }
        return res;
    }
}
