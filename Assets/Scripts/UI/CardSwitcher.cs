using UnityEngine;
using RTLTMPro;

public class CardSwitcher : MonoBehaviour
{
	public int difficultyId;

	public string titleName;
	public Sprite coverSprite;
	public int minValue;
	public int maxValue;

	public RTLTextMeshPro tmpSwitcherValue;

	void Start()
	{
		UpdateTextValue();
	}

	public void IncreaseDifficulty()
	{
		if (difficultyId >= minValue && difficultyId < maxValue)
		{
            difficultyId++;

			UpdateTextValue();
        }
	}

	public void DecreaseDifficulty()
	{
		if (difficultyId > minValue && difficultyId <= maxValue)
		{
			difficultyId--;

			UpdateTextValue();
		}
	}

	public void UpdateTextValue()
	{
        if (tmpSwitcherValue.text != difficultyId.ToString())
        {
            tmpSwitcherValue.text = difficultyId.ToString();
        }
    }
}