using UnityEngine;

public class Movement : MonoBehaviour {
    
    //public ChangeImages changeImages;
    public GameScript GameScript;
    private GameScript gameScript;

    public bool LongGlitch = false;

    public GameObject ChicaInKitchen;
    public double BonnieMovementTime;
    public double ChicaMovementTime;
    public double FreddyMovementTime;
    public double FoxyMovementTime;
    

    public float WhereBonnie = 1;
    public float WhereChica = 1;
    public float WhereFreddy = 1;
    public float WhereFoxy = 1;

    public float BonnieDifficulty;
    public float ChicaDifficulty;
    public float FreddyDifficulty;
    public float FoxyDifficulty;

    public bool bonnieInCount;
    public bool chicaInCount;
    public bool freddyInCount;
    public bool foxyInCount;

    public bool camIsUp = false;

    public float NightNumber;

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
    private float[,] bonnieDifficulties = {
    {0, 3, 0, 2, 5, 10}, // Night 1
    {0, 4, 0, 3, 6, 11}, // Night 2
    {0, 1, 1, 2, 3, 10}, // Night 3
    {2, 2, 2, 3, 5, 13}, // Night 4
    {5, 6, 7, 8, 9, 10}, // Night 5
    {10, 11, 12, 13, 14, 15}  // Night 6
};

    private float[,] chicaDifficulties = {
    {0, 1, 5, 4, 7, 12}, // Night 1
    {1, 2, 2, 4, 8, 12}, // Night 2
    {5, 5, 6, 7, 8, 13}, // Night 3
    {4, 4, 5, 6, 8, 14}, // Night 4
    {7, 7, 8, 9, 10, 15}, // Night 5
    {12, 12, 13, 14, 15, 20}  // Night 6
};

    private float[,] freddyDifficulties = {
    {0, 0, 1, 1, 3, 4}, // Night 1
    {0, 0, 0, 0, 3, 4}, // Night 2
    {1, 1, 1, 1, 3, 4}, // Night 3
    {1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 4}, // Night 4 (50/50 chance between 1 and 2)
    {3, 3, 3, 3, 3, 4}, // Night 5
    {4, 4, 4, 4, 4, 4}  // Night 6
};

    private float[,] foxyDifficulties = {
    {0, 0, 1, 6, 5, 16}, // Night 1
    {0, 0, 0, 0, 5, 16}, // Night 2
    {0, 0, 0, 0, 0, 17}, // Night 3
    {6, 6, 7, 8, 8, 18}, // Night 4
    {5, 5, 6, 7, 7, 19}, // Night 5
    {16, 16, 17, 18, 19, 20}  // Night 6
};



    void Start()
    {
        // Used for to get the "Hour" variable
        gameScript = GetComponent<GameScript>();

        //Random Generated number the animatronics (random int number 1-20)
        RandMovement randMovement = GetComponent<RandMovement>();
        randMovement.BonnieRandMove();

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
	void Update ()
    {
    
    int hourIndex = gameScript.Hour - 12; // Shift hour to index range (0 to 5)

    //Check if the night is valid (between 0 to 5) and hour (between 0 and 5)
    if (NightNumber >= 0 && NightNumber <= 5 && hourIndex >= 0 && hourIndex <= 5)
    {
        // Set difficulties based on the current night and hour
        BonnieDifficulty = bonnieDifficulties[(int)NightNumber, (int)hourIndex];
        ChicaDifficulty = chicaDifficulties[(int)NightNumber, (int)hourIndex];
        FreddyDifficulty = freddyDifficulties[(int)NightNumber, (int)hourIndex];
        FoxyDifficulty = foxyDifficulties[(int)NightNumber, (int)hourIndex];

        //call the function to save the difficulty of the AI
        SaveDifficulty();
    }

    //Night 7 (custom night) depend on the player's choice.
    else if (NightNumber == 7)
    {
        // Custom night: retrieve saved difficulties
        BonnieDifficulty = PlayerPrefs.GetFloat("BonnieDifficulty", BonnieDifficulty);
        ChicaDifficulty = PlayerPrefs.GetFloat("ChicaDifficulty", ChicaDifficulty);
        FreddyDifficulty = PlayerPrefs.GetFloat("FreddyDifficulty", FreddyDifficulty);
        FoxyDifficulty = PlayerPrefs.GetFloat("FoxyDifficulty", FoxyDifficulty);
    }
    
    //Press P to have the current state of the Hour, the night Number, the saved difficulties, and the Bonnie difficulty for test
    if (Input.GetKeyDown(KeyCode.P))
    {
        Debug.Log("Hour: " + gameScript.Hour + " NightNumber: " + NightNumber);
        Debug.Log("Saved: Bonnie=" + BonnieDifficulty + ", Chica=" + ChicaDifficulty + ", Freddy=" + FreddyDifficulty + ", Foxy=" + FoxyDifficulty);
        Debug.Log("Bonnie Difficulty (Array): " + bonnieDifficulties[(int)NightNumber - 1, (int)hourIndex]);
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
            if (WhereBonnie == 2) // checki if bonnie is on Dining Area
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
