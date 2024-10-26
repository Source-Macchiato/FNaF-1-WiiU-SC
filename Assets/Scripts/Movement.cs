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

    //these 4 variables is used for the Difficulties of animatronics depend of what hour it is
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
        BonnieMovementTime = 5f;
        ChicaMovementTime = 5f; 
        FreddyMovementTime = 5f;
        FoxyMovementTime = 5f;
        
        //Disable Chica sounds when she is in the kitchen
        ChicaInKitchen.SetActive(false);

        //Get the current NightNumber at the start of the game
        NightNumber = SaveManager.LoadNightNumber();
    }

    // Function to save the difficulty of each animatronic
    void SaveDifficulty()
    {
        PlayerPrefs.SetFloat("BonnieDifficulty", BonnieDifficulty);
        PlayerPrefs.SetFloat("ChicaDifficulty", ChicaDifficulty);
        PlayerPrefs.SetFloat("FreddyDifficulty", FreddyDifficulty);
        PlayerPrefs.SetFloat("FoxyDifficulty", FoxyDifficulty);
        PlayerPrefs.Save();
    }
    
	void Update()
{
    int hour = gameScript.Hour;
    int nightIndex = Mathf.Clamp((int)NightNumber, 0, 5);  // Limit nightIndex to valid range (0-5)

    // Set initial difficulties based on the starting array
    FreddyDifficulty = startingDifficulties[nightIndex, 0];
    BonnieDifficulty = startingDifficulties[nightIndex, 1];
    ChicaDifficulty = startingDifficulties[nightIndex, 2];
    FoxyDifficulty = startingDifficulties[nightIndex, 3];

    // Increment difficulties based on the hour
    if (hour == 2) {
        BonnieDifficulty += 1;
    }
    if (hour == 3) {
        BonnieDifficulty += 1;
        ChicaDifficulty += 1;
        FoxyDifficulty += 1;
    }
    if (hour == 4) {
        BonnieDifficulty += 1;
        ChicaDifficulty += 1;
        FoxyDifficulty += 1;
    }

    // Check if animatronics are active
    BonnieActive = BonnieDifficulty > 0;
    ChicaActive = ChicaDifficulty > 0;
    FreddyActive = FreddyDifficulty > 0;
    FoxyActive = FoxyDifficulty > 0;

    if (Input.GetKeyDown(KeyCode.P))
    {
        Debug.Log("Hour: " + gameScript.Hour + " NightNumber: " + NightNumber);
        Debug.Log("Saved: Bonnie=" + BonnieDifficulty + ", Chica=" + ChicaDifficulty + ", Freddy=" + FreddyDifficulty + ", Foxy=" + FoxyDifficulty);
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

    if(BonnieActive)
    {
        BonnieMovementTime -= Time.deltaTime;
        if(BonnieMovementTime <=0)
        {
            AttemptBonnieMovement();
            BonnieMovementTime = 5f;
        }
    }
    

    //start Countdown

    /**while(BonnieActive)
    {
        if(BonnieMovementTime <= 0)
        {
            BonnieMovementTime -= Time.deltaTime;
        }
        else
        {
            BonnieMovementTime = 5f;
        }
        
    }
    if(BonnieActive)
    {
        BonnieMovementTime -= Time.deltaTime;
    }
    if(ChicaActive)
    {
        ChicaMovementTime -= Time.deltaTime;
    }
    if(FreddyActive)
    {
        FreddyMovementTime -= Time.deltaTime;
    }
    if(FoxyActive)
    {
        FoxyMovementTime -= Time.deltaTime;
    } **/

    // BonnieMovementTime*5 == ChanceToMove

    
    
    /** MovementTime dev
    BonnieMovementTime -= Time.deltaTime;

      if (BonnieMovementTime <= 0) 
      {
        if (BonnieActive) 
        {
        if (WhereBonnie == WhereChica) 
        {
            WhereBonnie += 1;
            bonnieInCount = false;
            GlitchActive = true;
            MoveGlitch.SetActive(true);
    
          if (!camIsUp) 
              {
              GlitchActive = false;
              MoveGlitch.SetActive(false);
          }
          } 
          else 
          {
            WhereBonnie += 1;
            bonnieInCount = false;
            GlitchActive = true;
            MoveGlitch.SetActive(true);

            if (!camIsUp) {
              GlitchActive = false;
              MoveGlitch.SetActive(false);
            }
          }
        }
    /**


/**
    if (NightNumber >= 3) {
      //-------------------bonnie init------------------------------
      
      BonnieMovementTime -= Time.deltaTime;

      if (BonnieMovementTime <= 0) {
        if (BonnieActive) {

          if (WhereBonnie == WhereChica) {

            WhereBonnie += 1;
            bonnieInCount = false;
            GlitchActive = true;
            MoveGlitch.SetActive(true);

            if (!camIsUp) {
              GlitchActive = false;
              MoveGlitch.SetActive(false);
            }

            
          } else {

            WhereBonnie += 1;
            bonnieInCount = false;
            GlitchActive = true;
            MoveGlitch.SetActive(true);

            if (!camIsUp) {
              GlitchActive = false;
              MoveGlitch.SetActive(false);
            }

            
        
          }
        }
      }
      //-----------------------------------------------------

      //---------chica init--------------------------------
          if(WhereChica != WhereBonnie)
    {
        ChicaMovementTime -= Time.deltaTime;
    }

      if (ChicaMovementTime <= 0) {
        if (ChicaActive) {
          if (WhereBonnie != 1) //check if bonnie is ou of the stage.
          {
            if (WhereBonnie == 2) // check if bonnie is on Dining Area
            {

              WhereChica += 2;
              chicaInCount = false;
              GlitchActive = true;
              MoveGlitch.SetActive(true);

              if (!camIsUp) {
                GlitchActive = false;
                MoveGlitch.SetActive(false);
              }

              
            } else // if bonnie isn't at Dining Area 
            {

              WhereChica += 1;
              chicaInCount = false;
              GlitchActive = true;
              MoveGlitch.SetActive(true);

              if (!camIsUp) {
                GlitchActive = false;
                MoveGlitch.SetActive(false);
              }

              
            
            }

          }

        }
      }
      //-----------------------------------------------

      //------------Foxy init--------------------------
      
      if (!camIsUp) {
        FoxyMovementTime -= Time.deltaTime;
      }
      if (FoxyMovementTime <= 0) {
        if (FoxyActive) {
          WhereFoxy += 1;
          foxyInCount = false;
          GlitchActive = true;
          MoveGlitch.SetActive(true);

          if (!camIsUp) {
            GlitchActive = false;
            MoveGlitch.SetActive(false);
          }
          
        }
      }
      

      //-----------------------------------------------

      //-------freddy init--------------------
      FreddyMovementTime -= Time.deltaTime;

      if (FreddyMovementTime <= 0) {
        if (FreddyActive) 
        {
            if(WhereBonnie != 2 && WhereFreddy == 1 || WhereChica != 2 && WhereFreddy == 1 || WhereChica != 3 && WhereFreddy == 2 || WhereChica != 4 && WhereFreddy == 3 || WhereChica != 5 && WhereFreddy == 4 || WhereChica != 6 && WhereFreddy == 5)
            {
                if(WhereBonnie != 1 && WhereChica != 1)
                {
                    WhereFreddy += 1;
                    freddyInCount = false;
                    GlitchActive = true;
                    MoveGlitch.SetActive(true);
                    FreddyLaugh1.Play();


                    if (!camIsUp) 
                    {
                        GlitchActive = false;
                        MoveGlitch.SetActive(false);
                    }

                     


                }

            }
          
        }
      }
      //-------------------------------------------------
      
    }**/

    //----------------------------------------------



        //---------bonnie at door config------------
        /**if (BonnieOutsideDoor)
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
                        WhereBonnie = 3;
                        PlayerPrefs.SetFloat("WhereBonnie", WhereBonnie);
                        PlayerPrefs.Save();
                        WaitForMovingFromDoorBonnie = false;
                        BonnieOutsideDoorTime = 0;
                    }
                }
            }
        }
        //-----------------------------------------

        //-----------Chica at door config----------
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
                        WhereChica = 2;
                        PlayerPrefs.SetFloat("WhereChica", WhereChica);
                        PlayerPrefs.Save();
                        WaitForMovingFromDoorChica = false;
                        ChicaOutsideDoorTime = 0;
                    }
                }
            }
        }
        //------------------------------------------


        //-----------freddy is close to the office---------
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
                        WhereFreddy = 1;
                        PlayerPrefs.SetFloat("WhereFreddy", WhereFreddy);
                        PlayerPrefs.Save();
                        WaitForMovingFromDoorFreddy = false;
                        FreddyOutsideDoorTime = 0;
                    }
                }
            }
        }
        //--------------------------------------------------

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

        if(WhereChica == 5)
        {
            ChicaInKitchen.SetActive(true);
        } else if (WhereChica != 5)
        {
            ChicaInKitchen.SetActive(false);
        }  **/
    }

    void AttemptBonnieMovement()
    {
        if (Random.Range(0, 100) < BonnieDifficulty) // Exécute avec la probabilité de BonnieDifficulty
        {
            // Code pour permettre à Bonnie de bouger
            Debug.Log("Bonnie bouge!");
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
