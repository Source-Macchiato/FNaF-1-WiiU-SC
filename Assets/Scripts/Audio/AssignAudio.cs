using UnityEngine;
using WiiU = UnityEngine.WiiU;

[RequireComponent(typeof(AudioSource))]
public class AssignAudio : MonoBehaviour
{
	private AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();

		WiiU.AudioSourceOutput.Assign(audioSource, WiiU.AudioOutput.TV|WiiU.AudioOutput.GamePad);
	}
}
