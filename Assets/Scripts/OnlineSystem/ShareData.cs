using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class ShareData : MonoBehaviour
{
	private GameObject shareDataPanel;
	private GameObject updatePanel;

	private const string url = "http://localhost/v1/fnaf/analytics";

    void Start () {
		shareDataPanel = GameObject.Find("ShareDataPanel");
		updatePanel = GameObject.Find("UpdatePanel");

		shareDataPanel.SetActive(false);
	}
	
	void Update () {
		
	}

	private void GetData()
	{
        // Get username
        string username = WiiU.Core.accountName;

        // Get game version
        TextAsset versionAsset = Resources.Load<TextAsset>("Meta/version");
        string localVersion = versionAsset.text;
    }
}
