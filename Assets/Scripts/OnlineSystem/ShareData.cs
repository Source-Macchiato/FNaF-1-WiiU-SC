using UnityEngine;

public class ShareData : MonoBehaviour
{
    void Start () {
		
	}
	
	void Update () {
		
	}

	private void GetData()
	{
		// Get game version
        TextAsset versionAsset = Resources.Load<TextAsset>("Meta/version");
        string localVersion = versionAsset.text;
    }
}
