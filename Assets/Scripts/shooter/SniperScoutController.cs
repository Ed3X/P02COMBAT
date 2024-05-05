using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SniperScopeController : MonoBehaviour
{
    public GameObject sniperScope;
    public GameObject awp;
    public Camera playerCamera;
    public RawImage aimDefault;
    private float originalFOV = 45f;
    private float zoomedFOV = 20f;
    private float secondZoomedFOV = 10f; // Nuevo FOV para el segundo zoom
    private bool isZoomed = false; // Flag para controlar si se ha hecho zoom o no
    private bool isSecondZoom = false; // Flag para controlar el segundo zoom

    void Start()
    {
        // Al inicio, asegúrate de que el SniperScope esté oculto
        sniperScope.SetActive(false);
    }

    void Update()
    {
        // Si se hace clic derecho, cambia la visibilidad del SniperScope
        if (Input.GetMouseButtonDown(1))
        {
            if (!isZoomed)
            {
                // Primer zoom
                isZoomed = true;
                playerCamera.fieldOfView = zoomedFOV;
            }
            else if (!isSecondZoom)
            {
                // Segundo zoom
                isSecondZoom = true;
                playerCamera.fieldOfView = secondZoomedFOV;
            }
            else
            {
                // Si ya se hizo el segundo zoom, desactiva el zoom y restaura el FOV original
                isZoomed = false;
                isSecondZoom = false;
                playerCamera.fieldOfView = originalFOV;
            }

            // Cambia la visibilidad del SniperScope
            sniperScope.SetActive(isZoomed || isSecondZoom);

            // Oculta o muestra el GameObject "awp" según el estado del zoom
            awp.SetActive(!(isZoomed || isSecondZoom));

            // Oculta o muestra el Raw Image "AimDefault" según el estado del zoom
            if (aimDefault != null)
            {
                aimDefault.enabled = !(isZoomed || isSecondZoom);
            }
        }
    }
}
