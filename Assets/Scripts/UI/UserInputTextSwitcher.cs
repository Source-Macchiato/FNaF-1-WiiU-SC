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

    void Start()
    {
        string languagePlayerPrefs = PlayerPrefs.GetString("Language", "None");

        if (languagePlayerPrefs == "French")
        {
            currentIndex = 1;
        }
        else if (languagePlayerPrefs == "Spanish")
        {
            currentIndex = 2;
        }
        else
        {
            currentIndex = 0;
        }

        setLanguageText.text = setLanguageTextList[currentIndex];

        gamePad = WiiU.GamePad.access;
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;

        if (AudioMenuPanel.activeSelf)
        {
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.Left))
                {
                    currentIndex = Mathf.Max(currentIndex - 1, 0);
                    setLanguageText.text = setLanguageTextList[currentIndex];
                }
                else if (gamePadState.IsTriggered(WiiU.GamePadButton.Left))
                {
                    currentIndex = Mathf.Min(currentIndex + 1, setLanguageTextList.Count - 1);
                    setLanguageText.text = setLanguageTextList[currentIndex];
                }
            }

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