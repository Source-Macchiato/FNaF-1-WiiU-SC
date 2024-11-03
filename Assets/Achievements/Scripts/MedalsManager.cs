using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WiiU = UnityEngine.WiiU;
using UnityEngine;
using UnityEngine.UI;

public class MedalsManager : MonoBehaviour {

	public static MedalsManager medalsManager;

    public float durationPopup;
    public Image medalPopup;
    public Text mainText;
    public Text descText;
    public Text achievementObtained;
    public Image icon;

    public Animator medalAnim;

    public bool isShowing = false;

    public List<string> obtainedMedals = new List<string>();
    [Header("Things in wait list")]
    public List<string> medalsToShow = new List<string>();
    public List<string> medalsDescToShow = new List<string>();
    public List<Sprite> medalsIconToShow = new List<Sprite>();

    [Header("Dark Mode")]
    public bool darkMode = false;
    public Color normalText;
    public Color darkText;
    public Color normalPopup;
    public Color darkPopup;

    void Awake()
	{
        if (medalsManager != null)
        {
            Destroy(this.gameObject);
            return;
        }
		medalsManager = this;
        DontDestroyOnLoad(this);
	}

	void Start ()
    {
        MedalsSave.inst.Load();
		medalAnim.SetBool("show", false);
        if (darkMode)
        {
            medalPopup.color = darkPopup;
            mainText.color = darkText;
            descText.color = darkText;
            achievementObtained.color = darkText;
        }
        else
        {
            medalPopup.color = normalPopup;
            mainText.color = normalText;
            descText.color = normalText;
            achievementObtained.color = normalText;
        }
    }

    void Update()
    {
        if(obtainedMedals.Count != medalsToShow.Count && obtainedMedals.Count < medalsToShow.Count)
        {
            for(int i = 0; i < medalsToShow.Count; i++)
            {
                ShowAchievement(medalsToShow[i], medalsDescToShow[i], medalsIconToShow[i]);
            }
        }
    }

    public void ShowAchievement(string MainMessage, string Description, [Optional]Sprite medalIcon)
    {
        string match = "";
        foreach (string item in obtainedMedals)
        {
            if (item == MainMessage)
            {
                match = item;
            }
        }
        if (PlayerPrefs.GetString(MainMessage) == string.Empty || PlayerPrefs.GetString(MainMessage) == "" || PlayerPrefs.GetString(MainMessage) == null || match != MainMessage)
        {
            if (!medalsToShow.Contains(MainMessage))
            {
                medalsToShow.Add(MainMessage);
                medalsDescToShow.Add(Description);
                if(!medalIcon)
                    medalsIconToShow.Add(null);
                else
                    medalsIconToShow.Add(medalIcon);
            }
            if (isShowing) return;
            StartCoroutine(ShowAchievementIE(MainMessage, Description, medalIcon));
        }
    }

    private IEnumerator ShowAchievementIE(string MainMessage, string Description, [Optional] Sprite medalIcon)
	{
        isShowing = true;
        PlayerPrefs.SetString(MainMessage, "obtained");
        obtainedMedals.Add(MainMessage);
        MedalsSave.inst.Save();
        mainText.text = MainMessage;
        descText.text = Description;
        if(medalIcon)
            icon.sprite = medalIcon;
        else
            icon.sprite = null;
        medalAnim.SetBool("show", true);
        yield return new WaitForSeconds(durationPopup);
        medalAnim.SetBool("show", false);
    }

    void OnApplicationQuit() //Just in case
    {
        MedalsSave.inst.Save();
    }
}
