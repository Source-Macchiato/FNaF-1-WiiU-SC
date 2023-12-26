using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NextNight : MonoBehaviour {

    private float NightNumber;
    public Text NightNumberDisplayer;
    public GameObject loadingScreenPanel;

    void Start () {
        loadingScreenPanel.SetActive(false);
        NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);
        NightNumberDisplayer.text = NightNumber.ToString();

        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine() {
        yield return new WaitForSeconds(5);
        loadingScreenPanel.SetActive(true);
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevel("Office");
    }
}
