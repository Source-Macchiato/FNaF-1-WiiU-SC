using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackgroundButtonMinimap : MonoBehaviour
{
    public Sprite defaultBackgroundSprite;
    public Sprite greenBackgroundSprite;
    public Image[] sourceImages;

    private bool isBlinking = false;
    public string currentCameraName;

    void Start()
    {
        StartCoroutine(BlinkBackground(currentCameraName));
    }

    public void ButtonPressed(string cameraName)
    {
        if (currentCameraName == cameraName)
        {
            return;
        }

        if (isBlinking)
        {
            StopCoroutine("BlinkBackground");
            isBlinking = false;
        }

        StartCoroutine(BlinkBackground(cameraName));
    }

    IEnumerator BlinkBackground(string cameraName)
    {
        if (currentCameraName != null)
        {
            currentCameraName = cameraName;

            while (true)
            {
                if (!isBlinking)
                {
                    isBlinking = true;

                    foreach (Image image in sourceImages)
                    {
                        if (image.gameObject.name.Contains(cameraName + "-Background"))
                        {
                            image.sprite = greenBackgroundSprite;
                        }
                        else
                        {
                            image.sprite = defaultBackgroundSprite;
                        }
                    }

                    yield return new WaitForSeconds(0.5f);

                    foreach (Image image in sourceImages)
                    {
                        if (image.gameObject.name.Contains(cameraName + "-Background"))
                        {
                            image.sprite = defaultBackgroundSprite;
                        }
                    }

                    isBlinking = false;
                    yield return new WaitForSeconds(0.5f);
                }

                if (currentCameraName != cameraName)
                {
                    break;
                }

                yield return null;
            }
        }
    }
}