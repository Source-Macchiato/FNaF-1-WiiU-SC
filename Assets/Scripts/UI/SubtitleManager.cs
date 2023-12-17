using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public Text subtitleText;
    public List<string> subtitleLines;
    public List<float> displayDurations;

    public float startDelay = 0.0f;
    private float displayStartTime;
    private int currentIndex = 0;
    private bool isDelayOver = false;

    void Start()
    {
        if (subtitleLines.Count != displayDurations.Count)
        {
            Debug.LogError("Subtitle lines and display durations lists must have the same size!");
            return;
        }

        displayStartTime = Time.timeSinceLevelLoad + startDelay;
    }

    void Update()
    {
        if (!isDelayOver)
        {
            if (Time.timeSinceLevelLoad >= displayStartTime)
            {
                isDelayOver = true;
                DisplaySubtitle();
            }
            return;
        }

        if (currentIndex >= subtitleLines.Count)
        {
            return;
        }

        if (Time.timeSinceLevelLoad >= displayStartTime + displayDurations[currentIndex])
        {
            currentIndex++;

            if (currentIndex < subtitleLines.Count)
            {
                DisplaySubtitle();
            }
            else
            {
                subtitleText.text = "";
                subtitleText.gameObject.SetActive(false);
            }
        }
    }

    void DisplaySubtitle()
    {
        subtitleText.text = subtitleLines[currentIndex];
        displayStartTime = Time.timeSinceLevelLoad;
    }
}
