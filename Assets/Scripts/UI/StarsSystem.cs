using UnityEngine;
using UnityEngine.UI;

public class StarsSystem : MonoBehaviour
{
	public GameObject starsContainer;
	private int starsId;

	void Start()
	{
		starsId = SaveManager.LoadStarsId();

        int counter = 0;

        foreach (Image star in starsContainer.transform.GetComponentsInChildren<Image>())
		{
			star.enabled = counter < starsId;

			counter++;
		}
	}
}
