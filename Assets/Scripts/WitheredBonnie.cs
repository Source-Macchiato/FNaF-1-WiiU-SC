using UnityEngine;

public class WitheredBonnie : MonoBehaviour
{
	void Start()
	{
        if (MedalsManager.medalsManager != null)
		{
            MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.WITHEREDBONNIEGAMING);
        }
    }
}
