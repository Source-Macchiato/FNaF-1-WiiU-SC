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
            float nightNumber = PlayerPrefs.GetFloat("NightNumber");
            if (nightNumber > 5)
            {
                return 5;
            }
            else
            {
                return PlayerPrefs.GetFloat("NightNumber");
            }
        }
        else
        {
            return 1;
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
}