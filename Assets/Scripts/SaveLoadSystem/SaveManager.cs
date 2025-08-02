using System;
using System.Text;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveData saveData = new SaveData();

    void Start()
    {
        string json = SaveGameState.DoLoad();
        
        if (json != string.Empty)
        {
            saveData = JsonUtility.FromJson<SaveData>(json);

            Debug.Log("Data has been loaded.");
        }
        else
        {
            Debug.Log("Data has not been loaded.");
        }
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(saveData);
        byte[] data = Encoding.UTF8.GetBytes(json);
        bool saveResult = SaveGameState.DoSave(data);

        if (saveResult)
        {
            Debug.Log("Data has been saved.");
        }
        else
        {
            Debug.Log("An error occured while saving.");
        }
    }
}

[Serializable]
public class SaveData
{
    public Game game = new Game();
    public Settings settings = new Settings();
}

[Serializable]
public class Game
{
    public int nightNumber = 0;
    public int starsId = 0;
    public bool goldenFreddyUnlocked = false;
}

[Serializable]
public class Settings
{
    public string language = string.Empty;
    public string dubbingLanguage = string.Empty;
    public int layoutId = 1;
    public int shareAnalytics = -1;
    public Volume volume = new Volume();
    public bool motionControls = true;
    public bool pointerVisibility = true;
    public bool panoramaEffect = true;
}

[Serializable]
public class Volume
{
    public int generalVolume = 10;
    public int musicVolume = 10;
    public int voiceVolume = 10;
    public int sfxVolume = 10;
}