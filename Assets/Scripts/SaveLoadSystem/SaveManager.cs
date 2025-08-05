using System;
using System.Text;
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class SaveManager : MonoBehaviour
{
    public static SaveData saveData = new SaveData();
    public static string token;

    void Start()
    {
        // Load data
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

        // Load token
        WiiU.SDCard.Init();
        //token = WiiU.SDCard.ReadAllText("wiiu/apps/BrewConnect/token").Trim();
        WiiU.SDCard.DeInit();
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(saveData);
        byte[] data = Encoding.UTF8.GetBytes(json);
        bool saveResult = SaveGameState.DoSave(data);
    }
}

[Serializable]
public class SaveData
{
    public Game game = new Game();
    public Settings settings = new Settings();
    public bool[] achievements = new bool[Enum.GetValues(typeof(Achievements.achievements)).Length];

    public bool GetAchievement(Achievements.achievements a)
    {
        return achievements[(int)a];
    }

    public void UnlockAchievement(Achievements.achievements a)
    {
        achievements[(int)a] = true;
    }
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
    public bool subtitlesEnabled = true;
}

[Serializable]
public class Volume
{
    public int generalVolume = 10;
    public int musicVolume = 10;
    public int voiceVolume = 10;
    public int sfxVolume = 10;
}