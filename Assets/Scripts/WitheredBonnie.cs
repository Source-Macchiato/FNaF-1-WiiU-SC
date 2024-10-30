using UnityEngine;

public class WitheredBonnie : MonoBehaviour
{
	public Sprite achievementIcon;

	void Start()
	{
        MedalsManager.medalsManager.ShowAchievement("Withered Bonnie Gaming", "Find the easter egg.", achievementIcon);
    }
}
