using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class CameraScript : MonoBehaviour
{
    public AudioSource Boop;
    public bool camIsUp = false;
    public bool canUseCamera = true;
    private int layoutId;

    public float wait = 0.2f;

    public GameObject minimapGameObject;
    public GameObject cameraScreen;
    public AudioSource FlipOpen;
    public AudioSource FlipClose;
    public GameObject CamViewTabletOpen;
    public GameObject CamViewTabletClose;

    public GameObject Dot;
    public GameObject Glitch;
    public GameObject stripes;
    public GameObject stripesMovement;
    public GameObject hideOfficeGameObject;

    private GameScript gameScript;
    private Office office;
    private Movement movement;
    private ChangeImages changeImages;
    private RandNumberGen randNumberGen;
    private MoveInOffice moveInOffice;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    void Start()
    {
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        gameScript = FindObjectOfType<GameScript>();
        office = FindObjectOfType<Office>();
        movement = FindObjectOfType<Movement>();
        changeImages = FindObjectOfType<ChangeImages>();
        randNumberGen = FindObjectOfType<RandNumberGen>();
        moveInOffice = FindObjectOfType<MoveInOffice>();

        hideOfficeGameObject.SetActive(false);

        layoutId = SaveManager.LoadLayoutId();
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
                if (canUseCamera)
                {
                    CameraSystem();
                }
            }

            if (gamePadState.IsTriggered(WiiU.GamePadButton.X))
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
                    if (canUseCamera)
                    {
                        CameraSystem();
                    }
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
                    if (canUseCamera)
                    {
                        CameraSystem();
                    }
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
                    if (canUseCamera)
                    {
                        CameraSystem();
                    }
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
                if (canUseCamera)
                {
                    CameraSystem();
                }
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

                // Reset wait delay
                wait = 0.2f;

                // UI to enable and disable
                Dot.SetActive(true);
                Glitch.SetActive(true);
                stripes.SetActive(true);
                cameraScreen.SetActive(true);
                office.enabled = false;
                minimapGameObject.SetActive(true);
                hideOfficeGameObject.SetActive(layoutId != 2);

                if (office.leftLightIsOn)
                {
                    if (!office.BonnieOutsideDoor)
                    {
                        office.Light_L_No_Door.enabled = false;
                        office.lightSound.Pause();
                    }

                    if (office.BonnieOutsideDoor)
                    {
                        office.Light_L_Door_Bonnie.enabled = false;
                        office.lightSound.Pause();
                    }

                    office.OriginalOfficeImage.GetComponent<Image>().enabled = true;

                    office.DoorButton_L3.enabled = false;

                    if (office.L_Door_Closed)
                    {
                        office.DoorButton_L1.enabled = true;
                        office.DoorButton_L4.enabled = false;
                    }

                    gameScript.PowerUsage -= 1;

                    office.ToggleLeftLight();
                }


                else if (office.rightLightIsOn)
                {
                    if (!office.ChicaOutsideDoor)
                    {
                        office.Light_R_No_Door.enabled = false;
                        office.lightSound.Pause();
                    }

                    if (office.ChicaOutsideDoor)
                    {
                        office.Light_R_Door_Chica.enabled = false;
                        office.lightSound.Pause();
                    }

                    office.OriginalOfficeImage.GetComponent<Image>().enabled = true;

                    office.DoorButton_R3.enabled = false;

                    if (office.R_Door_Closed)
                    {
                        office.DoorButton_R1.enabled = true;
                        office.DoorButton_R4.enabled = false;
                    }

                    gameScript.PowerUsage -= 1;

                    office.ToggleRightLight();
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

    public void CameraSystem()
    {
        if (camIsUp)
        {
            minimapGameObject.SetActive(false);

            FlipClose.Play();

            CamViewTabletClose.SetActive(true);
            CamViewTabletOpen.SetActive(false);

            Dot.SetActive(false);
            Glitch.SetActive(false);
            stripes.SetActive(false);
            stripesMovement.SetActive(false);
            hideOfficeGameObject.SetActive(false);

            camIsUp = false;

            wait = 0.2f;

            gameScript.PowerUsage -= 1;
            office.enabled = true;
            movement.camIsUp = false;
            changeImages.camIsUp = false;
            randNumberGen.camIsUp = false;
            moveInOffice.camIsUp = false;
        }
        else
        {
            FlipOpen.Play();
            CamViewTabletOpen.SetActive(true);
            CamViewTabletClose.SetActive(false);

            camIsUp = true;

            wait = 0.2f;

            gameScript.PowerUsage += 1;
            office.centerPosition = 0;
            movement.camIsUp = true;
            changeImages.camIsUp = true;
            randNumberGen.camIsUp = true;
            moveInOffice.camIsUp = true;

            //OfficeStuff.transform.position = ResetPoint.transform.position; again wth was this for -- for real idk it's really dumb, use Vector3.zero or Vector2.zero is a better way
        }
    }
}