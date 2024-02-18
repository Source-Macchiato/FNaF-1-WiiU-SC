using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    private List<string> logMessages = new List<string>();
    private Vector2 scrollPosition = Vector2.zero;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logMessages.Add(logString);
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20));
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (var message in logMessages)
        {
            GUILayout.Label(message);
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
}