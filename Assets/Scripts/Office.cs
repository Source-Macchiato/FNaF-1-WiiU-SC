using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class Office : MonoBehaviour {

    public GameObject CheatPanel;
    private Movement movementScript;
    private bool LeftScareAlrdPlayed = false;
    private bool RightScareAlrdPlayed = false;

    public GameObject OfficeImage;
    public RectTransform officeBounds;

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

    public Image OriginalOfficeImage;

    public AudioSource DoorClose;
    public AudioSource Light;

    private const float speed = 5f;
    private const float leftEdge = 160f;
    private const float rightEdge = -130f;

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

        officeBounds = OfficeImage.GetComponent<RectTransform>();
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        Resources.UnloadUnusedAssets();

        // Display cheat panel
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

        // Navigation is office
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

        // Remotes
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
                else if (remoteState.pro.IsPressed(WiiU.ProControllerButton.Right) && OfficeImage.transform.localPosition.x >= rightEdge)
                {
                    OfficeImage.transform.Translate(Vector3.left * speed * Time.deltaTime);
                    if (OfficeImage.transform.localPosition.x <= rightEdge)
                    {
                        OfficeImage.transform.localPosition = new Vector3(rightEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                    }
                }
                break;
            case WiiU.RemoteDevType.Classic:
                if (remoteState.classic.IsPressed(WiiU.ClassicButton.Left) && OfficeImage.transform.localPosition.x <= leftEdge)
                {
                    OfficeImage.transform.Translate(Vector3.right * speed * Time.deltaTime);
                    if (OfficeImage.transform.localPosition.x >= leftEdge)
                    {
                        OfficeImage.transform.localPosition = new Vector3(leftEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                    }
                    else if (remoteState.classic.IsPressed(WiiU.ClassicButton.Right) && OfficeImage.transform.localPosition.x >= rightEdge)
                    {
                        OfficeImage.transform.Translate(Vector3.left * speed * Time.deltaTime);
                        if (OfficeImage.transform.localPosition.x <= rightEdge)
                        {
                            OfficeImage.transform.localPosition = new Vector3(rightEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                        }
                    }
                }
                break;
            default:
                if (remoteState.IsPressed(WiiU.RemoteButton.Left) && OfficeImage.transform.localPosition.x <= leftEdge)
                {
                    OfficeImage.transform.Translate(Vector3.right * speed * Time.deltaTime);
                    if (OfficeImage.transform.localPosition.x >= leftEdge)
                    {
                        OfficeImage.transform.localPosition = new Vector3(leftEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                    }
                    else if (remoteState.IsPressed(WiiU.RemoteButton.Right) && OfficeImage.transform.localPosition.x >= rightEdge)
                    {
                        OfficeImage.transform.Translate(Vector3.left * speed * Time.deltaTime);
                        if (OfficeImage.transform.localPosition.x <= rightEdge)
                        {
                            OfficeImage.transform.localPosition = new Vector3(rightEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                        }
                    }
                }
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
            else if (Input.GetKey(KeyCode.RightArrow) && OfficeImage.transform.localPosition.x >= rightEdge)
            {
                OfficeImage.transform.Translate(Vector3.left * speed * Time.deltaTime);
                if (OfficeImage.transform.localPosition.x <= rightEdge)
                {
                    OfficeImage.transform.localPosition = new Vector3(rightEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
                }
            }
        }

        // Check position on the left for the left door
        if (officeBounds.localPosition.x >= (leftEdge - 70f))
        {
            // Gamepad
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
                {
                    LeftDoorSystem();
                }
            }

            // Remotes
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.A))
                    {
                        LeftDoorSystem();
                    }
                    break;
                case WiiU.RemoteDevType.Classic:
                    if (remoteState.classic.IsTriggered(WiiU.ClassicButton.A))
                    {
                        LeftDoorSystem();
                    }
                    break;
                default:
                    if (remoteState.IsTriggered(WiiU.RemoteButton.A))
                    {
                        LeftDoorSystem();
                    }
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    LeftDoorSystem();
                }
            }

            // Left light
            // Gamepad
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.B))
                {
                    LeftDoorAndLightManager();
                }
            }

            // Remote
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.B))
                    {
                        LeftDoorAndLightManager();
                    }
                    break;
                case WiiU.RemoteDevType.Classic:
                    if (remoteState.classic.IsTriggered(WiiU.ClassicButton.B))
                    {
                        LeftDoorAndLightManager();
                    }
                    break;
                default:
                    if (remoteState.IsTriggered(WiiU.RemoteButton.B))
                    {
                        LeftDoorAndLightManager();
                    }
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.B))
                {
                    LeftDoorAndLightManager();
                }
            }
        }
        //-----------------------------------------------

        // Check position on the right for the right door
        if (officeBounds.localPosition.x <= (rightEdge + 70f))
        {
            // Gamepad
            if (gamePadState.gamePadErr == WiiU.GamePadError.None)
            {
                if (gamePadState.IsTriggered(WiiU.GamePadButton.A))
                {
                    RightDoorSystem();
                }

                if (gamePadState.IsTriggered(WiiU.GamePadButton.B))
                {
                    RightDoorAndLightManager();
                }
            }

            // Remote
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.A))
                    {
                        RightDoorSystem();
                    }

                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.B))
                    {
                        RightDoorAndLightManager();
                    }
                    break;
                case WiiU.RemoteDevType.Classic:
                    if (remoteState.classic.IsTriggered(WiiU.ClassicButton.A))
                    {
                        RightDoorSystem();
                    }

                    if (remoteState.classic.IsTriggered(WiiU.ClassicButton.B))
                    {
                        RightDoorAndLightManager();
                    }
                    break;
                default:
                    if (remoteState.IsTriggered(WiiU.RemoteButton.A))
                    {
                        RightDoorSystem();
                    }

                    if (remoteState.IsTriggered(WiiU.RemoteButton.B))
                    {
                        RightDoorAndLightManager();
                    }
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    RightDoorSystem();
                }

                if (Input.GetKeyDown(KeyCode.B))
                {
                    RightDoorAndLightManager();
                }
            }
        }

        if (!L_Door_Closed && DoorButton_L4.isActiveAndEnabled)
        {
            DoorButton_L4.enabled = false;
        }
        if (!R_Door_Closed && DoorButton_R4.isActiveAndEnabled)
        {
            DoorButton_R4.enabled = false;
        }
        if (DoorButton_L2.isActiveAndEnabled && DoorButton_L3.isActiveAndEnabled && !DoorButton_L4.isActiveAndEnabled)
        {
            DoorButton_L4.enabled = true;
        }
        if (DoorButton_R2.isActiveAndEnabled && DoorButton_R3.isActiveAndEnabled && !DoorButton_L4.isActiveAndEnabled)
        {
            DoorButton_R4.enabled = true;
        }
        //----------------------------------------------

        //----------------------------------------------
        // Gamepad
        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            if (gamePadState.IsPressed(WiiU.GamePadButton.B))
            {
                CharactersMovement();
            }
        }

        // Remotes
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                if (remoteState.pro.IsPressed(WiiU.ProControllerButton.B))
                {
                    CharactersMovement();
                }
                break;
            case WiiU.RemoteDevType.Classic:
                if (remoteState.classic.IsPressed(WiiU.ClassicButton.B))
                {
                    CharactersMovement();
                }
                break;
            default:
                if (remoteState.IsPressed(WiiU.RemoteButton.B))
                {
                    CharactersMovement();
                }
                break;
        }

        // Keyboard
        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.B))
            {
                CharactersMovement();
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

    void LeftDoorSystem()
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

            // Check if already displayed
            if (!DoorButton_L1.isActiveAndEnabled)
            {
                DoorButton_L1.enabled = true;
            }
            
            // Check if already displayed
            if (DoorButton_L2.isActiveAndEnabled)
            {
                DoorButton_L2.enabled = false;
            }
            
            // Check if already displayed
            if (DoorButton_R4.isActiveAndEnabled)
            {
                DoorButton_R4.enabled = false;
            }

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

            // Check if already displayed
            if (DoorButton_L1.isActiveAndEnabled)
            {
                DoorButton_L1.enabled = false;
            }
            
            // Check if already displayed
            if (!DoorButton_L2.isActiveAndEnabled)
            {
                DoorButton_L2.enabled = true;
            }

            DoorClose.Play();

            OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

            OfficeControllerObject.GetComponent<Movement>().LeftDoorClosed = true;

            OfficeControllerObject.GetComponent<ChangeImages>().L_Door_Closed = true;

        }
    }

    void RightDoorSystem()
    {
        if (R_Door_Closed)
        {
            Door_R_closed.SetActive(false);
            Door_R_open.SetActive(true);
            R_Door_Closed = false;

            // Check if already displayed
            if (!DoorButton_R1.isActiveAndEnabled)
            {
                DoorButton_R1.enabled = true;
            }
            
            // Check if already displayed
            if (DoorButton_R2.isActiveAndEnabled)
            {
                DoorButton_R2.enabled = false;
            }

            // Check if already displayed
            if (DoorButton_R4.isActiveAndEnabled)
            {
                DoorButton_R4.enabled = false;
            }

            DoorClose.Play();

            OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

            OfficeControllerObject.GetComponent<Movement>().RightDoorClosed = false;
        }
        else
        {
            Door_R_closed.SetActive(true);
            Door_R_open.SetActive(false);
            R_Door_Closed = true;
            
            // Check if already displayed
            if (DoorButton_R1.isActiveAndEnabled)
            {
                DoorButton_R1.enabled = false;
            }

            // Check if already displayed
            if (!DoorButton_R2.isActiveAndEnabled)
            {
                DoorButton_R2.enabled = true;
            }

            DoorClose.Play();

            OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

            OfficeControllerObject.GetComponent<Movement>().RightDoorClosed = true;

        }
    }

    void LeftDoorAndLightManager()
    {
        if (LeftLightIsOn)
        {
            if (!BonnieOutsideDoor)
            {
                // Check if already displayed
                if (Light_L_No_Door.isActiveAndEnabled)
                {
                    Light_L_No_Door.enabled = false;
                }

                Light.Pause();
            }

            if (BonnieOutsideDoor)
            {
                // Check if already displayed
                if (Light_L_Door_Bonnie.isActiveAndEnabled)
                {
                    Light_L_Door_Bonnie.enabled = false;
                }

                Light.Pause();
            }

            // Check if already displayed
            if (!OriginalOfficeImage.isActiveAndEnabled)
            {
                OriginalOfficeImage.enabled = true;
            }

            // Check if already displayed
            if (DoorButton_L3.isActiveAndEnabled)
            {
                DoorButton_L3.enabled = false;
            }

            if (L_Door_Closed)
            {
                // Check if already displayed
                if (!DoorButton_L1.isActiveAndEnabled)
                {
                    DoorButton_L1.enabled = true;
                }

                // Check if already displayed
                if (DoorButton_L4.isActiveAndEnabled)
                {
                    DoorButton_L4.enabled = false;
                }
            }

            OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

            LeftLightIsOn = false;
        }
        else
        {
            if (!BonnieOutsideDoor)
            {
                // Check if already displayed
                if (!Light_L_No_Door.isActiveAndEnabled)
                {
                    Light_L_No_Door.enabled = true;
                }

                Light.Play();
                LeftScareAlrdPlayed = false;
            }

            if (BonnieOutsideDoor)
            {
                // Check if already displayed
                if (!Light_L_Door_Bonnie.isActiveAndEnabled)
                {
                    Light_L_Door_Bonnie.enabled = true;
                }

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

            // Check if already displayed
            if (OriginalOfficeImage.isActiveAndEnabled)
            {
                OriginalOfficeImage.enabled = false;
            }

            OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

            // Check if already displayed
            if (!DoorButton_L3.isActiveAndEnabled)
            {
                DoorButton_L3.enabled = true;
            }

            if (L_Door_Closed)
            {
                // Check if already displayed
                if (DoorButton_L1.isActiveAndEnabled)
                {
                    DoorButton_L1.enabled = false;
                }

                // Check if already displayed
                if (!DoorButton_L4.isActiveAndEnabled)
                {
                    DoorButton_L4.enabled = true;
                }
            }

            LeftLightIsOn = true;
        }
    }

    void RightDoorAndLightManager()
    {
        if (RightLightIsOn)
        {
            if (!ChicaOutsideDoor)
            {
                // Check if already displayed
                if (Light_R_No_Door.isActiveAndEnabled)
                {
                    Light_R_No_Door.enabled = false;
                }

                Light.Pause();
            }

            if (ChicaOutsideDoor)
            {
                // Check if already displayed
                if (Light_R_Door_Chica.isActiveAndEnabled)
                {
                    Light_R_Door_Chica.enabled = false;
                }

                Light.Pause();
            }

            // Check if already displayed
            if (!OriginalOfficeImage.isActiveAndEnabled)
            {
                OriginalOfficeImage.enabled = true;
            }

            // Check if already displayed
            if (DoorButton_R3.isActiveAndEnabled)
            {
                DoorButton_R3.enabled = false;
            }

            if (R_Door_Closed)
            {
                // Check if already displayed
                if (!DoorButton_R1.isActiveAndEnabled)
                {
                    DoorButton_R1.enabled = true;
                }

                // Check if already displayed
                if (DoorButton_R4.isActiveAndEnabled)
                {
                    DoorButton_R4.enabled = false;
                }
            }

            OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;

            RightLightIsOn = false;
        }
        else
        {
            if (!ChicaOutsideDoor)
            {
                // Check if already displayed
                if (!Light_R_No_Door.isActiveAndEnabled)
                {
                    Light_R_No_Door.enabled = true;
                }

                Light.Play();
                RightScareAlrdPlayed = false;
            }

            if (ChicaOutsideDoor)
            {
                // Check if already displayed
                if (!Light_R_Door_Chica.isActiveAndEnabled)
                {
                    Light_R_Door_Chica.enabled = true;
                }

                Light.Play();

                if (!R_Door_Closed && RightScareAlrdPlayed == false)
                {
                    RightScareAlrdPlayed = true;
                    Scare.Play();
                }

            }

            OfficeControllerObject.GetComponent<GameScript>().PowerUsage += 1;

            // Check if already displayed
            if (!DoorButton_R3.isActiveAndEnabled)
            {
                DoorButton_R3.enabled = true;
            }

            if (R_Door_Closed)
            {
                // Check if already displayed
                if (DoorButton_R1.isActiveAndEnabled)
                {
                    DoorButton_R1.enabled = false;
                }

                // Check if already displayed
                if (!DoorButton_R4.isActiveAndEnabled)
                {
                    DoorButton_R4.enabled = true;
                }
            }

            RightLightIsOn = true;
        }
    }

    void CharactersMovement()
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