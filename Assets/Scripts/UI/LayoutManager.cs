using UnityEngine;

public class LayoutManager : MonoBehaviour
{
	public int layoutId;

    public GameObject minimap;
    [SerializeField] private GameObject[] subtitles;
    [SerializeField] private GameObject[] kitchenAudioOnly;

    [Header("Screens")]
    [SerializeField] private GameObject[] screenOffice;
    [SerializeField] private GameObject[] screenMonitor;
    [SerializeField] private GameObject[] screenMonitorUI;
    [SerializeField] private GameObject[] screenUI;
    [SerializeField] private GameObject[] screenMinimap;
    [SerializeField] private GameObject[] screenSubtitles;
    [SerializeField] private GameObject[] screenPointer;

    void Start()
	{
        // Get scripts
        layoutId = SaveManager.saveData.settings.layoutId;

        if (layoutId == 0)
        {
            TVOnly();
        }
        else if (layoutId == 1)
        {
            TVGamepadClassic();
        }
        else if (layoutId == 2)
        {
            TVGamepadAlternative();
        }
        else if (layoutId == 3)
        {
            GamepadOnly();
        }

        ChangeSubtitlePosition(false);
	}

    private void TVOnly()
    {
        // Screens
        screenOffice[0].SetActive(true);
        screenOffice[1].SetActive(false);

        screenMonitor[0].SetActive(true);
        screenMonitor[1].SetActive(false);

        screenMonitorUI[0].SetActive(true);
        screenMonitorUI[1].SetActive(false);

        screenUI[0].SetActive(true);
        screenUI[1].SetActive(false);

        screenMinimap[0].SetActive(true);
        screenMinimap[1].SetActive(false);

        screenSubtitles[0].SetActive(true);
        screenSubtitles[1].SetActive(false);

        screenPointer[0].SetActive(true);
        screenPointer[1].SetActive(false);

        // Change minimap position and scale
        minimap.transform.localScale = new Vector3(1f, 1f, 1f);
        minimap.transform.localPosition = new Vector3(407.7f, -152.4f, 0);

        // Kitchen Audio Only
        kitchenAudioOnly[0].SetActive(true);
        kitchenAudioOnly[1].SetActive(false);
    }

    private void TVGamepadClassic()
    {
        // Screens
        screenOffice[0].SetActive(true);
        screenOffice[1].SetActive(false);

        screenMonitor[0].SetActive(true);
        screenMonitor[1].SetActive(false);

        screenMonitorUI[0].SetActive(true);
        screenMonitorUI[1].SetActive(false);

        screenUI[0].SetActive(true);
        screenUI[1].SetActive(false);

        screenMinimap[0].SetActive(false);
        screenMinimap[1].SetActive(true);

        screenSubtitles[0].SetActive(false);
        screenSubtitles[1].SetActive(true);

        screenPointer[0].SetActive(true);
        screenPointer[1].SetActive(false);

        // Change minimap position and scale
        minimap.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        minimap.transform.localPosition = Vector3.zero;

        // Subtitles
        subtitles[0].SetActive(true);
        subtitles[1].SetActive(false);
        subtitles[2].SetActive(false);

        // Kitchen Audio Only
        kitchenAudioOnly[0].SetActive(true);
        kitchenAudioOnly[1].SetActive(false);
    }

    private void TVGamepadAlternative()
    {
        // Screens
        screenOffice[0].SetActive(true);
        screenOffice[1].SetActive(false);

        screenMonitor[0].SetActive(false);
        screenMonitor[1].SetActive(true);

        screenMonitorUI[0].SetActive(false);
        screenMonitorUI[1].SetActive(true);

        screenUI[0].SetActive(true);
        screenUI[1].SetActive(false);

        screenMinimap[0].SetActive(false);
        screenMinimap[1].SetActive(true);

        screenSubtitles[0].SetActive(true);
        screenSubtitles[1].SetActive(false);

        screenPointer[0].SetActive(true);
        screenPointer[1].SetActive(false);

        // Change minimap position and scale
        minimap.transform.localScale = new Vector3(1f, 1f, 1f);
        minimap.transform.localPosition = new Vector3(407.7f, -152.4f, 0);

        // Kitchen Audio Only
        kitchenAudioOnly[0].SetActive(false);
        kitchenAudioOnly[1].SetActive(true);
    }

    private void GamepadOnly()
    {
        // Screens
        screenOffice[0].SetActive(false);
        screenOffice[1].SetActive(true);

        screenMonitor[0].SetActive(false);
        screenMonitor[1].SetActive(true);

        screenMonitorUI[0].SetActive(false);
        screenMonitorUI[1].SetActive(true);

        screenUI[0].SetActive(false);
        screenUI[1].SetActive(true);

        screenMinimap[0].SetActive(false);
        screenMinimap[1].SetActive(true);

        screenSubtitles[0].SetActive(false);
        screenSubtitles[1].SetActive(true);

        screenPointer[0].SetActive(false);
        screenPointer[1].SetActive(true);

        // Change minimap position and scale
        minimap.transform.localScale = new Vector3(1f, 1f, 1f);
        minimap.transform.localPosition = new Vector3(407.7f, -152.4f, 0);

        // Kitchen Audio Only
        kitchenAudioOnly[0].SetActive(true);
        kitchenAudioOnly[1].SetActive(false);
    }

    public void ChangeSubtitlePosition(bool cameraStatus)
    {
        if (SaveManager.saveData.settings.subtitlesEnabled)
        {
            if (layoutId == 1)
            {
                if (cameraStatus)
                {
                    screenSubtitles[0].SetActive(false);
                    screenSubtitles[1].SetActive(true);

                    subtitles[0].SetActive(true);
                    subtitles[1].SetActive(false);
                    subtitles[2].SetActive(false);
                }
                else
                {
                    screenSubtitles[0].SetActive(true);
                    screenSubtitles[1].SetActive(false);

                    subtitles[0].SetActive(false);
                    subtitles[1].SetActive(true);
                    subtitles[2].SetActive(false);
                }
            }
            else if (layoutId == 0 || layoutId == 3)
            {
                if (cameraStatus)
                {
                    subtitles[0].SetActive(false);
                    subtitles[1].SetActive(false);
                    subtitles[2].SetActive(true);
                }
                else
                {
                    subtitles[0].SetActive(false);
                    subtitles[1].SetActive(true);
                    subtitles[2].SetActive(false);
                }
            }
            else
            {
                subtitles[0].SetActive(false);
                subtitles[1].SetActive(true);
                subtitles[2].SetActive(false);
            }
        }
        else
        {
            subtitles[0].SetActive(false);
            subtitles[1].SetActive(false);
            subtitles[2].SetActive(false);
        }
    }

    public void ChangePointerScreen(bool onGamepad)
    {
        if (layoutId == 1 || layoutId == 2)
        {
            screenPointer[0].SetActive(!onGamepad);
            screenPointer[1].SetActive(onGamepad);
        }
    }
}