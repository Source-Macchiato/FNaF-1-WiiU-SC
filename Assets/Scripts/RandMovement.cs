using UnityEngine;

public class RandMovement : MonoBehaviour
{
    
    public int BonnieRandNumb;
    public int ChicaRandNumb;
    public int FreddyRandNumb;
    public int FoxyRandNumb;

    void Start()
    {
        
    }
    public void BonnieRandMove() //generate the Random numbers to compare it to the AI Level
    {
     BonnieRandNumb = Random.Range(1, 100); 
    }
    public void ChicaRandMove()
    {
     ChicaRandNumb = Random.Range(1, 100);
    }
    public void FreddyRandMove()
    {
     FreddyRandNumb = Random.Range(1,100);
    }
    public void FoxyRandMove()
    {
     FoxyRandNumb = Random.Range(1, 100);
    }
}
