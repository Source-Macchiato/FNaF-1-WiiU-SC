using UnityEngine;

public class RandNumberGen : MonoBehaviour {

    public GameObject OfficeObject;

    public bool camIsUp = false;

    public double CountDown = 30f;
    public double RandNumberBonnie;
    public double RandNumberChica;
    public double RandNumberFreddy;

    public bool BonnieOutsideLeftDoor = false;
    public bool ChicaOutsideRightDoor = false;
    public bool FreddyOutsideRightDoor = false;

    public bool BonnieLeftStage = false;
    public bool ChicaLeftStage = false;
    public bool FreddyLeftStage = false;

    private Movement movement;

    void Start()
    {
        movement = FindObjectOfType<Movement>();
    }

    void Update()
    {
        CountDown -= Time.deltaTime;

        if (CountDown <= 0)
        {
            GenRandomNumber();
        }
    }

    void GenRandomNumber()
    {
        CountDown = System.Math.Round(Random.Range(30f, 40f), 0);

        if (GameScript.nightNumber == 0)
        {
            if (BonnieLeftStage)
            {
                RandNumberBonnie = System.Math.Round(Random.Range(2f, 6f), 0);
            }
        }

        if (GameScript.nightNumber >= 1)
        {
            if (ChicaLeftStage)
            {
                RandNumberChica = System.Math.Round(Random.Range(2f, 7f), 0);
            }
        }

        if (GameScript.nightNumber >= 2)
        {
            if (FreddyLeftStage)
            {
                RandNumberFreddy = System.Math.Round(Random.Range(1f, 2f), 0);
            }
        }

        ChangePos();
	}

    void ChangePos()
    {

        if (!BonnieOutsideLeftDoor)
        {
            if (RandNumberBonnie == 2)
            {
                movement.bonniePosition = 2;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }

            if (RandNumberBonnie == 3)
            {
                movement.bonniePosition = 3;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }

            if (RandNumberBonnie == 4)
            {
                movement.bonniePosition = 4;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }

            if (RandNumberBonnie == 5)
            {
                movement.bonniePosition = 5;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }

            if (RandNumberBonnie == 6)
            {
                movement.bonniePosition = 6;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }
        }

        if (!ChicaOutsideRightDoor)
        {
            if (RandNumberChica == 2)
            {
                movement.chicaPosition = 2;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }

            if (RandNumberChica == 3)
            {
                movement.chicaPosition = 3;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }

            if (RandNumberChica == 4)
            {
                movement.chicaPosition = 4;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }

            if (RandNumberChica == 5)
            {
                movement.chicaPosition = 5;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }

            if (RandNumberChica == 6)
            {
                movement.chicaPosition = 6;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }

            if (RandNumberChica == 7)
            {
                movement.chicaPosition = 7;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }
        }


        if (!FreddyOutsideRightDoor)
        {
            if (RandNumberFreddy == 1)
            {
                movement.freddyPosition = 1;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }

            if (RandNumberFreddy > 2)
            {
                movement.freddyPosition = 2;

                if (camIsUp)
                {
                    movement.MoveGlitch.SetActive(true);
                    movement.GlitchActive = true;
                }
            }
        }
    }
}