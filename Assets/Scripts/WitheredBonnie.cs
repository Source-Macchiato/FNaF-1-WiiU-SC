using UnityEngine;

public class WitheredBonnie : MonoBehaviour
{
	public Sprite achievementIcon;

	void Start()
	{
        if (MedalsManager.medalsManager != null)
		{
            MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.WITHEREDBONNIEGAMING);
        }
    }
}
