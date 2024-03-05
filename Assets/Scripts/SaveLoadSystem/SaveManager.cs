using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour {

	private string[] saveFiles;

	public string language;
	public int nightNumber;

	void Start () {
        saveFiles = Directory.GetFiles(Application.persistentDataPath, "game_data.bin");
    }

	public void SaveLanguage(string language)
	{
        string languageData = JsonUtility.ToJson(language);

        PlayerPrefs.SetString("Language", language);
        PlayerPrefs.Save();
    }

    public void LoadLanguage(string language)
	{

	}
}
