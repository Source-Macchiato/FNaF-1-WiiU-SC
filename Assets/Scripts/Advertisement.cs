using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Advertisement : MonoBehaviour {

    public GameObject Image;

    // Use this for initialization
    void Start () {
        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine() {
        Resources.UnloadUnusedAssets();

        yield return new WaitForSeconds(10f);

        Image.GetComponent<Animator>().enabled = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Office");
    }
}
