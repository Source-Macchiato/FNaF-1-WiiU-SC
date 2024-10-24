using UnityEngine;
using WiiU = UnityEngine.WiiU;

public class MoveInOffice : MonoBehaviour
{
    public GameObject OfficeImage;

    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    private const float speed = 5f;
    private const float leftEdge = 160f;
    private const float rightEdge = -130f;
    private float stickDeadzone = 0.19f;

    void Start()
	{
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);
    }
	
	void Update()
	{
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        // Gamepad
        if (gamePadState.gamePadErr == WiiU.GamePadError.None)
        {
            Vector2 leftStickGamepad = gamePadState.lStick;

            if (Mathf.Abs(leftStickGamepad.y) > stickDeadzone)
            {
                if (leftStickGamepad.y > 0)
                {
                    MoveLeft();
                }
                else
                {
                    MoveRight();
                }
            }

            if (gamePadState.IsPressed(WiiU.GamePadButton.Left))
            {
                MoveLeft();
            }
            else if (gamePadState.IsPressed(WiiU.GamePadButton.Right))
            {
                MoveRight();
            }
        }

        // Remotes
        switch (remoteState.devType)
        {
            case WiiU.RemoteDevType.ProController:
                Vector2 leftStickProController = remoteState.pro.leftStick;

                if (Mathf.Abs(leftStickProController.y) > stickDeadzone)
                {
                    if (leftStickProController.y > 0)
                    {
                        MoveLeft();
                    }
                    else
                    {
                        MoveRight();
                    }
                }

                if (remoteState.pro.IsPressed(WiiU.ProControllerButton.Left))
                {
                    MoveLeft();
                }
                else if (remoteState.pro.IsPressed(WiiU.ProControllerButton.Right))
                {
                    MoveRight();
                }
                break;
            case WiiU.RemoteDevType.Classic:
                Vector2 leftStickClassicController = remoteState.classic.leftStick;

                if (Mathf.Abs(leftStickClassicController.y) > stickDeadzone)
                {
                    if (leftStickClassicController.y > 0)
                    {
                        MoveLeft();
                    }
                    else
                    {
                        MoveRight();
                    }
                }

                if (remoteState.classic.IsPressed(WiiU.ClassicButton.Left))
                {
                    MoveLeft();
                }
                else if (remoteState.classic.IsPressed(WiiU.ClassicButton.Right))
                {
                    MoveRight();
                }
                break;
            default:
                Vector2 stickNunchuk = remoteState.nunchuk.stick;

                if (Mathf.Abs(stickNunchuk.y) > stickDeadzone)
                {
                    if (stickNunchuk.y > 0)
                    {
                        MoveLeft();
                    }
                    else
                    {
                        MoveRight();
                    }
                }

                // Pointer
                Vector2 pointerPosition = remoteState.pos;
                pointerPosition.x = ((pointerPosition.x + 1.0f) / 2.0f) * WiiU.Core.GetScreenWidth(WiiU.DisplayIndex.TV);

                if (remoteState.IsPressed(WiiU.RemoteButton.Left) || pointerPosition.x < 300f)
                {
                    MoveLeft();
                }
                else if (remoteState.IsPressed(WiiU.RemoteButton.Right) || pointerPosition.x > WiiU.Core.GetScreenWidth(WiiU.DisplayIndex.TV) - 300f)
                {
                    MoveRight();
                }
                break;
        }

        // Keyboard
        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x < 300f)
            {
                MoveLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x > WiiU.Core.GetScreenWidth(WiiU.DisplayIndex.TV) - 300f)
            {
                MoveRight();
            }
        }
    }

    private void MoveLeft()
    {
        OfficeImage.transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (OfficeImage.transform.localPosition.x >= leftEdge)
        {
            OfficeImage.transform.localPosition = new Vector3(leftEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
        }
    }

    private void MoveRight()
    {
        OfficeImage.transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (OfficeImage.transform.localPosition.x <= rightEdge)
        {
            OfficeImage.transform.localPosition = new Vector3(rightEdge, OfficeImage.transform.localPosition.y, OfficeImage.transform.localPosition.z);
        }
    }
}
