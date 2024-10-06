using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuData : MonoBehaviour
{
    public float nightNumber;
    public GameObject nightNumberPrefab;
    public int layoutId;

    [Header("Layout images")]
    public Sprite tvOnly;
    public Sprite tvGamepad;
    public Sprite gamepadOnly;

    [HideInInspector]
    public GameObject nightNumberGameObject;
    public Button[] layoutButtons;

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

        // Load
        nightNumber = SaveManager.LoadNightNumber();
        layoutId = SaveManager.LoadLayoutId();

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
        // Get SwitcherData scripts
        SwitcherData[] switchers = FindObjectsOfType<SwitcherData>();

        foreach (SwitcherData switcher in switchers)
        {
            if (switcher.switcherId == "switcher.translation")
            {
                saveManager.SaveLanguage(switcher.optionsName[switcher.currentOptionId]);
                bool saveResult = saveGameState.DoSave();

                // Reload the language
                I18n.LoadLanguage();
            }
        }
    }

    public void SaveLayout()
    {
        saveManager.SaveLayoutId(layoutId);
        bool saveResult = saveGameState.DoSave();
    }

    // Night number system 
    public void GenerateNightNumber()
    {
        Transform canvaUI = GameObject.Find("CanvaUI").transform;
        Transform nightNumberContainer = canvaUI.Find("NightNumberContainer").transform;

        nightNumberGameObject = Instantiate(nightNumberPrefab, nightNumberContainer);

        Text textNightNumber = nightNumberGameObject.transform.Find("NumberText").GetComponent<Text>();
        TMP_Text tmpTextNightNumber = nightNumberGameObject.transform.Find("NumberText").GetComponent<TextMeshProUGUI>();

        if (textNightNumber != null)
        {
            textNightNumber.text = nightNumber.ToString();
        }

        if (tmpTextNightNumber != null)
        {
            tmpTextNightNumber.text = nightNumber.ToString();
        }

        RectTransform nightNumberRect = nightNumberGameObject.GetComponent<RectTransform>();
        nightNumberRect.anchoredPosition = new Vector2(-700f, -234.2f);

        nightNumberGameObject.SetActive(false);
    }

    public void UpdateCursorSize(bool isOriginalSize, GameObject cursor)
    {
        Text textComponent = cursor.GetComponent<Text>();
        TMP_Text tmpTextComponent = cursor.GetComponent<TextMeshProUGUI>();
        RectTransform rect = cursor.GetComponent<RectTransform>();

        if (isOriginalSize)
        {
            if (textComponent != null)
            {
                textComponent.fontSize = 72;
            }

            if (tmpTextComponent != null)
            {
                tmpTextComponent.fontSize = 72;
            }

            rect.sizeDelta = new Vector2(110f, 110f);
        }
        else
        {
            if (textComponent != null)
            {
                textComponent.fontSize = 60;
            }

            if (tmpTextComponent != null)
            {
                tmpTextComponent.fontSize = 60;
            }

            rect.sizeDelta = new Vector2(80f, 80f);
        }
    }
}