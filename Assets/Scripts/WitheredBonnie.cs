using UnityEngine;

public class WitheredBonnie : MonoBehaviour
{
	public Sprite achievementIcon;

	void Start()
	{
        MedalsManager.medalsManager.UnlockAchievement(Achievements.achievements.WITHEREDBONNIEGAMING);
    }
}
