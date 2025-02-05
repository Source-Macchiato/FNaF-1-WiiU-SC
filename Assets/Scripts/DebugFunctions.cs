using UnityEngine;

public class DebugFunctions : MonoBehaviour
{
    private int nightNumber;

    SaveGameState saveGameState;
    SaveManager saveManager;

    void Start()
    {
        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        nightNumber = SaveManager.LoadNightNumber();
    }

    public void NightAdd()
    {
        nightNumber++;

        saveManager.SaveNightNumber(nightNumber);
        bool saveResult = saveGameState.DoSave();

        Debug.Log("Night number: " + nightNumber);
    }
}