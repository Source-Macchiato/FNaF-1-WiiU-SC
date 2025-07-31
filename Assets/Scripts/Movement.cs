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

    public static int freddyPosition = 1;
    public static int bonniePosition = 1;
    public static int chicaPosition = 1;
    public static int foxyPosition = 1;

    public int BonnieDifficulty; //done
    public int ChicaDifficulty; //done
    public int FreddyDifficulty; //done
    public int FoxyDifficulty; //done


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

    public float BonnieOutsideDoorTime = 5f;
    public float ChicaOutsideDoorTime = 5f;
    public float FreddyOutsideDoorTime = 5f;

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

        BonnieOutsideDoorTime = 5f;
        ChicaOutsideDoorTime = 5f;
        FreddyOutsideDoorTime = 5f;

        //generate AI Difficulties
        BonnieProbability = Random.Range(0, 100);
        ChicaProbability = Random.Range(0, 100);        
        FreddyProbability = Random.Range(0, 100);
        FoxyProbability = Random.Range(0, 100);

        //Disable Chica sounds when she is in the kitchen
        ChicaInKitchen.SetActive(false);

        //Get the current NightNumber at the start of the game
        NightNumber = SaveManager.saveData.game.nightNumber;
    }
    
	void Update()
    {
        Office office = OfficeObject.GetComponent<Office>();
        LeftDoorClosed = office.L_Door_Closed;
        RightDoorClosed = office.R_Door_Closed;
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
            BonnieDifficulty = PlayerPrefs.GetInt("BonnieDifficulty", 0) * 5;
            ChicaDifficulty = PlayerPrefs.GetInt("ChicaDifficulty", 0) * 5;
            FreddyDifficulty = PlayerPrefs.GetInt("FreddyDifficulty", 0) * 5;
            FoxyDifficulty = PlayerPrefs.GetInt("FoxyDifficulty", 0) * 5;
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
            Debug.Log("WhereBonnie=" + bonniePosition + "\nWhereChica= " + chicaPosition + "\nWhereFreddy=" + freddyPosition + "\nWhereFoxy=" + foxyPosition);
        }

        RandMovement randMovement = GetComponent<RandMovement>();

        // Get the references RandNumberGen (the other code was spaghetti asf)
        // Use PlayerPrefs too lmao
        RandNumberGen randNumberGen = OfficeObject.GetComponent<RandNumberGen>();
        // Check if somebody left the stage by checking if an Where<name> is higher than 2 or equal
        if (bonniePosition >= 2)
        {
            randNumberGen.BonnieLeftStage = true;
        }
        if (chicaPosition >= 2)
        {
            randNumberGen.ChicaLeftStage = true;
        }
        if (freddyPosition >= 2)
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
        if (BonnieActive)
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
        if (ChicaActive)
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
        if (FreddyActive)
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
        if (FoxyActive && !camIsUp && foxyPosition < 4)
        {
            FoxyMovementTime -= Time.deltaTime;
            if(FoxyMovementTime <=0)
            {
                FoxyProbability = Random.Range(0, 100);
                AttemptFoxyMovement();
                FoxyMovementTime = 5.01f;
            }
        }
        if (BonnieOutsideDoor)
        {
            if (LeftDoorClosed)
            {
                MoveFromDoorBonnie();
                BonnieMovementTime = 20;
                if (WaitForMovingFromDoorBonnie)
                {
                    BonnieOutsideDoorTime -= Time.deltaTime;
                    if (BonnieOutsideDoorTime <= 0)
                    {
                        BonnieOutsideDoor = false;
                        OfficeObject.GetComponent<Office>().BonnieOutsideDoor = false;
                        bonniePosition = 3;
                        WaitForMovingFromDoorBonnie = false;
                        BonnieOutsideDoorTime = 0;
                    }
                }
            }
        }

        if (ChicaOutsideDoor)
        {
            if (RightDoorClosed)
            {
                MoveFromDoorChica();
                ChicaMovementTime = 20;
                if (WaitForMovingFromDoorChica)
                {
                    ChicaOutsideDoorTime -= Time.deltaTime;
                    if (ChicaOutsideDoorTime <= 0)
                    {
                        ChicaOutsideDoor = false;
                        OfficeObject.GetComponent<Office>().ChicaOutsideDoor = false;
                        chicaPosition = 2;
                        WaitForMovingFromDoorChica = false;
                        ChicaOutsideDoorTime = 0;
                    }
                }
            }
        }
        if (FreddyOutsideDoor)
        {
            if (RightDoorClosed)
            {
                MoveFromDoorFreddy();
                FreddyMovementTime = 20;
                if (WaitForMovingFromDoorFreddy)
                {
                    FreddyOutsideDoorTime -= Time.deltaTime;
                    if  (FreddyOutsideDoorTime <= 0)
                    {
                        FreddyOutsideDoor = false;
                        OfficeObject.GetComponent<Office>().FreddyOutsideDoor = false;
                        freddyPosition = 1;
                        WaitForMovingFromDoorFreddy = false;
                        FreddyOutsideDoorTime = 0;
                    }
                }
            }
        }
        if (GlitchActive)
        {
            MoveGlitchUp -= Time.deltaTime;
            if (MoveGlitchUp <= 0 && LongGlitch == true)
            {
                MoveGlitch.SetActive(false);
                GlitchActive = false;
                MoveGlitchUp = 0.5f;
                LongGlitch = false;
            }
            else if(MoveGlitchUp <= 0)
            {
                MoveGlitch.SetActive(false);
                GlitchActive = false;
                MoveGlitchUp = 0.5f;
            }
        }

        ChicaInKitchen.SetActive(chicaPosition == 5);
    }
    //CHeck if the purcentage to move of each AI

    void AttemptBonnieMovement()
    {
        if (BonnieProbability < BonnieDifficulty) // probability bonnie to move
        {
            bonniePosition += 1;
            bonnieInCount = false;
            AIGlitch();
            if (!camIsUp) 
            {
                GlitchActive = false;
                MoveGlitch.SetActive(false);
            }
            Debug.Log("Bonnie Probability : "+BonnieProbability+"\nBonnie Difficulty : " + BonnieDifficulty);
        }
    }

    void AttemptChicaMovement()
    {
        if (ChicaProbability < ChicaDifficulty) // probability bonnie to move
        {

            if (bonniePosition == 2) // check if bonnie is on Dining Area
            {
                chicaPosition += 2;
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
                chicaPosition += 1;
                chicaInCount = false;
                AIGlitch();
                if (!camIsUp) 
                {
                GlitchActive = false;
                MoveGlitch.SetActive(false);
                }
            }
            Debug.Log("Chica Probability : "+BonnieProbability+"\nChica Difficulty : " + BonnieDifficulty);
        }
    }    
    void AttemptFreddyMovement()
    {
        if (FreddyProbability < FreddyDifficulty) // Check if Freddy should move
        {
            // Ensure Freddy’s path is clear
            bool pathClear = 
                (bonniePosition != 2 && freddyPosition == 1) || 
                (chicaPosition != 2 && freddyPosition == 1) || 
                (chicaPosition != 3 && freddyPosition == 2) || 
                (chicaPosition != 4 && freddyPosition == 3) || 
                (chicaPosition != 5 && freddyPosition == 4) || 
                (chicaPosition != 6 && freddyPosition == 5);

            // Ensure Bonnie and Chica aren't in Freddy's way at starting position
            if (pathClear && bonniePosition != 1 && chicaPosition != 1)
            {
                freddyPosition += 1; // Advance Freddy’s position
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
            foxyPosition += 1;
            foxyInCount = false;
            AIGlitch();
            if (!camIsUp) 
            {
              GlitchActive = false;
              MoveGlitch.SetActive(false);
            }

            //debug
            Debug.Log("Foxy Probability : "+BonnieProbability+"\nFoxy Difficulty : " + BonnieDifficulty);
        }
    }

    // Move From Door
    void MoveFromDoorBonnie()
    {
        if (!WaitForMovingFromDoorBonnie)
        {
            BonnieOutsideDoorTime -= Time.deltaTime;
            WaitForMovingFromDoorBonnie = true;
        }
    }

    void MoveFromDoorChica()
    {
        if (!WaitForMovingFromDoorChica)
        {
            ChicaOutsideDoorTime -= Time.deltaTime;
            WaitForMovingFromDoorChica = true;
        }
    }

    void MoveFromDoorFreddy()
    {
        if (!WaitForMovingFromDoorFreddy)
        {
            FreddyOutsideDoorTime -= Time.deltaTime;
            WaitForMovingFromDoorFreddy = true;
        }
    }


    //Debug garbage
    public void DebugWhereBonnieIncrease()
    {
        bonniePosition += 1;
    }
    public void DebugWhereBonnieMin()
    {
        bonniePosition -= 1;
    }
    public void DebugWhereChicaIncrease()
    {
        chicaPosition += 1;
    }
    public void DebugWhereChicaMin()
    {
        chicaPosition -= 1;
    }
    public void DebugWhereFreddyIncrease()
    {
        freddyPosition += 1;
    }
    public void DebugWhereFreddyMin()
    {
        freddyPosition -= 1;
    }
}