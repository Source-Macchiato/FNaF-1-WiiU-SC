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

    private float[,] bonnieDifficulties = {
    {11, 0, 1, 0, 0, 0}, // Night 1
    {0, 0, 0, 0, 0, 0}, // Night 2
    {0, 0, 0, 0, 0, 0}, // Night 3
    {0, 0, 0, 0, 0, 0}, // Night 4
    {0, 0, 0, 0, 0, 0}, // Night 5
    {0, 0, 0, 0, 0, 0}  // Night 6
};

// Repeat for each character (Chica, Freddy, Foxy)
private float[,] chicaDifficulties = {
    {11, 0, 1, 0, 0, 0}, // Night 1
    {0, 0, 0, 0, 0, 0}, // Night 2
    {0, 0, 0, 0, 0, 0}, // Night 3
    {0, 0, 0, 0, 0, 0}, // Night 4
    {0, 0, 0, 0, 0, 0}, // Night 5
    {0, 0, 0, 0, 0, 0}  // Night 6
};
private float[,] freddyDifficulties = {
    {11, 0, 1, 0, 0, 0}, // Night 1
    {0, 0, 0, 0, 0, 0}, // Night 2
    {0, 0, 0, 0, 0, 0}, // Night 3
    {0, 0, 0, 0, 0, 0}, // Night 4
    {0, 0, 0, 0, 0, 0}, // Night 5
    {0, 0, 0, 0, 0, 0}  // Night 6
};
private float[,] foxyDifficulties = {
    {0, 0, 1, 0, 0, 0}, // Night 1
    {0, 0, 0, 0, 0, 0}, // Night 2
    {0, 0, 0, 0, 0, 0}, // Night 3
    {0, 0, 0, 0, 0, 0}, // Night 4
    {0, 0, 0, 0, 0, 0}, // Night 5
    {0, 0, 0, 0, 0, 0}  // Night 6
};


    void Start()
    {
        //to get variable "hour"
        gameScript = GetComponent<GameScript>();
        //randMovement
        RandMovement randMovement = GetComponent<RandMovement>();
        randMovement.BonnieRandMove();

        ChicaInKitchen.SetActive(false);
        NightNumber = PlayerPrefs.GetFloat("NightNumber", 1);

        
    }

    // instead of copy and paste the same damn thing in GenNumber
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

    if (NightNumber >= 0 && NightNumber <= 5 && hourIndex >= 0 && hourIndex <= 5)
    {
        BonnieDifficulty = bonnieDifficulties[(int)NightNumber, (int)hourIndex];
        ChicaDifficulty = chicaDifficulties[(int)NightNumber, (int)hourIndex];
        FreddyDifficulty = freddyDifficulties[(int)NightNumber, (int)hourIndex];
        FoxyDifficulty = foxyDifficulties[(int)NightNumber, (int)hourIndex];

        SaveDifficulty();
    }

    else if (NightNumber == 7)
    {
        // Custom night: retrieve saved difficulties
        BonnieDifficulty = PlayerPrefs.GetFloat("BonnieDifficulty", BonnieDifficulty);
        ChicaDifficulty = PlayerPrefs.GetFloat("ChicaDifficulty", ChicaDifficulty);
        FreddyDifficulty = PlayerPrefs.GetFloat("FreddyDifficulty", FreddyDifficulty);
        FoxyDifficulty = PlayerPrefs.GetFloat("FoxyDifficulty", FoxyDifficulty);
    }

    Debug.Log("Hour: " + gameScript.Hour + " NightNumber: " + NightNumber);
    Debug.Log("Saved: Bonnie=" + BonnieDifficulty + ", Chica=" + ChicaDifficulty + ", Freddy=" + FreddyDifficulty + ", Foxy=" + FoxyDifficulty);
    Debug.Log("Bonnie Difficulty (Array): " + bonnieDifficulties[(int)NightNumber - 1, (int)hourIndex]);




        RandMovement randMovement = GetComponent<RandMovement>();
        PlayerPrefs.SetFloat("WhereBonnie", WhereBonnie);
        PlayerPrefs.Save();
        PlayerPrefs.SetFloat("WhereChica", WhereChica);
        PlayerPrefs.Save();
        PlayerPrefs.SetFloat("WhereFoxy", WhereFoxy);
        PlayerPrefs.Save();
        PlayerPrefs.SetFloat("WhereFreddy", WhereFreddy);
        PlayerPrefs.Save();

        if (WhereBonnie >= 2)
        {
            OfficeObject.GetComponent<RandNumberGen>().BonnieLeftStage = true;
        }

        if (WhereChica >= 2)
        {
            OfficeObject.GetComponent<RandNumberGen>().ChicaLeftStage = true;
        }

        if (WhereFreddy >= 2)
        {
            OfficeObject.GetComponent<RandNumberGen>().FreddyLeftStage = true;
        }

        if (BonnieOutsideDoor)
        {
            OfficeObject.GetComponent<Office>().BonnieOutsideDoor = true;
        }

        if (ChicaOutsideDoor)
        {
            if (!BonnieOutsideDoor)
            {
                OfficeObject.GetComponent<Office>().ChicaOutsideDoor = true;
            }
        }

        if (FreddyOutsideDoor)
        {
            if (!ChicaOutsideDoor)
            {
                OfficeObject.GetComponent<Office>().FreddyOutsideDoor = true;
            }
        }

        if (FoxyRunningHallway)
        {
            if (!BonnieOutsideDoor)
            {
                OfficeObject.GetComponent<Office>().FoxyRunningHallway = true;
            }
        }

        //------------------------AI param depend of nights----------------------------------------

//wait 240 seconds
if(GameScript.timeRemaining <= 267.0f)
{
    if (NightNumber == 1)
{
    //-------------------bonnie init------------------------------
    BonnieMovementTime -= Time.deltaTime;

    if (BonnieMovementTime <= 0)
    {
        if (BonnieActive)
        {
            
            if (WhereBonnie == WhereChica)
            {
                
                WhereBonnie += 1;
                bonnieInCount = false;
                
                MoveGlitchUp = 0.5f;
                MoveGlitch.SetActive(true);
                GlitchActive = true;
                LongGlitch = false;
                

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

                if (!camIsUp)
                {
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
    

    if (ChicaMovementTime <= 0)
    {
        if (ChicaActive)
        {
            if(WhereBonnie != 1) //check if bonnie is ou of the stage.
            {
            if (WhereBonnie == 2) // check if bonnie is on Dining Area
            {
                
                WhereChica += 2;
                chicaInCount = false;
                GlitchActive = true;
                MoveGlitch.SetActive(true);

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
                GlitchActive = true;
                MoveGlitch.SetActive(true);

                if (!camIsUp)
                {
                    GlitchActive = false;
                    MoveGlitch.SetActive(false);
                }

                
            }

            }
            

        }
    }
    //-----------------------------------------------
}





}
// ---------------Night 2 param------------------
if(GameScript.timeRemaining <= 300.0f)
{
    if (NightNumber == 2)
{
    
    //-------------------bonnie init------------------------------
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

                if (!camIsUp)
                {
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

    if (ChicaMovementTime <= 0)
    {
        if (ChicaActive)
        {
            if(WhereBonnie != 1) //check if bonnie is out of the stage.
            {
            if (WhereBonnie == 2) // checki if bonnie is on Dining Area
            {
                
                WhereChica += 2;
                chicaInCount = false;
                GlitchActive = true;
                MoveGlitch.SetActive(true);

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
                GlitchActive = true;
                MoveGlitch.SetActive(true);

                if (!camIsUp)
                {
                    GlitchActive = false;
                    MoveGlitch.SetActive(false);
                }

                
            }

            }
            

        }
    }
    //-----------------------------------------------

    //------------Foxy init--------------------------
    if (!camIsUp)
    {
        FoxyMovementTime -= Time.deltaTime;
    }
     if (FoxyMovementTime <= 0)
      {
          if (FoxyActive)
           {
               WhereFoxy += 1;
               foxyInCount = false;
               GlitchActive = true;
              MoveGlitch.SetActive(true);

              if (!camIsUp)
            {
                GlitchActive = false;
                MoveGlitch.SetActive(false);
               }
            
                }
            }

    //-----------------------------------------------
}

}


    
//----------night 3, 4, 5 param------------------------
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
    }

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
