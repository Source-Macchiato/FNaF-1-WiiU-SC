using UnityEngine;

public class ChangeBackgroundButtonMinimap : MonoBehaviour
{
    public GameObject[] camerasBackground;
    private string currentCameraName;
    public bool canChangeState = false;

    void Update()
    {
        if (currentCameraName != null)
        {
            if (canChangeState)
            {
                foreach (GameObject background in camerasBackground)
                {
                    Animator animator = background.GetComponent<Animator>();

                    if (background.name == (currentCameraName + "-Background"))
                    {
                        animator.Play("Blink");
                    }
                    else
                    {
                        animator.Play("Idle");
                    }
                }

                canChangeState = false;
            }

            /*if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {

            }*/
        }
    }

    public void ButtonPressed(string cameraName)
    {
        if (currentCameraName != cameraName)
        {
            currentCameraName = cameraName;

            canChangeState = true;
        }
    }
}