using UnityEngine;

public class Movement : MonoBehaviour {
    
    //public ChangeImages changeImages;
    public GameScript GameScript;
    private GameScript gameScript;

    public bool LongGlitch = false;

    public GameObject ChicaInKitchen;

    public float BonnieMovementTime = 5f;
    public float ChicaMovementTime = 5f; 
    public float FreddyMovementTime = 5f;
    public float FoxyMovementTime = 5f; 


    public int BonnieProbability = 0;
    public int ChicaProbability = 0;
    public int FreddyProbability = 0;
    public int FoxyProbability = 0;

    public float WhereBonnie = 1; //done
    public float WhereChica = 1; //done
    public float WhereFreddy = 1; //done
    public float WhereFoxy = 1; //done

    public float BonnieDifficulty; //done
    public float ChicaDifficulty; //done
    public float FreddyDifficulty; //done
    public float FoxyDifficulty; //done


    public bool bonnieInCount;
    public bool chicaInCount;
    public bool freddyInCount;
    public bool foxyInCount;

    public bool camIsUp = false;

    public float NightNumber; //done

    public GameObject MoveGlitch;
    public bool GlitchActive = false;
    public float MoveGlitchUp = 9.0f;

    public bool ChicaActive = false; 
    public bool BonnieActive = false;
    public bool FreddyActive = false;
    public bool FoxyActive = false;

    public bool BonnieOutsideDoor = false;
    public bool ChicaOutsideDoor = false;
    public bool FreddyOutsideDoor = false;
    public bool FoxyRunningHallway = false;

    public float BonnieOutsideDoorTime;
    public float ChicaOutsideDoorTime;
    public float FreddyOutsideDoorTime;

    public bool WaitForMovingFromDoorBonnie = false;
    public bool WaitForMovingFromDoorChica = false; 
    public bool WaitForMovingFromDoorFreddy = false;

    public bool LeftDoorClosed = false;
    public bool RightDoorClosed = false;

    public GameObject OfficeObject;

    public AudioSource FreddyLaugh1;

    //each column represent a night (night 1, night 2 etc...), and each row represent an Hour "12 AM to 5AM"
    private int[,] startingDifficulties = {
    {0, 0, 0, 0}, // Night 1 - Freddy, Bonnie, Chica, Foxy
    {0, 3, 1, 1}, // Night 2
    {1, 0, 5, 2}, // Night 3
    {1, 2, 4, 6}, // Night 4
    {3, 5, 7, 5}, // Night 5
    {4, 10, 12, 16} // Night 6
};
    //check if AI is active
    bool IsAIActive(float[,] difficulties, int nightNumber, int hourIndex)
    {
        return difficulties[nightNumber, hourIndex] <= 1;
    }
    void Start()
    {
        // Used for to get the "Hour" variable
        gameScript = GetComponent<GameScript>();

        //set movement time Z
        BonnieMovementTime = 4.97f;
        ChicaMovementTime = 4.98f; 
        FreddyMovementTime = 3.02f;
        FoxyMovementTime = 5.01f;

        //generate AI Difficulties
        BonnieProbability = Random.Range(0, 100);
        ChicaProbability = Random.Range(0, 100);        
        FreddyProbability = Random.Range(0, 100);
        FoxyProbability = Random.Range(0, 100);

        //Disable Chica sounds when she is in the kitchen
        ChicaInKitchen.SetActive(false);

        //Get the current NightNumber at the start of the game
        NightNumber = SaveManager.LoadNightNumber();
    }
    
	void Update()
{
    int hour = gameScript.Hour;
    int nightIndex = Mathf.Clamp((int)NightNumber, 0, 5);  // Limit nightIndex to valid range (0-5)

    if(NightNumber <= 5)
    {
        // Set initial difficulties based on the starting array
        FreddyDifficulty = startingDifficulties[nightIndex, 0] * 5;
        BonnieDifficulty = startingDifficulties[nightIndex, 1] * 5;
        ChicaDifficulty = startingDifficulties[nightIndex, 2] * 5;
        FoxyDifficulty = startingDifficulties[nightIndex, 3] * 5;
    }
    else
    {
        BonnieDifficulty = PlayerPrefs.GetFloat("BonnieDifficulty", BonnieDifficulty) * 5;
        ChicaDifficulty = PlayerPrefs.GetFloat("ChicaDifficulty", ChicaDifficulty) * 5;
        FreddyDifficulty = PlayerPrefs.GetFloat("FreddyDifficulty", FreddyDifficulty) * 5;
        FoxyDifficulty = PlayerPrefs.GetFloat("FoxyDifficulty", FoxyDifficulty) * 5;
    }
    

    // Increment difficulties based on the hour
    if (hour == 2) {
        BonnieDifficulty += 5;
    }
    if (hour == 3) {
        BonnieDifficulty += 5;
        ChicaDifficulty += 5;
        FoxyDifficulty += 5;
    }
    if (hour == 4) {
        BonnieDifficulty += 5;
        ChicaDifficulty += 5;
        FoxyDifficulty += 5;
    }

    // Check if animatronics are active
    BonnieActive = BonnieDifficulty > 0;
    ChicaActive = ChicaDifficulty > 0;
    FreddyActive = FreddyDifficulty > 0;
    FoxyActive = FoxyDifficulty > 0;

    //debug garbage
    if (Input.GetKeyDown(KeyCode.P))
    {
        Debug.ClearDeveloperConsole();
        Debug.Log("Hour: " + gameScript.Hour + " NightNumber: " + NightNumber);
        Debug.Log("Saved: Bonnie=" + BonnieDifficulty + "\n Chica=" + ChicaDifficulty + "\n Freddy=" + FreddyDifficulty + "\n Foxy=" + FoxyDifficulty);
        Debug.Log("WhereBonnie=" +WhereBonnie+ "\nWhereChica =" +WhereChica+ "\nWhereFreddy =" +WhereFreddy+ "\nWhereFoxy = " +WhereFoxy);
    }

    RandMovement randMovement = GetComponent<RandMovement>();
    PlayerPrefs.SetFloat("WhereBonnie", WhereBonnie);
    PlayerPrefs.Save();
    PlayerPrefs.SetFloat("WhereChica", WhereChica);
    PlayerPrefs.Save();
    PlayerPrefs.SetFloat("WhereFoxy", WhereFoxy);
    PlayerPrefs.Save();
    PlayerPrefs.SetFloat("WhereFreddy", WhereFreddy);
    PlayerPrefs.Save();
    //Get the references RandNumberGen (the other code was spaghetti asf)
    RandNumberGen randNumberGen = OfficeObject.GetComponent<RandNumberGen>();
    Office office = OfficeObject.GetComponent<Office>();
    //Check if somebody left the stage by checking if an Where<name> is higher than 2 or equal
    if (WhereBonnie >= 2)
    {
        randNumberGen.BonnieLeftStage = true;
    }
    if (WhereChica >= 2)
    {
        randNumberGen.ChicaLeftStage = true;
    }
    if (WhereFreddy >= 2)
    {
        randNumberGen.FreddyLeftStage = true;
    }
    // Update the current stat of the AI at the door
    if (BonnieOutsideDoor)
    {
        office.BonnieOutsideDoor = true;
    }
    if (ChicaOutsideDoor && !BonnieOutsideDoor)
    {
        office.ChicaOutsideDoor = true;
    }
    if (FreddyOutsideDoor && !ChicaOutsideDoor)
    {
        office.FreddyOutsideDoor = true;
    }
    if (FoxyRunningHallway && !BonnieOutsideDoor)
    {
        office.FoxyRunningHallway = true;
    }

    //generate bonnie chance  to move
    if(BonnieActive)
    {
        BonnieMovementTime -= Time.deltaTime;
        if(BonnieMovementTime <=0)
        {
            BonnieProbability = Random.Range(0, 100);
            AttemptBonnieMovement();
            BonnieMovementTime = 4.97f;
        }
    }
    //generate Chica chance  to move
    if(ChicaActive)
    {
        ChicaMovementTime -= Time.deltaTime;
        if(ChicaMovementTime <=0)
        {
            ChicaProbability = Random.Range(0, 100);
            AttemptChicaMovement();
            ChicaMovementTime = 4.98f;
        }
    }
    //generate Freddy chance  to move
    if(FreddyActive)
    {
        FreddyMovementTime -= Time.deltaTime;
        if(FreddyMovementTime <=0)
        {
            BonnieProbability = Random.Range(0, 100);
            AttemptFreddyMovement();
            FreddyMovementTime = 3.02f;
        }
    }
    //generate Foxy chance  to move
    if(FoxyActive && !camIsUp && WhereFoxy < 4)
    {
        FoxyMovementTime -= Time.deltaTime;
        if(FoxyMovementTime <=0)
        {
            FoxyProbability = Random.Range(0, 100);
            AttemptFoxyMovement();
            FoxyMovementTime = 5.01f;
        }
    }

    // Handle movement checks for each character outside their door
    HandleOutsideDoor(ref BonnieOutsideDoor, LeftDoorClosed, ref BonnieMovementTime, ref WaitForMovingFromDoorBonnie, ref BonnieOutsideDoorTime, "Bonnie", 3);
    
    HandleOutsideDoor(ref ChicaOutsideDoor, RightDoorClosed, ref ChicaMovementTime, ref WaitForMovingFromDoorChica,ref ChicaOutsideDoorTime, "Chica", 2);
    
    HandleOutsideDoor(ref FreddyOutsideDoor, RightDoorClosed, ref FreddyMovementTime, ref WaitForMovingFromDoorFreddy, ref FreddyOutsideDoorTime, "Freddy", 1);

    // Glitch activity handler
    if (GlitchActive)
    {
        MoveGlitchUp -= Time.deltaTime;
        if (MoveGlitchUp <= 0)
        {
            MoveGlitch.SetActive(false);
            GlitchActive = false;
            MoveGlitchUp = 0.5f;
            LongGlitch = false;
        }
    }

    // Chica in kitchen
    ChicaInKitchen.SetActive(WhereChica == 5);
}

    void HandleOutsideDoor(ref bool outsideDoor, bool doorClosed, ref float movementTime, ref bool waitForMoving, ref float outsideDoorTime, string animatronicName, int defaultPosition)
    {
        if (outsideDoor && doorClosed)
        {
            MoveFromDoor(animatronicName);
            movementTime = 20;

            if (waitForMoving)
            {
                outsideDoorTime -= Time.deltaTime;

                if (outsideDoorTime <= 0)
                {
                    outsideDoor = false;

                    // Set the corresponding animatronic's outside door status
                    if (animatronicName == "Bonnie")
                    {
                        OfficeObject.GetComponent<Office>().BonnieOutsideDoor = false;
                    }
                    else if (animatronicName == "Chica")
                    {
                        OfficeObject.GetComponent<Office>().ChicaOutsideDoor = false;
                    }
                    else if (animatronicName == "Freddy")
                    {
                        OfficeObject.GetComponent<Office>().FreddyOutsideDoor = false;
                    }

                    PlayerPrefs.SetFloat("Where" + animatronicName, defaultPosition);
                    PlayerPrefs.Save();
                    waitForMoving = false;
                    outsideDoorTime = 0;
                }
            }
        }
    }
    void MoveFromDoor(string animatronicName)
    {
        switch (animatronicName)
        {
            case "Bonnie":
                MoveFromDoorBonnie();
            break;
            case "Chica":
                MoveFromDoorChica();
            break;
            case "Freddy":
                MoveFromDoorFreddy();
            break;
        }
    }
    //CHeck if the purcentage to move of each AI

    void AttemptBonnieMovement()
    {
        if (BonnieProbability < BonnieDifficulty) // probability bonnie to move
        {
            WhereBonnie += 1;
            bonnieInCount = false;
            AIGlitch();
            if (!camIsUp) 
            {
                GlitchActive = false;
                MoveGlitch.SetActive(false);
            }
            Debug.Log("Bonnie Porbability : "+BonnieProbability+"\nBonnieDifficulty : " + BonnieDifficulty);
        }
    }

    void AttemptChicaMovement()
    {
        if (ChicaProbability < ChicaDifficulty) // probability bonnie to move
        {
            if (WhereBonnie != 1) //check if bonnie is ou of the stage.
            {
                if (WhereBonnie == 2) // check if bonnie is on Dining Area
                {

                  WhereChica += 2;
                  chicaInCount = false;
                  AIGlitch();

                  if (!camIsUp) 
                  {
                    GlitchActive = false;
                    MoveGlitch.SetActive(false);
                  }


                } 
                else // if bonnie isn't at Dining Area 
                {

                  WhereChica += 1;
                  chicaInCount = false;
                  AIGlitch();

                  if (!camIsUp) 
                  {
                    GlitchActive = false;
                    MoveGlitch.SetActive(false);
                  }
                }

            }
            Debug.Log("Chica Porbability : "+BonnieProbability+"\nChicaDifficulty : " + BonnieDifficulty);
        }
    }    
    void AttemptFreddyMovement()
    {
        if (FreddyProbability < FreddyDifficulty) // Check if Freddy should move
        {
            // Ensure Freddy’s path is clear
            bool pathClear = 
                (WhereBonnie != 2 && WhereFreddy == 1) || 
                (WhereChica != 2 && WhereFreddy == 1) || 
                (WhereChica != 3 && WhereFreddy == 2) || 
                (WhereChica != 4 && WhereFreddy == 3) || 
                (WhereChica != 5 && WhereFreddy == 4) || 
                (WhereChica != 6 && WhereFreddy == 5);

            // Ensure Bonnie and Chica aren't in Freddy's way at starting position
            if (pathClear && WhereBonnie != 1 && WhereChica != 1)
            {
                WhereFreddy += 1; // Advance Freddy’s position
                freddyInCount = false;
                AIGlitch();
                FreddyLaugh1.Play();

                // Check if camera is down, deactivate glitch
                if (!camIsUp) 
                {
                    GlitchActive = false;
                    MoveGlitch.SetActive(false);
                }
            }

            Debug.Log("Freddy Probability: " + FreddyProbability + "\nFreddy Difficulty: " + FreddyDifficulty);
        }
    }

void AIGlitch()
{
    GlitchActive = true;
    MoveGlitch.SetActive(true);
}
    void AttemptFoxyMovement()
    {
        if (FoxyProbability < FoxyDifficulty) // probability bonnie to move
        {
            WhereFoxy += 1;
            foxyInCount = false;
            AIGlitch();
            if (!camIsUp) 
            {
              GlitchActive = false;
              MoveGlitch.SetActive(false);
            }

            //debug
            Debug.Log("Foxy Porbability : "+BonnieProbability+"\nFoxyDifficulty : " + BonnieDifficulty);
        }
    }

    // Move From Door
    void MoveFromDoorBonnie()
    {
        if (!WaitForMovingFromDoorBonnie)
        {
            BonnieOutsideDoorTime += BonnieDifficulty;
            WaitForMovingFromDoorBonnie = true;
        }
    }

    void MoveFromDoorChica()
    {
        if (!WaitForMovingFromDoorChica)
        {
            ChicaOutsideDoorTime += ChicaDifficulty;
            WaitForMovingFromDoorChica = true;
        }
    }

    void MoveFromDoorFreddy()
    {
        if (!WaitForMovingFromDoorFreddy)
        {
            FreddyOutsideDoorTime += FreddyDifficulty;
            WaitForMovingFromDoorFreddy = true;
        }
    }


    //Debug garbage
    public void DebugWhereBonnieIncrease()
    {
        WhereBonnie += 1;
    }
    public void DebugWhereBonnieMin()
    {
        WhereBonnie -= 1;
    }
    public void DebugWhereChicaIncrease()
    {
        WhereChica += 1;
    }
    public void DebugWhereChicaMin()
    {
        WhereChica -= 1;
    }
    public void DebugWhereFreddyIncrease()
    {
        WhereFreddy += 1;
    }
    public void DebugWhereFreddyMin()
    {
        WhereFreddy -= 1;
    }
}