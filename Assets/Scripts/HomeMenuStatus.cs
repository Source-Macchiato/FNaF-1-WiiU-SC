using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class HomeMenuStatus : MonoBehaviour
{
	public bool enableHomeMenu = false;

	void Start()
	{
		WiiU.Core.homeMenuEnabled = enableHomeMenu;
	}
}
