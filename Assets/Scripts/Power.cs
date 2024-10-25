using UnityEngine;
using UnityEngine.SceneManagement;

public class Power : MonoBehaviour {

    public GameObject FreddySongDarkOffice;
    public GameObject freddyJumpScare;
    public GameObject Blink;
    public GameObject Song;
    public GameObject BlinkAudio;

    //---FLOATS---------------------------
    public float WaitBeforeStart = 20f;
    public float PlayTime; // Removed the fixed value to assign it randomly
    public float DarkTime; // random
    public float JumpscarePlayTime = 0.2f;

    private ControllersRumble controllersRumble;

    void Start()
    {
        Blink.SetActive(false);
        controllersRumble = FindObjectOfType<ControllersRumble>();

        // Assign PlayTime a random value between 15 and 28 seconds
        PlayTime = Random.Range(15f, 28f);
        //Asign a random value to draktime
        DarkTime = Random.Range(5f, 10f);
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
                //active Blink Image and start countdown and end the song
                Blink.SetActive(true);
                DarkTime -= Time.deltaTime;
                Song.SetActive(false);
                BlinkAudio.SetActive(true);

                //disable freddy animation
                FreddySongDarkOffice.GetComponent<Animator>().enabled = false;

                if (DarkTime <= 0)
                {
                    freddyJumpScare.SetActive(true);
                    PlayTime = 0;
                    JumpscarePlayTime -= Time.deltaTime;

                    controllersRumble.IsRumbleTriggered("No power");
                }
                if (JumpscarePlayTime <= 0)
                {
                    SceneManager.LoadScene("GameOver");
                    JumpscarePlayTime = 0;
                }
            }
        }
    }
}
