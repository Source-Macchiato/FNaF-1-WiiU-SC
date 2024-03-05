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
}