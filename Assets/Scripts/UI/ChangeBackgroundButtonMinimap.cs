using UnityEngine;
using UnityEngine.UI;

public class ChangeBackgroundButtonMinimap : MonoBehaviour {

    public Sprite defaultBackgroundSprite;
    public Sprite greenBackgroundSprite;
    public Image[] sourceImages;

    public void ButtonPressed(string cameraName) {
        if (cameraName == "1A")
        {
            foreach (Image image in sourceImages)
            {
                if (image.gameObject.name.Contains("1A-Background"))
                {
                    image.sprite = greenBackgroundSprite;
                }
                else
                {
                    image.sprite = defaultBackgroundSprite;
                }
            }
        }
        else if (cameraName == "1B")
        {
            foreach (Image image in sourceImages)
            {
                if (image.gameObject.name.Contains("1B-Background"))
                {
                    image.sprite = greenBackgroundSprite;
                }
                else
                {
                    image.sprite = defaultBackgroundSprite;
                }
            }
        }
        else if (cameraName == "1C")
        {
            foreach (Image image in sourceImages)
            {
                if (image.gameObject.name.Contains("1C-Background"))
                {
                    image.sprite = greenBackgroundSprite;
                }
                else
                {
                    image.sprite = defaultBackgroundSprite;
                }
            }
        }
        else if (cameraName == "2A")
        {
            foreach (Image image in sourceImages)
            {
                if (image.gameObject.name.Contains("2A-Background"))
                {
                    image.sprite = greenBackgroundSprite;
                }
                else
                {
                    image.sprite = defaultBackgroundSprite;
                }
            }
        }
        else if (cameraName == "2B")
        {
            foreach (Image image in sourceImages)
            {
                if (image.gameObject.name.Contains("2B-Background"))
                {
                    image.sprite = greenBackgroundSprite;
                }
                else
                {
                    image.sprite = defaultBackgroundSprite;
                }
            }
        }
        else if (cameraName == "3")
        {
            foreach (Image image in sourceImages)
            {
                if (image.gameObject.name.Contains("3-Background"))
                {
                    image.sprite = greenBackgroundSprite;
                }
                else
                {
                    image.sprite = defaultBackgroundSprite;
                }
            }
        }
        else if (cameraName == "4A")
        {
            foreach (Image image in sourceImages)
            {
                if (image.gameObject.name.Contains("4A-Background"))
                {
                    image.sprite = greenBackgroundSprite;
                }
                else
                {
                    image.sprite = defaultBackgroundSprite;
                }
            }
        }
        else if (cameraName == "4B")
        {
            foreach (Image image in sourceImages)
            {
                if (image.gameObject.name.Contains("4B-Background"))
                {
                    image.sprite = greenBackgroundSprite;
                }
                else
                {
                    image.sprite = defaultBackgroundSprite;
                }
            }
        }
        else if (cameraName == "5")
        {
            foreach (Image image in sourceImages)
            {
                if (image.gameObject.name.Contains("5-Background"))
                {
                    image.sprite = greenBackgroundSprite;
                }
                else
                {
                    image.sprite = defaultBackgroundSprite;
                }
            }
        }
        else if (cameraName == "6")
        {
            foreach (Image image in sourceImages)
            {
                if (image.gameObject.name.Contains("6-Background"))
                {
                    image.sprite = greenBackgroundSprite;
                }
                else
                {
                    image.sprite = defaultBackgroundSprite;
                }
            }
        }
        else if (cameraName == "7")
        {
            foreach (Image image in sourceImages)
            {
                if (image.gameObject.name.Contains("7-Background"))
                {
                    image.sprite = greenBackgroundSprite;
                }
                else
                {
                    image.sprite = defaultBackgroundSprite;
                }
            }
        }
    }
}
