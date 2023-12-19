using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextNight : MonoBehaviour {

    private float NightNumber;
    public Text NightNumberDisplayer;

    void Start () {
        NightNumber = PlayerPrefs.GetFloat("NightNumber");
        NightNumberDisplayer.text = NightNumber.ToString();

        StartCoroutine(InitCoroutine());
    }
    
    void Update () {
        Resources.UnloadUnusedAssets();
    }

    IEnumerator InitCoroutine() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Office");
    }
}
