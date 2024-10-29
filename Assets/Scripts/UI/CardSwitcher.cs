using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RTLTMPro;

public class CardSwitcher : MonoBehaviour
{
	public int difficultyId;

	public GameObject selection;
	public string titleName;
	public Sprite coverSprite;
	public string descriptionTranslationId;
	public int minValue;
	public int maxValue;

	[Header("Components")]
	public RTLTextMeshPro tmpTitle;
	public Image cover;
	public I18nTextTranslator descriptionTranslator;
	public RTLTextMeshPro tmpSwitcherValue;

	void Start()
	{
        UpdateCardSwitcher();
	}

	void Update()
	{
		if (EventSystem.current.currentSelectedGameObject == gameObject)
		{
			if (!selection.activeSelf)
			{
				selection.SetActive(true);
			}
		}
		else
		{
			if (selection.activeSelf)
			{
				selection.SetActive(false);
			}
		}
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
		if (tmpTitle.text != titleName)
		{
			tmpTitle.text = titleName;
		}

		// Update cover
		if (cover.sprite != coverSprite)
		{
			cover.sprite = coverSprite;
		}

		// Update description translation
		if (descriptionTranslator.textId != descriptionTranslationId)
		{
			descriptionTranslator.textId = descriptionTranslationId;
		}

		// Update value
        if (tmpSwitcherValue.text != difficultyId.ToString())
        {
            tmpSwitcherValue.text = difficultyId.ToString();
        }
    }
}