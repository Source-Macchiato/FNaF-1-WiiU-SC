using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeImages : MonoBehaviour
{
    private Animator foxyAnimator;
    public bool FoxyAnimationStarted = false;
    public float FoxyAnimationTimer = 0.50f;
    public GameObject KitckenAudioOnly;
    public float WhichCamera = 1;
    public GameObject WichCameraShower;
    public GameObject cameraScreen;
    public GameObject CanvasGameOver;
    public GameObject GameOverScript;

    public I18nTextTranslator i18nTextTranslator;
    private ControllersRumble controllersRumble;

    // -----------DiningArea var----------

    [Header("Dining Area sprites")]
    public Sprite DiningAreaEmptyDefault;
    public Sprite DiningAreaBonnieDefault;
    public Sprite DiningAreaChicaDefault;
    public Sprite DiningAreaFreddyDefault;

    [Header("Show Stage sprites")]
    public Sprite ShowStageBonnieChicaFreddyDefault;
    public Sprite ShowStageChicaFreddyDefault;
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

    public float WhereBonnie = 1;
    public float WhereChica = 1;
    public float WhereFreddy = 1;
    public float WhereFoxy = 1;

    public int GoldenFreddyChance;
    public bool GoldenFreddyActive;
    public GameObject GoldenFreddyOffice;
    public float GoldenFreddyJumpscareTime = 10f;
    public float GoldenFreddyDoJumpscare = 5f;
    public GameObject GoldenFreddyLaugh;
    public GameObject GoldenFreddyJumpscare;
    private bool hasGeneratedGFNumber = false;
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

    public GameObject LowerCanvas;
    public GameObject Phonecalls;
    public GameObject AudioSources;
    public GameObject CamViewTabletOpen;
    public GameObject CamViewTabletClose;
    public GameObject Dot;
    public GameObject Image;
    public GameObject Black;
    public GameObject StripeGlitches;

    public GameObject OriginalOfficeImage;
    public GameObject FoxyEnterOffice;

    public GameObject FoxyRunDownHall;
    public AudioSource DoorBang;
    public AudioSource FoxyFootsteps;

    public bool FoxyAnimationPlayed = false;
    public float foxyRunTime = 3.5f;

    public bool L_Door_Closed;
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
        
        GoldenFreddyJumpscareTime = 10f;
        GoldenFreddyOffice.SetActive(false);
        GoldenFreddyLaugh.SetActive(false);
    }

    void Update()
    {
        PlayerPrefs.SetFloat("WhichCamera", WhichCamera);
        PlayerPrefs.Save();

        WhereBonnie = PlayerPrefs.GetFloat("WhereBonnie", WhereBonnie);
        WhereChica = PlayerPrefs.GetFloat("WhereChica", WhereChica);
        WhereFreddy = PlayerPrefs.GetFloat("WhereFreddy", WhereFreddy);
        WhereFoxy = PlayerPrefs.GetFloat("WhereFoxy", WhereFoxy);

        // Get current sprite and assign it in a local variable
        currentSprite = cameraScreen.GetComponent<Image>().sprite;

        if(GoldenFreddyActive && !camIsUp)
        {
            GoldenFreddyJumpscareTime -= Time.deltaTime;
            if(GoldenFreddyJumpscareTime <= 0)
            {
                controllersRumble.IsRumbleTriggered("GoldenFreddy");
                GoldenFreddyJumpscare.SetActive(true);
                GoldenFreddyJumpscareTime = 10f;
                GoldenFreddyJumpscareTime -= Time.deltaTime;
                if(GoldenFreddyJumpscareTime <= 6f)
                {
                    SceneManager.LoadScene("GOLDENFREDDYCRASHCONSOLE");
                }
            }
        }
        else if(camIsUp)
        {
            GoldenFreddyActive = false;
        }

        if(!GoldenFreddyActive)
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
            
            if(GoldenFreddyChance == 0 && WhichCamera == 5)
            {
                GoldenFreddyActive = true;
                GoldenFreddyOffice.SetActive(true);
            }
            else
            {
                GoldenFreddyActive = false;
                GoldenFreddyOffice.SetActive(false);
            }
            if (WhichCamera == 1)
            {
                i18nTextTranslator.textId = "camera.showstage";
                i18nTextTranslator.UpdateText();

                if (currentSprite != ShowStageBonnieChicaFreddyDefault)
                {
                    currentSprite = ShowStageBonnieChicaFreddyDefault;
                }

                if (WhereBonnie >= 2)
                {
                    if (!BonnieLeft)
                    {
                        AmountLeft += 1;

                        BonnieLeft = true;
                    }
                }

                if (WhereChica >= 2)
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

                if (WhereFreddy >= 2)
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
                if (AmountLeft == 1)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != ShowStageChicaFreddyDefault)
                    {
                        currentSprite = ShowStageChicaFreddyDefault;
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
            if (WhichCamera == 2)
            {
                i18nTextTranslator.textId = "camera.diningarea";
                i18nTextTranslator.UpdateText();

                // Bonnie at Dining Area
                if (WhereBonnie == 2)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != DiningAreaBonnieDefault)
                    {
                        currentSprite = DiningAreaBonnieDefault;
                    }
                }

                // Chica at Dining Area
                if (WhereChica == 2)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != DiningAreaChicaDefault)
                    {
                        currentSprite = DiningAreaChicaDefault;
                    }
                }

                // Freddy at Dining Area
                if (WhereFreddy == 2)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != DiningAreaFreddyDefault)
                    {
                        currentSprite = DiningAreaFreddyDefault;
                    }

                }

                // Empty Dining Area
                if (WhereFreddy != 2 && WhereBonnie != 2 && WhereChica != 2)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != DiningAreaEmptyDefault)
                    {
                        currentSprite = DiningAreaEmptyDefault;
                    }
                }

            }

            if (WhichCamera == 3)
            {
                i18nTextTranslator.textId = "camera.piratecove";
                i18nTextTranslator.UpdateText();

                // Foxy phase 1
                if (WhereFoxy == 1)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != PirateCovePhase1Default)
                    {
                        currentSprite = PirateCovePhase1Default;
                    }
                }

                // Foxy phase 2
                if (WhereFoxy == 2)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != PirateCovePhase2Default)
                    {
                        currentSprite = PirateCovePhase2Default;
                    }
                }

                // Foxy phase 3
                if (WhereFoxy == 3)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != PirateCovePhase3Default)
                    {
                        currentSprite = PirateCovePhase3Default;
                    }
                }
                if(WhereFoxy == 4)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != PirateCovePhase4Default)
                    {
                        currentSprite = PirateCovePhase4Default;
                    }
                }
            }

            if (WhichCamera == 4)
            {
                i18nTextTranslator.textId = "camera.westhall";
                i18nTextTranslator.UpdateText();

                // Does Bonnie is at West Hall
                if (WhereBonnie == 5)
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

            if (WhichCamera == 5)
            {
                i18nTextTranslator.textId = "camera.westhallcorner";
                i18nTextTranslator.UpdateText();

                if (WhereBonnie == 6)
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

            if (WhichCamera == 6)
            {
                i18nTextTranslator.textId = "camera.supplycloset";
                i18nTextTranslator.UpdateText();

                // Does Bonnie is at Closet
                if (WhereBonnie == 4)
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

            if (WhichCamera == 7)
            {
                i18nTextTranslator.textId = "camera.easthall";
                i18nTextTranslator.UpdateText();

                // Chica at East Hall
                if (WhereChica == 5)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != EastHallChicaPhase1Default)
                    {
                        currentSprite = EastHallChicaPhase1Default;
                    }
                }
                else if (WhereChica == 6)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != EastHallChicaPhase2Default)
                    {
                        currentSprite = EastHallChicaPhase2Default;
                    }
                }

                // Freddy at East Hall
                if (WhereFreddy == 5)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != EastHallFreddyDefault)
                    {
                        currentSprite = EastHallFreddyDefault;
                    }
                }

                // East Hall empty
                if (WhereChica != 5 && WhereChica != 6 && WhereFreddy != 5)
                {
                    // Check if sprite is already displayed
                    if (currentSprite != EastHallEmptyDefault)
                    {
                        currentSprite = EastHallEmptyDefault;
                    }
                }
            }

            if (WhichCamera == 8)
            {
                i18nTextTranslator.textId = "camera.easthallcorner";
                i18nTextTranslator.UpdateText();

                if (WhereChica == 7)
                {
                    if (WhereFreddy <= 1)
                    {
                        currentSprite = EastHall2_2;
                    }
                }
                if (WhereFreddy == 6)
                {
                    if (WhereChica <= 6)
                    {
                        currentSprite = EastHall2_3;
                    }
                }

                // East Hall Corner empty
                if (WhereChica != 7 && WhereFreddy != 6)
                {
                    if (currentSprite != EastHallCornerEmptyDefault)
                    {
                        currentSprite = EastHallCornerEmptyDefault;
                    }
                }
            }

            if (WhichCamera == 9)
            {
                i18nTextTranslator.textId = "camera.backstage";
                i18nTextTranslator.UpdateText();

                // Does Bonnis is at Back Stage
                if (WhereBonnie == 3)
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

            if (WhichCamera == 10)
            {
                i18nTextTranslator.textId = "camera.kitchen";
                i18nTextTranslator.UpdateText();

                KitckenAudioOnly.SetActive(true);

                if (WhereChica == 5)
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
                KitckenAudioOnly.SetActive(false);
            }
        }
        else
        {
            KitckenAudioOnly.SetActive(false);
        }

        if(!camIsUp)
        {
            hasGeneratedGFNumber = false;
        }

        if (WhichCamera == 11)
        {
            i18nTextTranslator.textId = "camera.restrooms";
            i18nTextTranslator.UpdateText();

            // Chica is at Rest Rooms
            if (WhereChica == 3)
            {
                // Check if sprite is already displayed
                if (currentSprite != RestRoomsChicaPhase1Default)
                {
                    currentSprite = RestRoomsChicaPhase1Default;
                }
            }
            else if (WhereChica == 4)
            {
                // Check if sprite is already displayed
                if (currentSprite != RestRoomsChicaPhase2Default)
                {
                    currentSprite = RestRoomsChicaPhase2Default;
                }
            }

            // Freddy is at Rest Rooms
            if (WhereFreddy == 3)
            {
                // Check if sprite is already displayed
                if (currentSprite != RestRoomsFreddyDefault)
                {
                    currentSprite = RestRoomsFreddyDefault;
                }
            }

            // Rest Rooms empty
            if (WhereChica !=3 && WhereChica !=4 && WhereFreddy !=3)
            {
                // Check if sprite is already displayed
                if (currentSprite != RestRoomsEmptyDefault)
                {
                    currentSprite = RestRoomsEmptyDefault;
                }
            }
        }

        // where is Bonnie system
        if (WhereBonnie < 7)
        {
            OfficeObject.GetComponent<RandNumberGen>().BonnieOutsideLeftDoor = false;
        }


        if (WhereBonnie >= 7)
        {
            OfficeObject.GetComponent<Movement>().BonnieOutsideDoor = true;
            OfficeObject.GetComponent<RandNumberGen>().BonnieOutsideLeftDoor = true;
        }

        if (WhereBonnie >= 8)
        {
            BonnieJumpscare.SetActive(true);
            isBeingJumpscared = true;
            OriginalOfficeImage.SetActive(false);

            LowerCanvas.SetActive(false);
            CamViewTabletOpen.SetActive(false);
            CamViewTabletClose.SetActive(false);
            Black.SetActive(false);
            Image.SetActive(false);
            Dot.SetActive(false);
            StripeGlitches.SetActive(false);
            AudioSources.SetActive(false);
            Phonecalls.SetActive(false);

            controllersRumble.IsRumbleTriggered("Bonnie");
        }

        // where is Chica system
        if (WhereChica < 8)
        {
            OfficeObject.GetComponent<RandNumberGen>().ChicaOutsideRightDoor = false;
        }

        if (WhereChica >= 8)
        {
            OfficeObject.GetComponent<Movement>().ChicaOutsideDoor = true;
            OfficeObject.GetComponent<RandNumberGen>().ChicaOutsideRightDoor = true;
        }
        if (WhereChica >= 9)
        {
            if (camIsUp)
            {
                ChicaJumpscare.SetActive(true);
                isBeingJumpscared = true;
                OriginalOfficeImage.SetActive(false);

                LowerCanvas.SetActive(false);
                CamViewTabletOpen.SetActive(false);
                CamViewTabletClose.SetActive(false);
                Black.SetActive(false);
                Image.SetActive(false);
                Dot.SetActive(false);
                StripeGlitches.SetActive(false);
                AudioSources.SetActive(false);
                Phonecalls.SetActive(false);

                controllersRumble.IsRumbleTriggered("Chica");
            }
        }

        // where is Freddy system
        if (WhereFreddy >= 6)
        {
            OfficeObject.GetComponent<Movement>().FreddyOutsideDoor = true;
            OfficeObject.GetComponent<RandNumberGen>().FreddyOutsideRightDoor = true;
        }

        if (WhereFreddy >= 7)
        {
            FreddyJumpscare.SetActive(true);
            isBeingJumpscared = true;
            OriginalOfficeImage.SetActive(false);

            LowerCanvas.SetActive(false);
            CamViewTabletOpen.SetActive(false);
            CamViewTabletClose.SetActive(false);
            Black.SetActive(false);
            Image.SetActive(false);
            Dot.SetActive(false);
            StripeGlitches.SetActive(false);
            AudioSources.SetActive(false);
            Phonecalls.SetActive(false);

            controllersRumble.IsRumbleTriggered("Freddy");
        }

        if (WhereFoxy >= 4)
        {


            foxyStarted = true;

            if (foxyStarted)
            {
                FoxyFootsteps.Play();
                FoxyRunDownHall.SetActive(true);
                FoxyAnimationPlayed = true;
                
                foxyRunTime -= Time.deltaTime;
                FoxyAnimationTimer -= Time.deltaTime;
                if (FoxyAnimationTimer <= 0f)
                {
                    foxyAnimator.enabled = false;
                }
                if (foxyRunTime <= 0)
                {
                    if (!L_Door_Closed)
                    {
                        FoxyEnterOffice.SetActive(true);
                        OriginalOfficeImage.GetComponent<Image>().enabled = false;
                        FoxyRunDownHall.SetActive(false);
                        LowerCanvas.SetActive(false);
                        Phonecalls.SetActive(false);
                        AudioSources.SetActive(false);
                        CamViewTabletClose.SetActive(false);
                        CamViewTabletOpen.SetActive(false);
                        Dot.SetActive(false);
                        Black.SetActive(false);
                        StripeGlitches.SetActive(false);
                        if (foxyRunTime <= -3)
                        {
                            SceneManager.LoadScene("GameOver");
                            foxyRunTime = 3.5f;
                            foxyStarted = false;
                        }
                    }
                    if (L_Door_Closed)
                    {
                        DoorBang.Play();
                        OfficeObject.GetComponent<Movement>().WhereFoxy = 1;
                        OfficeObject.GetComponent<Movement>().foxyInCount = false;
                        //OfficeObject.GetComponent<Movement>().GenNumber();
                        OriginalOfficeImage.GetComponent<Image>().enabled = true;
                        FoxyRunDownHall.SetActive(false);
                        foxyRunTime = 3.5f;
                        foxyStarted = false;
                    }
                }
            }
            else if (!camIsUp)
            {
                FoxyRunDownHall.SetActive(false);
            }
            if(WhichCamera != 4)
            {
                FoxyRunDownHall.SetActive(false);
            }
        }

        if (isBeingJumpscared)
        {
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
        //GoldenFreddyChance = Random.Range(0, 32768);
        GoldenFreddyChance = Random.Range(0, 4);
        Debug.Log("GoldenFreddy chance generated : "+ GoldenFreddyChance);

    }

    public void cam1a()
    {
        WhichCamera = 1;
    }

    public void cam1b()
    {
        WhichCamera = 2;
    }

    public void cam1c()
    {
        WhichCamera = 3;
    }

    public void cam2a()
    {
        WhichCamera = 4;
    }

    public void cam2b()
    {
        WhichCamera = 5;
    }

    public void cam3()
    {
        WhichCamera = 6;
    }

    public void cam4a()
    {
        WhichCamera = 7;
    }

    public void cam4b()
    {
        WhichCamera = 8;
    }

    public void cam5()
    {
        WhichCamera = 9;
    }

    public void cam6()
    {
        WhichCamera = 10;
    }

    public void cam7()
    {
        WhichCamera = 11;
    }
    bool EasterEgg(int max)
    {
        int randomNumber = Random.Range(0, max);

        return randomNumber == 0;
    }
}