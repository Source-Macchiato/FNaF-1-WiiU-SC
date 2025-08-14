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
    public SwitcherData panoramaEffectSwitcher;
    public SwitcherData subtitlesSwitcher;

    [Header("Other")]
    public GameObject starsContainer;
    
    public AudioSource mainMenuThemeSound;
    public AudioMixer audioMixer;

    [HideInInspector]
    public GameObject nightNumberGameObject;
    public Button[] layoutButtons;

    // Scripts
    MenuManager menuManager;
    ControllersRumble controllersRumble;

    // Advertisement
    public GameObject advertisementImage;

    void Start()
    {
        // Get scripts
        menuManager = FindObjectOfType<MenuManager>();
        controllersRumble = FindObjectOfType<ControllersRumble>();

        // Load
        nightNumber = SaveManager.saveData.game.nightNumber;
        layoutId = SaveManager.saveData.settings.layoutId;
        LoadVolume();

        // Disable advertisement by default
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
        // Enable and disable stars
        starsContainer.SetActive(menuManager.currentMenuId == 0);

        // Display night text only when continue button is selected
        nightNumberContainer.SetActive(EventSystem.current.currentSelectedGameObject == continueButtonGameObject);
    }

    public void ToggleGameTitle(bool visibility)
    {
        gameTitle.SetActive(visibility);
    }

    public IEnumerator LoadAdvertisement()
    {
        advertisementImage.SetActive(true);

        yield return new WaitForSeconds(10f);

        SceneManager.LoadSceneAsync("NextNight");
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
        SaveManager.saveData.settings.shareAnalytics = analyticsSwitcher.currentOptionId == 1 ? 0 : 1;
        SaveManager.Save();

        AnalyticsData.analyticsData.CanShareAnalytics();
    }

    public void LoadAnalyticsAndUpdateSwitcher()
    {
        // Get share analytics
        int shareAnalytics = SaveManager.saveData.settings.shareAnalytics;

        int switcherIndex = shareAnalytics == 1 ? 0 : 1;

        if (switcherIndex >= 0 && switcherIndex < analyticsSwitcher.optionsName.Length)
        {
            analyticsSwitcher.currentOptionId = switcherIndex;
            analyticsSwitcher.UpdateText();
        }
    }

    private void LoadVolume()
    {
        audioMixer.SetFloat("Master", ConvertToDecibel(SaveManager.saveData.settings.volume.generalVolume));
        audioMixer.SetFloat("Music", ConvertToDecibel(SaveManager.saveData.settings.volume.musicVolume));
        audioMixer.SetFloat("Voice", ConvertToDecibel(SaveManager.saveData.settings.volume.voiceVolume));
        audioMixer.SetFloat("SFX", ConvertToDecibel(SaveManager.saveData.settings.volume.sfxVolume));
    }

    public void UpdateVolumeSwitchers()
    {
        generalVolumeSwitcher.currentOptionId = SaveManager.saveData.settings.volume.generalVolume;
        musicVolumeSwitcher.currentOptionId = SaveManager.saveData.settings.volume.musicVolume;
        voiceVolumeSwitcher.currentOptionId = SaveManager.saveData.settings.volume.voiceVolume;
        sfxVolumeSwitcher.currentOptionId = SaveManager.saveData.settings.volume.sfxVolume;
    }

    public void SaveAndUpdateLanguage()
    {
        SaveManager.saveData.settings.language = languageSwitcher.optionsName[languageSwitcher.currentOptionId];
        SaveManager.Save();

        // Reload the language
        I18n.LoadLanguage();
    }

    public void SaveAndUpdateVolume()
    {
        // Save and apply general volume
        SaveManager.saveData.settings.volume.generalVolume = generalVolumeSwitcher.currentOptionId;
        audioMixer.SetFloat("Master", ConvertToDecibel(generalVolumeSwitcher.currentOptionId));

        // Save and apply music volume
        SaveManager.saveData.settings.volume.musicVolume = musicVolumeSwitcher.currentOptionId;
        audioMixer.SetFloat("Music", ConvertToDecibel(musicVolumeSwitcher.currentOptionId));

        // Save and apply voice volume
        SaveManager.saveData.settings.volume.voiceVolume = voiceVolumeSwitcher.currentOptionId;
        audioMixer.SetFloat("Voice", ConvertToDecibel(voiceVolumeSwitcher.currentOptionId));

        // Save and apply SFX volume
        SaveManager.saveData.settings.volume.sfxVolume = sfxVolumeSwitcher.currentOptionId;
        audioMixer.SetFloat("SFX", ConvertToDecibel(sfxVolumeSwitcher.currentOptionId));

        SaveManager.Save();
    }

    public void LoadShareDataAndUpdateSwitcher()
    {
        // Get share data value
        int shareDataId = SaveManager.saveData.settings.shareAnalytics;

        analyticsSwitcher.currentOptionId = (shareDataId == 1) ? 1 : 0;
    }

    public void SaveMotionControls()
    {
        SaveManager.saveData.settings.motionControls = motionSwitcher.currentOptionId == 0;
        SaveManager.Save();
    }

    public void LoadMotionControls()
    {
        // Get motion controls status
        bool motionControls = SaveManager.saveData.settings.motionControls;

        int switcherIndex = motionControls ? 0 : 1;

        if (switcherIndex >= 0 && switcherIndex < motionSwitcher.optionsName.Length)
        {
            motionSwitcher.currentOptionId = switcherIndex;
            motionSwitcher.UpdateText();
        }
    }

    // Pointer visibility
    public void SavePointerVisibility()
    {
        SaveManager.saveData.settings.pointerVisibility = pointerSwitcher.currentOptionId == 0;
        SaveManager.Save();
    }

    public void LoadPointerVisibility()
    {
        bool pointerVisibility = SaveManager.saveData.settings.pointerVisibility;

        int switcherIndex = pointerVisibility ? 0 : 1;

        if (switcherIndex >= 0 && switcherIndex < pointerSwitcher.optionsName.Length)
        {
            pointerSwitcher.currentOptionId = switcherIndex;
            pointerSwitcher.UpdateText();
        }
    }

    // Panorama effect
    public void SavePanoramaEffect()
    {
        SaveManager.saveData.settings.panoramaEffect = panoramaEffectSwitcher.currentOptionId == 0;
        SaveManager.Save();
    }

    public void LoadPanoramaEffect()
    {
        bool panoramaEffect = SaveManager.saveData.settings.panoramaEffect;

        int switcherIndex = panoramaEffect ? 0 : 1;

        if (switcherIndex >= 0 && switcherIndex < panoramaEffectSwitcher.optionsName.Length)
        {
            panoramaEffectSwitcher.currentOptionId = switcherIndex;
            panoramaEffectSwitcher.UpdateText();
        }
    }

    public void SaveSubtitlesStatus()
    {
        SaveManager.saveData.settings.subtitlesEnabled = subtitlesSwitcher.currentOptionId == 0;
        SaveManager.Save();
    }

    public void LoadSubtitlesStatus()
    {
        bool subtitlesStatus = SaveManager.saveData.settings.subtitlesEnabled;

        int switcherIndex = subtitlesStatus ? 0 : 1;

        if (switcherIndex >= 0 && switcherIndex < subtitlesSwitcher.optionsName.Length)
        {
            subtitlesSwitcher.currentOptionId = switcherIndex;
            subtitlesSwitcher.UpdateText();
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

    public void GoldenFreddyEnabled(bool status)
    {
        if (status)
        {
            HorizontalLayoutGroup horizontalLayoutGroup = customNightContainer.GetComponent<HorizontalLayoutGroup>();
            horizontalLayoutGroup.spacing = 71;

            customNightCharacters[4].SetActive(true);
            customNightCharacters[4].GetComponent<Button>().interactable = true;

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
            customNightCharacters[4].GetComponent<Button>().interactable = false;

            Navigation navigationFoxyButton = customNightCharacters[3].GetComponent<Button>().navigation;
            navigationFoxyButton.selectOnRight = null;
            customNightCharacters[3].GetComponent<Button>().navigation = navigationFoxyButton;

            Navigation navigationReadyButton = readyButton.navigation;
            navigationReadyButton.selectOnUp = customNightCharacters[3].GetComponent<Button>();
            readyButton.navigation = navigationReadyButton;
        }
    }

    public IEnumerator ActivateGoldenFreddyJumpscare()
    {
        menuManager.canNavigate = false;
        goldenFreddyGameObject.SetActive(true);
        mainMenuThemeSound.mute = true;

        SaveManager.saveData.game.goldenFreddyUnlocked = true;
        SaveManager.saveData.UnlockAchievement(Achievements.achievements.THEBYTEOF87);
        SaveManager.Save();

        controllersRumble.TriggerRumble(15, "Activate Golden Freddy");

        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => SaveGameState.saveResult != -1);

        Application.Quit();
    }

    public void SaveLayout()
    {
        SaveManager.saveData.settings.layoutId = layoutId;
        SaveManager.Save();
    }

    public void ApplyCustomNightValues()
    {
        Movement.freddyDifficulty = customNightCharacters[0].GetComponent<CardSwitcherData>().difficultyValue;
        Movement.bonnieDifficulty = customNightCharacters[1].GetComponent<CardSwitcherData>().difficultyValue;
        Movement.chicaDifficulty = customNightCharacters[2].GetComponent<CardSwitcherData>().difficultyValue;
        Movement.foxyDifficulty = customNightCharacters[3].GetComponent<CardSwitcherData>().difficultyValue;

        if (SaveManager.saveData.game.goldenFreddyUnlocked)
        {
            Movement.goldenDifficulty = customNightCharacters[4].GetComponent<CardSwitcherData>().difficultyValue;
        }
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
        SaveManager.saveData.settings.layoutId = layoutId;
        SaveManager.Save();
    }

    private float ConvertToDecibel(int volume)
    {
        // Convert volume (0-10) to decibels (-80dB to 0dB)
        return Mathf.Log10(Mathf.Max(volume / 10f, 0.0001f)) * 20f;
    }
}