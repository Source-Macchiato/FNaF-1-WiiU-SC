using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WiiU = UnityEngine.WiiU;

public class EndScirpt : MonoBehaviour {
	WiiU.GamePad gamePad;
	void Start () {
		gamePad = WiiU.GamePad.access;
	}
	
	void Update () {
		WiiU.GamePadState gamePadState = gamePad.state;
		if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.A))
			{
				SceneManager.LoadScene("MainMenu");
			}
		}
		else if (!Application.isEditor)
		{
			if (gamePadState.gamePadErr == WiiU.GamePadError.None)
			{
				if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
				{
					SceneManager.LoadScene("MainMenu");
				}
			}
		}
		
	}
}
