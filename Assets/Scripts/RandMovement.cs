using UnityEngine;

public class RandMovement : MonoBehaviour
{
    
    public int BonnieRandNumb;
    public int ChicaRandNumb;

    void Start()
    {
        
    }


    public void BonnieRandMove()
    {
         BonnieRandNumb = Random.Range(1, 3); 
    }

    public void ChicaRandMove()
    {
         ChicaRandNumb = Random.Range(1, 3);
    }
}
