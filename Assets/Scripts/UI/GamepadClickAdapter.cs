using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using WiiU = UnityEngine.WiiU;

public class GamepadClickAdapter : MonoBehaviour
{
    // Résolution des canvas
    private float canvasWidth = 1280f;
    private float canvasHeight = 720f;

    // Résolution du gamepad
    private float gamepadWidth = 854f;
    private float gamepadHeight = 480f;

    // References to WiiU controllers
    WiiU.GamePad gamePad;
    WiiU.Remote remote;

    void Start()
    {
        // Access the WiiU GamePad and Remote
        gamePad = WiiU.GamePad.access;
        remote = WiiU.Remote.Access(0);

        // Disable default system
        EventSystem.current.GetComponent<StandaloneInputModule>().enabled = false;
    }

    void Update()
    {
        // Get the current state of the GamePad and Remote
        WiiU.GamePadState gamePadState = gamePad.state;
        WiiU.RemoteState remoteState = remote.state;

        if (Application.isEditor)
        {
            // Check for mouse click (left button)
            if (Input.GetMouseButtonDown(0))
            {
                // Récupérer la position de la souris
                Vector2 mousePos = Input.mousePosition;

                // Calculer les ratios de résolution de la souris
                float mouseRatioX = canvasWidth / Screen.width;
                float mouseRatioY = canvasHeight / Screen.height;

                // Adapter la position de la souris pour correspondre à la résolution du canvas
                Vector2 adaptedMousePos = new Vector2(
                    mousePos.x * mouseRatioX,
                    mousePos.y * mouseRatioY
                );

                // Convertir les coordonnées de la souris en Raycast pour interaction UI
                PointerEventData mousePointerData = new PointerEventData(EventSystem.current);
                mousePointerData.position = adaptedMousePos;

                // Créer une liste pour les résultats du Raycast
                List<RaycastResult> mouseRaycastResults = new List<RaycastResult>();

                // Effectuer le raycast UI
                EventSystem.current.RaycastAll(mousePointerData, mouseRaycastResults);

                // Si un élément UI est touché
                if (mouseRaycastResults.Count > 0)
                {
                    Debug.Log("UI Element clicked with mouse: " + mouseRaycastResults[0].gameObject.name);

                    // Simuler un clic sur l'élément UI avec la souris
                    ExecuteEvents.Execute(mouseRaycastResults[0].gameObject, mousePointerData, ExecuteEvents.pointerClickHandler);
                }
            }
        }
        else
        {
            // Check if is touching screen
            if (gamePadState.touch.touch == 1)
            {
                // Récupérer la position du clic à partir de gamePadState
                Vector2 touchPos = new Vector2(gamePadState.touch.x, gamePadState.touch.y);

                // Calculer les ratios de résolution
                float ratioX = canvasWidth / gamepadWidth;
                float ratioY = canvasHeight / gamepadHeight;

                // Adapter la position du clic pour correspondre à la résolution du canvas
                Vector2 adaptedTouchPos = new Vector2(
                    touchPos.x * ratioX,
                    touchPos.y * ratioY
                );

                // Convertir les coordonnées du clic en Raycast pour interaction UI
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = adaptedTouchPos;

                // Créer une liste pour les résultats du Raycast
                List<RaycastResult> raycastResults = new List<RaycastResult>();

                // Effectuer le raycast UI
                EventSystem.current.RaycastAll(pointerData, raycastResults);

                // Si un élément UI est touché
                if (raycastResults.Count > 0)
                {
                    Debug.Log("UI Element clicked: " + raycastResults[0].gameObject.name);

                    // Simuler un clic sur l'élément UI
                    ExecuteEvents.Execute(raycastResults[0].gameObject, pointerData, ExecuteEvents.pointerClickHandler);
                }
            }
        }
    }
}
