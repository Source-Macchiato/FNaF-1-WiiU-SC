using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class CameraScript : MonoBehaviour
{
    public bool camIsUp = false;

    public float wait = 0.2f;

    public GameObject CamSelectPanel;
    public GameObject OfficeStuff;
    public GameObject Black;
    public AudioSource FlipOpen;
    public AudioSource FlipClose;
    public GameObject CamViewTabletOpen;
    public GameObject CamViewTabletClose;

    public GameObject OfficeControllerObject;

    public GameObject Dot;
    public GameObject Glitch;
    public GameObject Stripes;

    public GameObject ResetPoint;

    WiiU.GamePad gamePad;

    void Start()
    {
        gamePad = WiiU.GamePad.access;
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;

        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsTriggered(WiiU.GamePadButton.L))
            {
                if (camIsUp)
                {
                    CamSelectPanel.SetActive(false);
                    OfficeStuff.SetActive(true);

                    FlipClose.Play();

                    CamViewTabletClose.SetActive(true);
                    CamViewTabletOpen.SetActive(false);

                    Dot.SetActive(false);
                    Glitch.SetActive(false);
                    Stripes.SetActive(false);

                    camIsUp = false;

                    wait = 0.2f;

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;
                    OfficeControllerObject.GetComponent<Office>().enabled = true;
                    OfficeControllerObject.GetComponent<Movement>().camIsUp = false;
                    OfficeControllerObject.GetComponent<ChangeImages>().camIsUp = false;
                    OfficeControllerObject.GetComponent<RandNumberGen>().camIsUp = false;
                    OfficeControllerObject.GetComponent<ChangeImages>().enabled = false;

                }
                else
                {
                    CamSelectPanel.SetActive(true);
                    OfficeStuff.SetActive(false);

                    FlipOpen.Play();
                    CamViewTabletOpen.SetActive(true);
                    CamViewTabletClose.SetActive(false);

                    Dot.SetActive(true);
                    Glitch.SetActive(true);
                    Stripes.SetActive(true);

                    camIsUp = true;

                    wait = 0.2f;

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;
                    OfficeControllerObject.GetComponent<Office>().enabled = false;
                    OfficeControllerObject.GetComponent<Office>().centerPosition = 0;
                    OfficeControllerObject.GetComponent<Movement>().camIsUp = true;
                    OfficeControllerObject.GetComponent<ChangeImages>().camIsUp = true;
                    OfficeControllerObject.GetComponent<RandNumberGen>().camIsUp = true;
                    OfficeControllerObject.GetComponent<ChangeImages>().enabled = true;

                    OfficeStuff.transform.position = ResetPoint.transform.position;
                }
            }
        }

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (camIsUp)
                {
                    CamSelectPanel.SetActive(false);
                    OfficeStuff.SetActive(true);

                    FlipClose.Play();

                    CamViewTabletClose.SetActive(true);
                    CamViewTabletOpen.SetActive(false);

                    Dot.SetActive(false);
                    Glitch.SetActive(false);
                    Stripes.SetActive(false);

                    camIsUp = false;

                    wait = 0.2f;

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;
                    OfficeControllerObject.GetComponent<Office>().enabled = true;
                    OfficeControllerObject.GetComponent<Movement>().camIsUp = false;
                    OfficeControllerObject.GetComponent<ChangeImages>().camIsUp = false;
                    OfficeControllerObject.GetComponent<RandNumberGen>().camIsUp = false;
                    OfficeControllerObject.GetComponent<ChangeImages>().enabled = false;

                }
                else
                {
                    CamSelectPanel.SetActive(true);
                    OfficeStuff.SetActive(false);

                    FlipOpen.Play();
                    CamViewTabletOpen.SetActive(true);
                    CamViewTabletClose.SetActive(false);

                    Dot.SetActive(true);
                    Glitch.SetActive(true);
                    Stripes.SetActive(true);

                    camIsUp = true;

                    wait = 0.2f;

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;
                    OfficeControllerObject.GetComponent<Office>().enabled = false;
                    OfficeControllerObject.GetComponent<Office>().centerPosition = 0;
                    OfficeControllerObject.GetComponent<Movement>().camIsUp = true;
                    OfficeControllerObject.GetComponent<ChangeImages>().camIsUp = true;
                    OfficeControllerObject.GetComponent<RandNumberGen>().camIsUp = true;
                    OfficeControllerObject.GetComponent<ChangeImages>().enabled = true;

                    OfficeStuff.transform.position = ResetPoint.transform.position;
                }
            }
        }

        if (camIsUp)
        {
            wait -= Time.deltaTime;

            if (wait <= 0)
            {
                RemoveAnimator();

                wait = 0.2f;

                Black.SetActive(true);
            }
        }

        if (!camIsUp)
        {
            wait -= Time.deltaTime;

            if (wait <= 0)
            {
                RemoveAnimator();

                wait = 0.2f;

                Black.SetActive(false);
            }
        }


    }

    void RemoveAnimator()
    {
        if (camIsUp)
        {
            if (CamViewTabletOpen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Flip"))
            {
                CamViewTabletOpen.SetActive(false);
            }
        }

        if (!camIsUp)
        {
            if (CamViewTabletClose.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Close"))
            {
                CamViewTabletClose.SetActive(false);
            }
        }
    }

}
