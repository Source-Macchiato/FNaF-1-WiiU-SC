using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class UserInputTextSwitcher : MonoBehaviour
{
    public int currentIndex = 0;
    public Text setLanguageText;
    public List<string> setLanguageTextList;
    public GameObject AudioMenuPanel;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    void Start()
    {
        string language = I18n.GetLanguage();

        if (language == "fr")
        {
            currentIndex = 1;
        }
        else if (language == "es")
        {
            currentIndex = 2;
        }
        else if (language == "it")
        {
            currentIndex = 3;
        }
        else if (language == "sk")
        {
            currentIndex = 4;
        }
        else if (language == "ar")
        {
            currentIndex = 5;
        }
        else
        {
            currentIndex = 0;
        }

        //setLanguageText.text = setLanguageTextList[currentIndex];

        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        if (AudioMenuPanel.activeSelf)
        {
            // Gamepad
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.Left))
                {
                    currentIndex = Mathf.Max(currentIndex - 1, 0);
                    setLanguageText.text = setLanguageTextList[currentIndex];
                }
                else if (gamePadState.IsTriggered(WiiU.GamePadButton.Right))
                {
                    currentIndex = Mathf.Min(currentIndex + 1, setLanguageTextList.Count - 1);
                    setLanguageText.text = setLanguageTextList[currentIndex];
                }
            }

            // Remote
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Left))
                    {
                        currentIndex = Mathf.Max(currentIndex - 1, 0);
                        setLanguageText.text = setLanguageTextList[currentIndex];
                    }
                    else if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Right))
                    {
                        currentIndex = Mathf.Min(currentIndex + 1, setLanguageTextList.Count - 1);
                        setLanguageText.text = setLanguageTextList[currentIndex];
                    }
                    break;

                default:
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currentIndex = Mathf.Max(currentIndex - 1, 0);
                    setLanguageText.text = setLanguageTextList[currentIndex];
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentIndex = Mathf.Min(currentIndex + 1, setLanguageTextList.Count - 1);
                    setLanguageText.text = setLanguageTextList[currentIndex];
                }
            }
        }
    }
}