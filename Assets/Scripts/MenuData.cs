using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class MenuData : MonoBehaviour
{
    public float nightNumber;
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
    public SwitcherData analyticsSwitcher;

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
        // Get SwitcherData scripts
        SwitcherData[] switchers = FindObjectsOfType<SwitcherData>();

        // Get language
        string language = I18n.GetLanguage();

        foreach (SwitcherData switcher in switchers)
        {
            if (switcher.switcherId == "switcher.translation")
            {
                if (language == "fr")
                {
                    switcher.currentOptionId = 1;
                }
                else if (language == "es")
                {
                    switcher.currentOptionId = 2;
                }
                else if (language == "it")
                {
                    switcher.currentOptionId = 3;
                }
                else if (language == "de")
                {
                    switcher.currentOptionId = 4;
                }
                else if (language == "ar")
                {
                    switcher.currentOptionId = 5;
                }
                else if (language == "sk")
                {
                    switcher.currentOptionId = 6;
                }
                else if (language == "ca")
                {
                    switcher.currentOptionId = 7;
                }
                else if (language == "tr")
                {
                    switcher.currentOptionId = 8;
                }
                else
                {
                    switcher.currentOptionId = 0;
                }
            }
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
        audioMixer.SetFloat("Master", (SaveManager.LoadGeneralVolume() / 10f) * 80f - 80f);
        audioMixer.SetFloat("Music", (SaveManager.LoadMusicVolume() / 10f) * 80f - 80f);
        audioMixer.SetFloat("Voice", (SaveManager.LoadVoiceVolume() / 10f) * 80f - 80f);
        audioMixer.SetFloat("SFX", (SaveManager.LoadSFXVolume() / 10f) * 80f - 80f);

    }

    public void UpdateVolumeSwitchers()
    {
        // Get SwitcherData scripts
        SwitcherData[] switchers = FindObjectsOfType<SwitcherData>();

        foreach (SwitcherData switcher in switchers)
        {
            if (switcher.switcherId == "switcher.generalvolume")
            {
                switcher.currentOptionId = SaveManager.LoadGeneralVolume();
            }
            else if (switcher.switcherId == "switcher.musicvolume")
            {
                switcher.currentOptionId = SaveManager.LoadMusicVolume();
            }
            else if (switcher.switcherId == "switcher.voicevolume")
            {
                switcher.currentOptionId = SaveManager.LoadVoiceVolume();
            }
            else if (switcher.switcherId == "switcher.sfxvolume")
            {
                switcher.currentOptionId = SaveManager.LoadSFXVolume();
            }
        }
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

    public void SaveAndUpdateVolume()
    {
        // Get SwitcherData scripts
        SwitcherData[] switchers = FindObjectsOfType<SwitcherData>();

        foreach (SwitcherData switcher in switchers)
        {
            if (switcher.switcherId == "switcher.generalvolume")
            {
                saveManager.SaveGeneralVolume(switcher.currentOptionId);
                bool saveResult = saveGameState.DoSave();

                audioMixer.SetFloat("Master", (switcher.currentOptionId / 10f) * 80f - 80f);
            }
            else if (switcher.switcherId == "switcher.musicvolume")
            {
                saveManager.SaveMusicVolume(switcher.currentOptionId);
                bool saveResult = saveGameState.DoSave();

                audioMixer.SetFloat("Music", (switcher.currentOptionId / 10f) * 80f - 80f);
            }
            else if (switcher.switcherId == "switcher.voicevolume")
            {
                saveManager.SaveVoiceVolume(switcher.currentOptionId);
                bool saveResult = saveGameState.DoSave();

                audioMixer.SetFloat("Voice", (switcher.currentOptionId / 10f) * 80f - 80f);
            }
            else if (switcher.switcherId == "switcher.sfxvolume")
            {
                saveManager.SaveSFXVolume(switcher.currentOptionId);
                bool saveResult = saveGameState.DoSave();

                audioMixer.SetFloat("SFX", (switcher.currentOptionId / 10f) * 80f - 80f);
            }
        }
    }

    public void LoadShareDataAndUpdateSwitcher()
    {
        // Get SwitcherData scripts
        SwitcherData[] switchers = FindObjectsOfType<SwitcherData>();

        // Get share data value
        int shareDataId = SaveManager.LoadShareAnalytics();

        foreach (SwitcherData switcher in switchers)
        {
            if (switcher.switcherId == "switcher.analyticdata")
            {
                if (shareDataId == 1f)
                {
                    switcher.currentOptionId = 1;
                }
                else
                {
                    switcher.currentOptionId = 0;
                }
            }
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
}