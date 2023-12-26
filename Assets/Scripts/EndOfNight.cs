using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfNight : MonoBehaviour {

    public GameObject Children;
    public float NightNumber;

    void Start () {
        StartCoroutine(InitCoroutine());

        NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);
    }

    IEnumerator InitCoroutine() {
        yield return new WaitForSeconds(6);

        Children.SetActive(true);

        yield return new WaitForSeconds(5);

        if (NightNumber < 5) {
            SceneManager.LoadScene("NextNight");
        } else if (NightNumber == 6) {
            SceneManager.LoadScene("TheEnd");
        }
    }
}
