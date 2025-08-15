using UnityEngine;

public class DebugFunctions : MonoBehaviour
{
    private int nightNumber;

    void Start()
    {
        nightNumber = SaveManager.saveData.game.nightNumber;
    }

    public void NightAdd()
    {
        SaveManager.saveData.game.nightNumber = nightNumber++;
        SaveManager.Save();

        Debug.Log("Night number: " + nightNumber);
    }
}