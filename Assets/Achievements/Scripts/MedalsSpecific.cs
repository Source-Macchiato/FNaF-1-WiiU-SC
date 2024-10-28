using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class MedalsSpecific : MonoBehaviour {
	public AudioSource achievementPlay;

	public void NoLongerShowing()
	{
		MedalsManager.medalsManager.isShowing = false;
	}

	public void PlaySFX()
    {
        WiiU.AudioSourceOutput.Assign(achievementPlay, WiiU.AudioOutput.TV | WiiU.AudioOutput.GamePad);
        achievementPlay.Play();
    }
}
