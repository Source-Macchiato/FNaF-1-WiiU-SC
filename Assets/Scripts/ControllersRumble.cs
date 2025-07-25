using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class ControllersRumble : MonoBehaviour
{
    private float totalDuration = 0f;
    private float elapsed = 0f;
    private bool rumbling = false;

    private const float CHUNK_DURATION = 1.0f;
    private float currentChunkTime = 0f;

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
            elapsed += Time.deltaTime;
            currentChunkTime += Time.deltaTime;

            if (currentChunkTime >= CHUNK_DURATION && elapsed < totalDuration)
            {
                StartChunkRumble(Mathf.Min(CHUNK_DURATION, totalDuration - elapsed));
                currentChunkTime = 0f;
            }

            if (elapsed >= totalDuration)
            {
                StopRumble();
            }
        }
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
                totalDuration = duration;
                elapsed = 0f;
                currentChunkTime = 0f;
                rumbling = true;

                StartChunkRumble(Mathf.Min(CHUNK_DURATION, totalDuration));
            }
        }
    }

    private void StartChunkRumble(float duration)
    {
        int patternLength = Mathf.CeilToInt(duration * 120f);
        byte[] pattern = new byte[patternLength];

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
}