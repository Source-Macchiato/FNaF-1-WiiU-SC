using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class CameraScript : MonoBehaviour
{
    public AudioSource Boop;
    public bool camIsUp = false;

    public float wait = 0.2f;

    public GameObject CamSelectPanel;
    public GameObject OfficeStuff;
    public GameObject cameraScreen;
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

            if (gamePadState.IsTriggered(WiiU.GamePadButton.X) && !camIsUp)
            {
                if (!camIsUp)
                {
                    Boop.Play();
                }
            }
        }

        // Remote
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.L))
                {
                    CameraSystem();
                }

                if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.X))
                {
                    if (!camIsUp)
                    {
                        Boop.Play();
                    }
                }
                break;
            case WiiU.RemoteDevType.Classic:
                if (remoteState.classic.IsTriggered(WiiU.ClassicButton.L))
                {
                    CameraSystem();
                }

                if (remoteState.classic.IsTriggered(WiiU.ClassicButton.X))
                {
                    if (!camIsUp)
                    {
                        Boop.Play();
                    }
                }
                break;
            default:
                if (remoteState.IsTriggered(WiiU.RemoteButton.One))
                {
                    CameraSystem();
                }

                if (remoteState.IsTriggered(WiiU.RemoteButton.Two))
                {
                    if (!camIsUp)
                    {
                        Boop.Play();
                    }
                }
                break;
        }

        // Keyboard
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                CameraSystem();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                if (!camIsUp)
                {
                    Boop.Play();
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
                cameraScreen.SetActive(true);
                OfficeControllerObject.GetComponent<Office>().enabled = false;
                OfficeStuff.SetActive(false);
                if (officescript.LeftLightIsOn)
                {
                    if (!officescript.BonnieOutsideDoor)
                    {
                        officescript.Light_L_No_Door.enabled = false;
                        officescript.lightSound.Pause();
                    }

                    if (officescript.BonnieOutsideDoor)
                    {
                        officescript.Light_L_Door_Bonnie.enabled = false;
                        officescript.lightSound.Pause();
                    }

                    officescript.OriginalOfficeImage.GetComponent<Image>().enabled = true;

                    officescript.DoorButton_L3.enabled = false;

                    if (officescript.L_Door_Closed)
                    {
                        officescript.DoorButton_L1.enabled = true;
                        officescript.DoorButton_L4.enabled = false;
                    }

                    officescript.OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                    officescript.LeftLightIsOn = false;
                }


                else if (officescript.RightLightIsOn)
                {
                    if (!officescript.ChicaOutsideDoor)
                    {
                        officescript.Light_R_No_Door.enabled = false;
                        officescript.lightSound.Pause();
                    }

                    if (officescript.ChicaOutsideDoor)
                    {
                        officescript.Light_R_Door_Chica.enabled = false;
                        officescript.lightSound.Pause();
                    }

                    officescript.OriginalOfficeImage.GetComponent<Image>().enabled = true;

                    officescript.DoorButton_R3.enabled = false;

                    if (officescript.R_Door_Closed)
                    {
                        officescript.DoorButton_R1.enabled = true;
                        officescript.DoorButton_R4.enabled = false;
                    }

                    officescript.OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                    officescript.RightLightIsOn = false;
                }
            }
        }

        if (!camIsUp)
        {
            cameraScreen.SetActive(false);

            // Set sprite to null for refresh sprite for easter eggs when camera is up
            cameraScreen.GetComponent<Image>().sprite = null;

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

    private void CameraSystem()
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