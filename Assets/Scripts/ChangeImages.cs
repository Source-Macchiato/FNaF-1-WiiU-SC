using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeImages : MonoBehaviour
{
    public bool CanEnableLight = true;
    private Animator foxyAnimator;
    public bool FoxyAnimationStarted = false;
    public float FoxyAnimationTimer = 0.50f;
    public float WaitPlayerToNotice = 6f;
    public bool PlayerSawFoxy = false;
    [SerializeField] private GameObject[] kitckenAudioOnly;
    private int currentCamera = 1;
    public GameObject cameraScreen;

    public I18nTextTranslator i18nTextTranslator;
    private ControllersRumble controllersRumble;
    private MoveInOffice moveInOffice;
    private CameraScript cameraScript;
    private Office office;
    private Movement movement;

    // -----------DiningArea var----------

    [Header("Dining Area sprites")]
    public Sprite DiningAreaEmptyDefault;
    public Sprite DiningAreaBonnieDefault;
    public Sprite DiningAreaChicaDefault;
    public Sprite DiningAreaFreddyDefault;

    [Header("Show Stage sprites")]
    public Sprite ShowStageBonnieChicaFreddyDefault;
    public Sprite ShowStageChicaFreddyDefault;
    public Sprite ShowStageBonnieFreddyDefault;
    public Sprite ShowStageFreddyDefault;
    public Sprite ShowStageFreddyEasterEgg;
    public Sprite ShowStageEmptyDefault;

    [Header("Pirate Cove sprites")]
    public Sprite PirateCovePhase1Default;
    public Sprite PirateCovePhase2Default;
    public Sprite PirateCovePhase3Default;
    public Sprite PirateCovePhase4Default;

    [Header("West Hall sprites")]
    public Sprite WestHallDefault;
    public Sprite WestHallBonnieDefault;

    [Header("West Hall Corner sprites")]
    public Sprite WestHallCornerDefault;
    public Sprite WestHallCornerEasterEgg;
    public Sprite WestHallCornerGoldenFreddy;
    public Sprite WestHallCornerBonnieDefault;

    [Header("Closet sprites")]
    public Sprite ClosetEmptyDefault;
    public Sprite ClosetBonnieDefault;

    [Header("East Hall sprites")]
    public Sprite EastHallEmptyDefault;
    public Sprite EastHallChicaPhase1Default;
    public Sprite EastHallChicaPhase2Default;
    public Sprite EastHallFreddyDefault;

    [Header("East Hall Corner sprites")]
    public Sprite EastHallCornerEmptyDefault;
    public Sprite EastHall2_2;
    public Sprite EastHall2_3;

    [Header("Back Stage sprites")]
    public Sprite BackStageEmptyDefault;
    public Sprite BackStageEmptyEasterEgg;
    public Sprite BackStageBonnieDefault;
    public Sprite BackStageBonnieEasterEgg;

    [Header("Kitchen sprites")]
    public Sprite KitchenEmptyDefault;

    [Header("Rest Rooms")]
    public Sprite RestRoomsEmptyDefault;
    public Sprite RestRoomsChicaPhase1Default;
    public Sprite RestRoomsChicaPhase2Default;
    public Sprite RestRoomsFreddyDefault;

    private Sprite currentSprite;

    public double RandCamNoise;
    public bool noiseIsPlaying;

    [Header("Golden Freddy stuff")]
    public int GoldenFreddyChance;
    public bool GoldenFreddyActive;
    public GameObject GoldenFreddyOffice;
    public float GoldenFreddyJumpscareTime = 10f;
    public float GoldenFreddyDoJumpscare = 5f;
    public int GoldenFreddyRange = 0;
    
    public GameObject GoldenFreddyLaugh;
    public GameObject GoldenFreddyJumpscare;
    private bool hasGeneratedGFNumber = false;
    public GameObject Itsme;
    public AudioSource ItsmeSound;

    public bool BonnieLeft = false;
    public bool ChicaLeft = false;
    public bool FreddyLeft = false;

    public GameObject BonnieJumpscare;
    public GameObject ChicaJumpscare;
    public GameObject FreddyJumpscare;

    public float WaitJumpscare = 2;
    public bool isBeingJumpscared = false;

    public GameObject OfficeObject;

    public float AmountLeft = 0;

    public bool camIsUp = false;

    public GameObject AudioSources;

    public GameObject OriginalOfficeImage;
    public GameObject FoxyEnterOffice;

    public GameObject FoxyRunDownHall;
    public AudioSource DoorBang;
    public AudioSource FoxyFootsteps;

    public bool FoxyAnimationPlayed = false;
    public float foxyRunTime = 3.5f;

    public bool foxyStarted;

    void GenRandNoise()
    {
        if (!noiseIsPlaying)
        {
            RandCamNoise = System.Math.Round(Random.Range(0f, 10f), 0);
        }
    }

    void Start()
    {
        foxyAnimator = FoxyRunDownHall.GetComponent<Animator>();
        controllersRumble = FindObjectOfType<ControllersRumble>();
        moveInOffice = FindObjectOfType<MoveInOffice>();
        cameraScript = FindObjectOfType<CameraScript>();
        office = FindObjectOfType<Office>();
        movement = FindObjectOfType<Movement>();
        
        GoldenFreddyJumpscareTime = 10f;
        GoldenFreddyOffice.SetActive(false);
        GoldenFreddyLaugh.SetActive(false);

        foxyRunTime = 3.5f;
        GoldenFreddyRange = Movement.goldenDifficulty * 600;
    }

    void Update()
    {
        // Get current sprite and assign it in a local variable
        currentSprite = cameraScreen.GetComponent<Image>().sprite;

        if(GoldenFreddyActive && !camIsUp)
        {
            ItsmeSound.Play();
            Itsme.SetActive(true);
            GoldenFreddyJumpscareTime -= Time.deltaTime;
            if(GoldenFreddyJumpscareTime <= 0)
            {
                controllersRumble.TriggerRumble(30, "Golden Freddy");
                GoldenFreddyJumpscare.SetActive(true);
                GoldenFreddyJumpscareTime = 10f;
            }
            if(GoldenFreddyJumpscare.activeInHierarchy)
            {
                GoldenFreddyDoJumpscare -= Time.deltaTime;
            }
            if(GoldenFreddyDoJumpscare <= 0f)
            {
                // The game is stopped
                Application.Quit();
            }
        }
        else if (camIsUp)
        {
            GoldenFreddyActive = false;
        }

        if (!GoldenFreddyActive)
        {
            GoldenFreddyOffice.SetActive(false);
        }
        if (camIsUp)
        {
            if(!hasGeneratedGFNumber)
            {
                GenerateGoldenFreddyChance();
                hasGeneratedGFNumber = true;
                
            }
            
            if(GoldenFreddyChance == GoldenFreddyRange && currentCamera == 5)
            {
                GoldenFreddyActive = true;
                GoldenFreddyOffice.SetActive(true);
            }
            else
            {
                GoldenFreddyActive = false;
                GoldenFreddyOffice.SetActive(false);
            }
            if (currentCamera == 1)
            {
                i18nTextTranslator.textId = "camera.showstage";
                i18nTextTranslator.UpdateText();

                if (currentSprite != ShowStageBonnieChicaFreddyDefault)
                {
                    currentSprite = ShowStageBonnieChicaFreddyDefault;
                }

                if (movement.bonniePosition >= 2)
                {
                    if (!BonnieLeft)
                    {
                        AmountLeft += 1;

                        BonnieLeft = true;
                    }
                }

                if (movement.chicaPosition >= 2)
                {
                    if (BonnieLeft)
                    {
                        if (!ChicaLeft)
                        {
                            AmountLeft += 1;

                            ChicaLeft = true;
                        }
                    }
                }

                if (movement.freddyPosition >= 2)
                {
                    if (BonnieLeft)
                    {
                        if (ChicaLeft)
                        {
                            if (!FreddyLeft)
                            {
                                AmountLeft += 1;

                                FreddyLeft = true;
                            }
                        }
                    }
                }

                // Chica and Freddy at Show Stage
                if (AmountLeft == 1 && movement.chicaPosition == 1)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != ShowStageChicaFreddyDefault)
                    {
                        currentSprite = ShowStageChicaFreddyDefault;
                    }
                }
                else if(movement.bonniePosition == 1 && movement.freddyPosition == 1 && movement.chicaPosition != 1)
                {
                    // Check if sprite is already displayed
                    if(currentSprite != ShowStageBonnieFreddyDefault)
                    {
                        currentSprite = ShowStageBonnieFreddyDefault;
                    }
                }

                // Freddy at Show Stage
                if (AmountLeft == 2)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != ShowStageFreddyDefault && currentSprite != ShowStageFreddyEasterEgg)
                    {
                        if (EasterEgg(100))
                        {
                            currentSprite = ShowStageFreddyEasterEgg;
                        }
                        else
                        {
                            currentSprite = ShowStageFreddyDefault;
                        }
                    }
                }

                // Empty Show Stage
                if (AmountLeft == 3)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != ShowStageEmptyDefault)
                    {
                        currentSprite = ShowStageEmptyDefault;
                    }
                }
            }

            //-----------------dining Area-------------------
            if (currentCamera == 2)
            {
                i18nTextTranslator.textId = "camera.diningarea";
                i18nTextTranslator.UpdateText();

                // Bonnie at Dining Area
                if (movement.bonniePosition == 2)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != DiningAreaBonnieDefault)
                    {
                        currentSprite = DiningAreaBonnieDefault;
                    }
                }

                // Chica at Dining Area
                if (movement.chicaPosition == 2)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != DiningAreaChicaDefault)
                    {
                        currentSprite = DiningAreaChicaDefault;
                    }
                }

                // Freddy at Dining Area
                if (movement.freddyPosition == 2)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != DiningAreaFreddyDefault)
                    {
                        currentSprite = DiningAreaFreddyDefault;
                    }

                }

                // Empty Dining Area
                if (movement.freddyPosition != 2 && movement.bonniePosition != 2 && movement.chicaPosition != 2)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != DiningAreaEmptyDefault)
                    {
                        currentSprite = DiningAreaEmptyDefault;
                    }
                }

            }

            if (currentCamera == 3)
            {
                i18nTextTranslator.textId = "camera.piratecove";
                i18nTextTranslator.UpdateText();

                // Foxy phase 1
                if (movement.foxyPosition == 1)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != PirateCovePhase1Default)
                    {
                        currentSprite = PirateCovePhase1Default;
                    }
                }

                // Foxy phase 2
                if (movement.foxyPosition == 2)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != PirateCovePhase2Default)
                    {
                        currentSprite = PirateCovePhase2Default;
                    }
                }

                // Foxy phase 3
                if (movement.foxyPosition == 3)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != PirateCovePhase3Default)
                    {
                        currentSprite = PirateCovePhase3Default;
                    }
                }
                if (movement.foxyPosition == 4)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != PirateCovePhase4Default)
                    {
                        currentSprite = PirateCovePhase4Default;
                    }
                }
            }

            if (currentCamera == 4)
            {
                i18nTextTranslator.textId = "camera.westhall";
                i18nTextTranslator.UpdateText();

                // Does Bonnie is at West Hall
                if (movement.bonniePosition == 5)
                {
                    // Check if sprite is alrady displayed
                    if (currentSprite != WestHallBonnieDefault)
                    {
                        currentSprite = WestHallBonnieDefault;
                    }
                }
                else
                {
                    // Check if sprite is already displayed
                    if (currentSprite != WestHallDefault)
                    {
                        currentSprite = WestHallDefault;
                    }
                }
            }

            if (currentCamera == 5)
            {
                i18nTextTranslator.textId = "camera.westhallcorner";
                i18nTextTranslator.UpdateText();

                if (movement.bonniePosition == 6)
                {
                    // Check if sprite is alraedy displayed
                    if (currentSprite != WestHallCornerBonnieDefault)
                    {
                        currentSprite = WestHallCornerBonnieDefault;
                    }
                }
                else
                {
                    // Check if sprite is already displayed
                    if (currentSprite != WestHallCornerDefault && currentSprite != WestHallCornerEasterEgg && !GoldenFreddyActive)
                    {
                        if (EasterEgg(100))
                        {
                            currentSprite = WestHallCornerEasterEgg;
                        }
                        else
                        {
                            currentSprite = WestHallCornerDefault;
                        }
                    }
                    if(GoldenFreddyActive && currentSprite != WestHallBonnieDefault)
                    {
                        currentSprite = WestHallCornerGoldenFreddy;
                    }
                    else{
                        currentSprite = WestHallCornerDefault;
                    }
                }
            }

            if (currentCamera == 6)
            {
                i18nTextTranslator.textId = "camera.supplycloset";
                i18nTextTranslator.UpdateText();

                // Does Bonnie is at Closet
                if (movement.bonniePosition == 4)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != ClosetBonnieDefault)
                    {
                        currentSprite = ClosetBonnieDefault;
                    }
                }
                else
                {
                    // Check if sprite is already displayed
                    if (currentSprite != ClosetEmptyDefault)
                    {
                        currentSprite = ClosetEmptyDefault;
                    }
                }
            }

            if (currentCamera == 7)
            {
                i18nTextTranslator.textId = "camera.easthall";
                i18nTextTranslator.UpdateText();

                // Chica at East Hall
                if (movement.chicaPosition == 5)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != EastHallChicaPhase1Default)
                    {
                        currentSprite = EastHallChicaPhase1Default;
                    }
                }
                else if (movement.chicaPosition == 6)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != EastHallChicaPhase2Default)
                    {
                        currentSprite = EastHallChicaPhase2Default;
                    }
                }

                // Freddy at East Hall
                if (movement.freddyPosition == 5)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != EastHallFreddyDefault)
                    {
                        currentSprite = EastHallFreddyDefault;
                    }
                }

                // East Hall empty
                if (movement.chicaPosition != 5 && movement.chicaPosition != 6 && movement.freddyPosition != 5)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != EastHallEmptyDefault)
                    {
                        currentSprite = EastHallEmptyDefault;
                    }
                }
            }

            if (currentCamera == 8)
            {
                i18nTextTranslator.textId = "camera.easthallcorner";
                i18nTextTranslator.UpdateText();

                if (movement.chicaPosition == 7)
                {
                    if (movement.freddyPosition <= 1)
                    {
                        currentSprite = EastHall2_2;
                    }
                }
                if (movement.freddyPosition == 6)
                {
                    if (movement.chicaPosition != 6)
                    {
                        currentSprite = EastHall2_3;
                    }
                }

                // East Hall Corner empty
                if (movement.chicaPosition != 7 && movement.freddyPosition != 6)
                {
                    if (currentSprite != EastHallCornerEmptyDefault)
                    {
                        currentSprite = EastHallCornerEmptyDefault;
                    }
                }
            }

            if (currentCamera == 9)
            {
                i18nTextTranslator.textId = "camera.backstage";
                i18nTextTranslator.UpdateText();

                // Does Bonnis is at Back Stage
                if (movement.bonniePosition == 3)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != BackStageBonnieDefault && currentSprite != BackStageBonnieEasterEgg)
                    {
                        if (EasterEgg(100))
                        {
                            currentSprite = BackStageBonnieEasterEgg;
                        }
                        else
                        {
                            currentSprite = BackStageBonnieDefault;
                        }
                    }
                }
                else
                {
                    // Check if sprite is already displayed
                    if (currentSprite != BackStageEmptyDefault && currentSprite != BackStageEmptyEasterEgg)
                    {
                        if (EasterEgg(100))
                        {
                            currentSprite = BackStageEmptyEasterEgg;
                        }
                        else
                        {
                            currentSprite = BackStageEmptyDefault;
                        }
                    }
                }
            }

            if (currentCamera == 10)
            {
                i18nTextTranslator.textId = "camera.kitchen";
                i18nTextTranslator.UpdateText();

                // Enable kitchen audio only if gameobject is active
                foreach (GameObject kao in kitckenAudioOnly)
                {
                    if (kao.activeSelf)
                    {
                        Image kaoImage = kao.GetComponent<Image>();

                        if (kaoImage != null)
                        {
                            kaoImage.enabled = true;
                        }
                    }
                }

                if (movement.chicaPosition == 5)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != KitchenEmptyDefault)
                    {
                        currentSprite = KitchenEmptyDefault;
                    }
                }
                else
                {
                    // Check if sprite is already displayed
                    if (currentSprite != KitchenEmptyDefault)
                    {
                        currentSprite = KitchenEmptyDefault;
                    }
                }
            }
            else
            {
                // Disable kitchen audio only if gameobject is active
                foreach (GameObject kao in kitckenAudioOnly)
                {
                    if (kao.activeSelf)
                    {
                        Image kaoImage = kao.GetComponent<Image>();

                        if (kaoImage != null)
                        {
                            kaoImage.enabled = false;
                        }
                    }
                }
            }
        }
        else
        {
            // Disable kitchen audio only if gameobject is active
            foreach (GameObject kao in kitckenAudioOnly)
            {
                if (kao.activeSelf)
                {
                    Image kaoImage = kao.GetComponent<Image>();
                    
                    if (kaoImage != null)
                    {
                        kaoImage.enabled = false;
                    }
                }
            }
        }

        if(!camIsUp)
        {
            hasGeneratedGFNumber = false;
        }

        if (currentCamera == 11)
        {
            i18nTextTranslator.textId = "camera.restrooms";
            i18nTextTranslator.UpdateText();

            // Chica is at Rest Rooms
            if (movement.chicaPosition == 3)
            {
                // Check if sprite is already displayed
                if (currentSprite != RestRoomsChicaPhase1Default)
                {
                    currentSprite = RestRoomsChicaPhase1Default;
                }
            }
            else if (movement.chicaPosition == 4)
            {
                // Check if sprite is already displayed
                if (currentSprite != RestRoomsChicaPhase2Default)
                {
                    currentSprite = RestRoomsChicaPhase2Default;
                }
            }

            // Freddy is at Rest Rooms
            if (movement.freddyPosition == 3)
            {
                // Check if sprite is already displayed
                if (currentSprite != RestRoomsFreddyDefault)
                {
                    currentSprite = RestRoomsFreddyDefault;
                }
            }

            // Rest Rooms empty
            if (movement.chicaPosition !=3 && movement.chicaPosition !=4 && movement.freddyPosition !=3)
            {
                // Check if sprite is already displayed
                if (currentSprite != RestRoomsEmptyDefault)
                {
                    currentSprite = RestRoomsEmptyDefault;
                }
            }
        }

        // where is Bonnie system
        if (movement.bonniePosition < 7)
        {
            OfficeObject.GetComponent<RandNumberGen>().BonnieOutsideLeftDoor = false;
        }


        if (movement.bonniePosition >= 7)
        {
            movement.BonnieOutsideDoor = true;
            OfficeObject.GetComponent<RandNumberGen>().BonnieOutsideLeftDoor = true;
        }

        if (movement.bonniePosition >= 8 && camIsUp)
        {
            BonnieJumpscare.SetActive(true);
            isBeingJumpscared = true;

            controllersRumble.TriggerRumble(30, "Bonnie");
        }
        // where is Chica system
        if (movement.chicaPosition < 8)
        {
            OfficeObject.GetComponent<RandNumberGen>().ChicaOutsideRightDoor = false;
        }

        if (movement.chicaPosition >= 8)
        {
            movement.ChicaOutsideDoor = true;
            OfficeObject.GetComponent<RandNumberGen>().ChicaOutsideRightDoor = true;
        }
        if (movement.chicaPosition >= 9 && camIsUp)
        {
            if (camIsUp)
            {
                ChicaJumpscare.SetActive(true);
                isBeingJumpscared = true;

                controllersRumble.TriggerRumble(30, "Chica");
            }
        }

        // where is Freddy system
        if (movement.freddyPosition >= 6)
        {
            movement.FreddyOutsideDoor = true;
            OfficeObject.GetComponent<RandNumberGen>().FreddyOutsideRightDoor = true;
        }

        if (movement.freddyPosition >= 7)
        {
            FreddyJumpscare.SetActive(true);

            isBeingJumpscared = true;

            controllersRumble.TriggerRumble(30, "Freddy");
        }

        if (movement.foxyPosition >= 4)
        {
            foxyStarted = true;

            if (foxyStarted)
            {
                WaitPlayerToNotice -= Time.deltaTime;
                FoxyFootsteps.Play();
                FoxyRunDownHall.SetActive(true);
                FoxyAnimationPlayed = true;
                
                if (currentCamera == 4)
                {
                    FoxyAnimationTimer -= Time.deltaTime;
                    PlayerSawFoxy = true;
                }
                if (PlayerSawFoxy)
                {
                    WaitPlayerToNotice = 0f;
                }
                if (FoxyAnimationTimer <= 0f)
                {
                    FoxyRunDownHall.SetActive(false);
                    foxyAnimator.enabled = false;
                }
                if (WaitPlayerToNotice <= 0)
                {
                    foxyRunTime -= Time.deltaTime;

                    if (foxyRunTime <= 0)
                    {
                        if (!office.leftDoorClosed)
                        {
                            FoxyEnterOffice.SetActive(true);
                            FoxyRunDownHall.SetActive(false);

                            isBeingJumpscared = true;

                            controllersRumble.TriggerRumble(30, "Foxy");
                        }
                        else
                        {
                            DoorBang.Play();
                            movement.foxyPosition = 1;
                            movement.foxyInCount = false;
                            //OfficeObject.GetComponent<Movement>().GenNumber();
                            OriginalOfficeImage.GetComponent<Image>().enabled = true;
                            FoxyRunDownHall.SetActive(false);
                            foxyRunTime = 3.5f;
                            foxyStarted = false;
                        }
                    }
                }
            }
            else if (!camIsUp)
            {
                FoxyRunDownHall.SetActive(false);
            }
            if (currentCamera != 4)
            {
                FoxyRunDownHall.SetActive(false);
            }
        }

        // When jumpscare
        if (isBeingJumpscared)
        {
            if (moveInOffice.canMove)
            {
                moveInOffice.canMove = false;
            }

            // Disable audios
            AudioSources.SetActive(false);

            if (cameraScript.canUseCamera)
            {
                cameraScript.canUseCamera = false;
            }

            if (cameraScript.camIsUp)
            {
                cameraScript.CameraSystem();
            }            

            WaitJumpscare -= Time.deltaTime;

            if (WaitJumpscare <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        // Refresh cameraScreen with currentSprite
        cameraScreen.GetComponent<Image>().sprite = currentSprite;
    }

    public void GenerateGoldenFreddyChance()
    {
        GoldenFreddyChance = Random.Range(GoldenFreddyRange, 491520);
        Debug.Log("GoldenFreddy chance generated : "+ GoldenFreddyChance);

    }

    public void cam1a()
    {
        currentCamera = 1;
    }

    public void cam1b()
    {
        currentCamera = 2;
    }

    public void cam1c()
    {
        currentCamera = 3;
    }

    public void cam2a()
    {
        currentCamera = 4;
    }

    public void cam2b()
    {
        currentCamera = 5;
    }

    public void cam3()
    {
        currentCamera = 6;
    }

    public void cam4a()
    {
        currentCamera = 7;
    }

    public void cam4b()
    {
        currentCamera = 8;
    }

    public void cam5()
    {
        currentCamera = 9;
    }

    public void cam6()
    {
        currentCamera = 10;
    }

    public void cam7()
    {
        currentCamera = 11;
    }
    bool EasterEgg(int max)
    {
        int randomNumber = Random.Range(0, max);

        return randomNumber == 0;
    }
}