using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonBackground : MonoBehaviour {

    public Sprite defaultBackgroundSprite;
    public Sprite greenBackgroundSprite;
    public Image buttonSourceImage;
    public Image[] otherButtons;

    public void ButtonPressed() {
        buttonSourceImage.sprite = greenBackgroundSprite;
        foreach (Image button in otherButtons)
        {
            button.sprite = defaultBackgroundSprite;
        }
    }
}
