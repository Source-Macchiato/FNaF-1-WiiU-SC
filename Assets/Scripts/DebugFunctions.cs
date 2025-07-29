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

        nightNumber = SaveManager.saveData.game.nightNumber;
    }

    public void NightAdd()
    {
        SaveManager.saveData.game.nightNumber = nightNumber++;
        SaveManager.Save();

        Debug.Log("Night number: " + nightNumber);
    }
}