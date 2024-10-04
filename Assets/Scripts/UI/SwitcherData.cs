using UnityEngine;
using UnityEngine.UI;

public class SwitcherData : MonoBehaviour
{
    public int currentOptionId;
    public string[] optionsName;

    private Text optionText;

    void Start()
    {
        optionText = transform.Find("Text").GetComponent<Text>();
    }

    void Update()
    {
        if (optionText.text != optionsName[currentOptionId] || optionText.text == null)
        {
            optionText.text = optionsName[currentOptionId];
        }
    }
}