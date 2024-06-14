using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class ControllersRumble : MonoBehaviour
{
    public int patternLength = 15;
    public bool rumbleTriggered = false;
    public float rumbleTimer = 0.0f;

    WiiU.GamePad gamePad;

    void Start()
    {
        gamePad = WiiU.GamePad.access;
    }

    public void Rumble()
    {
        byte[] pattern = new byte[patternLength];
        for (int i = 0; i < pattern.Length; ++i)
        {
            pattern[i] = 0xff;
        }

        gamePad.ControlMotor(pattern, pattern.Length * 8);
    }

    public void IsRumbleTriggered(string character)
    {
        if (!rumbleTriggered)
        {
            Debug.Log(character);
            rumbleTimer = 0.0f;
            Rumble();
            rumbleTriggered = true;
        }
    }

    public void CalculateRumbleDuration()
    {
        // this took me a lot of time
        if (rumbleTimer > (patternLength / 15))
        {
            rumbleTriggered = false;
        }
    }
}
