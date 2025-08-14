using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopupOptions : MonoBehaviour
{
	public Button[] buttons;
	public GameObject[] cursors;
	
	void Update()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			cursors[i].SetActive(EventSystem.current.currentSelectedGameObject == buttons[i].gameObject);
        }
	}

	public void ShareAnalytics(bool share)
	{
		AnalyticsData.analyticsData.ShareAnalytics(share);
	}
}
