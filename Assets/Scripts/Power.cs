using UnityEngine;
using UnityEngine.SceneManagement;

public class Power : MonoBehaviour {

    public GameObject FreddySongDarkOffice;
    public GameObject freddyJumpScare;
    public GameObject Song;

    //---FLOATS---------------------------
    public float WaitBeforeStart = 20f;
    public float PlayTime = 15f;
    public float JumpscarePlayTime = 0.2f;

    private ControllersRumble controllersRumble;

    void Start()
    {
        controllersRumble = FindObjectOfType<ControllersRumble>();
    }

    void Update()
    {
        WaitBeforeStart -= Time.deltaTime;

        if (WaitBeforeStart <= 0)
        {
            Song.SetActive(true);
            FreddySongDarkOffice.GetComponent<Animator>().enabled = true;
            PlayTime -= Time.deltaTime;
            WaitBeforeStart = 0;

            if (PlayTime <= 0)
            {
                FreddySongDarkOffice.GetComponent<Animator>().enabled = false;
                FreddySongDarkOffice.SetActive(false);

                freddyJumpScare.SetActive(true);
                PlayTime = 0;

                JumpscarePlayTime -= Time.deltaTime;

                controllersRumble.IsRumbleTriggered("No power");

                if (JumpscarePlayTime <= 0)
                {
                    SceneManager.LoadScene("GameOver");
                    JumpscarePlayTime = 0;
                }
            }
        }
    }
}
