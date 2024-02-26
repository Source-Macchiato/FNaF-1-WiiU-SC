using UnityEngine;
using WiiU = UnityEngine.WiiU;
using UnityEngine.UI;
public class CameraScript : MonoBehaviour
{
    public AudioSource Boop;
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
    public Office officescript;
    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    void Start()
    {
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        // Gamepad
        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsTriggered(WiiU.GamePadButton.L))
            {
                if (camIsUp)
                {
                    CamSelectPanel.SetActive(false);
                    OfficeStuff.SetActive(true);
                    foreach (Animator anim in FindObjectsOfType<Animator>())
                    {
                        if (anim.gameObject.name.Contains("Door"))
                        {
                            anim.enabled = false;
                        }
                    }
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
                    //OfficeStuff.SetActive(false);

                    FlipOpen.Play();
                    CamViewTabletOpen.SetActive(true);
                    CamViewTabletClose.SetActive(false);

                    camIsUp = true;

                    wait = 0.2f;

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;
                    //OfficeControllerObject.GetComponent<Office>().enabled = false;
                    OfficeControllerObject.GetComponent<Office>().centerPosition = 0;
                    OfficeControllerObject.GetComponent<Movement>().camIsUp = true;
                    OfficeControllerObject.GetComponent<ChangeImages>().camIsUp = true;
                    OfficeControllerObject.GetComponent<RandNumberGen>().camIsUp = true;
                    OfficeControllerObject.GetComponent<ChangeImages>().enabled = true;

                    //OfficeStuff.transform.position = ResetPoint.transform.position; again wth was this for
                }
            }
        }

        if (gamePadState.IsTriggered(WiiU.GamePadButton.Y) && !camIsUp)
            {
                Boop.Play();
            }
        if (Input.GetKeyDown(KeyCode.Y) && !camIsUp)
        {
            Boop.Play();
        }


        // Remote
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.L))
                {
                    if (camIsUp)
                    {
                        CamSelectPanel.SetActive(false);
                        OfficeStuff.SetActive(true);
                        foreach (Animator anim in FindObjectsOfType<Animator>())
                        {
                            if (anim.gameObject.name.Contains("Door"))
                            {
                                anim.enabled = false;
                            }
                        }
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
                        //OfficeStuff.SetActive(false);

                        FlipOpen.Play();
                        CamViewTabletOpen.SetActive(true);
                        CamViewTabletClose.SetActive(false);

                        camIsUp = true;

                        wait = 0.2f;

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;
                        //OfficeControllerObject.GetComponent<Office>().enabled = false;
                        OfficeControllerObject.GetComponent<Office>().centerPosition = 0;
                        OfficeControllerObject.GetComponent<Movement>().camIsUp = true;
                        OfficeControllerObject.GetComponent<ChangeImages>().camIsUp = true;
                        OfficeControllerObject.GetComponent<RandNumberGen>().camIsUp = true;
                        OfficeControllerObject.GetComponent<ChangeImages>().enabled = true;

                        //OfficeStuff.transform.position = ResetPoint.transform.position; again wth was this for
                    }
                }
                break;

            default:
                break;
        }

        // Keyboard
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (camIsUp)
                {
                    CamSelectPanel.SetActive(false);
                    OfficeStuff.SetActive(true);
                    foreach (Animator anim in FindObjectsOfType<Animator>())
                    {
                        if (anim.gameObject.name.Contains("Door"))
                        {
                            anim.enabled = false;
                        }
                    }
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
                    //OfficeStuff.SetActive(false);

                    FlipOpen.Play();
                    CamViewTabletOpen.SetActive(true);
                    CamViewTabletClose.SetActive(false);

                    camIsUp = true;

                    wait = 0.2f;

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;
                    //OfficeControllerObject.GetComponent<Office>().enabled = false;
                    OfficeControllerObject.GetComponent<Office>().centerPosition = 0;
                    OfficeControllerObject.GetComponent<Movement>().camIsUp = true;
                    OfficeControllerObject.GetComponent<ChangeImages>().camIsUp = true;
                    OfficeControllerObject.GetComponent<RandNumberGen>().camIsUp = true;
                    OfficeControllerObject.GetComponent<ChangeImages>().enabled = true;

                    //OfficeStuff.transform.position = ResetPoint.transform.position; wth was the point of this line of code anyways??????
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
                Dot.SetActive(true);
                Glitch.SetActive(true);
                Stripes.SetActive(true);
                Black.SetActive(true);
                OfficeControllerObject.GetComponent<Office>().enabled = false;
                OfficeStuff.SetActive(false);
                if (officescript.LeftLightIsOn)
                {
                    if (!officescript.BonnieOutsideDoor)
                    {
                        officescript.Light_L_No_Door.SetActive(false);
                        officescript.Light.Pause();
                    }

                    if (officescript.BonnieOutsideDoor)
                    {
                        officescript.Light_L_Door_Bonnie.SetActive(false);
                        officescript.Light.Pause();
                    }

                    officescript.OriginalOfficeImage.GetComponent<Image>().enabled = true;

                    officescript.DoorButton_L3.SetActive(false);

                    if (officescript.L_Door_Closed)
                    {
                        officescript.DoorButton_L1.SetActive(true);
                        officescript.DoorButton_L4.SetActive(false);
                    }

                    officescript.OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                    officescript.LeftLightIsOn = false;
                }


                else if (officescript.RightLightIsOn)
                {
                    if (!officescript.ChicaOutsideDoor)
                    {
                        officescript.Light_R_No_Door.SetActive(false);
                        officescript.Light.Pause();
                    }

                    if (officescript.ChicaOutsideDoor)
                    {
                        officescript.Light_R_Door_Chica.SetActive(false);
                        officescript.Light.Pause();
                    }

                    officescript.OriginalOfficeImage.GetComponent<Image>().enabled = true;

                    officescript.DoorButton_R3.SetActive(false);

                    if (officescript.R_Door_Closed)
                    {
                        officescript.DoorButton_R1.SetActive(true);
                        officescript.DoorButton_R4.SetActive(false);
                    }

                    officescript.OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                    officescript.RightLightIsOn = false;
                }
            }
        }

        if (!camIsUp)
        {
            Black.SetActive(false);
            wait -= Time.deltaTime;

            if (wait <= 0)
            {
                RemoveAnimator();

                wait = 0.2f;

                //Black.SetActive(false);
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