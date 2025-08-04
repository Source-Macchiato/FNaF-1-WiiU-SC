using System.Collections;
using UnityEngine;

public class NextNight : MonoBehaviour
{
    [SerializeField]
    private GameObject nightContainer;
    [SerializeField]
    private I18nTextTranslator nightTextTranslator;
    [SerializeField]
    private Animator nightAnimator;

    private int nightNumber;
    public GameObject loadingScreenPanel;

    private LevelLoader levelLoader;

    void Start()
    {
        nightNumber = SaveManager.saveData.game.nightNumber;

        loadingScreenPanel.SetActive(false);
        levelLoader = FindObjectOfType<LevelLoader>();

        switch (nightNumber)
        {
            case 0:
                nightTextTranslator.textId = "nextnight.firstnight";
                break;
            case 1:
                nightTextTranslator.textId = "nextnight.secondnight";
                break;
            case 2:
                nightTextTranslator.textId = "nextnight.thirdnight";
                break;
            case 3:
                nightTextTranslator.textId = "nextnight.fourthnight";
                break;
            case 4:
                nightTextTranslator.textId = "nextnight.fifthnight";
                break;
            case 5:
                nightTextTranslator.textId = "nextnight.sixthnight";
                break;
            case 6:
                nightTextTranslator.textId = "nextnight.seventhnight";
                break;
            default:
                nightTextTranslator.textId = "nextnight.firstnight";
                break;
        }

        nightTextTranslator.UpdateText();

        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        yield return new WaitForSeconds(3);

        nightAnimator.Play("Fade");

        yield return new WaitForSeconds(1);

        loadingScreenPanel.SetActive(true);
        levelLoader.LoadLevel("Office");
    }
}
