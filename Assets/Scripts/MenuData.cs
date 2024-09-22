using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuData : MonoBehaviour
{
    public float nightNumber;
    public GameObject nightNumberPrefab;
    public Text currentLanguageText;

    [HideInInspector]
    public GameObject nightNumberGameObject;

    // Scripts
    SaveGameState saveGameState;
    SaveManager saveManager;

    // Advertisement
    public GameObject advertisementImage;
    private bool advertisementIsActive;
    private float startTime;
    private float waitTime = 10f;

    void Start()
    {
        // Get scripts
        saveGameState = FindObjectOfType<SaveGameState>();
        saveManager = FindObjectOfType<SaveManager>();

        // Load night number from save and display it
        nightNumber = SaveManager.LoadNightNumber();

        // Disable advertisement by default
        advertisementIsActive = false;
        advertisementImage.SetActive(false);
    }
	
	// Update is called once per frame
	void Update()
    {
        // Load scene after advertisementload is called
        if (Time.time - startTime >= waitTime && advertisementIsActive == true)
        {
            SceneManager.LoadScene("NextNight");
        }
    }

    public void LoadAdvertisement()
    {
        advertisementIsActive = true;
        startTime = Time.time;
        advertisementImage.SetActive(true);
    }

    public void SaveAndUpdateLanguage()
    {
        saveManager.SaveLanguage(currentLanguageText.text);
        bool saveResult = saveGameState.DoSave();

        // Reload the language
        I18n.LoadLanguage();
    }

    // Night number system 
    public void GenerateNightNumber()
    {
        Transform canvaUI = GameObject.Find("CanvaUI").transform;

        nightNumberGameObject = Instantiate(nightNumberPrefab, canvaUI);
        nightNumberGameObject.transform.Find("NumberText").GetComponent<Text>().text = nightNumber.ToString();

        RectTransform nightNumberRect = nightNumberGameObject.GetComponent<RectTransform>();
        nightNumberRect.anchoredPosition = new Vector2(-700f, -234.2f);

        nightNumberGameObject.SetActive(false);
    }

    public void DisplayNightNumber(bool display)
    {
        nightNumberGameObject.SetActive(display);
    }
}