using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class AuthManager : MonoBehaviour
{
    WiiU.GamePad gamePad;

    public float joystickThreshold = 0.5f;
    public float buttonChangeDelay = 0.2f;

    private const string apiUrl = "https://api.portal-wiiu-edition.com/authenticate";

	public GameObject loginPanel;
    public InputField[] inputFields;
    private int currentIndex = 0;
    public Text statusText;

    TouchScreenKeyboard usernameKeyboard;
    TouchScreenKeyboard passwordKeyboard;

    void Start () {
        gamePad = WiiU.GamePad.access;
    }
	
	void Update () {
        WiiU.GamePadState gamePadState = gamePad.state;

        if (loginPanel.activeSelf)
        {
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (usernameKeyboard == null || !usernameKeyboard.active || passwordKeyboard == null || !passwordKeyboard.active)
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
                    Login();
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
                    Login();
                }
            }
        }
    }

    public void Login()
    {
        string username = inputFields[0].text;
        string password = inputFields[1].text;
        StartCoroutine(LoginRequest(username, password));
    }

    private IEnumerator LoginRequest(string username, string password)
    {
        string url = apiUrl;
        string jsonString = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonString);

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        WWW www = new WWW(url, postData, headers);

        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            statusText.text = "API connection error : " + www.error;
        }
        else
        {
            string jsonResponse = www.text;
            AuthenticationResponse response = JsonUtility.FromJson<AuthenticationResponse>(jsonResponse);

            if (response != null)
            {
                if (response.message == "Authentication successful")
                {
                    string[] badgesArray = response.badges.Split(',');

                    if (Array.Exists(badgesArray, badge => badge.Trim() == "1") || Array.Exists(badgesArray, badge => badge.Trim() == "2"))
                    {
                        loginPanel.SetActive(false);
                    }
                    else
                    {
                        statusText.text = "You are not authorized to access this test version.";
                    }
                }
                else
                {
                    statusText.text = "Authentication failed : " + response.message;
                }
            }
            else
            {
                statusText.text = "Invalid response from the server";
            }
        }
    }

    [System.Serializable]
    private class AuthenticationResponse
    {
        public string message;
        public string authToken;
        public string badges;
    }
}
