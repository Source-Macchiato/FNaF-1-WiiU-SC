using UnityEngine;

public class SaveManager : MonoBehaviour {

    // Save
	public void SaveLanguage(string language)
	{
        PlayerPrefs.SetString("Language", language);
        PlayerPrefs.Save();
    }

    public void SaveNightNumber(float nightNumber)
    {
        PlayerPrefs.SetFloat("NightNumber", nightNumber);
        PlayerPrefs.Save();
    }

    public void SaveShareData(float shareData)
    {
        PlayerPrefs.SetFloat("ShareData", shareData);
        PlayerPrefs.Save();
    }

    public void SaveLayoutId(int layoutId)
    {
        PlayerPrefs.SetInt("Layout", layoutId);
        PlayerPrefs.Save();
    }

    public void SaveDubbingLanguage(string language)
    {
        PlayerPrefs.SetString("DubbingLanguage", language);
        PlayerPrefs.Save();
    }

    public void SaveStars(int starsId)
    {
        PlayerPrefs.SetInt("Stars", starsId);
        PlayerPrefs.Save();
    }

    public void SaveGoldenFreddyStatus(int goldenFreddyEnabled)
    {
        PlayerPrefs.SetInt("GoldenFreddy", goldenFreddyEnabled);
        PlayerPrefs.Save();
    }

    public void SaveGeneralVolume(int volume)
    {
        PlayerPrefs.SetInt("GeneralVolume", volume);
        PlayerPrefs.Save();
    }

    public void SaveMusicVolume(int volume)
    {
        PlayerPrefs.SetInt("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SaveVoiceVolume(int volume)
    {
        PlayerPrefs.SetInt("VoiceVolume", volume);
        PlayerPrefs.Save();
    }

    public void SaveSFXVolume(int volume)
    {
        PlayerPrefs.SetInt("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    // Load
    public static string LoadLanguage()
	{
        if (PlayerPrefs.HasKey("Language"))
        {
            return PlayerPrefs.GetString("Language");
        }
        else
        {
            return null;
        }
    }

    public static float LoadNightNumber()
    {
        if (PlayerPrefs.HasKey("NightNumber"))
        {
            return PlayerPrefs.GetFloat("NightNumber");
        }
        else
        {
            return 0;
        }
    }

    public static float LoadShareData()
    {
        if (PlayerPrefs.HasKey("ShareData"))
        {
            float shareData = PlayerPrefs.GetFloat("ShareData");

            return shareData;
        }
        else
        {
            return -1;
        }
    }

    public static int LoadLayoutId()
    {
        if (PlayerPrefs.HasKey("Layout"))
        {
            int layoutId = PlayerPrefs.GetInt("Layout");

            return layoutId;
        }
        else
        {
            return 1;
        }
    }

    public static string LoadDubbingLanguage()
    {
        if (PlayerPrefs.HasKey("DubbingLanguage"))
        {
            return PlayerPrefs.GetString("DubbingLanguage");
        }
        else
        {
            return null;
        }
    }

    public static int LoadStarsId()
    {
        if (PlayerPrefs.HasKey("Stars"))
        {
            return PlayerPrefs.GetInt("Stars");
        }
        else
        {
            return 0;
        }
    }

    public static int LoadGoldenFreddyStatus()
    {
        if (PlayerPrefs.HasKey("GoldenFreddy"))
        {
            return PlayerPrefs.GetInt("GoldenFreddy");
        }
        else
        {
            return 0;
        }
    }

    public static int LoadGeneralVolume()
    {
        if (PlayerPrefs.HasKey("GeneralVolume"))
        {
            return PlayerPrefs.GetInt("GeneralVolume");
        }
        else
        {
            return 10;
        }
    }

    public static int LoadMusicVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            return PlayerPrefs.GetInt("MusicVolume");
        }
        else
        {
            return 10;
        }
    }

    public static int LoadVoiceVolume()
    {
        if (PlayerPrefs.HasKey("VoiceVolume"))
        {
            return PlayerPrefs.GetInt("VoiceVolume");
        }
        else
        {
            return 10;
        }
    }

    public static int LoadSFXVolume()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            return PlayerPrefs.GetInt("SFXVolume");
        }
        else
        {
            return 10;
        }
    }
}