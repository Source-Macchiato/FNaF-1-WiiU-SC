using UnityEngine;
using UnityEngine.EventSystems;

public class CustomCursor : MonoBehaviour
{
	public GameObject cursorGameObject;
	
	void Update()
	{
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (!cursorGameObject.activeSelf)
            {
                cursorGameObject.SetActive(true);
            }
        }
        else
        {
            if (cursorGameObject.activeSelf)
            {
                cursorGameObject.SetActive(false);
            }
        }
    }
}