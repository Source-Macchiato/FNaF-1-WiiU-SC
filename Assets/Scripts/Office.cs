using UnityEngine;
using UnityEngine.UI;
using WiiU = UnityEngine.WiiU;

public class Office : MonoBehaviour {

    public GameObject CheatPanel;
    private Movement movementScript;
    private bool LeftScareAlrdPlayed = false;
    private bool RightScareAlrdPlayed = false;

    public GameObject OfficeImage;

    [HideInInspector]
    public RectTransform officeBounds;

    public GameObject OfficeControllerObject;

    [Header("Doors Images")]
    public Image Door_L_closed;
    public Image Door_L_open;
    public Image Door_R_closed;
    public Image Door_R_open;

    [Header("Animators")]
    private Animator[] animators;
    public Animator leftDoorCloseAnimator;
    public Animator leftDoorOpenAnimator;
    public Animator rightDoorCloseAnimator;
    public Animator rightDoorOpenAnimator;

    [Header("Images")]
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

    [Header("Audios")]
    public AudioSource DoorClose;
    public AudioSource lightSound;
    public AudioSource Scare;

    private const float speed = 5f;
    private const float leftEdge = 160f;
    private const float rightEdge = -130f;

    public bool BonnieOutsideDoor = false;
    public bool ChicaOutsideDoor = false;
    public bool FreddyOutsideDoor = false;
    public bool FoxyRunningHallway = false;

    public bool leftLightIsOn = false;
    public bool rightLightIsOn = false;

    private bool previousLeftLightState = false;
    private bool previousRightLightState = false;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    private float joystickThreshold = 0.5f;

    public float centerPosition;

    ChangeImages changeImages;
    MoveInOffice moveInOffice;

    void Start()
    {
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        changeImages = FindObjectOfType<ChangeImages>();
        moveInOffice = FindObjectOfType<MoveInOffice>();

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

        Door_L_closed.enabled = false;
        Door_R_closed.enabled = false;

        officeBounds = OfficeImage.GetComponent<RectTransform>();

        animators = FindObjectsOfType<Animator>();
    }

    void Update()
    {
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;
        foreach (Animator anim in animators)
        {
            if (anim.gameObject.name.Contains("Office") || anim.gameObject.name.Contains("office"))
            {
                anim.enabled = true;
            }
        }

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
                        if (moveInOffice.isPointerDisplayed)
                        {
                            if (moveInOffice.lastPointerPosition.y > (WiiU.Core.GetScreenHeight(WiiU.DisplayIndex.TV) / 2) - 55)
                            {
                                LeftDoorSystem();
                            }
                        }
                        else
                        {
                            LeftDoorSystem();
                        }
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
                    ToggleLeftLight();
                }
            }

            // Remote
            switch (remoteState.devType)
            {
                case WiiU.RemoteDevType.ProController:
                    if (remoteState.pro.IsTriggered(WiiU.ProControllerButton.B))
                    {
                        ToggleLeftLight();
                    }
                    break;
                case WiiU.RemoteDevType.Classic:
                    if (remoteState.classic.IsTriggered(WiiU.ClassicButton.B))
                    {
                        ToggleLeftLight();
                    }
                    break;
                default:
                    if (moveInOffice.isPointerDisplayed)
                    {
                        if (remoteState.IsTriggered(WiiU.RemoteButton.A))
                        {
                            if (moveInOffice.lastPointerPosition.y <= (WiiU.Core.GetScreenHeight(WiiU.DisplayIndex.TV) / 2) - 55)
                            {
                                ToggleLeftLight();
                            }
                        }
                    }
                    else
                    {
                        if (remoteState.IsTriggered(WiiU.RemoteButton.B))
                        {
                            ToggleLeftLight();
                        }
                    }
                    break;
            }

            // Keyboard
            if (Application.isEditor)
            {
                if (Input.GetKeyDown(KeyCode.B))
                {
                    ToggleLeftLight();
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
                    ToggleRightLight();
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
                        ToggleRightLight();
                    }
                    break;
                case WiiU.RemoteDevType.Classic:
                    if (remoteState.classic.IsTriggered(WiiU.ClassicButton.A))
                    {
                        RightDoorSystem();
                    }

                    if (remoteState.classic.IsTriggered(WiiU.ClassicButton.B))
                    {
                        ToggleRightLight();
                    }
                    break;
                default:
                    if (remoteState.IsTriggered(WiiU.RemoteButton.A))
                    {
                        if (moveInOffice.isPointerDisplayed)
                        {
                            if (moveInOffice.lastPointerPosition.y > (WiiU.Core.GetScreenHeight(WiiU.DisplayIndex.TV) / 2) - 55)
                            {
                                RightDoorSystem();
                            }
                            else
                            {
                                ToggleRightLight();
                            }
                        }
                        else
                        {
                            RightDoorSystem();
                        }
                    }

                    if (remoteState.IsTriggered(WiiU.RemoteButton.B))
                    {
                        if (!moveInOffice.isPointerDisplayed)
                        {
                            ToggleRightLight();
                        }
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
                    ToggleRightLight();
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

        LeftLightSystem();
        RightLightSystem();
    }

    public void ToggleLeftLight()
    {
        leftLightIsOn = !leftLightIsOn;

        if (rightLightIsOn)
        {
            rightLightIsOn = false;
        }

        LightPowerUsage();
        LightSound();
        OfficeImageState();
    }

    public void ToggleRightLight()
    {
        rightLightIsOn = !rightLightIsOn;

        if (leftLightIsOn)
        {
            leftLightIsOn = false;
        }

        LightPowerUsage();
        LightSound();
        OfficeImageState();
    }

    private void LightPowerUsage()
    {
        GameScript gameScript = OfficeControllerObject.GetComponent<GameScript>();

        // Check the status of the left light and update the energy only if the status changes
        if (leftLightIsOn != previousLeftLightState)
        {
            if (leftLightIsOn)
            {
                gameScript.PowerUsage += 1;
            }
            else
            {
                gameScript.PowerUsage -= 1;
            }
            previousLeftLightState = leftLightIsOn;
        }

        // Check the status of the right light and update the energy only if the status changes
        if (rightLightIsOn != previousRightLightState)
        {
            if (rightLightIsOn)
            {
                gameScript.PowerUsage += 1;
            }
            else
            {
                gameScript.PowerUsage -= 1;
            }
            previousRightLightState = rightLightIsOn;
        }
    }

    private void LightSound()
    {
        if (!leftLightIsOn && !rightLightIsOn)
        {
            if (lightSound.isPlaying)
            {
                lightSound.Stop();
            }
        }
        else
        {
            if (!lightSound.isPlaying)
            {
                lightSound.Play();
            }
        }
    }

    private void OfficeImageState()
    {
        if (!leftLightIsOn && !rightLightIsOn)
        {
            if (!OriginalOfficeImage.isActiveAndEnabled)
            {
                OriginalOfficeImage.enabled = true;
            }
        }
        else
        {
            if (OriginalOfficeImage.isActiveAndEnabled)
            {
                OriginalOfficeImage.enabled = false;
            }
        }
    }

    private void LeftLightSystem()
    {
        if (leftLightIsOn)
        {
            // --- When the left light is enabled ---

            if (BonnieOutsideDoor)
            {
                // Enable Bonnie
                if (!Light_L_Door_Bonnie.isActiveAndEnabled)
                {
                    Light_L_Door_Bonnie.enabled = true;
                }

                // Disable light
                if (Light_L_No_Door.isActiveAndEnabled)
                {
                    Light_L_No_Door.enabled = false;
                }


                if (!L_Door_Closed && LeftScareAlrdPlayed == false)
                {
                    if (!Scare.isPlaying)
                    {
                        Scare.Play();
                    }

                    LeftScareAlrdPlayed = true;
                }
            }
            else
            {
                // Disable Bonnie
                if (Light_L_Door_Bonnie.isActiveAndEnabled)
                {
                    Light_L_Door_Bonnie.enabled = false;
                }

                // Enable Light
                if (!Light_L_No_Door.isActiveAndEnabled)
                {
                    Light_L_No_Door.enabled = true;
                }

                
                LeftScareAlrdPlayed = false;
            }

            // Check if already displayed
            if (OriginalOfficeImage.isActiveAndEnabled)
            {
                OriginalOfficeImage.enabled = false;
            }

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
        }
        else
        {
            // --- When the left light is disabled ---

            // Disable Bonnie or light image if Bonnie is not here
            if (BonnieOutsideDoor)
            {
                if (Light_L_Door_Bonnie.isActiveAndEnabled)
                {
                    Light_L_Door_Bonnie.enabled = false;
                }
            }
            else
            {
                if (Light_L_No_Door.isActiveAndEnabled)
                {
                    Light_L_No_Door.enabled = false;
                }
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
        }
    }

    private void RightLightSystem()
    {
        if (rightLightIsOn)
        {
            // --- When the left light is enabled ---

            if (ChicaOutsideDoor)
            {
                // Check if already displayed
                if (!Light_R_Door_Chica.isActiveAndEnabled)
                {
                    Light_R_Door_Chica.enabled = true;
                }

                if (Light_R_No_Door.isActiveAndEnabled)
                {
                    Light_R_No_Door.enabled = false;
                }

                if (!R_Door_Closed && RightScareAlrdPlayed == false)
                {
                    if (!Scare.isPlaying)
                    {
                        Scare.Play();
                    }

                    RightScareAlrdPlayed = true;
                }
            }
            else
            {
                // Check if already displayed
                if (Light_R_Door_Chica.isActiveAndEnabled)
                {
                    Light_R_Door_Chica.enabled = false;
                }

                if (!Light_R_No_Door.isActiveAndEnabled)
                {
                    Light_R_No_Door.enabled = true;
                }

                RightScareAlrdPlayed = false;
            }

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
        }
        else
        {
            // --- When the left light is disabled ---

            if (ChicaOutsideDoor)
            {
                // Check if already displayed
                if (Light_R_Door_Chica.isActiveAndEnabled)
                {
                    Light_R_Door_Chica.enabled = false;
                }
            }
            else
            {
                // Check if already displayed
                if (Light_R_No_Door.isActiveAndEnabled)
                {
                    Light_R_No_Door.enabled = false;
                }
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
        }
    }

    private void LeftDoorSystem()
    {
        if (L_Door_Closed)
        {
            Door_L_closed.enabled = false;
            Door_L_open.enabled = true;

            leftDoorOpenAnimator.Play("Base Layer.DooOpen", 0, 0);

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
            Door_L_closed.enabled = true;
            Door_L_open.enabled = false;

            leftDoorCloseAnimator.Play("Base Layer.Door_close", 0, 0);

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

    private void RightDoorSystem()
    {
        if (R_Door_Closed)
        {
            Door_R_closed.enabled = false;
            Door_R_open.enabled = true;

            rightDoorOpenAnimator.Play("Base Layer.R_door_opened", 0, 0);

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
            if (DoorButton_L4.isActiveAndEnabled)
            {
                DoorButton_L4.enabled = false;
            }

            DoorClose.Play();

            OfficeControllerObject.GetComponent<GameScript>().PowerUsage -= 1;
        }
        else
        {
            Door_R_closed.enabled = true;
            Door_R_open.enabled = false;

            rightDoorCloseAnimator.Play("Base Layer.R_door_closed", 0, 0);

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