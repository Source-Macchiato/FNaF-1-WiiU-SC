using System.Collections;
using UnityEngine;

public class NextNight : MonoBehaviour
{
    [Header("Nights")]
    public GameObject Night1;
    public GameObject Night2;
    public GameObject Night3;
    public GameObject Night4;
    public GameObject Night5;
    public GameObject Night6;
    public GameObject Night7;

    [Header("Loading")]
    private float nightNumber;
    public GameObject loadingScreenPanel;

    private LevelLoader levelLoader;

    void Start()
    {
        nightNumber = SaveManager.LoadNightNumber();

        loadingScreenPanel.SetActive(false);
        levelLoader = FindObjectOfType<LevelLoader>();

        Night1.SetActive(false);
        Night2.SetActive(false);
        Night3.SetActive(false);
        Night4.SetActive(false);
        Night5.SetActive(false);
        Night6.SetActive(false);
        Night7.SetActive(false);
    
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
        else if (nightNumber == 6)
        {
            Night7.SetActive(true);
        }

        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        yield return new WaitForSeconds(5);
        loadingScreenPanel.SetActive(true);
        levelLoader.LoadLevel("Office");
    }
}
