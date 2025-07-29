using RTLTMPro;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class SubtitlesManager : MonoBehaviour
{
    public GameObject[] subtitlesContainers;
    public GameObject muteCall;
    public AudioSource phoneCall;

    private List<string> subtitleIdentifiers;
    private List<float> displayDurations;

    private float displayStartTime;
    private int currentIndex = 0;
    private bool isDelayOver = false;

    private float nightNumber;

    private TextAsset subtitleFile;

    [Header("Fonts")]
    public TMP_FontAsset mainFont;
    public TMP_FontAsset arabicFont;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    void Start()
    {
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        subtitleIdentifiers = new List<string>();
        displayDurations = new List<float>();

        nightNumber = SaveManager.saveData.game.nightNumber;

        if (nightNumber >= 0 && nightNumber <= 4)
        {
            subtitleFile = Resources.Load<TextAsset>("Data/night" + (nightNumber + 1));

            string[] lines = subtitleFile.text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(new char[] { ';' });
                if (parts.Length == 2)
                {
                    subtitleIdentifiers.Add(parts[0]);
                    displayDurations.Add(float.Parse(parts[1]));
                }
            }

            displayStartTime = Time.timeSinceLevelLoad;
        }

        if (nightNumber == 4)
        {
            foreach (GameObject subtitleContainer in subtitlesContainers)
            {
                if (subtitleContainer.activeSelf)
                {
                    subtitleContainer.transform.Rotate(0f, 180f, 0f);
                }
            }
        }

        displayStartTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if (nightNumber >= 0 && nightNumber <= 4)
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

            if (currentIndex < subtitleIdentifiers.Count)
            {
                WiiU.GamePadState gamePadState = gamePad.state;
                WiiU.RemoteState remoteState = remote.state;
                if (gamePadState.gamePadErr == WiiU.GamePadError.None)
                {
                    if (gamePadState.IsTriggered(WiiU.GamePadButton.Minus))
                    {
                        MuteCall();
                        return;
                    }
                }
                switch (remoteState.devType)
                {
                    case WiiU.RemoteDevType.ProController:
                        if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Minus))
                        {
                            MuteCall();
                            return;
                        }
                        break;
                    case WiiU.RemoteDevType.Classic:
                        if (remoteState.classic.IsTriggered(WiiU.ClassicButton.Minus))
                        {
                            MuteCall();
                            return;
                        }
                        break;
                    default:
                        if (remoteState.IsTriggered(WiiU.RemoteButton.Minus))
                        {
                            MuteCall();
                            return;
                        }
                        break;
                }
                if (Application.isEditor)
                {
                    if (Input.GetKeyDown(KeyCode.KeypadMinus))
                    {
                        MuteCall();
                        return;
                    }
                }
            }

            if (currentIndex >= subtitleIdentifiers.Count)
            {
                return;
            }

            if (Time.timeSinceLevelLoad >= displayStartTime + displayDurations[currentIndex])
            {
                currentIndex++;

                if (currentIndex < subtitleIdentifiers.Count)
                {
                    DisplaySubtitle();
                }
                else
                {
                    muteCall.SetActive(false);

                    foreach (GameObject subtitleContainer in subtitlesContainers)
                    {
                        Text textComponent = subtitleContainer.GetComponent<Text>();
                        RTLTextMeshPro tmpTextComponent = subtitleContainer.GetComponent<RTLTextMeshPro>();

                        if (textComponent != null)
                        {
                            if (subtitleContainer.activeSelf)
                            {
                                textComponent.text = "";
                                subtitleContainer.SetActive(false);
                            }
                        }

                        if (tmpTextComponent != null)
                        {
                            if (subtitleContainer.activeSelf)
                            {
                                tmpTextComponent.text = "";
                                subtitleContainer.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            muteCall.SetActive(false);

            foreach (GameObject subtitleContainer in subtitlesContainers)
            {
                Text textComponent = subtitleContainer.GetComponent<Text>();
                RTLTextMeshPro tmpTextComponent = subtitleContainer.GetComponent<RTLTextMeshPro>();

                if (textComponent != null)
                {
                    if (subtitleContainer.activeSelf)
                    {
                        textComponent.text = "";
                        subtitleContainer.SetActive(false);
                    }
                }

                if (tmpTextComponent != null)
                {
                    if (subtitleContainer.activeSelf)
                    {
                        tmpTextComponent.text = "";
                        subtitleContainer.SetActive(false);
                    }
                }
            }
        }
    }

    void DisplaySubtitle()
    {
        string translatedText = GetTranslatedText(subtitleIdentifiers[currentIndex]);

        muteCall.SetActive(true);

        foreach (GameObject subtitleContainer in subtitlesContainers)
        {
            Text textComponent = subtitleContainer.GetComponent<Text>();
            RTLTextMeshPro tmpTextComponent = subtitleContainer.GetComponent<RTLTextMeshPro>();

            if (textComponent != null)
            {
                if (subtitleContainer.activeSelf)
                {
                    textComponent.text = translatedText;
                }
            }

            if (tmpTextComponent != null)
            {
                if (subtitleContainer.activeSelf)
                {
                    tmpTextComponent.text = translatedText;

                    if (I18n.GetLanguage() == "ar")
                    {
                        if (arabicFont != null)
                        {
                            tmpTextComponent.font = arabicFont;
                        }
                    }
                    else
                    {
                        if (mainFont != null)
                        {
                            tmpTextComponent.font = mainFont;
                        }
                    }
                }
            }
        }

        displayStartTime = Time.timeSinceLevelLoad;
    }

    string GetTranslatedText(string identifier)
    {
        if (I18n.Texts.ContainsKey(identifier))
        {
            return I18n.Texts[identifier];
        }
        else
        {
            return identifier;
        }
    }

    public void MuteCall()
    {
        currentIndex = subtitleIdentifiers.Count;

        foreach (GameObject subtitleContainer in subtitlesContainers)
        {
            Text textComponent = subtitleContainer.GetComponent<Text>();
            RTLTextMeshPro tmpTextComponent = subtitleContainer.GetComponent<RTLTextMeshPro>();

            if (textComponent != null)
            {
                if (subtitleContainer.activeSelf)
                {
                    textComponent.text = "";
                }
            }

            if (tmpTextComponent != null)
            {
                if (subtitleContainer.activeSelf)
                {
                    tmpTextComponent.text = "";
                }
            }

            subtitleContainer.SetActive(false);
        }

        phoneCall.Stop();
        muteCall.SetActive(false);
    }
}