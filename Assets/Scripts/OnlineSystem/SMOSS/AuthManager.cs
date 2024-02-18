using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class AuthManager : MonoBehaviour
{
    WiiU.GamePad gamePad;

    public float joystickThreshold = 0.5f;
    public float buttonChangeDelay = 0.2f;

    private const string apiUrl = "https://api.portal-wiiu-edition.com/authenticate";

	public GameObject LoginPanel;
    public InputField[] inputFields;
    private int currentIndex = 0;

    TouchScreenKeyboard usernameKeyboard;
    TouchScreenKeyboard passwordKeyboard;

    void Start () {
        gamePad = WiiU.GamePad.access;
    }
	
	void Update () {
        WiiU.GamePadState gamePadState = gamePad.state;

        if (LoginPanel.activeSelf)
        {
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (!usernameKeyboard.active || !passwordKeyboard.active)
                {
                    if (gamePadState.IsTriggered(WiiU.GamePadButton.Up))
                    {
                        inputFields[currentIndex].DeactivateInputField();

                        currentIndex = (currentIndex + 1) % inputFields.Length;
                    }
                    
                    if (gamePadState.IsTriggered(WiiU.GamePadButton.Down))
                    {
                        inputFields[currentIndex].DeactivateInputField();

                        currentIndex = (currentIndex - 1 + inputFields.Length) % inputFields.Length;
                    }
                }
                
                if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
                {
                    inputFields[currentIndex].ActivateInputField();

                    if (currentIndex == 0)
                    {
                        usernameKeyboard = TouchScreenKeyboard.Open(inputFields[0].text, TouchScreenKeyboardType.Default, false, false, false, false, "Username");
                        usernameKeyboard.targetDisplay = WiiU.DisplayIndex.GamePad;
                    }
                    else if (currentIndex == 1)
                    {
                        passwordKeyboard = TouchScreenKeyboard.Open(inputFields[1].text, TouchScreenKeyboardType.Default, false, false, true, false, "Password");
                        passwordKeyboard.targetDisplay = WiiU.DisplayIndex.GamePad;
                    }
                }

                if (gamePadState.IsTriggered(WiiU.GamePadButton.Plus))
                {
                    LoginPanel.SetActive(false);
                }
            }

            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    inputFields[currentIndex].DeactivateInputField();

                    currentIndex = (currentIndex + 1) % inputFields.Length;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    inputFields[currentIndex].DeactivateInputField();

                    currentIndex = (currentIndex - 1 + inputFields.Length) % inputFields.Length;
                }
                else if (Input.GetKeyDown(KeyCode.Return))
                {
                    inputFields[currentIndex].ActivateInputField();

                    if (currentIndex == 0)
                    {
                        usernameKeyboard = TouchScreenKeyboard.Open(inputFields[0].text, TouchScreenKeyboardType.Default, false, false, false, false, "Username");
                        usernameKeyboard.targetDisplay = WiiU.DisplayIndex.GamePad;
                    }
                    else if (currentIndex == 1)
                    {
                        passwordKeyboard = TouchScreenKeyboard.Open(inputFields[1].text, TouchScreenKeyboardType.Default, false, false, true, false, "Password");
                        passwordKeyboard.targetDisplay = WiiU.DisplayIndex.GamePad;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    LoginPanel.SetActive(false);
                }
            }
        }
    }
}
