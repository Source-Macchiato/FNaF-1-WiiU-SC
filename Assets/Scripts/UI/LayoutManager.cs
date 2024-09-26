using UnityEngine;

public class LayoutManager : MonoBehaviour
{
	private int layoutId;

    [Header("Canvas")]
    public Canvas canvaOffice;
    public Canvas canvaCamera;
    public Canvas canvaUI;
    public Canvas canvaMinimap;

    [Header("Cameras")]
    public Camera cameraTV;
    public Camera cameraGamepad;

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
	}
	
	void Update()
	{
		
	}

    private void TVOnly()
    {
        canvaOffice.worldCamera = cameraTV;
        canvaUI.targetDisplay = 0;
    }
}