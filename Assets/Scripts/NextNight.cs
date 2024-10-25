using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NextNight : MonoBehaviour {

    public GameObject LoadingSprite;

    public GameObject Night1;
    public GameObject Night2;
    public GameObject Night3;
    public GameObject Night4;
    public GameObject Night5;
    public GameObject Night6;
    private float nightNumber;
    public Text NightNumberDisplayer;
    public GameObject loadingScreenPanel;

    void Start () {
        loadingScreenPanel.SetActive(false);
        nightNumber = SaveManager.LoadNightNumber();
        LoadingSprite.SetActive(false);
        Night1.SetActive(false);
        Night2.SetActive(false);
        Night3.SetActive(false);
        Night4.SetActive(false);
        Night5.SetActive(false);
        Night6.SetActive(false);
    
        if (nightNumber == 0)
        {
            Night1.SetActive(true);
        }
        else if (nightNumber == 1)
        {
            Night2.SetActive(true);
        }
        else if (nightNumber == 2)
        {
            Night3.SetActive(true);
        }
        else if (nightNumber == 3)
        {
            Night4.SetActive(true);
        }
        else if (nightNumber == 4)
        {
            Night5.SetActive(true);
        }
        else if (nightNumber == 5)
        {
            Night6.SetActive(true);
        }

        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine() {
        yield return new WaitForSeconds(5);
        loadingScreenPanel.SetActive(true);
        LoadingSprite.SetActive(true);
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevel("Office");
    }
}
