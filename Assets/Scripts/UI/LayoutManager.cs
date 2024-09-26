using UnityEngine;

public class LayoutManager : MonoBehaviour
{
	private int layoutId;

    [Header("Screens")]
    public GameObject[] screenOffice;
    public GameObject[] screenCamera;
    public GameObject[] screenMinimap;
    public GameObject[] screenUI;

    public GameObject minimap;

    // Scripts
    SaveGameState saveGameState;
    SaveManager saveManager;

    void Start()
	{
        // Get scripts
        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        layoutId = SaveManager.LoadLayoutId();

        if (layoutId == 0)
        {
            TVOnly();
        }
        else if (layoutId == 1)
        {
            TVGamepad();
        }
        else if (layoutId == 2)
        {
            GamepadOnly();
        }
	}
	
	void Update()
	{
		
	}

    private void TVOnly()
    {
        screenOffice[0].SetActive(true);
        screenOffice[1].SetActive(false);

        screenCamera[0].SetActive(true);
        screenCamera[1].SetActive(false);

        screenMinimap[0].SetActive(true);
        screenMinimap[1].SetActive(false);

        screenUI[0].SetActive(true);
        screenUI[1].SetActive(false);

        minimap.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        minimap.transform.localPosition = new Vector3(611.3f, -230.3f, 0);
    }

    private void TVGamepad()
    {
        screenOffice[0].SetActive(true);
        screenOffice[1].SetActive(false);

        screenCamera[0].SetActive(true);
        screenCamera[1].SetActive(false);

        screenMinimap[0].SetActive(false);
        screenMinimap[1].SetActive(true);

        screenUI[0].SetActive(true);
        screenUI[1].SetActive(false);

        minimap.transform.localScale = new Vector3(2f, 2f, 1f);
        minimap.transform.localPosition = Vector3.zero;
    }

    private void GamepadOnly()
    {
        screenOffice[0].SetActive(false);
        screenOffice[1].SetActive(true);

        screenCamera[0].SetActive(false);
        screenCamera[1].SetActive(true);

        screenMinimap[0].SetActive(false);
        screenMinimap[1].SetActive(true);

        screenUI[0].SetActive(false);
        screenUI[1].SetActive(true);

        minimap.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        minimap.transform.localPosition = new Vector3(611.3f, -230.3f, 0);
    }
}