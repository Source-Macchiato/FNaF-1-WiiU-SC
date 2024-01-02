using UnityEngine;

public class DebugFunctions : MonoBehaviour
{
    private float NightNumber;

    void Start()
    {
        NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);
    }

    public void NightAdd()
    {
        NightNumber++;
        PlayerPrefs.SetFloat("NightNumber", NightNumber);
        PlayerPrefs.Save();
        Debug.Log("Night number = " + NightNumber);
    }
}