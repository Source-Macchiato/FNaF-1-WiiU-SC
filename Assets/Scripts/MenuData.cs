using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MenuData : MonoBehaviour
{
    public int nightNumber;
    public GameObject nightNumberContainer;
    public TextMeshProUGUI nightNumberText;
    public GameObject continueButtonGameObject;
    public GameObject gameTitle;
    public int layoutId;

    [Header("Custom Night")]
    public GameObject[] customNightCharacters;
    public GameObject customNightContainer;
    public GameObject customNightBackground;
    public GameObject goldenFreddyGameObject;
    public Button readyButton;

    [Header("Switchers")]
    public SwitcherData languageSwitcher;
    public SwitcherData analyticsSwitcher;
    public SwitcherData generalVolumeSwitcher;
    public SwitcherData musicVolumeSwitcher;
    public SwitcherData voiceVolumeSwitcher;
    public SwitcherData sfxVolumeSwitcher;
    public SwitcherData motionSwitcher;
    public SwitcherData pointerSwitcher;

    [Header("Other")]
    public GameObject starsContainer;
    
    public AudioSource mainMenuThemeSound;
    public AudioMixer audioMixer;

    [HideInInspector]
    public GameObject nightNumberGameObject;
    public Button[] layoutButtons;

    // Scripts
    SaveGameState saveGameState;
    SaveManager saveManager;
    MenuManager menuManager;

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
        menuManager = FindObjectOfType<MenuManager>();

        // Load
        nightNumber = SaveManager.LoadNightNumber();
        layoutId = SaveManager.LoadLayoutId();
        LoadVolume();

        // Disable advertisement by default
        advertisementIsActive = false;
        advertisementImage.SetActive(false);

        // System for display night number and prevent being out of range
        if (nightNumber >= 0 && nightNumber <= 4) // If is between night 1 and night 5
        {
            nightNumberText.text = (nightNumber + 1).ToString();
        }
        else if (nightNumber >= 5) // If is night 6 or more
        {
            nightNumberText.text = "5";
        }
        else // Or if is another value, in the case less than night 1
        {
            nightNumberText.text = "1";
        }
    }
	
	// Update is called once per frame
	void Update()
    {
        // Load scene after advertisementload is called
        if (Time.time - startTime >= waitTime && advertisementIsActive == true)
        {
            SceneManager.LoadScene("NextNight");
        }

        // Enable and disable stars
        starsContainer.SetActive(menuManager.currentMenuId == 0);

        // Display night text only when continue button is selected
        nightNumberContainer.SetActive(EventSystem.current.currentSelectedGameObject == continueButtonGameObject);
    }

    public void ToggleGameTitle(bool visibility)
    {
        gameTitle.SetActive(visibility);
    }

    public void LoadAdvertisement()
    {
        advertisementIsActive = true;
        startTime = Time.time;
        advertisementImage.SetActive(true);
    }

    public void LoadLanguageAndUpdateSwitcher()
    {
        string language = I18n.GetLanguage();

        if (language == "fr")
        {
            languageSwitcher.currentOptionId = 1;
        }
        else if (language == "es")
        {
            languageSwitcher.currentOptionId = 2;
        }
        else if (language == "it")
        {
            languageSwitcher.currentOptionId = 3;
        }
        else if (language == "de")
        {
            languageSwitcher.currentOptionId = 4;
        }
        else if (language == "ar")
        {
            languageSwitcher.currentOptionId = 5;
        }
        else if (language == "sk")
        {
            languageSwitcher.currentOptionId = 6;
        }
        else if (language == "ca")
        {
            languageSwitcher.currentOptionId = 7;
        }
        else if (language == "tr")
        {
            languageSwitcher.currentOptionId = 8;
        }
        else
        {
            languageSwitcher.currentOptionId = 0;
        }
    }

    public void SaveAndUpdateAnalytics()
    {
        // Get SwitcherData scripts
        saveManager.SaveShareAnalytics(analyticsSwitcher.currentOptionId == 1 ? 0 : 1);
        bool saveResult = saveGameState.DoSave();

        //analyticsData.CanShareAnalytics();
    }

    public void LoadAnalyticsAndUpdateSwitcher()
    {
        // Get share analytics
        int shareAnalytics = SaveManager.LoadShareAnalytics();

        int switcherIndex = shareAnalytics == 1 ? 0 : 1;

        if (switcherIndex >= 0 && switcherIndex < analyticsSwitcher.optionsName.Length)
        {
            analyticsSwitcher.currentOptionId = switcherIndex;
            analyticsSwitcher.UpdateText();
        }
    }

    private void LoadVolume()
    {
        audioMixer.SetFloat("Master", ConvertToDecibel(SaveManager.LoadGeneralVolume()));
        audioMixer.SetFloat("Music", ConvertToDecibel(SaveManager.LoadMusicVolume()));
        audioMixer.SetFloat("Voice", ConvertToDecibel(SaveManager.LoadVoiceVolume()));
        audioMixer.SetFloat("SFX", ConvertToDecibel(SaveManager.LoadSFXVolume()));

    }

    public void UpdateVolumeSwitchers()
    {
        generalVolumeSwitcher.currentOptionId = SaveManager.LoadGeneralVolume();
        musicVolumeSwitcher.currentOptionId = SaveManager.LoadMusicVolume();
        voiceVolumeSwitcher.currentOptionId = SaveManager.LoadVoiceVolume();
        sfxVolumeSwitcher.currentOptionId = SaveManager.LoadSFXVolume();
    }

    public void SaveAndUpdateLanguage()
    {
        saveManager.SaveLanguage(languageSwitcher.optionsName[languageSwitcher.currentOptionId]);
        bool saveResult = saveGameState.DoSave();

        // Reload the language
        I18n.LoadLanguage();
    }

    public void SaveAndUpdateVolume()
    {
        // Save and apply general volume
        saveManager.SaveGeneralVolume(generalVolumeSwitcher.currentOptionId);
        audioMixer.SetFloat("Master", ConvertToDecibel(generalVolumeSwitcher.currentOptionId));

        // Save and apply music volume
        saveManager.SaveMusicVolume(musicVolumeSwitcher.currentOptionId);
        audioMixer.SetFloat("Music", ConvertToDecibel(musicVolumeSwitcher.currentOptionId));

        // Save and apply voice volume
        saveManager.SaveVoiceVolume(voiceVolumeSwitcher.currentOptionId);
        audioMixer.SetFloat("Voice", ConvertToDecibel(voiceVolumeSwitcher.currentOptionId));

        // Save and apply SFX volume
        saveManager.SaveSFXVolume(sfxVolumeSwitcher.currentOptionId);
        audioMixer.SetFloat("SFX", ConvertToDecibel(sfxVolumeSwitcher.currentOptionId));

        bool saveResult = saveGameState.DoSave();
    }

    public void LoadShareDataAndUpdateSwitcher()
    {
        // Get share data value
        int shareDataId = SaveManager.LoadShareAnalytics();

        analyticsSwitcher.currentOptionId = (shareDataId == 1) ? 1 : 0;
    }

    public void SaveMotionControls()
    {
        saveManager.SaveMotionControls(motionSwitcher.currentOptionId == 0);
        bool saveResult = saveGameState.DoSave();
    }

    public void LoadMotionControls()
    {
        // Get motion controls status
        bool motionControls = SaveManager.LoadMotionControls();

        int switcherIndex = motionControls ? 0 : 1;

        if (switcherIndex >= 0 && switcherIndex < motionSwitcher.optionsName.Length)
        {
            motionSwitcher.currentOptionId = switcherIndex;
            motionSwitcher.UpdateText();
        }
    }

    public void SavePointerVisibility()
    {
        saveManager.SavePointerVisibility(pointerSwitcher.currentOptionId == 0);
        bool saveResult = saveGameState.DoSave();
    }

    public void LoadPointerVisibility()
    {
        bool pointerVisibility = SaveManager.LoadPointerVisibility();

        int switcherIndex = pointerVisibility ? 0 : 1;

        if (switcherIndex >= 0 && switcherIndex < pointerSwitcher.optionsName.Length)
        {
            pointerSwitcher.currentOptionId = switcherIndex;
            pointerSwitcher.UpdateText();
        }
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

    public void CustomNightBackgroundStatus(bool status)
    {
        customNightBackground.SetActive(status);
    }

    public void GoldenFreddy(bool status)
    {
        if (status)
        {
            HorizontalLayoutGroup horizontalLayoutGroup = customNightContainer.GetComponent<HorizontalLayoutGroup>();
            horizontalLayoutGroup.spacing = 71;

            customNightCharacters[4].SetActive(true);

            Navigation navigationFoxyButton = customNightCharacters[3].GetComponent<Button>().navigation;
            navigationFoxyButton.selectOnRight = customNightCharacters[4].GetComponent<Button>();
            customNightCharacters[3].GetComponent<Button>().navigation = navigationFoxyButton;

            Navigation navigationReadyButton = readyButton.navigation;
            navigationReadyButton.selectOnUp = customNightCharacters[4].GetComponent<Button>();
            readyButton.navigation = navigationReadyButton;
        }
        else
        {
            HorizontalLayoutGroup horizontalLayoutGroup = customNightContainer.GetComponent<HorizontalLayoutGroup>();
            horizontalLayoutGroup.spacing = 120;

            customNightCharacters[4].SetActive(false);

            Navigation navigationFoxyButton = customNightCharacters[3].GetComponent<Button>().navigation;
            navigationFoxyButton.selectOnRight = null;
            customNightCharacters[3].GetComponent<Button>().navigation = navigationFoxyButton;

            Navigation navigationReadyButton = readyButton.navigation;
            navigationReadyButton.selectOnUp = customNightCharacters[3].GetComponent<Button>();
            readyButton.navigation = navigationReadyButton;
        }
    }

    public void ActivateGoldenFreddyJumpscare()
    {
        StartCoroutine(GoldenFreddyJumpscareAnimation());
    }

    private IEnumerator GoldenFreddyJumpscareAnimation()
    {
        menuManager.canNavigate = false;
        goldenFreddyGameObject.SetActive(true);
        mainMenuThemeSound.mute = true;

        yield return new WaitForSeconds(1f);

        goldenFreddyGameObject.SetActive(false);
        mainMenuThemeSound.mute = false;
        GoldenFreddy(true);
        menuManager.canNavigate = true;
        menuManager.GoBack();
    }

    public void SaveLayout()
    {
        saveManager.SaveLayoutId(layoutId);
        bool saveResult = saveGameState.DoSave();
    }

    public void SaveNightNumber()
    {
        saveManager.SaveNightNumber(nightNumber);
        bool saveResult = saveGameState.DoSave();
    }

    public void SaveCustomNightValues()
    {
        PlayerPrefs.SetInt("FreddyDifficulty", customNightCharacters[0].GetComponent<CardSwitcherData>().difficultyValue);
        PlayerPrefs.SetInt("BonnieDifficulty", customNightCharacters[1].GetComponent<CardSwitcherData>().difficultyValue);
        PlayerPrefs.SetInt("ChicaDifficulty", customNightCharacters[2].GetComponent<CardSwitcherData>().difficultyValue);
        PlayerPrefs.SetInt("FoxyDifficulty", customNightCharacters[3].GetComponent<CardSwitcherData>().difficultyValue);

        if (customNightCharacters[4].activeSelf)
        {
            PlayerPrefs.SetInt("GoldenDifficulty", customNightCharacters[4].GetComponent<CardSwitcherData>().difficultyValue);
        }

        PlayerPrefs.Save();
    }

    public void SaveGoldenFreddy()
    {
        saveManager.SaveGoldenFreddyStatus(1);
        bool saveResult = saveGameState.DoSave();
    }

    public void DisplaySelectedLayoutButton()
    {
        // Dictionary to map cardId to layoutId
        Dictionary<string, int> cardLayoutMapping = new Dictionary<string, int>();
        cardLayoutMapping.Add("card.tvonly", 0);
        cardLayoutMapping.Add("card.tvgamepadclassic", 1);
        cardLayoutMapping.Add("card.tvgamepadalternative", 2);
        cardLayoutMapping.Add("card.gamepadonly", 3);

        // Get all CardData scripts
        CardData[] cards = FindObjectsOfType<CardData>();

        foreach (CardData card in cards)
        {
            // Check if cardId matches layoutId
            if (cardLayoutMapping.ContainsKey(card.cardId) && cardLayoutMapping[card.cardId] == layoutId)
            {
                Button button = card.GetComponent<Button>();
                if (button != null)
                {
                    button.Select();
                }
            }
        }
    }

    public void SelectLayoutButton()
    {
        // Dictionary to map cardId to layoutId
        Dictionary<string, int> cardLayoutMapping = new Dictionary<string, int>();
        cardLayoutMapping.Add("card.tvonly", 0);
        cardLayoutMapping.Add("card.tvgamepadclassic", 1);
        cardLayoutMapping.Add("card.tvgamepadalternative", 2);
        cardLayoutMapping.Add("card.gamepadonly", 3);

        string cardId = EventSystem.current.currentSelectedGameObject.GetComponent<CardData>().cardId;

        layoutId = cardLayoutMapping[cardId];

        // Save layout id
        saveManager.SaveLayoutId(layoutId);
        bool saveResult = saveGameState.DoSave();
    }

    private float ConvertToDecibel(int volume)
    {
        // Convert volume (0-10) to decibels (-80dB to 0dB)
        return Mathf.Log10(Mathf.Max(volume / 10f, 0.0001f)) * 20f;
    }
}