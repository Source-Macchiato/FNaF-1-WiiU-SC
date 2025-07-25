using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class ControllersRumble : MonoBehaviour
{
    private float rumbleDuration = 0.0f;
    private float rumbleTimer = 0.0f;
    private bool rumbling = false;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    void Start()
    {
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);
    }

    void Update()
    {
        if (rumbling)
        {
            rumbleTimer += Time.deltaTime;

            if (rumbleTimer >= rumbleDuration)
            {
                StopRumble();
            }
        }
    }

    private void StartRumble(float duration)
    {
        int patternlength = Mathf.CeilToInt(duration * 120);
        byte[] pattern = new byte[patternlength];

        for (int i = 0; i < pattern.Length; ++i)
        {
            pattern[i] = 0xff;
        }

        gamePad.ControlMotor(pattern, pattern.Length * 8);
        remote.PlayRumblePattern(pattern, pattern.Length * 8);
    }

    private void StopRumble()
    {
        byte[] pattern = new byte[1] { 0x00 };

        gamePad.ControlMotor(pattern, 8);
        remote.PlayRumblePattern(pattern, 8);

        rumbling = false;
    }

    public void TriggerRumble(float duration, string log = "")
    {
        if (!rumbling)
        {
            if (!string.IsNullOrEmpty(log))
            {
                Debug.Log(log);
            }

            if (duration >= 0.1f)
            {
                rumbleTimer = 0.0f;

                StartRumble(duration);

                rumbling = true;
            }
        }
    }
}