using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;

public class CardSwitcher : MonoBehaviour
{
	public int difficultyId;

	public string titleName;
	public Sprite coverSprite;
	public int minValue;
	public int maxValue;

	[Header("Components")]
	public Text textTitle;
	public Image cover;
	public RTLTextMeshPro tmpDescription;
	public RTLTextMeshPro tmpSwitcherValue;

	void Start()
	{
        UpdateCardSwitcher();
	}

	public void IncreaseDifficulty()
	{
		if (difficultyId >= minValue && difficultyId < maxValue)
		{
            difficultyId++;

            UpdateCardSwitcher();
        }
	}

	public void DecreaseDifficulty()
	{
		if (difficultyId > minValue && difficultyId <= maxValue)
		{
			difficultyId--;

            UpdateCardSwitcher();
		}
	}

	public void UpdateCardSwitcher()
	{
		// Update title
		if (textTitle.text != titleName)
		{
			textTitle.text = titleName;
		}

		// Update cover
		if (cover.sprite != coverSprite)
		{
			cover.sprite = coverSprite;
		}

		// Update value
        if (tmpSwitcherValue.text != difficultyId.ToString())
        {
            tmpSwitcherValue.text = difficultyId.ToString();
        }
    }
}