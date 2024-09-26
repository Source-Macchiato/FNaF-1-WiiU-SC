using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class Office : MonoBehaviour {

    public GameObject CheatPanel;
    private Movement movementScript;
    private bool LeftScareAlrdPlayed = false;
    private bool RightScareAlrdPlayed = false;

    public GameObject OfficeImage;
    public RectTransform OfficeBounds;

    public GameObject OfficeControllerObject;

    public GameObject Door_L_closed;
    public GameObject Door_L_open;

    public GameObject Door_R_closed;
    public GameObject Door_R_open;

    // Buttons textures
    public Image DoorButton_L1;
    public Image DoorButton_L2;
    public Image DoorButton_L3;
    public Image DoorButton_L4;

    public Image DoorButton_R1;
    public Image DoorButton_R2;
    public Image DoorButton_R3;
    public Image DoorButton_R4;

    public Image Light_L_No_Door;
    public Image Light_L_Door_Bonnie;

    public Image Light_R_No_Door;
    public Image Light_R_Door_Chica;

    public bool L_Door_Closed = false;
    public bool R_Door_Closed = false;

    public GameObject OriginalOfficeImage;

    public AudioSource DoorClose;
    public AudioSource Light;

    private const float speed = 6f;
    private const float leftEdge = 272f;
    private const float rightEdge = -208f;

    public bool BonnieOutsideDoor = false;
    public bool ChicaOutsideDoor = false;
    public bool FreddyOutsideDoor = false;
    public bool FoxyRunningHallway = false;

    public bool LeftLightIsOn = false;
    public bool RightLightIsOn = false;

    public AudioSource Scare;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    private float joystickThreshold = 0.5f;

    public float centerPosition;

    void Start()
    {
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        movementScript = GetComponent<Movement>();

        centerPosition = 0;

        CheatPanel.SetActive(false);

        // Disable buttons textures
        DoorButton_L2.enabled = false;
        DoorButton_L3.enabled = false;
        DoorButton_L4.enabled = false;

        DoorButton_R2.enabled = false;
        DoorButton_R3.enabled = false;
        DoorButton_R4.enabled = false;

        Light_L_No_Door.enabled = false;
        Light_R_No_Door.enabled = false;

        Light_L_Door_Bonnie.enabled = false;
        Light_R_Door_Chica.enabled = false;
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        Resources.UnloadUnusedAssets();

        // Gamepad
        /* if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsTriggered(WiiU.GamePadButton.Up) && gamePadState.IsTriggered(WiiU.GamePadButton.R))
            {
                if (CheatPanel.activeSelf)
                {
                    CheatPanel.SetActive(false);
                }
                else
                {
                    CheatPanel.SetActive(true);
                }
            }
        } */

        // Remote
        /* switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.Up) && remoteState.pro.IsTriggered(WiiU.ProControllerButton.R))
                {
                    if (CheatPanel.activeSelf)
                    {
                        CheatPanel.SetActive(false);
                    }
                    else
                    {
                        CheatPanel.SetActive(true);
                    }
                }
                break;

            default:
                break;
        } */

        // Keyboard
        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (CheatPanel.activeSelf)
                {
                    CheatPanel.SetActive(false);
                }
                else
                {
                    CheatPanel.SetActive(true);
                }
            }
        }

        float leftHorizontalInput = Input.GetAxis("LeftStickX");

        if (Mathf.Abs(leftHorizontalInput) > joystickThreshold)
        {
            int direction = leftHorizontalInput > 0 ? -1 : 1;

            if (direction > 0)
            {
                OfficeImage.transform.Translate(Vector3.right * speed * Time.deltaTime);
                if (OfficeImage.transform.localPosition.x >= leftEdge)
                {
                    OfficeImage.transform.localPosition = new Vector3(leftEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                }
            }
            else
            {
                OfficeImage.transform.Translate(Vector3.left * speed * Time.deltaTime);
                if (OfficeImage.transform.localPosition.x <= rightEdge)
                {
                    OfficeImage.transform.localPosition = new Vector3(rightEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                }
            }
        }
        // Gamepad
        else if (gamePadState.gamePadErr == WiiU.GamePadError.None) // (max here) i think if i didnt make this a else if then you could move faster if you use joystick and dpad at the same time, but look at this code it seems the french dont like else ifs
        {
            if (gamePadState.IsPressed(WiiU.GamePadButton.Left) && OfficeImage.transform.localPosition.x <= leftEdge)
            {
                OfficeImage.transform.Translate(Vector3.right * speed * Time.deltaTime);
                if (OfficeImage.transform.localPosition.x >= leftEdge)
                {
                    OfficeImage.transform.localPosition = new Vector3(leftEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                }
            }

            if (gamePadState.IsPressed(WiiU.GamePadButton.Right) && OfficeImage.transform.localPosition.x >= rightEdge)
            {
                OfficeImage.transform.Translate(Vector3.left * speed * Time.deltaTime);
                if (OfficeImage.transform.localPosition.x <= rightEdge)
                {
                    OfficeImage.transform.localPosition = new Vector3(rightEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                }
            }
        }

        // Remote
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsPressed(WiiU.ProControllerButton.Left) && OfficeImage.transform.localPosition.x <= leftEdge)
                {
                    OfficeImage.transform.Translate(Vector3.right * speed * Time.deltaTime);
                    if (OfficeImage.transform.localPosition.x >= leftEdge)
                    {
                        OfficeImage.transform.localPosition = new Vector3(leftEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                    }
                }

                if (remoteState.pro.IsPressed(WiiU.ProControllerButton.Right) && OfficeImage.transform.localPosition.x >= rightEdge)
                {
                    OfficeImage.transform.Translate(Vector3.left * speed * Time.deltaTime);
                    if (OfficeImage.transform.localPosition.x <= rightEdge)
                    {
                        OfficeImage.transform.localPosition = new Vector3(rightEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                    }
                }
                break;

            default:
                break;
        }

        // Keyboard
        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && OfficeImage.transform.localPosition.x <= leftEdge)
            {
                OfficeImage.transform.Translate(Vector3.right * speed * Time.deltaTime);
                if (OfficeImage.transform.localPosition.x >= leftEdge)
                {
                    OfficeImage.transform.localPosition = new Vector3(leftEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                }
            }

            if (Input.GetKey(KeyCode.RightArrow) && OfficeImage.transform.localPosition.x >= rightEdge)
            {
                OfficeImage.transform.Translate(Vector3.left * speed * Time.deltaTime);
                if (OfficeImage.transform.localPosition.x <= rightEdge)
                {
                    OfficeImage.transform.localPosition = new Vector3(rightEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                }
            }
        }

        // Check position on the left for the left door
        if (OfficeImage.transform.position.x >= (leftEdge - 100))
        {
            // Gamepad
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
                {
                    foreach (Animator anim in FindObjectsOfType<Animator>())
                    {
                        if (anim.gameObject.name.Contains("Door"))
                        {
                            anim.enabled = true;
                        }
                    }
                    if (L_Door_Closed)
                    {
                        Door_L_closed.SetActive(false);
                        Door_L_open.SetActive(true);
                        L_Door_Closed = false;
                        DoorButton_L1.enabled = true;
                        DoorButton_L2.enabled = false;
                        DoorButton_R4.enabled = false;

                        DoorClose.Play();

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                        OfficeControllerObject.GetComponent<Movement>().LeftDoorClosed = false;
                        OfficeControllerObject.GetComponent<ChangeImages>().L_Door_Closed = false;

                    }
                    else
                    {
                        Door_L_closed.SetActive(true);
                        Door_L_open.SetActive(false);
                        L_Door_Closed = true;
                        DoorButton_L1.enabled = false;
                        DoorButton_L2.enabled = true;

                        DoorClose.Play();

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                        OfficeControllerObject.GetComponent<Movement>().LeftDoorClosed = true;

                        OfficeControllerObject.GetComponent<ChangeImages>().L_Door_Closed = true;

                    }
                }
            }

            // Remote
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.A))
                    {
                        foreach (Animator anim in FindObjectsOfType<Animator>())
                        {
                            if (anim.gameObject.name.Contains("Door"))
                            {
                                anim.enabled = true;
                            }
                        }
                        if (L_Door_Closed)
                        {
                            Door_L_closed.SetActive(false);
                            Door_L_open.SetActive(true);
                            L_Door_Closed = false;
                            DoorButton_L1.enabled = true;
                            DoorButton_L2.enabled = false;
                            DoorButton_R4.enabled = false;

                            DoorClose.Play();

                            OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                            OfficeControllerObject.GetComponent<Movement>().LeftDoorClosed = false;
                            OfficeControllerObject.GetComponent<ChangeImages>().L_Door_Closed = false;

                        }
                        else
                        {
                            Door_L_closed.SetActive(true);
                            Door_L_open.SetActive(false);
                            L_Door_Closed = true;
                            DoorButton_L1.enabled = false;
                            DoorButton_L2.enabled = true;

                            DoorClose.Play();

                            OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                            OfficeControllerObject.GetComponent<Movement>().LeftDoorClosed = true;

                            OfficeControllerObject.GetComponent<ChangeImages>().L_Door_Closed = true;

                        }
                    }
                    break;

                default:
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    foreach (Animator anim in FindObjectsOfType<Animator>())
                    {
                        if (anim.gameObject.name.Contains("Door"))
                        {
                            anim.enabled = true;
                        }
                    }
                    if (L_Door_Closed)
                    {
                        Door_L_closed.SetActive(false);
                        Door_L_open.SetActive(true);
                        L_Door_Closed = false;
                        DoorButton_L1.enabled = true;
                        DoorButton_L2.enabled = false;
                        DoorButton_R4.enabled = false;

                        DoorClose.Play();

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                        OfficeControllerObject.GetComponent<Movement>().LeftDoorClosed = false;
                        OfficeControllerObject.GetComponent<ChangeImages>().L_Door_Closed = false;
                    }
                    else
                    {
                        Door_L_closed.SetActive(true);
                        Door_L_open.SetActive(false);
                        L_Door_Closed = true;
                        DoorButton_L1.enabled = false;
                        DoorButton_L2.enabled = true;

                        DoorClose.Play();


                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                        OfficeControllerObject.GetComponent<Movement>().LeftDoorClosed = true;

                        OfficeControllerObject.GetComponent<ChangeImages>().L_Door_Closed = true;

                    }
                }
            }

            // Left light
            // Gamepad
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.X))
                {
                    LeftLightIsOn = true;

                    if (!BonnieOutsideDoor)
                    {
                        Light_L_No_Door.enabled = true;
                        Light.Play();
                        LeftScareAlrdPlayed = false;
                    }

                    if (BonnieOutsideDoor)
                    {
                        Light_L_Door_Bonnie.enabled = true;
                        Light.Play();

                        if (!L_Door_Closed && LeftScareAlrdPlayed == false)
                        {
                            Scare.Play();
                            LeftScareAlrdPlayed = true;
                        }
                    }
                    else
                    {
                        LeftScareAlrdPlayed = false;
                    }

                    OriginalOfficeImage.GetComponent<Image>().enabled = false;

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                    DoorButton_L3.enabled = true;

                    if (L_Door_Closed)
                    {
                        DoorButton_L1.enabled = false;
                        DoorButton_L4.enabled = true;
                    }
                }
            }

            // Remote
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.X))
                    {
                        LeftLightIsOn = true;

                        if (!BonnieOutsideDoor)
                        {
                            Light_L_No_Door.enabled = true;
                            Light.Play();
                            LeftScareAlrdPlayed = false;
                        }

                        if (BonnieOutsideDoor)
                        {
                            Light_L_Door_Bonnie.enabled = true;
                            Light.Play();

                            if (!L_Door_Closed && LeftScareAlrdPlayed == false)
                            {
                                Scare.Play();
                                LeftScareAlrdPlayed = true;
                            }
                        }
                        else
                        {
                            LeftScareAlrdPlayed = false;
                        }

                        OriginalOfficeImage.GetComponent<Image>().enabled = false;

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                        DoorButton_L3.enabled = true;

                        if (L_Door_Closed)
                        {
                            DoorButton_L1.enabled = false;
                            DoorButton_L4.enabled = true;
                        }
                    }
                    break;

                default:
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    LeftLightIsOn = true;

                    if (!BonnieOutsideDoor)
                    {
                        Light_L_No_Door.enabled = true;
                        Light.Play();
                        LeftScareAlrdPlayed = false;
                    }

                    if (BonnieOutsideDoor)
                    {
                        Light_L_Door_Bonnie.enabled = true;
                        Light.Play();

                        if (!L_Door_Closed && LeftScareAlrdPlayed == false)
                        {
                            LeftScareAlrdPlayed = true;
                            Scare.Play();
                        }
                    }

                    OriginalOfficeImage.GetComponent<Image>().enabled = false;

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                    DoorButton_L3.enabled = true;

                    if (L_Door_Closed)
                    {
                        DoorButton_L1.enabled = false;
                        DoorButton_L4.enabled = true;
                    }
                }
            }
        }
        //-----------------------------------------------

        // Check position on the right for the right door
        if (OfficeImage.transform.position.x <= (rightEdge + 100))
        {
            // Gamepad
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
                {
                    if (R_Door_Closed)
                    {
                        Door_R_closed.SetActive(false);
                        Door_R_open.SetActive(true);
                        R_Door_Closed = false;
                        DoorButton_R1.enabled = true;
                        DoorButton_R2.enabled = false;
                        DoorButton_R4.enabled = false;

                        DoorClose.Play();

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                        OfficeControllerObject.GetComponent<Movement>().RightDoorClosed = false;
                    }
                    else
                    {
                        Door_R_closed.SetActive(true);
                        Door_R_open.SetActive(false);
                        R_Door_Closed = true;
                        DoorButton_R1.enabled = false;
                        DoorButton_R2.enabled = true;

                        DoorClose.Play();

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                        OfficeControllerObject.GetComponent<Movement>().RightDoorClosed = true;

                    }
                }
                bool camIsUp = movementScript.camIsUp;

                if (gamePadState.IsTriggered(WiiU.GamePadButton.X))
                {
                    RightLightIsOn = true;

                    if (!ChicaOutsideDoor)
                    {
                        Light_R_No_Door.enabled = true;
                        Light.Play();
                        RightScareAlrdPlayed = false;
                    }

                    if (ChicaOutsideDoor)
                    {
                        Light_R_Door_Chica.enabled = true;
                        Light.Play();

                        if (!R_Door_Closed && RightScareAlrdPlayed == false)
                        {
                            RightScareAlrdPlayed = true;
                            Scare.Play();
                        }

                    }

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                    DoorButton_R3.enabled = true;

                    if (R_Door_Closed)
                    {
                        DoorButton_R1.enabled = false;
                        DoorButton_R4.enabled = true;
                    }
                }
            }

            // Remote
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.A))
                    {
                        if (R_Door_Closed)
                        {
                            Door_R_closed.SetActive(false);
                            Door_R_open.SetActive(true);
                            R_Door_Closed = false;
                            DoorButton_R1.enabled = true;
                            DoorButton_R2.enabled = false;
                            DoorButton_R4.enabled = false;

                            DoorClose.Play();

                            OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                            OfficeControllerObject.GetComponent<Movement>().RightDoorClosed = false;
                        }
                        else
                        {
                            Door_R_closed.SetActive(true);
                            Door_R_open.SetActive(false);
                            R_Door_Closed = true;
                            DoorButton_R1.enabled = false;
                            DoorButton_R2.enabled = true;

                            DoorClose.Play();

                            OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                            OfficeControllerObject.GetComponent<Movement>().RightDoorClosed = true;

                        }
                    }
                    bool camIsUp = movementScript.camIsUp;

                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.X))
                    {
                        RightLightIsOn = true;

                        if (!ChicaOutsideDoor)
                        {
                            Light_R_No_Door.enabled = true;
                            Light.Play();
                            RightScareAlrdPlayed = false;
                        }

                        if (ChicaOutsideDoor)
                        {
                            Light_R_Door_Chica.enabled = true;
                            Light.Play();

                            if (!R_Door_Closed && RightScareAlrdPlayed == false)
                            {
                                RightScareAlrdPlayed = true;
                                Scare.Play();
                            }

                        }

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                        DoorButton_R3.enabled = true;

                        if (R_Door_Closed)
                        {
                            DoorButton_R1.enabled = false;
                            DoorButton_R4.enabled = true;
                        }
                    }
                    break;

                default:
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    if (R_Door_Closed)
                    {
                        Door_R_closed.SetActive(false);
                        Door_R_open.SetActive(true);
                        R_Door_Closed = false;
                        DoorButton_R1.enabled = true;
                        DoorButton_R2.enabled = false;
                        DoorButton_R4.enabled = false;

                        DoorClose.Play();

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                        OfficeControllerObject.GetComponent<Movement>().RightDoorClosed = false;
                    }
                    else
                    {
                        Door_R_closed.SetActive(true);
                        Door_R_open.SetActive(false);
                        R_Door_Closed = true;
                        DoorButton_R1.enabled = false;
                        DoorButton_R2.enabled = true;

                        DoorClose.Play();

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                        OfficeControllerObject.GetComponent<Movement>().RightDoorClosed = true;
                    }
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    RightLightIsOn = true;

                    if (!ChicaOutsideDoor)
                    {
                        Light_R_No_Door.enabled = true;
                        Light.Play();
                        RightScareAlrdPlayed = false;
                    }

                    if (ChicaOutsideDoor)
                    {
                        Light_R_Door_Chica.enabled = true;
                        Light.Play();

                        if (!R_Door_Closed && RightScareAlrdPlayed == false)
                        {
                            RightScareAlrdPlayed = true;
                            Scare.Play();
                        }
                    }
                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

                    DoorButton_R3.enabled = true;

                    if (R_Door_Closed)
                    {
                        DoorButton_R1.enabled = false;
                        DoorButton_R4.enabled = true;
                    }
                }
            }
        }

        // Right light
        // Gamepad
        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsReleased(WiiU.GamePadButton.X))
            {
                if (LeftLightIsOn)
                {
                    if (!BonnieOutsideDoor)
                    {
                        Light_L_No_Door.enabled = false;
                        Light.Pause();
                    }

                    if (BonnieOutsideDoor)
                    {
                        Light_L_Door_Bonnie.enabled = false;
                        Light.Pause();
                    }

                    OriginalOfficeImage.GetComponent<Image>().enabled = true;

                    DoorButton_L3.enabled = false;

                    if (L_Door_Closed)
                    {
                        DoorButton_L1.enabled = true;
                        DoorButton_L4.enabled = false;
                    }

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                    LeftLightIsOn = false;
                }


                if (RightLightIsOn)
                {
                    if (!ChicaOutsideDoor)
                    {
                        Light_R_No_Door.enabled = false;
                        Light.Pause();
                    }

                    if (ChicaOutsideDoor)
                    {
                        Light_R_Door_Chica.enabled = false;
                        Light.Pause();
                    }

                    OriginalOfficeImage.GetComponent<Image>().enabled = true;

                    DoorButton_R3.enabled = false;

                    if (R_Door_Closed)
                    {
                        DoorButton_R1.enabled = true;
                        DoorButton_R4.enabled = false;
                    }

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                    RightLightIsOn = false;
                }
            }
        }

        // Remote
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsReleased(WiiU.ProControllerButton.X))
                {
                    if (LeftLightIsOn)
                    {
                        if (!BonnieOutsideDoor)
                        {
                            Light_L_No_Door.enabled = false;
                            Light.Pause();
                        }

                        if (BonnieOutsideDoor)
                        {
                            Light_L_Door_Bonnie.enabled = false;
                            Light.Pause();
                        }

                        OriginalOfficeImage.GetComponent<Image>().enabled = true;

                        DoorButton_L3.enabled = false;

                        if (L_Door_Closed)
                        {
                            DoorButton_L1.enabled = true;
                            DoorButton_L4.enabled = false;
                        }

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                        LeftLightIsOn = false;
                    }


                    if (RightLightIsOn)
                    {
                        if (!ChicaOutsideDoor)
                        {
                            Light_R_No_Door.enabled = false;
                            Light.Pause();
                        }

                        if (ChicaOutsideDoor)
                        {
                            Light_R_Door_Chica.enabled = false;
                            Light.Pause();
                        }

                        OriginalOfficeImage.GetComponent<Image>().enabled = true;

                        DoorButton_R3.enabled = false;

                        if (R_Door_Closed)
                        {
                            DoorButton_R1.enabled = true;
                            DoorButton_R4.enabled = false;
                        }

                        OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                        RightLightIsOn = false;
                    }
                }
                break;

            default:
                break;
        }

        // Keyboard
        if (Application.isEditor)
        {
            if (Input.GetKeyUp(KeyCode.X))
            {
                if (LeftLightIsOn)
                {
                    if (!BonnieOutsideDoor)
                    {
                        Light_L_No_Door.enabled = false;
                        Light.Pause();
                    }

                    if (BonnieOutsideDoor)
                    {
                        Light_L_Door_Bonnie.enabled = false;
                        Light.Pause();
                    }

                    OriginalOfficeImage.GetComponent<Image>().enabled = true;

                    DoorButton_L3.enabled = false;

                    if (L_Door_Closed)
                    {
                        DoorButton_L1.enabled = true;
                        DoorButton_L4.enabled = false;
                    }

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                    LeftLightIsOn = false;
                }


                if (RightLightIsOn)
                {
                    if (!ChicaOutsideDoor)
                    {
                        Light_R_No_Door.enabled = false;
                        Light.Pause();
                    }

                    if (ChicaOutsideDoor)
                    {
                        Light_R_Door_Chica.enabled = false;
                        Light.Pause();
                    }

                    OriginalOfficeImage.GetComponent<Image>().enabled = true;

                    DoorButton_R3.enabled = false;

                    if (R_Door_Closed)
                    {
                        DoorButton_R1.enabled = true;
                        DoorButton_R4.enabled = false;
                    }

                    OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

                    RightLightIsOn = false;
                }
            }
        }
        if (!L_Door_Closed && DoorButton_L4.isActiveAndEnabled == true)
        {
            DoorButton_L4.enabled = false;
        }
        if (!R_Door_Closed && DoorButton_R4.isActiveAndEnabled == true)
        {
            DoorButton_R4.enabled = false;
        }
        if (DoorButton_L2.isActiveAndEnabled == true && DoorButton_L3.isActiveAndEnabled == true)
        {
            DoorButton_L4.enabled = true;
        }
        if (DoorButton_R2.isActiveAndEnabled == true && DoorButton_R3.isActiveAndEnabled == true)
        {
            DoorButton_R4.enabled = true;
        }
        //----------------------------------------------

        //----------------------------------------------
        // Gamepad
        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsPressed(WiiU.GamePadButton.X))
            {
                if (BonnieOutsideDoor)
                {
                    if (OfficeControllerObject.GetComponent<Movement>().BonnieOutsideDoorTime <= 2)
                    {
                        OfficeControllerObject.GetComponent<Movement>().BonnieOutsideDoorTime = 1;
                    }
                }

                if (ChicaOutsideDoor)
                {
                    if (OfficeControllerObject.GetComponent<Movement>().ChicaOutsideDoorTime <= 2)
                    {
                        OfficeControllerObject.GetComponent<Movement>().ChicaOutsideDoorTime = 1;
                    }
                }

                if (FreddyOutsideDoor)
                {
                    if (OfficeControllerObject.GetComponent<Movement>().FreddyOutsideDoorTime <= 2)
                    {
                        OfficeControllerObject.GetComponent<Movement>().FreddyOutsideDoorTime = 1;
                    }
                }
            }
        }

        // Remote
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsPressed(WiiU.ProControllerButton.X))
                {
                    if (BonnieOutsideDoor)
                    {
                        if (OfficeControllerObject.GetComponent<Movement>().BonnieOutsideDoorTime <= 2)
                        {
                            OfficeControllerObject.GetComponent<Movement>().BonnieOutsideDoorTime = 1;
                        }
                    }

                    if (ChicaOutsideDoor)
                    {
                        if (OfficeControllerObject.GetComponent<Movement>().ChicaOutsideDoorTime <= 2)
                        {
                            OfficeControllerObject.GetComponent<Movement>().ChicaOutsideDoorTime = 1;
                        }
                    }

                    if (FreddyOutsideDoor)
                    {
                        if (OfficeControllerObject.GetComponent<Movement>().FreddyOutsideDoorTime <= 2)
                        {
                            OfficeControllerObject.GetComponent<Movement>().FreddyOutsideDoorTime = 1;
                        }
                    }
                }
                break;

            default:
                break;
        }

        // Keyboard
        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.B))
            {
                if (BonnieOutsideDoor)
                {
                    if (OfficeControllerObject.GetComponent<Movement>().BonnieOutsideDoorTime <= 2)
                    {
                        OfficeControllerObject.GetComponent<Movement>().BonnieOutsideDoorTime = 1;
                    }
                }

                if (ChicaOutsideDoor)
                {
                    if (OfficeControllerObject.GetComponent<Movement>().ChicaOutsideDoorTime <= 2)
                    {
                        OfficeControllerObject.GetComponent<Movement>().ChicaOutsideDoorTime = 1;
                    }
                }

                if (FreddyOutsideDoor)
                {
                    if (OfficeControllerObject.GetComponent<Movement>().FreddyOutsideDoorTime <= 2)
                    {
                        OfficeControllerObject.GetComponent<Movement>().FreddyOutsideDoorTime = 1;
                    }
                }
            }
        }
        //-------------------------------------------------

        if (BonnieOutsideDoor)
        {
            if (L_Door_Closed)
            {
                OfficeControllerObject.GetComponent<Movement>().BonnieOutsideDoor = true;
                OfficeControllerObject.GetComponent<ChangeImages>().WhereBonnie = 2;
            }
        }

        if (ChicaOutsideDoor)
        {
            if (R_Door_Closed)
            {
                OfficeControllerObject.GetComponent<Movement>().ChicaOutsideDoor = true;
                OfficeControllerObject.GetComponent<ChangeImages>().WhereChica = 2;
            }
        }

        if (FreddyOutsideDoor)
        {
            if (R_Door_Closed)
            {
                OfficeControllerObject.GetComponent<Movement>().FreddyOutsideDoor = true;
                OfficeControllerObject.GetComponent<ChangeImages>().WhereFreddy = 1;
            }
        }
    }
}