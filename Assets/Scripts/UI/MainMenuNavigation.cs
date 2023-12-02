using UnityEngine;
using UnityEngine.UI;

public class MainMenuNavigation : MonoBehaviour
{

    public Button[] MainMenuButtons;
    public Text[] MainMenuSelectionTexts;

    private int selectedIndex = 0;

    void Start()
    {
        UpdateSelectionTexts();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex - 1 + MainMenuButtons.Length) % MainMenuButtons.Length;
            UpdateSelectionTexts();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + 1) % MainMenuButtons.Length;
            UpdateSelectionTexts();
        }
    }

    void UpdateSelectionTexts()
    {
        for (int i = 0; i < MainMenuButtons.Length; i++)
        {
            MainMenuSelectionTexts[i].gameObject.SetActive(i == selectedIndex);
        }
    }
}